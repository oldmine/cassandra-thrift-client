using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

using SKBKontur.Cassandra.CassandraClient.Core.GenericPool.Exceptions;
using SKBKontur.Cassandra.CassandraClient.Core.GenericPool.Utils;

using Vostok.Logging.Abstractions;

namespace SKBKontur.Cassandra.CassandraClient.Core.GenericPool
{
    internal class Pool<T> : IDisposable where T : class, IDisposable, ILiveness
    {
        public Pool(Func<Pool<T>, T> itemFactory, ILog logger)
        {
            this.itemFactory = itemFactory;
            this.logger = logger;
        }

        public void Dispose()
        {
            var items = freeItems.Select(x => x.Item).Union(busyItems.Keys).ToArray();
            foreach (var item in items)
                item.Dispose();
        }

        public T Acquire()
        {
            return TryAcquireExists(out var result) ? result : AcquireNew();
        }

        public bool TryAcquireExists(out T result)
        {
            while (TryPopFreeItem(out result))
            {
                if (!result.IsAlive)
                {
                    result.Dispose();
                    continue;
                }
                MarkItemAsBusy(result);
                return true;
            }
            return false;
        }

        public void Release(T item)
        {
            if (!busyItems.TryRemove(item, out var dummy))
                throw new FailedReleaseItemException(item.ToString());
            Interlocked.Decrement(ref busyItemCount);
            freeItems.Push(new FreeItemInfo(item, DateTime.UtcNow));
        }

        public T AcquireNew()
        {
            var result = itemFactory(this);
            MarkItemAsBusy(result);
            return result;
        }

        public int RemoveIdleItems(TimeSpan minIdleTimeSpan)
        {
            unusedItemCollectorLock.EnterWriteLock();
            try
            {
                var result = 0;
                var timer = Stopwatch.StartNew();
                try
                {
                    var tempStack = new Stack<FreeItemInfo>();
                    var now = DateTime.UtcNow;

                    while (freeItems.TryPop(out var item))
                    {
                        if (now - item.IdleTime >= minIdleTimeSpan)
                        {
                            result++;
                            item.Item.Dispose();
                            continue;
                        }
                        tempStack.Push(item);
                    }
                    while (tempStack.Count > 0)
                        freeItems.Push(tempStack.Pop());
                    return result;
                }
                finally
                {
                    if (timer.ElapsedMilliseconds > 1)
                        logger.Warn("RemoveIdleItems from pool: Time={0}ms, RemovedItemsCount={1}", timer.ElapsedMilliseconds, result);
                }
            }
            finally
            {
                unusedItemCollectorLock.ExitWriteLock();
            }
        }

        public void Remove(T item)
        {
            if (!busyItems.TryRemove(item, out var dummy))
                throw new RemoveFromPoolFailedException("Cannot find item to remove in busy items. This item does not belong in this pool or in released state.");
            Interlocked.Decrement(ref busyItemCount);
        }

        public int TotalCount => FreeItemCount + BusyItemCount;
        public int FreeItemCount => freeItems.Count;
        public int BusyItemCount => busyItemCount;

        private bool TryPopFreeItem(out T item)
        {
            FreeItemInfo freeItemInfo;
            unusedItemCollectorLock.EnterReadLock();
            bool result;
            try
            {
                result = freeItems.TryPop(out freeItemInfo);
            }
            finally
            {
                unusedItemCollectorLock.ExitReadLock();
            }
            item = result ? freeItemInfo.Item : null;
            return result;
        }

        private void MarkItemAsBusy(T result)
        {
            if (!busyItems.TryAdd(result, new object()))
                throw new ItemInPoolCollisionException();
            Interlocked.Increment(ref busyItemCount);
        }

        private int busyItemCount;
        private readonly ReaderWriterLockSlim unusedItemCollectorLock = new ReaderWriterLockSlim();
        private readonly Func<Pool<T>, T> itemFactory;
        private readonly ILog logger;
        private readonly ConcurrentStack<FreeItemInfo> freeItems = new ConcurrentStack<FreeItemInfo>();
        private readonly ConcurrentDictionary<T, object> busyItems = new ConcurrentDictionary<T, object>(ObjectReferenceEqualityComparer<T>.Default);

        private class FreeItemInfo
        {
            public FreeItemInfo(T item, DateTime idleTime)
            {
                Item = item;
                IdleTime = idleTime;
            }

            public T Item { get; }
            public DateTime IdleTime { get; }
        }
    }
}
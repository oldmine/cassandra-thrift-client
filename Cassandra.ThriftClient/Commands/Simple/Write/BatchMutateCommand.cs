using System.Collections.Generic;
using System.Linq;

using Apache.Cassandra;

using SkbKontur.Cassandra.ThriftClient.Abstractions;
using SkbKontur.Cassandra.ThriftClient.Abstractions.Internal;
using SkbKontur.Cassandra.ThriftClient.Commands.Base;
using SkbKontur.Cassandra.TimeBasedUuid.Bits;

using Vostok.Logging.Abstractions;

using ConsistencyLevel = Apache.Cassandra.ConsistencyLevel;

namespace SkbKontur.Cassandra.ThriftClient.Commands.Simple.Write
{
    internal class BatchMutateCommand : KeyspaceColumnFamilyDependantCommandBase, ISimpleCommand
    {
        public BatchMutateCommand(string keyspace, string columnFamily, ConsistencyLevel consistencyLevel, Dictionary<string, Dictionary<byte[], List<IMutation>>> mutations)
            : base(keyspace, columnFamily)
        {
            this.consistencyLevel = consistencyLevel;
            this.mutations = mutations;
        }

        public override void Execute(Apache.Cassandra.Cassandra.Client cassandraClient, ILog logger)
        {
            var mutationMap = TranslateMutations();
            cassandraClient.batch_mutate(mutationMap, consistencyLevel);
        }

        private Dictionary<byte[], Dictionary<string, List<Mutation>>> TranslateMutations()
        {
            var result = new Dictionary<byte[], Dictionary<string, List<Mutation>>>(ByteArrayComparer.Instance);
            foreach (var mutationsPerColumnFamily in mutations)
            {
                foreach (var mutationsPerRow in mutationsPerColumnFamily.Value)
                {
                    var mutationList = mutationsPerRow.Value.Select(mutation => mutation.ToCassandraMutation()).ToList();
                    if (!result.ContainsKey(mutationsPerRow.Key))
                        result.Add(mutationsPerRow.Key, new Dictionary<string, List<Mutation>>());
                    if (!result[mutationsPerRow.Key].ContainsKey(mutationsPerColumnFamily.Key))
                        result[mutationsPerRow.Key].Add(mutationsPerColumnFamily.Key, new List<Mutation>());
                    result[mutationsPerRow.Key][mutationsPerColumnFamily.Key].AddRange(mutationList);
                }
            }
            return result;
        }

        public int QueriedPartitionsCount { get { return mutations.Sum(columnFamilyMutations => columnFamilyMutations.Value.Count); } }
        public long? ResponseSize => null;

        private readonly ConsistencyLevel consistencyLevel;
        private readonly Dictionary<string, Dictionary<byte[], List<IMutation>>> mutations;
    }
}
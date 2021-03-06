using JetBrains.Annotations;

using SkbKontur.Cassandra.ThriftClient.Abstractions;
using SkbKontur.Cassandra.ThriftClient.Commands.Base;

using Vostok.Logging.Abstractions;

using ConsistencyLevel = Apache.Cassandra.ConsistencyLevel;

namespace SkbKontur.Cassandra.ThriftClient.Commands.Simple.Write
{
    internal class InsertCommand : KeyspaceColumnFamilyDependantCommandBase, ISinglePartitionQuery, ISimpleCommand
    {
        public InsertCommand(string keyspace, string columnFamily, byte[] rowKey, ConsistencyLevel consistencyLevel, RawColumn column)
            : base(keyspace, columnFamily)
        {
            PartitionKey = rowKey;
            this.consistencyLevel = consistencyLevel;
            this.column = column;
        }

        [NotNull]
        public byte[] PartitionKey { get; }

        public int QueriedPartitionsCount => 1;
        public long? ResponseSize => null;

        public override void Execute(Apache.Cassandra.Cassandra.Client cassandraClient, ILog logger)
        {
            cassandraClient.insert(PartitionKey, BuildColumnParent(), column.ToCassandraColumn(), consistencyLevel);
        }

        private readonly ConsistencyLevel consistencyLevel;
        private readonly RawColumn column;
    }
}
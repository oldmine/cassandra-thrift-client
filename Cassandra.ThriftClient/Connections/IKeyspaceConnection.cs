using SkbKontur.Cassandra.ThriftClient.Abstractions;

namespace SkbKontur.Cassandra.ThriftClient.Connections
{
    public interface IKeyspaceConnection
    {
        void RemoveColumnFamily(string columnFamily);
        void AddColumnFamily(string columnFamilyName);
        void UpdateColumnFamily(ColumnFamily columnFamily);
        void AddColumnFamily(ColumnFamily columnFamily);
        Keyspace DescribeKeyspace();
    }
}
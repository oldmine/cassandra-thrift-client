namespace SkbKontur.Cassandra.ThriftClient.Abstractions
{
    public class DataCenterReplicationFactor
    {
        public DataCenterReplicationFactor(string dataCenterName, int replicationFactor)
        {
            DataCenterName = dataCenterName;
            ReplicationFactor = replicationFactor;
        }

        public string DataCenterName { get; }
        public int ReplicationFactor { get; }
    }
}
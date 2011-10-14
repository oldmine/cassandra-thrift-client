using System;
using System.Collections.Generic;
using System.Linq;

using SKBKontur.Cassandra.CassandraClient.Abstractions;
using SKBKontur.Cassandra.CassandraClient.Clusters;
using SKBKontur.Cassandra.CassandraClient.Connections;
using SKBKontur.Cassandra.FunctionalTests.Tests;
using SKBKontur.Cassandra.StorageCore;

namespace SKBKontur.Cassandra.FunctionalTests.StorageCoreTests
{
    public static class CassandraInitializer
    {
        public static void CreateNewKeyspace(ICassandraCluster cassandraCluster,
                                             IColumnFamilyRegistry columnFamilyRegistry)
        {
            if(isInitialized)
            {
                ClearKeyspaceFast(cassandraCluster, OurKeyspace);
                return;
            }

            Keyspace keySpace = GetKeyspace(columnFamilyRegistry);
            bool wasOurKeyspace = false;
            var clusterConnection = cassandraCluster.RetrieveClusterConnection();
            var keyspaces = clusterConnection.RetrieveKeyspaces();
            foreach(var keyspace in keyspaces)
            {
                if(string.Compare(keySpace.Name, keyspace.Name, StringComparison.OrdinalIgnoreCase) == 0)
                    wasOurKeyspace = true;
            }
            if(!wasOurKeyspace)
                AddKeyspace(cassandraCluster, keySpace);
            else
                UpdateKeyspace(cassandraCluster, keySpace);

            OurKeyspace = keySpace;
            isInitialized = true;
        }

        private static void AddKeyspace(ICassandraCluster cassandraCluster, Keyspace keyspace)
        {
            var clusterConnection = cassandraCluster.RetrieveClusterConnection();
            clusterConnection.AddKeyspace(keyspace);
        }

        private static void UpdateKeyspace(ICassandraCluster cassandraCluster, Keyspace keyspace)
        {
            IDictionary<string, int> truncatedCfs = ClearKeyspace(cassandraCluster, keyspace.Name);
            var clusterConnection = cassandraCluster.RetrieveKeyspaceConnection(keyspace.Name);
            foreach(var cf in keyspace.ColumnFamilies)
            {
                if(!truncatedCfs.ContainsKey(cf.Key))
                    clusterConnection.AddColumnFamily(cf.Value);
                else
                {
                    ColumnFamily columnFamily = cf.Value;
                    columnFamily.Id = truncatedCfs[cf.Key];
                    clusterConnection.UpdateColumnFamily(columnFamily);
                }
            }
        }

        private static void ClearKeyspaceFast(ICassandraCluster cassandraCluster, Keyspace keyspace)
        {
            foreach(var cf in keyspace.ColumnFamilies)
            {
                IColumnFamilyConnection columnFamilyConnection = cassandraCluster.RetrieveColumnFamilyConnection(keyspace.Name, cf.Key);
                columnFamilyConnection.Truncate();
            }
        }

        private static IDictionary<string, int> ClearKeyspace(ICassandraCluster cassandraCluster, string keyspaceName)
        {
            var truncatedColumnFamilies = new Dictionary<string, int>();
            Keyspace keyspace;
            var conn = cassandraCluster.RetrieveKeyspaceConnection(keyspaceName);
            keyspace = conn.DescribeKeyspace();
            foreach(var cf in keyspace.ColumnFamilies)
            {
                truncatedColumnFamilies.Add(cf.Key, cf.Value.Id);
                var columnFamilyConnection = cassandraCluster.RetrieveColumnFamilyConnection(keyspaceName, cf.Key);
                columnFamilyConnection.Truncate();
            }
            return truncatedColumnFamilies;
        }

        private static Keyspace GetKeyspace(IColumnFamilyRegistry columnFamilyRegistry)
        {
            var columnFamilies = new Dictionary<string, ColumnFamily>();
            foreach(var columnFamily in columnFamilyRegistry.GetColumnFamilyNames())
            {
                columnFamilies.Add(columnFamily, new ColumnFamily
                    {
                        Name = columnFamily,
                        Indexes =
                            columnFamilyRegistry.GetIndexDefinitions(columnFamily).
                            ToList(),
                        RowCacheSize = 100
                    });
            }
            return new Keyspace
                {
                    ColumnFamilies = columnFamilies,
                    Name = Constants.KeyspaceName,
                    ReplicaPlacementStrategy =
                        "org.apache.cassandra.locator.SimpleStrategy",
                    ReplicationFactor = 1
                };
        }

        private static Keyspace OurKeyspace { get; set; }
        private static bool isInitialized;
    }
}
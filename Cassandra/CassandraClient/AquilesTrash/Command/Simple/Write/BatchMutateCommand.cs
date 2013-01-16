﻿using System.Collections.Generic;
using System.Linq;

using Apache.Cassandra;

using SKBKontur.Cassandra.CassandraClient.Abstractions.Internal;
using SKBKontur.Cassandra.CassandraClient.AquilesTrash.Command.Base;

namespace SKBKontur.Cassandra.CassandraClient.AquilesTrash.Command.Simple.Write
{
    internal class BatchMutateCommand : KeyspaceColumnFamilyDependantCommandBase
    {
        public BatchMutateCommand(string keyspace, string columnFamily, ConsistencyLevel consistencyLevel, Dictionary<string, Dictionary<byte[], List<IMutation>>> mutations)
            : base(keyspace, columnFamily)
        {
            this.consistencyLevel = consistencyLevel;
            this.mutations = mutations;
        }

        public override void Execute(Apache.Cassandra.Cassandra.Client cassandraClient)
        {
            var mutationMap = TranslateMutations();
            cassandraClient.batch_mutate(mutationMap, consistencyLevel);
        }

        private Dictionary<byte[], Dictionary<string, List<Mutation>>> TranslateMutations()
        {
            var result = new Dictionary<byte[], Dictionary<string, List<Mutation>>>(ByteArrayEqualityComparer.SimpleComparer);
            foreach(var mutationsPerColumnFamily in mutations)
            {
                foreach(var mutationsPerRow in mutationsPerColumnFamily.Value)
                {
                    var mutationList = mutationsPerRow.Value.Select(mutation => mutation.ToCassandraMutation()).ToList();
                    if(!result.ContainsKey(mutationsPerRow.Key))
                        result.Add(mutationsPerRow.Key, new Dictionary<string, List<Mutation>>());
                    if(!result[mutationsPerRow.Key].ContainsKey(mutationsPerColumnFamily.Key))
                        result[mutationsPerRow.Key].Add(mutationsPerColumnFamily.Key, new List<Mutation>());
                    result[mutationsPerRow.Key][mutationsPerColumnFamily.Key].AddRange(mutationList);
                }
            }
            return result;
        }

        private readonly ConsistencyLevel consistencyLevel;
        private readonly Dictionary<string, Dictionary<byte[], List<IMutation>>> mutations;
    }
}
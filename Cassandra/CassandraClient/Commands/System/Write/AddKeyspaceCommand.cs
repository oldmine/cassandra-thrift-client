﻿using SKBKontur.Cassandra.CassandraClient.Abstractions;
using SKBKontur.Cassandra.CassandraClient.Commands.Base;

namespace SKBKontur.Cassandra.CassandraClient.Commands.System.Write
{
    internal class AddKeyspaceCommand : CommandBase, ISchemeUpdateCommand
    {
        public AddKeyspaceCommand(Keyspace keyspaceDefinition)
        {
            this.keyspaceDefinition = keyspaceDefinition;
        }

        public override void Execute(Apache.Cassandra.Cassandra.Client cassandraClient)
        {
            Output = cassandraClient.system_add_keyspace(keyspaceDefinition.ToCassandraKsDef());
        }

        public string Output { get; private set; }
        public override bool IsFierce { get { return true; } }
        private readonly Keyspace keyspaceDefinition;
    }
}
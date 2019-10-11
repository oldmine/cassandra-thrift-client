﻿using System;

namespace SKBKontur.Cassandra.CassandraClient.Exceptions
{
    public class CassandraClientAuthorizationException : CassandraClientException
    {
        internal CassandraClientAuthorizationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public override bool IsCorruptConnection => false;
        public override bool ReduceReplicaLive => false;
        public override bool UseAttempts => false;
    }
}
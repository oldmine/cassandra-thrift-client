﻿using System;

namespace SkbKontur.Cassandra.ThriftClient.Exceptions
{
    public class CassandraClientAuthenticationException : CassandraClientException
    {
        internal CassandraClientAuthenticationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public override bool IsCorruptConnection => false;
        public override bool ReduceReplicaLive => false;
        public override bool UseAttempts => false;
    }
}
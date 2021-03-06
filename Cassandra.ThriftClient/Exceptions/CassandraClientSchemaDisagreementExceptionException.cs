﻿using System;

namespace SkbKontur.Cassandra.ThriftClient.Exceptions
{
    public class CassandraClientSchemaDisagreementException : CassandraClientException
    {
        internal CassandraClientSchemaDisagreementException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public override bool IsCorruptConnection => false;
        public override bool ReduceReplicaLive => false;
        public override bool UseAttempts => false;
    }
}
﻿using System;
using Apache.Cassandra;

using CassandraClient.AquilesTrash.Exceptions;

namespace CassandraClient.AquilesTrash.Command
{
    /// <summary>
    /// Command to describe snitch from the cluster
    /// </summary>
    public class DescribeSnitchCommand : CassandraClient.AquilesTrash.Command.AbstractCommand, IAquilesCommand
    {
        /// <summary>
        /// get return value
        /// </summary>
        public String Output
        {
            get;
            private set;
        }

        #region IAquilesSystemCommand Members

        /// <summary>
        /// Executes a "describe_snitch" over the connection.
        /// </summary>
        /// <param name="cassandraClient">opened Thrift client</param>
        public void Execute(Cassandra.Client cassandraClient)
        {
            this.Output = null;
            this.Output = cassandraClient.describe_snitch();
        }

        /// <summary>
        /// Validate the input parameters. 
        /// Throws <see cref="AquilesCommandParameterException"/>  in case there is some malformed or missing input parameters
        /// </summary>
        public void ValidateInput()
        {
            // DO NOTHING
        }

        #endregion

    }
}
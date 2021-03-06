using System;

using JetBrains.Annotations;

using SkbKontur.Cassandra.ThriftClient.Abstractions;
using SkbKontur.Cassandra.ThriftClient.Clusters;
using SkbKontur.Cassandra.ThriftClient.Core.GenericPool;
using SkbKontur.Cassandra.ThriftClient.Core.Metrics;
using SkbKontur.Cassandra.ThriftClient.Exceptions;

using Vostok.Logging.Abstractions;

namespace SkbKontur.Cassandra.ThriftClient.Core
{
    internal class SimpleCommandExecutor : CommandExecutorBase<ISimpleCommand>
    {
        public SimpleCommandExecutor([NotNull] IPoolSet<IThriftConnection, string> connectionPool, [NotNull] ICassandraClusterSettings settings, ILog logger)
            : base(connectionPool, settings)
        {
            if (settings.Attempts <= 0)
                throw new InvalidOperationException($"settings.Attempts <= 0 for: {settings}");
            this.logger = logger;
        }

        public override sealed void Execute([NotNull] Func<int, ISimpleCommand> createCommand)
        {
            var attempt = 0;
            var command = createCommand(attempt);
            var metrics = command.GetMetrics(settings);
            using (metrics.NewTotalContext())
            {
                while (true)
                {
                    try
                    {
                        ExecuteCommand(command, metrics);
                        metrics.RecordCommandExecutionInfo(command);
                        break;
                    }
                    catch (CassandraClientException exception)
                    {
                        metrics.RecordError(exception);
                        logger.Warn(exception, "Attempt {0} failed", attempt);
                        if (!exception.UseAttempts)
                            throw;
                        if (attempt == 0)
                            metrics.RecordRetry();
                        if (++attempt == settings.Attempts)
                            throw new CassandraAttemptsException(settings.Attempts, exception);
                        command = createCommand(attempt);
                    }
                }
            }
        }

        private readonly ILog logger;
    }
}
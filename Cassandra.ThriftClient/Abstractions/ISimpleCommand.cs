﻿namespace SkbKontur.Cassandra.ThriftClient.Abstractions
{
    internal interface ISimpleCommand : ICommand
    {
        int QueriedPartitionsCount { get; }
        long? ResponseSize { get; }
    }
}
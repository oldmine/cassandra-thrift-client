﻿using System.Collections.Generic;

using SKBKontur.Cassandra.CassandraClient.Abstractions;

namespace SKBKontur.Cassandra.CassandraClient.Connections
{
    public interface IColumnFamilyConnection
    {
        bool IsRowExist(string key);
        void DeleteRows(string[] keys, long? timestamp = null, int batchSize = 1000);
        void DeleteRow(string key, long ? timestamp = null);
        void AddColumn(string key, Column column);
        Column GetColumn(string key, string columnName);
        bool TryGetColumn(string key, string columnName, out Column result);
        void DeleteBatch(string key, IEnumerable<string> columnNames, long? timestamp = null);
        void AddBatch(string key, IEnumerable<Column> columns);
        void BatchInsert(IEnumerable<KeyValuePair<string, IEnumerable<Column>>> data);
        void BatchDelete(IEnumerable<KeyValuePair<string, IEnumerable<string>>> data, long? timestamp = null);
        List<KeyValuePair<string, Column[]>> GetRows(IEnumerable<string> keys, string startColumnName, int count);
        string[] GetRowsWhere(string exclusiveStartKey, int count, IndexExpression[] conditions, string[] columns);
        string[] GetRowsWithColumnValue(int maximalCount, string key, byte[] value);
        void Truncate();
        Column[] GetColumns(string key, string exclusiveStartColumnName, int count);
        IEnumerable<Column> GetRow(string key, int batchSize=1000);
        IEnumerable<Column> GetRow(string key, string exclusiveStartColumnName, int batchSize = 1000);
        string[] GetKeys(string exclusiveStartKey, int count);
        IEnumerable<string> GetKeys(int batchSize=1000);
        int GetCount(string key);
        Dictionary<string, int> GetCounts(IEnumerable<string> keys);
    }
}
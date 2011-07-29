﻿using System;
using System.Collections.Generic;

using CassandraClient.Abstractions;

namespace CassandraClient.Connections
{
    public interface IColumnFamilyConnection : IDisposable
    {
        void AddColumn(string key, Column column);
        Column GetColumn(string key, string columnName);
        bool TryGetColumn(string key, string columnName, out Column result);
        void DeleteBatch(string key, IEnumerable<string> columnNames);
        void AddBatch(string key, IEnumerable<Column> columns);
        void BatchInsert(IEnumerable<KeyValuePair<string, IEnumerable<Column>>> data);
        void BatchDelete(IEnumerable<KeyValuePair<string, IEnumerable<string>>> data);
        Column[] GetRow(string key, string greatThanColumnName, int count);
        List<KeyValuePair<string, Column[]>> GetRows(IEnumerable<string> keys, string startColumnName, int count);
        string[] GetRowsWhere(int maximalCount, IndexExpression[] conditions, string[] columns);
        string[] GetRowsWithColumnValue(int maximalCount, string key, byte[] value);
        void Truncate();
    }
}
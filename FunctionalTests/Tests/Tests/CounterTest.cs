﻿using System;

using NUnit.Framework;

using SKBKontur.Cassandra.CassandraClient.Abstractions;

namespace SKBKontur.Cassandra.FunctionalTests.Tests
{
    public class CounterTest : CassandraFunctionalTestWithRemoveKeyspacesBase
    {
        [Test]
        public void TestGetCount()
        {
            var start1 = DateTime.Now;
            for (int i = 0; i < 1000; i++)
            {
                var cols = new Column[1000];
                for (int j = 0; j < 1000; ++j)
                {
                    string columnName = "columnName" + i + "_" + j;
                    string columnValue = "columnValue" + i + "_" + j;
                    cols[j] = ToColumn(columnName, columnValue, 100);
                }

                cassandraClient.AddBatch(Constants.KeyspaceName, Constants.ColumnFamilyName, "row", cols);
            }
            var finish1 = DateTime.Now;
            var start = DateTime.Now;
            var count = cassandraClient.GetCount(Constants.KeyspaceName, Constants.ColumnFamilyName, "row");
            var finish = DateTime.Now;
            Assert.AreEqual(1000000, count);
            Console.WriteLine("GetCount Completed at " + (finish - start).TotalMilliseconds + "ms");
            Console.WriteLine("Write Completed at " + (finish1 - start1).TotalMilliseconds + "ms");
        }
        [Test]
        public void TestGetEmptyCount()
        {
            var count = cassandraClient.GetCount(Constants.KeyspaceName, Constants.ColumnFamilyName, "roww");
            Assert.AreEqual(0, count);
            count = cassandraClient.GetCount(Constants.KeyspaceName, Constants.ColumnFamilyName, "row");
            Assert.AreEqual(0, count);
        }
        [Test]
        public void TestGetCounts()
        {
            var rows = new string[10];
            for (int i = 0; i < rows.Length; i++)
            {
                var cols = new Column[10 * i];
                for (int j = 0; j < 10 * i; ++j)
                {
                    string columnName = "columnName" + i + "_" + j;
                    string columnValue = "columnValue" + i + "_" + j;
                    cols[j] = ToColumn(columnName, columnValue, 100);
                }
                rows[i] = "row" + i;
                cassandraClient.AddBatch(Constants.KeyspaceName, Constants.ColumnFamilyName, "row" + i, cols);
            }
            var counts = cassandraClient.GetCounts(Constants.KeyspaceName, Constants.ColumnFamilyName, rows);
            for (int i = 0; i < 10; ++i )
            {
                Assert.AreEqual(10 * i, counts["row" + i]);
            }
        }
    }
}
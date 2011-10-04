﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apache.Cassandra;
using CassandraClient.AquilesTrash.Model;

using CassandraClient.AquilesTrash.Converter;
using CassandraClient.AquilesTrash.Converter.Model;

namespace CassandraClient.AquilesTrash.Converter.Model.Impl
{
    /// <summary>
    /// Converter for AquilesSuperColumn
    /// </summary>
    public class AquilesSuperColumnConverter : IThriftConverter<AquilesSuperColumn, SuperColumn>
    {
        #region IThriftConverter<AquilesSuperColumn,SuperColumn> Members

        /// <summary>
        /// Transform AquilesSuperColumn structure into SuperColumn
        /// </summary>
        /// <param name="objectA"></param>
        /// <returns></returns>
        public SuperColumn Transform(AquilesSuperColumn objectA)
        {
            SuperColumn superColumn = new SuperColumn();
            superColumn.Name = objectA.Name;
            superColumn.Columns = new List<Column>(objectA.Columns.Count);
            foreach (AquilesColumn column in objectA.Columns)
            {
                superColumn.Columns.Add(ModelConverterHelper.Convert<AquilesColumn,Column>(column));
            }

            return superColumn;
        }

        /// <summary>
        /// Transform SuperColumn structure into AquilesSuperColumn
        /// </summary>
        /// <param name="objectB"></param>
        /// <returns></returns>
        public AquilesSuperColumn Transform(SuperColumn objectB)
        {
            AquilesSuperColumn superColumn = new AquilesSuperColumn();
            superColumn.Name = objectB.Name;
            superColumn.Columns = new List<AquilesColumn>(objectB.Columns.Count);
            foreach (Column column in objectB.Columns)
            {
                superColumn.Columns.Add(ModelConverterHelper.Convert<AquilesColumn,Column>(column));
            }

            return superColumn;
        }

        #endregion
    }
}
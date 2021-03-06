/**
 * Autogenerated by Thrift Compiler (0.12.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace Apache.Cassandra
{

  /// <summary>
  /// A named list of columns.
  /// @param name. see Column.name.
  /// @param columns. A collection of standard Columns.  The columns within a super column are defined in an adhoc manner.
  ///                 Columns within a super column do not have to have matching structures (similarly named child columns).
  /// </summary>
  #if !SILVERLIGHT
  [Serializable]
  #endif
  internal partial class SuperColumn : TBase
  {

    public byte[] Name { get; set; }

    public List<Column> Columns { get; set; }

    public SuperColumn() {
    }

    public SuperColumn(byte[] name, List<Column> columns) : this() {
      this.Name = name;
      this.Columns = columns;
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        bool isset_name = false;
        bool isset_columns = false;
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.String) {
                Name = iprot.ReadBinary();
                isset_name = true;
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.List) {
                {
                  Columns = new List<Column>();
                  TList _list0 = iprot.ReadListBegin();
                  for( int _i1 = 0; _i1 < _list0.Count; ++_i1)
                  {
                    Column _elem2;
                    _elem2 = new Column();
                    _elem2.Read(iprot);
                    Columns.Add(_elem2);
                  }
                  iprot.ReadListEnd();
                }
                isset_columns = true;
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
        if (!isset_name)
          throw new TProtocolException(TProtocolException.INVALID_DATA, "required field Name not set");
        if (!isset_columns)
          throw new TProtocolException(TProtocolException.INVALID_DATA, "required field Columns not set");
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("SuperColumn");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (Name == null)
          throw new TProtocolException(TProtocolException.INVALID_DATA, "required field Name not set");
        field.Name = "name";
        field.Type = TType.String;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteBinary(Name);
        oprot.WriteFieldEnd();
        if (Columns == null)
          throw new TProtocolException(TProtocolException.INVALID_DATA, "required field Columns not set");
        field.Name = "columns";
        field.Type = TType.List;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.Struct, Columns.Count));
          foreach (Column _iter3 in Columns)
          {
            _iter3.Write(oprot);
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("SuperColumn(");
      __sb.Append(", Name: ");
      __sb.Append(Name);
      __sb.Append(", Columns: ");
      __sb.Append(Columns);
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}

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
  /// Used to perform multiple slices on a single row key in one rpc operation
  /// @param key. The row key to be multi sliced
  /// @param column_parent. The column family (super columns are unsupported)
  /// @param column_slices. 0 to many ColumnSlice objects each will be used to select columns
  /// @param reversed. Direction of slice
  /// @param count. Maximum number of columns
  /// @param consistency_level. Level to perform the operation at
  /// </summary>
  #if !SILVERLIGHT
  [Serializable]
  #endif
  internal partial class MultiSliceRequest : TBase
  {
    private byte[] _key;
    private ColumnParent _column_parent;
    private List<ColumnSlice> _column_slices;
    private bool _reversed;
    private int _count;
    private ConsistencyLevel _consistency_level;

    public byte[] Key
    {
      get
      {
        return _key;
      }
      set
      {
        __isset.key = true;
        this._key = value;
      }
    }

    public ColumnParent Column_parent
    {
      get
      {
        return _column_parent;
      }
      set
      {
        __isset.column_parent = true;
        this._column_parent = value;
      }
    }

    public List<ColumnSlice> Column_slices
    {
      get
      {
        return _column_slices;
      }
      set
      {
        __isset.column_slices = true;
        this._column_slices = value;
      }
    }

    public bool Reversed
    {
      get
      {
        return _reversed;
      }
      set
      {
        __isset.reversed = true;
        this._reversed = value;
      }
    }

    public int Count
    {
      get
      {
        return _count;
      }
      set
      {
        __isset.count = true;
        this._count = value;
      }
    }

    /// <summary>
    /// 
    /// <seealso cref="ConsistencyLevel"/>
    /// </summary>
    public ConsistencyLevel Consistency_level
    {
      get
      {
        return _consistency_level;
      }
      set
      {
        __isset.consistency_level = true;
        this._consistency_level = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    internal struct Isset {
      public bool key;
      public bool column_parent;
      public bool column_slices;
      public bool reversed;
      public bool count;
      public bool consistency_level;
    }

    public MultiSliceRequest() {
      this._reversed = false;
      this.__isset.reversed = true;
      this._count = 1000;
      this.__isset.count = true;
      this._consistency_level = ConsistencyLevel.ONE;
      this.__isset.consistency_level = true;
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
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
                Key = iprot.ReadBinary();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.Struct) {
                Column_parent = new ColumnParent();
                Column_parent.Read(iprot);
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 3:
              if (field.Type == TType.List) {
                {
                  Column_slices = new List<ColumnSlice>();
                  TList _list108 = iprot.ReadListBegin();
                  for( int _i109 = 0; _i109 < _list108.Count; ++_i109)
                  {
                    ColumnSlice _elem110;
                    _elem110 = new ColumnSlice();
                    _elem110.Read(iprot);
                    Column_slices.Add(_elem110);
                  }
                  iprot.ReadListEnd();
                }
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 4:
              if (field.Type == TType.Bool) {
                Reversed = iprot.ReadBool();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 5:
              if (field.Type == TType.I32) {
                Count = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 6:
              if (field.Type == TType.I32) {
                Consistency_level = (ConsistencyLevel)iprot.ReadI32();
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
        TStruct struc = new TStruct("MultiSliceRequest");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (Key != null && __isset.key) {
          field.Name = "key";
          field.Type = TType.String;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          oprot.WriteBinary(Key);
          oprot.WriteFieldEnd();
        }
        if (Column_parent != null && __isset.column_parent) {
          field.Name = "column_parent";
          field.Type = TType.Struct;
          field.ID = 2;
          oprot.WriteFieldBegin(field);
          Column_parent.Write(oprot);
          oprot.WriteFieldEnd();
        }
        if (Column_slices != null && __isset.column_slices) {
          field.Name = "column_slices";
          field.Type = TType.List;
          field.ID = 3;
          oprot.WriteFieldBegin(field);
          {
            oprot.WriteListBegin(new TList(TType.Struct, Column_slices.Count));
            foreach (ColumnSlice _iter111 in Column_slices)
            {
              _iter111.Write(oprot);
            }
            oprot.WriteListEnd();
          }
          oprot.WriteFieldEnd();
        }
        if (__isset.reversed) {
          field.Name = "reversed";
          field.Type = TType.Bool;
          field.ID = 4;
          oprot.WriteFieldBegin(field);
          oprot.WriteBool(Reversed);
          oprot.WriteFieldEnd();
        }
        if (__isset.count) {
          field.Name = "count";
          field.Type = TType.I32;
          field.ID = 5;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(Count);
          oprot.WriteFieldEnd();
        }
        if (__isset.consistency_level) {
          field.Name = "consistency_level";
          field.Type = TType.I32;
          field.ID = 6;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32((int)Consistency_level);
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("MultiSliceRequest(");
      bool __first = true;
      if (Key != null && __isset.key) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Key: ");
        __sb.Append(Key);
      }
      if (Column_parent != null && __isset.column_parent) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Column_parent: ");
        __sb.Append(Column_parent== null ? "<null>" : Column_parent.ToString());
      }
      if (Column_slices != null && __isset.column_slices) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Column_slices: ");
        __sb.Append(Column_slices);
      }
      if (__isset.reversed) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Reversed: ");
        __sb.Append(Reversed);
      }
      if (__isset.count) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Count: ");
        __sb.Append(Count);
      }
      if (__isset.consistency_level) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Consistency_level: ");
        __sb.Append(Consistency_level);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}

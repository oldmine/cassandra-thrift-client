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

  #if !SILVERLIGHT
  [Serializable]
  #endif
  internal partial class KsDef : TBase
  {
    private Dictionary<string, string> _strategy_options;
    private int _replication_factor;
    private bool _durable_writes;

    public string Name { get; set; }

    public string Strategy_class { get; set; }

    public Dictionary<string, string> Strategy_options
    {
      get
      {
        return _strategy_options;
      }
      set
      {
        __isset.strategy_options = true;
        this._strategy_options = value;
      }
    }

    /// <summary>
    /// @deprecated ignored
    /// </summary>
    public int Replication_factor
    {
      get
      {
        return _replication_factor;
      }
      set
      {
        __isset.replication_factor = true;
        this._replication_factor = value;
      }
    }

    public List<CfDef> Cf_defs { get; set; }

    public bool Durable_writes
    {
      get
      {
        return _durable_writes;
      }
      set
      {
        __isset.durable_writes = true;
        this._durable_writes = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    internal struct Isset {
      public bool strategy_options;
      public bool replication_factor;
      public bool durable_writes;
    }

    public KsDef() {
      this._durable_writes = true;
      this.__isset.durable_writes = true;
    }

    public KsDef(string name, string strategy_class, List<CfDef> cf_defs) : this() {
      this.Name = name;
      this.Strategy_class = strategy_class;
      this.Cf_defs = cf_defs;
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        bool isset_name = false;
        bool isset_strategy_class = false;
        bool isset_cf_defs = false;
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
                Name = iprot.ReadString();
                isset_name = true;
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.String) {
                Strategy_class = iprot.ReadString();
                isset_strategy_class = true;
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 3:
              if (field.Type == TType.Map) {
                {
                  Strategy_options = new Dictionary<string, string>();
                  TMap _map73 = iprot.ReadMapBegin();
                  for( int _i74 = 0; _i74 < _map73.Count; ++_i74)
                  {
                    string _key75;
                    string _val76;
                    _key75 = iprot.ReadString();
                    _val76 = iprot.ReadString();
                    Strategy_options[_key75] = _val76;
                  }
                  iprot.ReadMapEnd();
                }
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 4:
              if (field.Type == TType.I32) {
                Replication_factor = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 5:
              if (field.Type == TType.List) {
                {
                  Cf_defs = new List<CfDef>();
                  TList _list77 = iprot.ReadListBegin();
                  for( int _i78 = 0; _i78 < _list77.Count; ++_i78)
                  {
                    CfDef _elem79;
                    _elem79 = new CfDef();
                    _elem79.Read(iprot);
                    Cf_defs.Add(_elem79);
                  }
                  iprot.ReadListEnd();
                }
                isset_cf_defs = true;
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 6:
              if (field.Type == TType.Bool) {
                Durable_writes = iprot.ReadBool();
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
        if (!isset_strategy_class)
          throw new TProtocolException(TProtocolException.INVALID_DATA, "required field Strategy_class not set");
        if (!isset_cf_defs)
          throw new TProtocolException(TProtocolException.INVALID_DATA, "required field Cf_defs not set");
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
        TStruct struc = new TStruct("KsDef");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (Name == null)
          throw new TProtocolException(TProtocolException.INVALID_DATA, "required field Name not set");
        field.Name = "name";
        field.Type = TType.String;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Name);
        oprot.WriteFieldEnd();
        if (Strategy_class == null)
          throw new TProtocolException(TProtocolException.INVALID_DATA, "required field Strategy_class not set");
        field.Name = "strategy_class";
        field.Type = TType.String;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Strategy_class);
        oprot.WriteFieldEnd();
        if (Strategy_options != null && __isset.strategy_options) {
          field.Name = "strategy_options";
          field.Type = TType.Map;
          field.ID = 3;
          oprot.WriteFieldBegin(field);
          {
            oprot.WriteMapBegin(new TMap(TType.String, TType.String, Strategy_options.Count));
            foreach (string _iter80 in Strategy_options.Keys)
            {
              oprot.WriteString(_iter80);
              oprot.WriteString(Strategy_options[_iter80]);
            }
            oprot.WriteMapEnd();
          }
          oprot.WriteFieldEnd();
        }
        if (__isset.replication_factor) {
          field.Name = "replication_factor";
          field.Type = TType.I32;
          field.ID = 4;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(Replication_factor);
          oprot.WriteFieldEnd();
        }
        if (Cf_defs == null)
          throw new TProtocolException(TProtocolException.INVALID_DATA, "required field Cf_defs not set");
        field.Name = "cf_defs";
        field.Type = TType.List;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.Struct, Cf_defs.Count));
          foreach (CfDef _iter81 in Cf_defs)
          {
            _iter81.Write(oprot);
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
        if (__isset.durable_writes) {
          field.Name = "durable_writes";
          field.Type = TType.Bool;
          field.ID = 6;
          oprot.WriteFieldBegin(field);
          oprot.WriteBool(Durable_writes);
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
      StringBuilder __sb = new StringBuilder("KsDef(");
      __sb.Append(", Name: ");
      __sb.Append(Name);
      __sb.Append(", Strategy_class: ");
      __sb.Append(Strategy_class);
      if (Strategy_options != null && __isset.strategy_options) {
        __sb.Append(", Strategy_options: ");
        __sb.Append(Strategy_options);
      }
      if (__isset.replication_factor) {
        __sb.Append(", Replication_factor: ");
        __sb.Append(Replication_factor);
      }
      __sb.Append(", Cf_defs: ");
      __sb.Append(Cf_defs);
      if (__isset.durable_writes) {
        __sb.Append(", Durable_writes: ");
        __sb.Append(Durable_writes);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}

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
  /// Invalid request could mean keyspace or column family does not exist, required parameters are missing, or a parameter is malformed.
  /// why contains an associated error message.
  /// </summary>
  #if !SILVERLIGHT
  [Serializable]
  #endif
  internal partial class InvalidRequestException : TException, TBase
  {

    public string Why { get; set; }

    public InvalidRequestException() {
    }

    public InvalidRequestException(string why) : this() {
      this.Why = why;
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        bool isset_why = false;
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
                Why = iprot.ReadString();
                isset_why = true;
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
        if (!isset_why)
          throw new TProtocolException(TProtocolException.INVALID_DATA, "required field Why not set");
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
        TStruct struc = new TStruct("InvalidRequestException");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (Why == null)
          throw new TProtocolException(TProtocolException.INVALID_DATA, "required field Why not set");
        field.Name = "why";
        field.Type = TType.String;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Why);
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
      StringBuilder __sb = new StringBuilder("InvalidRequestException(");
      __sb.Append(", Why: ");
      __sb.Append(Why);
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
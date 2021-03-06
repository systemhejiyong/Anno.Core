/**
 * Autogenerated by Thrift Compiler (0.13.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Thrift;
using Thrift.Collections;

using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;
using Thrift.Transport;
using Thrift.Transport.Client;
using Thrift.Transport.Server;
using Thrift.Processor;


public partial class BrokerCenter
{
  public interface IAsync
  {
    Task<bool> add_brokerAsync(Dictionary<string, string> input, CancellationToken cancellationToken = default(CancellationToken));

    Task<List<Micro>> GetMicroAsync(string channel, CancellationToken cancellationToken = default(CancellationToken));

  }


  public class Client : TBaseClient, IDisposable, IAsync
  {
    public Client(TProtocol protocol) : this(protocol, protocol)
    {
    }

    public Client(TProtocol inputProtocol, TProtocol outputProtocol) : base(inputProtocol, outputProtocol)    {
    }
    public async Task<bool> add_brokerAsync(Dictionary<string, string> input, CancellationToken cancellationToken = default(CancellationToken))
    {
      await OutputProtocol.WriteMessageBeginAsync(new TMessage("add_broker", TMessageType.Call, SeqId), cancellationToken);
      
      var args = new add_brokerArgs();
      args.Input = input;
      
      await args.WriteAsync(OutputProtocol, cancellationToken);
      await OutputProtocol.WriteMessageEndAsync(cancellationToken);
      await OutputProtocol.Transport.FlushAsync(cancellationToken);
      
      var msg = await InputProtocol.ReadMessageBeginAsync(cancellationToken);
      if (msg.Type == TMessageType.Exception)
      {
        var x = await TApplicationException.ReadAsync(InputProtocol, cancellationToken);
        await InputProtocol.ReadMessageEndAsync(cancellationToken);
        throw x;
      }

      var result = new add_brokerResult();
      await result.ReadAsync(InputProtocol, cancellationToken);
      await InputProtocol.ReadMessageEndAsync(cancellationToken);
      if (result.__isset.success)
      {
        return result.Success;
      }
      throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "add_broker failed: unknown result");
    }

    public async Task<List<Micro>> GetMicroAsync(string channel, CancellationToken cancellationToken = default(CancellationToken))
    {
      await OutputProtocol.WriteMessageBeginAsync(new TMessage("GetMicro", TMessageType.Call, SeqId), cancellationToken);
      
      var args = new GetMicroArgs();
      args.Channel = channel;
      
      await args.WriteAsync(OutputProtocol, cancellationToken);
      await OutputProtocol.WriteMessageEndAsync(cancellationToken);
      await OutputProtocol.Transport.FlushAsync(cancellationToken);
      
      var msg = await InputProtocol.ReadMessageBeginAsync(cancellationToken);
      if (msg.Type == TMessageType.Exception)
      {
        var x = await TApplicationException.ReadAsync(InputProtocol, cancellationToken);
        await InputProtocol.ReadMessageEndAsync(cancellationToken);
        throw x;
      }

      var result = new GetMicroResult();
      await result.ReadAsync(InputProtocol, cancellationToken);
      await InputProtocol.ReadMessageEndAsync(cancellationToken);
      if (result.__isset.success)
      {
        return result.Success;
      }
      throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "GetMicro failed: unknown result");
    }

  }

  public class AsyncProcessor : ITAsyncProcessor
  {
    private IAsync _iAsync;

    public AsyncProcessor(IAsync iAsync)
    {
      if (iAsync == null) throw new ArgumentNullException(nameof(iAsync));

      _iAsync = iAsync;
      processMap_["add_broker"] = add_broker_ProcessAsync;
      processMap_["GetMicro"] = GetMicro_ProcessAsync;
    }

    protected delegate Task ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken);
    protected Dictionary<string, ProcessFunction> processMap_ = new Dictionary<string, ProcessFunction>();

    public async Task<bool> ProcessAsync(TProtocol iprot, TProtocol oprot)
    {
      return await ProcessAsync(iprot, oprot, CancellationToken.None);
    }

    public async Task<bool> ProcessAsync(TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
    {
      try
      {
        var msg = await iprot.ReadMessageBeginAsync(cancellationToken);

        ProcessFunction fn;
        processMap_.TryGetValue(msg.Name, out fn);

        if (fn == null)
        {
          await TProtocolUtil.SkipAsync(iprot, TType.Struct, cancellationToken);
          await iprot.ReadMessageEndAsync(cancellationToken);
          var x = new TApplicationException (TApplicationException.ExceptionType.UnknownMethod, "Invalid method name: '" + msg.Name + "'");
          await oprot.WriteMessageBeginAsync(new TMessage(msg.Name, TMessageType.Exception, msg.SeqID), cancellationToken);
          await x.WriteAsync(oprot, cancellationToken);
          await oprot.WriteMessageEndAsync(cancellationToken);
          await oprot.Transport.FlushAsync(cancellationToken);
          return true;
        }

        await fn(msg.SeqID, iprot, oprot, cancellationToken);

      }
      catch (IOException)
      {
        return false;
      }

      return true;
    }

    public async Task add_broker_ProcessAsync(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
    {
      var args = new add_brokerArgs();
      await args.ReadAsync(iprot, cancellationToken);
      await iprot.ReadMessageEndAsync(cancellationToken);
      var result = new add_brokerResult();
      try
      {
        result.Success = await _iAsync.add_brokerAsync(args.Input, cancellationToken);
        await oprot.WriteMessageBeginAsync(new TMessage("add_broker", TMessageType.Reply, seqid), cancellationToken); 
        await result.WriteAsync(oprot, cancellationToken);
      }
      catch (TTransportException)
      {
        throw;
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine("Error occurred in processor:");
        Console.Error.WriteLine(ex.ToString());
        var x = new TApplicationException(TApplicationException.ExceptionType.InternalError," Internal error.");
        await oprot.WriteMessageBeginAsync(new TMessage("add_broker", TMessageType.Exception, seqid), cancellationToken);
        await x.WriteAsync(oprot, cancellationToken);
      }
      await oprot.WriteMessageEndAsync(cancellationToken);
      await oprot.Transport.FlushAsync(cancellationToken);
    }

    public async Task GetMicro_ProcessAsync(int seqid, TProtocol iprot, TProtocol oprot, CancellationToken cancellationToken)
    {
      var args = new GetMicroArgs();
      await args.ReadAsync(iprot, cancellationToken);
      await iprot.ReadMessageEndAsync(cancellationToken);
      var result = new GetMicroResult();
      try
      {
        result.Success = await _iAsync.GetMicroAsync(args.Channel, cancellationToken);
        await oprot.WriteMessageBeginAsync(new TMessage("GetMicro", TMessageType.Reply, seqid), cancellationToken); 
        await result.WriteAsync(oprot, cancellationToken);
      }
      catch (TTransportException)
      {
        throw;
      }
      catch (Exception ex)
      {
        Console.Error.WriteLine("Error occurred in processor:");
        Console.Error.WriteLine(ex.ToString());
        var x = new TApplicationException(TApplicationException.ExceptionType.InternalError," Internal error.");
        await oprot.WriteMessageBeginAsync(new TMessage("GetMicro", TMessageType.Exception, seqid), cancellationToken);
        await x.WriteAsync(oprot, cancellationToken);
      }
      await oprot.WriteMessageEndAsync(cancellationToken);
      await oprot.Transport.FlushAsync(cancellationToken);
    }

  }


  public partial class add_brokerArgs : TBase
  {
    private Dictionary<string, string> _input;

    public Dictionary<string, string> Input
    {
      get
      {
        return _input;
      }
      set
      {
        __isset.input = true;
        this._input = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool input;
    }

    public add_brokerArgs()
    {
    }

    public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        await iprot.ReadStructBeginAsync(cancellationToken);
        while (true)
        {
          field = await iprot.ReadFieldBeginAsync(cancellationToken);
          if (field.Type == TType.Stop)
          {
            break;
          }

          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.Map)
              {
                {
                  TMap _map5 = await iprot.ReadMapBeginAsync(cancellationToken);
                  Input = new Dictionary<string, string>(_map5.Count);
                  for(int _i6 = 0; _i6 < _map5.Count; ++_i6)
                  {
                    string _key7;
                    string _val8;
                    _key7 = await iprot.ReadStringAsync(cancellationToken);
                    _val8 = await iprot.ReadStringAsync(cancellationToken);
                    Input[_key7] = _val8;
                  }
                  await iprot.ReadMapEndAsync(cancellationToken);
                }
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            default: 
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              break;
          }

          await iprot.ReadFieldEndAsync(cancellationToken);
        }

        await iprot.ReadStructEndAsync(cancellationToken);
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
    {
      oprot.IncrementRecursionDepth();
      try
      {
        var struc = new TStruct("add_broker_args");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();
        if (Input != null && __isset.input)
        {
          field.Name = "input";
          field.Type = TType.Map;
          field.ID = 1;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          {
            await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, Input.Count), cancellationToken);
            foreach (string _iter9 in Input.Keys)
            {
              await oprot.WriteStringAsync(_iter9, cancellationToken);
              await oprot.WriteStringAsync(Input[_iter9], cancellationToken);
            }
            await oprot.WriteMapEndAsync(cancellationToken);
          }
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        await oprot.WriteFieldStopAsync(cancellationToken);
        await oprot.WriteStructEndAsync(cancellationToken);
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override bool Equals(object that)
    {
      var other = that as add_brokerArgs;
      if (other == null) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.input == other.__isset.input) && ((!__isset.input) || (TCollections.Equals(Input, other.Input))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if(__isset.input)
          hashcode = (hashcode * 397) + TCollections.GetHashCode(Input);
      }
      return hashcode;
    }

    public override string ToString()
    {
      var sb = new StringBuilder("add_broker_args(");
      bool __first = true;
      if (Input != null && __isset.input)
      {
        if(!__first) { sb.Append(", "); }
        __first = false;
        sb.Append("Input: ");
        sb.Append(Input);
      }
      sb.Append(")");
      return sb.ToString();
    }
  }


  public partial class add_brokerResult : TBase
  {
    private bool _success;

    public bool Success
    {
      get
      {
        return _success;
      }
      set
      {
        __isset.success = true;
        this._success = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool success;
    }

    public add_brokerResult()
    {
    }

    public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        await iprot.ReadStructBeginAsync(cancellationToken);
        while (true)
        {
          field = await iprot.ReadFieldBeginAsync(cancellationToken);
          if (field.Type == TType.Stop)
          {
            break;
          }

          switch (field.ID)
          {
            case 0:
              if (field.Type == TType.Bool)
              {
                Success = await iprot.ReadBoolAsync(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            default: 
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              break;
          }

          await iprot.ReadFieldEndAsync(cancellationToken);
        }

        await iprot.ReadStructEndAsync(cancellationToken);
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
    {
      oprot.IncrementRecursionDepth();
      try
      {
        var struc = new TStruct("add_broker_result");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();

        if(this.__isset.success)
        {
          field.Name = "Success";
          field.Type = TType.Bool;
          field.ID = 0;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          await oprot.WriteBoolAsync(Success, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        await oprot.WriteFieldStopAsync(cancellationToken);
        await oprot.WriteStructEndAsync(cancellationToken);
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override bool Equals(object that)
    {
      var other = that as add_brokerResult;
      if (other == null) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.success == other.__isset.success) && ((!__isset.success) || (System.Object.Equals(Success, other.Success))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if(__isset.success)
          hashcode = (hashcode * 397) + Success.GetHashCode();
      }
      return hashcode;
    }

    public override string ToString()
    {
      var sb = new StringBuilder("add_broker_result(");
      bool __first = true;
      if (__isset.success)
      {
        if(!__first) { sb.Append(", "); }
        __first = false;
        sb.Append("Success: ");
        sb.Append(Success);
      }
      sb.Append(")");
      return sb.ToString();
    }
  }


  public partial class GetMicroArgs : TBase
  {
    private string _channel;

    public string Channel
    {
      get
      {
        return _channel;
      }
      set
      {
        __isset.channel = true;
        this._channel = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool channel;
    }

    public GetMicroArgs()
    {
    }

    public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        await iprot.ReadStructBeginAsync(cancellationToken);
        while (true)
        {
          field = await iprot.ReadFieldBeginAsync(cancellationToken);
          if (field.Type == TType.Stop)
          {
            break;
          }

          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.String)
              {
                Channel = await iprot.ReadStringAsync(cancellationToken);
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            default: 
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              break;
          }

          await iprot.ReadFieldEndAsync(cancellationToken);
        }

        await iprot.ReadStructEndAsync(cancellationToken);
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
    {
      oprot.IncrementRecursionDepth();
      try
      {
        var struc = new TStruct("GetMicro_args");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();
        if (Channel != null && __isset.channel)
        {
          field.Name = "channel";
          field.Type = TType.String;
          field.ID = 1;
          await oprot.WriteFieldBeginAsync(field, cancellationToken);
          await oprot.WriteStringAsync(Channel, cancellationToken);
          await oprot.WriteFieldEndAsync(cancellationToken);
        }
        await oprot.WriteFieldStopAsync(cancellationToken);
        await oprot.WriteStructEndAsync(cancellationToken);
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override bool Equals(object that)
    {
      var other = that as GetMicroArgs;
      if (other == null) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.channel == other.__isset.channel) && ((!__isset.channel) || (System.Object.Equals(Channel, other.Channel))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if(__isset.channel)
          hashcode = (hashcode * 397) + Channel.GetHashCode();
      }
      return hashcode;
    }

    public override string ToString()
    {
      var sb = new StringBuilder("GetMicro_args(");
      bool __first = true;
      if (Channel != null && __isset.channel)
      {
        if(!__first) { sb.Append(", "); }
        __first = false;
        sb.Append("Channel: ");
        sb.Append(Channel);
      }
      sb.Append(")");
      return sb.ToString();
    }
  }


  public partial class GetMicroResult : TBase
  {
    private List<Micro> _success;

    public List<Micro> Success
    {
      get
      {
        return _success;
      }
      set
      {
        __isset.success = true;
        this._success = value;
      }
    }


    public Isset __isset;
    public struct Isset
    {
      public bool success;
    }

    public GetMicroResult()
    {
    }

    public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        await iprot.ReadStructBeginAsync(cancellationToken);
        while (true)
        {
          field = await iprot.ReadFieldBeginAsync(cancellationToken);
          if (field.Type == TType.Stop)
          {
            break;
          }

          switch (field.ID)
          {
            case 0:
              if (field.Type == TType.List)
              {
                {
                  TList _list10 = await iprot.ReadListBeginAsync(cancellationToken);
                  Success = new List<Micro>(_list10.Count);
                  for(int _i11 = 0; _i11 < _list10.Count; ++_i11)
                  {
                    Micro _elem12;
                    _elem12 = new Micro();
                    await _elem12.ReadAsync(iprot, cancellationToken);
                    Success.Add(_elem12);
                  }
                  await iprot.ReadListEndAsync(cancellationToken);
                }
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            default: 
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              break;
          }

          await iprot.ReadFieldEndAsync(cancellationToken);
        }

        await iprot.ReadStructEndAsync(cancellationToken);
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
    {
      oprot.IncrementRecursionDepth();
      try
      {
        var struc = new TStruct("GetMicro_result");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();

        if(this.__isset.success)
        {
          if (Success != null)
          {
            field.Name = "Success";
            field.Type = TType.List;
            field.ID = 0;
            await oprot.WriteFieldBeginAsync(field, cancellationToken);
            {
              await oprot.WriteListBeginAsync(new TList(TType.Struct, Success.Count), cancellationToken);
              foreach (Micro _iter13 in Success)
              {
                await _iter13.WriteAsync(oprot, cancellationToken);
              }
              await oprot.WriteListEndAsync(cancellationToken);
            }
            await oprot.WriteFieldEndAsync(cancellationToken);
          }
        }
        await oprot.WriteFieldStopAsync(cancellationToken);
        await oprot.WriteStructEndAsync(cancellationToken);
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override bool Equals(object that)
    {
      var other = that as GetMicroResult;
      if (other == null) return false;
      if (ReferenceEquals(this, other)) return true;
      return ((__isset.success == other.__isset.success) && ((!__isset.success) || (TCollections.Equals(Success, other.Success))));
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        if(__isset.success)
          hashcode = (hashcode * 397) + TCollections.GetHashCode(Success);
      }
      return hashcode;
    }

    public override string ToString()
    {
      var sb = new StringBuilder("GetMicro_result(");
      bool __first = true;
      if (Success != null && __isset.success)
      {
        if(!__first) { sb.Append(", "); }
        __first = false;
        sb.Append("Success: ");
        sb.Append(Success);
      }
      sb.Append(")");
      return sb.ToString();
    }
  }

}

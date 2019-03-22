﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public enum StreamControlNodeType
    {
        None,
        ScopeIn,
        ScopeOut,
        ParensIn,
        ParensOut,
        Breaker,
        Streamer,
        Statement,
    }

    public interface IExecutionStreamNode
    {
        IScope Scope { get; }
        StreamControlNodeType Type { get; }
    }

    public interface IDefinedStreamNode : IExecutionStreamNode
    {
        int GrammarNodeId { get; }
        int InStringPosition { get; }
    }

    public interface IScope
    {
        IScope ParentScope { get; }
        IList<IVariable> Variables { get; }
        IList<ILabel> Labels { get; }
        IList<IScope> ChildrenScopes { get; }
        IEnumerable<IExecutionStreamNode> Stream { get; }
        IEnumerable<IExecutionStreamNode> RpnStream { get; set; }
        IEnumerable<IExecutionStreamNode> GetConsistentStream();
        IEnumerable<IExecutionStreamNode> GetRpnConsistentStream();
    }

    public interface IVariable : IDefinedStreamNode
    {
        bool IsAssigned { get; }
        object Value { get; set; }
        string Name { get; }
    }

    public interface ILiteral : IDefinedStreamNode
    {
        object Value { get; }
    }

    public interface IOperator : IDefinedStreamNode
    {
        int OperandCount { get; }
    }

    public interface IStatement : IDefinedStreamNode
    {
        IEnumerable<IEnumerable<IExecutionStreamNode>> Streams { get; }
        IEnumerable<IEnumerable<IExecutionStreamNode>> RpnStreams { get; set; }
        IEnumerable<IExecutionStreamNode> RpnStreamProcessed { get; set; }
        int NodeId { get; }
        bool IsStreamMaxCountSet { get; }
        int StreamMaxCount { get; }
    }

    public interface IDelimiter : IDefinedStreamNode
    {
        IScope ChildScope { get; }
    }

    //#region System

    public interface ILabel : IExecutionStreamNode
    {
        string Name { get; }
    }

    public interface IDefinedLabel : ILabel, IDefinedStreamNode
    {

    }

    public interface IJump : IExecutionStreamNode
    {
        ILabel Label { get; }
    }

    public interface IUserJump : IJump
    {

    }

    public interface IJumpConditional : IJump
    {
        
    }

    public interface IJumpConditionalNegative : IJumpConditional
    {

    }
   
    public interface ICall : IExecutionStreamNode
    {
        string Address { get; }
        int ParamCount { get; }
    }

    //public interface IExecutionStream
    //{
    //    IEnumerable<IExecutionStreamNode> Tokens { get; }
    //}

    //#endregion
}

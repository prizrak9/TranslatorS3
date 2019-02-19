﻿[<AutoOpen>]
module SyntaxPredescenceTableParser.Types
open System.Collections.Generic
open System.Linq


type internal Rel = Core.Relationship


type internal Table = IDictionary<int, Core.IPredescenceNode>
type internal Errors = seq<Core.IParserError>

type SyntaxPredescenceTableParserResult =
    {errors:Errors; countLeft:int option} with
        interface Core.IParserResult with
            member this.Errors = this.errors

type internal Res = SyntaxPredescenceTableParserResult

type internal ParserError = 
    {message:string; tag:string; tokensOnError:Core.IParsedToken seq} with
        interface Core.IParserError with
            member this.Message = this.message
            member this.Tag = this.tag
            member this.TokensOnError = this.tokensOnError


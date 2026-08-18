using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web.Razor.Editor;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Resources;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer;
using System.Web.Razor.Tokenizer.Symbols;
using System.Web.Razor.Utils;

namespace System.Web.Razor.Parser
{
	// Token: 0x0200003B RID: 59
	public abstract class TokenizerBackedParser<TTokenizer, TSymbol, TSymbolType> : ParserBase where TTokenizer : Tokenizer<TSymbol, TSymbolType> where TSymbol : SymbolBase<TSymbolType>
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00007961 File Offset: 0x00005B61
		protected TokenizerBackedParser()
		{
			this.Span = new SpanBuilder();
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00007974 File Offset: 0x00005B74
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000797C File Offset: 0x00005B7C
		protected SpanBuilder Span { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00007985 File Offset: 0x00005B85
		protected TokenizerView<TTokenizer, TSymbol, TSymbolType> Tokenizer
		{
			get
			{
				return this._tokenizer ?? this.InitTokenizer();
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00007997 File Offset: 0x00005B97
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000799F File Offset: 0x00005B9F
		protected Action<SpanBuilder> SpanConfig { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000079A8 File Offset: 0x00005BA8
		protected TSymbol CurrentSymbol
		{
			get
			{
				return this.Tokenizer.Current;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000079B5 File Offset: 0x00005BB5
		// (set) Token: 0x06000233 RID: 563 RVA: 0x000079BD File Offset: 0x00005BBD
		private protected TSymbol PreviousSymbol { protected get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000234 RID: 564 RVA: 0x000079C8 File Offset: 0x00005BC8
		protected SourceLocation CurrentLocation
		{
			get
			{
				if (!this.EndOfFile && this.CurrentSymbol != null)
				{
					TSymbol currentSymbol = this.CurrentSymbol;
					return currentSymbol.Start;
				}
				return this.Context.Source.Location;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00007A0F File Offset: 0x00005C0F
		protected bool EndOfFile
		{
			get
			{
				return this.Tokenizer.EndOfFile;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000236 RID: 566
		protected abstract LanguageCharacteristics<TTokenizer, TSymbol, TSymbolType> Language { get; }

		// Token: 0x06000237 RID: 567 RVA: 0x00007A1C File Offset: 0x00005C1C
		protected virtual void HandleEmbeddedTransition()
		{
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007A1E File Offset: 0x00005C1E
		protected virtual bool IsAtEmbeddedTransition(bool allowTemplatesAndComments, bool allowTransitions)
		{
			return false;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007A24 File Offset: 0x00005C24
		public override void BuildSpan(SpanBuilder span, SourceLocation start, string content)
		{
			foreach (TSymbol tsymbol in this.Language.TokenizeString(start, content))
			{
				ISymbol symbol = tsymbol;
				span.Accept(symbol);
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00007A80 File Offset: 0x00005C80
		protected void Initialize(SpanBuilder span)
		{
			if (this.SpanConfig != null)
			{
				this.SpanConfig(span);
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00007A96 File Offset: 0x00005C96
		protected internal bool NextToken()
		{
			this.PreviousSymbol = this.CurrentSymbol;
			return this.Tokenizer.Next();
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00007AB0 File Offset: 0x00005CB0
		private TokenizerView<TTokenizer, TSymbol, TSymbolType> InitTokenizer()
		{
			return this._tokenizer = new TokenizerView<TTokenizer, TSymbol, TSymbolType>(this.Language.CreateTokenizer(this.Context.Source));
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00007AE1 File Offset: 0x00005CE1
		[Conditional("DEBUG")]
		internal void Assert(TSymbolType expectedType)
		{
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00007AE3 File Offset: 0x00005CE3
		protected internal void PutBack(TSymbol symbol)
		{
			if (symbol != null)
			{
				this.Tokenizer.PutBack(symbol);
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00007AFC File Offset: 0x00005CFC
		protected internal void PutBack(IEnumerable<TSymbol> symbols)
		{
			foreach (TSymbol symbol in symbols.Reverse<TSymbol>())
			{
				this.PutBack(symbol);
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00007B4C File Offset: 0x00005D4C
		protected internal void PutCurrentBack()
		{
			if (!this.EndOfFile && this.CurrentSymbol != null)
			{
				this.PutBack(this.CurrentSymbol);
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00007B70 File Offset: 0x00005D70
		protected internal bool Balance(BalancingModes mode)
		{
			TSymbol currentSymbol = this.CurrentSymbol;
			TSymbolType type = currentSymbol.Type;
			TSymbolType tsymbolType = this.Language.FlipBracket(type);
			SourceLocation currentLocation = this.CurrentLocation;
			this.AcceptAndMoveNext();
			if (this.EndOfFile && !mode.HasFlag(BalancingModes.NoErrorOnFailure))
			{
				this.Context.OnError(currentLocation, RazorResources.ParseError_Expected_CloseBracket_Before_EOF, new object[]
				{
					this.Language.GetSample(type),
					this.Language.GetSample(tsymbolType)
				});
			}
			return this.Balance(mode, type, tsymbolType, currentLocation);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00007C10 File Offset: 0x00005E10
		protected internal bool Balance(BalancingModes mode, TSymbolType left, TSymbolType right, SourceLocation start)
		{
			int absoluteIndex = this.CurrentLocation.AbsoluteIndex;
			int num = 1;
			if (!this.EndOfFile)
			{
				IList<TSymbol> list = new List<TSymbol>();
				do
				{
					if (this.IsAtEmbeddedTransition(mode.HasFlag(BalancingModes.AllowCommentsAndTemplates), mode.HasFlag(BalancingModes.AllowEmbeddedTransitions)))
					{
						this.Accept(list);
						list.Clear();
						this.HandleEmbeddedTransition();
						absoluteIndex = this.CurrentLocation.AbsoluteIndex;
					}
					if (this.At(left))
					{
						num++;
					}
					else if (this.At(right))
					{
						num--;
					}
					if (num > 0)
					{
						list.Add(this.CurrentSymbol);
					}
				}
				while (num > 0 && this.NextToken());
				if (num > 0)
				{
					if (!mode.HasFlag(BalancingModes.NoErrorOnFailure))
					{
						this.Context.OnError(start, RazorResources.ParseError_Expected_CloseBracket_Before_EOF, new object[]
						{
							this.Language.GetSample(left),
							this.Language.GetSample(right)
						});
					}
					if (mode.HasFlag(BalancingModes.BacktrackOnFailure))
					{
						this.Context.Source.Position = absoluteIndex;
						this.NextToken();
					}
					else
					{
						this.Accept(list);
					}
				}
				else
				{
					this.Accept(list);
				}
			}
			return num == 0;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00007D94 File Offset: 0x00005F94
		protected internal bool NextIs(TSymbolType type)
		{
			return this.NextIs((TSymbol sym) => sym != null && object.Equals(type, sym.Type));
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00007E3C File Offset: 0x0000603C
		protected internal bool NextIs(params TSymbolType[] types)
		{
			return this.NextIs((TSymbol sym) => sym != null && types.Any((TSymbolType t) => object.Equals(t, sym.Type)));
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00007E68 File Offset: 0x00006068
		protected internal bool NextIs(Func<TSymbol, bool> condition)
		{
			TSymbol currentSymbol = this.CurrentSymbol;
			this.NextToken();
			bool result = condition(this.CurrentSymbol);
			this.PutCurrentBack();
			this.PutBack(currentSymbol);
			this.EnsureCurrent();
			return result;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007EA8 File Offset: 0x000060A8
		protected internal bool Was(TSymbolType type)
		{
			if (this.PreviousSymbol != null)
			{
				TSymbol previousSymbol = this.PreviousSymbol;
				return object.Equals(previousSymbol.Type, type);
			}
			return false;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007EE8 File Offset: 0x000060E8
		protected internal bool At(TSymbolType type)
		{
			if (!this.EndOfFile && this.CurrentSymbol != null)
			{
				TSymbol currentSymbol = this.CurrentSymbol;
				return object.Equals(currentSymbol.Type, type);
			}
			return false;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007F30 File Offset: 0x00006130
		protected internal bool AcceptAndMoveNext()
		{
			this.Accept(this.CurrentSymbol);
			return this.NextToken();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007F44 File Offset: 0x00006144
		protected TSymbol AcceptSingleWhiteSpaceCharacter()
		{
			if (this.Language.IsWhiteSpace(this.CurrentSymbol))
			{
				Tuple<TSymbol, TSymbol> tuple = this.Language.SplitSymbol(this.CurrentSymbol, 1, this.Language.GetKnownSymbolType(KnownSymbolType.WhiteSpace));
				this.Accept(tuple.Item1);
				this.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
				this.NextToken();
				return tuple.Item2;
			}
			return default(TSymbol);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007FB8 File Offset: 0x000061B8
		protected internal void Accept(IEnumerable<TSymbol> symbols)
		{
			foreach (TSymbol symbol in symbols)
			{
				this.Accept(symbol);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008000 File Offset: 0x00006200
		protected internal void Accept(TSymbol symbol)
		{
			if (symbol != null)
			{
				foreach (RazorError item in symbol.Errors)
				{
					this.Context.Errors.Add(item);
				}
				this.Span.Accept(symbol);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00008078 File Offset: 0x00006278
		protected internal bool AcceptAll(params TSymbolType[] types)
		{
			int i = 0;
			while (i < types.Length)
			{
				TSymbolType tsymbolType = types[i];
				if (this.CurrentSymbol != null)
				{
					TSymbol currentSymbol = this.CurrentSymbol;
					if (object.Equals(currentSymbol.Type, tsymbolType))
					{
						this.AcceptAndMoveNext();
						i++;
						continue;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000080DF File Offset: 0x000062DF
		protected internal void AddMarkerSymbolIfNecessary()
		{
			this.AddMarkerSymbolIfNecessary(this.CurrentLocation);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000080ED File Offset: 0x000062ED
		protected internal void AddMarkerSymbolIfNecessary(SourceLocation location)
		{
			if (this.Span.Symbols.Count == 0 && this.Context.LastAcceptedCharacters != AcceptedCharacters.Any)
			{
				this.Accept(this.Language.CreateMarkerSymbol(location));
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008124 File Offset: 0x00006324
		protected internal void Output(SpanKind kind)
		{
			this.Configure(new SpanKind?(kind), null);
			this.Output();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000814C File Offset: 0x0000634C
		protected internal void Output(SpanKind kind, AcceptedCharacters accepts)
		{
			this.Configure(new SpanKind?(kind), new AcceptedCharacters?(accepts));
			this.Output();
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00008168 File Offset: 0x00006368
		protected internal void Output(AcceptedCharacters accepts)
		{
			this.Configure(null, new AcceptedCharacters?(accepts));
			this.Output();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00008190 File Offset: 0x00006390
		private void Output()
		{
			if (this.Span.Symbols.Count > 0)
			{
				this.Context.AddSpan(this.Span.Build());
				this.Initialize(this.Span);
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000081C7 File Offset: 0x000063C7
		protected IDisposable PushSpanConfig()
		{
			return this.PushSpanConfig(null);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000081E8 File Offset: 0x000063E8
		protected IDisposable PushSpanConfig(Action<SpanBuilder> newConfig)
		{
			return this.PushSpanConfig((newConfig == null) ? null : new Action<SpanBuilder, Action<SpanBuilder>>(delegate(SpanBuilder span, Action<SpanBuilder> _)
			{
				newConfig(span);
			}));
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000823C File Offset: 0x0000643C
		protected IDisposable PushSpanConfig(Action<SpanBuilder, Action<SpanBuilder>> newConfig)
		{
			Action<SpanBuilder> old = this.SpanConfig;
			this.ConfigureSpan(newConfig);
			return new DisposableAction(delegate()
			{
				this.SpanConfig = old;
			});
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000827A File Offset: 0x0000647A
		protected void ConfigureSpan(Action<SpanBuilder> config)
		{
			this.SpanConfig = config;
			this.Initialize(this.Span);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000082AC File Offset: 0x000064AC
		protected void ConfigureSpan(Action<SpanBuilder, Action<SpanBuilder>> config)
		{
			Action<SpanBuilder> prev = this.SpanConfig;
			if (config == null)
			{
				this.SpanConfig = null;
			}
			else
			{
				this.SpanConfig = delegate(SpanBuilder span)
				{
					config(span, prev);
				};
			}
			this.Initialize(this.Span);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00008308 File Offset: 0x00006508
		protected internal void Expected(KnownSymbolType type)
		{
			this.Expected(new TSymbolType[]
			{
				this.Language.GetKnownSymbolType(type)
			});
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00008336 File Offset: 0x00006536
		protected internal void Expected(params TSymbolType[] types)
		{
			this.AcceptAndMoveNext();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000833F File Offset: 0x0000653F
		protected internal bool Optional(KnownSymbolType type)
		{
			return this.Optional(this.Language.GetKnownSymbolType(type));
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00008353 File Offset: 0x00006553
		protected internal bool Optional(TSymbolType type)
		{
			if (this.At(type))
			{
				this.AcceptAndMoveNext();
				return true;
			}
			return false;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008368 File Offset: 0x00006568
		protected internal bool Required(TSymbolType expected, bool errorIfNotFound, string errorBase)
		{
			bool flag = this.At(expected);
			if (!flag && errorIfNotFound)
			{
				string text;
				if (this.Language.IsNewLine(this.CurrentSymbol))
				{
					text = RazorResources.ErrorComponent_Newline;
				}
				else if (this.Language.IsWhiteSpace(this.CurrentSymbol))
				{
					text = RazorResources.ErrorComponent_Whitespace;
				}
				else if (this.EndOfFile)
				{
					text = RazorResources.ErrorComponent_EndOfFile;
				}
				else
				{
					IFormatProvider currentCulture = CultureInfo.CurrentCulture;
					string errorComponent_Character = RazorResources.ErrorComponent_Character;
					object[] array = new object[1];
					object[] array2 = array;
					int num = 0;
					TSymbol currentSymbol = this.CurrentSymbol;
					array2[num] = currentSymbol.Content;
					text = string.Format(currentCulture, errorComponent_Character, array);
				}
				this.Context.OnError(this.CurrentLocation, errorBase, new object[]
				{
					text
				});
			}
			return flag;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00008420 File Offset: 0x00006620
		protected bool EnsureCurrent()
		{
			return this.CurrentSymbol != null || this.NextToken();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00008464 File Offset: 0x00006664
		protected internal void AcceptWhile(TSymbolType type)
		{
			this.AcceptWhile((TSymbol sym) => object.Equals(type, sym.Type));
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000084F0 File Offset: 0x000066F0
		protected internal void AcceptWhile(TSymbolType type1, TSymbolType type2)
		{
			this.AcceptWhile((TSymbol sym) => object.Equals(type1, sym.Type) || object.Equals(type2, sym.Type));
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000085A8 File Offset: 0x000067A8
		protected internal void AcceptWhile(TSymbolType type1, TSymbolType type2, TSymbolType type3)
		{
			this.AcceptWhile((TSymbol sym) => object.Equals(type1, sym.Type) || object.Equals(type2, sym.Type) || object.Equals(type3, sym.Type));
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00008650 File Offset: 0x00006850
		protected internal void AcceptWhile(params TSymbolType[] types)
		{
			this.AcceptWhile((TSymbol sym) => types.Any((TSymbolType expected) => object.Equals(expected, sym.Type)));
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000086AC File Offset: 0x000068AC
		protected internal void AcceptUntil(TSymbolType type)
		{
			this.AcceptWhile((TSymbol sym) => !object.Equals(type, sym.Type));
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00008738 File Offset: 0x00006938
		protected internal void AcceptUntil(TSymbolType type1, TSymbolType type2)
		{
			this.AcceptWhile((TSymbol sym) => !object.Equals(type1, sym.Type) && !object.Equals(type2, sym.Type));
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000087F0 File Offset: 0x000069F0
		protected internal void AcceptUntil(TSymbolType type1, TSymbolType type2, TSymbolType type3)
		{
			this.AcceptWhile((TSymbol sym) => !object.Equals(type1, sym.Type) && !object.Equals(type2, sym.Type) && !object.Equals(type3, sym.Type));
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00008898 File Offset: 0x00006A98
		protected internal void AcceptUntil(params TSymbolType[] types)
		{
			this.AcceptWhile((TSymbol sym) => types.All((TSymbolType expected) => !object.Equals(expected, sym.Type)));
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000088C4 File Offset: 0x00006AC4
		protected internal void AcceptWhile(Func<TSymbol, bool> condition)
		{
			this.Accept(this.ReadWhileLazy(condition));
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000088D3 File Offset: 0x00006AD3
		protected internal IEnumerable<TSymbol> ReadWhile(Func<TSymbol, bool> condition)
		{
			return this.ReadWhileLazy(condition).ToList<TSymbol>();
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000088E4 File Offset: 0x00006AE4
		protected TSymbol AcceptWhiteSpaceInLines()
		{
			TSymbol tsymbol = default(TSymbol);
			while (this.Language.IsWhiteSpace(this.CurrentSymbol) || this.Language.IsNewLine(this.CurrentSymbol))
			{
				if (tsymbol != null)
				{
					this.Accept(tsymbol);
				}
				if (this.Language.IsWhiteSpace(this.CurrentSymbol))
				{
					tsymbol = this.CurrentSymbol;
				}
				else if (this.Language.IsNewLine(this.CurrentSymbol))
				{
					this.Accept(this.CurrentSymbol);
					tsymbol = default(TSymbol);
				}
				this.Tokenizer.Next();
			}
			return tsymbol;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00008983 File Offset: 0x00006B83
		protected bool AtIdentifier(bool allowKeywords)
		{
			return this.CurrentSymbol != null && (this.Language.IsIdentifier(this.CurrentSymbol) || (allowKeywords && this.Language.IsKeyword(this.CurrentSymbol)));
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00008AD0 File Offset: 0x00006CD0
		internal IEnumerable<TSymbol> ReadWhileLazy(Func<TSymbol, bool> condition)
		{
			while (this.EnsureCurrent() && condition(this.CurrentSymbol))
			{
				yield return this.CurrentSymbol;
				this.NextToken();
			}
			yield break;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00008AF4 File Offset: 0x00006CF4
		private void Configure(SpanKind? kind, AcceptedCharacters? accepts)
		{
			if (kind != null)
			{
				this.Span.Kind = kind.Value;
			}
			if (accepts != null)
			{
				this.Span.EditHandler.AcceptedCharacters = accepts.Value;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00008B31 File Offset: 0x00006D31
		protected virtual void OutputSpanBeforeRazorComment()
		{
			throw new InvalidOperationException(RazorResources.Language_Does_Not_Support_RazorComment);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00008B3D File Offset: 0x00006D3D
		private void CommentSpanConfig(SpanBuilder span)
		{
			span.CodeGenerator = SpanCodeGenerator.Null;
			span.EditHandler = SpanEditHandler.CreateDefault(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString));
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00008B68 File Offset: 0x00006D68
		protected void RazorComment()
		{
			if (!this.Language.KnowsSymbolType(KnownSymbolType.CommentStart) || !this.Language.KnowsSymbolType(KnownSymbolType.CommentStar) || !this.Language.KnowsSymbolType(KnownSymbolType.CommentBody))
			{
				throw new InvalidOperationException(RazorResources.Language_Does_Not_Support_RazorComment);
			}
			this.OutputSpanBeforeRazorComment();
			using (this.PushSpanConfig(new Action<SpanBuilder>(this.CommentSpanConfig)))
			{
				using (this.Context.StartBlock(BlockType.Comment))
				{
					this.Context.CurrentBlock.CodeGenerator = new RazorCommentCodeGenerator();
					SourceLocation currentLocation = this.CurrentLocation;
					this.Expected(KnownSymbolType.CommentStart);
					this.Output(SpanKind.Transition, AcceptedCharacters.None);
					this.Expected(KnownSymbolType.CommentStar);
					this.Output(SpanKind.MetaCode, AcceptedCharacters.None);
					this.Optional(KnownSymbolType.CommentBody);
					this.AddMarkerSymbolIfNecessary();
					this.Output(SpanKind.Comment);
					bool flag = false;
					if (!this.Optional(KnownSymbolType.CommentStar))
					{
						flag = true;
						this.Context.OnError(currentLocation, RazorResources.ParseError_RazorComment_Not_Terminated);
					}
					else
					{
						this.Output(SpanKind.MetaCode, AcceptedCharacters.None);
					}
					if (!this.Optional(KnownSymbolType.CommentStart))
					{
						if (!flag)
						{
							this.Context.OnError(currentLocation, RazorResources.ParseError_RazorComment_Not_Terminated);
						}
					}
					else
					{
						this.Output(SpanKind.Transition, AcceptedCharacters.None);
					}
				}
			}
			this.Initialize(this.Span);
		}

		// Token: 0x0400009E RID: 158
		private TokenizerView<TTokenizer, TSymbol, TSymbolType> _tokenizer;
	}
}

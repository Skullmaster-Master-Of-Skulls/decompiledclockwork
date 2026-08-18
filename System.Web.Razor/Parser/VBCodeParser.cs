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

namespace System.Web.Razor.Parser
{
	// Token: 0x0200004D RID: 77
	public class VBCodeParser : TokenizerBackedParser<VBTokenizer, VBSymbol, VBSymbolType>
	{
		// Token: 0x06000375 RID: 885 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
		public VBCodeParser()
		{
			this.DirectParentIsCode = false;
			this.Keywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			this.SetUpKeywords();
			this.SetUpDirectives();
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000E329 File Offset: 0x0000C529
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000E331 File Offset: 0x0000C531
		protected internal ISet<string> Keywords { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000E33A File Offset: 0x0000C53A
		protected override LanguageCharacteristics<VBTokenizer, VBSymbol, VBSymbolType> Language
		{
			get
			{
				return VBLanguageCharacteristics.Instance;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000E341 File Offset: 0x0000C541
		protected override ParserBase OtherParser
		{
			get
			{
				return this.Context.MarkupParser;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000E34E File Offset: 0x0000C54E
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0000E356 File Offset: 0x0000C556
		private bool IsNested { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000E35F File Offset: 0x0000C55F
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0000E367 File Offset: 0x0000C567
		private bool DirectParentIsCode { get; set; }

		// Token: 0x0600037E RID: 894 RVA: 0x0000E370 File Offset: 0x0000C570
		protected override bool IsAtEmbeddedTransition(bool allowTemplatesAndComments, bool allowTransitions)
		{
			return (allowTransitions && this.Language.IsTransition(base.CurrentSymbol) && !base.Was(VBSymbolType.Dot)) || (allowTemplatesAndComments && this.Language.IsCommentStart(base.CurrentSymbol)) || (this.Language.IsTransition(base.CurrentSymbol) && base.NextIs(VBSymbolType.Transition));
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000E3D2 File Offset: 0x0000C5D2
		protected override void HandleEmbeddedTransition()
		{
			this.HandleEmbeddedTransition(null);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000E3DB File Offset: 0x0000C5DB
		protected void HandleEmbeddedTransition(VBSymbol lastWhiteSpace)
		{
			if (base.At(VBSymbolType.RazorCommentTransition))
			{
				base.Accept(lastWhiteSpace);
				base.RazorComment();
				return;
			}
			if (base.At(VBSymbolType.Transition) && !base.Was(VBSymbolType.Dot))
			{
				this.HandleTransition(lastWhiteSpace);
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000E41C File Offset: 0x0000C61C
		public override void ParseBlock()
		{
			if (this.Context == null)
			{
				throw new InvalidOperationException(RazorResources.Parser_Context_Not_Set);
			}
			using (base.PushSpanConfig())
			{
				if (this.Context == null)
				{
					throw new InvalidOperationException(RazorResources.Parser_Context_Not_Set);
				}
				base.Initialize(base.Span);
				base.NextToken();
				using (this.Context.StartBlock())
				{
					IEnumerable<VBSymbol> symbols = base.ReadWhile((VBSymbol sym) => sym.Type == VBSymbolType.WhiteSpace);
					if (base.At(VBSymbolType.Transition))
					{
						base.Accept(symbols);
						base.Span.CodeGenerator = new StatementCodeGenerator();
						base.Output(SpanKind.Code);
					}
					else
					{
						base.PutBack(symbols);
						base.EnsureCurrent();
					}
					if (base.Optional(VBSymbolType.Transition))
					{
						base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
						base.Span.CodeGenerator = SpanCodeGenerator.Null;
						base.Output(SpanKind.Transition);
					}
					this.Context.CurrentBlock.Type = new BlockType?(BlockType.Expression);
					this.Context.CurrentBlock.CodeGenerator = new ExpressionCodeGenerator();
					bool flag = false;
					Action<SpanBuilder> newConfig = null;
					if (!base.EndOfFile)
					{
						VBSymbolType type = base.CurrentSymbol.Type;
						switch (type)
						{
						case VBSymbolType.WhiteSpace:
						case VBSymbolType.NewLine:
							newConfig = new Action<SpanBuilder>(this.ImplictExpressionSpanConfig);
							this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_Unexpected_WhiteSpace_At_Start_Of_CodeBlock_VB);
							goto IL_22F;
						case VBSymbolType.LineContinuation:
						case VBSymbolType.Comment:
							break;
						case VBSymbolType.Identifier:
							if (!this.TryDirectiveBlock(ref flag))
							{
								this.ImplicitExpression();
								goto IL_22F;
							}
							goto IL_22F;
						case VBSymbolType.Keyword:
							this.Context.CurrentBlock.Type = new BlockType?(BlockType.Statement);
							this.Context.CurrentBlock.CodeGenerator = BlockCodeGenerator.Null;
							flag = this.KeywordBlock();
							goto IL_22F;
						default:
							if (type == VBSymbolType.LeftParenthesis)
							{
								flag = this.ExplicitExpression();
								goto IL_22F;
							}
							break;
						}
						newConfig = new Action<SpanBuilder>(this.ImplictExpressionSpanConfig);
						this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_Unexpected_Character_At_Start_Of_CodeBlock_VB, new object[]
						{
							base.CurrentSymbol.Content
						});
					}
					else
					{
						newConfig = new Action<SpanBuilder>(this.ImplictExpressionSpanConfig);
						this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_Unexpected_EndOfFile_At_Start_Of_CodeBlock);
					}
					IL_22F:
					using (base.PushSpanConfig(newConfig))
					{
						if (!flag && base.Span.Symbols.Count == 0 && this.Context.LastAcceptedCharacters != AcceptedCharacters.Any)
						{
							base.AddMarkerSymbolIfNecessary();
						}
						base.Output(SpanKind.Code);
						base.PutCurrentBack();
					}
				}
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000E70C File Offset: 0x0000C90C
		private void ImplictExpressionSpanConfig(SpanBuilder span)
		{
			span.CodeGenerator = new ExpressionCodeGenerator();
			span.EditHandler = new ImplicitExpressionEditHandler(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString), this.Keywords, this.DirectParentIsCode)
			{
				AcceptedCharacters = AcceptedCharacters.NonWhiteSpace
			};
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000E798 File Offset: 0x0000C998
		private Action<SpanBuilder> StatementBlockSpanConfiguration(SpanCodeGenerator codeGenerator)
		{
			return delegate(SpanBuilder span)
			{
				span.Kind = SpanKind.Code;
				span.CodeGenerator = codeGenerator;
				span.EditHandler = SpanEditHandler.CreateDefault(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString));
			};
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000E7C8 File Offset: 0x0000C9C8
		private bool TryDirectiveBlock(ref bool complete)
		{
			Func<bool> func;
			if (this._directiveHandlers.TryGetValue(base.CurrentSymbol.Content, out func))
			{
				this.Context.CurrentBlock.CodeGenerator = BlockCodeGenerator.Null;
				complete = func();
				return true;
			}
			return false;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000E810 File Offset: 0x0000CA10
		private bool KeywordBlock()
		{
			Func<bool> func;
			if (this._keywordHandlers.TryGetValue(base.CurrentSymbol.Keyword.Value, out func))
			{
				base.Span.CodeGenerator = new StatementCodeGenerator();
				this.Context.CurrentBlock.Type = new BlockType?(BlockType.Statement);
				return func();
			}
			this.ImplicitExpression();
			return false;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000E880 File Offset: 0x0000CA80
		private bool ExplicitExpression()
		{
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Expression);
			this.Context.CurrentBlock.CodeGenerator = new ExpressionCodeGenerator();
			SourceLocation currentLocation = base.CurrentLocation;
			base.Expected(new VBSymbolType[]
			{
				VBSymbolType.LeftParenthesis
			});
			base.Span.CodeGenerator = SpanCodeGenerator.Null;
			base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			base.Output(SpanKind.MetaCode);
			base.Span.CodeGenerator = new ExpressionCodeGenerator();
			bool result;
			using (base.PushSpanConfig(delegate(SpanBuilder span)
			{
				span.CodeGenerator = new ExpressionCodeGenerator();
			}))
			{
				if (!base.Balance(BalancingModes.BacktrackOnFailure | BalancingModes.NoErrorOnFailure | BalancingModes.AllowCommentsAndTemplates, VBSymbolType.LeftParenthesis, VBSymbolType.RightParenthesis, currentLocation))
				{
					this.Context.OnError(currentLocation, RazorResources.ParseError_Expected_EndOfBlock_Before_EOF, new object[]
					{
						RazorResources.BlockName_ExplicitExpression,
						VBSymbol.GetSample(VBSymbolType.RightParenthesis),
						VBSymbol.GetSample(VBSymbolType.LeftParenthesis)
					});
					base.AcceptUntil(VBSymbolType.NewLine);
					base.AddMarkerSymbolIfNecessary();
					base.Output(SpanKind.Code);
					base.PutCurrentBack();
					result = false;
				}
				else
				{
					base.AddMarkerSymbolIfNecessary();
					base.Output(SpanKind.Code);
					base.Expected(new VBSymbolType[]
					{
						VBSymbolType.RightParenthesis
					});
					base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
					base.Span.CodeGenerator = SpanCodeGenerator.Null;
					base.Output(SpanKind.MetaCode);
					base.PutCurrentBack();
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000EA28 File Offset: 0x0000CC28
		private void ImplicitExpression()
		{
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Expression);
			this.Context.CurrentBlock.CodeGenerator = new ExpressionCodeGenerator();
			using (base.PushSpanConfig(new Action<SpanBuilder>(this.ImplictExpressionSpanConfig)))
			{
				base.Expected(new VBSymbolType[]
				{
					VBSymbolType.Identifier,
					VBSymbolType.Keyword
				});
				base.Span.CodeGenerator = new ExpressionCodeGenerator();
				while (!base.EndOfFile)
				{
					VBSymbolType type = base.CurrentSymbol.Type;
					if (type != VBSymbolType.LeftParenthesis)
					{
						if (type != VBSymbolType.Dot)
						{
							base.PutCurrentBack();
							break;
						}
						VBSymbol currentSymbol = base.CurrentSymbol;
						base.NextToken();
						if (base.At(VBSymbolType.Identifier) || base.At(VBSymbolType.Keyword))
						{
							base.Accept(currentSymbol);
							base.AcceptAndMoveNext();
						}
						else
						{
							if (!base.At(VBSymbolType.Transition))
							{
								base.PutCurrentBack();
								if (this.IsNested)
								{
									base.Accept(currentSymbol);
								}
								else
								{
									base.PutBack(currentSymbol);
								}
								break;
							}
							VBSymbol currentSymbol2 = base.CurrentSymbol;
							base.NextToken();
							if (base.At(VBSymbolType.Identifier) || base.At(VBSymbolType.Keyword))
							{
								base.Accept(currentSymbol);
								base.Accept(currentSymbol2);
								base.AcceptAndMoveNext();
							}
							else
							{
								base.PutBack(currentSymbol2);
								base.PutBack(currentSymbol);
							}
						}
					}
					else
					{
						SourceLocation currentLocation = base.CurrentLocation;
						base.AcceptAndMoveNext();
						Action<SpanBuilder> oldConfig = base.SpanConfig;
						using (base.PushSpanConfig())
						{
							base.ConfigureSpan(delegate(SpanBuilder span)
							{
								oldConfig(span);
								span.EditHandler.AcceptedCharacters = AcceptedCharacters.Any;
							});
							base.Balance(BalancingModes.AllowCommentsAndTemplates, VBSymbolType.LeftParenthesis, VBSymbolType.RightParenthesis, currentLocation);
						}
						if (base.Optional(VBSymbolType.RightParenthesis))
						{
							base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.NonWhiteSpace;
						}
					}
				}
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000EC3C File Offset: 0x0000CE3C
		protected void MapKeyword(VBKeyword keyword, Func<bool> action)
		{
			this._keywordHandlers[keyword] = action;
			this.Keywords.Add(keyword.ToString());
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000EC62 File Offset: 0x0000CE62
		protected void MapDirective(string directive, Func<bool> action)
		{
			this._directiveHandlers[directive] = action;
			this.Keywords.Add(directive);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000EC7E File Offset: 0x0000CE7E
		[Conditional("DEBUG")]
		protected void Assert(VBKeyword keyword)
		{
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000EC80 File Offset: 0x0000CE80
		protected bool At(VBKeyword keyword)
		{
			return base.At(VBSymbolType.Keyword) && base.CurrentSymbol.Keyword == keyword;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000ECB9 File Offset: 0x0000CEB9
		protected void OtherParserBlock()
		{
			this.OtherParserBlock(null, null);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000ECC4 File Offset: 0x0000CEC4
		protected void OtherParserBlock(string startSequence, string endSequence)
		{
			using (base.PushSpanConfig())
			{
				if (base.Span.Symbols.Count > 0)
				{
					base.Output(SpanKind.Code);
				}
				this.Context.SwitchActiveParser();
				bool directParentIsCode = this.DirectParentIsCode;
				this.DirectParentIsCode = false;
				if (!string.IsNullOrEmpty(startSequence) || !string.IsNullOrEmpty(endSequence))
				{
					this.Context.MarkupParser.ParseSection(Tuple.Create<string, string>(startSequence, endSequence), false);
				}
				else
				{
					this.Context.MarkupParser.ParseBlock();
				}
				this.DirectParentIsCode = directParentIsCode;
				this.Context.SwitchActiveParser();
				base.EnsureCurrent();
			}
			base.Initialize(base.Span);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000ED88 File Offset: 0x0000CF88
		protected void HandleTransition(VBSymbol lastWhiteSpace)
		{
			if (base.At(VBSymbolType.RazorCommentTransition))
			{
				base.Accept(lastWhiteSpace);
				base.RazorComment();
				return;
			}
			VBSymbol currentSymbol = base.CurrentSymbol;
			base.NextToken();
			if (base.At(VBSymbolType.LessThan) || base.At(VBSymbolType.Colon))
			{
				base.PutCurrentBack();
				base.PutBack(currentSymbol);
				if (this.Context.DesignTimeMode)
				{
					base.Accept(lastWhiteSpace);
				}
				else
				{
					base.PutBack(lastWhiteSpace);
				}
				this.OtherParserBlock();
				return;
			}
			if (base.At(VBSymbolType.Transition))
			{
				if (this.Context.IsWithin(BlockType.Template))
				{
					this.Context.OnError(currentSymbol.Start, RazorResources.ParseError_InlineMarkup_Blocks_Cannot_Be_Nested);
				}
				base.Accept(lastWhiteSpace);
				VBSymbol currentSymbol2 = base.CurrentSymbol;
				base.NextToken();
				if (base.At(VBSymbolType.LessThan) || base.At(VBSymbolType.Colon))
				{
					base.PutCurrentBack();
					base.PutBack(currentSymbol2);
					base.PutBack(currentSymbol);
					base.Output(SpanKind.Code);
					using (this.Context.StartBlock(BlockType.Template))
					{
						this.Context.CurrentBlock.CodeGenerator = new TemplateBlockCodeGenerator();
						this.OtherParserBlock();
						base.Initialize(base.Span);
						return;
					}
				}
				base.Accept(currentSymbol);
				base.Accept(currentSymbol2);
				return;
			}
			base.Accept(lastWhiteSpace);
			base.PutCurrentBack();
			base.PutBack(currentSymbol);
			bool isNested = this.IsNested;
			this.IsNested = true;
			this.NestedBlock();
			this.IsNested = isNested;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000EF04 File Offset: 0x0000D104
		protected override void OutputSpanBeforeRazorComment()
		{
			base.Output(SpanKind.Code);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000EF10 File Offset: 0x0000D110
		protected bool ReservedWord()
		{
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Directive);
			this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_ReservedWord, new object[]
			{
				base.CurrentSymbol.Content
			});
			base.Span.CodeGenerator = SpanCodeGenerator.Null;
			base.AcceptAndMoveNext();
			base.Output(SpanKind.MetaCode, AcceptedCharacters.None);
			return true;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000EF80 File Offset: 0x0000D180
		protected void NestedBlock()
		{
			using (base.PushSpanConfig())
			{
				base.Output(SpanKind.Code);
				bool directParentIsCode = this.DirectParentIsCode;
				this.DirectParentIsCode = true;
				this.ParseBlock();
				this.DirectParentIsCode = directParentIsCode;
			}
			base.Initialize(base.Span);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
		protected bool Required(VBSymbolType expected, string errorBase)
		{
			if (!base.Optional(expected))
			{
				this.Context.OnError(base.CurrentLocation, errorBase, new object[]
				{
					this.GetCurrentSymbolDisplay()
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000F01C File Offset: 0x0000D21C
		protected bool Optional(VBKeyword keyword)
		{
			if (this.At(keyword))
			{
				base.AcceptAndMoveNext();
				return true;
			}
			return false;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000F031 File Offset: 0x0000D231
		protected void AcceptVBSpaces()
		{
			base.Accept(this.ReadVBSpacesLazy());
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000F03F File Offset: 0x0000D23F
		protected IEnumerable<VBSymbol> ReadVBSpaces()
		{
			return this.ReadVBSpacesLazy().ToList<VBSymbol>();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000F04C File Offset: 0x0000D24C
		public bool IsDirectiveDefined(string directive)
		{
			return this._directiveHandlers.ContainsKey(directive);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000F3B4 File Offset: 0x0000D5B4
		private IEnumerable<VBSymbol> ReadVBSpacesLazy()
		{
			foreach (VBSymbol symbol in base.ReadWhileLazy((VBSymbol sym) => sym.Type == VBSymbolType.WhiteSpace))
			{
				yield return symbol;
			}
			while (base.At(VBSymbolType.LineContinuation))
			{
				int bookmark = base.CurrentLocation.AbsoluteIndex;
				VBSymbol under = base.CurrentSymbol;
				base.NextToken();
				if (!base.At(VBSymbolType.NewLine))
				{
					this.Context.Source.Position = bookmark;
					base.NextToken();
					break;
				}
				yield return under;
				yield return base.CurrentSymbol;
				base.NextToken();
				foreach (VBSymbol symbol2 in this.ReadVBSpaces())
				{
					yield return symbol2;
				}
			}
			yield break;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000F3D4 File Offset: 0x0000D5D4
		private void SetUpDirectives()
		{
			this.MapDirective(SyntaxConstants.VB.CodeKeyword, this.EndTerminatedDirective(SyntaxConstants.VB.CodeKeyword, BlockType.Statement, new StatementCodeGenerator(), true));
			this.MapDirective(SyntaxConstants.VB.FunctionsKeyword, this.EndTerminatedDirective(SyntaxConstants.VB.FunctionsKeyword, BlockType.Functions, new TypeMemberCodeGenerator(), false));
			this.MapDirective(SyntaxConstants.VB.SectionKeyword, new Func<bool>(this.SectionDirective));
			this.MapDirective(SyntaxConstants.VB.HelperKeyword, new Func<bool>(this.HelperDirective));
			this.MapDirective(SyntaxConstants.VB.LayoutKeyword, new Func<bool>(this.LayoutDirective));
			this.MapDirective(SyntaxConstants.VB.SessionStateKeyword, new Func<bool>(this.SessionStateDirective));
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000F47C File Offset: 0x0000D67C
		protected virtual bool LayoutDirective()
		{
			base.AcceptAndMoveNext();
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Directive);
			bool flag = base.At(VBSymbolType.WhiteSpace);
			base.AcceptWhile(VBSymbolType.WhiteSpace);
			base.Output(SpanKind.MetaCode, flag ? AcceptedCharacters.None : AcceptedCharacters.Any);
			base.AcceptUntil(VBSymbolType.NewLine);
			base.Span.CodeGenerator = new SetLayoutCodeGenerator(base.Span.GetContent());
			base.Span.EditHandler.EditorHints = (EditorHints.VirtualPath | EditorHints.LayoutPage);
			bool flag2 = base.Optional(VBSymbolType.NewLine);
			base.AddMarkerSymbolIfNecessary();
			base.Output(SpanKind.MetaCode, flag2 ? AcceptedCharacters.None : AcceptedCharacters.Any);
			return true;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000F520 File Offset: 0x0000D720
		protected virtual bool SessionStateDirective()
		{
			base.AcceptAndMoveNext();
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Directive);
			bool flag = base.At(VBSymbolType.WhiteSpace);
			base.AcceptWhile(VBSymbolType.WhiteSpace);
			base.Output(SpanKind.MetaCode, flag ? AcceptedCharacters.None : AcceptedCharacters.Any);
			base.AcceptUntil(VBSymbolType.NewLine);
			string value = string.Concat(from sym in base.Span.Symbols
			select sym.Content);
			base.Span.CodeGenerator = new RazorDirectiveAttributeCodeGenerator(SyntaxConstants.VB.SessionStateKeyword, value);
			bool flag2 = base.Optional(VBSymbolType.NewLine);
			base.AddMarkerSymbolIfNecessary();
			base.Output(SpanKind.MetaCode, flag2 ? AcceptedCharacters.None : AcceptedCharacters.Any);
			return true;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000F5D4 File Offset: 0x0000D7D4
		protected virtual bool HelperDirective()
		{
			if (this.Context.IsWithin(BlockType.Helper))
			{
				this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_Helpers_Cannot_Be_Nested);
			}
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Helper);
			SourceLocation currentLocation = base.CurrentLocation;
			base.AcceptAndMoveNext();
			VBSymbolType vbsymbolType = VBSymbolType.Unknown;
			if (base.CurrentSymbol != null)
			{
				vbsymbolType = base.CurrentSymbol.Type;
			}
			VBSymbol vbsymbol = null;
			if (base.At(VBSymbolType.NewLine))
			{
				base.AcceptAndMoveNext();
			}
			else
			{
				vbsymbol = base.AcceptSingleWhiteSpaceCharacter();
			}
			if (vbsymbolType == VBSymbolType.WhiteSpace || vbsymbolType == VBSymbolType.NewLine)
			{
				base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			}
			base.Output(SpanKind.MetaCode);
			if (vbsymbolType != VBSymbolType.WhiteSpace)
			{
				string text;
				if (base.At(VBSymbolType.NewLine))
				{
					text = RazorResources.ErrorComponent_Newline;
				}
				else if (base.EndOfFile)
				{
					text = RazorResources.ErrorComponent_EndOfFile;
				}
				else
				{
					text = string.Format(CultureInfo.CurrentCulture, RazorResources.ErrorComponent_Character, new object[]
					{
						base.CurrentSymbol.Content
					});
				}
				this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_Unexpected_Character_At_Helper_Name_Start, new object[]
				{
					text
				});
				base.PutCurrentBack();
				base.Output(SpanKind.Code);
				return false;
			}
			if (vbsymbol != null)
			{
				base.Accept(vbsymbol);
			}
			bool flag = !this.Required(VBSymbolType.Identifier, RazorResources.ParseError_Unexpected_Character_At_Helper_Name_Start);
			base.AcceptWhile(VBSymbolType.WhiteSpace);
			SourceLocation currentLocation2 = base.CurrentLocation;
			bool flag2 = false;
			if (!base.Optional(VBSymbolType.LeftParenthesis))
			{
				if (!flag)
				{
					this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_MissingCharAfterHelperName, new object[]
					{
						VBSymbol.GetSample(VBSymbolType.LeftParenthesis)
					});
				}
			}
			else if (!base.Balance(BalancingModes.NoErrorOnFailure, VBSymbolType.LeftParenthesis, VBSymbolType.RightParenthesis, currentLocation2))
			{
				this.Context.OnError(currentLocation2, RazorResources.ParseError_UnterminatedHelperParameterList);
			}
			else
			{
				base.Expected(new VBSymbolType[]
				{
					VBSymbolType.RightParenthesis
				});
				flag2 = true;
			}
			base.AddMarkerSymbolIfNecessary();
			this.Context.CurrentBlock.CodeGenerator = new HelperCodeGenerator(base.Span.GetContent(), flag2);
			AutoCompleteEditHandler autoCompleteEditHandler = new AutoCompleteEditHandler(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString));
			base.Span.EditHandler = autoCompleteEditHandler;
			base.Output(SpanKind.Code);
			if (flag2)
			{
				bool isNested = this.IsNested;
				this.IsNested = true;
				using (this.Context.StartBlock(BlockType.Statement))
				{
					using (base.PushSpanConfig(this.StatementBlockSpanConfiguration(new StatementCodeGenerator())))
					{
						try
						{
							if (!this.EndTerminatedDirectiveBody(SyntaxConstants.VB.HelperKeyword, currentLocation, true))
							{
								if (this.Context.LastAcceptedCharacters != AcceptedCharacters.Any)
								{
									base.AddMarkerSymbolIfNecessary();
								}
								autoCompleteEditHandler.AutoCompleteString = SyntaxConstants.VB.EndHelperKeyword;
								return false;
							}
							return true;
						}
						finally
						{
							base.Output(SpanKind.Code);
							this.IsNested = isNested;
						}
					}
				}
			}
			base.Output(SpanKind.Code);
			base.PutCurrentBack();
			return false;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000F8CC File Offset: 0x0000DACC
		protected virtual bool SectionDirective()
		{
			SourceLocation currentLocation = base.CurrentLocation;
			base.AcceptAndMoveNext();
			if (this.Context.IsWithin(BlockType.Section))
			{
				this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_Sections_Cannot_Be_Nested, new object[]
				{
					RazorResources.SectionExample_VB
				});
			}
			if (base.At(VBSymbolType.NewLine))
			{
				base.AcceptAndMoveNext();
			}
			else
			{
				this.AcceptVBSpaces();
			}
			string text = null;
			if (!base.At(VBSymbolType.Identifier))
			{
				this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_Unexpected_Character_At_Section_Name_Start, new object[]
				{
					this.GetCurrentSymbolDisplay()
				});
			}
			else
			{
				text = base.CurrentSymbol.Content;
				base.AcceptAndMoveNext();
			}
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Section);
			this.Context.CurrentBlock.CodeGenerator = new SectionCodeGenerator(text ?? string.Empty);
			AutoCompleteEditHandler autoCompleteEditHandler = new AutoCompleteEditHandler(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString));
			base.Span.EditHandler = autoCompleteEditHandler;
			base.PutCurrentBack();
			base.Output(SpanKind.MetaCode);
			this.OtherParserBlock(null, SyntaxConstants.VB.EndSectionKeyword);
			base.Span.CodeGenerator = SpanCodeGenerator.Null;
			bool result = false;
			if (!this.At(VBKeyword.End))
			{
				this.Context.OnError(currentLocation, RazorResources.ParseError_BlockNotTerminated, new object[]
				{
					SyntaxConstants.VB.SectionKeyword,
					SyntaxConstants.VB.EndSectionKeyword
				});
				autoCompleteEditHandler.AutoCompleteString = SyntaxConstants.VB.EndSectionKeyword;
			}
			else
			{
				base.AcceptAndMoveNext();
				base.AcceptWhile(VBSymbolType.WhiteSpace);
				if (!this.At(SyntaxConstants.VB.SectionKeyword))
				{
					this.Context.OnError(currentLocation, RazorResources.ParseError_BlockNotTerminated, new object[]
					{
						SyntaxConstants.VB.SectionKeyword,
						SyntaxConstants.VB.EndSectionKeyword
					});
				}
				else
				{
					base.AcceptAndMoveNext();
					base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
					result = true;
				}
			}
			base.PutCurrentBack();
			base.Output(SpanKind.MetaCode);
			return result;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000FBE4 File Offset: 0x0000DDE4
		protected virtual Func<bool> EndTerminatedDirective(string directive, BlockType blockType, SpanCodeGenerator codeGenerator, bool allowMarkup)
		{
			return delegate()
			{
				SourceLocation currentLocation = this.CurrentLocation;
				this.Context.CurrentBlock.Type = new BlockType?(blockType);
				this.AcceptAndMoveNext();
				this.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
				this.Span.CodeGenerator = SpanCodeGenerator.Null;
				this.Output(SpanKind.MetaCode);
				bool result;
				using (this.PushSpanConfig(this.StatementBlockSpanConfiguration(codeGenerator)))
				{
					AutoCompleteEditHandler autoCompleteEditHandler = new AutoCompleteEditHandler(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString));
					this.Span.EditHandler = autoCompleteEditHandler;
					if (!this.EndTerminatedDirectiveBody(directive, currentLocation, allowMarkup))
					{
						autoCompleteEditHandler.AutoCompleteString = SyntaxConstants.VB.EndKeyword + " " + directive;
						result = false;
					}
					else
					{
						result = true;
					}
				}
				return result;
			};
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000FC28 File Offset: 0x0000DE28
		protected virtual bool EndTerminatedDirectiveBody(string directive, SourceLocation blockStart, bool allowAllTransitions)
		{
			while (!base.EndOfFile)
			{
				VBSymbol vbsymbol = base.AcceptWhiteSpaceInLines();
				if (this.IsAtEmbeddedTransition(allowAllTransitions, allowAllTransitions))
				{
					this.HandleEmbeddedTransition(vbsymbol);
				}
				else if (this.At(VBKeyword.End))
				{
					base.Accept(vbsymbol);
					VBSymbol currentSymbol = base.CurrentSymbol;
					base.NextToken();
					IEnumerable<VBSymbol> symbols = this.ReadVBSpaces();
					if (this.At(directive))
					{
						if (this.Context.LastAcceptedCharacters != AcceptedCharacters.Any)
						{
							base.AddMarkerSymbolIfNecessary(currentSymbol.Start);
						}
						base.Output(SpanKind.Code);
						base.Accept(currentSymbol);
						base.Accept(symbols);
						base.AcceptAndMoveNext();
						base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
						base.Span.CodeGenerator = SpanCodeGenerator.Null;
						base.Output(SpanKind.MetaCode);
						return true;
					}
					base.Accept(currentSymbol);
					base.Accept(symbols);
					base.AcceptAndMoveNext();
				}
				else
				{
					base.Accept(vbsymbol);
					base.AcceptAndMoveNext();
				}
			}
			this.Context.OnError(blockStart, RazorResources.ParseError_BlockNotTerminated, new object[]
			{
				directive,
				SyntaxConstants.VB.EndKeyword + " " + directive
			});
			return false;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000FD4A File Offset: 0x0000DF4A
		protected bool At(string directive)
		{
			return base.At(VBSymbolType.Identifier) && string.Equals(base.CurrentSymbol.Content, directive, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000FD69 File Offset: 0x0000DF69
		[Conditional("DEBUG")]
		protected void AssertDirective(string directive)
		{
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000FD6C File Offset: 0x0000DF6C
		private string GetCurrentSymbolDisplay()
		{
			if (base.EndOfFile)
			{
				return RazorResources.ErrorComponent_EndOfFile;
			}
			if (base.At(VBSymbolType.NewLine))
			{
				return RazorResources.ErrorComponent_Newline;
			}
			if (base.At(VBSymbolType.WhiteSpace))
			{
				return RazorResources.ErrorComponent_Whitespace;
			}
			return string.Format(CultureInfo.CurrentCulture, RazorResources.ErrorComponent_Character, new object[]
			{
				base.CurrentSymbol.Content
			});
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000FDCC File Offset: 0x0000DFCC
		private void SetUpKeywords()
		{
			this.MapKeyword(VBKeyword.Using, this.EndTerminatedStatement(VBKeyword.Using, false, false));
			this.MapKeyword(VBKeyword.While, this.EndTerminatedStatement(VBKeyword.While, true, true));
			this.MapKeyword(VBKeyword.If, this.EndTerminatedStatement(VBKeyword.If, false, false));
			this.MapKeyword(VBKeyword.Select, this.EndTerminatedStatement(VBKeyword.Select, true, false, SyntaxConstants.VB.SelectCaseKeyword));
			this.MapKeyword(VBKeyword.Try, this.EndTerminatedStatement(VBKeyword.Try, true, false));
			this.MapKeyword(VBKeyword.With, this.EndTerminatedStatement(VBKeyword.With, false, false));
			this.MapKeyword(VBKeyword.SyncLock, this.EndTerminatedStatement(VBKeyword.SyncLock, false, false));
			this.MapKeyword(VBKeyword.For, this.KeywordTerminatedStatement(VBKeyword.For, VBKeyword.Next, true, true));
			this.MapKeyword(VBKeyword.Do, this.KeywordTerminatedStatement(VBKeyword.Do, VBKeyword.Loop, true, true));
			this.MapKeyword(VBKeyword.Imports, new Func<bool>(this.ImportsStatement));
			this.MapKeyword(VBKeyword.Option, new Func<bool>(this.OptionStatement));
			this.MapKeyword(VBKeyword.Inherits, new Func<bool>(this.InheritsStatement));
			this.MapKeyword(VBKeyword.Class, new Func<bool>(this.ReservedWord));
			this.MapKeyword(VBKeyword.Namespace, new Func<bool>(this.ReservedWord));
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000FF04 File Offset: 0x0000E104
		protected virtual bool InheritsStatement()
		{
			base.Span.CodeGenerator = SpanCodeGenerator.Null;
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Directive);
			base.AcceptAndMoveNext();
			SourceLocation currentLocation = base.CurrentLocation;
			if (base.At(VBSymbolType.WhiteSpace))
			{
				base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			}
			base.AcceptWhile(VBSymbolType.WhiteSpace);
			base.Output(SpanKind.MetaCode);
			if (base.EndOfFile || base.At(VBSymbolType.WhiteSpace) || base.At(VBSymbolType.NewLine))
			{
				this.Context.OnError(currentLocation, RazorResources.ParseError_InheritsKeyword_Must_Be_Followed_By_TypeName);
			}
			base.AcceptUntil(VBSymbolType.NewLine);
			if (!this.Context.DesignTimeMode)
			{
				base.Optional(VBSymbolType.NewLine);
			}
			string text = base.Span.GetContent();
			base.Span.CodeGenerator = new SetBaseTypeCodeGenerator(text.Trim());
			base.Output(SpanKind.Code);
			return false;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000FFE4 File Offset: 0x0000E1E4
		protected virtual bool OptionStatement()
		{
			try
			{
				this.Context.CurrentBlock.Type = new BlockType?(BlockType.Directive);
				base.AcceptAndMoveNext();
				base.AcceptWhile(VBSymbolType.WhiteSpace);
				if (!base.At(VBSymbolType.Identifier))
				{
					if (base.CurrentSymbol != null)
					{
						this.Context.OnError(base.CurrentLocation, string.Format(CultureInfo.CurrentCulture, RazorResources.ParseError_Unexpected, new object[]
						{
							base.CurrentSymbol.Content
						}));
					}
					return false;
				}
				SourceLocation currentLocation = base.CurrentLocation;
				string content = base.CurrentSymbol.Content;
				base.AcceptAndMoveNext();
				base.AcceptWhile(VBSymbolType.WhiteSpace);
				bool flag;
				if (this.At(VBKeyword.On))
				{
					base.AcceptAndMoveNext();
					flag = true;
				}
				else
				{
					if (!base.At(VBSymbolType.Identifier))
					{
						if (!base.EndOfFile)
						{
							this.Context.OnError(base.CurrentLocation, string.Format(CultureInfo.CurrentCulture, RazorResources.ParseError_Unexpected, new object[]
							{
								base.CurrentSymbol.Content
							}));
							base.AcceptAndMoveNext();
						}
						return false;
					}
					if (!string.Equals(base.CurrentSymbol.Content, SyntaxConstants.VB.OffKeyword, StringComparison.OrdinalIgnoreCase))
					{
						this.Context.OnError(base.CurrentLocation, string.Format(CultureInfo.CurrentCulture, RazorResources.ParseError_InvalidOptionValue, new object[]
						{
							content,
							base.CurrentSymbol.Content
						}));
						base.AcceptAndMoveNext();
						return false;
					}
					base.AcceptAndMoveNext();
					flag = false;
				}
				if (string.Equals(content, SyntaxConstants.VB.StrictKeyword, StringComparison.OrdinalIgnoreCase))
				{
					base.Span.CodeGenerator = SetVBOptionCodeGenerator.Strict(flag);
				}
				else if (string.Equals(content, SyntaxConstants.VB.ExplicitKeyword, StringComparison.OrdinalIgnoreCase))
				{
					base.Span.CodeGenerator = SetVBOptionCodeGenerator.Explicit(flag);
				}
				else
				{
					base.Span.CodeGenerator = new SetVBOptionCodeGenerator(content, flag);
					this.Context.OnError(currentLocation, RazorResources.ParseError_UnknownOption, new object[]
					{
						content
					});
				}
			}
			finally
			{
				if (base.Span.Symbols.Count > 0)
				{
					base.Output(SpanKind.MetaCode);
				}
			}
			return true;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00010224 File Offset: 0x0000E424
		protected virtual bool ImportsStatement()
		{
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Directive);
			base.AcceptAndMoveNext();
			this.AcceptVBSpaces();
			if (base.At(VBSymbolType.WhiteSpace) || base.At(VBSymbolType.NewLine))
			{
				this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_NamespaceOrTypeAliasExpected);
			}
			base.AcceptUntil(VBSymbolType.NewLine);
			base.Optional(VBSymbolType.NewLine);
			string ns = string.Concat(from s in base.Span.Symbols.Skip(1)
			select s.Content);
			base.Span.CodeGenerator = new AddImportCodeGenerator(ns, SyntaxConstants.VB.ImportsKeywordLength);
			base.Output(SpanKind.MetaCode);
			return false;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000102E2 File Offset: 0x0000E4E2
		protected virtual Func<bool> EndTerminatedStatement(VBKeyword keyword, bool supportsExit, bool supportsContinue)
		{
			return this.EndTerminatedStatement(keyword, supportsExit, supportsContinue, keyword.ToString());
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001051C File Offset: 0x0000E71C
		protected virtual Func<bool> EndTerminatedStatement(VBKeyword keyword, bool supportsExit, bool supportsContinue, string blockName)
		{
			return delegate()
			{
				bool result;
				using (this.PushSpanConfig(this.StatementBlockSpanConfiguration(new StatementCodeGenerator())))
				{
					SourceLocation currentLocation = this.CurrentLocation;
					this.AcceptAndMoveNext();
					while (!this.EndOfFile)
					{
						VBSymbol vbsymbol = this.AcceptWhiteSpaceInLines();
						if (this.IsAtEmbeddedTransition(true, true))
						{
							this.HandleEmbeddedTransition(vbsymbol);
						}
						else
						{
							this.Accept(vbsymbol);
							if ((supportsExit && this.At(VBKeyword.Exit)) || (supportsContinue && this.At(VBKeyword.Continue)))
							{
								this.HandleExitOrContinue(keyword);
							}
							else if (this.At(VBKeyword.End))
							{
								this.AcceptAndMoveNext();
								this.AcceptVBSpaces();
								if (this.At(keyword))
								{
									this.AcceptAndMoveNext();
									if (!this.Context.DesignTimeMode)
									{
										this.Optional(VBSymbolType.NewLine);
									}
									this.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
									return false;
								}
							}
							else if (this.At(keyword))
							{
								this.EndTerminatedStatement(keyword, supportsExit, supportsContinue)();
							}
							else if (!this.EndOfFile)
							{
								this.AcceptAndMoveNext();
							}
						}
					}
					this.Context.OnError(currentLocation, RazorResources.ParseError_BlockNotTerminated, new object[]
					{
						blockName,
						VBKeyword.End + " " + keyword
					});
					result = false;
				}
				return result;
			};
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00010740 File Offset: 0x0000E940
		protected virtual Func<bool> KeywordTerminatedStatement(VBKeyword start, VBKeyword terminator, bool supportsExit, bool supportsContinue)
		{
			return delegate()
			{
				bool result;
				using (this.PushSpanConfig(this.StatementBlockSpanConfiguration(new StatementCodeGenerator())))
				{
					SourceLocation currentLocation = this.CurrentLocation;
					this.AcceptAndMoveNext();
					while (!this.EndOfFile)
					{
						VBSymbol vbsymbol = this.AcceptWhiteSpaceInLines();
						if (this.IsAtEmbeddedTransition(true, true))
						{
							this.HandleEmbeddedTransition(vbsymbol);
						}
						else
						{
							this.Accept(vbsymbol);
							if ((supportsExit && this.At(VBKeyword.Exit)) || (supportsContinue && this.At(VBKeyword.Continue)))
							{
								this.HandleExitOrContinue(start);
							}
							else if (this.At(start))
							{
								this.KeywordTerminatedStatement(start, terminator, supportsExit, supportsContinue)();
							}
							else
							{
								if (this.At(terminator))
								{
									this.AcceptUntil(VBSymbolType.NewLine);
									this.Optional(VBSymbolType.NewLine);
									this.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.AnyExceptNewline;
									return false;
								}
								if (!this.EndOfFile)
								{
									this.AcceptAndMoveNext();
								}
							}
						}
					}
					this.Context.OnError(currentLocation, RazorResources.ParseError_BlockNotTerminated, new object[]
					{
						start,
						terminator
					});
					result = false;
				}
				return result;
			};
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00010783 File Offset: 0x0000E983
		protected void HandleExitOrContinue(VBKeyword keyword)
		{
			base.AcceptAndMoveNext();
			base.AcceptWhile(VBSymbolType.WhiteSpace);
			this.Optional(keyword);
		}

		// Token: 0x040000F1 RID: 241
		internal static ISet<string> DefaultKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"functions",
			"code",
			"section",
			"do",
			"while",
			"if",
			"select",
			"for",
			"try",
			"with",
			"synclock",
			"using",
			"imports",
			"inherits",
			"option",
			"helper",
			"namespace",
			"class",
			"layout",
			"sessionstate"
		};

		// Token: 0x040000F2 RID: 242
		private Dictionary<VBKeyword, Func<bool>> _keywordHandlers = new Dictionary<VBKeyword, Func<bool>>();

		// Token: 0x040000F3 RID: 243
		private Dictionary<string, Func<bool>> _directiveHandlers = new Dictionary<string, Func<bool>>(StringComparer.OrdinalIgnoreCase);
	}
}

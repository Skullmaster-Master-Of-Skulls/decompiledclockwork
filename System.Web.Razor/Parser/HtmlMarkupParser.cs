using System;
using System.Collections.Generic;
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
	// Token: 0x02000041 RID: 65
	public class HtmlMarkupParser : TokenizerBackedParser<HtmlTokenizer, HtmlSymbol, HtmlSymbolType>
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x0000BD94 File Offset: 0x00009F94
		public override void ParseBlock()
		{
			if (this.Context == null)
			{
				throw new InvalidOperationException(RazorResources.Parser_Context_Not_Set);
			}
			using (base.PushSpanConfig(new Action<SpanBuilder>(this.DefaultMarkupSpan)))
			{
				using (this.Context.StartBlock(BlockType.Markup))
				{
					if (base.NextToken())
					{
						base.AcceptWhile(HtmlMarkupParser.IsSpacingToken(true));
						if (base.CurrentSymbol.Type == HtmlSymbolType.OpenAngle)
						{
							this.TagBlock(new Stack<Tuple<HtmlSymbol, SourceLocation>>());
						}
						else if (base.CurrentSymbol.Type == HtmlSymbolType.Transition)
						{
							base.Output(SpanKind.Markup);
							base.AcceptAndMoveNext();
							base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
							base.Span.CodeGenerator = SpanCodeGenerator.Null;
							base.Output(SpanKind.Transition);
							if (base.At(HtmlSymbolType.Transition))
							{
								base.Span.CodeGenerator = SpanCodeGenerator.Null;
								base.AcceptAndMoveNext();
								base.Output(SpanKind.MetaCode);
							}
							this.AfterTransition();
						}
						else
						{
							this.Context.OnError(base.CurrentSymbol.Start, RazorResources.ParseError_MarkupBlock_Must_Start_With_Tag);
						}
						base.Output(SpanKind.Markup);
					}
				}
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000BED8 File Offset: 0x0000A0D8
		private void DefaultMarkupSpan(SpanBuilder span)
		{
			span.CodeGenerator = new MarkupCodeGenerator();
			span.EditHandler = new SpanEditHandler(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString), AcceptedCharacters.Any);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000BF04 File Offset: 0x0000A104
		private void AfterTransition()
		{
			if (base.CurrentSymbol.Type == HtmlSymbolType.Text && base.CurrentSymbol.Content.Length > 0 && base.CurrentSymbol.Content[0] == ':')
			{
				Tuple<HtmlSymbol, HtmlSymbol> tuple = this.Language.SplitSymbol(base.CurrentSymbol, 1, HtmlSymbolType.Colon);
				base.Accept(tuple.Item1);
				base.Span.CodeGenerator = SpanCodeGenerator.Null;
				base.Output(SpanKind.MetaCode);
				if (tuple.Item2 != null)
				{
					base.Accept(tuple.Item2);
				}
				base.NextToken();
				this.SingleLineMarkup();
				return;
			}
			if (base.CurrentSymbol.Type == HtmlSymbolType.OpenAngle)
			{
				this.TagBlock(new Stack<Tuple<HtmlSymbol, SourceLocation>>());
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000BFC0 File Offset: 0x0000A1C0
		private void SingleLineMarkup()
		{
			bool whiteSpaceIsSignificantToAncestorBlock = this.Context.WhiteSpaceIsSignificantToAncestorBlock;
			this.Context.WhiteSpaceIsSignificantToAncestorBlock = true;
			base.Span.EditHandler = new SingleLineMarkupEditHandler(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString));
			this.SkipToAndParseCode(HtmlSymbolType.NewLine);
			if (!base.EndOfFile && base.CurrentSymbol.Type == HtmlSymbolType.NewLine)
			{
				base.AcceptAndMoveNext();
				base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			}
			base.PutCurrentBack();
			this.Context.WhiteSpaceIsSignificantToAncestorBlock = whiteSpaceIsSignificantToAncestorBlock;
			base.Output(SpanKind.Markup);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000C058 File Offset: 0x0000A258
		private void TagBlock(Stack<Tuple<HtmlSymbol, SourceLocation>> tags)
		{
			bool complete = false;
			do
			{
				this.SkipToAndParseCode(HtmlSymbolType.OpenAngle);
				if (base.EndOfFile)
				{
					this.EndTagBlock(tags, true);
				}
				else
				{
					this._bufferedOpenAngle = null;
					this._lastTagStart = base.CurrentLocation;
					this._bufferedOpenAngle = base.CurrentSymbol;
					SourceLocation currentLocation = base.CurrentLocation;
					if (!base.NextToken())
					{
						base.Accept(this._bufferedOpenAngle);
						this.EndTagBlock(tags, false);
					}
					else
					{
						complete = this.AfterTagStart(currentLocation, tags);
					}
				}
			}
			while (tags.Count > 0);
			this.EndTagBlock(tags, complete);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		private bool AfterTagStart(SourceLocation tagStart, Stack<Tuple<HtmlSymbol, SourceLocation>> tags)
		{
			if (base.EndOfFile)
			{
				if (tags.Count == 0)
				{
					this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_OuterTagMissingName);
				}
				return false;
			}
			switch (base.CurrentSymbol.Type)
			{
			case HtmlSymbolType.Bang:
				base.Accept(this._bufferedOpenAngle);
				return this.BangTag();
			case HtmlSymbolType.Solidus:
				return this.EndTag(tagStart, tags);
			case HtmlSymbolType.QuestionMark:
				base.Accept(this._bufferedOpenAngle);
				return this.XmlPI();
			default:
				return this.StartTag(tags);
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000C170 File Offset: 0x0000A370
		private bool XmlPI()
		{
			base.AcceptAndMoveNext();
			return this.AcceptUntilAll(new HtmlSymbolType[]
			{
				HtmlSymbolType.QuestionMark,
				HtmlSymbolType.CloseAngle
			});
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000C19C File Offset: 0x0000A39C
		private bool BangTag()
		{
			if (base.AcceptAndMoveNext())
			{
				if (base.CurrentSymbol.Type == HtmlSymbolType.DoubleHyphen)
				{
					base.AcceptAndMoveNext();
					return this.AcceptUntilAll(new HtmlSymbolType[]
					{
						HtmlSymbolType.DoubleHyphen,
						HtmlSymbolType.CloseAngle
					});
				}
				if (base.CurrentSymbol.Type != HtmlSymbolType.LeftBracket)
				{
					base.AcceptAndMoveNext();
					return this.AcceptUntilAll(new HtmlSymbolType[]
					{
						HtmlSymbolType.CloseAngle
					});
				}
				if (base.AcceptAndMoveNext())
				{
					return this.CData();
				}
			}
			return false;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000C218 File Offset: 0x0000A418
		private bool CData()
		{
			return base.CurrentSymbol.Type == HtmlSymbolType.Text && string.Equals(base.CurrentSymbol.Content, "cdata", StringComparison.OrdinalIgnoreCase) && base.AcceptAndMoveNext() && base.CurrentSymbol.Type == HtmlSymbolType.LeftBracket && this.AcceptUntilAll(new HtmlSymbolType[]
			{
				HtmlSymbolType.RightBracket,
				HtmlSymbolType.RightBracket,
				HtmlSymbolType.CloseAngle
			});
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000C284 File Offset: 0x0000A484
		private bool EndTag(SourceLocation tagStart, Stack<Tuple<HtmlSymbol, SourceLocation>> tags)
		{
			HtmlSymbol currentSymbol = base.CurrentSymbol;
			if (!base.NextToken())
			{
				base.Accept(this._bufferedOpenAngle);
				base.Accept(currentSymbol);
				return false;
			}
			string text = string.Empty;
			if (base.At(HtmlSymbolType.Text))
			{
				text = base.CurrentSymbol.Content;
			}
			bool flag = this.RemoveTag(tags, text, tagStart);
			if (tags.Count == 0 && string.Equals(text, SyntaxConstants.TextTagName, StringComparison.OrdinalIgnoreCase) && flag)
			{
				base.Output(SpanKind.Markup);
				return this.EndTextTag(currentSymbol);
			}
			base.Accept(this._bufferedOpenAngle);
			base.Accept(currentSymbol);
			base.AcceptUntil(HtmlSymbolType.CloseAngle);
			return base.Optional(HtmlSymbolType.CloseAngle);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000C328 File Offset: 0x0000A528
		private bool EndTextTag(HtmlSymbol solidus)
		{
			SourceLocation start = this._bufferedOpenAngle.Start;
			base.Accept(this._bufferedOpenAngle);
			base.Accept(solidus);
			base.AcceptAndMoveNext();
			bool flag = base.Optional(HtmlSymbolType.CloseAngle);
			if (!flag)
			{
				this.Context.OnError(start, RazorResources.ParseError_TextTagCannotContainAttributes);
			}
			else
			{
				base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			}
			base.Span.CodeGenerator = SpanCodeGenerator.Null;
			base.Output(SpanKind.Transition);
			return flag;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000C3A3 File Offset: 0x0000A5A3
		private bool IsTagRecoveryStopPoint(HtmlSymbol sym)
		{
			return sym.Type == HtmlSymbolType.CloseAngle || sym.Type == HtmlSymbolType.Solidus || sym.Type == HtmlSymbolType.OpenAngle || sym.Type == HtmlSymbolType.SingleQuote || sym.Type == HtmlSymbolType.DoubleQuote;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000C3D7 File Offset: 0x0000A5D7
		private void TagContent()
		{
			if (!base.At(HtmlSymbolType.WhiteSpace))
			{
				this.RecoverToEndOfTag();
				return;
			}
			while (!base.EndOfFile && !this.IsEndOfTag())
			{
				this.BeforeAttribute();
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000C3FF File Offset: 0x0000A5FF
		private bool IsEndOfTag()
		{
			if (base.At(HtmlSymbolType.Solidus))
			{
				if (base.NextIs(HtmlSymbolType.CloseAngle))
				{
					return true;
				}
				base.AcceptAndMoveNext();
			}
			return base.At(HtmlSymbolType.CloseAngle) || base.At(HtmlSymbolType.OpenAngle);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000C49C File Offset: 0x0000A69C
		private void BeforeAttribute()
		{
			IEnumerable<HtmlSymbol> enumerable = base.ReadWhile((HtmlSymbol sym) => sym.Type == HtmlSymbolType.WhiteSpace || sym.Type == HtmlSymbolType.NewLine);
			if (base.At(HtmlSymbolType.Transition))
			{
				base.Accept(enumerable);
				this.RecoverToEndOfTag();
				return;
			}
			IEnumerable<HtmlSymbol> enumerable2 = Enumerable.Empty<HtmlSymbol>();
			if (!base.At(HtmlSymbolType.Text))
			{
				base.Accept(enumerable);
				this.RecoverToEndOfTag();
				return;
			}
			enumerable2 = base.ReadWhile((HtmlSymbol sym) => sym.Type != HtmlSymbolType.WhiteSpace && sym.Type != HtmlSymbolType.NewLine && sym.Type != HtmlSymbolType.Equals && sym.Type != HtmlSymbolType.CloseAngle && sym.Type != HtmlSymbolType.OpenAngle && (sym.Type != HtmlSymbolType.Solidus || !base.NextIs(HtmlSymbolType.CloseAngle)));
			if (!base.At(HtmlSymbolType.Equals))
			{
				base.Accept(enumerable);
				base.Accept(enumerable2);
				return;
			}
			base.Output(SpanKind.Markup);
			using (this.Context.StartBlock(BlockType.Markup))
			{
				this.AttributePrefix(enumerable, enumerable2);
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000C58C File Offset: 0x0000A78C
		private void AttributePrefix(IEnumerable<HtmlSymbol> whitespace, IEnumerable<HtmlSymbol> nameSymbols)
		{
			LocationTagged<string> content = nameSymbols.GetContent(base.Span.Start);
			bool flag = !content.Value.StartsWith("data-", StringComparison.OrdinalIgnoreCase);
			base.Accept(whitespace);
			base.Accept(nameSymbols);
			base.AcceptAndMoveNext();
			HtmlSymbolType quote = HtmlSymbolType.Unknown;
			if (base.At(HtmlSymbolType.SingleQuote) || base.At(HtmlSymbolType.DoubleQuote))
			{
				quote = base.CurrentSymbol.Type;
				base.AcceptAndMoveNext();
			}
			LocationTagged<string> content2 = base.Span.GetContent();
			if (flag)
			{
				base.Span.CodeGenerator = SpanCodeGenerator.Null;
				base.Output(SpanKind.Markup);
				while (!base.EndOfFile && !this.IsEndOfAttributeValue(quote, base.CurrentSymbol))
				{
					this.AttributeValue(quote);
				}
				LocationTagged<string> suffix = new LocationTagged<string>(string.Empty, base.CurrentLocation);
				if (quote != HtmlSymbolType.Unknown && base.At(quote))
				{
					suffix = base.CurrentSymbol.GetContent();
					base.AcceptAndMoveNext();
				}
				if (base.Span.Symbols.Count > 0)
				{
					base.Span.CodeGenerator = SpanCodeGenerator.Null;
					base.Output(SpanKind.Markup);
				}
				this.Context.CurrentBlock.CodeGenerator = new AttributeBlockCodeGenerator(content, content2, suffix);
				return;
			}
			this.SkipToAndParseCode((HtmlSymbol sym) => this.IsEndOfAttributeValue(quote, sym));
			if (quote != HtmlSymbolType.Unknown)
			{
				base.Optional(quote);
			}
			base.Output(SpanKind.Markup);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000C784 File Offset: 0x0000A984
		private void AttributeValue(HtmlSymbolType quote)
		{
			SourceLocation currentLocation = base.CurrentLocation;
			IEnumerable<HtmlSymbol> symbols = base.ReadWhile((HtmlSymbol sym) => sym.Type == HtmlSymbolType.WhiteSpace || sym.Type == HtmlSymbolType.NewLine);
			base.Accept(symbols);
			if (base.At(HtmlSymbolType.Transition))
			{
				SourceLocation currentLocation2 = base.CurrentLocation;
				base.PutCurrentBack();
				base.Span.CodeGenerator = SpanCodeGenerator.Null;
				using (this.Context.StartBlock(BlockType.Markup))
				{
					this.Context.CurrentBlock.CodeGenerator = new DynamicAttributeBlockCodeGenerator(symbols.GetContent(currentLocation), currentLocation2);
					this.OtherParserBlock();
					goto IL_164;
				}
			}
			if (base.At(HtmlSymbolType.Text) && base.CurrentSymbol.Content.Length > 0 && base.CurrentSymbol.Content[0] == '~' && base.NextIs(HtmlSymbolType.Solidus))
			{
				SourceLocation currentLocation3 = base.CurrentLocation;
				this.VirtualPath();
				base.Span.CodeGenerator = new LiteralAttributeCodeGenerator(symbols.GetContent(currentLocation), new LocationTagged<SpanCodeGenerator>(new ResolveUrlCodeGenerator(), currentLocation3));
			}
			else
			{
				IEnumerable<HtmlSymbol> symbols2 = base.ReadWhile((HtmlSymbol sym) => sym.Type != HtmlSymbolType.WhiteSpace && sym.Type != HtmlSymbolType.NewLine && sym.Type != HtmlSymbolType.Transition && !this.IsEndOfAttributeValue(quote, sym));
				base.Accept(symbols2);
				base.Span.CodeGenerator = new LiteralAttributeCodeGenerator(symbols.GetContent(currentLocation), symbols2.GetContent(currentLocation));
			}
			IL_164:
			base.Output(SpanKind.Markup);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000C90C File Offset: 0x0000AB0C
		private bool IsEndOfAttributeValue(HtmlSymbolType quote, HtmlSymbol sym)
		{
			if (base.EndOfFile || sym == null)
			{
				return true;
			}
			if (quote == HtmlSymbolType.Unknown)
			{
				return this.IsUnquotedEndOfAttributeValue(sym);
			}
			return sym.Type == quote;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000C930 File Offset: 0x0000AB30
		private bool IsUnquotedEndOfAttributeValue(HtmlSymbol sym)
		{
			return sym.Type == HtmlSymbolType.DoubleQuote || sym.Type == HtmlSymbolType.SingleQuote || sym.Type == HtmlSymbolType.OpenAngle || sym.Type == HtmlSymbolType.Equals || (sym.Type == HtmlSymbolType.Solidus && base.NextIs(HtmlSymbolType.CloseAngle)) || sym.Type == HtmlSymbolType.CloseAngle || sym.Type == HtmlSymbolType.WhiteSpace || sym.Type == HtmlSymbolType.NewLine;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000C998 File Offset: 0x0000AB98
		private void VirtualPath()
		{
			base.AcceptUntil(new HtmlSymbolType[]
			{
				HtmlSymbolType.Transition,
				HtmlSymbolType.WhiteSpace,
				HtmlSymbolType.NewLine,
				HtmlSymbolType.SingleQuote,
				HtmlSymbolType.DoubleQuote
			});
			base.Span.EditHandler.EditorHints = EditorHints.VirtualPath;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000C9DC File Offset: 0x0000ABDC
		private void RecoverToEndOfTag()
		{
			while (!base.EndOfFile)
			{
				this.SkipToAndParseCode(new Func<HtmlSymbol, bool>(this.IsTagRecoveryStopPoint));
				if (!base.EndOfFile)
				{
					base.EnsureCurrent();
					HtmlSymbolType type = base.CurrentSymbol.Type;
					switch (type)
					{
					case HtmlSymbolType.OpenAngle:
					case HtmlSymbolType.Solidus:
						break;
					case HtmlSymbolType.Bang:
						goto IL_6B;
					default:
						switch (type)
						{
						case HtmlSymbolType.CloseAngle:
							break;
						case HtmlSymbolType.RightBracket:
						case HtmlSymbolType.Equals:
							goto IL_6B;
						case HtmlSymbolType.DoubleQuote:
						case HtmlSymbolType.SingleQuote:
							this.ParseQuoted();
							continue;
						default:
							goto IL_6B;
						}
						break;
					}
					return;
					IL_6B:
					base.AcceptAndMoveNext();
				}
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000CA64 File Offset: 0x0000AC64
		private void ParseQuoted()
		{
			HtmlSymbolType type = base.CurrentSymbol.Type;
			base.AcceptAndMoveNext();
			this.ParseQuoted(type);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000CA8B File Offset: 0x0000AC8B
		private void ParseQuoted(HtmlSymbolType type)
		{
			this.SkipToAndParseCode(type);
			if (!base.EndOfFile)
			{
				base.AcceptAndMoveNext();
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000CAA4 File Offset: 0x0000ACA4
		private bool StartTag(Stack<Tuple<HtmlSymbol, SourceLocation>> tags)
		{
			HtmlSymbol item;
			if (base.At(HtmlSymbolType.Text))
			{
				item = base.CurrentSymbol;
			}
			else
			{
				item = new HtmlSymbol(base.CurrentLocation, string.Empty, HtmlSymbolType.Unknown);
			}
			Tuple<HtmlSymbol, SourceLocation> tuple = Tuple.Create<HtmlSymbol, SourceLocation>(item, this._lastTagStart);
			if (tags.Count == 0 && string.Equals(tuple.Item1.Content, SyntaxConstants.TextTagName, StringComparison.OrdinalIgnoreCase))
			{
				base.Output(SpanKind.Markup);
				base.Span.CodeGenerator = SpanCodeGenerator.Null;
				base.Accept(this._bufferedOpenAngle);
				base.AcceptAndMoveNext();
				int absoluteIndex = base.CurrentLocation.AbsoluteIndex;
				IEnumerable<HtmlSymbol> symbols = base.ReadWhile(HtmlMarkupParser.IsSpacingToken(true));
				bool flag = base.At(HtmlSymbolType.Solidus);
				if (flag)
				{
					base.Accept(symbols);
					base.AcceptAndMoveNext();
					absoluteIndex = base.CurrentLocation.AbsoluteIndex;
					symbols = base.ReadWhile(HtmlMarkupParser.IsSpacingToken(true));
				}
				if (!base.Optional(HtmlSymbolType.CloseAngle))
				{
					this.Context.Source.Position = absoluteIndex;
					base.NextToken();
					this.Context.OnError(tuple.Item2, RazorResources.ParseError_TextTagCannotContainAttributes);
				}
				else
				{
					base.Accept(symbols);
					base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
				}
				if (!flag)
				{
					tags.Push(tuple);
				}
				base.Output(SpanKind.Transition);
				return true;
			}
			base.Accept(this._bufferedOpenAngle);
			base.Optional(HtmlSymbolType.Text);
			return this.RestOfTag(tuple, tags);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000CC0C File Offset: 0x0000AE0C
		private bool RestOfTag(Tuple<HtmlSymbol, SourceLocation> tag, Stack<Tuple<HtmlSymbol, SourceLocation>> tags)
		{
			this.TagContent();
			if (base.At(HtmlSymbolType.OpenAngle))
			{
				return false;
			}
			bool flag = base.At(HtmlSymbolType.Solidus);
			if (flag)
			{
				base.AcceptAndMoveNext();
			}
			bool flag2 = base.Optional(HtmlSymbolType.CloseAngle);
			if (!flag2)
			{
				this.Context.OnError(tag.Item2, RazorResources.ParseError_UnfinishedTag, new object[]
				{
					tag.Item1.Content
				});
			}
			else if (!flag)
			{
				string text = tag.Item1.Content.Trim();
				if (this.VoidElements.Contains(text))
				{
					int absoluteIndex = base.CurrentLocation.AbsoluteIndex;
					IEnumerable<HtmlSymbol> symbols = base.ReadWhile(HtmlMarkupParser.IsSpacingToken(true));
					if (base.At(HtmlSymbolType.OpenAngle) && base.NextIs(HtmlSymbolType.Solidus))
					{
						HtmlSymbol currentSymbol = base.CurrentSymbol;
						base.NextToken();
						HtmlSymbol currentSymbol2 = base.CurrentSymbol;
						base.NextToken();
						if (base.At(HtmlSymbolType.Text) && string.Equals(base.CurrentSymbol.Content, text, StringComparison.OrdinalIgnoreCase))
						{
							base.Accept(symbols);
							base.Accept(currentSymbol);
							base.Accept(currentSymbol2);
							base.AcceptAndMoveNext();
							base.AcceptUntil(HtmlSymbolType.CloseAngle, HtmlSymbolType.OpenAngle);
							return base.Optional(HtmlSymbolType.CloseAngle);
						}
					}
					this.Context.Source.Position = absoluteIndex;
					base.NextToken();
				}
				else if (string.Equals(text, "script", StringComparison.OrdinalIgnoreCase))
				{
					this.SkipToEndScriptAndParseCode();
				}
				else
				{
					tags.Push(tag);
				}
			}
			return flag2;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000CD78 File Offset: 0x0000AF78
		private void SkipToEndScriptAndParseCode()
		{
			bool flag = false;
			while (!flag && !base.EndOfFile)
			{
				this.SkipToAndParseCode(HtmlSymbolType.OpenAngle);
				SourceLocation currentLocation = base.CurrentLocation;
				base.AcceptAndMoveNext();
				base.AcceptWhile(HtmlSymbolType.WhiteSpace);
				if (base.Optional(HtmlSymbolType.Solidus))
				{
					base.AcceptWhile(HtmlSymbolType.WhiteSpace);
					if (base.At(HtmlSymbolType.Text) && string.Equals(base.CurrentSymbol.Content, "script", StringComparison.OrdinalIgnoreCase))
					{
						this.SkipToAndParseCode(HtmlSymbolType.CloseAngle);
						if (!base.Optional(HtmlSymbolType.CloseAngle))
						{
							this.Context.OnError(currentLocation, RazorResources.ParseError_UnfinishedTag, new object[]
							{
								"script"
							});
						}
						flag = true;
					}
				}
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000CE1C File Offset: 0x0000B01C
		private bool AcceptUntilAll(params HtmlSymbolType[] endSequence)
		{
			while (!base.EndOfFile)
			{
				this.SkipToAndParseCode(endSequence[0]);
				if (base.AcceptAll(endSequence))
				{
					return true;
				}
			}
			base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.Any;
			return false;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000CE50 File Offset: 0x0000B050
		private bool RemoveTag(Stack<Tuple<HtmlSymbol, SourceLocation>> tags, string tagName, SourceLocation tagStart)
		{
			Tuple<HtmlSymbol, SourceLocation> tuple = null;
			while (tags.Count > 0)
			{
				tuple = tags.Pop();
				if (string.Equals(tagName, tuple.Item1.Content, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			if (tuple != null)
			{
				this.Context.OnError(tuple.Item2, RazorResources.ParseError_MissingEndTag, new object[]
				{
					tuple.Item1.Content
				});
			}
			else
			{
				this.Context.OnError(tagStart, RazorResources.ParseError_UnexpectedEndTag, new object[]
				{
					tagName
				});
			}
			return false;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000CED8 File Offset: 0x0000B0D8
		private void EndTagBlock(Stack<Tuple<HtmlSymbol, SourceLocation>> tags, bool complete)
		{
			if (tags.Count > 0)
			{
				while (tags.Count > 1)
				{
					tags.Pop();
				}
				Tuple<HtmlSymbol, SourceLocation> tuple = tags.Pop();
				this.Context.OnError(tuple.Item2, RazorResources.ParseError_MissingEndTag, new object[]
				{
					tuple.Item1.Content
				});
			}
			else if (complete)
			{
				base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			}
			tags.Clear();
			if (!this.Context.DesignTimeMode)
			{
				base.AcceptWhile(HtmlSymbolType.WhiteSpace);
				if (!base.EndOfFile && base.CurrentSymbol.Type == HtmlSymbolType.NewLine)
				{
					base.AcceptAndMoveNext();
				}
			}
			else if (base.Span.EditHandler.AcceptedCharacters == AcceptedCharacters.Any)
			{
				base.AcceptWhile(HtmlSymbolType.WhiteSpace);
				base.Optional(HtmlSymbolType.NewLine);
			}
			base.PutCurrentBack();
			if (!complete)
			{
				base.AddMarkerSymbolIfNecessary();
			}
			base.Output(SpanKind.Markup);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000CFBC File Offset: 0x0000B1BC
		public override void ParseDocument()
		{
			if (this.Context == null)
			{
				throw new InvalidOperationException(RazorResources.Parser_Context_Not_Set);
			}
			using (base.PushSpanConfig(new Action<SpanBuilder>(this.DefaultMarkupSpan)))
			{
				using (this.Context.StartBlock(BlockType.Markup))
				{
					base.NextToken();
					while (!base.EndOfFile)
					{
						this.SkipToAndParseCode(HtmlSymbolType.OpenAngle);
						this.ScanTagInDocumentContext();
					}
					base.AddMarkerSymbolIfNecessary();
					base.Output(SpanKind.Markup);
				}
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000D05C File Offset: 0x0000B25C
		private bool ScanTagInDocumentContext()
		{
			if (base.Optional(HtmlSymbolType.OpenAngle))
			{
				if (base.At(HtmlSymbolType.Bang))
				{
					this.BangTag();
					return true;
				}
				if (base.At(HtmlSymbolType.QuestionMark))
				{
					this.XmlPI();
					return true;
				}
				if (!base.At(HtmlSymbolType.Solidus))
				{
					bool flag = base.At(HtmlSymbolType.Text) && string.Equals(base.CurrentSymbol.Content, "script", StringComparison.OrdinalIgnoreCase);
					base.Optional(HtmlSymbolType.Text);
					this.TagContent();
					base.Optional(HtmlSymbolType.Solidus);
					base.Optional(HtmlSymbolType.CloseAngle);
					if (flag)
					{
						this.SkipToEndScriptAndParseCode();
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000D0ED File Offset: 0x0000B2ED
		public ISet<string> VoidElements
		{
			get
			{
				return this._voidElements;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000D0F5 File Offset: 0x0000B2F5
		protected override ParserBase OtherParser
		{
			get
			{
				return this.Context.CodeParser;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000D102 File Offset: 0x0000B302
		protected override LanguageCharacteristics<HtmlTokenizer, HtmlSymbol, HtmlSymbolType> Language
		{
			get
			{
				return HtmlLanguageCharacteristics.Instance;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000D109 File Offset: 0x0000B309
		public override void BuildSpan(SpanBuilder span, SourceLocation start, string content)
		{
			span.Kind = SpanKind.Markup;
			span.CodeGenerator = new MarkupCodeGenerator();
			base.BuildSpan(span, start, content);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000D126 File Offset: 0x0000B326
		protected override void OutputSpanBeforeRazorComment()
		{
			base.Output(SpanKind.Markup);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000D148 File Offset: 0x0000B348
		protected void SkipToAndParseCode(HtmlSymbolType type)
		{
			this.SkipToAndParseCode((HtmlSymbol sym) => sym.Type == type);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000D174 File Offset: 0x0000B374
		protected void SkipToAndParseCode(Func<HtmlSymbol, bool> condition)
		{
			HtmlSymbol htmlSymbol = null;
			bool flag = false;
			while (!base.EndOfFile && !condition(base.CurrentSymbol))
			{
				if (base.At(HtmlSymbolType.NewLine))
				{
					if (htmlSymbol != null)
					{
						base.Accept(htmlSymbol);
					}
					flag = true;
					htmlSymbol = null;
					base.AcceptAndMoveNext();
				}
				else if (base.At(HtmlSymbolType.Transition))
				{
					HtmlSymbol currentSymbol = base.CurrentSymbol;
					base.NextToken();
					if (base.At(HtmlSymbolType.Transition))
					{
						if (htmlSymbol != null)
						{
							base.Accept(htmlSymbol);
							htmlSymbol = null;
						}
						base.Output(SpanKind.Markup);
						base.Accept(currentSymbol);
						base.Span.CodeGenerator = SpanCodeGenerator.Null;
						base.Output(SpanKind.Markup);
						base.AcceptAndMoveNext();
					}
					else
					{
						if (!base.EndOfFile)
						{
							base.PutCurrentBack();
						}
						base.PutBack(currentSymbol);
						if (htmlSymbol != null)
						{
							if (!this.Context.DesignTimeMode && htmlSymbol.Type == HtmlSymbolType.WhiteSpace && flag)
							{
								flag = false;
								base.PutBack(htmlSymbol);
								htmlSymbol = null;
							}
							else
							{
								base.Accept(htmlSymbol);
								htmlSymbol = null;
							}
						}
						this.OtherParserBlock();
					}
				}
				else if (base.At(HtmlSymbolType.RazorCommentTransition))
				{
					if (htmlSymbol != null)
					{
						base.Accept(htmlSymbol);
						htmlSymbol = null;
					}
					base.AddMarkerSymbolIfNecessary();
					base.Output(SpanKind.Markup);
					base.RazorComment();
				}
				else
				{
					flag &= base.At(HtmlSymbolType.WhiteSpace);
					if (htmlSymbol != null)
					{
						base.Accept(htmlSymbol);
					}
					htmlSymbol = base.CurrentSymbol;
					base.NextToken();
				}
			}
			if (htmlSymbol != null)
			{
				base.Accept(htmlSymbol);
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
		protected static Func<HtmlSymbol, bool> IsSpacingToken(bool includeNewLines)
		{
			return (HtmlSymbol sym) => sym.Type == HtmlSymbolType.WhiteSpace || (includeNewLines && sym.Type == HtmlSymbolType.NewLine);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000D320 File Offset: 0x0000B520
		private void OtherParserBlock()
		{
			base.AddMarkerSymbolIfNecessary();
			base.Output(SpanKind.Markup);
			using (base.PushSpanConfig())
			{
				this.Context.SwitchActiveParser();
				this.Context.CodeParser.ParseBlock();
				this.Context.SwitchActiveParser();
			}
			base.Initialize(base.Span);
			base.NextToken();
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000D398 File Offset: 0x0000B598
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		private bool CaseSensitive { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000D3A9 File Offset: 0x0000B5A9
		private StringComparison Comparison
		{
			get
			{
				if (!this.CaseSensitive)
				{
					return StringComparison.OrdinalIgnoreCase;
				}
				return StringComparison.Ordinal;
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		public override void ParseSection(Tuple<string, string> nestingSequences, bool caseSensitive)
		{
			if (this.Context == null)
			{
				throw new InvalidOperationException(RazorResources.Parser_Context_Not_Set);
			}
			using (base.PushSpanConfig(new Action<SpanBuilder>(this.DefaultMarkupSpan)))
			{
				using (this.Context.StartBlock(BlockType.Markup))
				{
					base.NextToken();
					this.CaseSensitive = caseSensitive;
					if (nestingSequences.Item1 == null)
					{
						this.NonNestingSection(nestingSequences.Item2.Split(new char[0]));
					}
					else
					{
						this.NestingSection(nestingSequences);
					}
					base.AddMarkerSymbolIfNecessary();
					base.Output(SpanKind.Markup);
				}
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000D498 File Offset: 0x0000B698
		private void NonNestingSection(string[] nestingSequenceComponents)
		{
			do
			{
				this.SkipToAndParseCode((HtmlSymbol sym) => sym.Type == HtmlSymbolType.OpenAngle || this.AtEnd(nestingSequenceComponents));
				this.ScanTagInDocumentContext();
			}
			while ((base.EndOfFile || !this.AtEnd(nestingSequenceComponents)) && !base.EndOfFile);
			base.PutCurrentBack();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000D514 File Offset: 0x0000B714
		private void NestingSection(Tuple<string, string> nestingSequences)
		{
			int num = 1;
			while (num > 0 && !base.EndOfFile)
			{
				this.SkipToAndParseCode((HtmlSymbol sym) => sym.Type == HtmlSymbolType.Text || sym.Type == HtmlSymbolType.OpenAngle);
				if (base.At(HtmlSymbolType.Text))
				{
					num += this.ProcessTextToken(nestingSequences, num);
					if (base.CurrentSymbol != null)
					{
						base.AcceptAndMoveNext();
					}
					else if (num > 0)
					{
						base.NextToken();
					}
				}
				else
				{
					this.ScanTagInDocumentContext();
				}
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000D590 File Offset: 0x0000B790
		private bool AtEnd(string[] nestingSequenceComponents)
		{
			base.EnsureCurrent();
			if (string.Equals(base.CurrentSymbol.Content, nestingSequenceComponents[0], this.Comparison))
			{
				int absoluteIndex = base.CurrentSymbol.Start.AbsoluteIndex;
				try
				{
					foreach (string b in nestingSequenceComponents)
					{
						if (!base.EndOfFile && !string.Equals(base.CurrentSymbol.Content, b, this.Comparison))
						{
							return false;
						}
						base.NextToken();
						while (!base.EndOfFile && HtmlMarkupParser.IsSpacingToken(true)(base.CurrentSymbol))
						{
							base.NextToken();
						}
					}
					return true;
				}
				finally
				{
					this.Context.Source.Position = absoluteIndex;
					base.NextToken();
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000D674 File Offset: 0x0000B874
		private int ProcessTextToken(Tuple<string, string> nestingSequences, int currentNesting)
		{
			for (int i = 0; i < base.CurrentSymbol.Content.Length; i++)
			{
				int num = this.HandleNestingSequence(nestingSequences.Item1, i, currentNesting, 1);
				if (num == 0)
				{
					num = this.HandleNestingSequence(nestingSequences.Item2, i, currentNesting, -1);
				}
				if (num != 0)
				{
					return num;
				}
			}
			return 0;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000D6C8 File Offset: 0x0000B8C8
		private int HandleNestingSequence(string sequence, int position, int currentNesting, int retIfMatched)
		{
			if (sequence != null && base.CurrentSymbol.Content[position] == sequence[0] && position + sequence.Length <= base.CurrentSymbol.Content.Length)
			{
				string a = base.CurrentSymbol.Content.Substring(position, sequence.Length);
				if (string.Equals(a, sequence, this.Comparison))
				{
					int position2 = this.Context.Source.Position;
					HtmlSymbol currentSymbol = base.CurrentSymbol;
					base.PutCurrentBack();
					Tuple<HtmlSymbol, HtmlSymbol> tuple = this.Language.SplitSymbol(currentSymbol, position, HtmlSymbolType.Text);
					HtmlSymbol item = tuple.Item1;
					tuple = this.Language.SplitSymbol(tuple.Item2, sequence.Length, HtmlSymbolType.Text);
					HtmlSymbol item2 = tuple.Item1;
					HtmlSymbol item3 = tuple.Item2;
					if (!string.IsNullOrEmpty(item.Content))
					{
						base.Accept(item);
					}
					if (currentNesting + retIfMatched == 0)
					{
						this.Context.Source.Position = item2.Start.AbsoluteIndex;
					}
					else
					{
						base.Accept(item2);
						if (item3 != null)
						{
							this.Context.Source.Position = item3.Start.AbsoluteIndex;
						}
						else
						{
							this.Context.Source.Position = position2;
						}
					}
					return retIfMatched;
				}
			}
			return 0;
		}

		// Token: 0x040000B5 RID: 181
		private SourceLocation _lastTagStart = SourceLocation.Zero;

		// Token: 0x040000B6 RID: 182
		private HtmlSymbol _bufferedOpenAngle;

		// Token: 0x040000B7 RID: 183
		private ISet<string> _voidElements = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"area",
			"base",
			"br",
			"col",
			"command",
			"embed",
			"hr",
			"img",
			"input",
			"keygen",
			"link",
			"meta",
			"param",
			"source",
			"track",
			"wbr"
		};
	}
}

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
	// Token: 0x0200003C RID: 60
	public class CSharpCodeParser : TokenizerBackedParser<CSharpTokenizer, CSharpSymbol, CSharpSymbolType>
	{
		// Token: 0x0600026F RID: 623 RVA: 0x00008CB0 File Offset: 0x00006EB0
		private void SetupDirectives()
		{
			this.MapDirectives(new Action(this.InheritsDirective), new string[]
			{
				SyntaxConstants.CSharp.InheritsKeyword
			});
			this.MapDirectives(new Action(this.FunctionsDirective), new string[]
			{
				SyntaxConstants.CSharp.FunctionsKeyword
			});
			this.MapDirectives(new Action(this.SectionDirective), new string[]
			{
				SyntaxConstants.CSharp.SectionKeyword
			});
			this.MapDirectives(new Action(this.HelperDirective), new string[]
			{
				SyntaxConstants.CSharp.HelperKeyword
			});
			this.MapDirectives(new Action(this.LayoutDirective), new string[]
			{
				SyntaxConstants.CSharp.LayoutKeyword
			});
			this.MapDirectives(new Action(this.SessionStateDirective), new string[]
			{
				SyntaxConstants.CSharp.SessionStateKeyword
			});
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008D98 File Offset: 0x00006F98
		protected virtual void LayoutDirective()
		{
			base.AcceptAndMoveNext();
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Directive);
			bool flag = base.At(CSharpSymbolType.WhiteSpace);
			base.AcceptWhile(CSharpSymbolType.WhiteSpace);
			base.Output(SpanKind.MetaCode, flag ? AcceptedCharacters.None : AcceptedCharacters.Any);
			base.AcceptUntil(CSharpSymbolType.NewLine);
			base.Span.CodeGenerator = new SetLayoutCodeGenerator(base.Span.GetContent());
			base.Span.EditHandler.EditorHints = (EditorHints.VirtualPath | EditorHints.LayoutPage);
			bool flag2 = base.Optional(CSharpSymbolType.NewLine);
			base.AddMarkerSymbolIfNecessary();
			base.Output(SpanKind.MetaCode, flag2 ? AcceptedCharacters.None : AcceptedCharacters.Any);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00008E33 File Offset: 0x00007033
		protected virtual void SessionStateDirective()
		{
			base.AcceptAndMoveNext();
			this.SessionStateDirectiveCore();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008E4B File Offset: 0x0000704B
		protected void SessionStateDirectiveCore()
		{
			this.SessionStateTypeDirective(RazorResources.ParserEror_SessionDirectiveMissingValue, (string key, string value) => new RazorDirectiveAttributeCodeGenerator(key, value));
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008E80 File Offset: 0x00007080
		protected void SessionStateTypeDirective(string noValueError, Func<string, string, SpanCodeGenerator> createCodeGenerator)
		{
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Directive);
			CSharpSymbol csharpSymbol = base.AcceptSingleWhiteSpaceCharacter();
			if (base.Span.Symbols.Count > 1)
			{
				base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			}
			base.Output(SpanKind.MetaCode);
			if (csharpSymbol != null)
			{
				base.Accept(csharpSymbol);
			}
			base.AcceptWhile(CSharpCodeParser.IsSpacingToken(false, true));
			if (!this.ValidSessionStateValue())
			{
				this.Context.OnError(base.CurrentLocation, noValueError);
			}
			string arg = string.Concat(from CSharpSymbol sym in base.Span.Symbols
			select sym.Content).Trim();
			base.Span.CodeGenerator = createCodeGenerator(SyntaxConstants.CSharp.SessionStateKeyword, arg);
			this.CompleteBlock();
			base.Output(SpanKind.Code);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00008F67 File Offset: 0x00007167
		protected virtual bool ValidSessionStateValue()
		{
			return base.Optional(CSharpSymbolType.Identifier);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00008F70 File Offset: 0x00007170
		protected virtual void HelperDirective()
		{
			bool flag = this.Context.IsWithin(BlockType.Helper);
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Helper);
			CSharpCodeParser.Block block = new CSharpCodeParser.Block(base.CurrentSymbol.Content.ToString().ToLowerInvariant(), base.CurrentLocation);
			base.AcceptAndMoveNext();
			if (flag)
			{
				this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_Helpers_Cannot_Be_Nested);
			}
			if (!base.At(CSharpSymbolType.WhiteSpace))
			{
				string text;
				if (base.At(CSharpSymbolType.NewLine))
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
				base.Output(SpanKind.MetaCode);
				return;
			}
			CSharpSymbol csharpSymbol = base.AcceptSingleWhiteSpaceCharacter();
			base.Output(SpanKind.MetaCode);
			if (csharpSymbol != null)
			{
				base.Accept(csharpSymbol);
			}
			base.AcceptWhile(CSharpCodeParser.IsSpacingToken(false, true));
			bool flag2 = !base.Required(CSharpSymbolType.Identifier, true, RazorResources.ParseError_Unexpected_Character_At_Helper_Name_Start);
			if (!flag2)
			{
				base.AcceptAndMoveNext();
			}
			base.AcceptWhile(CSharpCodeParser.IsSpacingToken(false, true));
			SourceLocation currentLocation = base.CurrentLocation;
			if (!base.Optional(CSharpSymbolType.LeftParenthesis))
			{
				if (!flag2)
				{
					flag2 = true;
					this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_MissingCharAfterHelperName, new object[]
					{
						"("
					});
				}
			}
			else
			{
				SourceLocation currentLocation2 = base.CurrentLocation;
				if (!base.Balance(BalancingModes.NoErrorOnFailure, CSharpSymbolType.LeftParenthesis, CSharpSymbolType.RightParenthesis, currentLocation2))
				{
					flag2 = true;
					this.Context.OnError(currentLocation, RazorResources.ParseError_UnterminatedHelperParameterList);
				}
				base.Optional(CSharpSymbolType.RightParenthesis);
			}
			int absoluteIndex = base.CurrentLocation.AbsoluteIndex;
			IEnumerable<CSharpSymbol> symbols = base.ReadWhile(CSharpCodeParser.IsSpacingToken(true, true));
			SourceLocation currentLocation3 = base.CurrentLocation;
			bool flag3 = base.At(CSharpSymbolType.LeftBrace);
			if (flag3)
			{
				base.Accept(symbols);
				base.AcceptAndMoveNext();
			}
			else
			{
				this.Context.Source.Position = absoluteIndex;
				base.NextToken();
				base.AcceptWhile(CSharpCodeParser.IsSpacingToken(false, true));
				if (!flag2)
				{
					this.Context.OnError(currentLocation3, RazorResources.ParseError_MissingCharAfterHelperParameters, new object[]
					{
						this.Language.GetSample(CSharpSymbolType.LeftBrace)
					});
				}
			}
			base.AddMarkerSymbolIfNecessary();
			LocationTagged<string> content = base.Span.GetContent();
			HelperCodeGenerator helperCodeGenerator = new HelperCodeGenerator(content, flag3);
			this.Context.CurrentBlock.CodeGenerator = helperCodeGenerator;
			base.Span.CodeGenerator = SpanCodeGenerator.Null;
			if (!flag3)
			{
				this.CompleteBlock();
				base.Output(SpanKind.Code);
				return;
			}
			base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			base.Output(SpanKind.Code);
			AutoCompleteEditHandler autoCompleteEditHandler = new AutoCompleteEditHandler(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString));
			using (base.PushSpanConfig(new Action<SpanBuilder>(this.DefaultSpanConfig)))
			{
				using (this.Context.StartBlock(BlockType.Statement))
				{
					base.Span.EditHandler = autoCompleteEditHandler;
					this.CodeBlock(false, block);
					this.CompleteBlock(true);
					base.Output(SpanKind.Code);
				}
			}
			base.Initialize(base.Span);
			base.EnsureCurrent();
			base.Span.CodeGenerator = SpanCodeGenerator.Null;
			if (!base.Optional(CSharpSymbolType.RightBrace))
			{
				autoCompleteEditHandler.AutoCompleteString = "}";
				autoCompleteEditHandler.AcceptedCharacters = AcceptedCharacters.Any;
			}
			else
			{
				helperCodeGenerator.Footer = base.Span.GetContent();
				base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			}
			this.CompleteBlock();
			base.Output(SpanKind.Code);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009340 File Offset: 0x00007540
		protected virtual void SectionDirective()
		{
			bool flag = this.Context.IsWithin(BlockType.Section);
			bool flag2 = false;
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Section);
			base.AcceptAndMoveNext();
			if (flag)
			{
				this.Context.OnError(base.CurrentLocation, string.Format(CultureInfo.CurrentCulture, RazorResources.ParseError_Sections_Cannot_Be_Nested, new object[]
				{
					RazorResources.SectionExample_CS
				}));
				flag2 = true;
			}
			IEnumerable<CSharpSymbol> symbols = base.ReadWhile(CSharpCodeParser.IsSpacingToken(true, false));
			string sectionName = string.Empty;
			if (!base.Required(CSharpSymbolType.Identifier, true, RazorResources.ParseError_Unexpected_Character_At_Section_Name_Start))
			{
				if (!flag2)
				{
					flag2 = true;
				}
				base.PutCurrentBack();
				base.PutBack(symbols);
				base.AcceptWhile(CSharpCodeParser.IsSpacingToken(false, false));
			}
			else
			{
				base.Accept(symbols);
				sectionName = base.CurrentSymbol.Content;
				base.AcceptAndMoveNext();
			}
			this.Context.CurrentBlock.CodeGenerator = new SectionCodeGenerator(sectionName);
			SourceLocation currentLocation = base.CurrentLocation;
			symbols = base.ReadWhile(CSharpCodeParser.IsSpacingToken(true, false));
			if (!base.At(CSharpSymbolType.LeftBrace))
			{
				if (!flag2)
				{
					this.Context.OnError(currentLocation, RazorResources.ParseError_MissingOpenBraceAfterSection);
				}
				base.PutCurrentBack();
				base.PutBack(symbols);
				base.AcceptWhile(CSharpCodeParser.IsSpacingToken(false, false));
				base.Optional(CSharpSymbolType.NewLine);
				base.Output(SpanKind.MetaCode);
				this.CompleteBlock();
				return;
			}
			base.Accept(symbols);
			AutoCompleteEditHandler autoCompleteEditHandler = new AutoCompleteEditHandler(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString))
			{
				AutoCompleteAtEndOfSpan = true
			};
			base.Span.EditHandler = autoCompleteEditHandler;
			base.Span.Accept(base.CurrentSymbol);
			base.Output(SpanKind.MetaCode);
			this.SectionBlock("{", "}", true);
			base.Span.CodeGenerator = SpanCodeGenerator.Null;
			if (!base.Optional(CSharpSymbolType.RightBrace))
			{
				autoCompleteEditHandler.AutoCompleteString = "}";
				this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_Expected_X, new object[]
				{
					this.Language.GetSample(CSharpSymbolType.RightBrace)
				});
			}
			else
			{
				base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			}
			this.CompleteBlock(false, true);
			base.Output(SpanKind.MetaCode);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000956C File Offset: 0x0000776C
		protected virtual void FunctionsDirective()
		{
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Functions);
			CSharpCodeParser.Block block = new CSharpCodeParser.Block(base.CurrentSymbol);
			base.AcceptAndMoveNext();
			base.AcceptWhile(CSharpCodeParser.IsSpacingToken(true, false));
			if (!base.At(CSharpSymbolType.LeftBrace))
			{
				this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_Expected_X, new object[]
				{
					this.Language.GetSample(CSharpSymbolType.LeftBrace)
				});
				this.CompleteBlock();
				base.Output(SpanKind.MetaCode);
				return;
			}
			base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			SourceLocation currentLocation = base.CurrentLocation;
			base.AcceptAndMoveNext();
			base.Output(SpanKind.MetaCode);
			AutoCompleteEditHandler autoCompleteEditHandler = new AutoCompleteEditHandler(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString));
			base.Span.EditHandler = autoCompleteEditHandler;
			base.Balance(BalancingModes.NoErrorOnFailure, CSharpSymbolType.LeftBrace, CSharpSymbolType.RightBrace, currentLocation);
			base.Span.CodeGenerator = new TypeMemberCodeGenerator();
			if (!base.At(CSharpSymbolType.RightBrace))
			{
				autoCompleteEditHandler.AutoCompleteString = "}";
				this.Context.OnError(block.Start, RazorResources.ParseError_Expected_EndOfBlock_Before_EOF, new object[]
				{
					block.Name,
					"}",
					"{"
				});
				this.CompleteBlock();
				base.Output(SpanKind.Code);
				return;
			}
			base.Output(SpanKind.Code);
			base.Span.CodeGenerator = SpanCodeGenerator.Null;
			base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			base.AcceptAndMoveNext();
			this.CompleteBlock();
			base.Output(SpanKind.MetaCode);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000096F7 File Offset: 0x000078F7
		protected virtual void InheritsDirective()
		{
			base.AcceptAndMoveNext();
			this.InheritsDirectiveCore();
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00009706 File Offset: 0x00007906
		[Conditional("DEBUG")]
		protected void AssertDirective(string directive)
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00009710 File Offset: 0x00007910
		protected void InheritsDirectiveCore()
		{
			this.BaseTypeDirective(RazorResources.ParseError_InheritsKeyword_Must_Be_Followed_By_TypeName, (string baseType) => new SetBaseTypeCodeGenerator(baseType));
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000973C File Offset: 0x0000793C
		protected void BaseTypeDirective(string noTypeNameError, Func<string, SpanCodeGenerator> createCodeGenerator)
		{
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Directive);
			CSharpSymbol csharpSymbol = base.AcceptSingleWhiteSpaceCharacter();
			if (base.Span.Symbols.Count > 1)
			{
				base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			}
			base.Output(SpanKind.MetaCode);
			if (csharpSymbol != null)
			{
				base.Accept(csharpSymbol);
			}
			base.AcceptWhile(CSharpCodeParser.IsSpacingToken(false, true));
			if (base.EndOfFile || base.At(CSharpSymbolType.WhiteSpace) || base.At(CSharpSymbolType.NewLine))
			{
				this.Context.OnError(base.CurrentLocation, noTypeNameError);
			}
			base.AcceptUntil(CSharpSymbolType.NewLine);
			if (!this.Context.DesignTimeMode)
			{
				base.Optional(CSharpSymbolType.NewLine);
			}
			string text = base.Span.GetContent();
			base.Span.CodeGenerator = createCodeGenerator(text.Trim());
			this.CompleteBlock();
			base.Output(SpanKind.Code);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00009828 File Offset: 0x00007A28
		private void SetUpKeywords()
		{
			this.MapKeywords(new Action<bool>(this.ConditionalBlock), new CSharpKeyword[]
			{
				CSharpKeyword.For,
				CSharpKeyword.Foreach,
				CSharpKeyword.While,
				CSharpKeyword.Switch,
				CSharpKeyword.Lock
			});
			this.MapKeywords(new Action<bool>(this.CaseStatement), false, new CSharpKeyword[]
			{
				CSharpKeyword.Case,
				CSharpKeyword.Default
			});
			this.MapKeywords(new Action<bool>(this.IfStatement), new CSharpKeyword[]
			{
				CSharpKeyword.If
			});
			this.MapKeywords(new Action<bool>(this.TryStatement), new CSharpKeyword[]
			{
				CSharpKeyword.Try
			});
			this.MapKeywords(new Action<bool>(this.UsingKeyword), new CSharpKeyword[]
			{
				CSharpKeyword.Using
			});
			this.MapKeywords(new Action<bool>(this.DoStatement), new CSharpKeyword[]
			{
				CSharpKeyword.Do
			});
			this.MapKeywords(new Action<bool>(this.ReservedDirective), new CSharpKeyword[]
			{
				CSharpKeyword.Namespace,
				CSharpKeyword.Class
			});
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00009938 File Offset: 0x00007B38
		protected virtual void ReservedDirective(bool topLevel)
		{
			this.Context.OnError(base.CurrentLocation, string.Format(CultureInfo.CurrentCulture, RazorResources.ParseError_ReservedWord, new object[]
			{
				base.CurrentSymbol.Content
			}));
			base.AcceptAndMoveNext();
			base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			base.Span.CodeGenerator = SpanCodeGenerator.Null;
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Directive);
			this.CompleteBlock();
			base.Output(SpanKind.MetaCode);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000099F9 File Offset: 0x00007BF9
		private void KeywordBlock(bool topLevel)
		{
			this.HandleKeyword(topLevel, delegate
			{
				this.Context.CurrentBlock.Type = new BlockType?(BlockType.Expression);
				this.Context.CurrentBlock.CodeGenerator = new ExpressionCodeGenerator();
				this.ImplicitExpression();
			});
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009A0E File Offset: 0x00007C0E
		private void CaseStatement(bool topLevel)
		{
			base.AcceptUntil(CSharpSymbolType.Colon);
			base.Optional(CSharpSymbolType.Colon);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009A21 File Offset: 0x00007C21
		private void DoStatement(bool topLevel)
		{
			this.UnconditionalBlock();
			this.WhileClause();
			if (topLevel)
			{
				this.CompleteBlock();
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00009A38 File Offset: 0x00007C38
		private void WhileClause()
		{
			base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.Any;
			IEnumerable<CSharpSymbol> symbols = this.SkipToNextImportantToken();
			if (this.At(CSharpKeyword.While))
			{
				base.Accept(symbols);
				base.AcceptAndMoveNext();
				base.AcceptWhile(CSharpCodeParser.IsSpacingToken(true, true));
				if (this.AcceptCondition() && base.Optional(CSharpSymbolType.Semicolon))
				{
					base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
					return;
				}
			}
			else
			{
				base.PutCurrentBack();
				base.PutBack(symbols);
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009AB4 File Offset: 0x00007CB4
		private void UsingKeyword(bool topLevel)
		{
			CSharpCodeParser.Block block = new CSharpCodeParser.Block(base.CurrentSymbol);
			base.AcceptAndMoveNext();
			base.AcceptWhile(CSharpCodeParser.IsSpacingToken(false, true));
			if (base.At(CSharpSymbolType.LeftParenthesis))
			{
				this.UsingStatement(block);
			}
			else if (base.At(CSharpSymbolType.Identifier))
			{
				if (!topLevel)
				{
					this.Context.OnError(block.Start, RazorResources.ParseError_NamespaceImportAndTypeAlias_Cannot_Exist_Within_CodeBlock);
					this.StandardStatement();
				}
				else
				{
					this.UsingDeclaration();
				}
			}
			if (topLevel)
			{
				this.CompleteBlock();
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009B38 File Offset: 0x00007D38
		private void UsingDeclaration()
		{
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Directive);
			this.NamespaceOrTypeName();
			IEnumerable<CSharpSymbol> symbols = base.ReadWhile(CSharpCodeParser.IsSpacingToken(true, true));
			if (base.At(CSharpSymbolType.Assign))
			{
				base.Accept(symbols);
				base.AcceptAndMoveNext();
				base.AcceptWhile(CSharpCodeParser.IsSpacingToken(true, true));
				this.NamespaceOrTypeName();
			}
			else
			{
				base.PutCurrentBack();
				base.PutBack(symbols);
			}
			base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.AnyExceptNewline;
			base.Span.CodeGenerator = new AddImportCodeGenerator(base.Span.GetContent((IEnumerable<ISymbol> syms) => syms.Skip(1)), SyntaxConstants.CSharp.UsingKeywordLength);
			if (base.EnsureCurrent())
			{
				base.Optional(CSharpSymbolType.Semicolon);
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00009C10 File Offset: 0x00007E10
		private bool NamespaceOrTypeName()
		{
			if (base.Optional(CSharpSymbolType.Identifier) || base.Optional(CSharpSymbolType.Keyword))
			{
				base.Optional(CSharpSymbolType.QuestionMark);
				if (base.Optional(CSharpSymbolType.DoubleColon) && !base.Optional(CSharpSymbolType.Identifier))
				{
					base.Optional(CSharpSymbolType.Keyword);
				}
				if (base.At(CSharpSymbolType.LessThan))
				{
					this.TypeArgumentList();
				}
				if (base.Optional(CSharpSymbolType.Dot))
				{
					this.NamespaceOrTypeName();
				}
				while (base.At(CSharpSymbolType.LeftBracket))
				{
					base.Balance(BalancingModes.None);
					base.Optional(CSharpSymbolType.RightBracket);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009C94 File Offset: 0x00007E94
		private void TypeArgumentList()
		{
			base.Balance(BalancingModes.None);
			base.Optional(CSharpSymbolType.GreaterThan);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00009CA7 File Offset: 0x00007EA7
		private void UsingStatement(CSharpCodeParser.Block block)
		{
			if (this.AcceptCondition())
			{
				base.AcceptWhile(CSharpCodeParser.IsSpacingToken(true, true));
				this.ExpectCodeBlock(block);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00009CC5 File Offset: 0x00007EC5
		private void TryStatement(bool topLevel)
		{
			this.UnconditionalBlock();
			this.AfterTryClause();
			if (topLevel)
			{
				this.CompleteBlock();
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00009CDC File Offset: 0x00007EDC
		private void IfStatement(bool topLevel)
		{
			this.ConditionalBlock(false);
			this.AfterIfClause();
			if (topLevel)
			{
				this.CompleteBlock();
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00009CF4 File Offset: 0x00007EF4
		private void AfterTryClause()
		{
			IEnumerable<CSharpSymbol> symbols = this.SkipToNextImportantToken();
			if (this.At(CSharpKeyword.Catch))
			{
				base.Accept(symbols);
				this.ConditionalBlock(false);
				this.AfterTryClause();
				return;
			}
			if (this.At(CSharpKeyword.Finally))
			{
				base.Accept(symbols);
				this.UnconditionalBlock();
				return;
			}
			base.PutCurrentBack();
			base.PutBack(symbols);
			base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.Any;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00009D60 File Offset: 0x00007F60
		private void AfterIfClause()
		{
			IEnumerable<CSharpSymbol> symbols = this.SkipToNextImportantToken();
			if (this.At(CSharpKeyword.Else))
			{
				base.Accept(symbols);
				this.ElseClause();
				return;
			}
			base.PutCurrentBack();
			base.PutBack(symbols);
			base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.Any;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00009DAC File Offset: 0x00007FAC
		private void ElseClause()
		{
			if (!this.At(CSharpKeyword.Else))
			{
				return;
			}
			CSharpCodeParser.Block block = new CSharpCodeParser.Block(base.CurrentSymbol);
			base.AcceptAndMoveNext();
			base.AcceptWhile(CSharpCodeParser.IsSpacingToken(true, true));
			if (this.At(CSharpKeyword.If))
			{
				block.Name = SyntaxConstants.CSharp.ElseIfKeyword;
				this.ConditionalBlock(block);
				this.AfterIfClause();
				return;
			}
			if (!base.EndOfFile)
			{
				this.ExpectCodeBlock(block);
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00009E18 File Offset: 0x00008018
		private void ExpectCodeBlock(CSharpCodeParser.Block block)
		{
			if (!base.EndOfFile)
			{
				if (!base.At(CSharpSymbolType.LeftBrace))
				{
					this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_SingleLine_ControlFlowStatements_Not_Allowed, new object[]
					{
						this.Language.GetSample(CSharpSymbolType.LeftBrace),
						base.CurrentSymbol.Content
					});
				}
				this.Statement(block);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009E7C File Offset: 0x0000807C
		private void UnconditionalBlock()
		{
			CSharpCodeParser.Block block = new CSharpCodeParser.Block(base.CurrentSymbol);
			base.AcceptAndMoveNext();
			base.AcceptWhile(CSharpCodeParser.IsSpacingToken(true, true));
			this.ExpectCodeBlock(block);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009EB0 File Offset: 0x000080B0
		private void ConditionalBlock(bool topLevel)
		{
			CSharpCodeParser.Block block = new CSharpCodeParser.Block(base.CurrentSymbol);
			this.ConditionalBlock(block);
			if (topLevel)
			{
				this.CompleteBlock();
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009ED9 File Offset: 0x000080D9
		private void ConditionalBlock(CSharpCodeParser.Block block)
		{
			base.AcceptAndMoveNext();
			base.AcceptWhile(CSharpCodeParser.IsSpacingToken(true, true));
			if (this.AcceptCondition())
			{
				base.AcceptWhile(CSharpCodeParser.IsSpacingToken(true, true));
				this.ExpectCodeBlock(block);
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00009F0C File Offset: 0x0000810C
		private bool AcceptCondition()
		{
			if (base.At(CSharpSymbolType.LeftParenthesis))
			{
				bool flag = base.Balance(BalancingModes.BacktrackOnFailure | BalancingModes.AllowCommentsAndTemplates);
				if (!flag)
				{
					base.AcceptUntil(CSharpSymbolType.NewLine);
				}
				else
				{
					base.Optional(CSharpSymbolType.RightParenthesis);
				}
				return flag;
			}
			return true;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009F43 File Offset: 0x00008143
		private void Statement()
		{
			this.Statement(null);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009F4C File Offset: 0x0000814C
		private void Statement(CSharpCodeParser.Block block)
		{
			base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.Any;
			CSharpSymbol csharpSymbol = base.AcceptWhiteSpaceInLines();
			if (base.EndOfFile)
			{
				if (csharpSymbol != null)
				{
					base.Accept(csharpSymbol);
				}
				return;
			}
			CSharpSymbolType type = base.CurrentSymbol.Type;
			SourceLocation currentLocation = base.CurrentLocation;
			bool flag = type == CSharpSymbolType.Transition && base.NextIs(CSharpSymbolType.Colon);
			bool flag2 = flag || type == CSharpSymbolType.LessThan || (type == CSharpSymbolType.Transition && base.NextIs(CSharpSymbolType.LessThan));
			if (this.Context.DesignTimeMode || !flag2)
			{
				if (csharpSymbol != null)
				{
					base.Accept(csharpSymbol);
				}
			}
			else
			{
				base.PutCurrentBack();
				base.PutBack(csharpSymbol);
			}
			if (flag2)
			{
				if (type == CSharpSymbolType.Transition && !flag)
				{
					this.Context.OnError(currentLocation, RazorResources.ParseError_AtInCode_Must_Be_Followed_By_Colon_Paren_Or_Identifier_Start);
				}
				base.Output(SpanKind.Code);
				if (this.Context.DesignTimeMode && base.CurrentSymbol != null && (base.CurrentSymbol.Type == CSharpSymbolType.LessThan || base.CurrentSymbol.Type == CSharpSymbolType.Transition))
				{
					base.PutCurrentBack();
				}
				this.OtherParserBlock();
				return;
			}
			this.HandleStatement(block, type);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000A05C File Offset: 0x0000825C
		private void HandleStatement(CSharpCodeParser.Block block, CSharpSymbolType type)
		{
			if (type <= CSharpSymbolType.Comment)
			{
				if (type == CSharpSymbolType.Keyword)
				{
					this.HandleKeyword(false, new Action(this.StandardStatement));
					return;
				}
				if (type == CSharpSymbolType.Comment)
				{
					base.AcceptAndMoveNext();
					return;
				}
			}
			else
			{
				if (type == CSharpSymbolType.LeftBrace)
				{
					block = (block ?? new CSharpCodeParser.Block(RazorResources.BlockName_Code, base.CurrentLocation));
					base.AcceptAndMoveNext();
					this.CodeBlock(block);
					return;
				}
				if (type == CSharpSymbolType.RightBrace)
				{
					return;
				}
				switch (type)
				{
				case CSharpSymbolType.Transition:
					this.EmbeddedExpression();
					return;
				case CSharpSymbolType.RazorCommentTransition:
					base.Output(SpanKind.Code);
					base.RazorComment();
					this.Statement(block);
					return;
				}
			}
			this.StandardStatement();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000A0FC File Offset: 0x000082FC
		private void EmbeddedExpression()
		{
			CSharpSymbol currentSymbol = base.CurrentSymbol;
			base.NextToken();
			if (base.At(CSharpSymbolType.Transition))
			{
				base.Output(SpanKind.Code);
				base.Accept(currentSymbol);
				base.Span.CodeGenerator = SpanCodeGenerator.Null;
				base.Output(SpanKind.Code);
				base.AcceptAndMoveNext();
				this.StandardStatement();
				return;
			}
			if (base.At(CSharpSymbolType.Keyword))
			{
				this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_Unexpected_Keyword_After_At, new object[]
				{
					CSharpLanguageCharacteristics.GetKeyword(base.CurrentSymbol.Keyword.Value)
				});
			}
			else if (base.At(CSharpSymbolType.LeftBrace))
			{
				this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_Unexpected_Nested_CodeBlock);
			}
			base.PutCurrentBack();
			base.PutBack(currentSymbol);
			base.AddMarkerSymbolIfNecessary();
			this.NestedBlock();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A22C File Offset: 0x0000842C
		private void StandardStatement()
		{
			while (!base.EndOfFile)
			{
				int absoluteIndex = base.CurrentLocation.AbsoluteIndex;
				IEnumerable<CSharpSymbol> symbols = base.ReadWhile((CSharpSymbol sym) => sym.Type != CSharpSymbolType.Semicolon && sym.Type != CSharpSymbolType.RazorCommentTransition && sym.Type != CSharpSymbolType.Transition && sym.Type != CSharpSymbolType.LeftBrace && sym.Type != CSharpSymbolType.LeftParenthesis && sym.Type != CSharpSymbolType.LeftBracket && sym.Type != CSharpSymbolType.RightBrace);
				if (base.At(CSharpSymbolType.LeftBrace) || base.At(CSharpSymbolType.LeftParenthesis) || base.At(CSharpSymbolType.LeftBracket))
				{
					base.Accept(symbols);
					if (!base.Balance(BalancingModes.BacktrackOnFailure | BalancingModes.AllowCommentsAndTemplates))
					{
						base.AcceptUntil(CSharpSymbolType.LessThan, CSharpSymbolType.RightBrace);
						return;
					}
					base.Optional(CSharpSymbolType.RightBrace);
				}
				else if (base.At(CSharpSymbolType.Transition) && base.NextIs(new CSharpSymbolType[]
				{
					CSharpSymbolType.LessThan,
					CSharpSymbolType.Colon
				}))
				{
					base.Accept(symbols);
					base.Output(SpanKind.Code);
					this.Template();
				}
				else if (base.At(CSharpSymbolType.RazorCommentTransition))
				{
					base.Accept(symbols);
					base.RazorComment();
				}
				else
				{
					if (base.At(CSharpSymbolType.Semicolon))
					{
						base.Accept(symbols);
						base.AcceptAndMoveNext();
						return;
					}
					if (base.At(CSharpSymbolType.RightBrace))
					{
						base.Accept(symbols);
						return;
					}
					this.Context.Source.Position = absoluteIndex;
					base.NextToken();
					base.AcceptUntil(CSharpSymbolType.LessThan, CSharpSymbolType.RightBrace);
					return;
				}
			}
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000A364 File Offset: 0x00008564
		private void CodeBlock(CSharpCodeParser.Block block)
		{
			this.CodeBlock(true, block);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000A370 File Offset: 0x00008570
		private void CodeBlock(bool acceptTerminatingBrace, CSharpCodeParser.Block block)
		{
			base.EnsureCurrent();
			while (!base.EndOfFile && !base.At(CSharpSymbolType.RightBrace))
			{
				this.Statement();
				base.EnsureCurrent();
			}
			if (base.EndOfFile)
			{
				this.Context.OnError(block.Start, RazorResources.ParseError_Expected_EndOfBlock_Before_EOF, new object[]
				{
					block.Name,
					'}',
					'{'
				});
				return;
			}
			if (acceptTerminatingBrace)
			{
				base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
				base.AcceptAndMoveNext();
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000A404 File Offset: 0x00008604
		private void HandleKeyword(bool topLevel, Action fallback)
		{
			Action<bool> action;
			if (this._keywordParsers.TryGetValue(base.CurrentSymbol.Keyword.Value, out action))
			{
				action(topLevel);
				return;
			}
			fallback();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000A444 File Offset: 0x00008644
		private IEnumerable<CSharpSymbol> SkipToNextImportantToken()
		{
			while (!base.EndOfFile)
			{
				IEnumerable<CSharpSymbol> enumerable = base.ReadWhile(CSharpCodeParser.IsSpacingToken(true, true));
				if (!base.At(CSharpSymbolType.RazorCommentTransition))
				{
					return enumerable;
				}
				base.Accept(enumerable);
				base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.Any;
				base.RazorComment();
			}
			return Enumerable.Empty<CSharpSymbol>();
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000A49A File Offset: 0x0000869A
		protected override void OutputSpanBeforeRazorComment()
		{
			base.AddMarkerSymbolIfNecessary();
			base.Output(SpanKind.Code);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000A4A9 File Offset: 0x000086A9
		public CSharpCodeParser()
		{
			this.Keywords = new HashSet<string>();
			this.SetUpKeywords();
			this.SetupDirectives();
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000A4DE File Offset: 0x000086DE
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000A4E6 File Offset: 0x000086E6
		protected internal ISet<string> Keywords { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000A4EF File Offset: 0x000086EF
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000A4F7 File Offset: 0x000086F7
		public bool IsNested { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000A500 File Offset: 0x00008700
		protected override ParserBase OtherParser
		{
			get
			{
				return this.Context.MarkupParser;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000A50D File Offset: 0x0000870D
		protected override LanguageCharacteristics<CSharpTokenizer, CSharpSymbol, CSharpSymbolType> Language
		{
			get
			{
				return CSharpLanguageCharacteristics.Instance;
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000A514 File Offset: 0x00008714
		protected void MapDirectives(Action handler, params string[] directives)
		{
			foreach (string text in directives)
			{
				this._directiveParsers.Add(text, handler);
				this.Keywords.Add(text);
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000A54F File Offset: 0x0000874F
		protected bool TryGetDirectiveHandler(string directive, out Action handler)
		{
			return this._directiveParsers.TryGetValue(directive, out handler);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A55E File Offset: 0x0000875E
		private void MapKeywords(Action<bool> handler, params CSharpKeyword[] keywords)
		{
			this.MapKeywords(handler, true, keywords);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A56C File Offset: 0x0000876C
		private void MapKeywords(Action<bool> handler, bool topLevel, params CSharpKeyword[] keywords)
		{
			foreach (CSharpKeyword csharpKeyword in keywords)
			{
				this._keywordParsers.Add(csharpKeyword, handler);
				if (topLevel)
				{
					this.Keywords.Add(CSharpLanguageCharacteristics.GetKeyword(csharpKeyword));
				}
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000A5AF File Offset: 0x000087AF
		[Conditional("DEBUG")]
		internal void Assert(CSharpKeyword expectedKeyword)
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A5B4 File Offset: 0x000087B4
		protected internal bool At(CSharpKeyword keyword)
		{
			return base.At(CSharpSymbolType.Keyword) && base.CurrentSymbol.Keyword != null && base.CurrentSymbol.Keyword.Value == keyword;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000A5F7 File Offset: 0x000087F7
		protected internal bool AcceptIf(CSharpKeyword keyword)
		{
			if (this.At(keyword))
			{
				base.AcceptAndMoveNext();
				return true;
			}
			return false;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000A648 File Offset: 0x00008848
		protected static Func<CSharpSymbol, bool> IsSpacingToken(bool includeNewLines, bool includeComments)
		{
			return (CSharpSymbol sym) => sym.Type == CSharpSymbolType.WhiteSpace || (includeNewLines && sym.Type == CSharpSymbolType.NewLine) || (includeComments && sym.Type == CSharpSymbolType.Comment);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000A678 File Offset: 0x00008878
		public override void ParseBlock()
		{
			using (base.PushSpanConfig(new Action<SpanBuilder>(this.DefaultSpanConfig)))
			{
				if (this.Context == null)
				{
					throw new InvalidOperationException(RazorResources.Parser_Context_Not_Set);
				}
				using (this.Context.StartBlock(BlockType.Statement))
				{
					base.NextToken();
					base.AcceptWhile(CSharpCodeParser.IsSpacingToken(true, true));
					CSharpSymbol csharpSymbol = base.CurrentSymbol;
					if (base.At(CSharpSymbolType.StringLiteral) && base.CurrentSymbol.Content.Length > 0 && base.CurrentSymbol.Content[0] == SyntaxConstants.TransitionCharacter)
					{
						Tuple<CSharpSymbol, CSharpSymbol> tuple = this.Language.SplitSymbol(base.CurrentSymbol, 1, CSharpSymbolType.Transition);
						csharpSymbol = tuple.Item1;
						this.Context.Source.Position = tuple.Item2.Start.AbsoluteIndex;
						base.NextToken();
					}
					else if (base.At(CSharpSymbolType.Transition))
					{
						base.NextToken();
					}
					if (csharpSymbol.Type == CSharpSymbolType.Transition)
					{
						if (base.Span.Symbols.Count > 0)
						{
							base.Output(SpanKind.Code);
						}
						this.AtTransition(csharpSymbol);
					}
					else
					{
						this.AfterTransition();
					}
					base.Output(SpanKind.Code);
				}
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000A7E8 File Offset: 0x000089E8
		private void DefaultSpanConfig(SpanBuilder span)
		{
			span.EditHandler = SpanEditHandler.CreateDefault(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString));
			span.CodeGenerator = new StatementCodeGenerator();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000A812 File Offset: 0x00008A12
		private void AtTransition(CSharpSymbol current)
		{
			base.Accept(current);
			base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			base.Span.CodeGenerator = SpanCodeGenerator.Null;
			base.Output(SpanKind.Transition);
			this.AfterTransition();
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000A84C File Offset: 0x00008A4C
		private void AfterTransition()
		{
			using (base.PushSpanConfig(new Action<SpanBuilder>(this.DefaultSpanConfig)))
			{
				base.EnsureCurrent();
				try
				{
					if (!base.EndOfFile)
					{
						if (base.CurrentSymbol.Type == CSharpSymbolType.LeftParenthesis)
						{
							this.Context.CurrentBlock.Type = new BlockType?(BlockType.Expression);
							this.Context.CurrentBlock.CodeGenerator = new ExpressionCodeGenerator();
							this.ExplicitExpression();
							return;
						}
						if (base.CurrentSymbol.Type == CSharpSymbolType.Identifier)
						{
							Action action;
							if (this.TryGetDirectiveHandler(base.CurrentSymbol.Content, out action))
							{
								base.Span.CodeGenerator = SpanCodeGenerator.Null;
								action();
								return;
							}
							this.Context.CurrentBlock.Type = new BlockType?(BlockType.Expression);
							this.Context.CurrentBlock.CodeGenerator = new ExpressionCodeGenerator();
							this.ImplicitExpression();
							return;
						}
						else
						{
							if (base.CurrentSymbol.Type == CSharpSymbolType.Keyword)
							{
								this.KeywordBlock(true);
								return;
							}
							if (base.CurrentSymbol.Type == CSharpSymbolType.LeftBrace)
							{
								this.VerbatimBlock();
								return;
							}
						}
					}
					this.Context.CurrentBlock.Type = new BlockType?(BlockType.Expression);
					this.Context.CurrentBlock.CodeGenerator = new ExpressionCodeGenerator();
					base.AddMarkerSymbolIfNecessary();
					base.Span.CodeGenerator = new ExpressionCodeGenerator();
					base.Span.EditHandler = new ImplicitExpressionEditHandler(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString), CSharpCodeParser.DefaultKeywords, this.IsNested)
					{
						AcceptedCharacters = AcceptedCharacters.NonWhiteSpace
					};
					if (base.At(CSharpSymbolType.WhiteSpace) || base.At(CSharpSymbolType.NewLine))
					{
						this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_Unexpected_WhiteSpace_At_Start_Of_CodeBlock_CS);
					}
					else if (base.EndOfFile)
					{
						this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_Unexpected_EndOfFile_At_Start_Of_CodeBlock);
					}
					else
					{
						this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_Unexpected_Character_At_Start_Of_CodeBlock_CS, new object[]
						{
							base.CurrentSymbol.Content
						});
					}
				}
				finally
				{
					base.PutCurrentBack();
				}
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		private void VerbatimBlock()
		{
			CSharpCodeParser.Block block = new CSharpCodeParser.Block(RazorResources.BlockName_Code, base.CurrentLocation);
			base.AcceptAndMoveNext();
			base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			base.Span.CodeGenerator = SpanCodeGenerator.Null;
			base.Output(SpanKind.MetaCode);
			AutoCompleteEditHandler autoCompleteEditHandler = new AutoCompleteEditHandler(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString));
			base.Span.EditHandler = autoCompleteEditHandler;
			this.CodeBlock(false, block);
			base.Span.CodeGenerator = new StatementCodeGenerator();
			base.AddMarkerSymbolIfNecessary();
			if (!base.At(CSharpSymbolType.RightBrace))
			{
				autoCompleteEditHandler.AutoCompleteString = "}";
			}
			base.Output(SpanKind.Code);
			if (base.Optional(CSharpSymbolType.RightBrace))
			{
				base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
				base.Span.CodeGenerator = SpanCodeGenerator.Null;
			}
			if (!base.At(CSharpSymbolType.WhiteSpace) && !base.At(CSharpSymbolType.NewLine))
			{
				base.PutCurrentBack();
			}
			this.CompleteBlock(false);
			base.Output(SpanKind.MetaCode);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000ABF0 File Offset: 0x00008DF0
		private void ImplicitExpression()
		{
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Expression);
			this.Context.CurrentBlock.CodeGenerator = new ExpressionCodeGenerator();
			using (base.PushSpanConfig(delegate(SpanBuilder span)
			{
				span.EditHandler = new ImplicitExpressionEditHandler(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString), this.Keywords, this.IsNested);
				span.EditHandler.AcceptedCharacters = AcceptedCharacters.NonWhiteSpace;
				span.CodeGenerator = new ExpressionCodeGenerator();
			}))
			{
				do
				{
					if (base.AtIdentifier(true))
					{
						base.AcceptAndMoveNext();
					}
				}
				while (this.MethodCallOrArrayIndex());
				base.PutCurrentBack();
				base.Output(SpanKind.Code);
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000AC9C File Offset: 0x00008E9C
		private bool MethodCallOrArrayIndex()
		{
			if (!base.EndOfFile)
			{
				if (base.CurrentSymbol.Type == CSharpSymbolType.LeftParenthesis || base.CurrentSymbol.Type == CSharpSymbolType.LeftBracket)
				{
					base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.Any;
					CSharpSymbolType type;
					bool flag;
					using (base.PushSpanConfig(delegate(SpanBuilder span, Action<SpanBuilder> prev)
					{
						prev(span);
						span.EditHandler.AcceptedCharacters = AcceptedCharacters.Any;
					}))
					{
						type = this.Language.FlipBracket(base.CurrentSymbol.Type);
						flag = base.Balance(BalancingModes.BacktrackOnFailure | BalancingModes.AllowCommentsAndTemplates);
					}
					if (!flag)
					{
						base.AcceptUntil(CSharpSymbolType.LessThan);
					}
					if (base.At(type))
					{
						base.AcceptAndMoveNext();
						base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.NonWhiteSpace;
					}
					return this.MethodCallOrArrayIndex();
				}
				if (base.CurrentSymbol.Type == CSharpSymbolType.Dot)
				{
					CSharpSymbol currentSymbol = base.CurrentSymbol;
					if (base.NextToken())
					{
						if (base.At(CSharpSymbolType.Identifier) || base.At(CSharpSymbolType.Keyword))
						{
							base.Accept(currentSymbol);
							return true;
						}
						base.PutCurrentBack();
					}
					if (!this.IsNested)
					{
						base.PutBack(currentSymbol);
					}
					else
					{
						base.Accept(currentSymbol);
					}
				}
				else if (!base.At(CSharpSymbolType.WhiteSpace) && !base.At(CSharpSymbolType.NewLine))
				{
					base.PutCurrentBack();
				}
			}
			return false;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000ADEC File Offset: 0x00008FEC
		private void CompleteBlock()
		{
			this.CompleteBlock(true);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000ADF5 File Offset: 0x00008FF5
		private void CompleteBlock(bool insertMarkerIfNecessary)
		{
			this.CompleteBlock(insertMarkerIfNecessary, insertMarkerIfNecessary);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000AE00 File Offset: 0x00009000
		private void CompleteBlock(bool insertMarkerIfNecessary, bool captureWhitespaceToEndOfLine)
		{
			if (insertMarkerIfNecessary && this.Context.LastAcceptedCharacters != AcceptedCharacters.Any)
			{
				base.AddMarkerSymbolIfNecessary();
			}
			base.EnsureCurrent();
			if (!this.Context.WhiteSpaceIsSignificantToAncestorBlock && this.Context.CurrentBlock.Type != BlockType.Expression && captureWhitespaceToEndOfLine && !this.Context.DesignTimeMode && !this.IsNested)
			{
				this.CaptureWhitespaceAtEndOfCodeOnlyLine();
				return;
			}
			base.PutCurrentBack();
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000AE94 File Offset: 0x00009094
		private void CaptureWhitespaceAtEndOfCodeOnlyLine()
		{
			IEnumerable<CSharpSymbol> symbols = base.ReadWhile((CSharpSymbol sym) => sym.Type == CSharpSymbolType.WhiteSpace);
			if (base.At(CSharpSymbolType.NewLine))
			{
				base.Accept(symbols);
				base.AcceptAndMoveNext();
				base.PutCurrentBack();
				return;
			}
			base.PutCurrentBack();
			base.PutBack(symbols);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000AEF0 File Offset: 0x000090F0
		private void ConfigureExplicitExpressionSpan(SpanBuilder sb)
		{
			sb.EditHandler = SpanEditHandler.CreateDefault(new Func<string, IEnumerable<ISymbol>>(this.Language.TokenizeString));
			sb.CodeGenerator = new ExpressionCodeGenerator();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000AF1C File Offset: 0x0000911C
		private void ExplicitExpression()
		{
			CSharpCodeParser.Block block = new CSharpCodeParser.Block(RazorResources.BlockName_ExplicitExpression, base.CurrentLocation);
			base.AcceptAndMoveNext();
			base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			base.Span.CodeGenerator = SpanCodeGenerator.Null;
			base.Output(SpanKind.MetaCode);
			using (base.PushSpanConfig(new Action<SpanBuilder>(this.ConfigureExplicitExpressionSpan)))
			{
				if (!base.Balance(BalancingModes.BacktrackOnFailure | BalancingModes.NoErrorOnFailure | BalancingModes.AllowCommentsAndTemplates, CSharpSymbolType.LeftParenthesis, CSharpSymbolType.RightParenthesis, block.Start))
				{
					base.AcceptUntil(CSharpSymbolType.LessThan);
					this.Context.OnError(block.Start, RazorResources.ParseError_Expected_EndOfBlock_Before_EOF, new object[]
					{
						block.Name,
						")",
						"("
					});
				}
				if (base.Span.Symbols.Count == 0)
				{
					base.Accept(new CSharpSymbol(base.CurrentLocation, string.Empty, CSharpSymbolType.Unknown));
				}
				base.Output(SpanKind.Code);
			}
			base.Optional(CSharpSymbolType.RightParenthesis);
			if (!base.EndOfFile)
			{
				base.PutCurrentBack();
			}
			base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			base.Span.CodeGenerator = SpanCodeGenerator.Null;
			this.CompleteBlock(false);
			base.Output(SpanKind.MetaCode);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B064 File Offset: 0x00009264
		private void Template()
		{
			if (this.Context.IsWithin(BlockType.Template))
			{
				this.Context.OnError(base.CurrentLocation, RazorResources.ParseError_InlineMarkup_Blocks_Cannot_Be_Nested);
			}
			base.Output(SpanKind.Code);
			using (this.Context.StartBlock(BlockType.Template))
			{
				this.Context.CurrentBlock.CodeGenerator = new TemplateBlockCodeGenerator();
				base.PutCurrentBack();
				this.OtherParserBlock();
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B0F0 File Offset: 0x000092F0
		private void OtherParserBlock()
		{
			this.ParseWithOtherParser(delegate(ParserBase p)
			{
				p.ParseBlock();
			});
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B13C File Offset: 0x0000933C
		private void SectionBlock(string left, string right, bool caseSensitive)
		{
			this.ParseWithOtherParser(delegate(ParserBase p)
			{
				p.ParseSection(Tuple.Create<string, string>(left, right), caseSensitive);
			});
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000B178 File Offset: 0x00009378
		private void NestedBlock()
		{
			base.Output(SpanKind.Code);
			bool isNested = this.IsNested;
			this.IsNested = true;
			using (base.PushSpanConfig())
			{
				this.ParseBlock();
			}
			base.Initialize(base.Span);
			this.IsNested = isNested;
			base.NextToken();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000B1E0 File Offset: 0x000093E0
		protected override bool IsAtEmbeddedTransition(bool allowTemplatesAndComments, bool allowTransitions)
		{
			return allowTemplatesAndComments && ((this.Language.IsTransition(base.CurrentSymbol) && base.NextIs(new CSharpSymbolType[]
			{
				CSharpSymbolType.LessThan,
				CSharpSymbolType.Colon
			})) || this.Language.IsCommentStart(base.CurrentSymbol));
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000B232 File Offset: 0x00009432
		protected override void HandleEmbeddedTransition()
		{
			if (this.Language.IsTransition(base.CurrentSymbol))
			{
				base.PutCurrentBack();
				this.Template();
				return;
			}
			if (this.Language.IsCommentStart(base.CurrentSymbol))
			{
				base.RazorComment();
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000B270 File Offset: 0x00009470
		private void ParseWithOtherParser(Action<ParserBase> parseAction)
		{
			using (base.PushSpanConfig())
			{
				this.Context.SwitchActiveParser();
				parseAction(this.Context.MarkupParser);
				this.Context.SwitchActiveParser();
			}
			base.Initialize(base.Span);
			base.NextToken();
		}

		// Token: 0x040000A2 RID: 162
		internal static readonly int UsingKeywordLength = 5;

		// Token: 0x040000A3 RID: 163
		internal static ISet<string> DefaultKeywords = new HashSet<string>
		{
			"if",
			"do",
			"try",
			"for",
			"foreach",
			"while",
			"switch",
			"lock",
			"using",
			"section",
			"inherits",
			"helper",
			"functions",
			"namespace",
			"class",
			"layout",
			"sessionstate"
		};

		// Token: 0x040000A4 RID: 164
		private Dictionary<string, Action> _directiveParsers = new Dictionary<string, Action>();

		// Token: 0x040000A5 RID: 165
		private Dictionary<CSharpKeyword, Action<bool>> _keywordParsers = new Dictionary<CSharpKeyword, Action<bool>>();

		// Token: 0x0200003D RID: 61
		protected class Block
		{
			// Token: 0x060002C9 RID: 713 RVA: 0x0000B3C7 File Offset: 0x000095C7
			public Block(string name, SourceLocation start)
			{
				this.Name = name;
				this.Start = start;
			}

			// Token: 0x060002CA RID: 714 RVA: 0x0000B3DD File Offset: 0x000095DD
			public Block(CSharpSymbol symbol) : this(CSharpCodeParser.Block.GetName(symbol), symbol.Start)
			{
			}

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x060002CB RID: 715 RVA: 0x0000B3F1 File Offset: 0x000095F1
			// (set) Token: 0x060002CC RID: 716 RVA: 0x0000B3F9 File Offset: 0x000095F9
			public string Name { get; set; }

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x060002CD RID: 717 RVA: 0x0000B402 File Offset: 0x00009602
			// (set) Token: 0x060002CE RID: 718 RVA: 0x0000B40A File Offset: 0x0000960A
			public SourceLocation Start { get; set; }

			// Token: 0x060002CF RID: 719 RVA: 0x0000B414 File Offset: 0x00009614
			private static string GetName(CSharpSymbol sym)
			{
				if (sym.Type == CSharpSymbolType.Keyword)
				{
					return CSharpLanguageCharacteristics.GetKeyword(sym.Keyword.Value);
				}
				return sym.Content;
			}
		}
	}
}

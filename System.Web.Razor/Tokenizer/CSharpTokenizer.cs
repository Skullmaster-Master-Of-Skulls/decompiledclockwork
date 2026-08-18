using System;
using System.Collections.Generic;
using System.Web.Razor.Parser;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Resources;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer.Symbols;

namespace System.Web.Razor.Tokenizer
{
	// Token: 0x02000077 RID: 119
	public class CSharpTokenizer : Tokenizer<CSharpSymbol, CSharpSymbolType>
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x00014038 File Offset: 0x00012238
		public CSharpTokenizer(ITextDocument source) : base(source)
		{
			base.CurrentState = new StateMachine<CSharpSymbol>.State(this.Data);
			Dictionary<char, Func<CSharpSymbolType>> dictionary = new Dictionary<char, Func<CSharpSymbolType>>();
			dictionary.Add('-', new Func<CSharpSymbolType>(this.MinusOperator));
			dictionary.Add('<', new Func<CSharpSymbolType>(this.LessThanOperator));
			dictionary.Add('>', new Func<CSharpSymbolType>(this.GreaterThanOperator));
			dictionary.Add('&', this.CreateTwoCharOperatorHandler(CSharpSymbolType.And, '=', CSharpSymbolType.AndAssign, '&', CSharpSymbolType.DoubleAnd));
			dictionary.Add('|', this.CreateTwoCharOperatorHandler(CSharpSymbolType.Or, '=', CSharpSymbolType.OrAssign, '|', CSharpSymbolType.DoubleOr));
			dictionary.Add('+', this.CreateTwoCharOperatorHandler(CSharpSymbolType.Plus, '=', CSharpSymbolType.PlusAssign, '+', CSharpSymbolType.Increment));
			dictionary.Add('=', this.CreateTwoCharOperatorHandler(CSharpSymbolType.Assign, '=', CSharpSymbolType.Equals, '>', CSharpSymbolType.GreaterThanEqual));
			dictionary.Add('!', this.CreateTwoCharOperatorHandler(CSharpSymbolType.Not, '=', CSharpSymbolType.NotEqual));
			dictionary.Add('%', this.CreateTwoCharOperatorHandler(CSharpSymbolType.Modulo, '=', CSharpSymbolType.ModuloAssign));
			dictionary.Add('*', this.CreateTwoCharOperatorHandler(CSharpSymbolType.Star, '=', CSharpSymbolType.MultiplyAssign));
			dictionary.Add(':', this.CreateTwoCharOperatorHandler(CSharpSymbolType.Colon, ':', CSharpSymbolType.DoubleColon));
			dictionary.Add('?', this.CreateTwoCharOperatorHandler(CSharpSymbolType.QuestionMark, '?', CSharpSymbolType.NullCoalesce));
			dictionary.Add('^', this.CreateTwoCharOperatorHandler(CSharpSymbolType.Xor, '=', CSharpSymbolType.XorAssign));
			dictionary.Add('(', () => CSharpSymbolType.LeftParenthesis);
			dictionary.Add(')', () => CSharpSymbolType.RightParenthesis);
			dictionary.Add('{', () => CSharpSymbolType.LeftBrace);
			dictionary.Add('}', () => CSharpSymbolType.RightBrace);
			dictionary.Add('[', () => CSharpSymbolType.LeftBracket);
			dictionary.Add(']', () => CSharpSymbolType.RightBracket);
			dictionary.Add(',', () => CSharpSymbolType.Comma);
			dictionary.Add(';', () => CSharpSymbolType.Semicolon);
			dictionary.Add('~', () => CSharpSymbolType.Tilde);
			dictionary.Add('#', () => CSharpSymbolType.Hash);
			this._operatorHandlers = dictionary;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x000142F1 File Offset: 0x000124F1
		protected override StateMachine<CSharpSymbol>.State StartState
		{
			get
			{
				return new StateMachine<CSharpSymbol>.State(this.Data);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x000142FF File Offset: 0x000124FF
		public override CSharpSymbolType RazorCommentType
		{
			get
			{
				return CSharpSymbolType.RazorComment;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00014303 File Offset: 0x00012503
		public override CSharpSymbolType RazorCommentTransitionType
		{
			get
			{
				return CSharpSymbolType.RazorCommentTransition;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00014307 File Offset: 0x00012507
		public override CSharpSymbolType RazorCommentStarType
		{
			get
			{
				return CSharpSymbolType.RazorCommentStar;
			}
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001430B File Offset: 0x0001250B
		protected override CSharpSymbol CreateSymbol(SourceLocation start, string content, CSharpSymbolType type, IEnumerable<RazorError> errors)
		{
			return new CSharpSymbol(start, content, type, errors);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001433C File Offset: 0x0001253C
		private StateMachine<CSharpSymbol>.StateResult Data()
		{
			if (ParserHelpers.IsNewLine(base.CurrentCharacter))
			{
				bool flag = base.CurrentCharacter == '\r';
				base.TakeCurrent();
				if (flag && base.CurrentCharacter == '\n')
				{
					base.TakeCurrent();
				}
				return base.Stay(base.EndSymbol(CSharpSymbolType.NewLine));
			}
			if (ParserHelpers.IsWhitespace(base.CurrentCharacter))
			{
				base.TakeUntil((char c) => !ParserHelpers.IsWhitespace(c));
				return base.Stay(base.EndSymbol(CSharpSymbolType.WhiteSpace));
			}
			if (CSharpHelpers.IsIdentifierStart(base.CurrentCharacter))
			{
				return this.Identifier();
			}
			if (char.IsDigit(base.CurrentCharacter))
			{
				return this.NumericLiteral();
			}
			char currentCharacter = base.CurrentCharacter;
			if (currentCharacter <= '\'')
			{
				if (currentCharacter == '"')
				{
					base.TakeCurrent();
					return base.Transition(() => this.QuotedLiteral('"', CSharpSymbolType.StringLiteral));
				}
				if (currentCharacter == '\'')
				{
					base.TakeCurrent();
					return base.Transition(() => this.QuotedLiteral('\'', CSharpSymbolType.CharacterLiteral));
				}
			}
			else
			{
				switch (currentCharacter)
				{
				case '.':
					if (char.IsDigit(base.Peek()))
					{
						return this.RealLiteral();
					}
					return base.Stay(base.Single(CSharpSymbolType.Dot));
				case '/':
					base.TakeCurrent();
					if (base.CurrentCharacter == '/')
					{
						base.TakeCurrent();
						return this.SingleLineComment();
					}
					if (base.CurrentCharacter == '*')
					{
						base.TakeCurrent();
						return base.Transition(new StateMachine<CSharpSymbol>.State(this.BlockComment));
					}
					if (base.CurrentCharacter == '=')
					{
						base.TakeCurrent();
						return base.Stay(base.EndSymbol(CSharpSymbolType.DivideAssign));
					}
					return base.Stay(base.EndSymbol(CSharpSymbolType.Slash));
				default:
					if (currentCharacter == '@')
					{
						return this.AtSymbol();
					}
					break;
				}
			}
			return base.Stay(base.EndSymbol(this.Operator()));
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00014530 File Offset: 0x00012730
		private StateMachine<CSharpSymbol>.StateResult AtSymbol()
		{
			base.TakeCurrent();
			if (base.CurrentCharacter == '"')
			{
				base.TakeCurrent();
				return base.Transition(new StateMachine<CSharpSymbol>.State(this.VerbatimStringLiteral));
			}
			if (base.CurrentCharacter == '*')
			{
				return base.Transition(base.EndSymbol(CSharpSymbolType.RazorCommentTransition), new StateMachine<CSharpSymbol>.State(base.AfterRazorCommentTransition));
			}
			if (base.CurrentCharacter == '@')
			{
				return base.Transition(base.EndSymbol(CSharpSymbolType.Transition), delegate()
				{
					base.TakeCurrent();
					return base.Transition(base.EndSymbol(CSharpSymbolType.Transition), new StateMachine<CSharpSymbol>.State(this.Data));
				});
			}
			return base.Stay(base.EndSymbol(CSharpSymbolType.Transition));
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x000145C8 File Offset: 0x000127C8
		private CSharpSymbolType Operator()
		{
			char currentCharacter = base.CurrentCharacter;
			base.TakeCurrent();
			Func<CSharpSymbolType> func;
			if (this._operatorHandlers.TryGetValue(currentCharacter, out func))
			{
				return func();
			}
			return CSharpSymbolType.Unknown;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000145FA File Offset: 0x000127FA
		private CSharpSymbolType LessThanOperator()
		{
			if (base.CurrentCharacter == '=')
			{
				base.TakeCurrent();
				return CSharpSymbolType.LessThanEqual;
			}
			return CSharpSymbolType.LessThan;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00014611 File Offset: 0x00012811
		private CSharpSymbolType GreaterThanOperator()
		{
			if (base.CurrentCharacter == '=')
			{
				base.TakeCurrent();
				return CSharpSymbolType.GreaterThanEqual;
			}
			return CSharpSymbolType.GreaterThan;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00014628 File Offset: 0x00012828
		private CSharpSymbolType MinusOperator()
		{
			if (base.CurrentCharacter == '>')
			{
				base.TakeCurrent();
				return CSharpSymbolType.Arrow;
			}
			if (base.CurrentCharacter == '-')
			{
				base.TakeCurrent();
				return CSharpSymbolType.Decrement;
			}
			if (base.CurrentCharacter == '=')
			{
				base.TakeCurrent();
				return CSharpSymbolType.MinusAssign;
			}
			return CSharpSymbolType.Minus;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0001469C File Offset: 0x0001289C
		private Func<CSharpSymbolType> CreateTwoCharOperatorHandler(CSharpSymbolType typeIfOnlyFirst, char second, CSharpSymbolType typeIfBoth)
		{
			return delegate()
			{
				if (this.CurrentCharacter == second)
				{
					this.TakeCurrent();
					return typeIfBoth;
				}
				return typeIfOnlyFirst;
			};
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00014740 File Offset: 0x00012940
		private Func<CSharpSymbolType> CreateTwoCharOperatorHandler(CSharpSymbolType typeIfOnlyFirst, char option1, CSharpSymbolType typeIfOption1, char option2, CSharpSymbolType typeIfOption2)
		{
			return delegate()
			{
				if (this.CurrentCharacter == option1)
				{
					this.TakeCurrent();
					return typeIfOption1;
				}
				if (this.CurrentCharacter == option2)
				{
					this.TakeCurrent();
					return typeIfOption2;
				}
				return typeIfOnlyFirst;
			};
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00014794 File Offset: 0x00012994
		private StateMachine<CSharpSymbol>.StateResult VerbatimStringLiteral()
		{
			base.TakeUntil((char c) => c == '"');
			if (base.CurrentCharacter == '"')
			{
				base.TakeCurrent();
				if (base.CurrentCharacter == '"')
				{
					base.TakeCurrent();
					return base.Stay();
				}
			}
			else if (base.EndOfFile)
			{
				base.CurrentErrors.Add(new RazorError(RazorResources.ParseError_Unterminated_String_Literal, base.CurrentStart));
			}
			return base.Transition(base.EndSymbol(CSharpSymbolType.StringLiteral), new StateMachine<CSharpSymbol>.State(this.Data));
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001484C File Offset: 0x00012A4C
		private StateMachine<CSharpSymbol>.StateResult QuotedLiteral(char quote, CSharpSymbolType literalType)
		{
			base.TakeUntil((char c) => c == '\\' || c == quote || ParserHelpers.IsNewLine(c));
			if (base.CurrentCharacter == '\\')
			{
				base.TakeCurrent();
				if (base.CurrentCharacter == quote || base.CurrentCharacter == '\\')
				{
					base.TakeCurrent();
				}
				return base.Stay();
			}
			if (base.EndOfFile || ParserHelpers.IsNewLine(base.CurrentCharacter))
			{
				base.CurrentErrors.Add(new RazorError(RazorResources.ParseError_Unterminated_String_Literal, base.CurrentStart));
			}
			else
			{
				base.TakeCurrent();
			}
			return base.Transition(base.EndSymbol(literalType), new StateMachine<CSharpSymbol>.State(this.Data));
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00014908 File Offset: 0x00012B08
		private StateMachine<CSharpSymbol>.StateResult BlockComment()
		{
			base.TakeUntil((char c) => c == '*');
			if (base.EndOfFile)
			{
				base.CurrentErrors.Add(new RazorError(RazorResources.ParseError_BlockComment_Not_Terminated, base.CurrentStart));
				return base.Transition(base.EndSymbol(CSharpSymbolType.Comment), new StateMachine<CSharpSymbol>.State(this.Data));
			}
			if (base.CurrentCharacter == '*')
			{
				base.TakeCurrent();
				if (base.CurrentCharacter == '/')
				{
					base.TakeCurrent();
					return base.Transition(base.EndSymbol(CSharpSymbolType.Comment), new StateMachine<CSharpSymbol>.State(this.Data));
				}
			}
			return base.Stay();
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000149BE File Offset: 0x00012BBE
		private StateMachine<CSharpSymbol>.StateResult SingleLineComment()
		{
			base.TakeUntil((char c) => ParserHelpers.IsNewLine(c));
			return base.Stay(base.EndSymbol(CSharpSymbolType.Comment));
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000149F1 File Offset: 0x00012BF1
		private StateMachine<CSharpSymbol>.StateResult NumericLiteral()
		{
			if (base.TakeAll("0x", true))
			{
				return this.HexLiteral();
			}
			return this.DecimalLiteral();
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00014A19 File Offset: 0x00012C19
		private StateMachine<CSharpSymbol>.StateResult HexLiteral()
		{
			base.TakeUntil((char c) => !ParserHelpers.IsHexDigit(c));
			this.TakeIntegerSuffix();
			return base.Stay(base.EndSymbol(CSharpSymbolType.IntegerLiteral));
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00014A60 File Offset: 0x00012C60
		private StateMachine<CSharpSymbol>.StateResult DecimalLiteral()
		{
			base.TakeUntil((char c) => !char.IsDigit(c));
			if (base.CurrentCharacter == '.' && char.IsDigit(base.Peek()))
			{
				return this.RealLiteral();
			}
			if (CSharpHelpers.IsRealLiteralSuffix(base.CurrentCharacter) || base.CurrentCharacter == 'E' || base.CurrentCharacter == 'e')
			{
				return this.RealLiteralExponentPart();
			}
			this.TakeIntegerSuffix();
			return base.Stay(base.EndSymbol(CSharpSymbolType.IntegerLiteral));
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00014AF8 File Offset: 0x00012CF8
		private StateMachine<CSharpSymbol>.StateResult RealLiteralExponentPart()
		{
			if (base.CurrentCharacter == 'E' || base.CurrentCharacter == 'e')
			{
				base.TakeCurrent();
				if (base.CurrentCharacter == '+' || base.CurrentCharacter == '-')
				{
					base.TakeCurrent();
				}
				base.TakeUntil((char c) => !char.IsDigit(c));
			}
			if (CSharpHelpers.IsRealLiteralSuffix(base.CurrentCharacter))
			{
				base.TakeCurrent();
			}
			return base.Stay(base.EndSymbol(CSharpSymbolType.RealLiteral));
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00014B88 File Offset: 0x00012D88
		private StateMachine<CSharpSymbol>.StateResult RealLiteral()
		{
			base.TakeCurrent();
			base.TakeUntil((char c) => !char.IsDigit(c));
			return this.RealLiteralExponentPart();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00014BBC File Offset: 0x00012DBC
		private void TakeIntegerSuffix()
		{
			if (char.ToLowerInvariant(base.CurrentCharacter) == 'u')
			{
				base.TakeCurrent();
				if (char.ToLowerInvariant(base.CurrentCharacter) == 'l')
				{
					base.TakeCurrent();
					return;
				}
			}
			else if (char.ToLowerInvariant(base.CurrentCharacter) == 'l')
			{
				base.TakeCurrent();
				if (char.ToLowerInvariant(base.CurrentCharacter) == 'u')
				{
					base.TakeCurrent();
				}
			}
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00014C2C File Offset: 0x00012E2C
		private StateMachine<CSharpSymbol>.StateResult Identifier()
		{
			base.TakeCurrent();
			base.TakeUntil((char c) => !CSharpHelpers.IsIdentifierPart(c));
			CSharpSymbol output = null;
			if (base.HaveContent)
			{
				CSharpKeyword? keyword = CSharpKeywordDetector.SymbolTypeForIdentifier(base.Buffer.ToString());
				CSharpSymbolType type = CSharpSymbolType.Identifier;
				if (keyword != null)
				{
					type = CSharpSymbolType.Keyword;
				}
				output = new CSharpSymbol(base.CurrentStart, base.Buffer.ToString(), type)
				{
					Keyword = keyword
				};
			}
			base.StartSymbol();
			return base.Stay(output);
		}

		// Token: 0x04000171 RID: 369
		private Dictionary<char, Func<CSharpSymbolType>> _operatorHandlers;
	}
}

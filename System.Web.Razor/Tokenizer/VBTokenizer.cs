using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Razor.Parser;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer.Symbols;

namespace System.Web.Razor.Tokenizer
{
	// Token: 0x02000082 RID: 130
	public class VBTokenizer : Tokenizer<VBSymbol, VBSymbolType>
	{
		// Token: 0x0600057A RID: 1402 RVA: 0x000157D0 File Offset: 0x000139D0
		public VBTokenizer(ITextDocument source) : base(source)
		{
			base.CurrentState = new StateMachine<VBSymbol>.State(this.Data);
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x000157EB File Offset: 0x000139EB
		protected override StateMachine<VBSymbol>.State StartState
		{
			get
			{
				return new StateMachine<VBSymbol>.State(this.Data);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x000157F9 File Offset: 0x000139F9
		public override VBSymbolType RazorCommentType
		{
			get
			{
				return VBSymbolType.RazorComment;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x000157FD File Offset: 0x000139FD
		public override VBSymbolType RazorCommentTransitionType
		{
			get
			{
				return VBSymbolType.RazorCommentTransition;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00015801 File Offset: 0x00013A01
		public override VBSymbolType RazorCommentStarType
		{
			get
			{
				return VBSymbolType.RazorCommentStar;
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00015994 File Offset: 0x00013B94
		internal static IEnumerable<VBSymbol> Tokenize(string content)
		{
			using (SeekableTextReader reader = new SeekableTextReader(content))
			{
				VBTokenizer tok = new VBTokenizer(reader);
				VBSymbol sym;
				while ((sym = tok.NextSymbol()) != null)
				{
					yield return sym;
				}
			}
			yield break;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000159B1 File Offset: 0x00013BB1
		protected override VBSymbol CreateSymbol(SourceLocation start, string content, VBSymbolType type, IEnumerable<RazorError> errors)
		{
			return new VBSymbol(start, content, type, errors);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000159EC File Offset: 0x00013BEC
		private StateMachine<VBSymbol>.StateResult Data()
		{
			if (ParserHelpers.IsNewLine(base.CurrentCharacter))
			{
				bool flag = base.CurrentCharacter == '\r';
				base.TakeCurrent();
				if (flag && base.CurrentCharacter == '\n')
				{
					base.TakeCurrent();
				}
				return base.Stay(base.EndSymbol(VBSymbolType.NewLine));
			}
			if (ParserHelpers.IsWhitespace(base.CurrentCharacter))
			{
				base.TakeUntil((char c) => !ParserHelpers.IsWhitespace(c));
				return base.Stay(base.EndSymbol(VBSymbolType.WhiteSpace));
			}
			if (VBHelpers.IsSingleQuote(base.CurrentCharacter))
			{
				base.TakeCurrent();
				return this.CommentBody();
			}
			if (this.IsIdentifierStart())
			{
				return this.Identifier();
			}
			if (char.IsDigit(base.CurrentCharacter))
			{
				return this.DecimalLiteral();
			}
			if (base.CurrentCharacter == '&')
			{
				char c2 = char.ToLower(base.Peek(), CultureInfo.InvariantCulture);
				if (c2 == 'h')
				{
					return this.HexLiteral();
				}
				if (c2 == 'o')
				{
					return this.OctLiteral();
				}
			}
			else
			{
				if (base.CurrentCharacter == '.' && char.IsDigit(base.Peek()))
				{
					return this.FloatingPointLiteralEnd();
				}
				if (VBHelpers.IsDoubleQuote(base.CurrentCharacter))
				{
					base.TakeCurrent();
					return base.Transition(new StateMachine<VBSymbol>.State(this.QuotedLiteral));
				}
				if (this.AtDateLiteral())
				{
					return this.DateLiteral();
				}
				if (base.CurrentCharacter == '@')
				{
					base.TakeCurrent();
					if (base.CurrentCharacter == '*')
					{
						return base.Transition(base.EndSymbol(VBSymbolType.RazorCommentTransition), new StateMachine<VBSymbol>.State(base.AfterRazorCommentTransition));
					}
					if (base.CurrentCharacter == '@')
					{
						return base.Transition(base.EndSymbol(VBSymbolType.Transition), delegate()
						{
							base.TakeCurrent();
							return base.Transition(base.EndSymbol(VBSymbolType.Transition), new StateMachine<VBSymbol>.State(this.Data));
						});
					}
					return base.Stay(base.EndSymbol(VBSymbolType.Transition));
				}
			}
			return base.Stay(base.EndSymbol(this.Operator()));
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00015BD0 File Offset: 0x00013DD0
		private StateMachine<VBSymbol>.StateResult DateLiteral()
		{
			base.TakeCurrent();
			base.TakeUntil((char c) => c == '#' || ParserHelpers.IsNewLine(c));
			if (base.CurrentCharacter == '#')
			{
				base.TakeCurrent();
			}
			return base.Stay(base.EndSymbol(VBSymbolType.DateLiteral));
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00015C28 File Offset: 0x00013E28
		private bool AtDateLiteral()
		{
			if (base.CurrentCharacter != '#')
			{
				return false;
			}
			int position = base.Source.Position;
			bool result;
			try
			{
				base.MoveNext();
				while (ParserHelpers.IsWhitespace(base.CurrentCharacter))
				{
					base.MoveNext();
				}
				result = char.IsDigit(base.CurrentCharacter);
			}
			finally
			{
				base.Source.Position = position;
			}
			return result;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00015CA8 File Offset: 0x00013EA8
		private StateMachine<VBSymbol>.StateResult QuotedLiteral()
		{
			base.TakeUntil((char c) => VBHelpers.IsDoubleQuote(c) || ParserHelpers.IsNewLine(c));
			if (VBHelpers.IsDoubleQuote(base.CurrentCharacter))
			{
				base.TakeCurrent();
				if (VBHelpers.IsDoubleQuote(base.CurrentCharacter))
				{
					base.TakeCurrent();
					return base.Stay();
				}
			}
			VBSymbolType type = VBSymbolType.StringLiteral;
			if (char.ToLowerInvariant(base.CurrentCharacter) == 'c')
			{
				base.TakeCurrent();
				type = VBSymbolType.CharacterLiteral;
			}
			return base.Transition(base.EndSymbol(type), new StateMachine<VBSymbol>.State(this.Data));
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00015D48 File Offset: 0x00013F48
		private StateMachine<VBSymbol>.StateResult DecimalLiteral()
		{
			base.TakeUntil((char c) => !char.IsDigit(c));
			char c2 = char.ToLowerInvariant(base.CurrentCharacter);
			if (VBTokenizer.IsFloatTypeSuffix(c2) || c2 == '.' || c2 == 'e')
			{
				return this.FloatingPointLiteralEnd();
			}
			this.TakeIntTypeSuffix();
			return base.Stay(base.EndSymbol(VBSymbolType.IntegerLiteral));
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00015DB1 File Offset: 0x00013FB1
		private static bool IsFloatTypeSuffix(char chr)
		{
			chr = char.ToLowerInvariant(chr);
			return chr == 'f' || chr == 'r' || chr == 'd';
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00015DE4 File Offset: 0x00013FE4
		private StateMachine<VBSymbol>.StateResult FloatingPointLiteralEnd()
		{
			if (base.CurrentCharacter == '.')
			{
				base.TakeCurrent();
				base.TakeUntil((char c) => !char.IsDigit(c));
			}
			if (char.ToLowerInvariant(base.CurrentCharacter) == 'e')
			{
				base.TakeCurrent();
				if (base.CurrentCharacter == '+' || base.CurrentCharacter == '-')
				{
					base.TakeCurrent();
				}
				base.TakeUntil((char c) => !char.IsDigit(c));
			}
			if (VBTokenizer.IsFloatTypeSuffix(base.CurrentCharacter))
			{
				base.TakeCurrent();
			}
			return base.Stay(base.EndSymbol(VBSymbolType.FloatingPointLiteral));
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00015EA4 File Offset: 0x000140A4
		private StateMachine<VBSymbol>.StateResult HexLiteral()
		{
			base.TakeCurrent();
			base.TakeCurrent();
			base.TakeUntil((char c) => !ParserHelpers.IsHexDigit(c));
			this.TakeIntTypeSuffix();
			return base.Stay(base.EndSymbol(VBSymbolType.IntegerLiteral));
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00015F00 File Offset: 0x00014100
		private StateMachine<VBSymbol>.StateResult OctLiteral()
		{
			base.TakeCurrent();
			base.TakeCurrent();
			base.TakeUntil((char c) => !VBHelpers.IsOctalDigit(c));
			this.TakeIntTypeSuffix();
			return base.Stay(base.EndSymbol(VBSymbolType.IntegerLiteral));
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00015F50 File Offset: 0x00014150
		private VBSymbolType Operator()
		{
			char currentCharacter = base.CurrentCharacter;
			base.TakeCurrent();
			VBSymbolType result;
			if (VBTokenizer._operatorTable.TryGetValue(currentCharacter, out result))
			{
				return result;
			}
			return VBSymbolType.Unknown;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00015F7C File Offset: 0x0001417C
		private void TakeIntTypeSuffix()
		{
			if (char.ToLowerInvariant(base.CurrentCharacter) == 'u')
			{
				base.TakeCurrent();
			}
			if (VBTokenizer.IsIntegerSuffix(base.CurrentCharacter))
			{
				base.TakeCurrent();
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00015FA6 File Offset: 0x000141A6
		private static bool IsIntegerSuffix(char chr)
		{
			chr = char.ToLowerInvariant(chr);
			return chr == 's' || chr == 'i' || chr == 'l';
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00015FC1 File Offset: 0x000141C1
		private StateMachine<VBSymbol>.StateResult CommentBody()
		{
			base.TakeUntil(new Func<char, bool>(ParserHelpers.IsNewLine));
			return base.Stay(base.EndSymbol(VBSymbolType.Comment));
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00015FF0 File Offset: 0x000141F0
		private StateMachine<VBSymbol>.StateResult Identifier()
		{
			bool flag = false;
			if (base.CurrentCharacter == '[')
			{
				base.TakeCurrent();
				flag = true;
			}
			base.TakeUntil((char c) => !ParserHelpers.IsIdentifierPart(c));
			if (flag && base.CurrentCharacter == ']')
			{
				base.TakeCurrent();
			}
			VBKeyword? keyword = VBKeywordDetector.GetKeyword(base.Buffer.ToString());
			if (keyword == VBKeyword.Rem)
			{
				return this.CommentBody();
			}
			VBSymbol output = new VBSymbol(base.CurrentStart, base.Buffer.ToString(), (keyword == null) ? VBSymbolType.Identifier : VBSymbolType.Keyword)
			{
				Keyword = keyword
			};
			base.StartSymbol();
			return base.Stay(output);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000160B4 File Offset: 0x000142B4
		private bool IsIdentifierStart()
		{
			if (base.CurrentCharacter == '_')
			{
				return ParserHelpers.IsIdentifierPart(base.Peek());
			}
			if (base.CurrentCharacter == '[')
			{
				return ParserHelpers.IsIdentifierPart(base.Peek());
			}
			return ParserHelpers.IsIdentifierStart(base.CurrentCharacter);
		}

		// Token: 0x040002F1 RID: 753
		private static Dictionary<char, VBSymbolType> _operatorTable = new Dictionary<char, VBSymbolType>
		{
			{
				'_',
				VBSymbolType.LineContinuation
			},
			{
				'(',
				VBSymbolType.LeftParenthesis
			},
			{
				')',
				VBSymbolType.RightParenthesis
			},
			{
				'[',
				VBSymbolType.LeftBracket
			},
			{
				']',
				VBSymbolType.RightBracket
			},
			{
				'{',
				VBSymbolType.LeftBrace
			},
			{
				'}',
				VBSymbolType.RightBrace
			},
			{
				'!',
				VBSymbolType.Bang
			},
			{
				'#',
				VBSymbolType.Hash
			},
			{
				',',
				VBSymbolType.Comma
			},
			{
				'.',
				VBSymbolType.Dot
			},
			{
				':',
				VBSymbolType.Colon
			},
			{
				'?',
				VBSymbolType.QuestionMark
			},
			{
				'&',
				VBSymbolType.Concatenation
			},
			{
				'*',
				VBSymbolType.Multiply
			},
			{
				'+',
				VBSymbolType.Add
			},
			{
				'-',
				VBSymbolType.Subtract
			},
			{
				'/',
				VBSymbolType.Divide
			},
			{
				'\\',
				VBSymbolType.IntegerDivide
			},
			{
				'^',
				VBSymbolType.Exponentiation
			},
			{
				'=',
				VBSymbolType.Equal
			},
			{
				'<',
				VBSymbolType.LessThan
			},
			{
				'>',
				VBSymbolType.GreaterThan
			},
			{
				'$',
				VBSymbolType.Dollar
			}
		};
	}
}

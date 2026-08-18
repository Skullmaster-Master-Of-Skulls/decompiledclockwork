using System;
using System.Collections.Generic;
using System.Web.Razor.Parser;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer.Symbols;

namespace System.Web.Razor.Tokenizer
{
	// Token: 0x02000078 RID: 120
	public class HtmlTokenizer : Tokenizer<HtmlSymbol, HtmlSymbolType>
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x00014CB9 File Offset: 0x00012EB9
		public HtmlTokenizer(ITextDocument source) : base(source)
		{
			base.CurrentState = new StateMachine<HtmlSymbol>.State(this.Data);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00014CD4 File Offset: 0x00012ED4
		protected override StateMachine<HtmlSymbol>.State StartState
		{
			get
			{
				return new StateMachine<HtmlSymbol>.State(this.Data);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00014CE2 File Offset: 0x00012EE2
		public override HtmlSymbolType RazorCommentType
		{
			get
			{
				return HtmlSymbolType.RazorComment;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00014CE6 File Offset: 0x00012EE6
		public override HtmlSymbolType RazorCommentTransitionType
		{
			get
			{
				return HtmlSymbolType.RazorCommentTransition;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00014CEA File Offset: 0x00012EEA
		public override HtmlSymbolType RazorCommentStarType
		{
			get
			{
				return HtmlSymbolType.RazorCommentStar;
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00014E7C File Offset: 0x0001307C
		internal static IEnumerable<HtmlSymbol> Tokenize(string content)
		{
			using (SeekableTextReader reader = new SeekableTextReader(content))
			{
				HtmlTokenizer tok = new HtmlTokenizer(reader);
				HtmlSymbol sym;
				while ((sym = tok.NextSymbol()) != null)
				{
					yield return sym;
				}
			}
			yield break;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00014E99 File Offset: 0x00013099
		protected override HtmlSymbol CreateSymbol(SourceLocation start, string content, HtmlSymbolType type, IEnumerable<RazorError> errors)
		{
			return new HtmlSymbol(start, content, type, errors);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00014EC8 File Offset: 0x000130C8
		private StateMachine<HtmlSymbol>.StateResult Data()
		{
			if (ParserHelpers.IsWhitespace(base.CurrentCharacter))
			{
				return base.Stay(this.Whitespace());
			}
			if (ParserHelpers.IsNewLine(base.CurrentCharacter))
			{
				return base.Stay(this.Newline());
			}
			if (base.CurrentCharacter == '@')
			{
				base.TakeCurrent();
				if (base.CurrentCharacter == '*')
				{
					return base.Transition(base.EndSymbol(HtmlSymbolType.RazorCommentTransition), new StateMachine<HtmlSymbol>.State(base.AfterRazorCommentTransition));
				}
				if (base.CurrentCharacter == '@')
				{
					return base.Transition(base.EndSymbol(HtmlSymbolType.Transition), delegate()
					{
						base.TakeCurrent();
						return base.Transition(base.EndSymbol(HtmlSymbolType.Transition), new StateMachine<HtmlSymbol>.State(this.Data));
					});
				}
				return base.Stay(base.EndSymbol(HtmlSymbolType.Transition));
			}
			else
			{
				if (this.AtSymbol())
				{
					return base.Stay(this.Symbol());
				}
				return base.Transition(new StateMachine<HtmlSymbol>.State(this.Text));
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00014FA0 File Offset: 0x000131A0
		private StateMachine<HtmlSymbol>.StateResult Text()
		{
			char value = '\0';
			while (!base.EndOfFile && !ParserHelpers.IsWhitespaceOrNewLine(base.CurrentCharacter) && !this.AtSymbol())
			{
				value = base.CurrentCharacter;
				base.TakeCurrent();
			}
			if (base.CurrentCharacter == '@')
			{
				char value2 = base.Peek();
				if (ParserHelpers.IsLetterOrDecimalDigit(value) && ParserHelpers.IsLetterOrDecimalDigit(value2))
				{
					base.TakeCurrent();
					return base.Stay();
				}
			}
			return base.Transition(base.EndSymbol(HtmlSymbolType.Text), new StateMachine<HtmlSymbol>.State(this.Data));
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00015024 File Offset: 0x00013224
		private HtmlSymbol Symbol()
		{
			char currentCharacter = base.CurrentCharacter;
			base.TakeCurrent();
			char c = currentCharacter;
			if (c <= '\'')
			{
				switch (c)
				{
				case '!':
					return base.EndSymbol(HtmlSymbolType.Bang);
				case '"':
					return base.EndSymbol(HtmlSymbolType.DoubleQuote);
				default:
					if (c == '\'')
					{
						return base.EndSymbol(HtmlSymbolType.SingleQuote);
					}
					break;
				}
			}
			else
			{
				switch (c)
				{
				case '-':
					base.TakeCurrent();
					return base.EndSymbol(HtmlSymbolType.DoubleHyphen);
				case '.':
					break;
				case '/':
					return base.EndSymbol(HtmlSymbolType.Solidus);
				default:
					switch (c)
					{
					case '<':
						return base.EndSymbol(HtmlSymbolType.OpenAngle);
					case '=':
						return base.EndSymbol(HtmlSymbolType.Equals);
					case '>':
						return base.EndSymbol(HtmlSymbolType.CloseAngle);
					case '?':
						return base.EndSymbol(HtmlSymbolType.QuestionMark);
					default:
						switch (c)
						{
						case '[':
							return base.EndSymbol(HtmlSymbolType.LeftBracket);
						case ']':
							return base.EndSymbol(HtmlSymbolType.RightBracket);
						}
						break;
					}
					break;
				}
			}
			return base.EndSymbol(HtmlSymbolType.Unknown);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00015113 File Offset: 0x00013313
		private HtmlSymbol Whitespace()
		{
			while (ParserHelpers.IsWhitespace(base.CurrentCharacter))
			{
				base.TakeCurrent();
			}
			return base.EndSymbol(HtmlSymbolType.WhiteSpace);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00015134 File Offset: 0x00013334
		private HtmlSymbol Newline()
		{
			bool flag = base.CurrentCharacter == '\r';
			base.TakeCurrent();
			if (flag && base.CurrentCharacter == '\n')
			{
				base.TakeCurrent();
			}
			return base.EndSymbol(HtmlSymbolType.NewLine);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001516C File Offset: 0x0001336C
		private bool AtSymbol()
		{
			return base.CurrentCharacter == '<' || base.CurrentCharacter == '<' || base.CurrentCharacter == '!' || base.CurrentCharacter == '/' || base.CurrentCharacter == '?' || base.CurrentCharacter == '[' || base.CurrentCharacter == '>' || base.CurrentCharacter == ']' || base.CurrentCharacter == '=' || base.CurrentCharacter == '"' || base.CurrentCharacter == '\'' || base.CurrentCharacter == '@' || (base.CurrentCharacter == '-' && base.Peek() == '-');
		}

		// Token: 0x04000185 RID: 389
		private const char TransitionChar = '@';
	}
}

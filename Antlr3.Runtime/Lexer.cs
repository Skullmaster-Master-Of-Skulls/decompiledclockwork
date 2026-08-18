using System;
using System.Diagnostics;

namespace Antlr.Runtime
{
	// Token: 0x02000024 RID: 36
	public abstract class Lexer : BaseRecognizer, ITokenSource
	{
		// Token: 0x060001AA RID: 426 RVA: 0x000055CE File Offset: 0x000037CE
		public Lexer()
		{
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000055D6 File Offset: 0x000037D6
		public Lexer(ICharStream input)
		{
			this.input = input;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000055E5 File Offset: 0x000037E5
		public Lexer(ICharStream input, RecognizerSharedState state) : base(state)
		{
			this.input = input;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000055F8 File Offset: 0x000037F8
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00005646 File Offset: 0x00003846
		public string Text
		{
			get
			{
				if (this.state.text != null)
				{
					return this.state.text;
				}
				return this.input.Substring(this.state.tokenStartCharIndex, this.CharIndex - this.state.tokenStartCharIndex);
			}
			set
			{
				this.state.text = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00005654 File Offset: 0x00003854
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00005661 File Offset: 0x00003861
		public int Line
		{
			get
			{
				return this.input.Line;
			}
			set
			{
				this.input.Line = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000566F File Offset: 0x0000386F
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000567C File Offset: 0x0000387C
		public int CharPositionInLine
		{
			get
			{
				return this.input.CharPositionInLine;
			}
			set
			{
				this.input.CharPositionInLine = value;
			}
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000568C File Offset: 0x0000388C
		public override void Reset()
		{
			base.Reset();
			if (this.input != null)
			{
				this.input.Seek(0);
			}
			if (this.state == null)
			{
				return;
			}
			this.state.token = null;
			this.state.type = 0;
			this.state.channel = 0;
			this.state.tokenStartCharIndex = -1;
			this.state.tokenStartCharPositionInLine = -1;
			this.state.tokenStartLine = -1;
			this.state.text = null;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005710 File Offset: 0x00003910
		public virtual IToken NextToken()
		{
			for (;;)
			{
				this.state.token = null;
				this.state.channel = 0;
				this.state.tokenStartCharIndex = this.input.Index;
				this.state.tokenStartCharPositionInLine = this.input.CharPositionInLine;
				this.state.tokenStartLine = this.input.Line;
				this.state.text = null;
				if (this.input.LA(1) == -1)
				{
					break;
				}
				IToken token;
				try
				{
					this.ParseNextToken();
					if (this.state.token == null)
					{
						this.Emit();
					}
					else if (this.state.token == Tokens.Skip)
					{
						continue;
					}
					token = this.state.token;
				}
				catch (MismatchedRangeException e)
				{
					this.ReportError(e);
					continue;
				}
				catch (MismatchedTokenException e2)
				{
					this.ReportError(e2);
					continue;
				}
				catch (RecognitionException ex)
				{
					this.ReportError(ex);
					this.Recover(ex);
					continue;
				}
				return token;
			}
			return this.GetEndOfFileToken();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00005834 File Offset: 0x00003A34
		public virtual IToken GetEndOfFileToken()
		{
			return new CommonToken(this.input, -1, 0, this.input.Index, this.input.Index)
			{
				Line = this.Line,
				CharPositionInLine = this.CharPositionInLine
			};
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000587E File Offset: 0x00003A7E
		public virtual void Skip()
		{
			this.state.token = Tokens.Skip;
		}

		// Token: 0x060001B7 RID: 439
		public abstract void mTokens();

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00005890 File Offset: 0x00003A90
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00005898 File Offset: 0x00003A98
		public virtual ICharStream CharStream
		{
			get
			{
				return this.input;
			}
			set
			{
				this.input = null;
				this.Reset();
				this.input = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000058AE File Offset: 0x00003AAE
		public override string SourceName
		{
			get
			{
				return this.input.SourceName;
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000058BB File Offset: 0x00003ABB
		public virtual void Emit(IToken token)
		{
			this.state.token = token;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000058CC File Offset: 0x00003ACC
		public virtual IToken Emit()
		{
			IToken token = new CommonToken(this.input, this.state.type, this.state.channel, this.state.tokenStartCharIndex, this.CharIndex - 1);
			token.Line = this.state.tokenStartLine;
			token.Text = this.state.text;
			token.CharPositionInLine = this.state.tokenStartCharPositionInLine;
			this.Emit(token);
			return token;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000594C File Offset: 0x00003B4C
		public virtual void Match(string s)
		{
			int i = 0;
			while (i < s.Length)
			{
				if (this.input.LA(1) != (int)s[i])
				{
					if (this.state.backtracking > 0)
					{
						this.state.failed = true;
						return;
					}
					MismatchedTokenException ex = new MismatchedTokenException((int)s[i], this.input, this.TokenNames);
					this.Recover(ex);
					throw ex;
				}
				else
				{
					i++;
					this.input.Consume();
					this.state.failed = false;
				}
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000059D3 File Offset: 0x00003BD3
		public virtual void MatchAny()
		{
			this.input.Consume();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000059E0 File Offset: 0x00003BE0
		public virtual void Match(int c)
		{
			if (this.input.LA(1) == c)
			{
				this.input.Consume();
				this.state.failed = false;
				return;
			}
			if (this.state.backtracking > 0)
			{
				this.state.failed = true;
				return;
			}
			MismatchedTokenException ex = new MismatchedTokenException(c, this.input, this.TokenNames);
			this.Recover(ex);
			throw ex;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00005A4C File Offset: 0x00003C4C
		public virtual void MatchRange(int a, int b)
		{
			if (this.input.LA(1) >= a && this.input.LA(1) <= b)
			{
				this.input.Consume();
				this.state.failed = false;
				return;
			}
			if (this.state.backtracking > 0)
			{
				this.state.failed = true;
				return;
			}
			MismatchedRangeException ex = new MismatchedRangeException(a, b, this.input);
			this.Recover(ex);
			throw ex;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00005AC0 File Offset: 0x00003CC0
		public virtual int CharIndex
		{
			get
			{
				return this.input.Index;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00005ACD File Offset: 0x00003CCD
		public override void ReportError(RecognitionException e)
		{
			this.DisplayRecognitionError(this.TokenNames, e);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005ADC File Offset: 0x00003CDC
		public override string GetErrorMessage(RecognitionException e, string[] tokenNames)
		{
			string result;
			if (e is MismatchedTokenException)
			{
				MismatchedTokenException ex = (MismatchedTokenException)e;
				result = "mismatched character " + this.GetCharErrorDisplay(e.Character) + " expecting " + this.GetCharErrorDisplay(ex.Expecting);
			}
			else if (e is NoViableAltException)
			{
				NoViableAltException ex2 = (NoViableAltException)e;
				result = "no viable alternative at character " + this.GetCharErrorDisplay(e.Character);
			}
			else if (e is EarlyExitException)
			{
				EarlyExitException ex3 = (EarlyExitException)e;
				result = "required (...)+ loop did not match anything at character " + this.GetCharErrorDisplay(e.Character);
			}
			else if (e is MismatchedNotSetException)
			{
				MismatchedNotSetException ex4 = (MismatchedNotSetException)e;
				result = string.Concat(new object[]
				{
					"mismatched character ",
					this.GetCharErrorDisplay(e.Character),
					" expecting set ",
					ex4.Expecting
				});
			}
			else if (e is MismatchedSetException)
			{
				MismatchedSetException ex5 = (MismatchedSetException)e;
				result = string.Concat(new object[]
				{
					"mismatched character ",
					this.GetCharErrorDisplay(e.Character),
					" expecting set ",
					ex5.Expecting
				});
			}
			else if (e is MismatchedRangeException)
			{
				MismatchedRangeException ex6 = (MismatchedRangeException)e;
				result = string.Concat(new string[]
				{
					"mismatched character ",
					this.GetCharErrorDisplay(e.Character),
					" expecting set ",
					this.GetCharErrorDisplay(ex6.A),
					"..",
					this.GetCharErrorDisplay(ex6.B)
				});
			}
			else
			{
				result = base.GetErrorMessage(e, tokenNames);
			}
			return result;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00005C94 File Offset: 0x00003E94
		public virtual string GetCharErrorDisplay(int c)
		{
			string str = ((char)c).ToString();
			if (c != -1)
			{
				switch (c)
				{
				case 9:
					str = "\\t";
					break;
				case 10:
					str = "\\n";
					break;
				case 13:
					str = "\\r";
					break;
				}
			}
			else
			{
				str = "<EOF>";
			}
			return "'" + str + "'";
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005CFF File Offset: 0x00003EFF
		public virtual void Recover(RecognitionException re)
		{
			this.input.Consume();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00005D0C File Offset: 0x00003F0C
		[Conditional("ANTLR_TRACE")]
		public virtual void TraceIn(string ruleName, int ruleIndex)
		{
			string inputSymbol = string.Concat(new object[]
			{
				(char)this.input.LT(1),
				" line=",
				this.Line,
				":",
				this.CharPositionInLine
			});
			base.TraceIn(ruleName, ruleIndex, inputSymbol);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005D74 File Offset: 0x00003F74
		[Conditional("ANTLR_TRACE")]
		public virtual void TraceOut(string ruleName, int ruleIndex)
		{
			string inputSymbol = string.Concat(new object[]
			{
				(char)this.input.LT(1),
				" line=",
				this.Line,
				":",
				this.CharPositionInLine
			});
			base.TraceOut(ruleName, ruleIndex, inputSymbol);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005DD9 File Offset: 0x00003FD9
		protected virtual void ParseNextToken()
		{
			this.mTokens();
		}

		// Token: 0x04000056 RID: 86
		protected ICharStream input;
	}
}

using System;

namespace Telerik.Web.Apoc.Fo.Expr
{
	// Token: 0x020013BF RID: 5055
	internal class PropertyTokenizer
	{
		// Token: 0x0600D17B RID: 53627 RVA: 0x002E50E5 File Offset: 0x002E32E5
		protected PropertyTokenizer(string s)
		{
			this.expr = s;
			this.exprLength = s.Length;
		}

		// Token: 0x0600D17C RID: 53628 RVA: 0x002E5100 File Offset: 0x002E3300
		protected void next()
		{
			this.currentTokenValue = null;
			this.currentTokenStartIndex = this.exprIndex;
			this.recognizeOperator = true;
			while (this.exprIndex < this.exprLength)
			{
				char c = this.expr[this.exprIndex++];
				char c2 = c;
				switch (c2)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				case '\v':
				case '\f':
					goto IL_427;
				default:
					switch (c2)
					{
					case ' ':
						break;
					case '!':
					case '$':
					case '%':
					case '&':
					case '/':
						goto IL_427;
					case '"':
					case '\'':
						this.exprIndex = this.expr.IndexOf(c, this.exprIndex);
						if (this.exprIndex < 0)
						{
							this.exprIndex = this.currentTokenStartIndex + 1;
							throw new PropertyException("missing quote");
						}
						this.currentTokenValue = this.expr.Substring(this.currentTokenStartIndex + 1, this.exprIndex++ - (this.currentTokenStartIndex + 1));
						this.currentToken = 5;
						return;
					case '#':
						if (this.exprIndex < this.exprLength && PropertyTokenizer.isHexDigit(this.expr[this.exprIndex]))
						{
							this.exprIndex++;
							this.scanHexDigits();
							this.currentToken = 15;
							this.currentTokenValue = this.expr.Substring(this.currentTokenStartIndex, this.exprIndex - this.currentTokenStartIndex);
							return;
						}
						throw new PropertyException("illegal character '#'");
					case '(':
						this.currentToken = 3;
						this.recognizeOperator = false;
						return;
					case ')':
						this.currentToken = 4;
						return;
					case '*':
						this.currentToken = 2;
						return;
					case '+':
						this.recognizeOperator = false;
						this.currentToken = 8;
						return;
					case ',':
						this.recognizeOperator = false;
						this.currentToken = 13;
						return;
					case '-':
						this.recognizeOperator = false;
						this.currentToken = 9;
						return;
					case '.':
						if (this.exprIndex < this.exprLength && PropertyTokenizer.isDigit(this.expr[this.exprIndex]))
						{
							this.exprIndex++;
							this.scanDigits();
							if (this.exprIndex < this.exprLength && this.expr[this.exprIndex] == '%')
							{
								this.exprIndex++;
								this.currentToken = 14;
							}
							else
							{
								this.currentUnitLength = this.exprIndex;
								this.scanName();
								this.currentUnitLength = this.exprIndex - this.currentUnitLength;
								this.currentToken = ((this.currentUnitLength > 0) ? 12 : 16);
							}
							this.currentTokenValue = this.expr.Substring(this.currentTokenStartIndex, this.exprIndex - this.currentTokenStartIndex);
							return;
						}
						throw new PropertyException("illegal character '.'");
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
					{
						this.scanDigits();
						bool flag;
						if (this.exprIndex < this.exprLength && this.expr[this.exprIndex] == '.')
						{
							this.exprIndex++;
							flag = true;
							if (this.exprIndex < this.exprLength && PropertyTokenizer.isDigit(this.expr[this.exprIndex]))
							{
								this.exprIndex++;
								this.scanDigits();
							}
						}
						else
						{
							flag = false;
						}
						if (this.exprIndex < this.exprLength && this.expr[this.exprIndex] == '%')
						{
							this.exprIndex++;
							this.currentToken = 14;
						}
						else
						{
							this.currentUnitLength = this.exprIndex;
							this.scanName();
							this.currentUnitLength = this.exprIndex - this.currentUnitLength;
							this.currentToken = ((this.currentUnitLength > 0) ? 12 : (flag ? 16 : 17));
						}
						this.currentTokenValue = this.expr.Substring(this.currentTokenStartIndex, this.exprIndex - this.currentTokenStartIndex);
						return;
					}
					default:
						goto IL_427;
					}
					break;
				}
				this.currentTokenStartIndex = this.exprIndex;
				continue;
				IL_427:
				this.exprIndex--;
				this.scanName();
				if (this.exprIndex == this.currentTokenStartIndex)
				{
					throw new PropertyException("illegal character");
				}
				this.currentTokenValue = this.expr.Substring(this.currentTokenStartIndex, this.exprIndex - this.currentTokenStartIndex);
				if (this.currentTokenValue.Equals("mod"))
				{
					this.currentToken = 10;
					return;
				}
				if (this.currentTokenValue.Equals("div"))
				{
					this.currentToken = 11;
					return;
				}
				if (this.followingParen())
				{
					this.currentToken = 7;
					this.recognizeOperator = false;
					return;
				}
				this.currentToken = 1;
				this.recognizeOperator = false;
				return;
			}
			this.currentToken = 0;
		}

		// Token: 0x0600D17D RID: 53629 RVA: 0x002E55E0 File Offset: 0x002E37E0
		private void scanName()
		{
			if (this.exprIndex < this.exprLength && PropertyTokenizer.isNameStartChar(this.expr[this.exprIndex]))
			{
				while (++this.exprIndex < this.exprLength && PropertyTokenizer.isNameChar(this.expr[this.exprIndex]))
				{
				}
			}
		}

		// Token: 0x0600D17E RID: 53630 RVA: 0x002E5644 File Offset: 0x002E3844
		private void scanDigits()
		{
			while (this.exprIndex < this.exprLength && PropertyTokenizer.isDigit(this.expr[this.exprIndex]))
			{
				this.exprIndex++;
			}
		}

		// Token: 0x0600D17F RID: 53631 RVA: 0x002E567C File Offset: 0x002E387C
		private void scanHexDigits()
		{
			while (this.exprIndex < this.exprLength && PropertyTokenizer.isHexDigit(this.expr[this.exprIndex]))
			{
				this.exprIndex++;
			}
		}

		// Token: 0x0600D180 RID: 53632 RVA: 0x002E56B4 File Offset: 0x002E38B4
		private bool followingParen()
		{
			for (int i = this.exprIndex; i < this.exprLength; i++)
			{
				char c = this.expr[i];
				switch (c)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				case '\v':
				case '\f':
					return false;
				default:
					if (c != ' ')
					{
						if (c == '(')
						{
							this.exprIndex = i + 1;
							return true;
						}
						return false;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x0600D181 RID: 53633 RVA: 0x002E5719 File Offset: 0x002E3919
		private static bool isDigit(char c)
		{
			return "0123456789".IndexOf(c) >= 0;
		}

		// Token: 0x0600D182 RID: 53634 RVA: 0x002E572C File Offset: 0x002E392C
		private static bool isHexDigit(char c)
		{
			return "0123456789abcdefABCDEF".IndexOf(c) >= 0;
		}

		// Token: 0x0600D183 RID: 53635 RVA: 0x002E5740 File Offset: 0x002E3940
		private static bool isSpace(char c)
		{
			switch (c)
			{
			case '\t':
			case '\n':
			case '\r':
				break;
			case '\v':
			case '\f':
				return false;
			default:
				if (c != ' ')
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x0600D184 RID: 53636 RVA: 0x002E5774 File Offset: 0x002E3974
		private static bool isNameStartChar(char c)
		{
			return "_abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(c) >= 0 || c >= '\u0080';
		}

		// Token: 0x0600D185 RID: 53637 RVA: 0x002E5791 File Offset: 0x002E3991
		private static bool isNameChar(char c)
		{
			return "_abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(c) >= 0 || ".-0123456789".IndexOf(c) >= 0 || c >= '\u0080';
		}

		// Token: 0x0400382F RID: 14383
		protected const int TOK_EOF = 0;

		// Token: 0x04003830 RID: 14384
		protected const int TOK_NCNAME = 1;

		// Token: 0x04003831 RID: 14385
		protected const int TOK_MULTIPLY = 2;

		// Token: 0x04003832 RID: 14386
		protected const int TOK_LPAR = 3;

		// Token: 0x04003833 RID: 14387
		protected const int TOK_RPAR = 4;

		// Token: 0x04003834 RID: 14388
		protected const int TOK_LITERAL = 5;

		// Token: 0x04003835 RID: 14389
		protected const int TOK_NUMBER = 6;

		// Token: 0x04003836 RID: 14390
		protected const int TOK_FUNCTION_LPAR = 7;

		// Token: 0x04003837 RID: 14391
		protected const int TOK_PLUS = 8;

		// Token: 0x04003838 RID: 14392
		protected const int TOK_MINUS = 9;

		// Token: 0x04003839 RID: 14393
		protected const int TOK_MOD = 10;

		// Token: 0x0400383A RID: 14394
		protected const int TOK_DIV = 11;

		// Token: 0x0400383B RID: 14395
		protected const int TOK_NUMERIC = 12;

		// Token: 0x0400383C RID: 14396
		protected const int TOK_COMMA = 13;

		// Token: 0x0400383D RID: 14397
		protected const int TOK_PERCENT = 14;

		// Token: 0x0400383E RID: 14398
		protected const int TOK_COLORSPEC = 15;

		// Token: 0x0400383F RID: 14399
		protected const int TOK_FLOAT = 16;

		// Token: 0x04003840 RID: 14400
		protected const int TOK_INTEGER = 17;

		// Token: 0x04003841 RID: 14401
		private const string nameStartChars = "_abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

		// Token: 0x04003842 RID: 14402
		private const string nameChars = ".-0123456789";

		// Token: 0x04003843 RID: 14403
		private const string digits = "0123456789";

		// Token: 0x04003844 RID: 14404
		private const string hexchars = "0123456789abcdefABCDEF";

		// Token: 0x04003845 RID: 14405
		protected int currentToken;

		// Token: 0x04003846 RID: 14406
		protected string currentTokenValue;

		// Token: 0x04003847 RID: 14407
		protected int currentUnitLength;

		// Token: 0x04003848 RID: 14408
		private int currentTokenStartIndex;

		// Token: 0x04003849 RID: 14409
		private string expr;

		// Token: 0x0400384A RID: 14410
		private int exprIndex;

		// Token: 0x0400384B RID: 14411
		private int exprLength;

		// Token: 0x0400384C RID: 14412
		private bool recognizeOperator;
	}
}

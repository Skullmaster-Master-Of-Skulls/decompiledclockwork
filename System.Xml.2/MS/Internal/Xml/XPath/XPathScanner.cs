using System;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000049 RID: 73
	internal sealed class XPathScanner
	{
		// Token: 0x06000254 RID: 596 RVA: 0x000098F4 File Offset: 0x00007AF4
		public XPathScanner(string xpathExpr)
		{
			if (xpathExpr == null)
			{
				throw XPathException.Create("Xp_ExprExpected", string.Empty);
			}
			this.xpathExpr = xpathExpr;
			this.NextChar();
			this.NextLex();
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00009949 File Offset: 0x00007B49
		public string SourceText
		{
			get
			{
				return this.xpathExpr;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00009951 File Offset: 0x00007B51
		private char CurerntChar
		{
			get
			{
				return this.currentChar;
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000995C File Offset: 0x00007B5C
		private bool NextChar()
		{
			if (this.xpathExprIndex < this.xpathExpr.Length)
			{
				string text = this.xpathExpr;
				int num = this.xpathExprIndex;
				this.xpathExprIndex = num + 1;
				this.currentChar = text[num];
				return true;
			}
			this.currentChar = '\0';
			return false;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000258 RID: 600 RVA: 0x000099A8 File Offset: 0x00007BA8
		public XPathScanner.LexKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000259 RID: 601 RVA: 0x000099B0 File Offset: 0x00007BB0
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600025A RID: 602 RVA: 0x000099B8 File Offset: 0x00007BB8
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600025B RID: 603 RVA: 0x000099C0 File Offset: 0x00007BC0
		public string StringValue
		{
			get
			{
				return this.stringValue;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600025C RID: 604 RVA: 0x000099C8 File Offset: 0x00007BC8
		public double NumberValue
		{
			get
			{
				return this.numberValue;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000099D0 File Offset: 0x00007BD0
		public bool CanBeFunction
		{
			get
			{
				return this.canBeFunction;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000099D8 File Offset: 0x00007BD8
		private void SkipSpace()
		{
			while (this.xmlCharType.IsWhiteSpace(this.CurerntChar) && this.NextChar())
			{
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000099F8 File Offset: 0x00007BF8
		public bool NextLex()
		{
			this.SkipSpace();
			char curerntChar = this.CurerntChar;
			if (curerntChar <= '@')
			{
				if (curerntChar == '\0')
				{
					this.kind = XPathScanner.LexKind.Eof;
					return false;
				}
				switch (curerntChar)
				{
				case '!':
					this.kind = XPathScanner.LexKind.Bang;
					this.NextChar();
					if (this.CurerntChar == '=')
					{
						this.kind = XPathScanner.LexKind.Ne;
						this.NextChar();
						return true;
					}
					return true;
				case '"':
				case '\'':
					this.kind = XPathScanner.LexKind.String;
					this.stringValue = this.ScanString();
					return true;
				case '#':
				case '$':
				case '(':
				case ')':
				case '*':
				case '+':
				case ',':
				case '-':
				case '=':
				case '@':
					break;
				case '%':
				case '&':
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
				case ':':
				case ';':
				case '?':
					goto IL_21D;
				case '.':
					this.kind = XPathScanner.LexKind.Dot;
					this.NextChar();
					if (this.CurerntChar == '.')
					{
						this.kind = XPathScanner.LexKind.DotDot;
						this.NextChar();
						return true;
					}
					if (XmlCharType.IsDigit(this.CurerntChar))
					{
						this.kind = XPathScanner.LexKind.Number;
						this.numberValue = this.ScanFraction();
						return true;
					}
					return true;
				case '/':
					this.kind = XPathScanner.LexKind.Slash;
					this.NextChar();
					if (this.CurerntChar == '/')
					{
						this.kind = XPathScanner.LexKind.SlashSlash;
						this.NextChar();
						return true;
					}
					return true;
				case '<':
					this.kind = XPathScanner.LexKind.Lt;
					this.NextChar();
					if (this.CurerntChar == '=')
					{
						this.kind = XPathScanner.LexKind.Le;
						this.NextChar();
						return true;
					}
					return true;
				case '>':
					this.kind = XPathScanner.LexKind.Gt;
					this.NextChar();
					if (this.CurerntChar == '=')
					{
						this.kind = XPathScanner.LexKind.Ge;
						this.NextChar();
						return true;
					}
					return true;
				default:
					goto IL_21D;
				}
			}
			else if (curerntChar != '[' && curerntChar != ']' && curerntChar != '|')
			{
				goto IL_21D;
			}
			this.kind = (XPathScanner.LexKind)Convert.ToInt32(this.CurerntChar, CultureInfo.InvariantCulture);
			this.NextChar();
			return true;
			IL_21D:
			if (XmlCharType.IsDigit(this.CurerntChar))
			{
				this.kind = XPathScanner.LexKind.Number;
				this.numberValue = this.ScanNumber();
			}
			else
			{
				if (!this.xmlCharType.IsStartNCNameSingleChar(this.CurerntChar))
				{
					throw XPathException.Create("Xp_InvalidToken", this.SourceText);
				}
				this.kind = XPathScanner.LexKind.Name;
				this.name = this.ScanName();
				this.prefix = string.Empty;
				if (this.CurerntChar == ':')
				{
					this.NextChar();
					if (this.CurerntChar == ':')
					{
						this.NextChar();
						this.kind = XPathScanner.LexKind.Axe;
					}
					else
					{
						this.prefix = this.name;
						if (this.CurerntChar == '*')
						{
							this.NextChar();
							this.name = "*";
						}
						else
						{
							if (!this.xmlCharType.IsStartNCNameSingleChar(this.CurerntChar))
							{
								throw XPathException.Create("Xp_InvalidName", this.SourceText);
							}
							this.name = this.ScanName();
						}
					}
				}
				else
				{
					this.SkipSpace();
					if (this.CurerntChar == ':')
					{
						this.NextChar();
						if (this.CurerntChar != ':')
						{
							throw XPathException.Create("Xp_InvalidName", this.SourceText);
						}
						this.NextChar();
						this.kind = XPathScanner.LexKind.Axe;
					}
				}
				this.SkipSpace();
				this.canBeFunction = (this.CurerntChar == '(');
			}
			return true;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009D78 File Offset: 0x00007F78
		private double ScanNumber()
		{
			int startIndex = this.xpathExprIndex - 1;
			int num = 0;
			while (XmlCharType.IsDigit(this.CurerntChar))
			{
				this.NextChar();
				num++;
			}
			if (this.CurerntChar == '.')
			{
				this.NextChar();
				num++;
				while (XmlCharType.IsDigit(this.CurerntChar))
				{
					this.NextChar();
					num++;
				}
			}
			return XmlConvert.ToXPathDouble(this.xpathExpr.Substring(startIndex, num));
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00009DEC File Offset: 0x00007FEC
		private double ScanFraction()
		{
			int startIndex = this.xpathExprIndex - 2;
			int num = 1;
			while (XmlCharType.IsDigit(this.CurerntChar))
			{
				this.NextChar();
				num++;
			}
			return XmlConvert.ToXPathDouble(this.xpathExpr.Substring(startIndex, num));
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00009E30 File Offset: 0x00008030
		private string ScanString()
		{
			char curerntChar = this.CurerntChar;
			this.NextChar();
			int startIndex = this.xpathExprIndex - 1;
			int num = 0;
			while (this.CurerntChar != curerntChar)
			{
				if (!this.NextChar())
				{
					throw XPathException.Create("Xp_UnclosedString");
				}
				num++;
			}
			this.NextChar();
			return this.xpathExpr.Substring(startIndex, num);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009E8C File Offset: 0x0000808C
		private string ScanName()
		{
			int startIndex = this.xpathExprIndex - 1;
			int num = 0;
			while (this.xmlCharType.IsNCNameSingleChar(this.CurerntChar))
			{
				this.NextChar();
				num++;
			}
			return this.xpathExpr.Substring(startIndex, num);
		}

		// Token: 0x040000F1 RID: 241
		private string xpathExpr;

		// Token: 0x040000F2 RID: 242
		private int xpathExprIndex;

		// Token: 0x040000F3 RID: 243
		private XPathScanner.LexKind kind;

		// Token: 0x040000F4 RID: 244
		private char currentChar;

		// Token: 0x040000F5 RID: 245
		private string name;

		// Token: 0x040000F6 RID: 246
		private string prefix;

		// Token: 0x040000F7 RID: 247
		private string stringValue;

		// Token: 0x040000F8 RID: 248
		private double numberValue = double.NaN;

		// Token: 0x040000F9 RID: 249
		private bool canBeFunction;

		// Token: 0x040000FA RID: 250
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x02000303 RID: 771
		public enum LexKind
		{
			// Token: 0x0400141D RID: 5149
			Comma = 44,
			// Token: 0x0400141E RID: 5150
			Slash = 47,
			// Token: 0x0400141F RID: 5151
			At = 64,
			// Token: 0x04001420 RID: 5152
			Dot = 46,
			// Token: 0x04001421 RID: 5153
			LParens = 40,
			// Token: 0x04001422 RID: 5154
			RParens,
			// Token: 0x04001423 RID: 5155
			LBracket = 91,
			// Token: 0x04001424 RID: 5156
			RBracket = 93,
			// Token: 0x04001425 RID: 5157
			Star = 42,
			// Token: 0x04001426 RID: 5158
			Plus,
			// Token: 0x04001427 RID: 5159
			Minus = 45,
			// Token: 0x04001428 RID: 5160
			Eq = 61,
			// Token: 0x04001429 RID: 5161
			Lt = 60,
			// Token: 0x0400142A RID: 5162
			Gt = 62,
			// Token: 0x0400142B RID: 5163
			Bang = 33,
			// Token: 0x0400142C RID: 5164
			Dollar = 36,
			// Token: 0x0400142D RID: 5165
			Apos = 39,
			// Token: 0x0400142E RID: 5166
			Quote = 34,
			// Token: 0x0400142F RID: 5167
			Union = 124,
			// Token: 0x04001430 RID: 5168
			Ne = 78,
			// Token: 0x04001431 RID: 5169
			Le = 76,
			// Token: 0x04001432 RID: 5170
			Ge = 71,
			// Token: 0x04001433 RID: 5171
			And = 65,
			// Token: 0x04001434 RID: 5172
			Or = 79,
			// Token: 0x04001435 RID: 5173
			DotDot = 68,
			// Token: 0x04001436 RID: 5174
			SlashSlash = 83,
			// Token: 0x04001437 RID: 5175
			Name = 110,
			// Token: 0x04001438 RID: 5176
			String = 115,
			// Token: 0x04001439 RID: 5177
			Number = 100,
			// Token: 0x0400143A RID: 5178
			Axe = 97,
			// Token: 0x0400143B RID: 5179
			Eof = 69
		}
	}
}

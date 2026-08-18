using System;
using System.Collections;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000523 RID: 1315
	internal class XPathLexer
	{
		// Token: 0x060031FF RID: 12799 RVA: 0x000BFF10 File Offset: 0x000BE110
		static XPathLexer()
		{
			XPathLexer.namedTypes.Add("and", XPathTokenID.And);
			XPathLexer.namedTypes.Add("or", XPathTokenID.Or);
			XPathLexer.namedTypes.Add("mod", XPathTokenID.Mod);
			XPathLexer.namedTypes.Add("div", XPathTokenID.Div);
			XPathLexer.namedTypes.Add("ancestor", XPathTokenID.Ancestor);
			XPathLexer.namedTypes.Add("ancestor-or-self", XPathTokenID.AncestorOrSelf);
			XPathLexer.namedTypes.Add("attribute", XPathTokenID.Attribute);
			XPathLexer.namedTypes.Add("child", XPathTokenID.Child);
			XPathLexer.namedTypes.Add("descendant", XPathTokenID.Descendant);
			XPathLexer.namedTypes.Add("descendant-or-self", XPathTokenID.DescendantOrSelf);
			XPathLexer.namedTypes.Add("following", XPathTokenID.Following);
			XPathLexer.namedTypes.Add("following-sibling", XPathTokenID.FollowingSibling);
			XPathLexer.namedTypes.Add("namespace", XPathTokenID.Namespace);
			XPathLexer.namedTypes.Add("parent", XPathTokenID.Parent);
			XPathLexer.namedTypes.Add("preceding", XPathTokenID.Preceding);
			XPathLexer.namedTypes.Add("preceding-sibling", XPathTokenID.PrecedingSibling);
			XPathLexer.namedTypes.Add("self", XPathTokenID.Self);
			XPathLexer.namedTypes.Add("comment", XPathTokenID.Comment);
			XPathLexer.namedTypes.Add("text", XPathTokenID.Text);
			XPathLexer.namedTypes.Add("processing-instruction", XPathTokenID.Processing);
			XPathLexer.namedTypes.Add("node", XPathTokenID.Node);
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x000C0134 File Offset: 0x000BE334
		internal XPathLexer(string xpath) : this(xpath, true)
		{
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x000C0140 File Offset: 0x000BE340
		internal XPathLexer(string xpath, bool resolveKeywords)
		{
			this.resolveKeywords = resolveKeywords;
			this.xpath = string.Copy(xpath);
			this.xpathLength = this.xpath.Length;
			this.tokenStart = 0;
			this.currChar = 0;
			this.ch = '\0';
			this.previousID = XPathTokenID.Unknown;
			this.token = new XPathToken();
			this.ConsumeWhitespace();
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06003202 RID: 12802 RVA: 0x000C01A4 File Offset: 0x000BE3A4
		internal int FirstTokenChar
		{
			get
			{
				return this.tokenStart;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x000C01AC File Offset: 0x000BE3AC
		internal XPathToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x000C01B4 File Offset: 0x000BE3B4
		private bool AdvanceChar()
		{
			if (this.currChar < this.xpathLength)
			{
				this.ch = this.xpath[this.currChar];
				this.currChar++;
				return true;
			}
			if (this.currChar == this.xpathLength)
			{
				this.currChar++;
				this.ch = '\0';
			}
			return false;
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x000C021A File Offset: 0x000BE41A
		private void ConsumeToken()
		{
			this.tokenStart = this.currChar;
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x000C0228 File Offset: 0x000BE428
		internal string ConsumedSubstring()
		{
			return this.xpath.Substring(0, this.tokenStart);
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x000C023C File Offset: 0x000BE43C
		private string CurrentSubstring()
		{
			return this.xpath.Substring(this.tokenStart, this.currChar - this.tokenStart);
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x000C025C File Offset: 0x000BE45C
		private char PeekChar()
		{
			return this.PeekChar(1);
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x000C0268 File Offset: 0x000BE468
		private char PeekChar(int offset)
		{
			int num = this.currChar + offset - 1;
			if (num < this.xpathLength)
			{
				return this.xpath[num];
			}
			return '\0';
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x000C0297 File Offset: 0x000BE497
		private void PutbackChar()
		{
			if (this.currChar > this.tokenStart)
			{
				this.currChar--;
			}
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x000C02B8 File Offset: 0x000BE4B8
		internal bool MoveNext()
		{
			this.previousID = this.token.TokenID;
			if (!this.AdvanceChar())
			{
				return false;
			}
			if (XPathCharTypes.IsNCNameStart(this.ch))
			{
				this.TokenizeQName();
			}
			else if (XPathCharTypes.IsDigit(this.ch))
			{
				this.TokenizeNumber();
			}
			else
			{
				char c = this.ch;
				if (c <= '[')
				{
					switch (c)
					{
					case '!':
						if (this.PeekChar() == '=')
						{
							this.AdvanceChar();
							this.token.Set(XPathTokenID.Neq);
							goto IL_3FD;
						}
						this.ThrowError(QueryCompileError.UnsupportedOperator, this.CurrentSubstring());
						goto IL_3FD;
					case '"':
						this.TokenizeLiteral('"');
						goto IL_3FD;
					case '#':
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
					case ';':
					case '?':
						break;
					case '$':
					{
						XPathParser.QName qname = this.GetQName();
						if (qname.Prefix.Length == 0 && qname.Name.Length == 0)
						{
							this.AdvanceChar();
							this.ThrowError(QueryCompileError.InvalidVariable, (this.ch == '\0') ? string.Empty : this.CurrentSubstring());
						}
						this.token.Set(XPathTokenID.Variable, qname);
						goto IL_3FD;
					}
					case '\'':
						this.TokenizeLiteral('\'');
						goto IL_3FD;
					case '(':
						this.token.Set(XPathTokenID.LParen);
						goto IL_3FD;
					case ')':
						this.token.Set(XPathTokenID.RParen);
						goto IL_3FD;
					case '*':
						if (this.IsSpecialPrev())
						{
							this.token.Set(XPathTokenID.Multiply);
							goto IL_3FD;
						}
						this.token.Set(XPathTokenID.Wildcard, new XPathParser.QName(string.Empty, QueryDataModel.Wildcard));
						goto IL_3FD;
					case '+':
						this.token.Set(XPathTokenID.Plus);
						goto IL_3FD;
					case ',':
						this.token.Set(XPathTokenID.Comma);
						goto IL_3FD;
					case '-':
						this.token.Set(XPathTokenID.Minus);
						goto IL_3FD;
					case '.':
						if (this.PeekChar() == '.')
						{
							this.AdvanceChar();
							this.token.Set(XPathTokenID.DblPeriod);
							goto IL_3FD;
						}
						if (XPathCharTypes.IsDigit(this.PeekChar()))
						{
							this.TokenizeNumber();
							goto IL_3FD;
						}
						this.token.Set(XPathTokenID.Period);
						goto IL_3FD;
					case '/':
						if (this.PeekChar() == '/')
						{
							this.AdvanceChar();
							this.token.Set(XPathTokenID.DblSlash);
							goto IL_3FD;
						}
						this.token.Set(XPathTokenID.Slash);
						goto IL_3FD;
					case ':':
						if (this.PeekChar() == ':')
						{
							this.AdvanceChar();
							this.token.Set(XPathTokenID.DblColon);
							goto IL_3FD;
						}
						this.ThrowError(QueryCompileError.UnexpectedToken, this.CurrentSubstring());
						goto IL_3FD;
					case '<':
						if (this.PeekChar() == '=')
						{
							this.AdvanceChar();
							this.token.Set(XPathTokenID.Lte);
							goto IL_3FD;
						}
						this.token.Set(XPathTokenID.Lt);
						goto IL_3FD;
					case '=':
						this.token.Set(XPathTokenID.Eq);
						goto IL_3FD;
					case '>':
						if (this.PeekChar() == '=')
						{
							this.AdvanceChar();
							this.token.Set(XPathTokenID.Gte);
							goto IL_3FD;
						}
						this.token.Set(XPathTokenID.Gt);
						goto IL_3FD;
					case '@':
						this.token.Set(XPathTokenID.AtSign);
						goto IL_3FD;
					default:
						if (c == '[')
						{
							this.token.Set(XPathTokenID.LBracket);
							goto IL_3FD;
						}
						break;
					}
				}
				else
				{
					if (c == ']')
					{
						this.token.Set(XPathTokenID.RBracket);
						goto IL_3FD;
					}
					if (c == '|')
					{
						this.token.Set(XPathTokenID.Pipe);
						goto IL_3FD;
					}
				}
				this.token.Set(XPathTokenID.Unknown);
			}
			IL_3FD:
			this.ConsumeWhitespace();
			return true;
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x000C06C9 File Offset: 0x000BE8C9
		private void ConsumeWhitespace()
		{
			while (XPathCharTypes.IsWhitespace(this.PeekChar()))
			{
				this.AdvanceChar();
			}
			this.ConsumeToken();
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x000C06E8 File Offset: 0x000BE8E8
		private void TokenizeQName()
		{
			while (XPathCharTypes.IsNCName(this.PeekChar()))
			{
				this.AdvanceChar();
			}
			string text = this.CurrentSubstring();
			XPathTokenID xpathTokenID = XPathTokenID.Unknown;
			XPathParser.QName qname = new XPathParser.QName("", "");
			if (this.PeekChar() == ':' && this.PeekChar(2) != ':')
			{
				this.AdvanceChar();
				this.ConsumeToken();
				this.AdvanceChar();
				if (XPathCharTypes.IsNCNameStart(this.ch))
				{
					while (XPathCharTypes.IsNCName(this.PeekChar()))
					{
						this.AdvanceChar();
					}
					xpathTokenID = XPathTokenID.NameTest;
					qname = new XPathParser.QName(text, this.CurrentSubstring());
				}
				else if (this.ch == '*')
				{
					xpathTokenID = XPathTokenID.NameWildcard;
					qname = new XPathParser.QName(text, QueryDataModel.Wildcard);
				}
				else
				{
					this.ThrowError(QueryCompileError.InvalidNCName, (this.ch == '\0') ? "" : this.CurrentSubstring());
				}
			}
			else
			{
				xpathTokenID = XPathTokenID.NameTest;
				qname = new XPathParser.QName(string.Empty, text);
			}
			this.ConsumeWhitespace();
			if (this.IsSpecialPrev())
			{
				this.token.Set(this.GetNamedOperator(qname));
				return;
			}
			if (qname.Prefix.Length != 0)
			{
				if (this.PeekChar() == '(')
				{
					xpathTokenID = XPathTokenID.Function;
				}
				this.token.Set(xpathTokenID, qname);
				return;
			}
			if (this.PeekChar() == '(')
			{
				xpathTokenID = this.GetNodeTypeOrFunction(qname);
				if (xpathTokenID != XPathTokenID.Function)
				{
					this.token.Set(xpathTokenID);
					return;
				}
				this.token.Set(xpathTokenID, qname);
				return;
			}
			else
			{
				if (this.PeekChar() == ':' && this.PeekChar(2) == ':')
				{
					this.token.Set(this.GetAxisName(qname));
					return;
				}
				this.token.Set(xpathTokenID, qname);
				return;
			}
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x000C0898 File Offset: 0x000BEA98
		private XPathParser.QName GetQName()
		{
			string text = this.GetNCName();
			if (text == null)
			{
				return new XPathParser.QName(string.Empty, string.Empty);
			}
			if (text[0] == '$')
			{
				text = text.Substring(1);
			}
			if (this.PeekChar() == ':' && XPathCharTypes.IsNCNameStart(this.PeekChar(2)))
			{
				this.AdvanceChar();
				this.ConsumeToken();
				return new XPathParser.QName(text, this.GetNCName());
			}
			return new XPathParser.QName(string.Empty, text);
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x000C0910 File Offset: 0x000BEB10
		private string GetNCName()
		{
			if (XPathCharTypes.IsNCNameStart(this.PeekChar()))
			{
				this.AdvanceChar();
				while (XPathCharTypes.IsNCName(this.PeekChar()))
				{
					this.AdvanceChar();
				}
				string result = this.CurrentSubstring();
				this.ConsumeToken();
				return result;
			}
			return null;
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000C0958 File Offset: 0x000BEB58
		private void TokenizeNumber()
		{
			XPathTokenID id = XPathTokenID.Integer;
			while (XPathCharTypes.IsDigit(this.ch))
			{
				this.AdvanceChar();
			}
			if (this.ch == '.')
			{
				this.AdvanceChar();
				if (XPathCharTypes.IsDigit(this.ch))
				{
					id = XPathTokenID.Decimal;
					while (XPathCharTypes.IsDigit(this.ch))
					{
						this.AdvanceChar();
					}
				}
			}
			this.PutbackChar();
			double number = QueryValueModel.Double(this.CurrentSubstring());
			this.token.Set(id, number);
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000C09DC File Offset: 0x000BEBDC
		private void TokenizeLiteral(char c)
		{
			this.ConsumeToken();
			this.AdvanceChar();
			while (this.ch != c)
			{
				if (this.ch == '\0')
				{
					this.PutbackChar();
					this.ThrowError(QueryCompileError.InvalidLiteral, this.CurrentSubstring());
				}
				this.AdvanceChar();
			}
			this.PutbackChar();
			this.token.Set(XPathTokenID.Literal, this.CurrentSubstring());
			this.AdvanceChar();
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x000C0A48 File Offset: 0x000BEC48
		private bool IsSpecialPrev()
		{
			return this.previousID != XPathTokenID.Unknown && this.previousID != XPathTokenID.AtSign && this.previousID != XPathTokenID.DblColon && this.previousID != XPathTokenID.LParen && this.previousID != XPathTokenID.LBracket && this.previousID != XPathTokenID.Comma && (this.previousID & XPathTokenID.Operator) == XPathTokenID.Unknown && (this.previousID & XPathTokenID.NamedOperator) == XPathTokenID.Unknown;
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x000C0AC0 File Offset: 0x000BECC0
		private XPathTokenID GetNamedOperator(XPathParser.QName qname)
		{
			if (qname.Prefix.Length != 0)
			{
				this.ThrowError(QueryCompileError.InvalidOperatorName, qname.Prefix + ":" + qname.Name);
			}
			XPathTokenID namedType = this.GetNamedType(qname.Name);
			if (this.resolveKeywords && (namedType & XPathTokenID.NamedOperator) == XPathTokenID.Unknown)
			{
				this.ThrowError(QueryCompileError.UnsupportedOperator, this.previousID.ToString() + "->" + qname.Name);
			}
			return namedType;
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x000C0B44 File Offset: 0x000BED44
		private XPathTokenID GetAxisName(XPathParser.QName qname)
		{
			if (qname.Prefix.Length != 0)
			{
				this.ThrowError(QueryCompileError.InvalidAxisSpecifier, qname.Prefix + ":" + qname.Name);
			}
			XPathTokenID namedType = this.GetNamedType(qname.Name);
			if (this.resolveKeywords && (namedType & XPathTokenID.Axis) == XPathTokenID.Unknown)
			{
				this.ThrowError(QueryCompileError.UnsupportedAxis, qname.Name);
			}
			return namedType;
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x000C0BB0 File Offset: 0x000BEDB0
		private XPathTokenID GetNodeTypeOrFunction(XPathParser.QName qname)
		{
			XPathTokenID xpathTokenID = this.GetNamedType(qname.Name);
			if ((xpathTokenID & XPathTokenID.NodeType) == XPathTokenID.Unknown)
			{
				xpathTokenID = XPathTokenID.Function;
			}
			else if (qname.Prefix.Length > 0)
			{
				this.ThrowError(QueryCompileError.InvalidNodeType, qname.Prefix + ":" + qname.Name);
			}
			return xpathTokenID;
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x000C0C0C File Offset: 0x000BEE0C
		private XPathTokenID GetNamedType(string name)
		{
			if (this.resolveKeywords && XPathLexer.namedTypes.ContainsKey(name))
			{
				return (XPathTokenID)XPathLexer.namedTypes[name];
			}
			return XPathTokenID.Unknown;
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x000C0C35 File Offset: 0x000BEE35
		private void ThrowError(QueryCompileError err, string msg)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(err, msg));
		}

		// Token: 0x040026D3 RID: 9939
		private static Hashtable namedTypes = new Hashtable();

		// Token: 0x040026D4 RID: 9940
		private XPathTokenID previousID;

		// Token: 0x040026D5 RID: 9941
		private string xpath;

		// Token: 0x040026D6 RID: 9942
		private int tokenStart;

		// Token: 0x040026D7 RID: 9943
		private int currChar;

		// Token: 0x040026D8 RID: 9944
		private int xpathLength;

		// Token: 0x040026D9 RID: 9945
		private char ch;

		// Token: 0x040026DA RID: 9946
		private XPathToken token;

		// Token: 0x040026DB RID: 9947
		private bool resolveKeywords;
	}
}

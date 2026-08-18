using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000054 RID: 84
	internal class CssScanner
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0001720F File Offset: 0x0001540F
		public string RawNumber
		{
			get
			{
				return this.m_rawNumber;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00017217 File Offset: 0x00015417
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x0001721F File Offset: 0x0001541F
		public bool AllowEmbeddedAspNetBlocks { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00017228 File Offset: 0x00015428
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x00017230 File Offset: 0x00015430
		public bool GotEndOfLine { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00017239 File Offset: 0x00015439
		public bool EndOfFile
		{
			get
			{
				return this.m_isAtEOF;
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00017241 File Offset: 0x00015441
		public CssScanner(TextReader reader)
		{
			this.m_context = new CssContext();
			this.m_reader = reader;
			this.NextChar();
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00017264 File Offset: 0x00015464
		public CssToken NextToken()
		{
			this.GotEndOfLine = false;
			this.m_context.Advance();
			this.m_rawNumber = null;
			CssToken cssToken = null;
			for (;;)
			{
				bool flag = false;
				char currentChar = this.m_currentChar;
				if (currentChar <= '@')
				{
					if (currentChar != '\0')
					{
						switch (currentChar)
						{
						case '\t':
							break;
						case '\n':
						case '\f':
						case '\r':
							this.GotEndOfLine = true;
							break;
						case '\v':
							goto IL_299;
						default:
							switch (currentChar)
							{
							case ' ':
								break;
							case '!':
								cssToken = this.ScanImportant();
								goto IL_2A0;
							case '"':
							case '\'':
								cssToken = this.ScanString();
								goto IL_2A0;
							case '#':
								cssToken = this.ScanHash();
								goto IL_2A0;
							case '$':
								cssToken = this.ScanSuffixMatch();
								goto IL_2A0;
							case '%':
							case '&':
							case '(':
							case ')':
							case '+':
							case ',':
							case ':':
							case ';':
							case '=':
							case '>':
							case '?':
								goto IL_299;
							case '*':
								cssToken = this.ScanSubstringMatch();
								goto IL_2A0;
							case '-':
							{
								cssToken = this.ScanCDC();
								if (cssToken != null)
								{
									goto IL_2A0;
								}
								string ident = this.GetIdent();
								if (ident == null)
								{
									cssToken = new CssToken(TokenType.Character, '-', this.m_context);
									goto IL_2A0;
								}
								if (this.m_currentChar == '(')
								{
									this.NextChar();
									cssToken = new CssToken(TokenType.Function, "-" + ident + '(', this.m_context);
									goto IL_2A0;
								}
								cssToken = new CssToken(TokenType.Identifier, "-" + ident, this.m_context);
								goto IL_2A0;
							}
							case '.':
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
								cssToken = this.ScanNum();
								goto IL_2A0;
							case '/':
								cssToken = this.ScanComment();
								if (cssToken == null)
								{
									flag = true;
									goto IL_2A0;
								}
								goto IL_2A0;
							case '<':
								if (this.AllowEmbeddedAspNetBlocks && this.PeekChar() == '%')
								{
									cssToken = this.ScanAspNetBlock();
									goto IL_2A0;
								}
								cssToken = this.ScanCDO();
								goto IL_2A0;
							case '@':
								cssToken = this.ScanAtKeyword();
								goto IL_2A0;
							default:
								goto IL_299;
							}
							break;
						}
						while (CssScanner.IsSpace(this.m_currentChar))
						{
							if (this.m_currentChar == '\r' || this.m_currentChar == '\n' || this.m_currentChar == '\f')
							{
								this.GotEndOfLine = true;
							}
							this.NextChar();
						}
						cssToken = new CssToken(TokenType.Space, ' ', this.m_context);
					}
					else
					{
						this.m_isAtEOF = true;
					}
				}
				else
				{
					if (currentChar <= '^')
					{
						if (currentChar != 'U')
						{
							if (currentChar != '^')
							{
								goto IL_299;
							}
							cssToken = this.ScanPrefixMatch();
							goto IL_2A0;
						}
					}
					else if (currentChar != 'u')
					{
						switch (currentChar)
						{
						case '|':
							cssToken = this.ScanDashMatch();
							goto IL_2A0;
						case '}':
							goto IL_299;
						case '~':
							cssToken = this.ScanIncludes();
							goto IL_2A0;
						default:
							goto IL_299;
						}
					}
					cssToken = this.ScanUrl();
				}
				IL_2A0:
				if (!flag)
				{
					break;
				}
				continue;
				IL_299:
				cssToken = this.ScanIdent();
				goto IL_2A0;
			}
			return cssToken;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00017518 File Offset: 0x00015718
		public CssToken ScanReplacementToken()
		{
			CssToken result = null;
			string replacementToken = this.GetReplacementToken(false);
			if (!replacementToken.IsNullOrWhiteSpace())
			{
				result = new CssToken(TokenType.ReplacementToken, replacementToken, this.m_context);
			}
			return result;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00017548 File Offset: 0x00015748
		private CssToken ScanComment()
		{
			CssToken cssToken = null;
			this.NextChar();
			if (this.m_currentChar == '*')
			{
				this.NextChar();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("/*");
				bool flag = false;
				while (this.m_currentChar != '\0')
				{
					stringBuilder.Append(this.m_currentChar);
					if (this.m_currentChar == '*' && this.PeekChar() == '/')
					{
						stringBuilder.Append('/');
						this.NextChar();
						this.NextChar();
						if (stringBuilder.ToString() == "/*!/*/" && this.ReadString("/*/"))
						{
							stringBuilder.Append("/*/");
						}
						flag = true;
						break;
					}
					this.NextChar();
				}
				if (!flag)
				{
					this.ReportError(0, CssErrorCode.UnterminatedComment, new object[0]);
				}
				string text = stringBuilder.ToString();
				if (string.Compare(text, 2, "/#SOURCE", 0, 8, StringComparison.OrdinalIgnoreCase) == 0)
				{
					Match match = CssScanner.s_sourceDirective.Match(text);
					int line;
					int column;
					if (match != null && int.TryParse(match.Result("${line}"), out line) && int.TryParse(match.Result("${col}"), out column))
					{
						this.OnContextChange(match.Result("${path}"), line, column);
						this.SkipToNextLineWithoutUpdate();
						return null;
					}
				}
				cssToken = new CssToken(TokenType.Comment, text, this.m_context);
			}
			else if (this.m_currentChar == '/')
			{
				if (this.PeekChar() == '/')
				{
					this.NextChar();
					if (this.PeekChar() == '#')
					{
						this.NextChar();
						if (this.ReadString("#SOURCE"))
						{
							this.DirectiveSkipSpace();
							int num = this.DirectiveScanInteger();
							if (num > 0)
							{
								this.DirectiveSkipSpace();
								int num2 = this.DirectiveScanInteger();
								if (num2 > 0)
								{
									this.DirectiveSkipSpace();
									StringBuilder stringBuilder2 = new StringBuilder();
									while (this.m_currentChar != '\n' && this.m_currentChar != '\r')
									{
										stringBuilder2.Append(this.m_currentChar);
										this.DirectiveNextChar();
									}
									string text2 = stringBuilder2.ToString().TrimEnd(new char[0]);
									if (!string.IsNullOrEmpty(text2))
									{
										this.OnContextChange(text2, num, num2);
										this.SkipToNextLineWithoutUpdate();
										return null;
									}
								}
							}
						}
					}
				}
				while (this.m_currentChar != '\n' && this.m_currentChar != '\r' && this.m_currentChar != '\0')
				{
					this.NextChar();
				}
				return null;
			}
			if (cssToken == null)
			{
				cssToken = new CssToken(TokenType.Character, '/', this.m_context);
			}
			return cssToken;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x000177A0 File Offset: 0x000159A0
		private void SkipToNextLineWithoutUpdate()
		{
			while (this.m_currentChar != '\n' && this.m_currentChar != '\r')
			{
				this.DirectiveNextChar();
			}
			if (this.m_currentChar == '\n' || this.m_currentChar == '\f')
			{
				this.DirectiveNextChar();
				return;
			}
			if (this.m_currentChar == '\r' && this.DirectiveNextChar() == '\n')
			{
				this.DirectiveNextChar();
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00017804 File Offset: 0x00015A04
		private CssToken ScanAspNetBlock()
		{
			StringBuilder stringBuilder = new StringBuilder();
			char c = ' ';
			while (this.m_currentChar != '\0' && (this.m_currentChar != '>' || c != '%'))
			{
				stringBuilder.Append(this.m_currentChar);
				c = this.m_currentChar;
				this.NextChar();
			}
			if (this.m_currentChar != '\0')
			{
				stringBuilder.Append(this.m_currentChar);
				this.NextChar();
			}
			return new CssToken(TokenType.AspNetBlock, stringBuilder.ToString(), this.m_context);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001787C File Offset: 0x00015A7C
		private CssToken ScanCDO()
		{
			CssToken cssToken = null;
			this.NextChar();
			if (this.m_currentChar == '!' && this.PeekChar() == '-')
			{
				this.NextChar();
				if (this.PeekChar() == '-')
				{
					this.NextChar();
					this.NextChar();
					cssToken = new CssToken(TokenType.CommentOpen, "<!--", this.m_context);
				}
				else
				{
					this.PushChar('!');
				}
			}
			if (cssToken == null)
			{
				return new CssToken(TokenType.Character, '<', this.m_context);
			}
			return cssToken;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000178F4 File Offset: 0x00015AF4
		private CssToken ScanCDC()
		{
			CssToken result = null;
			this.NextChar();
			if (this.m_currentChar == '-' && this.PeekChar() == '>')
			{
				this.NextChar();
				this.NextChar();
				result = new CssToken(TokenType.CommentClose, "-->", this.m_context);
			}
			return result;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001793C File Offset: 0x00015B3C
		private CssToken ScanIncludes()
		{
			CssToken cssToken = null;
			this.NextChar();
			if (this.m_currentChar == '=')
			{
				this.NextChar();
				cssToken = new CssToken(TokenType.Includes, "~=", this.m_context);
			}
			if (cssToken == null)
			{
				return new CssToken(TokenType.Character, '~', this.m_context);
			}
			return cssToken;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00017988 File Offset: 0x00015B88
		private CssToken ScanDashMatch()
		{
			CssToken cssToken;
			if (this.PeekChar() == '=')
			{
				this.NextChar();
				this.NextChar();
				cssToken = new CssToken(TokenType.DashMatch, "|=", this.m_context);
			}
			else
			{
				cssToken = this.ScanIdent();
			}
			if (cssToken == null)
			{
				this.NextChar();
				cssToken = new CssToken(TokenType.Character, '|', this.m_context);
			}
			return cssToken;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000179E4 File Offset: 0x00015BE4
		private CssToken ScanPrefixMatch()
		{
			CssToken cssToken = null;
			this.NextChar();
			if (this.m_currentChar == '=')
			{
				this.NextChar();
				cssToken = new CssToken(TokenType.PrefixMatch, "^=", this.m_context);
			}
			if (cssToken == null)
			{
				return new CssToken(TokenType.Character, '^', this.m_context);
			}
			return cssToken;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00017A30 File Offset: 0x00015C30
		private CssToken ScanSuffixMatch()
		{
			CssToken cssToken = null;
			this.NextChar();
			if (this.m_currentChar == '=')
			{
				this.NextChar();
				cssToken = new CssToken(TokenType.SuffixMatch, "$=", this.m_context);
			}
			if (cssToken == null)
			{
				return new CssToken(TokenType.Character, '$', this.m_context);
			}
			return cssToken;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00017A7C File Offset: 0x00015C7C
		private CssToken ScanSubstringMatch()
		{
			CssToken cssToken;
			if (this.PeekChar() == '=')
			{
				this.NextChar();
				this.NextChar();
				cssToken = new CssToken(TokenType.SubstringMatch, "*=", this.m_context);
			}
			else
			{
				cssToken = this.ScanIdent();
			}
			if (cssToken == null)
			{
				this.NextChar();
				cssToken = new CssToken(TokenType.Character, '*', this.m_context);
			}
			return cssToken;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00017AD8 File Offset: 0x00015CD8
		private CssToken ScanString()
		{
			string @string = this.GetString();
			return new CssToken((@string.Length >= 2 && @string[0] == @string[@string.Length - 1]) ? TokenType.String : TokenType.Error, @string, this.m_context);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00017B24 File Offset: 0x00015D24
		private CssToken ScanHash()
		{
			this.NextChar();
			string text = (this.m_currentChar == '%') ? this.GetReplacementToken(true) : this.GetName();
			if (text != null)
			{
				return new CssToken(TokenType.Hash, '#' + text, this.m_context);
			}
			return new CssToken(TokenType.Character, '#', this.m_context);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00017B80 File Offset: 0x00015D80
		private CssToken ScanAtKeyword()
		{
			this.NextChar();
			TokenType tokenType = TokenType.Character;
			bool flag = this.m_currentChar == '-';
			if (flag)
			{
				this.NextChar();
			}
			string text = this.GetIdent();
			if (text != null)
			{
				if (flag)
				{
					text = '-' + text;
				}
				string key;
				switch (key = text.ToUpperInvariant())
				{
				case "IMPORT":
					tokenType = TokenType.ImportSymbol;
					goto IL_2B3;
				case "PAGE":
					tokenType = TokenType.PageSymbol;
					goto IL_2B3;
				case "MEDIA":
					tokenType = TokenType.MediaSymbol;
					goto IL_2B3;
				case "FONT-FACE":
					tokenType = TokenType.FontFaceSymbol;
					goto IL_2B3;
				case "CHARSET":
					tokenType = TokenType.CharacterSetSymbol;
					goto IL_2B3;
				case "NAMESPACE":
					tokenType = TokenType.NamespaceSymbol;
					goto IL_2B3;
				case "TOP-LEFT-CORNER":
					tokenType = TokenType.TopLeftCornerSymbol;
					goto IL_2B3;
				case "TOP-LEFT":
					tokenType = TokenType.TopLeftSymbol;
					goto IL_2B3;
				case "TOP-CENTER":
					tokenType = TokenType.TopCenterSymbol;
					goto IL_2B3;
				case "TOP-RIGHT":
					tokenType = TokenType.TopRightSymbol;
					goto IL_2B3;
				case "TOP-RIGHT-CORNER":
					tokenType = TokenType.TopRightCornerSymbol;
					goto IL_2B3;
				case "BOTTOM-LEFT-CORNER":
					tokenType = TokenType.BottomLeftCornerSymbol;
					goto IL_2B3;
				case "BOTTOM-LEFT":
					tokenType = TokenType.BottomLeftSymbol;
					goto IL_2B3;
				case "BOTTOM-CENTER":
					tokenType = TokenType.BottomCenterSymbol;
					goto IL_2B3;
				case "BOTTOM-RIGHT":
					tokenType = TokenType.BottomRightSymbol;
					goto IL_2B3;
				case "BOTTOM-RIGHT-CORNER":
					tokenType = TokenType.BottomRightCornerSymbol;
					goto IL_2B3;
				case "LEFT-TOP":
					tokenType = TokenType.LeftTopSymbol;
					goto IL_2B3;
				case "LEFT-MIDDLE":
					tokenType = TokenType.LeftMiddleSymbol;
					goto IL_2B3;
				case "LEFT-BOTTOM":
					tokenType = TokenType.LeftBottomSymbol;
					goto IL_2B3;
				case "RIGHT-TOP":
					tokenType = TokenType.RightTopSymbol;
					goto IL_2B3;
				case "RIGHT-MIDDLE":
					tokenType = TokenType.RightMiddleSymbol;
					goto IL_2B3;
				case "RIGHT-BOTTOM":
					tokenType = TokenType.RightBottomSymbol;
					goto IL_2B3;
				case "KEYFRAMES":
				case "-MS-KEYFRAMES":
				case "-MOZ-KEYFRAMES":
				case "-WEBKIT-KEYFRAMES":
					tokenType = TokenType.KeyFramesSymbol;
					goto IL_2B3;
				}
				tokenType = TokenType.AtKeyword;
			}
			else if (flag)
			{
				this.PushChar('-');
			}
			IL_2B3:
			return new CssToken(tokenType, '@' + ((text == null) ? string.Empty : text), this.m_context);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00017E64 File Offset: 0x00016064
		private CssToken ScanImportant()
		{
			CssToken cssToken = null;
			this.NextChar();
			string w = this.GetW();
			if (char.ToUpperInvariant(this.m_currentChar) == 'I' && this.ReadString("IMPORTANT"))
			{
				cssToken = new CssToken(TokenType.ImportantSymbol, "!important", this.m_context);
			}
			if (cssToken == null && w.Length > 0)
			{
				this.PushChar(' ');
			}
			if (cssToken == null)
			{
				return new CssToken(TokenType.Character, '!', this.m_context);
			}
			return cssToken;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00017ED8 File Offset: 0x000160D8
		private CssToken ScanUnicodeRange()
		{
			CssToken cssToken = null;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("U+");
			bool flag = false;
			int num = 0;
			bool flag2 = true;
			int num2 = 0;
			while (this.m_currentChar != '\0' && num < 6 && (this.m_currentChar == '?' || (!flag && CssScanner.IsH(this.m_currentChar))))
			{
				if (flag2 && this.m_currentChar != '0')
				{
					flag2 = false;
				}
				if (this.m_currentChar == '?')
				{
					flag = true;
					num2 = num2 * 16 + CssScanner.HValue('F');
				}
				else
				{
					num2 = num2 * 16 + CssScanner.HValue(this.m_currentChar);
				}
				if (!flag2)
				{
					stringBuilder.Append(this.m_currentChar);
				}
				num++;
				this.NextChar();
			}
			if (num > 0)
			{
				if (num2 < 0 || 1114111 < num2)
				{
					this.ReportError(0, CssErrorCode.InvalidUnicodeRange, new object[]
					{
						stringBuilder.ToString()
					});
				}
				if (flag2)
				{
					stringBuilder.Append('0');
				}
				if (flag)
				{
					cssToken = new CssToken(TokenType.UnicodeRange, stringBuilder.ToString(), this.m_context);
				}
				else if (this.m_currentChar == '-')
				{
					stringBuilder.Append('-');
					this.NextChar();
					num = 0;
					flag2 = true;
					int num3 = 0;
					while (this.m_currentChar != '\0' && num < 6 && CssScanner.IsH(this.m_currentChar))
					{
						if (flag2 && this.m_currentChar != '0')
						{
							flag2 = false;
						}
						num3 = num3 * 16 + CssScanner.HValue(this.m_currentChar);
						if (!flag2)
						{
							stringBuilder.Append(this.m_currentChar);
						}
						num++;
						this.NextChar();
					}
					if (num > 0)
					{
						if (flag2)
						{
							stringBuilder.Append('0');
						}
						if (num3 < 0 || 1114111 < num3 || num2 >= num3)
						{
							this.ReportError(0, CssErrorCode.InvalidUnicodeRange, new object[]
							{
								stringBuilder.ToString()
							});
						}
						cssToken = new CssToken(TokenType.UnicodeRange, stringBuilder.ToString(), this.m_context);
					}
				}
				else
				{
					cssToken = new CssToken(TokenType.UnicodeRange, stringBuilder.ToString(), this.m_context);
				}
			}
			if (cssToken == null)
			{
				this.PushString(stringBuilder.ToString());
				cssToken = this.ScanIdent();
			}
			return cssToken;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000180EC File Offset: 0x000162EC
		private CssToken ScanUrl()
		{
			CssToken cssToken = null;
			if (this.PeekChar() == '+')
			{
				this.NextChar();
				this.NextChar();
				cssToken = this.ScanUnicodeRange();
			}
			else if (this.ReadString("URL("))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("url(");
				this.GetW();
				string text = this.GetString();
				if (text == null)
				{
					text = this.GetUrl();
				}
				if (text != null)
				{
					stringBuilder.Append(text);
					this.GetW();
					if (this.m_currentChar == ')')
					{
						stringBuilder.Append(')');
						this.NextChar();
						cssToken = new CssToken(TokenType.Uri, stringBuilder.ToString(), this.m_context);
					}
				}
			}
			if (cssToken == null)
			{
				return this.ScanIdent();
			}
			return cssToken;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0001819C File Offset: 0x0001639C
		private CssToken ScanNum()
		{
			CssToken result = null;
			string num = this.GetNum();
			if (num != null)
			{
				if (this.m_currentChar == '%')
				{
					this.NextChar();
					result = new CssToken(TokenType.Percentage, num + '%', this.m_context);
					this.m_rawNumber += '%';
				}
				else
				{
					string ident = this.GetIdent();
					if (ident == null)
					{
						result = new CssToken(TokenType.Number, num, this.m_context);
					}
					else
					{
						this.m_rawNumber += ident;
						TokenType tokenType = TokenType.Dimension;
						string key;
						switch (key = ident.ToUpperInvariant())
						{
						case "EM":
						case "EX":
						case "CH":
						case "REM":
						case "VW":
						case "VH":
						case "VM":
						case "VMIN":
						case "VMAX":
						case "FR":
						case "GR":
						case "GD":
							tokenType = TokenType.RelativeLength;
							break;
						case "CM":
						case "MM":
						case "IN":
						case "PX":
						case "PT":
						case "PC":
							tokenType = TokenType.AbsoluteLength;
							break;
						case "DEG":
						case "GRAD":
						case "RAD":
						case "TURN":
							tokenType = TokenType.Angle;
							break;
						case "MS":
						case "S":
							tokenType = TokenType.Time;
							break;
						case "DPI":
						case "DPCM":
						case "DPPX":
							tokenType = TokenType.Resolution;
							break;
						case "HZ":
						case "KHZ":
							tokenType = TokenType.Frequency;
							break;
						case "DB":
						case "ST":
							tokenType = TokenType.Speech;
							break;
						}
						if (num == "0" && tokenType != TokenType.Dimension && tokenType != TokenType.Angle && tokenType != TokenType.Time && tokenType != TokenType.Frequency && tokenType != TokenType.Resolution)
						{
							result = new CssToken(TokenType.Number, num, this.m_context);
						}
						else
						{
							result = new CssToken(tokenType, num + ident, this.m_context);
						}
					}
				}
			}
			else if (this.m_currentChar == '.')
			{
				result = new CssToken(TokenType.Character, '.', this.m_context);
				this.NextChar();
			}
			else
			{
				this.ReportError(1, CssErrorCode.UnexpectedNumberCharacter, new object[]
				{
					this.m_currentChar
				});
			}
			return result;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00018538 File Offset: 0x00016738
		private CssToken ScanIdent()
		{
			CssToken result = null;
			string ident = this.GetIdent();
			if (ident != null)
			{
				if (this.m_currentChar == '(')
				{
					this.NextChar();
					if (string.Compare(ident, "not", StringComparison.OrdinalIgnoreCase) == 0)
					{
						result = new CssToken(TokenType.Not, ident + '(', this.m_context);
					}
					else
					{
						result = new CssToken(TokenType.Function, ident + '(', this.m_context);
					}
				}
				else if (string.Compare(ident, "progid", StringComparison.OrdinalIgnoreCase) == 0 && this.m_currentChar == ':')
				{
					this.NextChar();
					result = this.ScanProgId();
				}
				else
				{
					result = new CssToken(TokenType.Identifier, ident, this.m_context);
				}
			}
			if (ident == null && this.m_currentChar != '\0')
			{
				result = new CssToken(TokenType.Character, this.m_currentChar, this.m_context);
				this.NextChar();
			}
			return result;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00018608 File Offset: 0x00016808
		private CssToken ScanProgId()
		{
			CssToken result = null;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("progid:");
			for (string ident = this.GetIdent(); ident != null; ident = this.GetIdent())
			{
				stringBuilder.Append(ident);
				if (this.m_currentChar == '.')
				{
					stringBuilder.Append('.');
					this.NextChar();
				}
			}
			if (this.m_currentChar == '(')
			{
				stringBuilder.Append('(');
				this.NextChar();
				result = new CssToken(TokenType.ProgId, stringBuilder.ToString(), this.m_context);
			}
			else
			{
				this.ReportError(1, CssErrorCode.ExpectedOpenParenthesis, new object[0]);
			}
			return result;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000186A0 File Offset: 0x000168A0
		private static bool IsSpace(char ch)
		{
			switch (ch)
			{
			case '\t':
			case '\n':
			case '\f':
			case '\r':
				break;
			case '\v':
				return false;
			default:
				if (ch != ' ')
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000186D4 File Offset: 0x000168D4
		private static int HValue(char ch)
		{
			if ('0' <= ch && ch <= '9')
			{
				return (int)(ch - '0');
			}
			if ('a' <= ch && ch <= 'f')
			{
				return (int)(ch - 'a' + '\n');
			}
			if ('A' <= ch && ch <= 'F')
			{
				return (int)(ch - 'A' + '\n');
			}
			return 0;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0001870A File Offset: 0x0001690A
		public static bool IsH(char ch)
		{
			return ('0' <= ch && ch <= '9') || ('a' <= ch && ch <= 'f') || ('A' <= ch && ch <= 'F');
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00018731 File Offset: 0x00016931
		private static bool IsD(char ch)
		{
			return '0' <= ch && ch <= '9';
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00018742 File Offset: 0x00016942
		private static bool IsNonAscii(char ch)
		{
			return '\u0080' <= ch && ch <= char.MaxValue;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00018759 File Offset: 0x00016959
		internal static bool IsNmChar(char ch)
		{
			return CssScanner.IsNonAscii(ch) || ch == '-' || ch == '_' || ('0' <= ch && ch <= '9') || ('a' <= ch && ch <= 'z') || ('A' <= ch && ch <= 'Z');
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00018794 File Offset: 0x00016994
		private string GetReplacementToken(bool advancePastDelimiter)
		{
			bool flag = false;
			char currentChar = this.m_currentChar;
			StringBuilder stringBuilder = new StringBuilder();
			if (advancePastDelimiter)
			{
				this.NextChar();
			}
			string name = this.GetName();
			while (name != null)
			{
				stringBuilder.Append(name);
				if (this.m_currentChar == '.')
				{
					stringBuilder.Append('.');
					this.NextChar();
					name = this.GetName();
					if (name != null)
					{
						continue;
					}
				}
				else if (this.m_currentChar == ':')
				{
					this.NextChar();
					stringBuilder.Append(':');
					stringBuilder.Append(this.GetName());
					if (this.m_currentChar == '%')
					{
						this.NextChar();
						stringBuilder.Append('%');
						flag = true;
						break;
					}
				}
				else if (this.m_currentChar == '%')
				{
					this.NextChar();
					stringBuilder.Append('%');
					flag = true;
					break;
				}
				this.PushString(stringBuilder.ToString());
				break;
			}
			if (!flag)
			{
				this.m_currentChar = currentChar;
			}
			if (!flag)
			{
				return null;
			}
			return '%' + stringBuilder.ToString();
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00018888 File Offset: 0x00016A88
		private int GetUnicodeEncodingValue(out bool follwedByWhitespace)
		{
			int num = 0;
			int num2 = 0;
			while (this.m_currentChar != '\0' && num2++ < 6 && CssScanner.IsH(this.m_currentChar))
			{
				num = num * 16 + CssScanner.HValue(this.m_currentChar);
				this.NextChar();
			}
			follwedByWhitespace = CssScanner.IsSpace(this.m_currentChar);
			if (follwedByWhitespace)
			{
				this.NextChar();
			}
			return num;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000188E8 File Offset: 0x00016AE8
		private string GetUnicode()
		{
			string result = null;
			if (this.m_currentChar == '\\')
			{
				char ch = this.PeekChar();
				if (CssScanner.IsH(ch))
				{
					this.NextChar();
					bool flag;
					int num = this.GetUnicodeEncodingValue(out flag);
					if (num == 92 || num == 32)
					{
						result = (flag ? "\\{0:x} " : "\\{0:x}").FormatInvariant(new object[]
						{
							num
						});
					}
					else
					{
						if (55296 <= num && num <= 56319)
						{
							int num2 = num;
							if (this.m_currentChar == '\\' && CssScanner.IsH(this.PeekChar()))
							{
								this.NextChar();
								int unicodeEncodingValue = this.GetUnicodeEncodingValue(out flag);
								if (56320 <= unicodeEncodingValue && unicodeEncodingValue <= 57343)
								{
									num = 65536 + (num2 - 55296) * 1024 + (unicodeEncodingValue - 56320);
								}
								else
								{
									this.ReportError(0, CssErrorCode.InvalidLowSurrogate, new object[]
									{
										num2,
										unicodeEncodingValue
									});
								}
							}
							else
							{
								this.ReportError(0, CssErrorCode.HighSurrogateNoLow, new object[]
								{
									num
								});
							}
						}
						result = CssScanner.ConvertUtf32ToUtf16(num);
					}
				}
			}
			return result;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00018A25 File Offset: 0x00016C25
		private static string ConvertUtf32ToUtf16(int unicodeValue)
		{
			return char.ConvertFromUtf32(unicodeValue);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00018A30 File Offset: 0x00016C30
		private string GetEscape()
		{
			string unicode = this.GetUnicode();
			if (unicode == null && this.m_currentChar == '\\')
			{
				char c = this.PeekChar();
				if ((' ' <= c && c <= '~') || CssScanner.IsNonAscii(c))
				{
					this.NextChar();
					this.NextChar();
					return "\\" + c;
				}
			}
			return unicode;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00018A88 File Offset: 0x00016C88
		private string GetNmStart()
		{
			string text = this.GetEscape();
			if (text == null && (CssScanner.IsNonAscii(this.m_currentChar) || this.m_currentChar == '_' || ('a' <= this.m_currentChar && this.m_currentChar <= 'z') || ('A' <= this.m_currentChar && this.m_currentChar <= 'Z')))
			{
				if (this.m_currentChar == '_')
				{
					this.ReportError(4, CssErrorCode.UnderscoreNotValid, new object[0]);
				}
				text = char.ToString(this.m_currentChar);
				this.NextChar();
			}
			return text;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00018B10 File Offset: 0x00016D10
		private string GetNmChar()
		{
			string text = this.GetEscape();
			if (text == null && CssScanner.IsNmChar(this.m_currentChar))
			{
				if (this.m_currentChar == '_')
				{
					this.ReportError(4, CssErrorCode.UnderscoreNotValid, new object[0]);
				}
				text = char.ToString(this.m_currentChar);
				this.NextChar();
			}
			return text;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00018B64 File Offset: 0x00016D64
		private string GetString()
		{
			string text = null;
			if (this.m_currentChar == '\'' || this.m_currentChar == '"')
			{
				char currentChar = this.m_currentChar;
				this.NextChar();
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(currentChar);
				while (this.m_currentChar != '\0' && this.m_currentChar != currentChar)
				{
					text = this.GetEscape();
					if (text != null)
					{
						if (text.Length == 1 && text[0] == currentChar)
						{
							text = "\\" + currentChar;
						}
						stringBuilder.Append(text);
					}
					else if (CssScanner.IsNonAscii(this.m_currentChar))
					{
						stringBuilder.Append(this.m_currentChar);
						this.NextChar();
					}
					else if (this.m_currentChar == '\\')
					{
						this.NextChar();
						if (this.GetNewline() == null)
						{
							this.ReportError(0, CssErrorCode.UnexpectedEscape, new object[]
							{
								this.m_currentChar
							});
						}
					}
					else if (this.m_currentChar == ' ' || this.m_currentChar == '\t' || this.m_currentChar == '!' || this.m_currentChar == '#' || this.m_currentChar == '$' || this.m_currentChar == '%' || this.m_currentChar == '&' || ('(' <= this.m_currentChar && this.m_currentChar <= '~') || this.m_currentChar == ((currentChar == '"') ? '\'' : '"'))
					{
						char currentChar2 = this.m_currentChar;
						stringBuilder.Append(this.m_currentChar);
						this.NextChar();
						if (this.AllowEmbeddedAspNetBlocks && currentChar2 == '<' && this.m_currentChar == '%')
						{
							this.SkipAspNetBlock(stringBuilder);
						}
					}
					else
					{
						if (this.m_currentChar == '\n' || this.m_currentChar == '\r')
						{
							this.GotEndOfLine = true;
							this.ReportError(0, CssErrorCode.UnterminatedString, new object[]
							{
								stringBuilder.ToString()
							});
							stringBuilder.AppendLine();
							while (CssScanner.IsSpace(this.m_currentChar))
							{
								this.NextChar();
							}
							return stringBuilder.ToString();
						}
						this.ReportError(0, CssErrorCode.UnexpectedStringCharacter, new object[]
						{
							this.m_currentChar
						});
					}
				}
				if (this.m_currentChar == currentChar)
				{
					stringBuilder.Append(currentChar);
					this.NextChar();
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00018DBC File Offset: 0x00016FBC
		private void SkipAspNetBlock(StringBuilder sb)
		{
			sb.Append(this.m_currentChar);
			this.NextChar();
			bool flag = false;
			while (this.m_currentChar != '\0')
			{
				if (this.m_currentChar == '%')
				{
					flag = true;
				}
				else
				{
					if (flag && this.m_currentChar == '>')
					{
						sb.Append(this.m_currentChar);
						this.NextChar();
						return;
					}
					flag = false;
				}
				sb.Append(this.m_currentChar);
				this.NextChar();
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00018E2C File Offset: 0x0001702C
		private string GetIdent()
		{
			string text = this.GetNmStart();
			if (text != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(text);
				while (this.m_currentChar != '\0' && (text = this.GetNmChar()) != null)
				{
					stringBuilder.Append(text);
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00018E78 File Offset: 0x00017078
		private string GetName()
		{
			string text = this.GetNmChar();
			if (text != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(text);
				while (this.m_currentChar != '\0' && (text = this.GetNmChar()) != null)
				{
					stringBuilder.Append(text);
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00018EC4 File Offset: 0x000170C4
		private string GetNum()
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			bool flag = false;
			if (CssScanner.IsD(this.m_currentChar))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.m_currentChar);
				this.NextChar();
				while (CssScanner.IsD(this.m_currentChar))
				{
					stringBuilder.Append(this.m_currentChar);
					this.NextChar();
				}
				text2 = stringBuilder.ToString();
			}
			if (this.m_currentChar == '.')
			{
				if (CssScanner.IsD(this.PeekChar()))
				{
					flag = true;
					this.NextChar();
					StringBuilder stringBuilder2 = new StringBuilder();
					while (CssScanner.IsD(this.m_currentChar))
					{
						stringBuilder2.Append(this.m_currentChar);
						this.NextChar();
					}
					text3 = stringBuilder2.ToString();
				}
				else if (text2 != null)
				{
					flag = true;
					this.ReportError(2, CssErrorCode.DecimalNoDigit, new object[0]);
					text3 = string.Empty;
					this.NextChar();
				}
			}
			if (text2 != null || text3 != null)
			{
				string rawNumber;
				if ((rawNumber = text2) == null)
				{
					rawNumber = (((flag ? "." : "") + text3) ?? "");
				}
				this.m_rawNumber = rawNumber;
				if (text2 != null)
				{
					text2 = CssScanner.s_leadingZeros.Replace(text2, "$1");
				}
				if (text3 != null)
				{
					text3 = CssScanner.s_trailingZeros.Replace(text3, "$1");
					if (text3 == "0" || text3.Length == 0)
					{
						text3 = null;
					}
				}
				if (text3 != null && text2 == "0")
				{
					text2 = null;
				}
				if (text3 == null)
				{
					text = text2;
					if (text == null)
					{
						text = "0";
					}
				}
				else
				{
					text = text2 + '.' + text3;
				}
			}
			return text;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00019048 File Offset: 0x00017248
		private string GetUrl()
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (this.m_currentChar != '\0')
			{
				string escape = this.GetEscape();
				if (escape != null)
				{
					stringBuilder.Append(escape);
				}
				else
				{
					if (!CssScanner.IsNonAscii(this.m_currentChar) && this.m_currentChar != '!' && this.m_currentChar != '#' && this.m_currentChar != '$' && this.m_currentChar != '%' && this.m_currentChar != '&' && ('*' > this.m_currentChar || this.m_currentChar > '~'))
					{
						break;
					}
					stringBuilder.Append(this.m_currentChar);
					this.NextChar();
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000190E8 File Offset: 0x000172E8
		private string GetW()
		{
			string result = string.Empty;
			if (CssScanner.IsSpace(this.m_currentChar))
			{
				result = " ";
				this.NextChar();
				while (CssScanner.IsSpace(this.m_currentChar))
				{
					this.NextChar();
				}
			}
			return result;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001912C File Offset: 0x0001732C
		private string GetNewline()
		{
			string result = null;
			switch (this.m_currentChar)
			{
			case '\n':
				this.NextChar();
				result = "\n";
				break;
			case '\f':
				this.NextChar();
				result = "\f";
				break;
			case '\r':
				this.NextChar();
				if (this.m_currentChar == '\n')
				{
					this.NextChar();
					result = "\r\n";
				}
				else
				{
					result = "\r";
				}
				break;
			}
			return result;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x000191A0 File Offset: 0x000173A0
		private void NextChar()
		{
			if (this.m_readAhead != null)
			{
				this.m_currentChar = this.m_readAhead[0];
				if (this.m_readAhead.Length == 1)
				{
					this.m_readAhead = null;
				}
				else
				{
					this.m_readAhead = this.m_readAhead.Substring(1);
				}
				this.m_context.End.NextChar();
				return;
			}
			int num = this.m_reader.Read();
			if (num < 0)
			{
				this.m_currentChar = '\0';
				return;
			}
			this.m_currentChar = (char)num;
			switch (this.m_currentChar)
			{
			case '\n':
			case '\f':
				this.m_context.End.NextLine();
				return;
			case '\r':
				if (this.PeekChar() != '\n')
				{
					this.m_context.End.NextLine();
					return;
				}
				return;
			}
			this.m_context.End.NextChar();
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00019280 File Offset: 0x00017480
		public char PeekChar()
		{
			if (this.m_readAhead != null)
			{
				return this.m_readAhead[0];
			}
			int num = this.m_reader.Peek();
			if (num < 0)
			{
				return '\0';
			}
			return (char)num;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x000192B8 File Offset: 0x000174B8
		private bool ReadString(string str)
		{
			if (char.ToUpperInvariant(this.m_currentChar) != char.ToUpperInvariant(str[0]))
			{
				return false;
			}
			StringBuilder stringBuilder = null;
			for (int i = 1; i < str.Length; i++)
			{
				if (char.ToUpperInvariant(this.PeekChar()) != char.ToUpperInvariant(str[i]))
				{
					if (i > 1 && stringBuilder != null)
					{
						this.PushString(stringBuilder.ToString());
					}
					return false;
				}
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(str.Length);
				}
				stringBuilder.Append(this.m_currentChar);
				this.NextChar();
			}
			this.NextChar();
			return true;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001934C File Offset: 0x0001754C
		private void PushChar(char ch)
		{
			if (this.m_readAhead == null)
			{
				this.m_readAhead = char.ToString(this.m_currentChar);
				this.m_currentChar = ch;
			}
			else
			{
				this.m_readAhead = this.m_currentChar + this.m_readAhead;
				this.m_currentChar = ch;
			}
			this.m_context.End.PreviousChar();
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000193B0 File Offset: 0x000175B0
		private void PushString(string str)
		{
			if (str.Length > 0)
			{
				if (str.Length > 1)
				{
					this.m_readAhead = str.Substring(1) + this.m_currentChar + this.m_readAhead;
				}
				else
				{
					this.m_readAhead = this.m_currentChar + this.m_readAhead;
				}
				this.m_currentChar = str[0];
			}
			for (int i = 0; i < str.Length; i++)
			{
				this.m_context.End.PreviousChar();
			}
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00019440 File Offset: 0x00017640
		private char DirectiveNextChar()
		{
			int num = this.m_reader.Read();
			this.m_currentChar = ((num < 0) ? '\0' : ((char)num));
			return this.m_currentChar;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001946E File Offset: 0x0001766E
		private void DirectiveSkipSpace()
		{
			while (this.m_currentChar == ' ' || this.m_currentChar == '\t')
			{
				this.NextChar();
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001948C File Offset: 0x0001768C
		private int DirectiveScanInteger()
		{
			int num = 0;
			while ('0' <= this.m_currentChar && this.m_currentChar <= '9')
			{
				num = num * 10 + (int)(this.m_currentChar - '0');
				this.NextChar();
			}
			return num;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000194C8 File Offset: 0x000176C8
		private void ReportError(int severity, CssErrorCode error, params object[] args)
		{
			string message = CssStrings.ResourceManager.GetString(error.ToString(), CssStrings.Culture).FormatInvariant(args);
			this.OnScannerError(new ContextError
			{
				IsError = (severity < 2),
				Severity = severity,
				Subcategory = CssStrings.ScannerSubsystem,
				File = "",
				ErrorNumber = (int)error,
				ErrorCode = "CSS{0}".FormatInvariant(new object[]
				{
					(int)(error & (CssErrorCode)65535)
				}),
				StartLine = this.m_context.End.Line,
				StartColumn = this.m_context.End.Char,
				Message = message
			});
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600054A RID: 1354 RVA: 0x0001958C File Offset: 0x0001778C
		// (remove) Token: 0x0600054B RID: 1355 RVA: 0x000195C4 File Offset: 0x000177C4
		public event EventHandler<ContextErrorEventArgs> ScannerError;

		// Token: 0x0600054C RID: 1356 RVA: 0x000195FC File Offset: 0x000177FC
		protected void OnScannerError(ContextError error)
		{
			if (this.ScannerError != null)
			{
				this.ScannerError(this, new ContextErrorEventArgs
				{
					Error = error
				});
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600054D RID: 1357 RVA: 0x0001962C File Offset: 0x0001782C
		// (remove) Token: 0x0600054E RID: 1358 RVA: 0x00019664 File Offset: 0x00017864
		public event EventHandler<CssScannerContextChangeEventArgs> ContextChange;

		// Token: 0x0600054F RID: 1359 RVA: 0x00019699 File Offset: 0x00017899
		protected void OnContextChange(string fileContext, int line, int column)
		{
			this.m_context.Reset(line, column);
			if (this.ContextChange != null)
			{
				this.ContextChange(this, new CssScannerContextChangeEventArgs(fileContext));
			}
		}

		// Token: 0x040001B7 RID: 439
		private const string c_scanIncludes = "~=";

		// Token: 0x040001B8 RID: 440
		private const string c_dashMatch = "|=";

		// Token: 0x040001B9 RID: 441
		private const string c_prefixMatch = "^=";

		// Token: 0x040001BA RID: 442
		private const string c_suffixMatch = "$=";

		// Token: 0x040001BB RID: 443
		private const string c_substringMatch = "*=";

		// Token: 0x040001BC RID: 444
		private const string c_commentStart = "<!--";

		// Token: 0x040001BD RID: 445
		private const string c_commentEnd = "-->";

		// Token: 0x040001BE RID: 446
		private TextReader m_reader;

		// Token: 0x040001BF RID: 447
		private string m_readAhead;

		// Token: 0x040001C0 RID: 448
		private char m_currentChar;

		// Token: 0x040001C1 RID: 449
		private string m_rawNumber;

		// Token: 0x040001C2 RID: 450
		private CssContext m_context;

		// Token: 0x040001C3 RID: 451
		private static Regex s_leadingZeros = new Regex("^0*([0-9]+?)$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040001C4 RID: 452
		private static Regex s_trailingZeros = new Regex("^([0-9]+?)0*$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040001C5 RID: 453
		private static Regex s_sourceDirective = new Regex("#SOURCE\\s+(?<line>\\d+)\\s+(?<col>\\d+)\\s+(?<path>.*)\\s*\\*/", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040001C6 RID: 454
		private bool m_isAtEOF;
	}
}

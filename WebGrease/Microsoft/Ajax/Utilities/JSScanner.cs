using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000A6 RID: 166
	public sealed class JSScanner
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00032258 File Offset: 0x00030458
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x00032260 File Offset: 0x00030460
		internal ICollection<string> DebugLookupCollection { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x00032269 File Offset: 0x00030469
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x00032271 File Offset: 0x00030471
		public bool UsePreprocessorDefines { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0003227A File Offset: 0x0003047A
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x00032282 File Offset: 0x00030482
		public bool IgnoreConditionalCompilation { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x0003228B File Offset: 0x0003048B
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x00032293 File Offset: 0x00030493
		public bool AllowEmbeddedAspNetBlocks { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0003229C File Offset: 0x0003049C
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x000322A4 File Offset: 0x000304A4
		public bool StripDebugCommentBlocks { get; set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x000322AD File Offset: 0x000304AD
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x000322B5 File Offset: 0x000304B5
		public bool SuppressErrors { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x000322BE File Offset: 0x000304BE
		public int CurrentLine
		{
			get
			{
				return this.m_currentLine;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x000322C6 File Offset: 0x000304C6
		public bool IsEndOfFile
		{
			get
			{
				return this.m_currentPosition >= this.m_endPos;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x000322D9 File Offset: 0x000304D9
		public int StartLinePosition
		{
			get
			{
				return this.m_startLinePosition;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x000322E1 File Offset: 0x000304E1
		public bool LiteralHasIssues
		{
			get
			{
				return this.m_literalIssues;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x000322E9 File Offset: 0x000304E9
		public string StringLiteralValue
		{
			get
			{
				return this.m_decodedString;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x000322F1 File Offset: 0x000304F1
		public string Identifier
		{
			get
			{
				if (this.m_identifier.Length <= 0)
				{
					return this.m_currentToken.Code;
				}
				return this.m_identifier.ToString();
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00032318 File Offset: 0x00030518
		public Context CurrentToken
		{
			get
			{
				return this.m_currentToken;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00032320 File Offset: 0x00030520
		private bool IsAtEndOfLine
		{
			get
			{
				return this.IsEndLineOrEOF(this.GetChar(this.m_currentPosition), 0);
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000AB3 RID: 2739 RVA: 0x00032338 File Offset: 0x00030538
		// (remove) Token: 0x06000AB4 RID: 2740 RVA: 0x00032370 File Offset: 0x00030570
		public event EventHandler<GlobalDefineEventArgs> GlobalDefine;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000AB5 RID: 2741 RVA: 0x000323A8 File Offset: 0x000305A8
		// (remove) Token: 0x06000AB6 RID: 2742 RVA: 0x000323E0 File Offset: 0x000305E0
		public event EventHandler<NewModuleEventArgs> NewModule;

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00032418 File Offset: 0x00030618
		public JSScanner(DocumentContext sourceContext)
		{
			if (sourceContext == null)
			{
				throw new ArgumentNullException("sourceContext");
			}
			this.m_currentToken = new Context(sourceContext)
			{
				EndPosition = 0
			};
			this.m_currentLine = 1;
			this.m_strSourceCode = sourceContext.Source;
			this.m_endPos = sourceContext.Source.Length;
			this.UsePreprocessorDefines = true;
			this.StripDebugCommentBlocks = true;
			this.m_identifier = new StringBuilder(128);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00032490 File Offset: 0x00030690
		private JSScanner(IDictionary<string, string> defines)
		{
			this.SetPreprocessorDefines(defines);
			this.m_decodedString = null;
			this.m_identifier = new StringBuilder(128);
			this.DebugLookupCollection = new HashSet<string>();
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000324C4 File Offset: 0x000306C4
		public JSScanner Clone()
		{
			return new JSScanner(this.m_defines)
			{
				AllowEmbeddedAspNetBlocks = this.AllowEmbeddedAspNetBlocks,
				IgnoreConditionalCompilation = this.IgnoreConditionalCompilation,
				m_conditionalCompilationIfLevel = this.m_conditionalCompilationIfLevel,
				m_conditionalCompilationOn = this.m_conditionalCompilationOn,
				m_currentLine = this.m_currentLine,
				m_currentPosition = this.m_currentPosition,
				m_currentToken = this.m_currentToken.Clone(),
				m_endPos = this.m_endPos,
				m_ifDirectiveLevel = this.m_ifDirectiveLevel,
				m_inConditionalComment = this.m_inConditionalComment,
				m_inMultipleLineComment = this.m_inMultipleLineComment,
				m_inSingleLineComment = this.m_inSingleLineComment,
				m_lastPosOnBuilder = this.m_lastPosOnBuilder,
				m_startLinePosition = this.m_startLinePosition,
				m_strSourceCode = this.m_strSourceCode,
				UsePreprocessorDefines = this.UsePreprocessorDefines,
				StripDebugCommentBlocks = this.StripDebugCommentBlocks
			};
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x000325B0 File Offset: 0x000307B0
		public void SetPreprocessorDefines(IDictionary<string, string> defines)
		{
			if (defines != null && defines.Count > 0)
			{
				this.m_defines = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
				using (IEnumerator<KeyValuePair<string, string>> enumerator = defines.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, string> keyValuePair = enumerator.Current;
						if (JSScanner.IsValidIdentifier(keyValuePair.Key) && !this.m_defines.ContainsKey(keyValuePair.Key))
						{
							this.m_defines.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
					return;
				}
			}
			this.m_defines = null;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00032650 File Offset: 0x00030850
		public Context ScanNextToken()
		{
			this.m_currentToken.StartPosition = this.m_currentPosition;
			this.m_currentToken.StartLineNumber = this.m_currentLine;
			this.m_currentToken.StartLinePosition = this.m_startLinePosition;
			this.m_identifier.Length = 0;
			this.m_mightBeKeyword = false;
			char @char = this.GetChar(this.m_currentPosition);
			JSToken jstoken;
			switch (@char)
			{
			case '\t':
			case '\v':
			case '\f':
			case ' ':
				jstoken = JSToken.WhiteSpace;
				while (JSScanner.IsBlankSpace(this.GetChar(++this.m_currentPosition)))
				{
				}
				goto IL_E53;
			case '\n':
			case '\r':
				jstoken = this.ScanLineTerminator(@char);
				goto IL_E53;
			case '!':
				jstoken = JSToken.LogicalNot;
				if ('=' != this.GetChar(++this.m_currentPosition))
				{
					goto IL_E53;
				}
				jstoken = JSToken.NotEqual;
				if ('=' == this.GetChar(++this.m_currentPosition))
				{
					this.m_currentPosition++;
					jstoken = JSToken.StrictNotEqual;
					goto IL_E53;
				}
				goto IL_E53;
			case '"':
			case '\'':
				jstoken = JSToken.StringLiteral;
				this.ScanString(@char);
				goto IL_E53;
			case '#':
				this.m_currentPosition++;
				jstoken = this.IllegalCharacter();
				goto IL_E53;
			case '$':
			case '_':
				jstoken = this.ScanIdentifier(true);
				goto IL_E53;
			case '%':
				jstoken = JSToken.Modulo;
				if ('=' == this.GetChar(++this.m_currentPosition))
				{
					this.m_currentPosition++;
					jstoken = JSToken.ModuloAssign;
					goto IL_E53;
				}
				goto IL_E53;
			case '&':
				jstoken = JSToken.BitwiseAnd;
				@char = this.GetChar(++this.m_currentPosition);
				if ('&' == @char)
				{
					this.m_currentPosition++;
					jstoken = JSToken.LogicalAnd;
					goto IL_E53;
				}
				if ('=' == @char)
				{
					this.m_currentPosition++;
					jstoken = JSToken.BitwiseAndAssign;
					goto IL_E53;
				}
				goto IL_E53;
			case '(':
				jstoken = JSToken.LeftParenthesis;
				this.m_currentPosition++;
				goto IL_E53;
			case ')':
				jstoken = JSToken.RightParenthesis;
				this.m_currentPosition++;
				goto IL_E53;
			case '*':
				jstoken = JSToken.Multiply;
				if ('=' == this.GetChar(++this.m_currentPosition))
				{
					this.m_currentPosition++;
					jstoken = JSToken.MultiplyAssign;
					goto IL_E53;
				}
				goto IL_E53;
			case '+':
				jstoken = JSToken.FirstBinaryOperator;
				@char = this.GetChar(++this.m_currentPosition);
				if ('+' == @char)
				{
					this.m_currentPosition++;
					jstoken = JSToken.Increment;
					goto IL_E53;
				}
				if ('=' == @char)
				{
					this.m_currentPosition++;
					jstoken = JSToken.PlusAssign;
					goto IL_E53;
				}
				goto IL_E53;
			case ',':
				jstoken = JSToken.Comma;
				this.m_currentPosition++;
				goto IL_E53;
			case '-':
				jstoken = JSToken.Minus;
				@char = this.GetChar(++this.m_currentPosition);
				if ('-' == @char)
				{
					this.m_currentPosition++;
					jstoken = JSToken.Decrement;
					goto IL_E53;
				}
				if ('=' == @char)
				{
					this.m_currentPosition++;
					jstoken = JSToken.MinusAssign;
					goto IL_E53;
				}
				goto IL_E53;
			case '.':
				jstoken = JSToken.AccessField;
				@char = this.GetChar(++this.m_currentPosition);
				if (@char == '.' && this.GetChar(++this.m_currentPosition) == '.')
				{
					jstoken = JSToken.RestSpread;
					this.m_currentPosition++;
					goto IL_E53;
				}
				if (JSScanner.IsDigit(@char))
				{
					jstoken = this.ScanNumber('.');
					goto IL_E53;
				}
				goto IL_E53;
			case '/':
			{
				jstoken = JSToken.Divide;
				@char = this.GetChar(++this.m_currentPosition);
				char c = @char;
				if (c != '*')
				{
					if (c != '/')
					{
						if (c != '=')
						{
							goto IL_E53;
						}
						this.m_currentPosition++;
						jstoken = JSToken.DivideAssign;
						goto IL_E53;
					}
					else
					{
						jstoken = JSToken.SingleLineComment;
						this.m_inSingleLineComment = true;
						@char = this.GetChar(++this.m_currentPosition);
						if (@char == '/')
						{
							if (this.GetChar(++this.m_currentPosition) == '#')
							{
								jstoken = JSToken.PreprocessorDirective;
								if (!this.ScanPreprocessingDirective())
								{
									goto IL_E53;
								}
							}
						}
						else if (@char == '@' && !this.IgnoreConditionalCompilation && (this.m_conditionalCompilationOn || this.CheckSubstring(this.m_currentPosition + 1, "cc_on")))
						{
							if (!JSScanner.IsValidIdentifierStart(this.m_strSourceCode, this.m_currentPosition + 1))
							{
								this.m_currentPosition++;
							}
							this.m_inConditionalComment = true;
							jstoken = JSToken.ConditionalCommentStart;
							goto IL_E53;
						}
						this.SkipSingleLineComment();
						if (!this.m_inMultipleLineComment && this.m_inConditionalComment)
						{
							this.m_inConditionalComment = false;
							jstoken = JSToken.ConditionalCommentEnd;
							goto IL_E53;
						}
						goto IL_E53;
					}
				}
				else
				{
					this.m_inMultipleLineComment = true;
					if (this.GetChar(++this.m_currentPosition) != '@' || this.IgnoreConditionalCompilation)
					{
						this.SkipMultilineComment();
						jstoken = JSToken.MultipleLineComment;
						goto IL_E53;
					}
					if (!this.m_conditionalCompilationOn && !this.CheckSubstring(this.m_currentPosition + 1, "cc_on"))
					{
						this.SkipMultilineComment();
						jstoken = JSToken.MultipleLineComment;
						goto IL_E53;
					}
					if (!JSScanner.IsValidIdentifierStart(this.m_strSourceCode, this.m_currentPosition + 1))
					{
						this.m_currentPosition++;
					}
					this.m_inConditionalComment = true;
					jstoken = JSToken.ConditionalCommentStart;
					goto IL_E53;
				}
				break;
			}
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
				this.m_currentPosition++;
				jstoken = this.ScanNumber(@char);
				goto IL_E53;
			case ':':
				jstoken = JSToken.Colon;
				this.m_currentPosition++;
				goto IL_E53;
			case ';':
				jstoken = JSToken.Semicolon;
				this.m_currentPosition++;
				goto IL_E53;
			case '<':
				if (this.AllowEmbeddedAspNetBlocks && '%' == this.GetChar(++this.m_currentPosition))
				{
					jstoken = this.ScanAspNetBlock();
					goto IL_E53;
				}
				jstoken = JSToken.LessThan;
				if ('<' == this.GetChar(++this.m_currentPosition))
				{
					this.m_currentPosition++;
					jstoken = JSToken.LeftShift;
				}
				if ('=' == this.GetChar(this.m_currentPosition))
				{
					this.m_currentPosition++;
					jstoken = ((jstoken == JSToken.LessThan) ? JSToken.LessThanEqual : JSToken.LeftShiftAssign);
					goto IL_E53;
				}
				goto IL_E53;
			case '=':
				jstoken = JSToken.Assign;
				if ('=' == this.GetChar(++this.m_currentPosition))
				{
					jstoken = JSToken.Equal;
					if ('=' == this.GetChar(++this.m_currentPosition))
					{
						this.m_currentPosition++;
						jstoken = JSToken.StrictEqual;
						goto IL_E53;
					}
					goto IL_E53;
				}
				else
				{
					if (this.GetChar(this.m_currentPosition) == '>')
					{
						this.m_currentPosition++;
						jstoken = JSToken.ArrowFunction;
						goto IL_E53;
					}
					goto IL_E53;
				}
				break;
			case '>':
				jstoken = JSToken.GreaterThan;
				if ('>' == this.GetChar(++this.m_currentPosition))
				{
					jstoken = JSToken.RightShift;
					if ('>' == this.GetChar(++this.m_currentPosition))
					{
						this.m_currentPosition++;
						jstoken = JSToken.UnsignedRightShift;
					}
				}
				if ('=' == this.GetChar(this.m_currentPosition))
				{
					this.m_currentPosition++;
					jstoken = ((jstoken == JSToken.GreaterThan) ? JSToken.GreaterThanEqual : ((jstoken == JSToken.RightShift) ? JSToken.RightShiftAssign : ((jstoken == JSToken.UnsignedRightShift) ? JSToken.UnsignedRightShiftAssign : JSToken.Error)));
					goto IL_E53;
				}
				goto IL_E53;
			case '?':
				jstoken = JSToken.ConditionalIf;
				this.m_currentPosition++;
				goto IL_E53;
			case '@':
			{
				if (this.IgnoreConditionalCompilation)
				{
					this.m_currentPosition++;
					jstoken = this.IllegalCharacter();
					goto IL_E53;
				}
				int num = ++this.m_currentPosition;
				this.ScanIdentifier(false);
				switch (this.m_currentPosition - num)
				{
				case 0:
					if ('*' == this.GetChar(this.m_currentPosition) && '/' == this.GetChar(this.m_currentPosition + 1))
					{
						this.m_currentPosition += 2;
						this.m_inMultipleLineComment = false;
						this.m_inConditionalComment = false;
						jstoken = JSToken.ConditionalCommentEnd;
						goto IL_E53;
					}
					jstoken = this.IllegalCharacter();
					goto IL_E53;
				case 2:
					if (this.CheckSubstring(num, "if"))
					{
						jstoken = JSToken.ConditionalCompilationIf;
						this.m_conditionalCompilationIfLevel++;
						if (!this.m_inConditionalComment && !this.m_conditionalCompilationOn)
						{
							this.m_conditionalCompilationOn = true;
							goto IL_E53;
						}
						goto IL_E53;
					}
					break;
				case 3:
					if (this.CheckSubstring(num, "set"))
					{
						jstoken = JSToken.ConditionalCompilationSet;
						if (!this.m_inConditionalComment && !this.m_conditionalCompilationOn)
						{
							this.m_conditionalCompilationOn = true;
							goto IL_E53;
						}
						goto IL_E53;
					}
					else if (this.CheckSubstring(num, "end"))
					{
						jstoken = JSToken.ConditionalCompilationEnd;
						if (this.m_conditionalCompilationIfLevel > 0)
						{
							this.m_conditionalCompilationIfLevel--;
							goto IL_E53;
						}
						this.HandleError(JSError.CCInvalidEnd);
						goto IL_E53;
					}
					break;
				case 4:
					if (this.CheckSubstring(num, "else"))
					{
						jstoken = JSToken.ConditionalCompilationElse;
						if (this.m_conditionalCompilationIfLevel <= 0)
						{
							this.HandleError(JSError.CCInvalidElse);
							goto IL_E53;
						}
						goto IL_E53;
					}
					else if (this.CheckSubstring(num, "elif"))
					{
						jstoken = JSToken.ConditionalCompilationElseIf;
						if (this.m_conditionalCompilationIfLevel <= 0)
						{
							this.HandleError(JSError.CCInvalidElseIf);
							goto IL_E53;
						}
						goto IL_E53;
					}
					break;
				case 5:
					if (this.CheckSubstring(num, "cc_on"))
					{
						this.m_conditionalCompilationOn = true;
						jstoken = JSToken.ConditionalCompilationOn;
						goto IL_E53;
					}
					break;
				}
				if (!this.m_conditionalCompilationOn)
				{
					this.HandleError(JSError.CCOff);
				}
				jstoken = JSToken.ConditionalCompilationVariable;
				goto IL_E53;
			}
			case 'A':
			case 'B':
			case 'C':
			case 'D':
			case 'E':
			case 'F':
			case 'G':
			case 'H':
			case 'I':
			case 'J':
			case 'K':
			case 'L':
			case 'M':
			case 'N':
			case 'O':
			case 'P':
			case 'Q':
			case 'R':
			case 'S':
			case 'T':
			case 'U':
			case 'V':
			case 'W':
			case 'X':
			case 'Y':
			case 'Z':
				jstoken = this.ScanIdentifier(true);
				goto IL_E53;
			case '[':
				jstoken = JSToken.LeftBracket;
				this.m_currentPosition++;
				goto IL_E53;
			case '\\':
				jstoken = this.ScanIdentifier(true);
				if (jstoken == JSToken.Identifier)
				{
					goto IL_E53;
				}
				if (this.GetChar(this.m_currentPosition + 1) == 'u')
				{
					int currentPosition = this.m_currentPosition;
					JSScanner.PeekUnicodeEscape(this.m_strSourceCode, ref this.m_currentPosition);
					int num2 = this.m_currentPosition - currentPosition;
					if (num2 > 1)
					{
						this.HandleError(JSError.IllegalChar);
						goto IL_E53;
					}
					jstoken = this.ScanIdentifier(true);
					this.HandleError(JSError.BadHexEscapeSequence);
					goto IL_E53;
				}
				else
				{
					if (JSScanner.IsValidIdentifierStart(this.m_strSourceCode, this.m_currentPosition + 1))
					{
						this.m_currentPosition++;
						jstoken = this.ScanIdentifier(true);
						goto IL_E53;
					}
					this.m_currentPosition++;
					this.HandleError(JSError.IllegalChar);
					goto IL_E53;
				}
				break;
			case ']':
				jstoken = JSToken.RightBracket;
				this.m_currentPosition++;
				goto IL_E53;
			case '^':
				jstoken = JSToken.BitwiseXor;
				if ('=' == this.GetChar(++this.m_currentPosition))
				{
					this.m_currentPosition++;
					jstoken = JSToken.BitwiseXorAssign;
					goto IL_E53;
				}
				goto IL_E53;
			case '`':
				jstoken = this.ScanTemplateLiteral(@char);
				goto IL_E53;
			case 'a':
			case 'b':
			case 'c':
			case 'd':
			case 'e':
			case 'f':
			case 'g':
			case 'h':
			case 'i':
			case 'j':
			case 'k':
			case 'l':
			case 'm':
			case 'n':
			case 'o':
			case 'p':
			case 'q':
			case 'r':
			case 's':
			case 't':
			case 'u':
			case 'v':
			case 'w':
			case 'x':
			case 'y':
			case 'z':
				this.m_mightBeKeyword = true;
				jstoken = this.ScanKeyword(JSScanner.s_Keywords[(int)(@char - 'a')]);
				goto IL_E53;
			case '{':
				jstoken = JSToken.LeftCurly;
				this.m_currentPosition++;
				goto IL_E53;
			case '|':
				jstoken = JSToken.BitwiseOr;
				@char = this.GetChar(++this.m_currentPosition);
				if ('|' == @char)
				{
					this.m_currentPosition++;
					jstoken = JSToken.LogicalOr;
					goto IL_E53;
				}
				if ('=' == @char)
				{
					this.m_currentPosition++;
					jstoken = JSToken.BitwiseOrAssign;
					goto IL_E53;
				}
				goto IL_E53;
			case '}':
				jstoken = JSToken.RightCurly;
				this.m_currentPosition++;
				goto IL_E53;
			case '~':
				jstoken = JSToken.BitwiseNot;
				this.m_currentPosition++;
				goto IL_E53;
			}
			if (@char == '\0')
			{
				if (this.IsEndOfFile)
				{
					jstoken = JSToken.EndOfFile;
					if (this.m_conditionalCompilationIfLevel > 0)
					{
						this.m_currentToken.EndLineNumber = this.m_currentLine;
						this.m_currentToken.EndLinePosition = this.m_startLinePosition;
						this.m_currentToken.EndPosition = this.m_currentPosition;
						this.HandleError(JSError.NoCCEnd);
					}
				}
				else
				{
					this.m_currentPosition++;
					jstoken = this.IllegalCharacter();
				}
			}
			else if (@char == '\u2028' || @char == '\u2029')
			{
				jstoken = this.ScanLineTerminator(@char);
			}
			else if ('\ud800' <= @char && @char <= '\udbff')
			{
				char char2 = this.GetChar(this.m_currentPosition + 1);
				if ('\udc00' <= char2 && char2 <= '\udfff')
				{
					jstoken = this.ScanIdentifier(true);
					if (jstoken != JSToken.Identifier)
					{
						this.m_currentPosition += 2;
						jstoken = this.IllegalCharacter();
					}
				}
				else
				{
					this.m_currentPosition++;
					jstoken = this.IllegalCharacter();
				}
			}
			else if (JSScanner.IsValidIdentifierStart(this.m_strSourceCode, this.m_currentPosition))
			{
				jstoken = this.ScanIdentifier(true);
			}
			else if (JSScanner.IsBlankSpace(@char))
			{
				jstoken = JSToken.WhiteSpace;
				while (JSScanner.IsBlankSpace(this.GetChar(++this.m_currentPosition)))
				{
				}
			}
			else
			{
				this.m_currentPosition++;
				jstoken = this.IllegalCharacter();
			}
			IL_E53:
			this.m_currentToken.EndLineNumber = this.m_currentLine;
			this.m_currentToken.EndLinePosition = this.m_startLinePosition;
			this.m_currentToken.EndPosition = this.m_currentPosition;
			this.m_currentToken.Token = jstoken;
			return this.m_currentToken;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000334F8 File Offset: 0x000316F8
		public Context UpdateToken(UpdateHint updateHint)
		{
			if (updateHint == UpdateHint.RegularExpression && this.m_currentToken.IsOne(new JSToken[]
			{
				JSToken.Divide,
				JSToken.DivideAssign
			}))
			{
				this.m_currentToken.Token = this.ScanRegExp();
			}
			else if (updateHint == UpdateHint.TemplateLiteral && this.m_currentToken.Is(JSToken.RightCurly))
			{
				this.m_currentToken.Token = this.ScanTemplateLiteral('}');
			}
			else if (updateHint == UpdateHint.ReplacementToken && this.m_currentToken.Is(JSToken.Modulo))
			{
				this.m_currentToken.Token = this.ScanReplacementToken();
			}
			this.m_currentToken.EndLineNumber = this.m_currentLine;
			this.m_currentToken.EndLinePosition = this.m_startLinePosition;
			this.m_currentToken.EndPosition = this.m_currentPosition;
			return this.m_currentToken;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000335BF File Offset: 0x000317BF
		public static bool IsDigit(char character)
		{
			return '0' <= character && character <= '9';
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x000335D0 File Offset: 0x000317D0
		public static bool IsKeyword(string name, bool strictMode)
		{
			bool result = false;
			if (name != null)
			{
				int num = (int)(name[0] - 'a');
				if (0 <= num && num < JSScanner.s_Keywords.Length)
				{
					JSKeyword jskeyword = JSScanner.s_Keywords[(int)(name[0] - 'a')];
					if (jskeyword != null)
					{
						JSToken keyword = jskeyword.GetKeyword(name, 0, name.Length);
						if (keyword != JSToken.Identifier)
						{
							switch (keyword)
							{
							case JSToken.Let:
							case JSToken.Implements:
							case JSToken.Interface:
							case JSToken.Package:
							case JSToken.Private:
							case JSToken.Protected:
							case JSToken.Public:
							case JSToken.Static:
							case JSToken.Yield:
								return strictMode;
							case JSToken.Get:
							case JSToken.Set:
								goto IL_84;
							}
							return true;
						}
						IL_84:
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0003366C File Offset: 0x0003186C
		public static bool IsValidIdentifier(string name)
		{
			bool result = false;
			if (name != null)
			{
				int i = 0;
				if (JSScanner.IsValidIdentifierStart(name, ref i))
				{
					result = true;
					while (i < name.Length)
					{
						if (!JSScanner.IsValidIdentifierPart(name, ref i))
						{
							result = false;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000336A6 File Offset: 0x000318A6
		private static bool IsValidIdentifierStart(string text, int index)
		{
			return JSScanner.IsValidIdentifierStart(text, ref index);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000336B0 File Offset: 0x000318B0
		private static bool IsValidIdentifierStart(string name, ref int startIndex)
		{
			bool result = false;
			if (name != null && startIndex < name.Length)
			{
				int num = startIndex;
				char c = name[num];
				if (c == '\\')
				{
					name = JSScanner.PeekUnicodeEscape(name, ref num);
					if (name != null && JSScanner.IsValidIdentifierStart(name, 0, name.Length))
					{
						startIndex = num;
						result = true;
					}
				}
				else
				{
					if ('\ud800' <= c && c <= '\udbff')
					{
						c = name[++num];
						if ('\udc00' <= c && c <= '\udfff')
						{
							num++;
						}
					}
					else
					{
						num++;
					}
					if (JSScanner.IsValidIdentifierStart(name, startIndex, num - startIndex))
					{
						startIndex = num;
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0003374C File Offset: 0x0003194C
		private static bool IsValidIdentifierPart(string text, int index)
		{
			return JSScanner.IsValidIdentifierPart(text, ref index);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00033758 File Offset: 0x00031958
		private static bool IsValidIdentifierPart(string name, ref int startIndex)
		{
			bool result = false;
			if (name != null && startIndex < name.Length)
			{
				int num = startIndex;
				char c = name[num];
				if (c == '\\')
				{
					name = JSScanner.PeekUnicodeEscape(name, ref num);
					if (name != null && JSScanner.IsValidIdentifierPart(name, 0, name.Length))
					{
						startIndex = num;
						result = true;
					}
				}
				else
				{
					if ('\ud800' <= c && c <= '\udbff')
					{
						c = name[++num];
						if ('\udc00' <= c && c <= '\udfff')
						{
							num++;
						}
					}
					else
					{
						num++;
					}
					if (JSScanner.IsValidIdentifierPart(name, startIndex, num - startIndex))
					{
						startIndex = num;
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000337F4 File Offset: 0x000319F4
		private static bool IsValidIdentifierStart(string text, int index, int length)
		{
			if (text != null)
			{
				char c = text[index];
				if (length == 1 && (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z') || c == '_' || c == '$' || c == '�'))
				{
					return true;
				}
				switch (char.GetUnicodeCategory(text, index))
				{
				case UnicodeCategory.UppercaseLetter:
				case UnicodeCategory.LowercaseLetter:
				case UnicodeCategory.TitlecaseLetter:
				case UnicodeCategory.ModifierLetter:
				case UnicodeCategory.OtherLetter:
				case UnicodeCategory.LetterNumber:
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00033874 File Offset: 0x00031A74
		private static bool IsValidIdentifierPart(string text, int index, int length)
		{
			if (text != null)
			{
				char c = text[index];
				if (length == 1 && (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z') || ('0' <= c && c <= '9') || c == '_' || c == '$' || c == '‌' || c == '‍' || c == '�'))
				{
					return true;
				}
				switch (char.GetUnicodeCategory(text, index))
				{
				case UnicodeCategory.UppercaseLetter:
				case UnicodeCategory.LowercaseLetter:
				case UnicodeCategory.TitlecaseLetter:
				case UnicodeCategory.ModifierLetter:
				case UnicodeCategory.OtherLetter:
				case UnicodeCategory.NonSpacingMark:
				case UnicodeCategory.SpacingCombiningMark:
				case UnicodeCategory.DecimalDigitNumber:
				case UnicodeCategory.LetterNumber:
				case UnicodeCategory.ConnectorPunctuation:
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00033934 File Offset: 0x00031B34
		public static bool StartsWithValidIdentifierPart(string text)
		{
			bool result = false;
			if (text != null)
			{
				char c = text[0];
				if (c == '\\')
				{
					int num = 0;
					string text2 = JSScanner.PeekUnicodeEscape(text, ref num);
					result = (text2 != null && JSScanner.IsValidIdentifierPart(text2, 0, text2.Length));
				}
				else
				{
					result = JSScanner.IsValidIdentifierPart(text, 0, 1);
				}
			}
			return result;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0003397E File Offset: 0x00031B7E
		public static bool IsValidIdentifierPart(char letter)
		{
			return JSScanner.IsValidIdentifierPart(new string(letter, 1), 0);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0003398D File Offset: 0x00031B8D
		public static bool IsAssignmentOperator(JSToken token)
		{
			return JSToken.Assign <= token && token <= JSToken.UnsignedRightShiftAssign;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0003399E File Offset: 0x00031B9E
		public static bool IsRightAssociativeOperator(JSToken token)
		{
			return JSToken.Assign <= token && token <= JSToken.ConditionalIf;
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000339B0 File Offset: 0x00031BB0
		public static bool IsSafeIdentifier(string name)
		{
			bool result = false;
			if (!string.IsNullOrEmpty(name) && JSScanner.IsSafeIdentifierStart(name[0]))
			{
				for (int i = 1; i < name.Length; i++)
				{
					char letter = name[i];
					if (!JSScanner.IsSafeIdentifierPart(letter))
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000339FB File Offset: 0x00031BFB
		public static bool IsSafeIdentifierStart(char letter)
		{
			return ('a' <= letter && letter <= 'z') || ('A' <= letter && letter <= 'Z') || letter == '_' || letter == '$';
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00033A1E File Offset: 0x00031C1E
		public static bool IsSafeIdentifierPart(char letter)
		{
			return ('a' <= letter && letter <= 'z') || ('A' <= letter && letter <= 'Z') || ('0' <= letter && letter <= '9') || letter == '_' || letter == '$';
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00033A4C File Offset: 0x00031C4C
		private void OnGlobalDefine(string name)
		{
			if (this.GlobalDefine != null)
			{
				this.GlobalDefine(this, new GlobalDefineEventArgs
				{
					Name = name
				});
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00033A7C File Offset: 0x00031C7C
		private void OnNewModule(string newModule)
		{
			if (this.NewModule != null)
			{
				this.NewModule(this, new NewModuleEventArgs
				{
					Module = newModule
				});
			}
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00033AAC File Offset: 0x00031CAC
		private JSToken ScanLineTerminator(char ch)
		{
			JSToken result = JSToken.EndOfLine;
			if (this.m_inConditionalComment && this.m_inSingleLineComment)
			{
				result = JSToken.ConditionalCommentEnd;
				this.m_inConditionalComment = (this.m_inSingleLineComment = false);
			}
			else
			{
				this.m_currentPosition++;
				if (ch == '\r' && this.GetChar(this.m_currentPosition) == '\n')
				{
					this.m_currentPosition++;
				}
				this.m_currentLine++;
				this.m_startLinePosition = this.m_currentPosition;
				while ((ch = this.GetChar(this.m_currentPosition)) == '\r' || ch == '\n' || ch == '\u2028' || ch == '\u2029')
				{
					if (ch == '\r')
					{
						if (this.GetChar(++this.m_currentPosition) == '\n')
						{
							this.m_currentPosition++;
						}
					}
					else
					{
						this.m_currentPosition++;
					}
					this.m_currentLine++;
					this.m_startLinePosition = this.m_currentPosition;
				}
			}
			this.m_inSingleLineComment = false;
			return result;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00033BBC File Offset: 0x00031DBC
		private JSToken ScanIdentifier(bool possibleTemplateLiteral)
		{
			bool flag = false;
			int num = this.m_currentPosition;
			int num2 = this.m_currentPosition;
			char @char = this.GetChar(num2);
			if (@char == '\\')
			{
				this.m_mightBeKeyword = false;
				string text = JSScanner.PeekUnicodeEscape(this.m_strSourceCode, ref num2);
				if (text != null && JSScanner.IsValidIdentifierStart(text, 0, text.Length))
				{
					this.m_identifier.Append(text);
					num = (this.m_currentPosition = num2);
					flag = true;
				}
			}
			else
			{
				if ('\ud800' <= @char && @char <= '\udbff')
				{
					this.m_mightBeKeyword = false;
					@char = this.GetChar(++num2);
					if ('\udc00' <= @char && @char <= '\udfff')
					{
						num2++;
					}
				}
				else
				{
					num2++;
				}
				if (JSScanner.IsValidIdentifierStart(this.m_strSourceCode, this.m_currentPosition, num2 - this.m_currentPosition))
				{
					this.m_mightBeKeyword = (this.m_mightBeKeyword && 'a' <= @char && @char <= 'z');
					this.m_currentPosition = num2;
					flag = true;
				}
			}
			if (flag)
			{
				for (@char = this.GetChar(this.m_currentPosition); @char != '\0'; @char = this.GetChar(this.m_currentPosition))
				{
					num2 = this.m_currentPosition;
					if (@char == '\\')
					{
						this.m_mightBeKeyword = false;
						string text2 = JSScanner.PeekUnicodeEscape(this.m_strSourceCode, ref num2);
						if (text2 == null || !JSScanner.IsValidIdentifierPart(text2, 0, text2.Length))
						{
							break;
						}
						if (this.m_currentPosition > num)
						{
							this.m_identifier.Append(this.m_strSourceCode, num, this.m_currentPosition - num);
						}
						this.m_identifier.Append(text2);
						num = (this.m_currentPosition = num2);
					}
					else
					{
						if ('\ud800' <= @char && @char <= '\udbff')
						{
							this.m_mightBeKeyword = false;
							@char = this.GetChar(++num2);
							if ('\udc00' <= @char && @char <= '\udfff')
							{
								num2++;
							}
						}
						else
						{
							num2++;
						}
						if (!JSScanner.IsValidIdentifierPart(this.m_strSourceCode, this.m_currentPosition, num2 - this.m_currentPosition))
						{
							break;
						}
						this.m_mightBeKeyword = (this.m_mightBeKeyword && 'a' <= @char && @char <= 'z');
						this.m_currentPosition = num2;
					}
				}
				if (this.AllowEmbeddedAspNetBlocks && this.CheckSubstring(this.m_currentPosition, "<%="))
				{
					this.m_currentPosition++;
					this.ScanAspNetBlock();
				}
				if (this.m_identifier.Length > 0 && this.m_currentPosition - num > 0)
				{
					this.m_identifier.Append(this.m_strSourceCode, num, this.m_currentPosition - num);
				}
			}
			if (possibleTemplateLiteral && flag && this.GetChar(this.m_currentPosition) == '`')
			{
				return this.ScanTemplateLiteral('`');
			}
			if (!flag)
			{
				return JSToken.Error;
			}
			return JSToken.Identifier;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00033E68 File Offset: 0x00032068
		private JSToken ScanKeyword(JSKeyword keyword)
		{
			JSToken jstoken = this.ScanIdentifier(true);
			if (keyword != null && this.m_mightBeKeyword)
			{
				if (jstoken == JSToken.Identifier)
				{
					jstoken = keyword.GetKeyword(this.m_strSourceCode, this.m_currentToken.StartPosition, this.m_currentPosition - this.m_currentToken.StartPosition);
				}
				else if (jstoken == JSToken.TemplateLiteral)
				{
					int num = this.m_strSourceCode.IndexOf('`', this.m_currentToken.StartPosition);
					JSToken keyword2 = keyword.GetKeyword(this.m_strSourceCode, this.m_currentToken.StartPosition, num - this.m_currentToken.StartPosition);
					if (keyword2 != JSToken.Identifier)
					{
						jstoken = keyword2;
						this.m_currentPosition = num;
					}
				}
			}
			return jstoken;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00033F14 File Offset: 0x00032114
		private JSToken ScanNumber(char leadChar)
		{
			bool flag = '.' == leadChar;
			JSToken jstoken = flag ? JSToken.NumericLiteral : JSToken.IntegerLiteral;
			bool flag2 = false;
			this.m_literalIssues = false;
			char @char;
			if ('0' == leadChar)
			{
				@char = this.GetChar(this.m_currentPosition);
				if ('x' == @char || 'X' == @char)
				{
					if (JSScanner.IsHexDigit(this.GetChar(this.m_currentPosition + 1)))
					{
						while (JSScanner.IsHexDigit(this.GetChar(++this.m_currentPosition)))
						{
						}
					}
					return this.CheckForNumericBadEnding(jstoken);
				}
				if ('b' == @char || 'B' == @char)
				{
					@char = this.GetChar(this.m_currentPosition + 1);
					if (@char == '1' || @char == '0')
					{
						while ('0' == (@char = this.GetChar(++this.m_currentPosition)) || @char == '1')
						{
						}
					}
					return this.CheckForNumericBadEnding(jstoken);
				}
				if ('o' == @char || 'O' == @char)
				{
					@char = this.GetChar(this.m_currentPosition + 1);
					if ('0' <= @char && @char <= '7')
					{
						while ('0' <= (@char = this.GetChar(++this.m_currentPosition)) && @char <= '7')
						{
						}
					}
					return this.CheckForNumericBadEnding(jstoken);
				}
				if ('0' <= @char && @char <= '7')
				{
					while ('0' <= @char && @char <= '7')
					{
						@char = this.GetChar(++this.m_currentPosition);
					}
					if (JSScanner.IsDigit(@char) && '7' < @char)
					{
						this.m_literalIssues = true;
						while ('0' <= @char && @char <= '9')
						{
							@char = this.GetChar(++this.m_currentPosition);
						}
						this.HandleError(JSError.BadNumericLiteral);
					}
					this.m_literalIssues = true;
					this.HandleError(JSError.OctalLiteralsDeprecated);
					return jstoken;
				}
				if (@char != 'e' && @char != 'E' && JSScanner.IsValidIdentifierStart(this.m_strSourceCode, this.m_currentPosition))
				{
					return this.CheckForNumericBadEnding(jstoken);
				}
			}
			for (;;)
			{
				@char = this.GetChar(this.m_currentPosition);
				if (!JSScanner.IsDigit(@char))
				{
					if ('.' == @char)
					{
						if (flag)
						{
							break;
						}
						flag = true;
						jstoken = JSToken.NumericLiteral;
					}
					else if ('e' == @char || 'E' == @char)
					{
						if (flag2)
						{
							break;
						}
						flag = (flag2 = true);
						jstoken = JSToken.NumericLiteral;
					}
					else
					{
						if ('+' != @char && '-' != @char)
						{
							break;
						}
						char char2 = this.GetChar(this.m_currentPosition - 1);
						if ('e' != char2 && 'E' != char2)
						{
							break;
						}
					}
				}
				this.m_currentPosition++;
			}
			@char = this.GetChar(this.m_currentPosition - 1);
			if ('+' == @char || '-' == @char)
			{
				this.m_currentPosition--;
				@char = this.GetChar(this.m_currentPosition - 1);
			}
			if ('e' == @char || 'E' == @char)
			{
				this.m_currentPosition--;
				@char = this.GetChar(this.m_currentPosition - 1);
			}
			if (jstoken == JSToken.NumericLiteral && @char == '.')
			{
				jstoken = JSToken.IntegerLiteral;
			}
			return this.CheckForNumericBadEnding(jstoken);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x000341C8 File Offset: 0x000323C8
		private JSToken ScanReplacementToken()
		{
			int currentPosition = this.m_currentPosition;
			int currentLine = this.m_currentLine;
			int startLinePosition = this.m_startLinePosition;
			char @char = this.GetChar(this.m_currentPosition);
			if (@char != '.')
			{
				while (JSScanner.IsValidIdentifierPart(@char) || @char == '.' || @char == '-')
				{
					@char = this.GetChar(++this.m_currentPosition);
				}
				if (@char == ':')
				{
					@char = this.GetChar(++this.m_currentPosition);
					while (JSScanner.IsValidIdentifierPart(@char))
					{
						@char = this.GetChar(++this.m_currentPosition);
					}
				}
			}
			if (@char == '%' && this.m_currentPosition > currentPosition + 1 && this.GetChar(this.m_currentPosition - 1) != '.')
			{
				this.m_currentPosition++;
				return JSToken.ReplacementToken;
			}
			this.m_currentPosition = currentPosition;
			this.m_currentLine = currentLine;
			this.m_startLinePosition = startLinePosition;
			return this.m_currentToken.Token;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x000342C0 File Offset: 0x000324C0
		private JSToken ScanRegExp()
		{
			int currentPosition = this.m_currentPosition;
			int currentLine = this.m_currentLine;
			int startLinePosition = this.m_startLinePosition;
			bool flag = false;
			bool flag2 = false;
			char @char;
			while (!this.IsEndLineOrEOF(@char = this.GetChar(this.m_currentPosition++), 0))
			{
				if (flag)
				{
					flag = false;
				}
				else if (@char == '[')
				{
					flag2 = true;
				}
				else if (flag2)
				{
					if (@char == ']')
					{
						flag2 = false;
					}
				}
				else if (@char == '/')
				{
					if (currentPosition != this.m_currentPosition)
					{
						return JSToken.RegularExpression;
					}
					break;
				}
				else if (@char == '\\')
				{
					flag = true;
				}
			}
			this.m_currentPosition = currentPosition;
			this.m_currentLine = currentLine;
			this.m_startLinePosition = startLinePosition;
			return this.m_currentToken.Token;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0003436C File Offset: 0x0003256C
		private JSToken ScanAspNetBlock()
		{
			JSToken result = JSToken.AspNetBlock;
			char @char = this.GetChar(++this.m_currentPosition);
			this.m_currentPosition++;
			while (this.GetChar(this.m_currentPosition - 1) != '%' || this.GetChar(this.m_currentPosition) != '>' || this.IsEndOfFile)
			{
				this.m_currentPosition++;
			}
			this.m_currentToken.EndPosition = this.m_currentPosition + 1;
			this.m_currentToken.EndLineNumber = this.m_currentLine;
			this.m_currentToken.EndLinePosition = this.m_startLinePosition;
			if (this.IsEndOfFile)
			{
				this.HandleError(JSError.UnterminatedAspNetBlock);
			}
			else
			{
				this.m_currentPosition++;
				if (@char == '=')
				{
					result = JSToken.Identifier;
					if (JSScanner.IsValidIdentifierPart(this.m_strSourceCode, this.m_currentPosition) || this.CheckSubstring(this.m_currentPosition, "<%="))
					{
						for (;;)
						{
							if (JSScanner.IsValidIdentifierPart(this.m_strSourceCode, ref this.m_currentPosition))
							{
								while (JSScanner.IsValidIdentifierPart(this.m_strSourceCode, ref this.m_currentPosition))
								{
								}
								this.m_currentToken.EndPosition = this.m_currentPosition;
							}
							else
							{
								if (!this.CheckSubstring(this.m_currentPosition, "<%="))
								{
									break;
								}
								this.m_currentPosition += 4;
								while (this.GetChar(this.m_currentPosition - 1) != '%' || this.GetChar(this.m_currentPosition) != '>' || this.IsEndOfFile)
								{
									this.m_currentPosition++;
								}
								this.m_currentToken.EndPosition = this.m_currentPosition + 1;
								this.m_currentToken.EndLineNumber = this.m_currentLine;
								this.m_currentToken.EndLinePosition = this.m_startLinePosition;
								if (this.IsEndOfFile)
								{
									this.HandleError(JSError.UnterminatedAspNetBlock);
								}
								else
								{
									this.m_currentPosition++;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00034564 File Offset: 0x00032764
		private void ScanString(char delimiter)
		{
			int num = ++this.m_currentPosition;
			this.m_decodedString = null;
			this.m_literalIssues = false;
			StringBuilder stringBuilder = null;
			char @char;
			while ((@char = this.GetChar(this.m_currentPosition++)) != delimiter)
			{
				if (@char == '\\')
				{
					int num2 = 0;
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(128);
					}
					if (this.m_currentPosition - num - 1 > 0)
					{
						stringBuilder.Append(this.m_strSourceCode, num, this.m_currentPosition - num - 1);
					}
					bool flag = false;
					@char = this.GetChar(this.m_currentPosition++);
					char c = @char;
					if (c <= '7')
					{
						if (c <= '\r')
						{
							if (c == '\n')
							{
								goto IL_38A;
							}
							if (c != '\r')
							{
								goto IL_56C;
							}
							if ('\n' == this.GetChar(this.m_currentPosition))
							{
								this.m_currentPosition++;
								goto IL_38A;
							}
							goto IL_38A;
						}
						else if (c != '"')
						{
							switch (c)
							{
							case '\'':
								stringBuilder.Append('\'');
								goto IL_574;
							case '(':
							case ')':
							case '*':
							case '+':
							case ',':
							case '-':
							case '.':
							case '/':
								goto IL_56C;
							case '0':
							case '1':
							case '2':
							case '3':
								flag = true;
								num2 = (int)((int)(@char - '0') << 6);
								break;
							case '4':
							case '5':
							case '6':
							case '7':
								break;
							default:
								goto IL_56C;
							}
							this.m_literalIssues = true;
							if (!flag)
							{
								num2 = (int)((int)(@char - '0') << 3);
							}
							@char = this.GetChar(this.m_currentPosition++);
							if ('0' <= @char && @char <= '7')
							{
								if (flag)
								{
									num2 |= (int)((int)(@char - '0') << 3);
									@char = this.GetChar(this.m_currentPosition++);
									if ('0' <= @char && @char <= '7')
									{
										num2 |= (int)(@char - '0');
										stringBuilder.Append((char)num2);
									}
									else
									{
										stringBuilder.Append((char)(num2 >> 3));
										this.m_currentPosition--;
									}
								}
								else
								{
									num2 |= (int)(@char - '0');
									stringBuilder.Append((char)num2);
								}
							}
							else
							{
								if (flag)
								{
									stringBuilder.Append((char)(num2 >> 6));
								}
								else
								{
									stringBuilder.Append((char)(num2 >> 3));
								}
								this.m_currentPosition--;
							}
							this.HandleError(JSError.OctalLiteralsDeprecated);
						}
						else
						{
							stringBuilder.Append('"');
						}
					}
					else if (c <= 'b')
					{
						if (c != '\\')
						{
							if (c != 'b')
							{
								goto IL_56C;
							}
							stringBuilder.Append('\b');
						}
						else
						{
							stringBuilder.Append('\\');
						}
					}
					else if (c != 'f')
					{
						switch (c)
						{
						case 'n':
							stringBuilder.Append('\n');
							break;
						case 'o':
						case 'p':
						case 'q':
						case 's':
						case 'w':
							goto IL_56C;
						case 'r':
							stringBuilder.Append('\r');
							break;
						case 't':
							stringBuilder.Append('\t');
							break;
						case 'u':
						case 'x':
						{
							string value;
							if (this.ScanHexEscape(@char, out value))
							{
								stringBuilder.Append(value);
							}
							else
							{
								stringBuilder.Append(this.m_strSourceCode.Substring(this.m_currentPosition - 2, 2));
								this.m_literalIssues = true;
								this.HandleError(JSError.BadHexEscapeSequence);
							}
							break;
						}
						case 'v':
							this.m_literalIssues = true;
							stringBuilder.Append('\v');
							break;
						default:
							switch (c)
							{
							case '\u2028':
							case '\u2029':
								goto IL_38A;
							default:
								goto IL_56C;
							}
							break;
						}
					}
					else
					{
						stringBuilder.Append('\f');
					}
					IL_574:
					num = this.m_currentPosition;
					continue;
					IL_38A:
					this.m_currentLine++;
					this.m_startLinePosition = this.m_currentPosition;
					goto IL_574;
					IL_56C:
					stringBuilder.Append(@char);
					goto IL_574;
				}
				if (this.IsLineTerminator(@char, 0))
				{
					this.HandleError(JSError.UnterminatedString);
					this.m_currentPosition--;
					if (this.GetChar(this.m_currentPosition - 1) == '\r')
					{
						this.m_currentPosition--;
						break;
					}
					break;
				}
				else
				{
					if (@char == '\0')
					{
						this.m_literalIssues = true;
						if (this.IsEndOfFile)
						{
							this.m_currentPosition--;
							this.HandleError(JSError.UnterminatedString);
							break;
						}
					}
					if (this.AllowEmbeddedAspNetBlocks && @char == '<' && this.GetChar(this.m_currentPosition) == '%')
					{
						this.SkipAspNetReplacement();
						this.m_literalIssues = true;
					}
					else if ('\ud800' <= @char && @char <= '\udbff')
					{
						@char = this.GetChar(this.m_currentPosition);
						if ('\udc00' <= @char && @char <= '\udfff')
						{
							this.m_currentPosition++;
						}
						else if (@char == '\\' && this.GetChar(this.m_currentPosition + 1) == 'u')
						{
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder(128);
							}
							if (this.m_currentPosition - num > 0)
							{
								stringBuilder.Append(this.m_strSourceCode, num, this.m_currentPosition - num);
							}
							int num3;
							if (this.ScanHexSequence(this.m_currentPosition += 2, 'u', out num3))
							{
								stringBuilder.Append((char)num3);
								num = this.m_currentPosition;
								if (num3 < 56320 || 57343 < num3)
								{
									this.m_literalIssues = true;
									this.HandleError(JSError.HighSurrogate);
								}
							}
							else
							{
								this.m_literalIssues = true;
								this.HandleError(JSError.HighSurrogate);
							}
						}
						else
						{
							this.m_literalIssues = true;
							this.HandleError(JSError.HighSurrogate);
						}
					}
					else if ('\udc00' <= @char && @char <= '\udfff')
					{
						this.m_literalIssues = true;
						this.HandleError(JSError.LowSurrogate);
					}
				}
			}
			if (stringBuilder != null)
			{
				if (this.m_currentPosition - num - 1 > 0)
				{
					stringBuilder.Append(this.m_strSourceCode, num, this.m_currentPosition - num - 1);
				}
				this.m_decodedString = stringBuilder.ToString();
				return;
			}
			if (this.m_currentPosition == this.m_currentToken.StartPosition + 1)
			{
				this.m_decodedString = string.Empty;
				return;
			}
			int num4 = (this.GetChar(this.m_currentPosition - 1) == delimiter) ? 2 : 1;
			this.m_decodedString = this.m_strSourceCode.Substring(this.m_currentToken.StartPosition + 1, this.m_currentPosition - this.m_currentToken.StartPosition - num4);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00034BB0 File Offset: 0x00032DB0
		private bool ScanHexEscape(char hexType, out string unescaped)
		{
			int currentPosition = this.m_currentPosition;
			int num;
			bool flag = this.ScanHexSequence(currentPosition, hexType, out num);
			if (flag)
			{
				if (55296 <= num && num <= 56319)
				{
					char @char = this.GetChar(this.m_currentPosition);
					if ('\udc00' <= @char && @char <= '\udfff')
					{
						this.m_currentPosition++;
						unescaped = new string(new char[]
						{
							(char)num,
							@char
						});
						return true;
					}
					if (@char == '\\' && this.GetChar(this.m_currentPosition + 1) == 'u')
					{
						this.m_currentPosition += 2;
						int num2;
						flag = this.ScanHexSequence(this.m_currentPosition, hexType, out num2);
						if (flag)
						{
							unescaped = new string(new char[]
							{
								(char)num,
								(char)num2
							});
							return true;
						}
					}
					this.HandleError(JSError.HighSurrogate);
					this.m_literalIssues = true;
					unescaped = new string((char)num, 1);
					return true;
				}
				else if (56320 <= num && num <= 57343)
				{
					this.HandleError(JSError.LowSurrogate);
					this.m_literalIssues = true;
					unescaped = new string((char)num, 1);
					return true;
				}
			}
			unescaped = (flag ? char.ConvertFromUtf32(num) : null);
			return flag;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00034CE8 File Offset: 0x00032EE8
		private bool ScanHexSequence(int startOfDigits, char hexType, out int accumulator)
		{
			bool result = true;
			int num = (hexType == 'x') ? 2 : 4;
			if (hexType == 'u' && this.GetChar(this.m_currentPosition) == '{')
			{
				this.m_currentPosition++;
				num = 6;
			}
			accumulator = 0;
			char @char = this.GetChar(this.m_currentPosition);
			while (this.m_currentPosition - startOfDigits < num && JSScanner.IsHexDigit(@char))
			{
				if (JSScanner.IsDigit(@char))
				{
					accumulator = (accumulator << 4 | (int)(@char - '0'));
				}
				else if ('A' <= @char && @char <= 'F')
				{
					accumulator = (accumulator << 4 | (int)(@char - 'A' + '\n'));
				}
				else if ('a' <= @char && @char <= 'f')
				{
					accumulator = (accumulator << 4 | (int)(@char - 'a' + '\n'));
				}
				@char = this.GetChar(++this.m_currentPosition);
			}
			int num2 = this.m_currentPosition - startOfDigits;
			if (num2 == 0 || (num != 6 && num2 != num) || (num == 6 && @char != '}'))
			{
				result = false;
				this.m_currentPosition = startOfDigits;
			}
			else if (num == 6 && @char == '}')
			{
				this.m_currentPosition++;
			}
			return result;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00034DEC File Offset: 0x00032FEC
		private JSToken ScanTemplateLiteral(char ch)
		{
			StringBuilder stringBuilder = null;
			int num = this.m_currentToken.StartPosition;
			if (ch == '`')
			{
				this.m_currentPosition++;
			}
			ch = this.GetChar(this.m_currentPosition);
			while (ch != '\0' && ch != '`')
			{
				if (ch == '$')
				{
					if (this.GetChar(this.m_currentPosition + 1) == '{')
					{
						this.m_currentPosition += 2;
						break;
					}
				}
				else
				{
					if (ch == '\\')
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(128);
						}
						if (this.m_currentPosition > num)
						{
							stringBuilder.Append(this.m_strSourceCode, num, this.m_currentPosition - num);
						}
						ch = this.GetChar(++this.m_currentPosition);
						char c = ch;
						if (c <= '0')
						{
							if (c <= '\r')
							{
								if (c == '\n')
								{
									goto IL_1DB;
								}
								if (c != '\r')
								{
									goto IL_14E;
								}
								if (this.GetChar(this.m_currentPosition + 1) == '\n')
								{
									this.m_currentPosition++;
									goto IL_1DB;
								}
								goto IL_1DB;
							}
							else
							{
								if (c == '"' || c == '\'')
								{
									goto IL_14E;
								}
								if (c != '0')
								{
									goto IL_14E;
								}
								stringBuilder.Append('\0');
							}
						}
						else if (c <= 'b')
						{
							if (c == '\\')
							{
								goto IL_14E;
							}
							if (c != 'b')
							{
								goto IL_14E;
							}
							stringBuilder.Append('\b');
						}
						else if (c != 'f')
						{
							switch (c)
							{
							case 'n':
								stringBuilder.Append('\n');
								break;
							case 'o':
							case 'p':
							case 'q':
							case 's':
							case 'w':
								goto IL_14E;
							case 'r':
								stringBuilder.Append('\r');
								break;
							case 't':
								stringBuilder.Append('\t');
								break;
							case 'u':
							case 'x':
							{
								this.m_currentPosition++;
								string value;
								if (this.ScanHexEscape(ch, out value))
								{
									stringBuilder.Append(value);
								}
								else
								{
									stringBuilder.Append(this.m_strSourceCode.Substring(this.m_currentPosition - 2, 2));
									this.m_literalIssues = true;
									this.HandleError(JSError.BadHexEscapeSequence);
								}
								this.m_currentPosition--;
								break;
							}
							case 'v':
								stringBuilder.Append('\v');
								break;
							default:
								switch (c)
								{
								case '\u2028':
								case '\u2029':
									goto IL_1DB;
								}
								goto IL_14E;
							}
						}
						else
						{
							stringBuilder.Append('\f');
						}
						IL_255:
						num = this.m_currentPosition + 1;
						goto IL_294;
						IL_14E:
						stringBuilder.Append(ch);
						goto IL_255;
						IL_1DB:
						this.m_currentLine++;
						this.m_startLinePosition = this.m_currentPosition;
						goto IL_255;
					}
					if (this.IsLineTerminator(ch, 1))
					{
						this.m_currentPosition++;
						this.m_currentLine++;
						this.m_startLinePosition = this.m_currentPosition + 1;
					}
				}
				IL_294:
				ch = this.GetChar(++this.m_currentPosition);
			}
			if (ch == '`')
			{
				this.m_currentPosition++;
			}
			if (stringBuilder != null)
			{
				if (this.m_currentPosition > num)
				{
					stringBuilder.Append(this.m_strSourceCode, num, this.m_currentPosition - num);
				}
				this.m_decodedString = stringBuilder.ToString();
			}
			else
			{
				this.m_decodedString = this.m_strSourceCode.Substring(this.m_currentToken.StartPosition, this.m_currentPosition - this.m_currentToken.StartPosition);
			}
			return JSToken.TemplateLiteral;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00035128 File Offset: 0x00033328
		private void SkipAspNetReplacement()
		{
			this.m_currentPosition++;
			char @char;
			while ((@char = this.GetChar(this.m_currentPosition++)) != '\0' || !this.IsEndOfFile)
			{
				if (@char == '%' && this.GetChar(this.m_currentPosition) == '>')
				{
					this.m_currentPosition++;
					return;
				}
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0003518C File Offset: 0x0003338C
		private void SkipSingleLineComment()
		{
			this.SkipToEndOfLine();
			this.m_inSingleLineComment = false;
			this.m_currentToken.EndPosition = this.m_currentPosition;
			this.m_currentToken.EndLinePosition = this.m_startLinePosition;
			this.m_currentToken.EndLineNumber = this.m_currentLine;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000351DC File Offset: 0x000333DC
		private void SkipToEndOfLine()
		{
			char @char = this.GetChar(this.m_currentPosition);
			while (@char != '\0' && @char != '\n' && @char != '\r' && @char != '\u2028' && @char != '\u2029')
			{
				@char = this.GetChar(++this.m_currentPosition);
			}
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x00035230 File Offset: 0x00033430
		private void SkipOneLineTerminator()
		{
			char @char = this.GetChar(this.m_currentPosition);
			if (@char == '\r')
			{
				if (this.GetChar(++this.m_currentPosition) == '\n')
				{
					this.m_currentPosition++;
				}
				this.m_currentLine++;
				this.m_startLinePosition = this.m_currentPosition;
				return;
			}
			if (@char == '\n' || @char == '\u2028' || @char == '\u2029')
			{
				this.m_currentPosition++;
				this.m_currentLine++;
				this.m_startLinePosition = this.m_currentPosition;
			}
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000352D0 File Offset: 0x000334D0
		private void SkipMultilineComment()
		{
			for (;;)
			{
				char @char = this.GetChar(this.m_currentPosition);
				while ('*' == @char)
				{
					@char = this.GetChar(++this.m_currentPosition);
					if ('/' == @char)
					{
						goto Block_0;
					}
					if (@char == '\0')
					{
						break;
					}
					if (this.IsLineTerminator(@char, 1))
					{
						@char = this.GetChar(++this.m_currentPosition);
						this.m_currentLine++;
						this.m_startLinePosition = this.m_currentPosition + 1;
					}
				}
				if (@char == '\0' && this.IsEndOfFile)
				{
					goto IL_105;
				}
				if (this.IsLineTerminator(@char, 1))
				{
					this.m_currentLine++;
					this.m_startLinePosition = this.m_currentPosition + 1;
				}
				this.m_currentPosition++;
			}
			Block_0:
			this.m_currentPosition++;
			this.m_inMultipleLineComment = false;
			this.m_currentToken.EndPosition = this.m_currentPosition;
			this.m_currentToken.EndLinePosition = this.m_startLinePosition;
			this.m_currentToken.EndLineNumber = this.m_currentLine;
			return;
			IL_105:
			this.m_currentToken.EndPosition = this.m_currentPosition;
			this.m_currentToken.EndLinePosition = this.m_startLinePosition;
			this.m_currentToken.EndLineNumber = this.m_currentLine;
			this.m_currentToken.HandleError(JSError.NoCommentEnd, true);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00035428 File Offset: 0x00033628
		private void SkipBlanks()
		{
			char @char = this.GetChar(this.m_currentPosition);
			while (JSScanner.IsBlankSpace(@char))
			{
				@char = this.GetChar(++this.m_currentPosition);
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00035464 File Offset: 0x00033664
		private bool CheckSubstring(int startIndex, string target)
		{
			for (int i = 0; i < target.Length; i++)
			{
				if (target[i] != this.GetChar(startIndex + i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00035498 File Offset: 0x00033698
		private bool CheckCaseInsensitiveSubstring(string target)
		{
			int currentPosition = this.m_currentPosition;
			for (int i = 0; i < target.Length; i++)
			{
				if (target[i] != char.ToUpperInvariant(this.GetChar(currentPosition + i)))
				{
					return false;
				}
			}
			this.m_currentPosition += target.Length;
			return true;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x000354EC File Offset: 0x000336EC
		private JSToken CheckForNumericBadEnding(JSToken token)
		{
			bool flag = false;
			char @char = this.GetChar(this.m_currentPosition);
			if ('0' <= @char && @char <= '9')
			{
				this.m_currentPosition++;
				flag = true;
			}
			else if (JSScanner.IsValidIdentifierStart(this.m_strSourceCode, ref this.m_currentPosition))
			{
				flag = true;
			}
			if (flag)
			{
				while (JSScanner.IsValidIdentifierPart(this.m_strSourceCode, ref this.m_currentPosition))
				{
				}
				this.m_literalIssues = true;
				this.HandleError(JSError.BadNumericLiteral);
				token = JSToken.NumericLiteral;
			}
			return token;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00035566 File Offset: 0x00033766
		private char GetChar(int index)
		{
			if (index < this.m_endPos)
			{
				return this.m_strSourceCode[index];
			}
			return '\0';
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00035580 File Offset: 0x00033780
		private static int GetHexValue(char hex)
		{
			int result;
			if ('0' <= hex && hex <= '9')
			{
				result = (int)(hex - '0');
			}
			else if ('a' <= hex && hex <= 'f')
			{
				result = (int)(hex - 'a' + '\n');
			}
			else
			{
				result = (int)(hex - 'A' + '\n');
			}
			return result;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000355BC File Offset: 0x000337BC
		private static int DecodeOneUnicodeEscapeSequence(string text, ref int index)
		{
			int result = -1;
			if (text != null && index + 4 < text.Length && text[index] == '\\' && text[index + 1] == 'u')
			{
				if (text[index + 2] == '{')
				{
					char c = '\0';
					int num = 0;
					index += 2;
					while (++index < text.Length && (c = text[index]) != '}' && JSScanner.IsHexDigit(c))
					{
						num = (num << 4 | JSScanner.GetHexValue(c));
					}
					if (c == '}')
					{
						result = num;
						index++;
					}
				}
				else if (index + 5 < text.Length && JSScanner.IsHexDigit(text[index + 2]) && JSScanner.IsHexDigit(text[index + 3]) && JSScanner.IsHexDigit(text[index + 4]) && JSScanner.IsHexDigit(text[index + 5]))
				{
					result = (JSScanner.GetHexValue(text[index + 2]) << 12 | JSScanner.GetHexValue(text[index + 3]) << 8 | JSScanner.GetHexValue(text[index + 4]) << 4 | JSScanner.GetHexValue(text[index + 5]));
					index += 6;
				}
				else
				{
					index++;
				}
			}
			return result;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00035710 File Offset: 0x00033910
		private static string PeekUnicodeEscape(string text, ref int index)
		{
			int num = JSScanner.DecodeOneUnicodeEscapeSequence(text, ref index);
			if (55296 <= num && num <= 56319)
			{
				int num2 = index;
				int num3 = JSScanner.DecodeOneUnicodeEscapeSequence(text, ref index);
				if (56320 <= num3 && num3 <= 57343)
				{
					return new string(new char[]
					{
						(char)num,
						(char)num3
					});
				}
				index = num2;
				return new string(new char[]
				{
					(char)num
				});
			}
			else
			{
				if (56320 <= num && num <= 57343)
				{
					return new string(new char[]
					{
						(char)num
					});
				}
				if (num < 0 || num > 1114111)
				{
					return null;
				}
				return char.ConvertFromUtf32(num);
			}
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000357BB File Offset: 0x000339BB
		private static bool IsHexDigit(char c)
		{
			return ('0' <= c && c <= '9') || ('A' <= c && c <= 'F') || ('a' <= c && c <= 'f');
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000357E4 File Offset: 0x000339E4
		private bool IsLineTerminator(char c, int increment)
		{
			if (c == '\n')
			{
				return true;
			}
			if (c == '\r')
			{
				if ('\n' == this.GetChar(this.m_currentPosition + increment))
				{
					this.m_currentPosition++;
				}
				return true;
			}
			switch (c)
			{
			case '\u2028':
				return true;
			case '\u2029':
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0003583C File Offset: 0x00033A3C
		private bool IsEndLineOrEOF(char c, int increment)
		{
			return this.IsLineTerminator(c, increment) || (c == '\0' && this.IsEndOfFile);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00035858 File Offset: 0x00033A58
		private static bool IsBlankSpace(char c)
		{
			if (c <= ' ')
			{
				switch (c)
				{
				case '\t':
				case '\v':
				case '\f':
					break;
				case '\n':
					goto IL_39;
				default:
					if (c != ' ')
					{
						goto IL_39;
					}
					break;
				}
			}
			else if (c != '\u00a0' && c != '﻿')
			{
				goto IL_39;
			}
			return true;
			IL_39:
			return c >= '\u0080' && char.GetUnicodeCategory(c) == UnicodeCategory.SpaceSeparator;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000358B2 File Offset: 0x00033AB2
		internal static bool IsProcessableOperator(JSToken token)
		{
			return JSToken.FirstBinaryOperator <= token && token <= JSToken.ConditionalIf;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000358C4 File Offset: 0x00033AC4
		private string PPScanIdentifier(bool forceUpper)
		{
			string text = null;
			int currentPosition = this.m_currentPosition;
			if (JSScanner.IsValidIdentifierStart(this.m_strSourceCode, ref this.m_currentPosition))
			{
				while (JSScanner.IsValidIdentifierPart(this.m_strSourceCode, ref this.m_currentPosition))
				{
				}
			}
			if (this.m_currentPosition > currentPosition)
			{
				text = this.m_strSourceCode.Substring(currentPosition, this.m_currentPosition - currentPosition);
				if (forceUpper)
				{
					text = text.ToUpperInvariant();
				}
			}
			return text;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0003592C File Offset: 0x00033B2C
		private bool PPScanInteger(out int intValue)
		{
			int currentPosition = this.m_currentPosition;
			while (JSScanner.IsDigit(this.GetChar(this.m_currentPosition)))
			{
				this.m_currentPosition++;
			}
			bool result = false;
			if (this.m_currentPosition > currentPosition)
			{
				result = int.TryParse(this.m_strSourceCode.Substring(currentPosition, this.m_currentPosition - currentPosition), out intValue);
			}
			else
			{
				intValue = 0;
			}
			return result;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00035990 File Offset: 0x00033B90
		private int PPSkipToDirective(params string[] endStrings)
		{
			int currentPosition = this.m_currentPosition;
			int currentLine = this.m_currentLine;
			int startLinePosition = this.m_startLinePosition;
			for (;;)
			{
				char @char = this.GetChar(this.m_currentPosition++);
				char c = @char;
				if (c <= '\n')
				{
					if (c != '\0')
					{
						if (c == '\n')
						{
							this.m_currentLine++;
							this.m_startLinePosition = this.m_currentPosition;
						}
					}
					else if (this.IsEndOfFile)
					{
						break;
					}
				}
				else if (c != '\r')
				{
					if (c != '/')
					{
						switch (c)
						{
						case '\u2028':
							this.m_currentLine++;
							this.m_startLinePosition = this.m_currentPosition;
							break;
						case '\u2029':
							this.m_currentLine++;
							this.m_startLinePosition = this.m_currentPosition;
							break;
						}
					}
					else if (this.CheckSubstring(this.m_currentPosition, "//"))
					{
						this.m_currentPosition += 2;
						if (this.CheckCaseInsensitiveSubstring("#IFDEF") || this.CheckCaseInsensitiveSubstring("#IFNDEF") || this.CheckCaseInsensitiveSubstring("#IF"))
						{
							this.PPSkipToDirective(new string[]
							{
								"#ENDIF"
							});
						}
						else
						{
							for (int i = 0; i < endStrings.Length; i++)
							{
								if (this.CheckCaseInsensitiveSubstring(endStrings[i]))
								{
									return i;
								}
							}
							if (this.CheckCaseInsensitiveSubstring("#END"))
							{
								@char = this.GetChar(this.m_currentPosition);
								if (JSScanner.IsBlankSpace(@char) || this.IsAtEndOfLine)
								{
									return 0;
								}
							}
						}
					}
				}
				else
				{
					if (this.GetChar(this.m_currentPosition) == '\n')
					{
						this.m_currentPosition++;
					}
					this.m_currentLine++;
					this.m_startLinePosition = this.m_currentPosition;
				}
			}
			this.m_currentPosition--;
			this.m_currentToken.EndPosition = this.m_currentPosition;
			this.m_currentToken.EndLineNumber = this.m_currentLine;
			this.m_currentToken.EndLinePosition = this.m_startLinePosition;
			Context context = this.m_currentToken.Clone();
			context.EndPosition = currentPosition;
			context.EndLineNumber = currentLine;
			context.EndLinePosition = startLinePosition;
			context.HandleError((string.CompareOrdinal(endStrings[0], "#ENDDEBUG") == 0) ? JSError.NoEndDebugDirective : JSError.NoEndIfDirective, false);
			throw new EndOfStreamException();
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x00035BFC File Offset: 0x00033DFC
		private bool ScanPreprocessingDirective()
		{
			if (this.CheckCaseInsensitiveSubstring("#GLOBALS"))
			{
				return this.ScanGlobalsDirective();
			}
			if (this.CheckCaseInsensitiveSubstring("#SOURCE"))
			{
				return this.ScanSourceDirective();
			}
			if (this.UsePreprocessorDefines)
			{
				if (this.CheckCaseInsensitiveSubstring("#DEBUG"))
				{
					return this.ScanDebugDirective();
				}
				if (this.CheckCaseInsensitiveSubstring("#IF"))
				{
					return this.ScanIfDirective();
				}
				if (this.CheckCaseInsensitiveSubstring("#ELSE") && this.m_ifDirectiveLevel > 0)
				{
					return this.ScanElseDirective();
				}
				if (this.CheckCaseInsensitiveSubstring("#ENDIF") && this.m_ifDirectiveLevel > 0)
				{
					return this.ScanEndIfDirective();
				}
				if (this.CheckCaseInsensitiveSubstring("#DEFINE"))
				{
					return this.ScanDefineDirective();
				}
				if (this.CheckCaseInsensitiveSubstring("#UNDEF"))
				{
					return this.ScanUndefineDirective();
				}
			}
			return true;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x00035CC8 File Offset: 0x00033EC8
		private bool ScanGlobalsDirective()
		{
			this.SkipBlanks();
			while (!this.IsAtEndOfLine)
			{
				string text = this.PPScanIdentifier(false);
				if (text != null)
				{
					this.OnGlobalDefine(text);
				}
				this.SkipBlanks();
			}
			return true;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00035D00 File Offset: 0x00033F00
		private bool ScanSourceDirective()
		{
			this.SkipBlanks();
			int currentLine = 0;
			int num = 0;
			if (this.PPScanInteger(out currentLine))
			{
				this.SkipBlanks();
				if (this.PPScanInteger(out num))
				{
					this.SkipBlanks();
					int currentPosition = this.m_currentPosition;
					this.SkipToEndOfLine();
					if (this.m_currentPosition > currentPosition)
					{
						this.SkipOneLineTerminator();
						string text = this.m_strSourceCode.Substring(currentPosition, this.m_currentPosition - currentPosition).TrimEnd(new char[0]);
						this.m_currentToken.ChangeFileContext(text);
						this.m_currentLine = currentLine;
						this.m_startLinePosition = this.m_currentPosition - num + 1;
						this.m_currentToken.SourceOffsetStart = (this.m_currentToken.SourceOffsetEnd = this.m_currentPosition);
						this.OnNewModule(text);
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x00035DC8 File Offset: 0x00033FC8
		private bool ScanIfDirective()
		{
			bool flag = this.CheckCaseInsensitiveSubstring("DEF");
			bool flag2 = !flag && this.CheckCaseInsensitiveSubstring("NDEF");
			this.SkipBlanks();
			if (!this.IsAtEndOfLine)
			{
				string text = this.PPScanIdentifier(true);
				if (!string.IsNullOrEmpty(text))
				{
					this.m_ifDirectiveLevel++;
					bool flag3 = this.m_defines != null && this.m_defines.ContainsKey(text);
					this.SkipBlanks();
					if (flag || flag2 || this.IsAtEndOfLine)
					{
						if ((flag2 || !flag3) && (!flag2 || flag3) && this.PPSkipToDirective(new string[]
						{
							"#ENDIF",
							"#ELSE"
						}) == 0)
						{
							this.m_ifDirectiveLevel--;
						}
					}
					else
					{
						Func<string, string, bool> func = this.CheckForOperator(JSScanner.PPOperators.Instance);
						if (func != null)
						{
							this.SkipBlanks();
							int currentPosition = this.m_currentPosition;
							if (!this.IsAtEndOfLine)
							{
								this.SkipToEndOfLine();
							}
							string text2 = this.m_strSourceCode.Substring(currentPosition, this.m_currentPosition - currentPosition);
							if ((!flag3 || !func(this.m_defines[text], text2.TrimEnd(new char[0]))) && this.PPSkipToDirective(new string[]
							{
								"#ENDIF",
								"#ELSE"
							}) == 0)
							{
								this.m_ifDirectiveLevel--;
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x00035F50 File Offset: 0x00034150
		private Func<string, string, bool> CheckForOperator(SortedDictionary<string, Func<string, string, bool>> operators)
		{
			foreach (KeyValuePair<string, Func<string, string, bool>> keyValuePair in operators)
			{
				if (this.CheckCaseInsensitiveSubstring(keyValuePair.Key))
				{
					return keyValuePair.Value;
				}
			}
			return null;
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00035FB4 File Offset: 0x000341B4
		private bool ScanElseDirective()
		{
			this.m_ifDirectiveLevel--;
			this.PPSkipToDirective(new string[]
			{
				"#ENDIF"
			});
			return true;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x00035FE7 File Offset: 0x000341E7
		private bool ScanEndIfDirective()
		{
			this.m_ifDirectiveLevel--;
			return true;
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x00035FF8 File Offset: 0x000341F8
		private bool ScanDefineDirective()
		{
			this.SkipBlanks();
			if (!this.IsAtEndOfLine)
			{
				string text = this.PPScanIdentifier(true);
				if (!string.IsNullOrEmpty(text))
				{
					string value = string.Empty;
					this.SkipBlanks();
					if (this.GetChar(this.m_currentPosition) == '=')
					{
						int num = ++this.m_currentPosition;
						this.SkipToEndOfLine();
						value = this.m_strSourceCode.Substring(num, this.m_currentPosition - num).Trim();
					}
					if (this.m_defines == null)
					{
						this.m_defines = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
					}
					if (!this.m_defines.ContainsKey(text))
					{
						this.m_defines.Add(text, value);
					}
					else
					{
						this.m_defines[text] = value;
					}
				}
			}
			return true;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000360BC File Offset: 0x000342BC
		private bool ScanUndefineDirective()
		{
			this.SkipBlanks();
			if (!this.IsAtEndOfLine)
			{
				string text = this.PPScanIdentifier(true);
				if (!string.IsNullOrEmpty(text) && this.m_defines != null && this.m_defines.ContainsKey(text))
				{
					this.m_defines.Remove(text);
				}
			}
			return true;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0003610C File Offset: 0x0003430C
		private bool ScanDebugDirective()
		{
			if (this.GetChar(this.m_currentPosition) == '=')
			{
				this.m_currentPosition++;
				string text = this.PPScanIdentifier(false);
				if (text == null)
				{
					this.DebugLookupCollection.Clear();
				}
				else
				{
					this.OnGlobalDefine(text);
					while (this.GetChar(this.m_currentPosition) == '.')
					{
						this.m_currentPosition++;
						string text2 = this.PPScanIdentifier(false);
						if (text2 == null)
						{
							text = null;
							break;
						}
						text = text + '.' + text2;
					}
					if (text != null)
					{
						this.DebugLookupCollection.Add(text);
					}
				}
			}
			else if (this.StripDebugCommentBlocks && (this.m_defines == null || !this.m_defines.ContainsKey("DEBUG")))
			{
				this.PPSkipToDirective(new string[]
				{
					"#ENDDEBUG"
				});
			}
			return true;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000361E8 File Offset: 0x000343E8
		private void HandleError(JSError error)
		{
			this.m_currentToken.EndPosition = this.m_currentPosition;
			this.m_currentToken.EndLinePosition = this.m_startLinePosition;
			this.m_currentToken.EndLineNumber = this.m_currentLine;
			if (!this.SuppressErrors)
			{
				this.m_currentToken.HandleError(error, false);
			}
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0003623D File Offset: 0x0003443D
		private JSToken IllegalCharacter()
		{
			this.HandleError(JSError.IllegalChar);
			return JSToken.Error;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0003624C File Offset: 0x0003444C
		public static JSToken StripAssignment(JSToken assignOp)
		{
			if (JSScanner.IsAssignmentOperator(assignOp))
			{
				int num = assignOp - JSToken.Assign;
				if (num > 0)
				{
					assignOp = JSToken.FirstBinaryOperator + num - 1;
				}
			}
			return assignOp;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00036273 File Offset: 0x00034473
		public static OperatorPrecedence GetOperatorPrecedence(Context op)
		{
			if (op != null && op.Token != JSToken.None)
			{
				return JSScanner.s_OperatorsPrec[op.Token - JSToken.FirstBinaryOperator];
			}
			return OperatorPrecedence.None;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00036294 File Offset: 0x00034494
		private static OperatorPrecedence[] InitOperatorsPrec()
		{
			OperatorPrecedence[] array = new OperatorPrecedence[38];
			array[0] = OperatorPrecedence.Additive;
			array[1] = OperatorPrecedence.Additive;
			array[20] = OperatorPrecedence.LogicalOr;
			array[19] = OperatorPrecedence.LogicalAnd;
			array[6] = OperatorPrecedence.BitwiseOr;
			array[7] = OperatorPrecedence.BitwiseXor;
			array[5] = OperatorPrecedence.BitwiseAnd;
			array[11] = OperatorPrecedence.Equality;
			array[12] = OperatorPrecedence.Equality;
			array[13] = OperatorPrecedence.Equality;
			array[14] = OperatorPrecedence.Equality;
			array[21] = OperatorPrecedence.Relational;
			array[22] = OperatorPrecedence.Relational;
			array[17] = OperatorPrecedence.Relational;
			array[15] = OperatorPrecedence.Relational;
			array[16] = OperatorPrecedence.Relational;
			array[18] = OperatorPrecedence.Relational;
			array[8] = OperatorPrecedence.Shift;
			array[9] = OperatorPrecedence.Shift;
			array[10] = OperatorPrecedence.Shift;
			array[2] = OperatorPrecedence.Multiplicative;
			array[3] = OperatorPrecedence.Multiplicative;
			array[4] = OperatorPrecedence.Multiplicative;
			array[24] = OperatorPrecedence.Assignment;
			array[25] = OperatorPrecedence.Assignment;
			array[26] = OperatorPrecedence.Assignment;
			array[27] = OperatorPrecedence.Assignment;
			array[28] = OperatorPrecedence.Assignment;
			array[30] = OperatorPrecedence.Assignment;
			array[31] = OperatorPrecedence.Assignment;
			array[32] = OperatorPrecedence.Assignment;
			array[29] = OperatorPrecedence.Assignment;
			array[33] = OperatorPrecedence.Assignment;
			array[34] = OperatorPrecedence.Assignment;
			array[35] = OperatorPrecedence.Assignment;
			array[36] = OperatorPrecedence.Conditional;
			array[37] = OperatorPrecedence.Conditional;
			array[23] = OperatorPrecedence.Comma;
			return array;
		}

		// Token: 0x040003F5 RID: 1013
		private static readonly JSKeyword[] s_Keywords = JSKeyword.InitKeywords();

		// Token: 0x040003F6 RID: 1014
		private static readonly OperatorPrecedence[] s_OperatorsPrec = JSScanner.InitOperatorsPrec();

		// Token: 0x040003F7 RID: 1015
		private string m_strSourceCode;

		// Token: 0x040003F8 RID: 1016
		private int m_endPos;

		// Token: 0x040003F9 RID: 1017
		private StringBuilder m_identifier;

		// Token: 0x040003FA RID: 1018
		private bool m_literalIssues;

		// Token: 0x040003FB RID: 1019
		private Dictionary<string, string> m_defines;

		// Token: 0x040003FC RID: 1020
		private int m_startLinePosition;

		// Token: 0x040003FD RID: 1021
		private int m_currentPosition;

		// Token: 0x040003FE RID: 1022
		private int m_currentLine;

		// Token: 0x040003FF RID: 1023
		private int m_lastPosOnBuilder;

		// Token: 0x04000400 RID: 1024
		private int m_ifDirectiveLevel;

		// Token: 0x04000401 RID: 1025
		private int m_conditionalCompilationIfLevel;

		// Token: 0x04000402 RID: 1026
		private bool m_conditionalCompilationOn;

		// Token: 0x04000403 RID: 1027
		private bool m_inConditionalComment;

		// Token: 0x04000404 RID: 1028
		private bool m_inSingleLineComment;

		// Token: 0x04000405 RID: 1029
		private bool m_inMultipleLineComment;

		// Token: 0x04000406 RID: 1030
		private bool m_mightBeKeyword;

		// Token: 0x04000407 RID: 1031
		private string m_decodedString;

		// Token: 0x04000408 RID: 1032
		private Context m_currentToken;

		// Token: 0x020000A7 RID: 167
		private sealed class PPOperators : SortedDictionary<string, Func<string, string, bool>>
		{
			// Token: 0x06000AFF RID: 2815 RVA: 0x00036388 File Offset: 0x00034588
			private PPOperators() : base(new JSScanner.PPOperators.LengthComparer())
			{
				base.Add("==", new Func<string, string, bool>(JSScanner.PPOperators.PPIsEqual));
				base.Add("!=", new Func<string, string, bool>(JSScanner.PPOperators.PPIsNotEqual));
				base.Add("===", new Func<string, string, bool>(JSScanner.PPOperators.PPIsStrictEqual));
				base.Add("!==", new Func<string, string, bool>(JSScanner.PPOperators.PPIsNotStrictEqual));
				base.Add("<", new Func<string, string, bool>(JSScanner.PPOperators.PPIsLessThan));
				base.Add(">", new Func<string, string, bool>(JSScanner.PPOperators.PPIsGreaterThan));
				base.Add("<=", new Func<string, string, bool>(JSScanner.PPOperators.PPIsLessThanOrEqual));
				base.Add(">=", new Func<string, string, bool>(JSScanner.PPOperators.PPIsGreaterThanOrEqual));
			}

			// Token: 0x170002E4 RID: 740
			// (get) Token: 0x06000B00 RID: 2816 RVA: 0x00036458 File Offset: 0x00034658
			public static JSScanner.PPOperators Instance
			{
				get
				{
					return JSScanner.PPOperators.Nested.Instance;
				}
			}

			// Token: 0x06000B01 RID: 2817 RVA: 0x0003645F File Offset: 0x0003465F
			private static bool PPIsStrictEqual(string left, string right)
			{
				return string.Compare(left, right, StringComparison.OrdinalIgnoreCase) == 0;
			}

			// Token: 0x06000B02 RID: 2818 RVA: 0x0003646C File Offset: 0x0003466C
			private static bool PPIsNotStrictEqual(string left, string right)
			{
				return string.Compare(left, right, StringComparison.OrdinalIgnoreCase) != 0;
			}

			// Token: 0x06000B03 RID: 2819 RVA: 0x0003647C File Offset: 0x0003467C
			private static bool PPIsEqual(string left, string right)
			{
				bool flag = string.Compare(left, right, StringComparison.OrdinalIgnoreCase) == 0;
				double num;
				double num2;
				if (!flag && JSScanner.PPOperators.ConvertToNumeric(left, right, out num, out num2))
				{
					flag = (num == num2);
				}
				return flag;
			}

			// Token: 0x06000B04 RID: 2820 RVA: 0x000364AC File Offset: 0x000346AC
			private static bool PPIsNotEqual(string left, string right)
			{
				bool flag = string.Compare(left, right, StringComparison.OrdinalIgnoreCase) != 0;
				double num;
				double num2;
				if (flag && JSScanner.PPOperators.ConvertToNumeric(left, right, out num, out num2))
				{
					flag = (num != num2);
				}
				return flag;
			}

			// Token: 0x06000B05 RID: 2821 RVA: 0x000364E4 File Offset: 0x000346E4
			private static bool PPIsLessThan(string left, string right)
			{
				bool result = false;
				double num;
				double num2;
				if (JSScanner.PPOperators.ConvertToNumeric(left, right, out num, out num2))
				{
					result = (num < num2);
				}
				return result;
			}

			// Token: 0x06000B06 RID: 2822 RVA: 0x00036508 File Offset: 0x00034708
			private static bool PPIsGreaterThan(string left, string right)
			{
				bool result = false;
				double num;
				double num2;
				if (JSScanner.PPOperators.ConvertToNumeric(left, right, out num, out num2))
				{
					result = (num > num2);
				}
				return result;
			}

			// Token: 0x06000B07 RID: 2823 RVA: 0x0003652C File Offset: 0x0003472C
			private static bool PPIsLessThanOrEqual(string left, string right)
			{
				bool result = false;
				double num;
				double num2;
				if (JSScanner.PPOperators.ConvertToNumeric(left, right, out num, out num2))
				{
					result = (num <= num2);
				}
				return result;
			}

			// Token: 0x06000B08 RID: 2824 RVA: 0x00036554 File Offset: 0x00034754
			private static bool PPIsGreaterThanOrEqual(string left, string right)
			{
				bool result = false;
				double num;
				double num2;
				if (JSScanner.PPOperators.ConvertToNumeric(left, right, out num, out num2))
				{
					result = (num >= num2);
				}
				return result;
			}

			// Token: 0x06000B09 RID: 2825 RVA: 0x00036579 File Offset: 0x00034779
			private static bool ConvertToNumeric(string left, string right, out double leftNumeric, out double rightNumeric)
			{
				rightNumeric = 0.0;
				return double.TryParse(left, NumberStyles.Any, CultureInfo.InvariantCulture, out leftNumeric) && double.TryParse(right, NumberStyles.Any, CultureInfo.InvariantCulture, out rightNumeric);
			}

			// Token: 0x020000A8 RID: 168
			private static class Nested
			{
				// Token: 0x04000411 RID: 1041
				internal static readonly JSScanner.PPOperators Instance = new JSScanner.PPOperators();
			}

			// Token: 0x020000A9 RID: 169
			private class LengthComparer : IComparer<string>
			{
				// Token: 0x06000B0B RID: 2827 RVA: 0x000365B8 File Offset: 0x000347B8
				public int Compare(string x, string y)
				{
					int num = (x != null && y != null) ? (y.Length - x.Length) : 0;
					if (num == 0)
					{
						return string.CompareOrdinal(x, y);
					}
					return num;
				}
			}
		}
	}
}

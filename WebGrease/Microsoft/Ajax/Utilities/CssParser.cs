using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000052 RID: 82
	public class CssParser
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x000121FF File Offset: 0x000103FF
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x00012207 File Offset: 0x00010407
		public CssSettings Settings { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00012210 File Offset: 0x00010410
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x00012218 File Offset: 0x00010418
		public string FileContext { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00012221 File Offset: 0x00010421
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x0001222C File Offset: 0x0001042C
		public CodeSettings JSSettings
		{
			get
			{
				return this.m_jsSettings;
			}
			set
			{
				if (value != null)
				{
					this.m_jsSettings = value.Clone();
					this.m_jsSettings.SourceMode = JavaScriptSourceMode.Expression;
					return;
				}
				this.m_jsSettings = new CodeSettings
				{
					KillSwitch = 1048576L,
					SourceMode = JavaScriptSourceMode.Expression
				};
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00012275 File Offset: 0x00010475
		private TokenType CurrentTokenType
		{
			get
			{
				if (this.m_currentToken == null)
				{
					return TokenType.None;
				}
				return this.m_currentToken.TokenType;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0001228C File Offset: 0x0001048C
		private string CurrentTokenText
		{
			get
			{
				if (this.m_currentToken == null)
				{
					return string.Empty;
				}
				return this.m_currentToken.Text;
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000122A7 File Offset: 0x000104A7
		public CssParser()
		{
			this.Settings = new CssSettings();
			this.JSSettings = null;
			this.m_namespaces = new HashSet<string>();
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00012300 File Offset: 0x00010500
		public string Parse(string source)
		{
			this.m_namespaces.Clear();
			if (source.IsNullOrWhiteSpace())
			{
				source = string.Empty;
			}
			else
			{
				bool flag = false;
				try
				{
					source = this.HandleCharset(source);
					if (this.Settings.CommentMode == CssComment.Hacks)
					{
						source = CssParser.s_regexHack1.Replace(source, "/*! \\*/${inner}/*!*/");
						source = CssParser.s_regexHack2.Replace(source, "/*!/*//*/${inner}/**/");
						source = CssParser.s_regexHack3.Replace(source, "/*!/*/${inner}/*!*/");
						source = CssParser.s_regexHack4.Replace(source, "/*!*/");
						source = CssParser.s_regexHack5.Replace(source, "/*!*/");
						source = CssParser.s_regexHack6.Replace(source, "/*!*/");
						source = CssParser.s_regexHack7.Replace(source, "/*!*/");
						this.Settings.CommentMode = CssComment.Important;
						flag = true;
					}
					using (StringReader stringReader = new StringReader(source))
					{
						this.m_scanner = new CssScanner(stringReader);
						this.m_scanner.AllowEmbeddedAspNetBlocks = this.Settings.AllowEmbeddedAspNetBlocks;
						this.m_scanner.ScannerError += delegate(object sender, ContextErrorEventArgs ea)
						{
							ea.Error.File = this.FileContext;
							this.OnCssError(ea.Error);
						};
						this.m_scanner.ContextChange += delegate(object sender, CssScannerContextChangeEventArgs ea)
						{
							this.FileContext = ea.FileContext;
						};
						this.m_parsed = new StringBuilder();
						this.NextToken();
						switch (this.Settings.CssType)
						{
						default:
							this.ParseStylesheet();
							break;
						case CssType.DeclarationList:
							this.SkipIfSpace();
							this.ParseDeclarationList(false);
							break;
						}
						if (!this.m_scanner.EndOfFile)
						{
							int num = 1050;
							this.OnCssError(new ContextError
							{
								IsError = true,
								Severity = 0,
								Subcategory = ContextError.GetSubcategory(0),
								File = this.FileContext,
								ErrorNumber = num,
								ErrorCode = "CSS{0}".FormatInvariant(new object[]
								{
									num & 65535
								}),
								StartLine = this.m_currentToken.Context.Start.Line,
								StartColumn = this.m_currentToken.Context.Start.Char,
								Message = CssStrings.ExpectedEndOfFile
							});
						}
						source = this.m_parsed.ToString();
						this.m_parsed = null;
					}
				}
				finally
				{
					if (flag)
					{
						this.Settings.CommentMode = CssComment.Hacks;
					}
				}
			}
			return source;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000125AC File Offset: 0x000107AC
		private string HandleCharset(string source)
		{
			if (source.StartsWith("/*/#SOURCE", StringComparison.OrdinalIgnoreCase))
			{
				int num = source.IndexOfAny(new char[]
				{
					'\n',
					'\r'
				});
				if (num >= 0)
				{
					if (source[num] == '\r' && source[num + 1] == '\n')
					{
						num++;
					}
					source = source.Substring(num + 1);
				}
			}
			if (source.StartsWith("ï»¿", StringComparison.Ordinal))
			{
				string text = "@charset ";
				if (string.CompareOrdinal(source, 3, text, 0, text.Length) != 0 || (source[3 + text.Length] != '"' && source[3 + text.Length] != '\'') || string.Compare(source, 4 + text.Length, "ascii", 0, 5, StringComparison.OrdinalIgnoreCase) != 0)
				{
					this.ReportError(1, CssErrorCode.PossibleCharsetError, new object[0]);
				}
				source = source.Substring(3);
			}
			else if (source.StartsWith("þÿ\0\0", StringComparison.Ordinal) || source.StartsWith("\0\0ÿþ", StringComparison.Ordinal))
			{
				this.ReportError(0, CssErrorCode.PossibleCharsetError, new object[0]);
				source = source.Substring(4);
			}
			else if (source.StartsWith("þÿ", StringComparison.Ordinal) || source.StartsWith("ÿþ", StringComparison.Ordinal))
			{
				this.ReportError(0, CssErrorCode.PossibleCharsetError, new object[0]);
				source = source.Substring(2);
			}
			else if (source.Length > 0 && source[0] == '﻿')
			{
				source = source.Substring(1);
			}
			return source;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00012720 File Offset: 0x00010920
		private CssParser.Parsed ParseStylesheet()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			this.SkipSemicolons();
			if (this.CurrentTokenType == TokenType.CharacterSetSymbol)
			{
				this.ParseCharset();
			}
			this.ParseSCDOCDCComments();
			while (this.ParseImport() == CssParser.Parsed.True)
			{
				this.ParseSCDOCDCComments();
			}
			while (this.ParseNamespace() == CssParser.Parsed.True)
			{
				this.ParseSCDOCDCComments();
			}
			while (this.ParseRule() == CssParser.Parsed.True || this.ParseMedia() == CssParser.Parsed.True || this.ParsePage() == CssParser.Parsed.True || this.ParseFontFace() == CssParser.Parsed.True || this.ParseKeyFrames() == CssParser.Parsed.True || this.ParseAtKeyword() == CssParser.Parsed.True || this.ParseAspNetBlock() == CssParser.Parsed.True)
			{
				this.ParseSCDOCDCComments();
			}
			while (!this.m_scanner.EndOfFile)
			{
				this.ReportError(0, CssErrorCode.UnexpectedToken, new object[]
				{
					this.CurrentTokenText
				});
				this.NextToken();
				this.ParseSCDOCDCComments();
				while (this.ParseRule() == CssParser.Parsed.True || this.ParseMedia() == CssParser.Parsed.True || this.ParsePage() == CssParser.Parsed.True || this.ParseFontFace() == CssParser.Parsed.True || this.ParseAtKeyword() == CssParser.Parsed.True || this.ParseAspNetBlock() == CssParser.Parsed.True)
				{
					this.ParseSCDOCDCComments();
				}
			}
			return result;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00012820 File Offset: 0x00010A20
		private CssParser.Parsed ParseCharset()
		{
			this.AppendCurrent();
			this.SkipSpace();
			if (this.CurrentTokenType != TokenType.String)
			{
				this.ReportError(0, CssErrorCode.ExpectedCharset, new object[]
				{
					this.CurrentTokenText
				});
				this.SkipToEndOfStatement();
				this.AppendCurrent();
			}
			else
			{
				this.Append(' ');
				this.AppendCurrent();
				this.SkipSpace();
				if (this.CurrentTokenType != TokenType.Character || this.CurrentTokenText != ";")
				{
					this.ReportError(0, CssErrorCode.ExpectedSemicolon, new object[]
					{
						this.CurrentTokenText
					});
					this.SkipToEndOfStatement();
					this.AppendCurrent();
				}
				else
				{
					this.Append(';');
					this.NextToken();
				}
			}
			return CssParser.Parsed.True;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000128EC File Offset: 0x00010AEC
		private void ParseSCDOCDCComments()
		{
			while (this.CurrentTokenType == TokenType.Space || this.CurrentTokenType == TokenType.Comment || this.CurrentTokenType == TokenType.CommentOpen || this.CurrentTokenType == TokenType.CommentClose || (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ";"))
			{
				if (this.CurrentTokenType != TokenType.Space && this.CurrentTokenType != TokenType.Character)
				{
					this.AppendCurrent();
				}
				this.NextToken();
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00012960 File Offset: 0x00010B60
		private CssParser.Parsed ParseAtKeyword()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.AtKeyword)
			{
				if (!this.CurrentTokenText.StartsWith("@-", StringComparison.OrdinalIgnoreCase))
				{
					this.ReportError(2, CssErrorCode.UnexpectedAtKeyword, new object[]
					{
						this.CurrentTokenText
					});
				}
				this.SkipToEndOfStatement();
				this.AppendCurrent();
				this.SkipSpace();
				this.NewLine();
				result = CssParser.Parsed.True;
			}
			else if (this.CurrentTokenType == TokenType.CharacterSetSymbol)
			{
				this.ReportError(2, CssErrorCode.UnexpectedCharset, new object[]
				{
					this.CurrentTokenText
				});
				result = this.ParseCharset();
			}
			return result;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000129F8 File Offset: 0x00010BF8
		private CssParser.Parsed ParseAspNetBlock()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.Settings.AllowEmbeddedAspNetBlocks && this.CurrentTokenType == TokenType.AspNetBlock)
			{
				this.AppendCurrent();
				this.SkipSpace();
				result = CssParser.Parsed.True;
			}
			return result;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00012A30 File Offset: 0x00010C30
		private CssParser.Parsed ParseNamespace()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.NamespaceSymbol)
			{
				this.NewLine();
				this.AppendCurrent();
				this.SkipSpace();
				if (this.CurrentTokenType == TokenType.Identifier)
				{
					this.Append(' ');
					this.AppendCurrent();
					if (!this.m_namespaces.Add(this.CurrentTokenText))
					{
						this.ReportError(1, CssErrorCode.DuplicateNamespaceDeclaration, new object[]
						{
							this.CurrentTokenText
						});
					}
					this.SkipSpace();
				}
				if (this.CurrentTokenType != TokenType.String && this.CurrentTokenType != TokenType.Uri)
				{
					this.ReportError(0, CssErrorCode.ExpectedNamespace, new object[]
					{
						this.CurrentTokenText
					});
					this.SkipToEndOfStatement();
					this.AppendCurrent();
				}
				else
				{
					this.Append(' ');
					this.AppendCurrent();
					this.SkipSpace();
					if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ";")
					{
						this.Append(';');
						this.SkipSpace();
						this.NewLine();
					}
					else
					{
						this.ReportError(0, CssErrorCode.ExpectedSemicolon, new object[]
						{
							this.CurrentTokenText
						});
						this.SkipToEndOfStatement();
						this.AppendCurrent();
					}
				}
				result = CssParser.Parsed.True;
			}
			return result;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00012B74 File Offset: 0x00010D74
		private void ValidateNamespace(string namespaceIdent)
		{
			if (!string.IsNullOrEmpty(namespaceIdent) && namespaceIdent != "*" && !this.m_namespaces.Contains(namespaceIdent))
			{
				this.ReportError(0, CssErrorCode.UndeclaredNamespace, new object[]
				{
					namespaceIdent
				});
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00012BBC File Offset: 0x00010DBC
		private CssParser.Parsed ParseKeyFrames()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.KeyFramesSymbol)
			{
				result = CssParser.Parsed.True;
				this.NewLine();
				this.AppendCurrent();
				this.SkipSpace();
				if (this.CurrentTokenType == TokenType.Identifier || this.CurrentTokenType == TokenType.String)
				{
					if (this.CurrentTokenType == TokenType.Identifier || this.Settings.OutputMode == OutputMode.MultipleLines)
					{
						this.Append(' ');
					}
					this.AppendCurrent();
					this.SkipSpace();
				}
				else
				{
					this.ReportError(0, CssErrorCode.ExpectedIdentifier, new object[]
					{
						this.CurrentTokenText
					});
				}
				if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "{")
				{
					if (this.Settings.BlocksStartOnSameLine == BlockStart.NewLine || (this.Settings.BlocksStartOnSameLine == BlockStart.UseSource && this.m_encounteredNewLine))
					{
						this.NewLine();
					}
					else if (this.Settings.OutputMode == OutputMode.MultipleLines)
					{
						this.Append(' ');
					}
					this.AppendCurrent();
					this.Indent();
					this.NewLine();
					this.SkipSpace();
					this.ParseKeyFrameBlocks();
					this.Unindent();
					this.NewLine();
					if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "}")
					{
						this.NewLine();
						this.AppendCurrent();
						this.SkipSpace();
					}
					else
					{
						this.ReportError(0, CssErrorCode.ExpectedClosingBrace, new object[]
						{
							this.CurrentTokenText
						});
						this.SkipToEndOfDeclaration();
					}
				}
				else
				{
					this.ReportError(0, CssErrorCode.ExpectedOpenBrace, new object[]
					{
						this.CurrentTokenText
					});
					this.SkipToEndOfStatement();
				}
			}
			return result;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00012D61 File Offset: 0x00010F61
		private void ParseKeyFrameBlocks()
		{
			while (this.ParseKeyFrameSelectors() == CssParser.Parsed.True)
			{
				this.ParseDeclarationBlock(false);
				this.m_forceNewLine = true;
			}
			this.m_forceNewLine = false;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00012D84 File Offset: 0x00010F84
		private CssParser.Parsed ParseKeyFrameSelectors()
		{
			CssParser.Parsed parsed = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.Percentage)
			{
				this.AppendCurrent();
				this.SkipSpace();
				parsed = CssParser.Parsed.True;
			}
			else if (this.CurrentTokenType == TokenType.Identifier)
			{
				string strA = this.CurrentTokenText.ToUpperInvariant();
				if (string.CompareOrdinal(strA, "FROM") == 0 || string.CompareOrdinal(strA, "TO") == 0)
				{
					this.AppendCurrent();
					this.SkipSpace();
					parsed = CssParser.Parsed.True;
				}
			}
			while (parsed == CssParser.Parsed.True && this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ",")
			{
				this.AppendCurrent();
				if (this.Settings.OutputMode == OutputMode.MultipleLines)
				{
					this.Append(' ');
				}
				this.SkipSpace();
				if (this.CurrentTokenType == TokenType.Percentage)
				{
					this.AppendCurrent();
					this.SkipSpace();
				}
				else if (this.CurrentTokenType == TokenType.Identifier)
				{
					string strA2 = this.CurrentTokenText.ToUpperInvariant();
					if (string.CompareOrdinal(strA2, "FROM") == 0 || string.CompareOrdinal(strA2, "TO") == 0)
					{
						this.AppendCurrent();
						this.SkipSpace();
					}
				}
				else
				{
					this.ReportError(0, CssErrorCode.ExpectedPercentageFromOrTo, new object[]
					{
						this.CurrentTokenText
					});
				}
			}
			return parsed;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00012EBC File Offset: 0x000110BC
		private CssParser.Parsed ParseImport()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.ImportSymbol)
			{
				this.NewLine();
				this.AppendCurrent();
				this.SkipSpace();
				if (this.CurrentTokenType != TokenType.String && this.CurrentTokenType != TokenType.Uri)
				{
					this.ReportError(0, CssErrorCode.ExpectedImport, new object[]
					{
						this.CurrentTokenText
					});
					this.SkipToEndOfStatement();
					this.AppendCurrent();
				}
				else
				{
					if (this.CurrentTokenType == TokenType.Uri || this.Settings.OutputMode == OutputMode.MultipleLines)
					{
						this.Append(' ');
					}
					this.AppendCurrent();
					this.SkipSpace();
					this.ParseMediaQueryList(false);
					if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ";")
					{
						this.Append(';');
						this.NewLine();
					}
					else
					{
						this.ReportError(0, CssErrorCode.ExpectedSemicolon, new object[]
						{
							this.CurrentTokenText
						});
						this.SkipToEndOfStatement();
						this.AppendCurrent();
					}
				}
				this.SkipSpace();
				result = CssParser.Parsed.True;
			}
			return result;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00012FD0 File Offset: 0x000111D0
		private CssParser.Parsed ParseMedia()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.MediaSymbol)
			{
				this.NewLine();
				this.AppendCurrent();
				this.SkipSpace();
				bool flag = false;
				if (this.ParseMediaQueryList(true) == CssParser.Parsed.True)
				{
					if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "{")
					{
						if (this.Settings.BlocksStartOnSameLine == BlockStart.NewLine || (this.Settings.BlocksStartOnSameLine == BlockStart.UseSource && this.m_encounteredNewLine))
						{
							this.NewLine();
						}
						else if (this.Settings.OutputMode == OutputMode.MultipleLines)
						{
							this.Append(' ');
						}
						this.AppendCurrent();
						this.Indent();
						flag = true;
						this.SkipSpace();
						while (this.ParseRule() == CssParser.Parsed.True || this.ParseMedia() == CssParser.Parsed.True || this.ParsePage() == CssParser.Parsed.True || this.ParseFontFace() == CssParser.Parsed.True || this.ParseAtKeyword() == CssParser.Parsed.True || this.ParseAspNetBlock() == CssParser.Parsed.True)
						{
							this.ParseSCDOCDCComments();
						}
					}
					else
					{
						this.SkipToEndOfStatement();
					}
					if (this.CurrentTokenType == TokenType.Character)
					{
						if (this.CurrentTokenText == ";")
						{
							this.AppendCurrent();
							if (flag)
							{
								this.Unindent();
							}
							this.NewLine();
						}
						else if (this.CurrentTokenText == "}")
						{
							if (flag)
							{
								this.Unindent();
							}
							this.NewLine();
							this.AppendCurrent();
						}
						else
						{
							this.SkipToEndOfStatement();
							this.AppendCurrent();
						}
					}
					else
					{
						this.SkipToEndOfStatement();
						this.AppendCurrent();
					}
					this.SkipSpace();
					result = CssParser.Parsed.True;
				}
				else
				{
					this.SkipToEndOfStatement();
				}
			}
			return result;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00013158 File Offset: 0x00011358
		private CssParser.Parsed ParseMediaQueryList(bool mightNeedSpace)
		{
			CssParser.Parsed result = this.ParseMediaQuery(mightNeedSpace);
			while (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ",")
			{
				this.AppendCurrent();
				this.SkipSpace();
				if (this.ParseMediaQuery(false) != CssParser.Parsed.True)
				{
					this.ReportError(0, CssErrorCode.ExpectedMediaQuery, new object[]
					{
						this.CurrentTokenText
					});
				}
			}
			return result;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000131C0 File Offset: 0x000113C0
		private CssParser.Parsed ParseMediaQuery(bool firstQuery)
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			bool flag = firstQuery;
			if (this.CurrentTokenType == TokenType.Identifier && (string.Compare(this.CurrentTokenText, "ONLY", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.CurrentTokenText, "NOT", StringComparison.OrdinalIgnoreCase) == 0))
			{
				if (firstQuery || this.Settings.OutputMode == OutputMode.MultipleLines)
				{
					this.Append(' ');
				}
				this.AppendCurrent();
				this.SkipSpace();
				flag = true;
			}
			if (this.CurrentTokenType == TokenType.Identifier)
			{
				if (flag || this.Settings.OutputMode == OutputMode.MultipleLines)
				{
					this.Append(' ');
				}
				this.AppendCurrent();
				this.SkipSpace();
				flag = true;
				result = CssParser.Parsed.True;
			}
			else if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "(")
			{
				this.ParseMediaQueryExpression();
				flag = true;
				result = CssParser.Parsed.True;
			}
			else if (this.CurrentTokenType != TokenType.Character || this.CurrentTokenText != ";")
			{
				this.ReportError(0, CssErrorCode.ExpectedMediaIdentifier, new object[]
				{
					this.CurrentTokenText
				});
			}
			while ((this.CurrentTokenType == TokenType.Identifier && string.Compare(this.CurrentTokenText, "AND", StringComparison.OrdinalIgnoreCase) == 0) || (this.CurrentTokenType == TokenType.Function && string.Compare(this.CurrentTokenText, "AND(", StringComparison.OrdinalIgnoreCase) == 0))
			{
				if (flag || this.Settings.OutputMode == OutputMode.MultipleLines)
				{
					this.Append(' ');
				}
				if (this.CurrentTokenType == TokenType.Function)
				{
					this.ReportError(1, CssErrorCode.MediaQueryRequiresSpace, new object[]
					{
						this.CurrentTokenText
					});
					this.Append("and (");
					this.SkipSpace();
					this.ParseMediaQueryExpression();
				}
				else
				{
					this.AppendCurrent();
					this.SkipSpace();
					if (this.CurrentTokenType != TokenType.Character || !(this.CurrentTokenText == "("))
					{
						this.ReportError(0, CssErrorCode.ExpectedMediaQueryExpression, new object[]
						{
							this.CurrentTokenText
						});
						break;
					}
					this.Append(' ');
					this.ParseMediaQueryExpression();
				}
			}
			return result;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000133DC File Offset: 0x000115DC
		private void ParseMediaQueryExpression()
		{
			if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "(")
			{
				this.AppendCurrent();
				this.SkipSpace();
			}
			if (this.CurrentTokenType != TokenType.Identifier)
			{
				this.ReportError(0, CssErrorCode.ExpectedMediaFeature, new object[]
				{
					this.CurrentTokenText
				});
				return;
			}
			this.AppendCurrent();
			this.SkipSpace();
			if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ":")
			{
				this.AppendCurrent();
				this.SkipSpace();
				if (this.Settings.OutputMode == OutputMode.MultipleLines)
				{
					this.Append(' ');
				}
				if (this.ParseExpr() != CssParser.Parsed.True)
				{
					this.ReportError(0, CssErrorCode.ExpectedExpression, new object[]
					{
						this.CurrentTokenText
					});
				}
				if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ")")
				{
					this.AppendCurrent();
					this.SkipSpace();
					return;
				}
				this.ReportError(0, CssErrorCode.ExpectedClosingParenthesis, new object[]
				{
					this.CurrentTokenText
				});
				return;
			}
			else
			{
				if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ")")
				{
					this.AppendCurrent();
					this.SkipSpace();
					return;
				}
				this.ReportError(0, CssErrorCode.ExpectedClosingParenthesis, new object[]
				{
					this.CurrentTokenText
				});
				return;
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001354C File Offset: 0x0001174C
		private CssParser.Parsed ParseDeclarationBlock(bool allowMargins)
		{
			if (this.CurrentTokenType != TokenType.Character || this.CurrentTokenText != "{")
			{
				this.ReportError(0, CssErrorCode.ExpectedOpenBrace, new object[]
				{
					this.CurrentTokenText
				});
				this.SkipToEndOfStatement();
				this.AppendCurrent();
				this.SkipSpace();
			}
			else
			{
				if (this.Settings.BlocksStartOnSameLine == BlockStart.NewLine || (this.Settings.BlocksStartOnSameLine == BlockStart.UseSource && this.m_encounteredNewLine))
				{
					this.NewLine();
				}
				else if (this.Settings.OutputMode == OutputMode.MultipleLines)
				{
					this.Append(' ');
				}
				this.Append('{');
				this.Indent();
				this.SkipSpace();
				if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "}")
				{
					this.Unindent();
					this.AppendCurrent();
					this.SkipSpace();
				}
				else
				{
					this.ParseDeclarationList(allowMargins);
					if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "}")
					{
						this.Unindent();
						this.NewLine();
						this.Append('}');
						this.SkipSpace();
					}
					else if (this.m_scanner.EndOfFile)
					{
						this.ReportError(0, CssErrorCode.UnexpectedEndOfFile, new object[0]);
					}
					else
					{
						this.ReportError(0, CssErrorCode.ExpectedClosingBrace, new object[]
						{
							this.CurrentTokenText
						});
					}
				}
			}
			return CssParser.Parsed.True;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000136C4 File Offset: 0x000118C4
		private CssParser.Parsed ParseDeclarationList(bool allowMargins)
		{
			CssParser.Parsed parsed = CssParser.Parsed.Empty;
			while (!this.m_scanner.EndOfFile)
			{
				if (this.m_lineLength >= this.Settings.LineBreakThreshold)
				{
					this.AddNewLine();
				}
				CssParser.Parsed parsed2 = this.ParseDeclaration();
				if (parsed == CssParser.Parsed.Empty && parsed2 != CssParser.Parsed.Empty)
				{
					parsed = parsed2;
				}
				bool flag = false;
				if (allowMargins && parsed2 == CssParser.Parsed.Empty)
				{
					flag = (this.ParseMargin() == CssParser.Parsed.True);
				}
				if (!flag && (this.CurrentTokenType != TokenType.Character || (this.CurrentTokenText != ";" && this.CurrentTokenText != "}")) && !this.m_scanner.EndOfFile)
				{
					this.ReportError(0, CssErrorCode.ExpectedSemicolonOrClosingBrace, new object[]
					{
						this.CurrentTokenText
					});
					this.SkipToEndOfDeclaration();
				}
				if (this.m_scanner.EndOfFile)
				{
					if (this.Settings.TermSemicolons)
					{
						this.Append(';');
					}
				}
				else if (this.CurrentTokenText == "}")
				{
					if (this.Settings.TermSemicolons && parsed2 == CssParser.Parsed.True)
					{
						this.Append(';');
						break;
					}
					break;
				}
				else if (this.CurrentTokenText == ";")
				{
					if (this.Settings.TermSemicolons)
					{
						this.Append(';');
						this.SkipSpace();
					}
					else
					{
						string text = this.NextSignificantToken();
						if (this.m_scanner.EndOfFile)
						{
							if (text.Length > 0)
							{
								if (text != "/* */" && text != "/**/")
								{
									this.Append(';');
								}
								this.Append(text);
								this.m_outputNewLine = true;
								this.m_lineLength = 0;
								break;
							}
							break;
						}
						else
						{
							if (this.CurrentTokenType != TokenType.Character || (this.CurrentTokenText != "}" && this.CurrentTokenText != ";") || (text.Length > 0 && text != "/* */" && text != "/**/"))
							{
								this.Append(';');
							}
							if (text.Length > 0)
							{
								this.Append(text);
								this.m_outputNewLine = true;
								this.m_lineLength = 0;
							}
						}
					}
				}
			}
			return parsed;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00013914 File Offset: 0x00011B14
		private CssParser.Parsed ParsePage()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.PageSymbol)
			{
				this.NewLine();
				this.AppendCurrent();
				this.SkipSpace();
				if (this.CurrentTokenType == TokenType.Identifier)
				{
					this.Append(' ');
					this.AppendCurrent();
					this.NextToken();
				}
				this.ParsePseudoPage();
				if (this.CurrentTokenType == TokenType.Space)
				{
					this.SkipSpace();
				}
				if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "{")
				{
					result = this.ParseDeclarationBlock(true);
					this.NewLine();
				}
				else
				{
					this.SkipToEndOfStatement();
					this.AppendCurrent();
					this.SkipSpace();
				}
			}
			return result;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000139C0 File Offset: 0x00011BC0
		private CssParser.Parsed ParsePseudoPage()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ":")
			{
				this.Append(':');
				this.NextToken();
				if (this.CurrentTokenType != TokenType.Identifier)
				{
					this.ReportError(0, CssErrorCode.ExpectedIdentifier, new object[]
					{
						this.CurrentTokenText
					});
				}
				this.AppendCurrent();
				this.NextToken();
				result = CssParser.Parsed.True;
			}
			return result;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00013A38 File Offset: 0x00011C38
		private CssParser.Parsed ParseMargin()
		{
			CssParser.Parsed result = CssParser.Parsed.Empty;
			switch (this.CurrentTokenType)
			{
			case TokenType.TopLeftCornerSymbol:
			case TokenType.TopLeftSymbol:
			case TokenType.TopCenterSymbol:
			case TokenType.TopRightSymbol:
			case TokenType.TopRightCornerSymbol:
			case TokenType.BottomLeftCornerSymbol:
			case TokenType.BottomLeftSymbol:
			case TokenType.BottomCenterSymbol:
			case TokenType.BottomRightSymbol:
			case TokenType.BottomRightCornerSymbol:
			case TokenType.LeftTopSymbol:
			case TokenType.LeftMiddleSymbol:
			case TokenType.LeftBottomSymbol:
			case TokenType.RightTopSymbol:
			case TokenType.RightMiddleSymbol:
			case TokenType.RightBottomSymbol:
				this.NewLine();
				this.AppendCurrent();
				this.SkipSpace();
				result = this.ParseDeclarationBlock(false);
				this.NewLine();
				break;
			}
			return result;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00013ABC File Offset: 0x00011CBC
		private CssParser.Parsed ParseFontFace()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.FontFaceSymbol)
			{
				this.NewLine();
				this.AppendCurrent();
				this.SkipSpace();
				result = this.ParseDeclarationBlock(false);
				this.NewLine();
			}
			return result;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00013AF8 File Offset: 0x00011CF8
		private CssParser.Parsed ParseOperator()
		{
			CssParser.Parsed result = CssParser.Parsed.Empty;
			if (this.CurrentTokenType == TokenType.Character && (this.CurrentTokenText == "/" || this.CurrentTokenText == ","))
			{
				this.AppendCurrent();
				this.SkipSpace();
				result = CssParser.Parsed.True;
			}
			return result;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00013B48 File Offset: 0x00011D48
		private CssParser.Parsed ParseCombinator()
		{
			CssParser.Parsed result = CssParser.Parsed.Empty;
			if (this.CurrentTokenType == TokenType.Character && (this.CurrentTokenText == "+" || this.CurrentTokenText == ">" || this.CurrentTokenText == "~"))
			{
				this.AppendCurrent();
				this.SkipSpace();
				result = CssParser.Parsed.True;
			}
			return result;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00013BA8 File Offset: 0x00011DA8
		private CssParser.Parsed ParseRule()
		{
			if (this.m_lineLength >= this.Settings.LineBreakThreshold)
			{
				this.AddNewLine();
			}
			this.m_forceNewLine = true;
			CssParser.Parsed parsed = this.ParseSelector();
			if (parsed == CssParser.Parsed.True)
			{
				if (this.m_scanner.EndOfFile)
				{
					this.ReportError(0, CssErrorCode.UnexpectedEndOfFile, new object[0]);
				}
				while (!this.m_scanner.EndOfFile)
				{
					if (this.CurrentTokenType != TokenType.Character || (this.CurrentTokenText != "," && this.CurrentTokenText != "{"))
					{
						this.ReportError(0, CssErrorCode.ExpectedCommaOrOpenBrace, new object[]
						{
							this.CurrentTokenText
						});
						this.SkipToEndOfStatement();
						this.AppendCurrent();
						this.SkipSpace();
						break;
					}
					if (this.CurrentTokenText == "{")
					{
						if (this.m_lastOutputString == "first-letter" || this.m_lastOutputString == "first-line")
						{
							this.Append(' ');
						}
						parsed = this.ParseDeclarationBlock(false);
						break;
					}
					this.Append(',');
					if (this.m_lineLength >= this.Settings.LineBreakThreshold)
					{
						this.AddNewLine();
					}
					else if (this.Settings.OutputMode == OutputMode.MultipleLines)
					{
						this.Append(' ');
					}
					this.SkipSpace();
					if (this.ParseSelector() != CssParser.Parsed.True)
					{
						if (this.CurrentTokenType != TokenType.Character || !(this.CurrentTokenText == "{"))
						{
							this.ReportError(0, CssErrorCode.ExpectedSelector, new object[]
							{
								this.CurrentTokenText
							});
							this.SkipToEndOfStatement();
							this.AppendCurrent();
							this.SkipSpace();
							break;
						}
						this.ReportError(4, CssErrorCode.ExpectedSelector, new object[]
						{
							this.CurrentTokenText
						});
					}
				}
			}
			return parsed;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00013D90 File Offset: 0x00011F90
		private CssParser.Parsed ParseSelector()
		{
			CssParser.Parsed parsed = this.ParseSimpleSelector();
			if (parsed == CssParser.Parsed.False && this.CurrentTokenType != TokenType.None)
			{
				CssContext context = this.m_currentToken.Context;
				string currentTokenText = this.CurrentTokenText;
				parsed = this.ParseCombinator();
				if (parsed == CssParser.Parsed.True)
				{
					this.ReportError(4, CssErrorCode.HackGeneratesInvalidCss, context, new object[]
					{
						currentTokenText
					});
				}
			}
			if (parsed == CssParser.Parsed.True)
			{
				bool flag = this.SkipIfSpace();
				while (!this.m_scanner.EndOfFile)
				{
					CssParser.Parsed parsed2 = this.ParseCombinator();
					if (parsed2 != CssParser.Parsed.True)
					{
						if (this.CurrentTokenType == TokenType.Character && (this.CurrentTokenText == "," || this.CurrentTokenText == "{"))
						{
							break;
						}
						if (flag)
						{
							this.Append(' ');
						}
					}
					if (this.ParseSimpleSelector() == CssParser.Parsed.False)
					{
						this.ReportError(0, CssErrorCode.ExpectedSelector, new object[]
						{
							this.CurrentTokenText
						});
						break;
					}
					flag = this.SkipIfSpace();
				}
			}
			return parsed;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00013E88 File Offset: 0x00012088
		private CssParser.Parsed ParseSimpleSelector()
		{
			CssParser.Parsed result = this.ParseElementName();
			while (!this.m_scanner.EndOfFile)
			{
				if (this.CurrentTokenType == TokenType.Hash)
				{
					this.AppendCurrent();
					this.NextToken();
					result = CssParser.Parsed.True;
				}
				else if (this.ParseClass() == CssParser.Parsed.True)
				{
					result = CssParser.Parsed.True;
				}
				else if (this.ParseAttrib() == CssParser.Parsed.True)
				{
					result = CssParser.Parsed.True;
				}
				else
				{
					if (this.ParsePseudo() != CssParser.Parsed.True)
					{
						break;
					}
					result = CssParser.Parsed.True;
				}
			}
			return result;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00013EEC File Offset: 0x000120EC
		private CssParser.Parsed ParseClass()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ".")
			{
				this.AppendCurrent();
				this.NextToken();
				if (this.CurrentTokenType == TokenType.Identifier)
				{
					this.AppendCurrent();
					this.NextToken();
					result = CssParser.Parsed.True;
				}
				else if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "%")
				{
					this.UpdateIfReplacementToken();
					if (this.CurrentTokenType == TokenType.ReplacementToken)
					{
						this.AppendCurrent();
						this.NextToken();
						result = CssParser.Parsed.True;
					}
					else
					{
						this.ReportError(0, CssErrorCode.ExpectedIdentifier, new object[]
						{
							this.CurrentTokenText
						});
					}
				}
				else
				{
					this.ReportError(0, CssErrorCode.ExpectedIdentifier, new object[]
					{
						this.CurrentTokenText
					});
				}
			}
			else if (this.CurrentTokenType == TokenType.Dimension || this.CurrentTokenType == TokenType.Number)
			{
				string text = this.m_scanner.RawNumber;
				if (text != null && text.StartsWith(".", StringComparison.Ordinal))
				{
					result = CssParser.Parsed.True;
					this.NextToken();
					if (this.CurrentTokenType == TokenType.Identifier)
					{
						text += this.CurrentTokenText;
						this.NextToken();
					}
					this.ReportError(2, CssErrorCode.PossibleInvalidClassName, new object[]
					{
						text
					});
					this.Append(text);
				}
			}
			return result;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00014048 File Offset: 0x00012248
		private CssParser.Parsed ParseElementName()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			bool flag = false;
			if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "|")
			{
				flag = true;
				this.AppendCurrent();
				this.NextToken();
			}
			if (this.CurrentTokenType == TokenType.Identifier || (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "*"))
			{
				string namespaceIdent = flag ? null : this.CurrentTokenText;
				this.AppendCurrent();
				this.NextToken();
				result = CssParser.Parsed.True;
				if (!flag && this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "|")
				{
					this.ValidateNamespace(namespaceIdent);
					this.AppendCurrent();
					this.NextToken();
					if (this.CurrentTokenType == TokenType.Identifier || (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "*"))
					{
						this.AppendCurrent();
						this.NextToken();
					}
					else
					{
						result = CssParser.Parsed.False;
						this.ReportError(0, CssErrorCode.ExpectedIdentifier, new object[]
						{
							this.CurrentTokenText
						});
					}
				}
			}
			else if (flag)
			{
				this.ReportError(0, CssErrorCode.ExpectedIdentifier, new object[]
				{
					this.CurrentTokenText
				});
			}
			return result;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00014188 File Offset: 0x00012388
		private CssParser.Parsed ParseAttrib()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "[")
			{
				this.Append('[');
				this.SkipSpace();
				bool flag = false;
				if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "|")
				{
					flag = true;
					this.AppendCurrent();
					this.NextToken();
				}
				if (this.CurrentTokenType == TokenType.Identifier || (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "*"))
				{
					string namespaceIdent = flag ? null : this.CurrentTokenText;
					this.AppendCurrent();
					this.SkipSpace();
					if (!flag && this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "|")
					{
						this.ValidateNamespace(namespaceIdent);
						this.AppendCurrent();
						this.SkipSpace();
						if (this.CurrentTokenType == TokenType.Identifier || (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "*"))
						{
							this.AppendCurrent();
							this.SkipSpace();
						}
						else
						{
							this.ReportError(0, CssErrorCode.ExpectedIdentifier, new object[]
							{
								this.CurrentTokenText
							});
						}
					}
				}
				else
				{
					this.ReportError(0, CssErrorCode.ExpectedIdentifier, new object[]
					{
						this.CurrentTokenText
					});
				}
				if ((this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "=") || this.CurrentTokenType == TokenType.Includes || this.CurrentTokenType == TokenType.DashMatch || this.CurrentTokenType == TokenType.PrefixMatch || this.CurrentTokenType == TokenType.SuffixMatch || this.CurrentTokenType == TokenType.SubstringMatch)
				{
					this.AppendCurrent();
					this.SkipSpace();
					if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "%")
					{
						this.UpdateIfReplacementToken();
						if (this.CurrentTokenType != TokenType.ReplacementToken)
						{
							this.ReportError(0, CssErrorCode.ExpectedIdentifierOrString, new object[]
							{
								this.CurrentTokenText
							});
						}
					}
					else if (this.CurrentTokenType != TokenType.Identifier && this.CurrentTokenType != TokenType.String)
					{
						this.ReportError(0, CssErrorCode.ExpectedIdentifierOrString, new object[]
						{
							this.CurrentTokenText
						});
					}
					this.AppendCurrent();
					this.SkipSpace();
				}
				if (this.CurrentTokenType != TokenType.Character || this.CurrentTokenText != "]")
				{
					this.ReportError(0, CssErrorCode.ExpectedClosingBracket, new object[]
					{
						this.CurrentTokenText
					});
				}
				this.Append(']');
				this.NextToken();
				result = CssParser.Parsed.True;
			}
			return result;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00014430 File Offset: 0x00012630
		private CssParser.Parsed ParsePseudo()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ":")
			{
				this.Append(':');
				this.NextToken();
				if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ":")
				{
					this.Append(':');
					this.NextToken();
				}
				TokenType currentTokenType = this.CurrentTokenType;
				if (currentTokenType != TokenType.Identifier)
				{
					switch (currentTokenType)
					{
					case TokenType.Function:
						this.AppendCurrent();
						this.SkipSpace();
						this.ParseExpression();
						while (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ",")
						{
							this.AppendCurrent();
							this.NextToken();
							this.ParseExpression();
						}
						if (this.CurrentTokenType != TokenType.Character || this.CurrentTokenText != ")")
						{
							this.ReportError(0, CssErrorCode.ExpectedIdentifier, new object[]
							{
								this.CurrentTokenText
							});
						}
						this.AppendCurrent();
						this.NextToken();
						break;
					case TokenType.Not:
						this.AppendCurrent();
						this.SkipSpace();
						result = this.ParseSimpleSelector();
						this.SkipIfSpace();
						if (this.CurrentTokenType != TokenType.Character || this.CurrentTokenText != ")")
						{
							this.ReportError(0, CssErrorCode.ExpectedIdentifier, new object[]
							{
								this.CurrentTokenText
							});
						}
						this.AppendCurrent();
						this.NextToken();
						break;
					default:
						this.ReportError(0, CssErrorCode.ExpectedIdentifier, new object[]
						{
							this.CurrentTokenText
						});
						break;
					}
				}
				else
				{
					this.AppendCurrent();
					this.NextToken();
				}
				result = CssParser.Parsed.True;
			}
			return result;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000145F8 File Offset: 0x000127F8
		private CssParser.Parsed ParseExpression()
		{
			CssParser.Parsed result = CssParser.Parsed.Empty;
			for (;;)
			{
				TokenType currentTokenType = this.CurrentTokenType;
				if (currentTokenType <= TokenType.Identifier)
				{
					if (currentTokenType != TokenType.Space)
					{
						switch (currentTokenType)
						{
						case TokenType.String:
						case TokenType.Identifier:
							goto IL_41;
						}
						break;
					}
					this.NextToken();
					continue;
				}
				else
				{
					switch (currentTokenType)
					{
					case TokenType.Dimension:
					case TokenType.Number:
						break;
					case TokenType.Percentage:
						return result;
					default:
						if (currentTokenType != TokenType.Character)
						{
							goto Block_5;
						}
						if (this.CurrentTokenText == "+" || this.CurrentTokenText == "-")
						{
							result = CssParser.Parsed.True;
							this.AppendCurrent();
							this.NextToken();
							continue;
						}
						return result;
					}
				}
				IL_41:
				result = CssParser.Parsed.True;
				this.AppendCurrent();
				this.NextToken();
			}
			Block_5:
			return result;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x000146A0 File Offset: 0x000128A0
		private CssParser.Parsed ParseDeclaration()
		{
			CssParser.Parsed parsed = CssParser.Parsed.Empty;
			string text = null;
			if (this.CurrentTokenType == TokenType.Character && (this.CurrentTokenText == "*" || this.CurrentTokenText == "."))
			{
				this.ReportError(4, CssErrorCode.HackGeneratesInvalidCss, new object[]
				{
					this.CurrentTokenText
				});
				text = this.CurrentTokenText;
				this.NextToken();
			}
			if (this.CurrentTokenType == TokenType.Identifier)
			{
				string currentTokenText = this.CurrentTokenText;
				this.NewLine();
				if (text != null)
				{
					this.Append(text);
				}
				this.AppendCurrent();
				this.SkipSpaceComment();
				if (this.CurrentTokenType != TokenType.Character || this.CurrentTokenText != ":")
				{
					this.ReportError(0, CssErrorCode.ExpectedColon, new object[]
					{
						this.CurrentTokenText
					});
					this.SkipToEndOfDeclaration();
					return CssParser.Parsed.True;
				}
				this.Append(':');
				if (this.Settings.OutputMode == OutputMode.MultipleLines)
				{
					this.Append(' ');
				}
				this.SkipSpace();
				if (this.m_valueReplacement != null)
				{
					this.Append(this.m_valueReplacement);
					this.m_valueReplacement = null;
					this.m_noOutput = true;
					this.ParseExpr();
					this.m_noOutput = false;
				}
				else
				{
					this.m_parsingColorValue = CssParser.MightContainColorNames(currentTokenText);
					parsed = this.ParseExpr();
					this.m_parsingColorValue = false;
					if (parsed != CssParser.Parsed.True)
					{
						this.ReportError(0, CssErrorCode.ExpectedExpression, new object[]
						{
							this.CurrentTokenText
						});
						this.SkipToEndOfDeclaration();
						return CssParser.Parsed.True;
					}
				}
				this.ParsePrio();
				parsed = CssParser.Parsed.True;
			}
			return parsed;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00014834 File Offset: 0x00012A34
		private CssParser.Parsed ParsePrio()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.ImportantSymbol)
			{
				if (this.Settings.OutputMode == OutputMode.MultipleLines)
				{
					this.Append(' ');
				}
				this.AppendCurrent();
				this.SkipSpace();
				if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "!")
				{
					this.ReportError(4, CssErrorCode.HackGeneratesInvalidCss, new object[]
					{
						this.CurrentTokenText
					});
					this.AppendCurrent();
					this.SkipSpace();
				}
				result = CssParser.Parsed.True;
			}
			else if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "!")
			{
				if (this.Settings.OutputMode == OutputMode.MultipleLines)
				{
					this.Append(' ');
				}
				this.AppendCurrent();
				this.NextToken();
				if (this.CurrentTokenType == TokenType.Identifier)
				{
					this.ReportError(4, CssErrorCode.HackGeneratesInvalidCss, new object[]
					{
						this.CurrentTokenText
					});
					this.AppendCurrent();
					this.SkipSpace();
					result = CssParser.Parsed.True;
				}
				else
				{
					this.ReportError(0, CssErrorCode.ExpectedIdentifier, new object[]
					{
						this.CurrentTokenText
					});
				}
			}
			return result;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00014964 File Offset: 0x00012B64
		private CssParser.Parsed ParseExpr()
		{
			CssParser.Parsed parsed = this.ParseTerm(false);
			if (parsed == CssParser.Parsed.True)
			{
				while (!this.m_scanner.EndOfFile)
				{
					CssParser.Parsed parsed2 = this.ParseOperator();
					if (parsed2 != CssParser.Parsed.False && this.ParseTerm(parsed2 == CssParser.Parsed.Empty) == CssParser.Parsed.False)
					{
						break;
					}
				}
			}
			return parsed;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000149A4 File Offset: 0x00012BA4
		private CssParser.Parsed ParseFunctionParameters()
		{
			CssParser.Parsed parsed = this.ParseTerm(false);
			if (parsed == CssParser.Parsed.True)
			{
				while (!this.m_scanner.EndOfFile)
				{
					if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "=")
					{
						this.AppendCurrent();
						this.SkipSpace();
						this.ParseTerm(false);
					}
					CssParser.Parsed parsed2 = this.ParseOperator();
					if (parsed2 != CssParser.Parsed.False && this.ParseTerm(parsed2 == CssParser.Parsed.Empty) == CssParser.Parsed.False)
					{
						break;
					}
				}
			}
			else if (parsed == CssParser.Parsed.False && this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ")")
			{
				parsed = CssParser.Parsed.Empty;
			}
			return parsed;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00014A3C File Offset: 0x00012C3C
		private CssParser.Parsed ParseTerm(bool wasEmpty)
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			bool flag = false;
			if (this.CurrentTokenType == TokenType.Character && (this.CurrentTokenText == "-" || this.CurrentTokenText == "+"))
			{
				if (wasEmpty)
				{
					if (this.m_skippedSpace)
					{
						this.Append(' ');
					}
					wasEmpty = false;
				}
				this.AppendCurrent();
				this.NextToken();
				flag = true;
			}
			switch (this.CurrentTokenType)
			{
			case TokenType.String:
			case TokenType.Identifier:
			case TokenType.Uri:
			case TokenType.UnicodeRange:
				if (flag)
				{
					this.ReportError(0, CssErrorCode.TokenAfterUnaryNotAllowed, new object[]
					{
						this.CurrentTokenText
					});
				}
				if (wasEmpty)
				{
					if (this.m_skippedSpace)
					{
						this.Append(' ');
					}
					wasEmpty = false;
				}
				this.AppendCurrent();
				this.SkipSpace();
				return CssParser.Parsed.True;
			case TokenType.Hash:
				if (flag)
				{
					this.ReportError(0, CssErrorCode.HashAfterUnaryNotAllowed, new object[]
					{
						this.CurrentTokenText
					});
				}
				if (wasEmpty)
				{
					this.Append(' ');
					wasEmpty = false;
				}
				if (this.ParseHexcolor() == CssParser.Parsed.False)
				{
					this.ReportError(0, CssErrorCode.ExpectedHexColor, new object[]
					{
						this.CurrentTokenText
					});
					this.AppendCurrent();
					this.SkipSpace();
				}
				return CssParser.Parsed.True;
			case TokenType.ImportSymbol:
			case TokenType.PageSymbol:
			case TokenType.MediaSymbol:
			case TokenType.FontFaceSymbol:
			case TokenType.CharacterSetSymbol:
			case TokenType.AtKeyword:
			case TokenType.ImportantSymbol:
			case TokenType.NamespaceSymbol:
			case TokenType.KeyFramesSymbol:
			case TokenType.Speech:
			case TokenType.Not:
				goto IL_452;
			case TokenType.RelativeLength:
			case TokenType.AbsoluteLength:
			case TokenType.Resolution:
			case TokenType.Angle:
			case TokenType.Time:
			case TokenType.Frequency:
			case TokenType.Percentage:
			case TokenType.Number:
				break;
			case TokenType.Dimension:
				this.ReportError(2, CssErrorCode.UnexpectedDimension, new object[]
				{
					this.CurrentTokenText
				});
				break;
			case TokenType.Function:
				if (wasEmpty)
				{
					this.Append(' ');
					wasEmpty = false;
				}
				if (this.ParseFunction() == CssParser.Parsed.False)
				{
					this.ReportError(0, CssErrorCode.ExpectedFunction, new object[]
					{
						this.CurrentTokenText
					});
				}
				return CssParser.Parsed.True;
			case TokenType.ProgId:
				if (wasEmpty)
				{
					this.Append(' ');
					wasEmpty = false;
				}
				if (this.ParseProgId() == CssParser.Parsed.False)
				{
					this.ReportError(0, CssErrorCode.ExpectedProgId, new object[]
					{
						this.CurrentTokenText
					});
				}
				return CssParser.Parsed.True;
			case TokenType.Character:
				if (this.CurrentTokenText == "(")
				{
					if (wasEmpty)
					{
						if (this.m_skippedSpace)
						{
							this.Append(' ');
						}
						wasEmpty = false;
					}
					this.AppendCurrent();
					this.SkipSpace();
					if (this.ParseExpr() == CssParser.Parsed.False)
					{
						this.ReportError(0, CssErrorCode.ExpectedExpression, new object[]
						{
							this.CurrentTokenText
						});
					}
					if (this.CurrentTokenType != TokenType.Character || !(this.CurrentTokenText == ")"))
					{
						this.ReportError(0, CssErrorCode.ExpectedClosingParenthesis, new object[]
						{
							this.CurrentTokenText
						});
						return result;
					}
					this.AppendCurrent();
					result = CssParser.Parsed.True;
					this.m_skippedSpace = false;
					this.NextRawToken();
					if (this.CurrentTokenType == TokenType.Space)
					{
						this.m_skippedSpace = true;
					}
					if (this.CurrentTokenType != TokenType.Character || !(this.CurrentTokenText == "["))
					{
						return result;
					}
					this.AppendCurrent();
					this.SkipSpace();
					if (this.CurrentTokenType != TokenType.Number)
					{
						this.ReportError(0, CssErrorCode.ExpectedNumber, new object[]
						{
							this.CurrentTokenText
						});
						return CssParser.Parsed.False;
					}
					this.AppendCurrent();
					this.SkipSpace();
					if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "]")
					{
						this.AppendCurrent();
						this.SkipSpace();
						return result;
					}
					this.ReportError(0, CssErrorCode.ExpectedClosingBracket, new object[]
					{
						this.CurrentTokenText
					});
					return CssParser.Parsed.False;
				}
				else
				{
					if (!(this.CurrentTokenText == "%"))
					{
						goto IL_452;
					}
					this.UpdateIfReplacementToken();
					if (this.CurrentTokenType == TokenType.ReplacementToken)
					{
						if (wasEmpty)
						{
							this.Append(' ');
							wasEmpty = false;
						}
						this.AppendCurrent();
						this.SkipSpace();
						return CssParser.Parsed.True;
					}
					goto IL_452;
				}
				break;
			default:
				goto IL_452;
			}
			if (wasEmpty)
			{
				this.Append(' ');
				wasEmpty = false;
			}
			this.AppendCurrent();
			this.SkipSpace();
			return CssParser.Parsed.True;
			IL_452:
			if (flag)
			{
				this.ReportError(0, CssErrorCode.UnexpectedToken, new object[]
				{
					this.CurrentTokenText
				});
			}
			return result;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00014EC0 File Offset: 0x000130C0
		private CssParser.Parsed ParseProgId()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.ProgId)
			{
				this.ReportError(4, CssErrorCode.ProgIdIEOnly, new object[0]);
				this.m_noColorAbbreviation = true;
				this.AppendCurrent();
				this.SkipSpace();
				while (this.CurrentTokenType == TokenType.Identifier)
				{
					this.AppendCurrent();
					this.SkipSpace();
					if (this.CurrentTokenType != TokenType.Character && this.CurrentTokenText != "=")
					{
						this.ReportError(0, CssErrorCode.ExpectedEqualSign, new object[]
						{
							this.CurrentTokenText
						});
					}
					this.Append('=');
					this.SkipSpace();
					if (this.ParseTerm(false) != CssParser.Parsed.True)
					{
						this.ReportError(0, CssErrorCode.ExpectedTerm, new object[]
						{
							this.CurrentTokenText
						});
					}
					if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ",")
					{
						this.Append(',');
						this.SkipSpace();
					}
				}
				this.m_noColorAbbreviation = false;
				if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ")")
				{
					this.Append(')');
					this.SkipSpace();
				}
				else
				{
					this.ReportError(0, CssErrorCode.UnexpectedToken, new object[]
					{
						this.CurrentTokenText
					});
				}
				result = CssParser.Parsed.True;
			}
			return result;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00015020 File Offset: 0x00013220
		private static string GetRoot(string text)
		{
			if (text.StartsWith("-", StringComparison.Ordinal))
			{
				Match match = CssParser.s_vendorSpecific.Match(text);
				if (match.Success)
				{
					text = match.Result("${root}");
				}
			}
			return text;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00015060 File Offset: 0x00013260
		private CssParser.Parsed ParseFunction()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.Function)
			{
				string root = CssParser.GetRoot(this.CurrentTokenText);
				string a;
				if ((a = root.ToUpperInvariant()) != null)
				{
					if (a == "RGB(")
					{
						return this.ParseRgb();
					}
					if (a == "EXPRESSION(")
					{
						return this.ParseExpressionFunction();
					}
					if (a == "CALC(")
					{
						return this.ParseCalc();
					}
					if (a == "MIN(" || a == "MAX(")
					{
						return this.ParseMinMax();
					}
				}
				this.AppendCurrent();
				this.SkipSpace();
				if (this.ParseFunctionParameters() == CssParser.Parsed.False)
				{
					this.ReportError(0, CssErrorCode.ExpectedExpression, new object[]
					{
						this.CurrentTokenText
					});
				}
				if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ")")
				{
					this.AppendCurrent();
					this.SkipSpace();
					result = CssParser.Parsed.True;
				}
				else
				{
					this.ReportError(0, CssErrorCode.UnexpectedToken, new object[]
					{
						this.CurrentTokenText
					});
				}
			}
			return result;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00015188 File Offset: 0x00013388
		private CssParser.Parsed ParseRgb()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.Function && string.Compare(this.CurrentTokenText, "rgb(", StringComparison.OrdinalIgnoreCase) == 0)
			{
				bool flag = false;
				bool flag2 = false;
				int[] array = new int[3];
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.CurrentTokenText.ToLowerInvariant());
				string text = this.NextSignificantToken();
				if (text.Length > 0)
				{
					stringBuilder.Append(text);
					flag = true;
				}
				for (int i = 0; i < 3; i++)
				{
					if (i > 0)
					{
						if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ",")
						{
							stringBuilder.Append(',');
						}
						else
						{
							if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ")")
							{
								this.ReportError(0, CssErrorCode.ExpectedComma, new object[]
								{
									this.CurrentTokenText
								});
								flag = true;
								break;
							}
							this.ReportError(0, CssErrorCode.ExpectedComma, new object[]
							{
								this.CurrentTokenText
							});
							stringBuilder.Append(this.CurrentTokenText);
							flag = true;
						}
						text = this.NextSignificantToken();
						if (text.Length > 0)
						{
							stringBuilder.Append(text);
							flag = true;
						}
					}
					bool flag3 = false;
					if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == "-")
					{
						flag3 = true;
						text = this.NextSignificantToken();
						if (text.Length > 0)
						{
							stringBuilder.Append(text);
							flag = true;
						}
					}
					string text2 = this.CurrentTokenText;
					float num2;
					if (this.CurrentTokenType != TokenType.Number && this.CurrentTokenType != TokenType.Percentage)
					{
						this.ReportError(0, CssErrorCode.ExpectedRgbNumberOrPercentage, new object[]
						{
							this.CurrentTokenText
						});
						flag = true;
					}
					else if (this.CurrentTokenType == TokenType.Number)
					{
						float num;
						if (text2.TryParseSingleInvariant(out num))
						{
							num *= (float)(flag3 ? -1 : 1);
							if (num < 0f)
							{
								text2 = "0";
								array[i] = 0;
							}
							else if (num > 255f)
							{
								text2 = "255";
								array[i] = 255;
							}
							else
							{
								array[i] = Convert.ToInt32(num);
							}
						}
						else
						{
							flag = true;
						}
					}
					else if (text2.Substring(0, text2.Length - 1).TryParseSingleInvariant(out num2))
					{
						num2 *= (float)(flag3 ? -1 : 1);
						if (num2 < 0f)
						{
							text2 = "0%";
							array[i] = 0;
						}
						else if (num2 > 100f)
						{
							text2 = "100%";
							array[i] = 255;
						}
						else
						{
							array[i] = Convert.ToInt32(num2 * 255f / 100f);
						}
					}
					else
					{
						flag = true;
					}
					stringBuilder.Append(text2);
					text = this.NextSignificantToken();
					if (text.Length > 0)
					{
						stringBuilder.Append(text);
						flag = true;
					}
				}
				if (flag)
				{
					this.Append(stringBuilder.ToString());
				}
				else
				{
					string hexColor = "#{0:x2}{1:x2}{2:x2}".FormatInvariant(new object[]
					{
						array[0],
						array[1],
						array[2]
					});
					string obj = CssParser.CrunchHexColor(hexColor, this.Settings.ColorNames, this.m_noColorAbbreviation);
					this.Append(obj);
					flag2 = true;
				}
				if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ")")
				{
					if (!flag2)
					{
						this.AppendCurrent();
					}
					this.SkipSpace();
					result = CssParser.Parsed.True;
				}
				else
				{
					this.ReportError(0, CssErrorCode.ExpectedClosingParenthesis, new object[]
					{
						this.CurrentTokenText
					});
				}
			}
			return result;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00015568 File Offset: 0x00013768
		private CssParser.Parsed ParseExpressionFunction()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.Function && string.Compare(this.CurrentTokenText, "expression(", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.Append(this.CurrentTokenText.ToLowerInvariant());
				this.NextToken();
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				while (!this.m_scanner.EndOfFile && (this.CurrentTokenType != TokenType.Character || this.CurrentTokenText != ")" || num > 0))
				{
					string currentTokenText;
					if (this.CurrentTokenType == TokenType.Function)
					{
						num++;
					}
					else if (this.CurrentTokenType == TokenType.Character && (currentTokenText = this.CurrentTokenText) != null)
					{
						if (!(currentTokenText == "("))
						{
							if (currentTokenText == ")")
							{
								num--;
							}
						}
						else
						{
							num++;
						}
					}
					stringBuilder.Append(this.CurrentTokenText);
					this.NextToken();
				}
				string text = stringBuilder.ToString();
				if (this.Settings.MinifyExpressions)
				{
					JSParser jsparser = new JSParser();
					bool containsErrors = false;
					jsparser.CompilerError += delegate(object sender, ContextErrorEventArgs ea)
					{
						this.ReportError(0, CssErrorCode.ExpressionError, new object[]
						{
							ea.Error.Message
						});
						containsErrors = true;
					};
					Block block = jsparser.Parse(new DocumentContext(text)
					{
						FileContext = this.FileContext
					}, this.m_jsSettings);
					if (block != null && !containsErrors)
					{
						this.Append(OutputVisitor.Apply(block, jsparser.Settings));
					}
					else
					{
						this.Append(text);
					}
				}
				else
				{
					this.Append(text);
				}
				if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ")")
				{
					this.AppendCurrent();
					this.SkipSpace();
					result = CssParser.Parsed.True;
				}
				else
				{
					this.ReportError(0, CssErrorCode.ExpectedClosingParenthesis, new object[]
					{
						this.CurrentTokenText
					});
				}
			}
			return result;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00015744 File Offset: 0x00013944
		private CssParser.Parsed ParseHexcolor()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.Hash)
			{
				string text = this.CurrentTokenText;
				bool flag = false;
				if ((text.Length == 5 || text.Length == 8 || text.Length == 10) && text.EndsWith("\t", StringComparison.Ordinal))
				{
					text = text.Substring(0, text.Length - 1);
					flag = true;
				}
				if (text.Length == 4 || text.Length == 7 || text.Length == 9)
				{
					result = CssParser.Parsed.True;
					string obj = CssParser.CrunchHexColor(text, this.Settings.ColorNames, this.m_noColorAbbreviation);
					this.Append(obj);
					if (flag)
					{
						this.Append("\\9");
					}
					this.SkipSpace();
				}
			}
			return result;
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000157FC File Offset: 0x000139FC
		private CssParser.Parsed ParseUnit()
		{
			CssParser.Parsed parsed = CssParser.Parsed.Empty;
			if (this.CurrentTokenType == TokenType.Character && (this.CurrentTokenText == "+" || this.CurrentTokenText == "-"))
			{
				this.AppendCurrent();
				this.NextToken();
				parsed = CssParser.Parsed.False;
			}
			switch (this.CurrentTokenType)
			{
			case TokenType.RelativeLength:
			case TokenType.AbsoluteLength:
			case TokenType.Resolution:
			case TokenType.Angle:
			case TokenType.Time:
			case TokenType.Frequency:
			case TokenType.Dimension:
			case TokenType.Percentage:
			case TokenType.Number:
				this.AppendCurrent();
				this.SkipSpace();
				parsed = CssParser.Parsed.True;
				break;
			case TokenType.Function:
				parsed = this.ParseFunction();
				if (parsed == CssParser.Parsed.Empty)
				{
					this.ReportError(0, CssErrorCode.UnexpectedFunction, new object[]
					{
						this.CurrentTokenText
					});
					parsed = CssParser.Parsed.False;
				}
				break;
			case TokenType.Character:
				if (this.CurrentTokenText == "(")
				{
					this.AppendCurrent();
					this.SkipSpace();
					parsed = this.ParseSum();
					if (parsed != CssParser.Parsed.True)
					{
						this.ReportError(0, CssErrorCode.ExpectedSum, new object[]
						{
							this.CurrentTokenText
						});
						parsed = CssParser.Parsed.False;
					}
					else if (this.CurrentTokenType != TokenType.Character || this.CurrentTokenText != ")")
					{
						this.ReportError(0, CssErrorCode.ExpectedClosingParenthesis, new object[]
						{
							this.CurrentTokenText
						});
						parsed = CssParser.Parsed.False;
					}
					else
					{
						this.AppendCurrent();
						this.SkipSpace();
						parsed = CssParser.Parsed.True;
					}
				}
				break;
			}
			return parsed;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00015984 File Offset: 0x00013B84
		private CssParser.Parsed ParseProduct()
		{
			CssParser.Parsed parsed = this.ParseUnit();
			if (parsed == CssParser.Parsed.True)
			{
				for (;;)
				{
					if (this.CurrentTokenType != TokenType.Character || (!(this.CurrentTokenText == "*") && !(this.CurrentTokenText == "/")))
					{
						if (this.CurrentTokenType != TokenType.Identifier)
						{
							break;
						}
						if (string.Compare(this.CurrentTokenText, "mod", StringComparison.OrdinalIgnoreCase) != 0)
						{
							break;
						}
					}
					if (this.CurrentTokenText == "*" || this.CurrentTokenText == "/")
					{
						if (this.Settings.OutputMode == OutputMode.MultipleLines)
						{
							this.Append(' ');
						}
						this.AppendCurrent();
						if (this.Settings.OutputMode == OutputMode.MultipleLines)
						{
							this.Append(' ');
						}
					}
					else
					{
						this.Append(" mod ");
					}
					this.SkipSpace();
					parsed = this.ParseUnit();
					if (parsed != CssParser.Parsed.True)
					{
						this.ReportError(0, CssErrorCode.ExpectedUnit, new object[]
						{
							this.CurrentTokenText
						});
						parsed = CssParser.Parsed.False;
					}
				}
			}
			else
			{
				this.ReportError(0, CssErrorCode.ExpectedUnit, new object[]
				{
					this.CurrentTokenText
				});
				parsed = CssParser.Parsed.False;
			}
			return parsed;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00015ABC File Offset: 0x00013CBC
		private CssParser.Parsed ParseSum()
		{
			CssParser.Parsed parsed = this.ParseProduct();
			if (parsed == CssParser.Parsed.True)
			{
				while (this.CurrentTokenType == TokenType.Character)
				{
					if (!(this.CurrentTokenText == "+") && !(this.CurrentTokenText == "-"))
					{
						break;
					}
					this.Append(' ');
					this.AppendCurrent();
					this.Append(' ');
					this.SkipSpace();
					parsed = this.ParseProduct();
					if (parsed != CssParser.Parsed.True)
					{
						this.ReportError(0, CssErrorCode.ExpectedProduct, new object[]
						{
							this.CurrentTokenText
						});
						parsed = CssParser.Parsed.False;
					}
				}
			}
			else
			{
				this.ReportError(0, CssErrorCode.ExpectedProduct, new object[]
				{
					this.CurrentTokenText
				});
				parsed = CssParser.Parsed.False;
			}
			return parsed;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00015B7C File Offset: 0x00013D7C
		private CssParser.Parsed ParseMinMax()
		{
			CssParser.Parsed parsed = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.Function && (string.Compare(this.CurrentTokenText, "min(", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(this.CurrentTokenText, "max(", StringComparison.OrdinalIgnoreCase) == 0))
			{
				this.Append(this.CurrentTokenText.ToLowerInvariant());
				this.SkipSpace();
				parsed = this.ParseSum();
				while (parsed == CssParser.Parsed.True && this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ",")
				{
					this.AppendCurrent();
					this.SkipSpace();
					parsed = this.ParseSum();
				}
				if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ")")
				{
					this.AppendCurrent();
					this.SkipSpace();
					parsed = CssParser.Parsed.True;
				}
				else
				{
					this.ReportError(0, CssErrorCode.ExpectedClosingParenthesis, new object[]
					{
						this.CurrentTokenText
					});
					parsed = CssParser.Parsed.False;
				}
			}
			return parsed;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00015C64 File Offset: 0x00013E64
		private CssParser.Parsed ParseCalc()
		{
			CssParser.Parsed result = CssParser.Parsed.False;
			if (this.CurrentTokenType == TokenType.Function && string.Compare(CssParser.GetRoot(this.CurrentTokenText), "calc(", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.Append(this.CurrentTokenText.ToLowerInvariant());
				this.SkipSpace();
				if (this.ParseSum() != CssParser.Parsed.True)
				{
					this.ReportError(0, CssErrorCode.ExpectedSum, new object[]
					{
						this.CurrentTokenText
					});
				}
				if (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ")")
				{
					this.AppendCurrent();
					this.SkipSpace();
					result = CssParser.Parsed.True;
				}
				else
				{
					this.ReportError(0, CssErrorCode.ExpectedClosingParenthesis, new object[]
					{
						this.CurrentTokenText
					});
				}
			}
			return result;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00015D24 File Offset: 0x00013F24
		private TokenType NextToken()
		{
			this.m_currentToken = this.m_scanner.NextToken();
			this.m_encounteredNewLine = this.m_scanner.GotEndOfLine;
			while (this.CurrentTokenType == TokenType.Comment)
			{
				if (this.AppendCurrent())
				{
					this.NewLine();
				}
				this.m_currentToken = this.m_scanner.NextToken();
				this.m_encounteredNewLine = (this.m_encounteredNewLine || this.m_scanner.GotEndOfLine);
			}
			return this.CurrentTokenType;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00015DA0 File Offset: 0x00013FA0
		private TokenType NextRawToken()
		{
			this.m_currentToken = this.m_scanner.NextToken();
			this.m_encounteredNewLine = this.m_scanner.GotEndOfLine;
			return this.CurrentTokenType;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00015DCC File Offset: 0x00013FCC
		private string NextSignificantToken()
		{
			StringBuilder stringBuilder = null;
			this.m_currentToken = this.m_scanner.NextToken();
			this.m_encounteredNewLine = this.m_scanner.GotEndOfLine;
			while (this.CurrentTokenType == TokenType.Space || this.CurrentTokenType == TokenType.Comment)
			{
				if (this.CurrentTokenType == TokenType.Comment)
				{
					string text = this.CurrentTokenText;
					bool flag = text.StartsWith("/*!", StringComparison.Ordinal);
					if (flag)
					{
						text = this.NormalizeImportantComment(text);
					}
					bool flag2 = this.Settings.CommentMode == CssComment.All || (flag && this.Settings.CommentMode != CssComment.None);
					if (!flag)
					{
						Match match = CssParser.s_valueReplacement.Match(text);
						if (match.Success)
						{
							this.m_valueReplacement = null;
							IList<ResourceStrings> resourceStrings = this.Settings.ResourceStrings;
							if (resourceStrings.Count > 0)
							{
								string name = match.Result("${id}");
								for (int i = resourceStrings.Count - 1; i >= 0; i--)
								{
									this.m_valueReplacement = resourceStrings[i][name];
									if (this.m_valueReplacement != null)
									{
										break;
									}
								}
							}
							flag2 = (this.m_valueReplacement == null);
							if (flag2)
							{
								text = CssParser.NormalizedValueReplacementComment(text);
							}
						}
					}
					if (flag2)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder();
						}
						stringBuilder.Append(text);
					}
				}
				this.m_currentToken = this.m_scanner.NextToken();
				this.m_encounteredNewLine = (this.m_encounteredNewLine || this.m_scanner.GotEndOfLine);
			}
			if (stringBuilder != null)
			{
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00015F52 File Offset: 0x00014152
		private void UpdateIfReplacementToken()
		{
			this.m_currentToken = (this.m_scanner.ScanReplacementToken() ?? this.m_currentToken);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00015F70 File Offset: 0x00014170
		private void SkipSpace()
		{
			this.m_skippedSpace = false;
			this.NextToken();
			bool flag = this.m_encounteredNewLine;
			while (this.CurrentTokenType == TokenType.Space)
			{
				this.m_skippedSpace = true;
				this.NextToken();
				flag = (flag || this.m_encounteredNewLine);
			}
			this.m_encounteredNewLine = flag;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00015FC0 File Offset: 0x000141C0
		private void SkipSpaceComment()
		{
			this.m_skippedSpace = false;
			if (this.NextRawToken() == TokenType.Space)
			{
				this.m_skippedSpace = true;
				bool flag = this.m_encounteredNewLine;
				while (this.NextRawToken() == TokenType.Space)
				{
					flag = (flag || this.m_encounteredNewLine);
				}
				if (this.CurrentTokenType == TokenType.Comment)
				{
					if (this.Settings.CommentMode == CssComment.All || this.CurrentTokenText.StartsWith("/*!", StringComparison.Ordinal))
					{
						this.Append(' ');
						this.AppendCurrent();
					}
					this.SkipSpace();
					flag = (flag || this.m_encounteredNewLine);
				}
				this.m_encounteredNewLine = flag;
				return;
			}
			if (this.CurrentTokenType == TokenType.Comment)
			{
				bool encounteredNewLine = this.m_encounteredNewLine;
				this.AppendCurrent();
				this.SkipSpace();
				this.m_encounteredNewLine = (this.m_encounteredNewLine || encounteredNewLine);
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00016094 File Offset: 0x00014294
		private bool SkipIfSpace()
		{
			this.m_skippedSpace = false;
			bool result = this.CurrentTokenType == TokenType.Space;
			bool flag = this.m_encounteredNewLine;
			while (this.CurrentTokenType == TokenType.Space)
			{
				this.m_skippedSpace = true;
				this.NextToken();
				flag = (flag || this.m_encounteredNewLine);
			}
			this.m_encounteredNewLine = flag;
			return result;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000160E8 File Offset: 0x000142E8
		private void SkipToEndOfStatement()
		{
			bool flag = false;
			while (!this.m_scanner.EndOfFile && (this.CurrentTokenType != TokenType.Character || this.CurrentTokenText != ";"))
			{
				if (this.CurrentTokenType == TokenType.Character && (this.CurrentTokenText == "(" || this.CurrentTokenText == "[" || this.CurrentTokenText == "{"))
				{
					bool flag2 = this.CurrentTokenText == "{";
					this.SkipToClose();
					if (flag2)
					{
						return;
					}
					flag = false;
				}
				if (this.CurrentTokenType == TokenType.Space)
				{
					flag = true;
				}
				else
				{
					if (flag && CssParser.NeedsSpaceBefore(this.CurrentTokenText) && CssParser.NeedsSpaceAfter(this.m_lastOutputString))
					{
						this.Append(' ');
					}
					this.AppendCurrent();
					flag = false;
				}
				this.NextToken();
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000161D0 File Offset: 0x000143D0
		private void SkipToEndOfDeclaration()
		{
			bool flag = false;
			while (!this.m_scanner.EndOfFile && (this.CurrentTokenType != TokenType.Character || (this.CurrentTokenText != ";" && this.CurrentTokenText != "}")))
			{
				if (this.CurrentTokenType == TokenType.Character && (this.CurrentTokenText == "(" || this.CurrentTokenText == "[" || this.CurrentTokenText == "{"))
				{
					if (flag)
					{
						this.Append(' ');
					}
					this.SkipToClose();
					flag = false;
				}
				if (this.CurrentTokenType == TokenType.Space)
				{
					flag = true;
				}
				else
				{
					if (flag && CssParser.NeedsSpaceBefore(this.CurrentTokenText) && CssParser.NeedsSpaceAfter(this.m_lastOutputString))
					{
						this.Append(' ');
					}
					this.AppendCurrent();
					flag = false;
				}
				this.m_skippedSpace = false;
				this.NextToken();
				if (this.CurrentTokenType == TokenType.Space)
				{
					this.m_skippedSpace = true;
				}
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000162E0 File Offset: 0x000144E0
		private void SkipToClose()
		{
			bool flag = false;
			string currentTokenText;
			if ((currentTokenText = this.CurrentTokenText) != null)
			{
				string b;
				if (!(currentTokenText == "("))
				{
					if (!(currentTokenText == "["))
					{
						if (!(currentTokenText == "{"))
						{
							goto IL_4D;
						}
						b = "}";
					}
					else
					{
						b = "]";
					}
				}
				else
				{
					b = ")";
				}
				if (this.m_skippedSpace && this.CurrentTokenText != "{")
				{
					this.Append(' ');
				}
				this.AppendCurrent();
				this.m_skippedSpace = false;
				this.NextToken();
				if (this.CurrentTokenType == TokenType.Space)
				{
					this.m_skippedSpace = true;
				}
				while (!this.m_scanner.EndOfFile && (this.CurrentTokenType != TokenType.Character || this.CurrentTokenText != b))
				{
					if (this.CurrentTokenType == TokenType.Character && (this.CurrentTokenText == "(" || this.CurrentTokenText == "[" || this.CurrentTokenText == "{"))
					{
						this.SkipToClose();
						flag = false;
					}
					if (this.CurrentTokenType == TokenType.Space)
					{
						flag = true;
					}
					else
					{
						if (flag && CssParser.NeedsSpaceBefore(this.CurrentTokenText) && CssParser.NeedsSpaceAfter(this.m_lastOutputString))
						{
							this.Append(' ');
						}
						this.AppendCurrent();
						flag = false;
					}
					this.m_skippedSpace = false;
					this.NextToken();
					if (this.CurrentTokenType == TokenType.Space)
					{
						this.m_skippedSpace = true;
					}
				}
				return;
			}
			IL_4D:
			throw new ArgumentException("invalid closing match");
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001646C File Offset: 0x0001466C
		private void SkipSemicolons()
		{
			while (this.CurrentTokenType == TokenType.Character && this.CurrentTokenText == ";")
			{
				this.NextToken();
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000164A3 File Offset: 0x000146A3
		private static bool NeedsSpaceBefore(string text)
		{
			return text.IfNotNull((string t) => !"{}()[],;".Contains(t));
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000164D8 File Offset: 0x000146D8
		private static bool NeedsSpaceAfter(string text)
		{
			return text.IfNotNull((string t) => !"{}()[],;:".Contains(t));
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000164FD File Offset: 0x000146FD
		private bool AppendCurrent()
		{
			return this.Append(this.CurrentTokenText, this.CurrentTokenType);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00016514 File Offset: 0x00014714
		private bool Append(object obj, TokenType tokenType)
		{
			bool flag = false;
			bool flag2 = false;
			if (!this.m_noOutput)
			{
				string text = obj.ToString();
				if (this.Settings.ReplacementTokens.Count > 0)
				{
					text = CommonData.ReplacementToken.Replace(text, new MatchEvaluator(this.GetReplacementValue));
				}
				if (tokenType == TokenType.Identifier || tokenType == TokenType.Dimension)
				{
					StringBuilder stringBuilder = null;
					int num = 0;
					bool flag3 = false;
					int num2 = 0;
					if (tokenType == TokenType.Identifier)
					{
						num2 = ((text[0] == '_' || text[0] == '-') ? 1 : 0);
						if (num2 < text.Length)
						{
							char c = text[num2];
							if (c < '\u0080' && (c < 'A' || 'Z' < c) && (c < 'a' || 'z' < c) && c != '\\')
							{
								stringBuilder = new StringBuilder();
								if (num2 > 0)
								{
									stringBuilder.Append(text[0]);
								}
								flag3 = CssParser.EscapeCharacter(stringBuilder, text[num2]);
								flag2 = true;
								num = num2 + 1;
							}
						}
					}
					else
					{
						if (text[0] == '+' || text[0] == '-')
						{
							num2++;
						}
						while ('0' <= text[num2] && text[num2] <= '9')
						{
							num2++;
						}
						if (text[num2] == '.')
						{
							num2++;
						}
						while ('0' <= text[num2] && text[num2] <= '9')
						{
							num2++;
						}
						num2--;
					}
					for (int i = num2 + 1; i < text.Length; i++)
					{
						char c2 = text[i];
						if (c2 < '\u0080')
						{
							if (c2 == '\\')
							{
								i++;
							}
							else if (c2 != '-' && c2 != '_' && c2 != ' ' && ('0' > c2 || c2 > '9') && ('a' > c2 || c2 > 'z') && ('A' > c2 || c2 > 'Z'))
							{
								if (stringBuilder == null)
								{
									stringBuilder = new StringBuilder();
								}
								if (num < i)
								{
									string text2 = text.Substring(num, i - num);
									if ((flag3 && CssScanner.IsH(text2[0])) || (flag2 && text2[0] == ' '))
									{
										stringBuilder.Append(' ');
									}
									stringBuilder.Append(text2);
								}
								flag3 = CssParser.EscapeCharacter(stringBuilder, text[i]);
								flag2 = true;
								num = i + 1;
							}
						}
					}
					if (stringBuilder != null)
					{
						if (num < text.Length)
						{
							string text3 = text.Substring(num);
							if ((flag3 && CssScanner.IsH(text3[0])) || text3[0] == ' ')
							{
								stringBuilder.Append(' ');
							}
							stringBuilder.Append(text3);
							flag2 = false;
						}
						text = stringBuilder.ToString();
					}
				}
				else if (tokenType == TokenType.String)
				{
					StringBuilder stringBuilder2 = null;
					int num3 = 0;
					for (int j = 0; j < text.Length; j++)
					{
						char c3 = text[j];
						if (c3 < ' ')
						{
							if (stringBuilder2 == null)
							{
								stringBuilder2 = new StringBuilder();
							}
							if (num3 < j)
							{
								stringBuilder2.Append(text.Substring(num3, j - num3));
							}
							stringBuilder2.Append("\\{0:x}".FormatInvariant(new object[]
							{
								char.ConvertToUtf32(text, j)
							}));
							if (j + 1 < text.Length && CssScanner.IsH(text[j + 1]))
							{
								stringBuilder2.Append(' ');
							}
							num3 = j + 1;
						}
					}
					if (stringBuilder2 != null && num3 < text.Length)
					{
						stringBuilder2.Append(text.Substring(num3));
					}
					text = ((stringBuilder2 == null) ? text : stringBuilder2.ToString());
				}
				bool flag4 = false;
				flag = (tokenType != TokenType.Comment);
				if (!flag)
				{
					if (text.StartsWith("/*!", StringComparison.Ordinal))
					{
						if (this.Settings.CommentMode == CssComment.None)
						{
							return false;
						}
						text = this.NormalizeImportantComment(text);
						int num4 = text.IndexOf('/');
						if (num4 > 0 && this.m_outputNewLine)
						{
							text = text.Substring(num4);
						}
					}
					else
					{
						Match match = CssParser.s_valueReplacement.Match(this.CurrentTokenText);
						if (match.Success)
						{
							this.m_valueReplacement = null;
							IList<ResourceStrings> resourceStrings = this.Settings.ResourceStrings;
							if (resourceStrings.Count > 0)
							{
								string name = match.Result("${id}");
								for (int k = resourceStrings.Count - 1; k >= 0; k--)
								{
									this.m_valueReplacement = resourceStrings[k][name];
									if (this.m_valueReplacement != null)
									{
										break;
									}
								}
							}
							if (this.m_valueReplacement != null)
							{
								return false;
							}
							text = CssParser.NormalizedValueReplacementComment(text);
						}
						else if (this.Settings.CommentMode != CssComment.All)
						{
							return false;
						}
					}
					flag4 = text.StartsWith("/*!", StringComparison.Ordinal);
				}
				else if (this.m_parsingColorValue && (tokenType == TokenType.Identifier || tokenType == TokenType.ReplacementToken))
				{
					if (!text.StartsWith("#", StringComparison.Ordinal))
					{
						bool flag5 = false;
						string text4 = text.ToLowerInvariant();
						string text5;
						switch (this.Settings.ColorNames)
						{
						case CssColor.Strict:
							if (ColorSlice.StrictHexShorterThanNameAndAllNonStrict.TryGetValue(text4, out text5))
							{
								text = text5;
								flag5 = true;
							}
							break;
						case CssColor.Hex:
							if (ColorSlice.AllColorNames.TryGetValue(text4, out text5))
							{
								text = text5;
								flag5 = true;
							}
							break;
						case CssColor.Major:
							if (ColorSlice.HexShorterThanName.TryGetValue(text4, out text5))
							{
								text = text5;
								flag5 = true;
							}
							break;
						}
						if (this.Settings.ColorNames != CssColor.Hex && !flag5 && ColorSlice.AllColorNames.TryGetValue(text4, out text5))
						{
							text = text4;
						}
					}
					else if (this.CurrentTokenType == TokenType.ReplacementToken)
					{
						text = CssParser.CrunchHexColor(text, this.Settings.ColorNames, this.m_noColorAbbreviation);
					}
				}
				if (this.m_mightNeedSpace && (CssScanner.IsH(text[0]) || text[0] == ' '))
				{
					if (this.m_lineLength >= this.Settings.LineBreakThreshold)
					{
						this.AddNewLine();
					}
					else
					{
						this.m_parsed.Append(' ');
						this.m_lineLength++;
					}
				}
				if (tokenType == TokenType.Comment && flag4)
				{
					this.AddNewLine();
				}
				if (text == " ")
				{
					if (this.m_lineLength >= this.Settings.LineBreakThreshold)
					{
						this.AddNewLine();
					}
					else
					{
						this.m_parsed.Append(' ');
						this.m_lineLength++;
					}
				}
				else
				{
					if (this.m_forceNewLine)
					{
						if (!this.m_outputNewLine && this.Settings.OutputMode == OutputMode.MultipleLines)
						{
							this.AddNewLine();
						}
						this.m_forceNewLine = false;
					}
					this.m_parsed.Append(text);
					this.m_outputNewLine = false;
					if (tokenType == TokenType.Comment && flag4)
					{
						this.AddNewLine();
						this.m_lineLength = 0;
						this.m_outputNewLine = true;
					}
					else
					{
						this.m_lineLength += text.Length;
					}
				}
				this.m_mightNeedSpace = flag2;
				this.m_lastOutputString = text;
			}
			return flag;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00016BF0 File Offset: 0x00014DF0
		private string GetReplacementValue(Match match)
		{
			string text = null;
			string text2 = match.Result("${token}");
			if (!text2.IsNullOrWhiteSpace() && !this.Settings.ReplacementTokens.TryGetValue(text2, out text))
			{
				string text3 = match.Result("${fallback}");
				if (!text3.IsNullOrWhiteSpace())
				{
					this.Settings.ReplacementFallbacks.TryGetValue(text3, out text);
				}
			}
			return text.IfNullOrWhiteSpace(string.Empty);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00016C5C File Offset: 0x00014E5C
		private static bool EscapeCharacter(StringBuilder sb, char character)
		{
			string text = "\\{0:x}".FormatInvariant(new object[]
			{
				(int)character
			});
			sb.Append(text);
			return text.Length < 7;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00016C96 File Offset: 0x00014E96
		private bool Append(object obj)
		{
			return this.Append(obj, TokenType.None);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00016CA0 File Offset: 0x00014EA0
		private void NewLine()
		{
			if (this.Settings.OutputMode == OutputMode.MultipleLines && !this.m_outputNewLine)
			{
				this.AddNewLine();
				this.m_lineLength = 0;
				this.m_outputNewLine = true;
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00016CCC File Offset: 0x00014ECC
		private void AddNewLine()
		{
			if (!this.m_outputNewLine)
			{
				if (this.Settings.OutputMode == OutputMode.MultipleLines)
				{
					this.m_parsed.AppendLine();
					string tabSpaces = this.Settings.TabSpaces;
					this.m_lineLength = tabSpaces.Length;
					if (this.m_lineLength > 0)
					{
						this.m_parsed.Append(tabSpaces);
					}
				}
				else
				{
					this.m_parsed.Append('\n');
					this.m_lineLength = 0;
				}
				this.m_outputNewLine = true;
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00016D47 File Offset: 0x00014F47
		private void Indent()
		{
			this.Settings.Indent();
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00016D54 File Offset: 0x00014F54
		private void Unindent()
		{
			this.Settings.Unindent();
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00016D64 File Offset: 0x00014F64
		private static string CrunchHexColor(string hexColor, CssColor colorNames, bool noAbbr)
		{
			if (!noAbbr)
			{
				hexColor = CssParser.s_rrggbb.Replace(hexColor, "#${r}${g}${b}").ToLowerInvariant();
			}
			if (colorNames != CssColor.Hex)
			{
				string text;
				if (ColorSlice.StrictNameShorterThanHex.TryGetValue(hexColor, out text))
				{
					hexColor = text;
				}
				else if (colorNames == CssColor.Major && ColorSlice.NameShorterThanHex.TryGetValue(hexColor, out text))
				{
					hexColor = text;
				}
			}
			return hexColor;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00016DBC File Offset: 0x00014FBC
		private static bool MightContainColorNames(string propertyName)
		{
			bool flag = propertyName.EndsWith("color", StringComparison.Ordinal);
			if (!flag && propertyName != null)
			{
				if (<PrivateImplementationDetails>{86487675-C393-48D4-AFEC-7657DB09B21F}.$$method0x600045d-1 == null)
				{
					<PrivateImplementationDetails>{86487675-C393-48D4-AFEC-7657DB09B21F}.$$method0x600045d-1 = new Dictionary<string, int>(7)
					{
						{
							"background",
							0
						},
						{
							"border-top",
							1
						},
						{
							"border-right",
							2
						},
						{
							"border-bottom",
							3
						},
						{
							"border-left",
							4
						},
						{
							"border",
							5
						},
						{
							"outline",
							6
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{86487675-C393-48D4-AFEC-7657DB09B21F}.$$method0x600045d-1.TryGetValue(propertyName, out num))
				{
					switch (num)
					{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
					case 6:
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00016E86 File Offset: 0x00015086
		public static string ErrorFormat(CssErrorCode errorCode)
		{
			return CssStrings.ResourceManager.GetString(errorCode.ToString(), CssStrings.Culture);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00016EBC File Offset: 0x000150BC
		private void ReportError(int severity, CssErrorCode errorNumber, CssContext context, params object[] arguments)
		{
			string message = CssParser.ErrorFormat(errorNumber).FormatInvariant(arguments);
			ContextError contextError = new ContextError();
			contextError.IsError = (severity < 2);
			contextError.Severity = severity;
			contextError.Subcategory = ContextError.GetSubcategory(severity);
			contextError.File = this.FileContext;
			contextError.ErrorNumber = (int)errorNumber;
			contextError.ErrorCode = "CSS{0}".FormatInvariant(new object[]
			{
				(int)(errorNumber & (CssErrorCode)65535)
			});
			contextError.StartLine = context.IfNotNull((CssContext c) => c.Start.Line);
			contextError.StartColumn = context.IfNotNull((CssContext c) => c.Start.Char);
			contextError.Message = message;
			ContextError cssError = contextError;
			this.OnCssError(cssError);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00016F9E File Offset: 0x0001519E
		private void ReportError(int severity, CssErrorCode errorNumber, params object[] arguments)
		{
			this.ReportError(severity, errorNumber, this.m_currentToken.IfNotNull((CssToken c) => c.Context), arguments);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000502 RID: 1282 RVA: 0x00016FD4 File Offset: 0x000151D4
		// (remove) Token: 0x06000503 RID: 1283 RVA: 0x0001700C File Offset: 0x0001520C
		public event EventHandler<ContextErrorEventArgs> CssError;

		// Token: 0x06000504 RID: 1284 RVA: 0x00017044 File Offset: 0x00015244
		protected void OnCssError(ContextError cssError)
		{
			if (this.CssError != null && cssError != null && !this.Settings.IgnoreAllErrors && !this.Settings.IgnoreErrorCollection.Contains(cssError.ErrorCode))
			{
				this.CssError(this, new ContextErrorEventArgs
				{
					Error = cssError
				});
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001709B File Offset: 0x0001529B
		private static string NormalizedValueReplacementComment(string source)
		{
			return CssParser.s_valueReplacement.Replace(source, "/*[${id}]*/");
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000170B0 File Offset: 0x000152B0
		private static bool CommentContainsText(string comment)
		{
			for (int i = 0; i < comment.Length; i++)
			{
				if (char.IsLetterOrDigit(comment[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000170E0 File Offset: 0x000152E0
		private string NormalizeImportantComment(string source)
		{
			if (CssParser.CommentContainsText(source))
			{
				if (source[3] == '/' && source.EndsWith("/**/", StringComparison.Ordinal))
				{
					source = "/*" + source.Substring(3);
				}
			}
			else
			{
				source = "/*" + source.Substring(3);
			}
			if (this.Settings.OutputMode == OutputMode.SingleLine)
			{
				source = source.Replace("\r\n", "\n");
			}
			return source;
		}

		// Token: 0x04000191 RID: 401
		private CssScanner m_scanner;

		// Token: 0x04000192 RID: 402
		private CssToken m_currentToken;

		// Token: 0x04000193 RID: 403
		private StringBuilder m_parsed;

		// Token: 0x04000194 RID: 404
		private bool m_noOutput;

		// Token: 0x04000195 RID: 405
		private string m_lastOutputString;

		// Token: 0x04000196 RID: 406
		private bool m_mightNeedSpace;

		// Token: 0x04000197 RID: 407
		private bool m_skippedSpace;

		// Token: 0x04000198 RID: 408
		private int m_lineLength;

		// Token: 0x04000199 RID: 409
		private bool m_noColorAbbreviation;

		// Token: 0x0400019A RID: 410
		private bool m_encounteredNewLine;

		// Token: 0x0400019B RID: 411
		private bool m_outputNewLine = true;

		// Token: 0x0400019C RID: 412
		private bool m_forceNewLine;

		// Token: 0x0400019D RID: 413
		private readonly HashSet<string> m_namespaces;

		// Token: 0x0400019E RID: 414
		private CodeSettings m_jsSettings;

		// Token: 0x0400019F RID: 415
		private static Regex s_vendorSpecific = new Regex("^(\\-(?<vendor>[^\\-]+)\\-)?(?<root>.+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x040001A0 RID: 416
		private static Regex s_regexHack1 = new Regex("/\\*([^*]|(\\*+[^*/]))*\\**\\\\\\*/(?<inner>.*?)/\\*([^*]|(\\*+[^*/]))*\\*+/", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x040001A1 RID: 417
		private static Regex s_regexHack2 = new Regex("/\\*/\\*//\\*/(?<inner>.*?)/\\*([^*]|(\\*+[^*/]))*\\*+/", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x040001A2 RID: 418
		private static Regex s_regexHack3 = new Regex("/\\*/\\*/(?<inner>.*?)/\\*([^*]|(\\*+[^*/]))*\\*+/", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x040001A3 RID: 419
		private static Regex s_regexHack4 = new Regex("(?<=\\w\\s+)/\\*([^*]|(\\*+[^*/]))*\\*+/\\s*(?=:)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x040001A4 RID: 420
		private static Regex s_regexHack5 = new Regex("(?<=[\\w/]\\s*:)\\s*/\\*([^*]|(\\*+[^*/]))*\\*+/", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x040001A5 RID: 421
		private static Regex s_regexHack6 = new Regex("(?<=\\w)/\\*([^*]|(\\*+[^*/]))*\\*+/\\s*(?=:)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x040001A6 RID: 422
		private static Regex s_regexHack7 = new Regex("/\\*(\\s?)\\*/", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x040001A7 RID: 423
		private static Regex s_rrggbb = new Regex("^\\#(?<r>[0-9a-fA-F])\\k<r>(?<g>[0-9a-fA-F])\\k<g>(?<b>[0-9a-fA-F])\\k<b>$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x040001A8 RID: 424
		private bool m_parsingColorValue;

		// Token: 0x040001A9 RID: 425
		private static Regex s_valueReplacement = new Regex("/\\*\\s*\\[(?<id>\\w+)\\]\\s*\\*/", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

		// Token: 0x040001AA RID: 426
		private string m_valueReplacement;

		// Token: 0x02000053 RID: 83
		private enum Parsed
		{
			// Token: 0x040001B4 RID: 436
			True,
			// Token: 0x040001B5 RID: 437
			False,
			// Token: 0x040001B6 RID: 438
			Empty
		}
	}
}

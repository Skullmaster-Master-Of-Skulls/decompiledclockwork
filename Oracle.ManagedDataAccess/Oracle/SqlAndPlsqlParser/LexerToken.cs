using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x02000263 RID: 611
	internal class LexerToken
	{
		// Token: 0x06001888 RID: 6280 RVA: 0x001033D8 File Offset: 0x001015D8
		public LexerToken(string refText, int begin, int end, Token t) : this(refText, begin, end)
		{
			this.m_vType = t;
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x001033EC File Offset: 0x001015EC
		public LexerToken(string refText, int begin, int end)
		{
			this.m_vReferrencedText = refText;
			this.m_vBegin = begin;
			this.m_vEnd = end;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00103418 File Offset: 0x00101618
		public void Print()
		{
			Console.WriteLine(this.ToString());
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00103428 File Offset: 0x00101628
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append('[');
			stringBuilder.Append(this.m_vBegin);
			stringBuilder.Append(',');
			stringBuilder.Append(this.m_vEnd);
			stringBuilder.Append(") ");
			stringBuilder.Append(this.m_vContent);
			stringBuilder.Append("   <");
			stringBuilder.Append(this.m_vType);
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x001034B4 File Offset: 0x001016B4
		public static void PrintTokens(List<LexerToken> src)
		{
			foreach (LexerToken lexerToken in src)
			{
				lexerToken.Print();
			}
			Console.WriteLine("------------------------------------------------------------------------");
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0010350C File Offset: 0x0010170C
		public static List<LexerToken> Tokenize(string sourceExpr, bool quotedStrings)
		{
			List<LexerToken> list = new List<LexerToken>();
			StringTokenizer stringTokenizer = new StringTokenizer(sourceExpr, LexerToken.c_vOperationAndWhitespaceTable, true);
			char c = ' ';
			bool flag = false;
			LexerToken lexerToken = null;
			bool flag2 = false;
			int num = 0;
			char c2 = ' ';
			bool flag3 = false;
			while (stringTokenizer.HasMoreTokens())
			{
				LexerToken lexerToken2 = stringTokenizer.NextToken();
				int num2 = lexerToken2.m_vEnd - lexerToken2.m_vBegin;
				char c3 = lexerToken2.m_vReferrencedText[lexerToken2.m_vBegin];
				int num3 = lexerToken2.m_vEnd;
				if (flag3)
				{
					if ('/' == c3 && num2 == 1 && lexerToken != null && '\n' == c2 && 1 == num)
					{
						list.Add(new LexerToken(LexerToken.c_vWrappedMarker, 0, LexerToken.c_vWrappedMarker.Length, Token.IDENTIFIER));
						flag3 = false;
					}
					else if ('\n' == c3 && num2 == 1)
					{
						lexerToken2.m_vType = Token.WS;
						list.Add(lexerToken2);
						c2 = '\n';
						num = 1;
					}
					else if ('\n' == c2 && 1 == num)
					{
						c2 = '?';
						num = 1;
					}
				}
				else
				{
					if (lexerToken != null)
					{
						int vBegin = lexerToken.m_vBegin;
						num = lexerToken.m_vEnd - vBegin;
						string vReferrencedText = lexerToken.m_vReferrencedText;
						c2 = vReferrencedText[lexerToken.m_vBegin];
						char c4 = vReferrencedText[lexerToken.m_vEnd - 1];
						switch (lexerToken.m_vType)
						{
						case Token.COMMENT:
							if (string.Compare("*/", 0, vReferrencedText, vBegin + num - 2, 2) != 0 || string.Compare("/*/", 0, vReferrencedText, vBegin, 3) == 0)
							{
								if ('*' == c3 || '/' == c3)
								{
									lexerToken.m_vEnd = num3;
									continue;
								}
								continue;
							}
							break;
						case Token.LINE_COMMENT:
							if ('\n' != c3)
							{
								lexerToken.m_vEnd = num3;
								continue;
							}
							break;
						case Token.QUOTED_STRING:
							if (flag)
							{
								if (c == ' ')
								{
									char c5 = c3;
									if (c5 <= '<')
									{
										if (c5 != '(')
										{
											if (c5 != '<')
											{
												goto IL_20C;
											}
											c = '>';
										}
										else
										{
											c = ')';
										}
									}
									else if (c5 != '[')
									{
										if (c5 != '{')
										{
											goto IL_20C;
										}
										c = '}';
									}
									else
									{
										c = ']';
									}
									IL_20F:
									lexerToken.m_vEnd = num3;
									continue;
									IL_20C:
									c = c3;
									goto IL_20F;
								}
								lexerToken.m_vEnd = num3;
								if (c3 == '\'' && c == c4 && num > 3)
								{
									flag = false;
									c = ' ';
									string text = "'" + lexerToken.m_vReferrencedText.Substring(lexerToken.m_vBegin + 3, lexerToken.m_vEnd - lexerToken.m_vBegin - 5) + "'";
									lexerToken = new LexerToken(text, 0, text.Length, lexerToken.m_vType);
									continue;
								}
								continue;
							}
							else if (c3 == '\'')
							{
								LexerToken lexerToken3 = stringTokenizer.PeekNextToken();
								if (lexerToken3 != null && lexerToken3.m_vReferrencedText[lexerToken3.m_vBegin] == '\'')
								{
									stringTokenizer.NextToken();
									num3++;
									flag2 = true;
									continue;
								}
								lexerToken.m_vEnd = num3;
								if (flag2)
								{
									string text2 = "'" + lexerToken.m_vReferrencedText.Substring(lexerToken.m_vBegin + 1, lexerToken.m_vEnd - lexerToken.m_vBegin - 2).Replace("''", "'") + "'";
									lexerToken = new LexerToken(text2, 0, text2.Length, lexerToken.m_vType);
									flag2 = false;
									continue;
								}
								continue;
							}
							else
							{
								if (num == 1)
								{
									continue;
								}
								if (c4 != '\'')
								{
									continue;
								}
							}
							break;
						case Token.DQUOTED_STRING:
							if (c3 == '"')
							{
								lexerToken.m_vEnd = num3;
								continue;
							}
							if (num == 1)
							{
								continue;
							}
							if (c4 != '"')
							{
								continue;
							}
							break;
						default:
							if (num2 == 1)
							{
								if ('*' == c3 && '/' == c2 && num == 1)
								{
									lexerToken.m_vEnd = num3;
									lexerToken.m_vType = Token.COMMENT;
									continue;
								}
								if ('-' == c3 && '-' == c2 && num == 1)
								{
									lexerToken.m_vEnd = num3;
									lexerToken.m_vType = Token.LINE_COMMENT;
									continue;
								}
							}
							if (string.Compare("rem", 0, lexerToken2.m_vReferrencedText, lexerToken2.m_vBegin, 3, true) == 0 && num == 1 && ('\n' == c2 || '\r' == c2))
							{
								lexerToken = new LexerToken(lexerToken2.m_vReferrencedText, num3 - 1, num3 - 1, Token.LINE_COMMENT);
								list.Add(lexerToken);
								continue;
							}
							break;
						}
					}
					else if (string.Compare("rem", 0, lexerToken2.m_vReferrencedText, lexerToken2.m_vBegin, 3, true) == 0)
					{
						lexerToken = new LexerToken(lexerToken2.m_vReferrencedText, num3 - 1, num3 - 1, Token.LINE_COMMENT);
						list.Add(lexerToken);
						continue;
					}
					if (num2 == 1)
					{
						if (quotedStrings)
						{
							char c6 = c3;
							if (c6 == '"')
							{
								lexerToken = new LexerToken(lexerToken2.m_vReferrencedText, num3 - 1, num3 - 1, Token.DQUOTED_STRING);
								list.Add(lexerToken);
								continue;
							}
							if (c6 == '\'')
							{
								if (lexerToken != null && num == 1)
								{
									if ('q' == c2 || 'Q' == c2)
									{
										flag = true;
										lexerToken.m_vType = Token.QUOTED_STRING;
									}
									else if ('n' == c2 || 'N' == c2)
									{
										lexerToken.m_vType = Token.QUOTED_STRING;
									}
								}
								lexerToken = new LexerToken(lexerToken2.m_vReferrencedText, num3 - 1, num3, Token.QUOTED_STRING);
								list.Add(lexerToken);
								continue;
							}
						}
						if (LexerToken.c_vOperationTable.Contains(c3))
						{
							lexerToken2.m_vType = Token.OPERATION;
							lexerToken = lexerToken2;
							list.Add(lexerToken);
							continue;
						}
						if (LexerToken.c_vWhitespaceTable.Contains(c3))
						{
							lexerToken2.m_vType = Token.WS;
							lexerToken = lexerToken2;
							list.Add(lexerToken);
							continue;
						}
					}
					if (char.IsDigit(c3))
					{
						int num4 = lexerToken2.m_vReferrencedText.IndexOfAny(LexerToken.c_vExp, lexerToken2.m_vBegin, num2);
						if (num4 == -1)
						{
							lexerToken2.m_vType = Token.DIGITS;
							lexerToken = lexerToken2;
						}
						else
						{
							list.Add(new LexerToken(lexerToken2.m_vReferrencedText, lexerToken2.m_vBegin, num4, Token.DIGITS));
							lexerToken = new LexerToken(lexerToken2.m_vReferrencedText, num4, num4 + 1, Token.IDENTIFIER);
							if (num4 != lexerToken2.m_vEnd - 1)
							{
								list.Add(lexerToken);
								lexerToken = new LexerToken(lexerToken2.m_vReferrencedText, num4 + 1, lexerToken2.m_vEnd, Token.DIGITS);
							}
						}
						list.Add(lexerToken);
					}
					else
					{
						if (string.Compare("wrapped", 0, lexerToken2.m_vReferrencedText, lexerToken2.m_vBegin, 7, true) == 0 && lexerToken != null)
						{
							bool flag4 = false;
							for (int i = list.Count - 1; i >= 0; i--)
							{
								LexerToken lexerToken4 = list[i];
								string text3 = lexerToken4.m_vReferrencedText.Substring(lexerToken4.m_vBegin, lexerToken4.m_vEnd - lexerToken4.m_vBegin).ToUpper();
								if (flag4)
								{
									char c7 = text3[0];
									if (c7 <= 'F')
									{
										if (c7 != 'B')
										{
											if (c7 == 'F')
											{
												if ("FUNCTION" == text3)
												{
													flag3 = true;
												}
											}
										}
										else if ("BODY" == text3)
										{
											flag3 = true;
										}
									}
									else if (c7 != 'P')
									{
										if (c7 == 'T')
										{
											if ("TRIGGER" == text3 || "TYPE" == text3)
											{
												flag3 = true;
											}
										}
									}
									else if ("PROCEDURE" == text3 || "PACKAGE" == text3)
									{
										flag3 = true;
									}
									if (flag3)
									{
										break;
									}
								}
								if (lexerToken4.m_vType != Token.WS && lexerToken4.m_vType != Token.COMMENT)
								{
									if (lexerToken4.m_vType != Token.IDENTIFIER)
									{
										break;
									}
									flag4 = true;
								}
							}
						}
						lexerToken2.m_vType = Token.IDENTIFIER;
						lexerToken = lexerToken2;
						list.Add(lexerToken);
					}
				}
			}
			return list;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00103C68 File Offset: 0x00101E68
		public static List<LexerToken> Parse(string input)
		{
			return LexerToken.Parse(input, false, true);
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00103C74 File Offset: 0x00101E74
		public static List<LexerToken> Parse(string input, bool keepWsAndComments, bool quotedStrings)
		{
			List<LexerToken> list = new List<LexerToken>();
			LexerToken lexerToken = null;
			foreach (LexerToken lexerToken2 in LexerToken.Tokenize(input, quotedStrings))
			{
				switch (lexerToken2.m_vType)
				{
				case Token.COMMENT:
				case Token.LINE_COMMENT:
				case Token.WS:
					if (!keepWsAndComments)
					{
						continue;
					}
					break;
				case Token.QUOTED_STRING:
					if (lexerToken != null && lexerToken.m_vType == Token.QUOTED_STRING)
					{
						if (lexerToken.m_vContent == null)
						{
							lexerToken.m_vContent = lexerToken.m_vReferrencedText.Substring(lexerToken.m_vBegin, lexerToken.m_vEnd - lexerToken.m_vBegin);
						}
						if (lexerToken2.m_vContent == null)
						{
							lexerToken2.m_vContent = lexerToken2.m_vReferrencedText.Substring(lexerToken2.m_vBegin, lexerToken2.m_vEnd - lexerToken2.m_vBegin);
						}
						lexerToken.m_vContent = (lexerToken.m_vReferrencedText = lexerToken.m_vContent + lexerToken2.m_vContent);
						lexerToken.m_vBegin = 0;
						lexerToken.m_vEnd = lexerToken.m_vReferrencedText.Length;
						continue;
					}
					break;
				}
				list.Add(lexerToken2);
				lexerToken2.m_vContent = lexerToken2.m_vReferrencedText.Substring(lexerToken2.m_vBegin, lexerToken2.m_vEnd - lexerToken2.m_vBegin);
				lexerToken = lexerToken2;
			}
			return list;
		}

		// Token: 0x04001AE9 RID: 6889
		internal const char c_vNewLine = '\n';

		// Token: 0x04001AEA RID: 6890
		internal const char c_vCarriageReturn = '\r';

		// Token: 0x04001AEB RID: 6891
		internal static string c_vOperation = "(){}[]^-|!*+./><='\",;:%@?";

		// Token: 0x04001AEC RID: 6892
		internal static string c_vWhiteSpace = " \n\r\t";

		// Token: 0x04001AED RID: 6893
		internal static string c_vOperationAndWhiteSpace = LexerToken.c_vOperation + LexerToken.c_vWhiteSpace;

		// Token: 0x04001AEE RID: 6894
		internal static string c_vWrappedMarker = "\"/\"";

		// Token: 0x04001AEF RID: 6895
		internal static char[] c_vExp = "eE".ToCharArray();

		// Token: 0x04001AF0 RID: 6896
		internal static DoubleStageCharPropertiesTable c_vOperationTable = new DoubleStageCharPropertiesTable(LexerToken.c_vOperation);

		// Token: 0x04001AF1 RID: 6897
		internal static DoubleStageCharPropertiesTable c_vWhitespaceTable = new DoubleStageCharPropertiesTable(LexerToken.c_vWhiteSpace);

		// Token: 0x04001AF2 RID: 6898
		internal static DoubleStageCharPropertiesTable c_vOperationAndWhitespaceTable = new DoubleStageCharPropertiesTable(LexerToken.c_vOperationAndWhiteSpace);

		// Token: 0x04001AF3 RID: 6899
		public string m_vContent;

		// Token: 0x04001AF4 RID: 6900
		public string m_vReferrencedText;

		// Token: 0x04001AF5 RID: 6901
		public int m_vBegin = -1;

		// Token: 0x04001AF6 RID: 6902
		public int m_vEnd = -1;

		// Token: 0x04001AF7 RID: 6903
		public Token m_vType;
	}
}

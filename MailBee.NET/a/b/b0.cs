using System;
using System.Collections.Specialized;
using System.Text;

namespace a.b
{
	// Token: 0x02000331 RID: 817
	internal class b0
	{
		// Token: 0x06001D95 RID: 7573 RVA: 0x0007F79C File Offset: 0x0007E79C
		private void a()
		{
			if (b0.a.Count > 0)
			{
				return;
			}
			b0.a.Add("!DOCTYPE", "243");
			b0.a.Add("HTML", "19");
			b0.a.Add("/HTML", "27");
			b0.a.Add("HEAD", "34");
			b0.a.Add("/HEAD", "41");
			b0.a.Add("TITLE", "177");
			b0.a.Add("/TITLE", "185");
			b0.a.Add("META", "161");
			b0.a.Add("STYLE", "241");
			b0.a.Add("/STYLE", "249");
			b0.a.Add("BODY", "50");
			b0.a.Add("/BODY", "58");
			b0.a.Add("DIV", "96");
			b0.a.Add("/DIV", "104");
			b0.a.Add("SPAN", "84");
			b0.a.Add("/SPAN", "92");
			b0.a.Add("FONT", "148");
			b0.a.Add("/FONT", "156");
			b0.a.Add("P", "64");
			b0.a.Add("/P", "72");
			b0.a.Add("B", "84");
			b0.a.Add("/B", "92");
			b0.a.Add("I", "84");
			b0.a.Add("/I", "92");
			b0.a.Add("U", "84");
			b0.a.Add("/U", "92");
			b0.a.Add("STRONG", "84");
			b0.a.Add("/STRONG", "92");
			b0.a.Add("EM", "84");
			b0.a.Add("/EM", "92");
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x0007FA0F File Offset: 0x0007EA0F
		public b0()
		{
			this.a();
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x0007FA20 File Offset: 0x0007EA20
		public string b(string A_0)
		{
			this.a();
			StringCollection stringCollection = new StringCollection();
			int num = 0;
			int num2 = 4;
			char c = ' ';
			for (int i = 0; i < A_0.Length; i++)
			{
				if (num2 == 1 || num2 == 3)
				{
					num2 = 4;
				}
				if (num2 == 4)
				{
					if (A_0[i] == '<')
					{
						num2 = 0;
					}
					else
					{
						num2 = 2;
					}
					num = i;
				}
				if (num2 == 0)
				{
					if (i > 2 && A_0.Length >= num + 4 && A_0.Substring(num, 4) == "<!--")
					{
						if (A_0.Substring(i - 2, 3) == "-->" || i == A_0.Length - 1)
						{
							num2 = 1;
						}
					}
					else if (A_0[i] == '\'' || A_0[i] == '"')
					{
						num2 = 5;
						c = A_0[i];
					}
					else if (A_0[i] == '>' || i == A_0.Length - 1)
					{
						num2 = 1;
					}
				}
				else if (num2 == 5 && (A_0[i] == '>' || i == A_0.Length - 1))
				{
					num2 = 0;
					c = ' ';
				}
				else if (num2 == 5 && c == A_0[i])
				{
					num2 = 0;
					c = ' ';
				}
				else if (num2 == 2 && ((i < A_0.Length - 2 && A_0[i + 1] == '<' && A_0[i + 2] != ' ') || i == A_0.Length - 1))
				{
					num2 = 3;
				}
				if (num2 == 1 || num2 == 3 || i == A_0.Length - 1)
				{
					int length = i - num + 1;
					if (num2 == 3)
					{
						stringCollection.Add(A_0.Substring(num, length).Replace("\\", "\\\\"));
					}
					else
					{
						stringCollection.Add(A_0.Substring(num, length));
					}
				}
			}
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\\rtf1\\fromhtml1\r\n\r\n");
			int num3 = 0;
			for (int j = 0; j < stringCollection.Count; j++)
			{
				while (stringCollection[j].StartsWith("\r\n"))
				{
					stringCollection[j] = stringCollection[j].Substring(2);
					stringBuilder.Append("{\\*\\htmltag1 \\par }\r\n\r\n");
				}
				if (!(stringCollection[j] == string.Empty))
				{
					if (stringCollection[j][0] == '<')
					{
						stringCollection[j] = stringCollection[j].Replace("\r\n", "\\par ");
						string text = stringCollection[j].Split(new char[]
						{
							' ',
							'>'
						}, 2)[0].ToUpper().Substring(1);
						num3 = j;
						string text2 = b0.a[text];
						if (text2 != null)
						{
							num3 = int.Parse(text2);
						}
						else if (text != string.Empty)
						{
							num3 = ((text[0] == '/') ? 248 : 240);
						}
						if (text == "STYLE")
						{
							flag = true;
						}
						else if (text == "/STYLE")
						{
							flag = false;
						}
						string a_ = flag ? stringCollection[j].Replace("{", "\\{").Replace("}", "\\}") : stringCollection[j];
						stringBuilder.Append(string.Concat(new object[]
						{
							"{\\*\\htmltag",
							num3,
							" ",
							this.a(a_),
							"}\r\n"
						}));
					}
					else if (!flag)
					{
						stringBuilder.Append(this.a(stringCollection[j]).Replace("\r\n", ""));
					}
					else
					{
						string a_2 = stringCollection[j].Replace("{", "\\{").Replace("}", "\\}");
						stringBuilder.Append(string.Concat(new object[]
						{
							"{\\*\\htmltag",
							num3,
							" ",
							this.a(a_2),
							"}\r\n"
						}));
					}
				}
			}
			stringBuilder.Append("\r\n\r\n}");
			return stringBuilder.ToString();
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x0007FE54 File Offset: 0x0007EE54
		private string a(string A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < A_0.Length; i++)
			{
				if ((short)A_0[i] > 127 || (short)A_0[i] < 0)
				{
					stringBuilder.Append("\\u" + ((short)A_0[i]).ToString() + "\\'f3");
				}
				else
				{
					stringBuilder.Append(A_0[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x0007FECC File Offset: 0x0007EECC
		public string c(string A_0)
		{
			A_0 = A_0.Replace("</p>", "<br>");
			this.a();
			StringCollection stringCollection = new StringCollection();
			int num = 0;
			int num2 = 4;
			char c = ' ';
			for (int i = 0; i < A_0.Length; i++)
			{
				if (num2 == 1 || num2 == 3)
				{
					num2 = 4;
				}
				if (num2 == 4)
				{
					if (A_0[i] == '<')
					{
						num2 = 0;
					}
					else
					{
						num2 = 2;
					}
					num = i;
				}
				if (num2 == 0)
				{
					if (i > 2 && A_0.Length >= num + 4 && A_0.Substring(num, 4) == "<!--")
					{
						if (A_0.Substring(i - 2, 3) == "-->" || i == A_0.Length - 1)
						{
							num2 = 1;
						}
					}
					else if (A_0[i] == '\'' || A_0[i] == '"')
					{
						num2 = 5;
						c = A_0[i];
					}
					else if (A_0[i] == '>' || i == A_0.Length - 1)
					{
						num2 = 1;
					}
				}
				else if (num2 == 5 && (A_0[i] == '>' || i == A_0.Length - 1))
				{
					num2 = 0;
					c = ' ';
				}
				else if (num2 == 5 && c == A_0[i])
				{
					num2 = 0;
					c = ' ';
				}
				else if (num2 == 2 && ((i < A_0.Length - 1 && A_0[i + 1] == '<') || i == A_0.Length - 1))
				{
					num2 = 3;
				}
				if (num2 == 1 || num2 == 3 || i == A_0.Length - 1)
				{
					int length = i - num + 1;
					stringCollection.Add(A_0.Substring(num, length));
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\\rtf1\\fromhtml1\r\n\r\n");
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int j = 0; j < stringCollection.Count; j++)
			{
				if (stringCollection[j][0] == '<')
				{
					if (stringCollection[j].IndexOf("<p") <= -1)
					{
						stringCollection[j] = stringCollection[j].Replace("\r\n", "\\par ");
						string text = stringCollection[j].Split(new char[]
						{
							' ',
							'>'
						}, 2)[0].ToUpper().Substring(1);
						string text2 = b0.a[text];
						int num3;
						if (text2 != null)
						{
							num3 = int.Parse(text2);
						}
						else
						{
							num3 = ((text[0] == '/') ? 248 : 240);
						}
						if (j > 0 && !flag)
						{
							stringBuilder.Append("\r\n\r\n");
						}
						if (stringCollection[j].Trim().ToUpper().StartsWith("<P") || stringCollection[j].Trim().ToUpper().StartsWith("<TR"))
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								"{\\*\\htmltag",
								num3,
								" ",
								stringCollection[j],
								"} \\line"
							}));
							flag = false;
						}
						else if (stringCollection[j].Trim().ToUpper().StartsWith("<HR"))
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								"{\\*\\htmltag",
								num3,
								" ",
								stringCollection[j],
								"}"
							}));
							flag = false;
						}
						else if (stringCollection[j].Trim().ToUpper().StartsWith("<B>") || stringCollection[j].Trim().ToUpper().StartsWith("<STRONG>"))
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								"{\\*\\htmltag",
								num3,
								" ",
								stringCollection[j],
								"} \\b"
							}));
							flag = false;
						}
						else if (stringCollection[j].Trim().ToUpper().StartsWith("</B>") || stringCollection[j].Trim().ToUpper().StartsWith("</STRONG>"))
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								"{\\*\\htmltag",
								num3,
								" ",
								stringCollection[j],
								"} \\b0"
							}));
							flag = false;
						}
						else if (stringCollection[j].Trim().ToUpper().StartsWith("<I>") || stringCollection[j].Trim().ToUpper().StartsWith("<EM>"))
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								"{\\*\\htmltag",
								num3,
								" ",
								stringCollection[j],
								"} \\i"
							}));
							flag = false;
						}
						else if (stringCollection[j].Trim().ToUpper().StartsWith("</I>") || stringCollection[j].Trim().ToUpper().StartsWith("</EM>"))
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								"{\\*\\htmltag",
								num3,
								" ",
								stringCollection[j],
								"} \\i0"
							}));
							flag = false;
						}
						else if (stringCollection[j].Trim().ToUpper().StartsWith("<U>"))
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								"{\\*\\htmltag",
								num3,
								" ",
								stringCollection[j],
								"} \\ul"
							}));
							flag = false;
						}
						else if (stringCollection[j].Trim().ToUpper().StartsWith("</U>"))
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								"{\\*\\htmltag",
								num3,
								" ",
								stringCollection[j],
								"} \\ul0"
							}));
							flag = false;
						}
						else if (stringCollection[j].Trim().ToUpper().StartsWith("<A HREF"))
						{
							int num4 = stringCollection[j].IndexOf('"');
							int num5 = -1;
							if (num4 > 0)
							{
								num5 = stringCollection[j].LastIndexOf('"');
							}
							else
							{
								num4 = stringCollection[j].IndexOf('\'');
								if (num4 > 0)
								{
									num5 = stringCollection[j].LastIndexOf('\'');
								}
								else
								{
									num4 = stringCollection[j].IndexOf('=');
									if (num4 > 0)
									{
										num5 = stringCollection[j].LastIndexOf('>');
									}
								}
							}
							if (num4 == num5 && num4 != -1)
							{
								num5 = stringCollection[j].IndexOf('>');
							}
							string text3 = string.Empty;
							if (num4 != -1 && num5 != -1)
							{
								text3 = stringCollection[j].Substring(num4 + 1, num5 - num4 - 1);
							}
							stringBuilder.Append(string.Concat(new object[]
							{
								"{\\*\\htmltag",
								j,
								" ",
								stringCollection[j],
								"} {\\field{\\*\\fldinst{HYPERLINK \"",
								text3,
								"\"}}{\\fldrslt\\ul "
							}));
							flag = true;
							flag3 = true;
						}
						else if (stringCollection[j].Trim().ToUpper().StartsWith("</A>"))
						{
							if (flag3)
							{
								stringBuilder.Append("}}\r\n");
							}
							stringBuilder.Append(string.Concat(new object[]
							{
								"{\\*\\htmltag",
								num3,
								" ",
								stringCollection[j],
								"}"
							}));
							flag = false;
							flag3 = false;
						}
						else if (stringCollection[j].Trim().ToUpper().StartsWith("<STYLE"))
						{
							flag2 = true;
						}
						else if (stringCollection[j].Trim().ToUpper().StartsWith("</STYLE"))
						{
							flag2 = false;
						}
						else
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								"{\\*\\htmltag",
								num3,
								" ",
								stringCollection[j],
								"}"
							}));
						}
					}
				}
				else if (!(stringCollection[j].Trim() == string.Empty) && !flag2)
				{
					stringBuilder.Append("\r\n ");
					for (int k = 0; k < stringCollection[j].Length; k++)
					{
						if ((short)stringCollection[j][k] > 127 || (short)stringCollection[j][k] < 0)
						{
							stringBuilder.Append("\\u" + ((short)stringCollection[j][k]).ToString() + "\\'f3");
						}
						else
						{
							stringBuilder.Append(stringCollection[j][k]);
						}
					}
					flag = false;
				}
			}
			stringBuilder.Append("\r\n\r\n}");
			return stringBuilder.ToString();
		}

		// Token: 0x0400138A RID: 5002
		private static StringDictionary a = new StringDictionary();

		// Token: 0x02000332 RID: 818
		private enum a
		{
			// Token: 0x0400138C RID: 5004
			a,
			// Token: 0x0400138D RID: 5005
			b,
			// Token: 0x0400138E RID: 5006
			c,
			// Token: 0x0400138F RID: 5007
			d,
			// Token: 0x04001390 RID: 5008
			e,
			// Token: 0x04001391 RID: 5009
			f
		}
	}
}

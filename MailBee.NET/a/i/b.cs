using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MailBee;
using MailBee.Mime;

namespace a.i
{
	// Token: 0x020001EB RID: 491
	internal class b
	{
		// Token: 0x06000FBE RID: 4030 RVA: 0x0003D3CE File Offset: 0x0003C3CE
		private b()
		{
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0003D3D8 File Offset: 0x0003C3D8
		public static string a(string A_0, bool A_1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			int num = 0;
			int num2 = 0;
			bool flag8 = false;
			bool flag9 = false;
			int num3 = 0;
			bool flag10 = false;
			char value = ' ';
			int num4 = 0;
			string text = A_0.ToLower();
			int num5 = text.IndexOf("<head");
			if (num5 > -1)
			{
				int num6 = num5 + "<head".Length;
				if (text.Length > num6 && (text[num6] == '>' || char.IsWhiteSpace(text[num6])))
				{
					int num7 = num5;
					num5 = text.IndexOf("</head", num5 + 5);
					if (num5 > -1)
					{
						num6 = num5 + "</head".Length;
						if (text.Length > num6 && (text[num6] == '>' || char.IsWhiteSpace(text[num6])))
						{
							num5 = text.IndexOf('>', num6);
							if (num5 <= -1)
							{
								return string.Empty;
							}
							num5++;
						}
						else
						{
							num5 = num7;
						}
					}
				}
				else
				{
					num5 = 0;
				}
			}
			if (num5 < 0)
			{
				num5 = 0;
			}
			text = A_0.Substring(num5);
			int length = text.Length;
			int i = 0;
			int num8 = 0;
			while (i <= length)
			{
				if (i < length)
				{
					if (flag)
					{
						if (flag5)
						{
							if (text[i] != '>' || text[i - 1] != '-' || text[i - 2] != '-')
							{
								i++;
								continue;
							}
							flag5 = false;
						}
						if (flag4 && i < length - 1 && text[i] == '-' && text[i + 1] == '-')
						{
							flag4 = false;
							flag5 = true;
							i += 2;
							continue;
						}
						if (flag6 && text[i] == '"')
						{
							flag6 = false;
						}
						else if (flag7 && text[i] == '\'')
						{
							flag7 = false;
						}
						else
						{
							char c = text[i];
							if (c != '"')
							{
								if (c != '\'')
								{
									if (c == '>')
									{
										flag = false;
										int num9 = i + 1;
										bool flag11 = true;
										int num10 = num2 - 1;
										while (++num10 < num9 && global::a.i.b.a(text[num10]))
										{
										}
										int num11 = num10 - num2;
										if (A_1 && ((num11 == 1 && string.Compare(text, num2, "a", 0, 1, StringComparison.OrdinalIgnoreCase) == 0) || (num11 == 3 && string.Compare(text, num2, "img", 0, 3, StringComparison.OrdinalIgnoreCase) == 0)))
										{
											stringBuilder.Append(text.Substring(num8, num9 - num8));
											flag11 = false;
										}
										if (flag11)
										{
											stringBuilder.Append(text.Substring(num8, num - num8));
										}
										num8 = num9;
										if (flag3 && num11 == 2 && string.Compare(text, num2, "br", 0, 2, StringComparison.OrdinalIgnoreCase) == 0)
										{
											if (flag9)
											{
												flag9 = false;
												stringBuilder.Remove(stringBuilder.Length - 1, 1);
											}
											stringBuilder.Append("\r\n");
											if (num3 > 0)
											{
												num3--;
											}
											flag10 = false;
											flag8 = false;
										}
										else if ((num11 == 1 && string.Compare(text, num2, "p", 0, 1, StringComparison.OrdinalIgnoreCase) == 0) || (num11 == 5 && string.Compare(text, num2, "table", 0, 5, StringComparison.OrdinalIgnoreCase) == 0) || (num11 == 2 && string.Compare(text, num2, "tr", 0, 2, StringComparison.OrdinalIgnoreCase) == 0) || (num11 == 3 && string.Compare(text, num2, "div", 0, 3, StringComparison.OrdinalIgnoreCase) == 0) || (num11 == 2 && string.Compare(text, num2, "li", 0, 2, StringComparison.OrdinalIgnoreCase) == 0) || (num11 == 2 && string.Compare(text, num2, "ul", 0, 2, StringComparison.OrdinalIgnoreCase) == 0) || (num11 == 2 && string.Compare(text, num2, "ol", 0, 2, StringComparison.OrdinalIgnoreCase) == 0))
										{
											if (flag9)
											{
												flag9 = false;
												stringBuilder.Remove(stringBuilder.Length - 1, 1);
											}
											if (num11 != 1 || string.Compare(text, num2, "p", 0, 1, StringComparison.OrdinalIgnoreCase) != 0)
											{
												num3 /= 2;
												flag10 = true;
											}
											else
											{
												if (flag10)
												{
													num3++;
												}
												flag10 = false;
											}
											for (int j = 0; j < num3; j++)
											{
												stringBuilder.Append(" \r\n ");
											}
											num3 = 0;
											flag8 = false;
										}
										else
										{
											flag10 = false;
										}
									}
								}
								else
								{
									flag7 = true;
								}
							}
							else
							{
								flag6 = true;
							}
						}
						if (!flag)
						{
							i++;
							continue;
						}
					}
					else if (flag2)
					{
						if (global::a.i.b.a(text[i]) || text[i] == '!' || text[i] == '?' || text[i] == '/')
						{
							flag3 = (text[i] != '/');
							flag4 = (text[i] == '!');
							flag = true;
							num = i - 1;
							num2 = i + (flag3 ? 0 : 1);
						}
						else
						{
							flag9 = false;
							flag8 = true;
							num3 = 2;
							flag10 = false;
						}
						flag2 = false;
					}
					else if (text[i] == '<')
					{
						flag2 = true;
					}
				}
				if (!flag && !flag2 && i < length)
				{
					bool flag13;
					if (text[i] == '&')
					{
						if (!A_1 && length - i > 2)
						{
							if (text[i + 1] == '#')
							{
								num4 = i + 1;
								bool flag12 = false;
								int num12 = 2;
								if (num4 + 1 < length && char.ToLower(text[num4 + 1]) == 'x')
								{
									flag12 = true;
									num4++;
									num12 = 3;
								}
								while (num4 < text.Length - 1 && ((!flag12 && global::a.i.b.b(text[++num4])) || (flag12 && global::a.i.b.a(text[++num4]))))
								{
								}
								int num13 = 32;
								try
								{
									if (text.Length > i + num12 && num4 - (i + num12) > 0)
									{
										num13 = Convert.ToInt32(text.Substring(i + num12, num4 - (i + num12)), flag12 ? 16 : 10);
									}
									if (num13 < 0 || num13 > 65535)
									{
										num13 = 32;
									}
								}
								catch (FormatException)
								{
								}
								value = Convert.ToChar(num13);
							}
							else if (string.Compare(text, i + 1, "quot", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = '"';
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "amp", 0, 3, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = '&';
								num4 = i + 4;
							}
							else if (string.Compare(text, i + 1, "lt", 0, 2, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = '<';
								num4 = i + 3;
							}
							else if (string.Compare(text, i + 1, "gt", 0, 2, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = '>';
								num4 = i + 3;
							}
							else if (string.Compare(text, i + 1, "nbsp", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ' ';
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "iexcl", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(161);
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "cent", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(162);
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "pound", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(163);
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "curren", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(164);
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "yen", 0, 3, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(165);
								num4 = i + 4;
							}
							else if (string.Compare(text, i + 1, "brvbar", 0, 6, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(text, i + 1, "brkbar", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(166);
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "sect", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = '§';
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "uml", 0, 3, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(text, i + 1, "dir", 0, 3, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(168);
								num4 = i + 4;
							}
							else if (string.Compare(text, i + 1, "copy", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(169);
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "ordf", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(170);
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "laquo", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = '«';
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "not", 0, 3, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(172);
								num4 = i + 4;
							}
							else if (string.Compare(text, i + 1, "shy", 0, 3, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = '-';
								num4 = i + 4;
							}
							else if (string.Compare(text, i + 1, "reg", 0, 3, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = '®';
								num4 = i + 4;
							}
							else if (string.Compare(text, i + 1, "macr", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(175);
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "hibar", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(175);
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "deg", 0, 3, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(176);
								num4 = i + 4;
							}
							else if (string.Compare(text, i + 1, "plusmn", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(177);
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "sup2", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(178);
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "sup3", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(179);
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "acute", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(180);
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "micro", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(181);
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "para", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(182);
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "middot", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(183);
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "cedil", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(184);
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "sup1", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(185);
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "ordm", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(186);
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "raquo", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(187);
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "frac14", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(188);
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "frac12", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(189);
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "frac34", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(190);
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "iquest", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(191);
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "times", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(215);
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "szlig", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(223);
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "agrave", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'a') ? Convert.ToChar(224) : Convert.ToChar(192));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "aacute", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'a') ? Convert.ToChar(225) : Convert.ToChar(193));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "acirc", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'a') ? Convert.ToChar(226) : Convert.ToChar(194));
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "atilde", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'a') ? Convert.ToChar(227) : Convert.ToChar(195));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "auml", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'a') ? Convert.ToChar(228) : Convert.ToChar(196));
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "aring", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'a') ? Convert.ToChar(229) : Convert.ToChar(197));
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "aelig", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'a') ? Convert.ToChar(230) : Convert.ToChar(198));
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "ccedil", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'c') ? Convert.ToChar(231) : Convert.ToChar(199));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "egrave", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'e') ? Convert.ToChar(232) : Convert.ToChar(200));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "eacute", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'e') ? Convert.ToChar(233) : Convert.ToChar(201));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "ecirc", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'e') ? Convert.ToChar(234) : Convert.ToChar(202));
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "euml", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'e') ? Convert.ToChar(235) : Convert.ToChar(203));
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "igrave", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'i') ? Convert.ToChar(236) : Convert.ToChar(204));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "iacute", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'i') ? Convert.ToChar(237) : Convert.ToChar(205));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "icirc", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'i') ? Convert.ToChar(238) : Convert.ToChar(206));
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "iuml", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'i') ? Convert.ToChar(239) : Convert.ToChar(207));
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "eth", 0, 3, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'e') ? Convert.ToChar(240) : Convert.ToChar(208));
								num4 = i + 4;
							}
							else if (string.Compare(text, i + 1, "ntilde", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'n') ? Convert.ToChar(241) : Convert.ToChar(209));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "ograve", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'o') ? Convert.ToChar(242) : Convert.ToChar(210));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "oacute", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'o') ? Convert.ToChar(243) : Convert.ToChar(211));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "ocirc", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'o') ? Convert.ToChar(244) : Convert.ToChar(212));
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "otilde", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'o') ? Convert.ToChar(245) : Convert.ToChar(213));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "ouml", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'o') ? Convert.ToChar(246) : Convert.ToChar(214));
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "divide", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(247);
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "oslash", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'o') ? Convert.ToChar(248) : Convert.ToChar(216));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "ugrave", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'u') ? Convert.ToChar(249) : Convert.ToChar(217));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "uacute", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'u') ? Convert.ToChar(250) : Convert.ToChar(218));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "ucirc", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'u') ? Convert.ToChar(251) : Convert.ToChar(219));
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "uuml", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'u') ? Convert.ToChar(252) : Convert.ToChar(220));
								num4 = i + 5;
							}
							else if (string.Compare(text, i + 1, "yacute", 0, 6, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 'y') ? Convert.ToChar(253) : Convert.ToChar(221));
								num4 = i + 7;
							}
							else if (string.Compare(text, i + 1, "thorn", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = ((text[i + 1] == 't') ? Convert.ToChar(254) : Convert.ToChar(222));
								num4 = i + 6;
							}
							else if (string.Compare(text, i + 1, "yuml", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
							{
								value = Convert.ToChar(255);
								num4 = i + 5;
							}
							if (num4 < length && text[num4] == ';')
							{
								num4++;
							}
						}
						flag13 = true;
						flag9 = false;
						flag8 = true;
						num3 = 2;
						flag10 = false;
					}
					else
					{
						num4 = i - 1;
						while (++num4 < length && global::a.i.b.c(text[num4]))
						{
						}
						if (num4 > i && flag8)
						{
							flag9 = true;
						}
						value = ' ';
						flag13 = false;
					}
					if (num4 > i)
					{
						stringBuilder.Append(text.Substring(num8, i - num8));
						num8 = num4;
						i = num4 - 1;
						if (flag13 || flag9)
						{
							stringBuilder.Append(value);
						}
					}
					else
					{
						flag9 = false;
						flag8 = true;
						num3 = 2;
						flag10 = false;
					}
				}
				i++;
			}
			i--;
			stringBuilder.Append(text.Substring(num8, i - num8));
			return stringBuilder.ToString();
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003E9DC File Offset: 0x0003D9DC
		public static bool c(char A_0)
		{
			return A_0 == ' ' || A_0 == '\t' || A_0 == '\f';
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0003E9EF File Offset: 0x0003D9EF
		public static bool b(char A_0)
		{
			return A_0 >= '0' && A_0 <= '9';
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0003EA00 File Offset: 0x0003DA00
		private static bool a(char A_0)
		{
			return (A_0 >= '0' && A_0 <= '9') || (A_0 >= 'a' && A_0 <= 'z') || (A_0 >= 'A' && A_0 <= 'Z');
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0003EA28 File Offset: 0x0003DA28
		private static string o(string A_0)
		{
			string result = string.Empty;
			char[] trimChars = new char[]
			{
				'<',
				'>'
			};
			A_0 = A_0.Trim(trimChars);
			string text = string.Empty;
			Match match = global::a.i.m.m.Match(A_0);
			if (match.Groups["srcText"] != null)
			{
				text = match.Groups["srcText"].Value;
				char[] trimChars2 = new char[]
				{
					' ',
					'\'',
					'"'
				};
				text = text.Trim(trimChars2);
				if (text != null && text.Length != 0)
				{
					result = string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
					{
						text
					});
				}
			}
			return result;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003EAD4 File Offset: 0x0003DAD4
		private static string b(string A_0, HtmlToPlainConvertOptions A_1)
		{
			string result = string.Empty;
			string text = string.Empty;
			string text2 = string.Empty;
			char[] trimChars = new char[]
			{
				'<',
				'>'
			};
			A_0 = A_0.Trim(trimChars);
			text = global::a.i.m.n.Match(A_0).Groups["altText"].Value;
			text2 = global::a.i.m.o.Match(A_0).Groups["srcText"].Value;
			char[] trimChars2 = new char[]
			{
				' ',
				'\'',
				'"'
			};
			text2 = text2.Trim(trimChars2);
			text = text.Trim(trimChars2);
			if ((A_1 & HtmlToPlainConvertOptions.AddImgAltText) != HtmlToPlainConvertOptions.AddImgAltText && text != null && text.Length != 0)
			{
				text = string.Empty;
			}
			if ((A_1 & HtmlToPlainConvertOptions.WriteImageIfNoAlt) == HtmlToPlainConvertOptions.WriteImageIfNoAlt && text != null && text.Length == 0)
			{
				text = "image";
			}
			if ((A_1 & HtmlToPlainConvertOptions.AddUriForImg) != HtmlToPlainConvertOptions.AddUriForImg && text2 != null && text2.Length != 0)
			{
				text2 = string.Empty;
			}
			if (text.Length != 0 && text2.Length != 0)
			{
				result = string.Format(CultureInfo.InvariantCulture, "[{0} {1}]", new object[]
				{
					text,
					text2
				});
			}
			else if (text != null && text.Length != 0)
			{
				result = string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
				{
					text
				});
			}
			else if (text != null && text2.Length != 0)
			{
				result = string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
				{
					text2
				});
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0003EC3C File Offset: 0x0003DC3C
		public static string a(string A_0, HtmlToPlainConvertOptions A_1, bool A_2)
		{
			Match match = global::a.i.m.p.Match(A_0);
			if (match.Success)
			{
				int index = match.Index;
				match = match.NextMatch();
				int num;
				if (match.Success)
				{
					num = match.Index;
				}
				else
				{
					num = A_0.Length - 1;
				}
				A_0 = A_0.Substring(index, num - index);
			}
			string a_ = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			int num2 = 0;
			match = global::a.i.m.q.Match(A_0);
			while (match.Success)
			{
				string input = A_0.Substring(num2, match.Index - num2);
				Regex r = global::a.i.m.r;
				stringBuilder.Append(r.Replace(input, " "));
				num2 = match.Index + match.Length;
				stringBuilder.Append(match.Value);
				match = match.NextMatch();
			}
			if (num2 < A_0.Length)
			{
				string input2 = A_0.Substring(num2);
				Regex r2 = global::a.i.m.r;
				stringBuilder.Append(r2.Replace(input2, " "));
			}
			a_ = stringBuilder.ToString();
			if ((A_1 & HtmlToPlainConvertOptions.AddUriForAHRef) == HtmlToPlainConvertOptions.AddUriForAHRef)
			{
				a_ = global::a.i.b.f(a_);
			}
			if ((A_1 & HtmlToPlainConvertOptions.AddImgAltText) == HtmlToPlainConvertOptions.AddImgAltText || (A_1 & HtmlToPlainConvertOptions.AddUriForImg) == HtmlToPlainConvertOptions.AddUriForImg || (A_1 & HtmlToPlainConvertOptions.WriteImageIfNoAlt) == HtmlToPlainConvertOptions.WriteImageIfNoAlt)
			{
				a_ = global::a.i.b.a(a_, A_1);
			}
			a_ = global::a.i.b.n(a_);
			a_ = global::a.i.b.m(a_);
			a_ = global::a.i.b.l(a_);
			a_ = global::a.i.b.a(a_, A_2);
			return au.c(a_);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0003ED90 File Offset: 0x0003DD90
		private static string n(string A_0)
		{
			int num = 0;
			for (;;)
			{
				num = A_0.IndexOf("<!--", num);
				if (num == -1)
				{
					break;
				}
				int num2 = A_0.IndexOf("-->", num);
				if (num2 == -1)
				{
					break;
				}
				A_0 = A_0.Remove(num, num2 + 3 - num);
			}
			return A_0;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0003EDD4 File Offset: 0x0003DDD4
		private static string m(string A_0)
		{
			Match match;
			do
			{
				match = global::a.i.m.s.Match(A_0);
				if (match.Success)
				{
					A_0 = A_0.Remove(match.Index, match.Length);
				}
			}
			while (match.Success);
			return A_0;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0003EE14 File Offset: 0x0003DE14
		private static string l(string A_0)
		{
			Match match;
			do
			{
				match = global::a.i.m.t.Match(A_0);
				if (match.Success)
				{
					A_0 = A_0.Remove(match.Index, match.Length);
				}
			}
			while (match.Success);
			return A_0;
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0003EE54 File Offset: 0x0003DE54
		public static string k(string A_0)
		{
			Regex u = global::a.i.m.u;
			Match match = u.Match(A_0);
			while (match.Success)
			{
				string text = match.Groups["specSymb"].Value.ToLower();
				if (!(text == "nbsp"))
				{
					if (!(text == "lt"))
					{
						if (!(text == "gt"))
						{
							if (!(text == "amp"))
							{
								if (!(text == "quot"))
								{
									if (!(text == "copy"))
									{
										if (text[0] == '#')
										{
											try
											{
												int value = int.Parse(text.Substring(1), CultureInfo.InvariantCulture);
												A_0 = A_0.Replace(match.Value, Convert.ToChar(value).ToString());
											}
											catch (FormatException)
											{
											}
										}
									}
									else
									{
										A_0 = A_0.Replace(match.Value, "©");
									}
								}
								else
								{
									A_0 = A_0.Replace(match.Value, "\"");
								}
							}
							else
							{
								A_0 = A_0.Replace(match.Value, "@");
							}
						}
						else
						{
							A_0 = A_0.Replace(match.Value, ">");
						}
					}
					else
					{
						A_0 = A_0.Replace(match.Value, "<");
					}
				}
				else
				{
					A_0 = A_0.Replace(match.Value, " ");
				}
				match = u.Match(A_0, match.Index + text.Length);
			}
			return A_0;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0003EFD8 File Offset: 0x0003DFD8
		public static string j(string A_0)
		{
			if (A_0 == null)
			{
				return A_0;
			}
			A_0 = A_0.Replace("&", "&amp;");
			A_0 = A_0.Replace("<", "&lt;");
			A_0 = A_0.Replace(">", "&gt;");
			A_0 = A_0.Replace("\"", "&quot;");
			return A_0;
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0003F034 File Offset: 0x0003E034
		public static string b(string A_0, string A_1)
		{
			foreach (string oldValue in bb.a(A_0, A_1, false))
			{
				A_1 = A_1.Replace(oldValue, string.Empty);
			}
			return A_1;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0003F094 File Offset: 0x0003E094
		public static ArrayList a(ref string A_0, string A_1, bool A_2, ReplaceUriWithCidHandler A_3)
		{
			ArrayList arrayList = new ArrayList();
			bool flag = false;
			bool a_ = false;
			global::a.i.a a = default(global::a.i.a);
			bool flag2 = false;
			flag2 = global::a.i.m.v.Match(A_1).Success;
			foreach (string text in global::a.i.b.i(A_0))
			{
				flag = false;
				string oldValue = string.Empty;
				string text2 = string.Empty;
				string strA = global::a.i.b.e(text);
				foreach (object obj in global::a.i.b.g(text))
				{
					Match match = (Match)obj;
					if (string.Compare(match.Groups["paramName"].Value, "src", true) == 0 || string.Compare(match.Groups["paramName"].Value, "background", true) == 0)
					{
						flag = true;
					}
					else if (string.Compare(strA, "link", true) == 0 && string.Compare(match.Groups["paramName"].Value, "href", true) == 0)
					{
						flag = true;
					}
					if (flag)
					{
						a.c(match.Groups["paramValue"].Value);
						if (match.Groups["paramValue"].Value.IndexOf("/") > -1)
						{
							string value = match.Groups["paramValue"].Value;
							if (value.ToLower().StartsWith("file://") && value.IndexOf("\\") > -1)
							{
								a_ = false;
								a.c(a.c().Substring(7));
								if (a.c().Length > 1 && a.c()[0] == '/')
								{
									a.c(a.c().Substring(1));
								}
							}
							else
							{
								a_ = true;
							}
						}
						else
						{
							a_ = flag2;
						}
						oldValue = match.Value;
						text2 = match.Groups["paramName"].Value;
						break;
					}
				}
				string empty = string.Empty;
				if (!flag || A_3 == null || a.c() == null || !(a.c() != string.Empty) || A_3(a.c()))
				{
					if (flag && global::a.i.b.a(arrayList, a.c(), out empty))
					{
						string newValue = text.Replace(oldValue, string.Format(CultureInfo.InvariantCulture, " {0}=\"cid:{1}\"", new object[]
						{
							text2,
							empty
						}));
						A_0 = A_0.Replace(text, newValue);
					}
					else if (flag && a.c().Length <= 1024 && !a.c().ToLower().StartsWith("data:"))
					{
						a.b(global::a.i.k.a());
						string a_2 = a.c();
						try
						{
							a_2 = global::a.i.b.a(a.c(), A_1, a_, flag2);
						}
						catch (MailBeeIOException)
						{
							continue;
						}
						if (A_2)
						{
							try
							{
								string a_3;
								a.a(global::a.i.b.a(a_2, out a_3));
								a.a(a_3);
								arrayList.Add(a);
								goto IL_357;
							}
							catch (MailBeeWebException)
							{
								continue;
							}
						}
						try
						{
							a.a(ap.e(a_2));
							arrayList.Add(a);
						}
						catch (MailBeeIOException)
						{
							continue;
						}
						IL_357:
						string newValue2 = text.Replace(oldValue, string.Format(CultureInfo.InvariantCulture, " {0}=\"cid:{1}\"", new object[]
						{
							text2,
							a.a()
						}));
						A_0 = A_0.Replace(text, newValue2);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0003F4D4 File Offset: 0x0003E4D4
		private static bool a(ArrayList A_0, string A_1, out string A_2)
		{
			A_2 = string.Empty;
			foreach (object obj in A_0)
			{
				global::a.i.a a = (global::a.i.a)obj;
				if (string.Compare(a.c(), A_1, true) == 0)
				{
					A_2 = a.a();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0003F548 File Offset: 0x0003E548
		public static StringCollection i(string A_0)
		{
			StringCollection stringCollection = new StringCollection();
			Regex regex = new Regex("<[!A-Z_a-z].+", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			if (!regex.Match(A_0).Success)
			{
				return stringCollection;
			}
			regex = new Regex("<(/)?([^>])+(\\s+[^=\\s>]+([\\s]*=[\\s]*(?(\")([\"][^\"]*[\"])|(?(')(['][^']*['])|([^>]+))))?)*\\s*>", RegexOptions.IgnoreCase | RegexOptions.Singleline, new TimeSpan(0, 0, 1));
			try
			{
				foreach (object obj in regex.Matches(A_0))
				{
					Match match = (Match)obj;
					stringCollection.Add(match.Value);
				}
			}
			catch (RegexMatchTimeoutException)
			{
				return stringCollection;
			}
			return stringCollection;
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0003F5FC File Offset: 0x0003E5FC
		private static StringCollection h(string A_0)
		{
			return bb.a("a", A_0, true);
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003F60C File Offset: 0x0003E60C
		public static MatchCollection g(string A_0)
		{
			char[] trimChars = new char[]
			{
				'<',
				'>',
				' '
			};
			A_0 = A_0.Trim(trimChars);
			return global::a.i.m.w.Matches(A_0);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003F640 File Offset: 0x0003E640
		private static string b(string A_0, AHRefTagAttributes A_1, string A_2)
		{
			bool flag = false;
			MatchCollection matchCollection = global::a.i.b.g(A_0);
			bool flag2 = false;
			using (IEnumerator enumerator = matchCollection.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (string.Compare(((Match)enumerator.Current).Groups["paramName"].Value, "href", true) == 0)
					{
						flag2 = true;
						break;
					}
				}
			}
			if (flag2)
			{
				foreach (object obj in matchCollection)
				{
					Match match = (Match)obj;
					if ((A_1 & AHRefTagAttributes.Target) == AHRefTagAttributes.Target && string.Compare(match.Groups["paramName"].Value, "target", true) == 0)
					{
						flag = true;
					}
					if ((A_1 & AHRefTagAttributes.Onclick) == AHRefTagAttributes.Onclick && string.Compare(match.Groups["paramName"].Value, "onclick", true) == 0)
					{
						flag = true;
					}
					if ((A_1 & AHRefTagAttributes.ClassAndStyle) == AHRefTagAttributes.ClassAndStyle && string.Compare(match.Groups["paramName"].Value, "class", true) == 0)
					{
						flag = true;
					}
					if ((A_1 & AHRefTagAttributes.ClassAndStyle) == AHRefTagAttributes.ClassAndStyle && string.Compare(match.Groups["paramName"].Value, "style", true) == 0)
					{
						flag = true;
					}
					if (flag)
					{
						A_0 = A_0.Replace(match.Value, string.Empty);
						flag = false;
					}
				}
				if (A_2 != null && A_2.Length != 0)
				{
					char[] trimChars = new char[]
					{
						'<',
						'>',
						' '
					};
					A_0 = A_0.Trim(trimChars);
					A_0 = string.Format(CultureInfo.InvariantCulture, "<{0} {1}>", new object[]
					{
						A_0,
						A_2
					});
				}
			}
			return A_0;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0003F818 File Offset: 0x0003E818
		public static string a(string A_0, AHRefTagAttributes A_1, string A_2)
		{
			foreach (string text in global::a.i.b.h(A_0))
			{
				string newValue = global::a.i.b.b(text, A_1, A_2);
				A_0 = A_0.Replace(text, newValue);
			}
			return A_0;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0003F87C File Offset: 0x0003E87C
		private static string f(string A_0)
		{
			Match match = global::a.i.m.x.Match(A_0);
			while (match.Success)
			{
				StringCollection stringCollection = bb.a("a", match.Value, true);
				if (stringCollection.Count > 0)
				{
					string text = global::a.i.b.o(stringCollection[0]);
					text = string.Format("{0} {1}", match.Groups["tagContent"].Value, text);
					A_0 = A_0.Replace(match.Value, text);
				}
				match = match.NextMatch();
			}
			return A_0;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0003F900 File Offset: 0x0003E900
		private static string a(string A_0, HtmlToPlainConvertOptions A_1)
		{
			foreach (string text in bb.a("img", A_0, true))
			{
				string newValue = global::a.i.b.b(text, A_1);
				A_0 = A_0.Replace(text, newValue);
			}
			return A_0;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0003F968 File Offset: 0x0003E968
		public static string a(string A_0, PlainToHtmlConvertOptions A_1)
		{
			StringBuilder stringBuilder = new StringBuilder(global::a.i.b.j(A_0));
			if (A_1 == PlainToHtmlConvertOptions.UriToLink)
			{
				Regex y = global::a.i.m.y;
				Match match = y.Match(stringBuilder.ToString(), 0);
				while (match.Success)
				{
					string text = string.Format(CultureInfo.InvariantCulture, "<a href=\"{0}\">{0}</a>", new object[]
					{
						match.Value
					});
					stringBuilder.Replace(match.Value, text, match.Index, match.Length);
					match = y.Match(stringBuilder.ToString(), match.Index + text.Length);
				}
			}
			stringBuilder = new StringBuilder(global::a.i.m.z.Replace(stringBuilder.ToString(), "\r\n<br>\r\n"));
			return stringBuilder.ToString();
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0003FA18 File Offset: 0x0003EA18
		public static string a(string A_0, HtmlToSimpleHtmlConvertOptions A_1)
		{
			HtmlToPlainConvertOptions a_ = HtmlToPlainConvertOptions.None;
			A_0 = global::a.i.b.a(A_0, a_, true);
			Match match = global::a.i.m.aa.Match(A_0);
			while (match.Success)
			{
				if (string.Compare(match.Groups["tagName"].Value, "a", true) != 0)
				{
					goto IL_11B;
				}
				if (match.Value[1] != '/')
				{
					using (IEnumerator enumerator = global::a.i.b.g(match.Value).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							Match match2 = (Match)obj;
							if (string.Compare(match2.Groups["paramName"].Value, "href", true) == 0)
							{
								char[] trimChars = new char[]
								{
									' ',
									'"',
									'\''
								};
								string newValue = string.Format(CultureInfo.InvariantCulture, "<a href=\"{0}\">", new object[]
								{
									match2.Groups["paramValue"].Value.Trim(trimChars)
								});
								A_0 = A_0.Replace(match.Value, newValue);
								break;
							}
						}
						goto IL_2C5;
					}
					goto IL_11B;
				}
				IL_2C5:
				match = match.NextMatch();
				continue;
				IL_11B:
				if (string.Compare(match.Groups["tagName"].Value, "img", true) != 0)
				{
					goto IL_2C5;
				}
				if (A_1 == HtmlToSimpleHtmlConvertOptions.None)
				{
					A_0 = A_0.Replace(match.Value, string.Empty);
					goto IL_2C5;
				}
				string value = match.Value;
				string text = string.Empty;
				string text2 = string.Empty;
				string newValue2 = string.Empty;
				foreach (object obj2 in global::a.i.b.g(value))
				{
					Match match3 = (Match)obj2;
					if (match3.Groups["paramName"].Value.ToLower() == "src")
					{
						text2 = match3.Groups["paramValue"].Value;
					}
					if (match3.Groups["paramName"].Value.ToLower() == "alt")
					{
						text = match3.Groups["paramValue"].Value;
					}
				}
				if ((A_1 & HtmlToSimpleHtmlConvertOptions.WriteImageIfNoAlt) > HtmlToSimpleHtmlConvertOptions.None)
				{
					if (text == null || text.Length == 0)
					{
						text = "image";
					}
					newValue2 = string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
					{
						text
					});
				}
				if ((A_1 & HtmlToSimpleHtmlConvertOptions.AddImgAltText) > HtmlToSimpleHtmlConvertOptions.None && text != null)
				{
					newValue2 = string.Format(CultureInfo.InvariantCulture, "[{0}]", new object[]
					{
						text
					});
				}
				if ((A_1 & HtmlToSimpleHtmlConvertOptions.MakeLinkForImg) > HtmlToSimpleHtmlConvertOptions.None)
				{
					newValue2 = string.Format(CultureInfo.InvariantCulture, "<a href=\"{0}\">{1}</a>", new object[]
					{
						text2,
						text
					});
				}
				A_0 = A_0.Replace(value, newValue2);
				goto IL_2C5;
			}
			A_0 = A_0.Replace("\r\n", "<br/>");
			return A_0;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0003FD2C File Offset: 0x0003ED2C
		private static string e(string A_0)
		{
			return global::a.i.m.ab.Match(A_0).Groups["tagName"].Value;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0003FD50 File Offset: 0x0003ED50
		public static string a(string A_0, string A_1, bool A_2, bool A_3)
		{
			if (A_2 && A_0 != null)
			{
				if (global::a.i.m.ad.Match(A_0).Success)
				{
					return A_0;
				}
				if (A_3)
				{
					return global::a.i.b.a(A_1, A_0);
				}
				A_0 = au.d(A_0, Global.DefaultEncoding).Replace('/', '\\');
				if ((A_0.Length <= 1 || !(A_0.Substring(0, 2) == "\\\\")) && A_0.Length > 0 && A_0[0] == '\\')
				{
					A_0 = A_0.Substring(1);
				}
			}
			if (A_0.Length > 1 && A_0.Substring(0, 2) == "\\\\")
			{
				return A_0;
			}
			if (A_0 != null && A_0.Length != 0 && A_0[0] == '\\')
			{
				return ap.a(Path.GetPathRoot(A_1), A_0);
			}
			if (global::a.i.m.ae.Match(A_0).Success)
			{
				return A_0;
			}
			return ap.a(A_1, A_0);
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0003FE30 File Offset: 0x0003EE30
		public static byte[] d(string A_0)
		{
			byte[] result;
			try
			{
				result = new WebClient().DownloadData(A_0);
			}
			catch (WebException a_)
			{
				throw new MailBeeWebException(34, a_);
			}
			return result;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0003FE68 File Offset: 0x0003EE68
		private static string c(string A_0)
		{
			string text = null;
			if (A_0 != null)
			{
				string[] array = A_0.Split(new char[]
				{
					';'
				});
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i].Trim();
					if (text2.StartsWith("filename"))
					{
						string[] array2 = text2.Split(new char[]
						{
							'='
						});
						if (array2.Length > 1)
						{
							text = array2[1].Trim();
							if (text == string.Empty)
							{
								text = null;
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0003FEE4 File Offset: 0x0003EEE4
		public static byte[] a(string A_0, out string A_1)
		{
			byte[] result;
			try
			{
				A_1 = null;
				WebClient webClient = new WebClient();
				byte[] array = webClient.DownloadData(A_0);
				string a_ = webClient.ResponseHeaders["Content-Disposition"];
				A_1 = global::a.i.b.c(a_);
				result = array;
			}
			catch (WebException a_2)
			{
				throw new MailBeeWebException(34, a_2);
			}
			return result;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0003FF38 File Offset: 0x0003EF38
		public static string a(string A_0, string A_1)
		{
			string empty = string.Empty;
			if (A_0 == null || A_0.Length == 0)
			{
				return A_1;
			}
			if (A_0[A_0.Length - 1] == '/')
			{
				A_0 = A_0.Substring(0, A_0.Length - 1);
			}
			if (A_1 != null && A_1.Length != 0 && A_1[0] == '/')
			{
				A_1 = A_1.Substring(1, A_1.Length - 1);
				if (A_0.IndexOf("://") != -1)
				{
					int num = A_0.IndexOf('/', A_0.IndexOf(':') + 3);
					if (num > -1)
					{
						A_0 = A_0.Substring(0, num);
					}
				}
			}
			return string.Format(Global.DefaultCulture, "{0}/{1}", new object[]
			{
				A_0,
				A_1
			});
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0003FFF0 File Offset: 0x0003EFF0
		public static Task<ArrayList> a(global::a.i.c<string> A_0, string A_1, bool A_2, ReplaceUriWithCidHandler A_3)
		{
			global::a.i.b.d d;
			d.d = A_0;
			d.c = A_1;
			d.j = A_2;
			d.g = A_3;
			d.b = AsyncTaskMethodBuilder<ArrayList>.Create();
			d.a = -1;
			AsyncTaskMethodBuilder<ArrayList> b = d.b;
			b.Start<global::a.i.b.d>(ref d);
			return d.b.Task;
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00040050 File Offset: 0x0003F050
		public static Task<byte[]> b(string A_0)
		{
			global::a.i.b.a a;
			a.c = A_0;
			a.b = AsyncTaskMethodBuilder<byte[]>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<byte[]> b = a.b;
			b.Start<global::a.i.b.a>(ref a);
			return a.b.Task;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x00040098 File Offset: 0x0003F098
		private static Task<global::a.i.b.c> a(string A_0)
		{
			global::a.i.b.b b;
			b.c = A_0;
			b.b = AsyncTaskMethodBuilder<global::a.i.b.c>.Create();
			b.a = -1;
			AsyncTaskMethodBuilder<global::a.i.b.c> b2 = b.b;
			b2.Start<global::a.i.b.b>(ref b);
			return b.b.Task;
		}

		// Token: 0x020001EC RID: 492
		private struct c
		{
			// Token: 0x06000FE0 RID: 4064 RVA: 0x000400DD File Offset: 0x0003F0DD
			public c(byte[] A_0, string A_1)
			{
				this.a = A_0;
				this.b = A_1;
			}

			// Token: 0x04000B8B RID: 2955
			public readonly byte[] a;

			// Token: 0x04000B8C RID: 2956
			public readonly string b;
		}
	}
}

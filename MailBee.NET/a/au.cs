using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;

namespace a
{
	// Token: 0x020004FA RID: 1274
	internal class au
	{
		// Token: 0x06002A5E RID: 10846 RVA: 0x000C6C10 File Offset: 0x000C5C10
		private static Hashtable b()
		{
			object obj = au.b;
			Hashtable result;
			lock (obj)
			{
				if (au.a == null)
				{
					au.a();
				}
				result = au.a;
			}
			return result;
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x000C6C5C File Offset: 0x000C5C5C
		private static void a()
		{
			au.a = new Hashtable();
			au.a.Add("nbsp", '\u00a0');
			au.a.Add("iexcl", '¡');
			au.a.Add("cent", '¢');
			au.a.Add("pound", '£');
			au.a.Add("curren", '¤');
			au.a.Add("yen", '¥');
			au.a.Add("brvbar", '¦');
			au.a.Add("sect", '§');
			au.a.Add("uml", '¨');
			au.a.Add("copy", '©');
			au.a.Add("ordf", 'ª');
			au.a.Add("laquo", '«');
			au.a.Add("not", '¬');
			au.a.Add("shy", '­');
			au.a.Add("reg", '®');
			au.a.Add("macr", '¯');
			au.a.Add("deg", '°');
			au.a.Add("plusmn", '±');
			au.a.Add("sup2", '²');
			au.a.Add("sup3", '³');
			au.a.Add("acute", '´');
			au.a.Add("micro", 'µ');
			au.a.Add("para", '¶');
			au.a.Add("middot", '·');
			au.a.Add("cedil", '¸');
			au.a.Add("sup1", '¹');
			au.a.Add("ordm", 'º');
			au.a.Add("raquo", '»');
			au.a.Add("frac14", '¼');
			au.a.Add("frac12", '½');
			au.a.Add("frac34", '¾');
			au.a.Add("iquest", '¿');
			au.a.Add("Agrave", 'À');
			au.a.Add("Aacute", 'Á');
			au.a.Add("Acirc", 'Â');
			au.a.Add("Atilde", 'Ã');
			au.a.Add("Auml", 'Ä');
			au.a.Add("Aring", 'Å');
			au.a.Add("AElig", 'Æ');
			au.a.Add("Ccedil", 'Ç');
			au.a.Add("Egrave", 'È');
			au.a.Add("Eacute", 'É');
			au.a.Add("Ecirc", 'Ê');
			au.a.Add("Euml", 'Ë');
			au.a.Add("Igrave", 'Ì');
			au.a.Add("Iacute", 'Í');
			au.a.Add("Icirc", 'Î');
			au.a.Add("Iuml", 'Ï');
			au.a.Add("ETH", 'Ð');
			au.a.Add("Ntilde", 'Ñ');
			au.a.Add("Ograve", 'Ò');
			au.a.Add("Oacute", 'Ó');
			au.a.Add("Ocirc", 'Ô');
			au.a.Add("Otilde", 'Õ');
			au.a.Add("Ouml", 'Ö');
			au.a.Add("times", '×');
			au.a.Add("Oslash", 'Ø');
			au.a.Add("Ugrave", 'Ù');
			au.a.Add("Uacute", 'Ú');
			au.a.Add("Ucirc", 'Û');
			au.a.Add("Uuml", 'Ü');
			au.a.Add("Yacute", 'Ý');
			au.a.Add("THORN", 'Þ');
			au.a.Add("szlig", 'ß');
			au.a.Add("agrave", 'à');
			au.a.Add("aacute", 'á');
			au.a.Add("acirc", 'â');
			au.a.Add("atilde", 'ã');
			au.a.Add("auml", 'ä');
			au.a.Add("aring", 'å');
			au.a.Add("aelig", 'æ');
			au.a.Add("ccedil", 'ç');
			au.a.Add("egrave", 'è');
			au.a.Add("eacute", 'é');
			au.a.Add("ecirc", 'ê');
			au.a.Add("euml", 'ë');
			au.a.Add("igrave", 'ì');
			au.a.Add("iacute", 'í');
			au.a.Add("icirc", 'î');
			au.a.Add("iuml", 'ï');
			au.a.Add("eth", 'ð');
			au.a.Add("ntilde", 'ñ');
			au.a.Add("ograve", 'ò');
			au.a.Add("oacute", 'ó');
			au.a.Add("ocirc", 'ô');
			au.a.Add("otilde", 'õ');
			au.a.Add("ouml", 'ö');
			au.a.Add("divide", '÷');
			au.a.Add("oslash", 'ø');
			au.a.Add("ugrave", 'ù');
			au.a.Add("uacute", 'ú');
			au.a.Add("ucirc", 'û');
			au.a.Add("uuml", 'ü');
			au.a.Add("yacute", 'ý');
			au.a.Add("thorn", 'þ');
			au.a.Add("yuml", 'ÿ');
			au.a.Add("fnof", 'ƒ');
			au.a.Add("Alpha", 'Α');
			au.a.Add("Beta", 'Β');
			au.a.Add("Gamma", 'Γ');
			au.a.Add("Delta", 'Δ');
			au.a.Add("Epsilon", 'Ε');
			au.a.Add("Zeta", 'Ζ');
			au.a.Add("Eta", 'Η');
			au.a.Add("Theta", 'Θ');
			au.a.Add("Iota", 'Ι');
			au.a.Add("Kappa", 'Κ');
			au.a.Add("Lambda", 'Λ');
			au.a.Add("Mu", 'Μ');
			au.a.Add("Nu", 'Ν');
			au.a.Add("Xi", 'Ξ');
			au.a.Add("Omicron", 'Ο');
			au.a.Add("Pi", 'Π');
			au.a.Add("Rho", 'Ρ');
			au.a.Add("Sigma", 'Σ');
			au.a.Add("Tau", 'Τ');
			au.a.Add("Upsilon", 'Υ');
			au.a.Add("Phi", 'Φ');
			au.a.Add("Chi", 'Χ');
			au.a.Add("Psi", 'Ψ');
			au.a.Add("Omega", 'Ω');
			au.a.Add("alpha", 'α');
			au.a.Add("beta", 'β');
			au.a.Add("gamma", 'γ');
			au.a.Add("delta", 'δ');
			au.a.Add("epsilon", 'ε');
			au.a.Add("zeta", 'ζ');
			au.a.Add("eta", 'η');
			au.a.Add("theta", 'θ');
			au.a.Add("iota", 'ι');
			au.a.Add("kappa", 'κ');
			au.a.Add("lambda", 'λ');
			au.a.Add("mu", 'μ');
			au.a.Add("nu", 'ν');
			au.a.Add("xi", 'ξ');
			au.a.Add("omicron", 'ο');
			au.a.Add("pi", 'π');
			au.a.Add("rho", 'ρ');
			au.a.Add("sigmaf", 'ς');
			au.a.Add("sigma", 'σ');
			au.a.Add("tau", 'τ');
			au.a.Add("upsilon", 'υ');
			au.a.Add("phi", 'φ');
			au.a.Add("chi", 'χ');
			au.a.Add("psi", 'ψ');
			au.a.Add("omega", 'ω');
			au.a.Add("thetasym", 'ϑ');
			au.a.Add("upsih", 'ϒ');
			au.a.Add("piv", 'ϖ');
			au.a.Add("bull", '•');
			au.a.Add("hellip", '…');
			au.a.Add("prime", '′');
			au.a.Add("Prime", '″');
			au.a.Add("oline", '‾');
			au.a.Add("frasl", '⁄');
			au.a.Add("weierp", '℘');
			au.a.Add("image", 'ℑ');
			au.a.Add("real", 'ℜ');
			au.a.Add("trade", '™');
			au.a.Add("alefsym", 'ℵ');
			au.a.Add("larr", '←');
			au.a.Add("uarr", '↑');
			au.a.Add("rarr", '→');
			au.a.Add("darr", '↓');
			au.a.Add("harr", '↔');
			au.a.Add("crarr", '↵');
			au.a.Add("lArr", '⇐');
			au.a.Add("uArr", '⇑');
			au.a.Add("rArr", '⇒');
			au.a.Add("dArr", '⇓');
			au.a.Add("hArr", '⇔');
			au.a.Add("forall", '∀');
			au.a.Add("part", '∂');
			au.a.Add("exist", '∃');
			au.a.Add("empty", '∅');
			au.a.Add("nabla", '∇');
			au.a.Add("isin", '∈');
			au.a.Add("notin", '∉');
			au.a.Add("ni", '∋');
			au.a.Add("prod", '∏');
			au.a.Add("sum", '∑');
			au.a.Add("minus", '−');
			au.a.Add("lowast", '∗');
			au.a.Add("radic", '√');
			au.a.Add("prop", '∝');
			au.a.Add("infin", '∞');
			au.a.Add("ang", '∠');
			au.a.Add("and", '∧');
			au.a.Add("or", '∨');
			au.a.Add("cap", '∩');
			au.a.Add("cup", '∪');
			au.a.Add("int", '∫');
			au.a.Add("there4", '∴');
			au.a.Add("sim", '∼');
			au.a.Add("cong", '≅');
			au.a.Add("asymp", '≈');
			au.a.Add("ne", '≠');
			au.a.Add("equiv", '≡');
			au.a.Add("le", '≤');
			au.a.Add("ge", '≥');
			au.a.Add("sub", '⊂');
			au.a.Add("sup", '⊃');
			au.a.Add("nsub", '⊄');
			au.a.Add("sube", '⊆');
			au.a.Add("supe", '⊇');
			au.a.Add("oplus", '⊕');
			au.a.Add("otimes", '⊗');
			au.a.Add("perp", '⊥');
			au.a.Add("sdot", '⋅');
			au.a.Add("lceil", '⌈');
			au.a.Add("rceil", '⌉');
			au.a.Add("lfloor", '⌊');
			au.a.Add("rfloor", '⌋');
			au.a.Add("lang", '〈');
			au.a.Add("rang", '〉');
			au.a.Add("loz", '◊');
			au.a.Add("spades", '♠');
			au.a.Add("clubs", '♣');
			au.a.Add("hearts", '♥');
			au.a.Add("diams", '♦');
			au.a.Add("quot", '"');
			au.a.Add("amp", '&');
			au.a.Add("lt", '<');
			au.a.Add("gt", '>');
			au.a.Add("OElig", 'Œ');
			au.a.Add("oelig", 'œ');
			au.a.Add("Scaron", 'Š');
			au.a.Add("scaron", 'š');
			au.a.Add("Yuml", 'Ÿ');
			au.a.Add("circ", 'ˆ');
			au.a.Add("tilde", '˜');
			au.a.Add("ensp", '\u2002');
			au.a.Add("emsp", '\u2003');
			au.a.Add("thinsp", '\u2009');
			au.a.Add("zwnj", '‌');
			au.a.Add("zwj", '‍');
			au.a.Add("lrm", '‎');
			au.a.Add("rlm", '‏');
			au.a.Add("ndash", '–');
			au.a.Add("mdash", '—');
			au.a.Add("lsquo", '‘');
			au.a.Add("rsquo", '’');
			au.a.Add("sbquo", '‚');
			au.a.Add("ldquo", '“');
			au.a.Add("rdquo", '”');
			au.a.Add("bdquo", '„');
			au.a.Add("dagger", '†');
			au.a.Add("Dagger", '‡');
			au.a.Add("permil", '‰');
			au.a.Add("lsaquo", '‹');
			au.a.Add("rsaquo", '›');
			au.a.Add("euro", '€');
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x000C850B File Offset: 0x000C750B
		public static void c(string A_0, TextWriter A_1)
		{
			A_1.Write(au.j(A_0));
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x000C851C File Offset: 0x000C751C
		public static string j(string A_0)
		{
			if (A_0 == null)
			{
				return null;
			}
			bool flag = false;
			for (int i = 0; i < A_0.Length; i++)
			{
				if (A_0[i] == '&' || A_0[i] == '"' || A_0[i] == '<')
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return A_0;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = A_0.Length;
			for (int j = 0; j < length; j++)
			{
				char c = A_0[j];
				if (c != '"')
				{
					if (c != '&')
					{
						if (c != '<')
						{
							stringBuilder.Append(A_0[j]);
						}
						else
						{
							stringBuilder.Append("&lt;");
						}
					}
					else
					{
						stringBuilder.Append("&amp;");
					}
				}
				else
				{
					stringBuilder.Append("&quot;");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000C85E5 File Offset: 0x000C75E5
		public static string i(string A_0)
		{
			return au.d(A_0, Encoding.UTF8);
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000C85F2 File Offset: 0x000C75F2
		private static char[] a(MemoryStream A_0, Encoding A_1)
		{
			return A_1.GetChars(A_0.GetBuffer(), 0, (int)A_0.Length);
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x000C8608 File Offset: 0x000C7608
		public static string d(string A_0, Encoding A_1)
		{
			MemoryStream memoryStream = new MemoryStream();
			for (int i = 0; i < A_0.Length; i++)
			{
				if (A_0[i] == '%' && i + 2 < A_0.Length && A_0[i + 1] != '%')
				{
					string s = A_0.Substring(i + 1, 2);
					byte value = 0;
					if (byte.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out value))
					{
						memoryStream.WriteByte(value);
					}
					else
					{
						memoryStream.WriteByte(Convert.ToByte(A_0[i]));
						memoryStream.WriteByte(Convert.ToByte(A_0[i + 1]));
						memoryStream.WriteByte(Convert.ToByte(A_0[i + 2]));
					}
					i += 2;
				}
				else
				{
					memoryStream.WriteByte(Convert.ToByte(A_0[i]));
				}
			}
			byte[] bytes = memoryStream.ToArray();
			return A_1.GetString(bytes);
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x000C86E0 File Offset: 0x000C76E0
		public static string a(byte[] A_0, Encoding A_1)
		{
			if (A_0 == null)
			{
				return null;
			}
			return au.a(A_0, 0, A_0.Length, A_1);
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x000C86F4 File Offset: 0x000C76F4
		private static int a(byte A_0)
		{
			if (A_0 >= 48 && A_0 <= 57)
			{
				return (int)(A_0 - 48);
			}
			if (A_0 >= 97 && A_0 <= 102)
			{
				return (int)(A_0 - 97 + 10);
			}
			if (A_0 >= 65 && A_0 <= 70)
			{
				return (int)(A_0 - 65 + 10);
			}
			return -1;
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x000C8738 File Offset: 0x000C7738
		private static int d(byte[] A_0, int A_1, int A_2)
		{
			int num = 0;
			int num2 = A_2 + A_1;
			for (int i = A_1; i < num2; i++)
			{
				int num3 = au.a(A_0[i]);
				if (num3 == -1)
				{
					return -1;
				}
				num = (num << 4) + num3;
			}
			return num;
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x000C8770 File Offset: 0x000C7770
		private static int a(string A_0, int A_1, int A_2)
		{
			int num = 0;
			int num2 = A_2 + A_1;
			for (int i = A_1; i < num2; i++)
			{
				char c = A_0[i];
				if (c > '\u007f')
				{
					return -1;
				}
				int num3 = au.a((byte)c);
				if (num3 == -1)
				{
					return -1;
				}
				num = (num << 4) + num3;
			}
			return num;
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000C87B8 File Offset: 0x000C77B8
		public static string a(byte[] A_0, int A_1, int A_2, Encoding A_3)
		{
			if (A_0 == null)
			{
				return null;
			}
			if (A_2 == 0)
			{
				return string.Empty;
			}
			if (A_0 == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (A_1 < 0 || A_1 > A_0.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (A_2 < 0 || A_1 + A_2 > A_0.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			StringBuilder stringBuilder = new StringBuilder();
			MemoryStream memoryStream = new MemoryStream();
			int num = A_2 + A_1;
			int i = A_1;
			while (i < num)
			{
				if (A_0[i] != 37 || i + 2 >= A_2 || A_0[i + 1] == 37)
				{
					goto IL_EE;
				}
				if (A_0[i + 1] == 117 && i + 5 < num)
				{
					if (memoryStream.Length > 0L)
					{
						stringBuilder.Append(au.a(memoryStream, A_3));
						memoryStream.SetLength(0L);
					}
					int num2 = au.d(A_0, i + 2, 4);
					if (num2 == -1)
					{
						goto IL_EE;
					}
					stringBuilder.Append((char)num2);
					i += 5;
				}
				else
				{
					int num2;
					if ((num2 = au.d(A_0, i + 1, 2)) == -1)
					{
						goto IL_EE;
					}
					memoryStream.WriteByte((byte)num2);
					i += 2;
				}
				IL_12C:
				i++;
				continue;
				IL_EE:
				if (memoryStream.Length > 0L)
				{
					stringBuilder.Append(au.a(memoryStream, A_3));
					memoryStream.SetLength(0L);
				}
				if (A_0[i] == 43)
				{
					stringBuilder.Append(' ');
					goto IL_12C;
				}
				stringBuilder.Append((char)A_0[i]);
				goto IL_12C;
			}
			if (memoryStream.Length > 0L)
			{
				stringBuilder.Append(au.a(memoryStream, A_3));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000C891F File Offset: 0x000C791F
		public static byte[] c(byte[] A_0)
		{
			if (A_0 == null)
			{
				return null;
			}
			return au.c(A_0, 0, A_0.Length);
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000C8930 File Offset: 0x000C7930
		public static byte[] h(string A_0)
		{
			return au.c(A_0, Encoding.UTF8);
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000C893D File Offset: 0x000C793D
		public static byte[] c(string A_0, Encoding A_1)
		{
			if (A_0 == null)
			{
				return null;
			}
			if (A_1 == null)
			{
				throw new ArgumentNullException("e");
			}
			return au.c(A_1.GetBytes(A_0));
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000C8960 File Offset: 0x000C7960
		public static byte[] c(byte[] A_0, int A_1, int A_2)
		{
			if (A_0 == null)
			{
				return null;
			}
			if (A_2 == 0)
			{
				return new byte[0];
			}
			int num = A_0.Length;
			if (A_1 < 0 || A_1 >= num)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (A_2 < 0 || A_1 > num - A_2)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			MemoryStream memoryStream = new MemoryStream();
			int num2 = A_1 + A_2;
			for (int i = A_1; i < num2; i++)
			{
				char c = (char)A_0[i];
				if (c == '+')
				{
					c = ' ';
				}
				else if (c == '%' && i < num2 - 2)
				{
					int num3 = au.d(A_0, i + 1, 2);
					if (num3 != -1)
					{
						c = (char)num3;
						i += 2;
					}
				}
				memoryStream.WriteByte((byte)c);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000C8A04 File Offset: 0x000C7A04
		public static string g(string A_0)
		{
			return au.b(A_0, Encoding.UTF8);
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000C8A14 File Offset: 0x000C7A14
		public static string b(string A_0, Encoding A_1)
		{
			if (A_0 == null)
			{
				return null;
			}
			if (A_0 == "")
			{
				return "";
			}
			bool flag = false;
			int length = A_0.Length;
			for (int i = 0; i < length; i++)
			{
				char c = A_0[i];
				if ((c < '0' || (c < 'A' && c > '9') || (c > 'Z' && c < 'a') || c > 'z') && !au.a(c))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return A_0;
			}
			byte[] array = new byte[A_1.GetMaxByteCount(A_0.Length)];
			int bytes = A_1.GetBytes(A_0, 0, A_0.Length, array, 0);
			byte[] array2 = au.a(array, 0, bytes);
			return Encoding.ASCII.GetString(array2, 0, array2.Length);
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000C8AD0 File Offset: 0x000C7AD0
		public static string b(byte[] A_0)
		{
			if (A_0 == null)
			{
				return null;
			}
			if (A_0.Length == 0)
			{
				return "";
			}
			byte[] array = au.a(A_0, 0, A_0.Length);
			return Encoding.ASCII.GetString(array, 0, array.Length);
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000C8B08 File Offset: 0x000C7B08
		public static string b(byte[] A_0, int A_1, int A_2)
		{
			if (A_0 == null)
			{
				return null;
			}
			if (A_0.Length == 0)
			{
				return "";
			}
			byte[] array = au.a(A_0, A_1, A_2);
			return Encoding.ASCII.GetString(array, 0, array.Length);
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000C8B3C File Offset: 0x000C7B3C
		public static byte[] f(string A_0)
		{
			return au.a(A_0, Encoding.UTF8);
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000C8B4C File Offset: 0x000C7B4C
		public static byte[] a(string A_0, Encoding A_1)
		{
			if (A_0 == null)
			{
				return null;
			}
			if (A_0 == "")
			{
				return new byte[0];
			}
			byte[] bytes = A_1.GetBytes(A_0);
			return au.a(bytes, 0, bytes.Length);
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000C8B84 File Offset: 0x000C7B84
		public static byte[] a(byte[] A_0)
		{
			if (A_0 == null)
			{
				return null;
			}
			if (A_0.Length == 0)
			{
				return new byte[0];
			}
			return au.a(A_0, 0, A_0.Length);
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000C8BA0 File Offset: 0x000C7BA0
		private static bool a(char A_0)
		{
			return A_0 == '!' || A_0 == '\'' || A_0 == '(' || A_0 == ')' || A_0 == '*' || A_0 == '-' || A_0 == '.' || A_0 == '_';
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000C8BCC File Offset: 0x000C7BCC
		private static void a(char A_0, Stream A_1, bool A_2)
		{
			if (A_0 > 'ÿ')
			{
				A_1.WriteByte(37);
				A_1.WriteByte(117);
				int num = (int)(A_0 >> 12);
				A_1.WriteByte((byte)au.c[num]);
				num = (int)(A_0 >> 8 & '\u000f');
				A_1.WriteByte((byte)au.c[num]);
				num = (int)(A_0 >> 4 & '\u000f');
				A_1.WriteByte((byte)au.c[num]);
				num = (int)(A_0 & '\u000f');
				A_1.WriteByte((byte)au.c[num]);
				return;
			}
			if (A_0 > ' ' && au.a(A_0))
			{
				A_1.WriteByte((byte)A_0);
				return;
			}
			if (A_0 == ' ')
			{
				A_1.WriteByte(43);
				return;
			}
			if (A_0 < '0' || (A_0 < 'A' && A_0 > '9') || (A_0 > 'Z' && A_0 < 'a') || A_0 > 'z')
			{
				if (A_2 && A_0 > '\u007f')
				{
					A_1.WriteByte(37);
					A_1.WriteByte(117);
					A_1.WriteByte(48);
					A_1.WriteByte(48);
				}
				else
				{
					A_1.WriteByte(37);
				}
				int num2 = (int)(A_0 >> 4);
				A_1.WriteByte((byte)au.c[num2]);
				num2 = (int)(A_0 & '\u000f');
				A_1.WriteByte((byte)au.c[num2]);
				return;
			}
			A_1.WriteByte((byte)A_0);
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000C8CE4 File Offset: 0x000C7CE4
		public static byte[] a(byte[] A_0, int A_1, int A_2)
		{
			if (A_0 == null)
			{
				return null;
			}
			int num = A_0.Length;
			if (num == 0)
			{
				return new byte[0];
			}
			if (A_1 < 0 || A_1 >= num)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (A_2 < 0 || A_2 > num - A_1)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			MemoryStream memoryStream = new MemoryStream(A_2);
			int num2 = A_1 + A_2;
			for (int i = A_1; i < num2; i++)
			{
				au.a((char)A_0[i], memoryStream, false);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000C8D54 File Offset: 0x000C7D54
		public static string e(string A_0)
		{
			if (A_0 == null)
			{
				return null;
			}
			byte[] array = au.d(A_0);
			return Encoding.ASCII.GetString(array, 0, array.Length);
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x000C8D7C File Offset: 0x000C7D7C
		public static byte[] d(string A_0)
		{
			if (A_0 == null)
			{
				return null;
			}
			if (A_0 == "")
			{
				return new byte[0];
			}
			MemoryStream memoryStream = new MemoryStream(A_0.Length);
			for (int i = 0; i < A_0.Length; i++)
			{
				au.a(A_0[i], memoryStream, true);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x000C8DD8 File Offset: 0x000C7DD8
		public static string c(string A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("s");
			}
			if (A_0.IndexOf('&') == -1)
			{
				return A_0;
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			int length = A_0.Length;
			int num = 0;
			int num2 = 0;
			bool flag = false;
			for (int i = 0; i < length; i++)
			{
				char c = A_0[i];
				if (num == 0)
				{
					if (c == '&')
					{
						stringBuilder.Append(c);
						num = 1;
					}
					else
					{
						stringBuilder2.Append(c);
					}
				}
				else if (c == '&')
				{
					num = 1;
					if (flag)
					{
						stringBuilder.Append(num2.ToString(CultureInfo.InvariantCulture));
						flag = false;
					}
					stringBuilder2.Append(stringBuilder.ToString());
					stringBuilder.Length = 0;
					stringBuilder.Append('&');
				}
				else if (num == 1)
				{
					if (c == ';')
					{
						num = 0;
						stringBuilder2.Append(stringBuilder.ToString());
						stringBuilder2.Append(c);
						stringBuilder.Length = 0;
					}
					else
					{
						num2 = 0;
						if (c != '#')
						{
							num = 2;
						}
						else
						{
							num = 3;
						}
						stringBuilder.Append(c);
					}
				}
				else if (num == 2)
				{
					stringBuilder.Append(c);
					if (c == ';')
					{
						string text = stringBuilder.ToString();
						if (text.Length > 1 && au.b().ContainsKey(text.Substring(1, text.Length - 2)))
						{
							text = au.b()[text.Substring(1, text.Length - 2)].ToString();
						}
						stringBuilder2.Append(text);
						num = 0;
						stringBuilder.Length = 0;
					}
				}
				else if (num == 3)
				{
					if (c == ';')
					{
						if (num2 > 65535)
						{
							stringBuilder2.Append("&#");
							stringBuilder2.Append(num2.ToString(CultureInfo.InvariantCulture));
							stringBuilder2.Append(";");
						}
						else
						{
							stringBuilder2.Append((char)num2);
						}
						num = 0;
						stringBuilder.Length = 0;
						flag = false;
					}
					else if (char.IsDigit(c))
					{
						num2 = num2 * 10 + (int)(c - '0');
						flag = true;
					}
					else
					{
						num = 2;
						if (flag)
						{
							stringBuilder.Append(num2.ToString(CultureInfo.InvariantCulture));
							flag = false;
						}
						stringBuilder.Append(c);
					}
				}
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder2.Append(stringBuilder.ToString());
			}
			else if (flag)
			{
				stringBuilder2.Append(num2.ToString(CultureInfo.InvariantCulture));
			}
			return stringBuilder2.ToString();
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000C903F File Offset: 0x000C803F
		public static void b(string A_0, TextWriter A_1)
		{
			if (A_0 != null)
			{
				A_1.Write(au.c(A_0));
			}
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000C9050 File Offset: 0x000C8050
		public static string b(string A_0)
		{
			if (A_0 == null)
			{
				return null;
			}
			bool flag = false;
			foreach (char c in A_0)
			{
				if (c == '&' || c == '"' || c == '<' || c == '>' || c > '\u009f')
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return A_0;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = A_0.Length;
			int j = 0;
			while (j < length)
			{
				char c2 = A_0[j];
				if (c2 <= '&')
				{
					if (c2 != '"')
					{
						if (c2 != '&')
						{
							goto IL_C4;
						}
						stringBuilder.Append("&amp;");
					}
					else
					{
						stringBuilder.Append("&quot;");
					}
				}
				else if (c2 != '<')
				{
					if (c2 != '>')
					{
						goto IL_C4;
					}
					stringBuilder.Append("&gt;");
				}
				else
				{
					stringBuilder.Append("&lt;");
				}
				IL_119:
				j++;
				continue;
				IL_C4:
				if (A_0[j] > '\u009f')
				{
					stringBuilder.Append("&#");
					stringBuilder.Append(((int)A_0[j]).ToString(CultureInfo.InvariantCulture));
					stringBuilder.Append(";");
					goto IL_119;
				}
				stringBuilder.Append(A_0[j]);
				goto IL_119;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000C918A File Offset: 0x000C818A
		public static void a(string A_0, TextWriter A_1)
		{
			if (A_0 != null)
			{
				A_1.Write(au.b(A_0));
			}
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000C919C File Offset: 0x000C819C
		public static string a(string A_0)
		{
			if (A_0 == null || A_0.Length == 0)
			{
				return A_0;
			}
			MemoryStream memoryStream = new MemoryStream();
			int length = A_0.Length;
			for (int i = 0; i < length; i++)
			{
				au.a(A_0[i], memoryStream);
			}
			byte[] array = memoryStream.ToArray();
			return Encoding.ASCII.GetString(array, 0, array.Length);
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000C91F4 File Offset: 0x000C81F4
		private static void a(char A_0, Stream A_1)
		{
			if (A_0 < '!' || A_0 > '~')
			{
				byte[] bytes = Encoding.UTF8.GetBytes(A_0.ToString());
				for (int i = 0; i < bytes.Length; i++)
				{
					A_1.WriteByte(37);
					int num = bytes[i] >> 4;
					A_1.WriteByte((byte)au.c[num]);
					num = (int)(bytes[i] & 15);
					A_1.WriteByte((byte)au.c[num]);
				}
				return;
			}
			if (A_0 == ' ')
			{
				A_1.WriteByte(37);
				A_1.WriteByte(50);
				A_1.WriteByte(48);
				return;
			}
			A_1.WriteByte((byte)A_0);
		}

		// Token: 0x04001D3E RID: 7486
		private static Hashtable a;

		// Token: 0x04001D3F RID: 7487
		private static object b = new object();

		// Token: 0x04001D40 RID: 7488
		private static char[] c = "0123456789abcdef".ToCharArray();
	}
}

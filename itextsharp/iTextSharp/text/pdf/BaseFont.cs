using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.util;
using iTextSharp.text.error_messages;
using iTextSharp.text.xml.simpleparser;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000E5 RID: 229
	public abstract class BaseFont
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x0002BDB8 File Offset: 0x0002ADB8
		static BaseFont()
		{
			BaseFont.BuiltinFonts14.Add("Courier", PdfName.COURIER);
			BaseFont.BuiltinFonts14.Add("Courier-Bold", PdfName.COURIER_BOLD);
			BaseFont.BuiltinFonts14.Add("Courier-BoldOblique", PdfName.COURIER_BOLDOBLIQUE);
			BaseFont.BuiltinFonts14.Add("Courier-Oblique", PdfName.COURIER_OBLIQUE);
			BaseFont.BuiltinFonts14.Add("Helvetica", PdfName.HELVETICA);
			BaseFont.BuiltinFonts14.Add("Helvetica-Bold", PdfName.HELVETICA_BOLD);
			BaseFont.BuiltinFonts14.Add("Helvetica-BoldOblique", PdfName.HELVETICA_BOLDOBLIQUE);
			BaseFont.BuiltinFonts14.Add("Helvetica-Oblique", PdfName.HELVETICA_OBLIQUE);
			BaseFont.BuiltinFonts14.Add("Symbol", PdfName.SYMBOL);
			BaseFont.BuiltinFonts14.Add("Times-Roman", PdfName.TIMES_ROMAN);
			BaseFont.BuiltinFonts14.Add("Times-Bold", PdfName.TIMES_BOLD);
			BaseFont.BuiltinFonts14.Add("Times-BoldItalic", PdfName.TIMES_BOLDITALIC);
			BaseFont.BuiltinFonts14.Add("Times-Italic", PdfName.TIMES_ITALIC);
			BaseFont.BuiltinFonts14.Add("ZapfDingbats", PdfName.ZAPFDINGBATS);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002BFC8 File Offset: 0x0002AFC8
		public static BaseFont CreateFont()
		{
			return BaseFont.CreateFont("Helvetica", "Cp1252", false);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0002BFDA File Offset: 0x0002AFDA
		public static BaseFont CreateFont(string name, string encoding, bool embedded)
		{
			return BaseFont.CreateFont(name, encoding, embedded, true, null, null, false);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0002BFE8 File Offset: 0x0002AFE8
		public static BaseFont CreateFont(string name, string encoding, bool embedded, bool forceRead)
		{
			return BaseFont.CreateFont(name, encoding, embedded, true, null, null, forceRead);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002BFF6 File Offset: 0x0002AFF6
		public static BaseFont CreateFont(string name, string encoding, bool embedded, bool cached, byte[] ttfAfm, byte[] pfb)
		{
			return BaseFont.CreateFont(name, encoding, embedded, cached, ttfAfm, pfb, false);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0002C006 File Offset: 0x0002B006
		public static BaseFont CreateFont(string name, string encoding, bool embedded, bool cached, byte[] ttfAfm, byte[] pfb, bool noThrow)
		{
			return BaseFont.CreateFont(name, encoding, embedded, cached, ttfAfm, pfb, noThrow, false);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0002C018 File Offset: 0x0002B018
		public static BaseFont CreateFont(string name, string encoding, bool embedded, bool cached, byte[] ttfAfm, byte[] pfb, bool noThrow, bool forceRead)
		{
			string baseName = BaseFont.GetBaseName(name);
			encoding = BaseFont.NormalizeEncoding(encoding);
			bool flag = BaseFont.BuiltinFonts14.ContainsKey(name);
			bool flag2 = !flag && CJKFont.IsCJKFont(baseName, encoding);
			if (flag || flag2)
			{
				embedded = false;
			}
			else if (encoding.Equals("Identity-H") || encoding.Equals("Identity-V"))
			{
				embedded = true;
			}
			BaseFont baseFont = null;
			BaseFont baseFont2 = null;
			string key = string.Concat(new object[]
			{
				name,
				"\n",
				encoding,
				"\n",
				embedded
			});
			if (cached)
			{
				lock (BaseFont.fontCache)
				{
					BaseFont.fontCache.TryGetValue(key, out baseFont);
				}
				if (baseFont != null)
				{
					return baseFont;
				}
			}
			if (flag || name.ToLower(CultureInfo.InvariantCulture).EndsWith(".afm") || name.ToLower(CultureInfo.InvariantCulture).EndsWith(".pfm"))
			{
				baseFont2 = new Type1Font(name, encoding, embedded, ttfAfm, pfb, forceRead);
				baseFont2.fastWinansi = encoding.Equals("Cp1252");
			}
			else if (baseName.ToLower(CultureInfo.InvariantCulture).EndsWith(".ttf") || baseName.ToLower(CultureInfo.InvariantCulture).EndsWith(".otf") || baseName.ToLower(CultureInfo.InvariantCulture).IndexOf(".ttc,") > 0)
			{
				if (encoding.Equals("Identity-H") || encoding.Equals("Identity-V"))
				{
					baseFont2 = new TrueTypeFontUnicode(name, encoding, embedded, ttfAfm, forceRead);
				}
				else
				{
					baseFont2 = new TrueTypeFont(name, encoding, embedded, ttfAfm, false, forceRead);
					baseFont2.fastWinansi = encoding.Equals("Cp1252");
				}
			}
			else if (flag2)
			{
				baseFont2 = new CJKFont(name, encoding, embedded);
			}
			else
			{
				if (noThrow)
				{
					return null;
				}
				throw new DocumentException(MessageLocalization.GetComposedMessage("font.1.with.2.is.not.recognized", name, encoding));
			}
			if (cached)
			{
				lock (BaseFont.fontCache)
				{
					BaseFont.fontCache.TryGetValue(key, out baseFont);
					if (baseFont != null)
					{
						return baseFont;
					}
					BaseFont.fontCache[key] = baseFont2;
				}
				return baseFont2;
			}
			return baseFont2;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0002C254 File Offset: 0x0002B254
		public static BaseFont CreateFont(PRIndirectReference fontRef)
		{
			return new DocumentFont(fontRef);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0002C25C File Offset: 0x0002B25C
		protected static string GetBaseName(string name)
		{
			if (name.EndsWith(",Bold"))
			{
				return name.Substring(0, name.Length - 5);
			}
			if (name.EndsWith(",Italic"))
			{
				return name.Substring(0, name.Length - 7);
			}
			if (name.EndsWith(",BoldItalic"))
			{
				return name.Substring(0, name.Length - 11);
			}
			return name;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0002C2C4 File Offset: 0x0002B2C4
		protected static string NormalizeEncoding(string enc)
		{
			if (enc.Equals("winansi") || enc.Equals(""))
			{
				return "Cp1252";
			}
			if (enc.Equals("macroman"))
			{
				return "MacRoman";
			}
			int encodingNumber = IanaEncodings.GetEncodingNumber(enc);
			if (encodingNumber == 1252)
			{
				return "Cp1252";
			}
			if (encodingNumber == 10000)
			{
				return "MacRoman";
			}
			return enc;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0002C328 File Offset: 0x0002B328
		protected void CreateEncoding()
		{
			if (this.encoding.StartsWith("#"))
			{
				this.specialMap = new IntHashtable();
				StringTokenizer stringTokenizer = new StringTokenizer(this.encoding.Substring(1), " ,\t\n\r\f");
				if (stringTokenizer.NextToken().Equals("full"))
				{
					while (stringTokenizer.HasMoreTokens())
					{
						string text = stringTokenizer.NextToken();
						string text2 = stringTokenizer.NextToken();
						char c = (char)int.Parse(stringTokenizer.NextToken(), NumberStyles.HexNumber);
						int num;
						if (text.StartsWith("'"))
						{
							num = (int)text[1];
						}
						else
						{
							num = int.Parse(text);
						}
						num %= 256;
						this.specialMap[(int)c] = num;
						this.differences[num] = text2;
						this.unicodeDifferences[num] = c;
						this.widths[num] = this.GetRawWidth((int)c, text2);
						this.charBBoxes[num] = this.GetRawCharBBox((int)c, text2);
					}
				}
				else
				{
					int num2 = 0;
					if (stringTokenizer.HasMoreTokens())
					{
						num2 = int.Parse(stringTokenizer.NextToken());
					}
					while (stringTokenizer.HasMoreTokens() && num2 < 256)
					{
						string s = stringTokenizer.NextToken();
						int num3 = int.Parse(s, NumberStyles.HexNumber) % 65536;
						string text3 = GlyphList.UnicodeToName(num3);
						if (text3 != null)
						{
							this.specialMap[num3] = num2;
							this.differences[num2] = text3;
							this.unicodeDifferences[num2] = (char)num3;
							this.widths[num2] = this.GetRawWidth(num3, text3);
							this.charBBoxes[num2] = this.GetRawCharBBox(num3, text3);
							num2++;
						}
					}
				}
				for (int i = 0; i < 256; i++)
				{
					if (this.differences[i] == null)
					{
						this.differences[i] = ".notdef";
					}
				}
				return;
			}
			if (this.fontSpecific)
			{
				for (int j = 0; j < 256; j++)
				{
					this.widths[j] = this.GetRawWidth(j, null);
					this.charBBoxes[j] = this.GetRawCharBBox(j, null);
				}
				return;
			}
			byte[] array = new byte[1];
			for (int k = 0; k < 256; k++)
			{
				array[0] = (byte)k;
				string text4 = PdfEncodings.ConvertToString(array, this.encoding);
				char c2;
				if (text4.Length > 0)
				{
					c2 = text4[0];
				}
				else
				{
					c2 = '?';
				}
				string text5 = GlyphList.UnicodeToName((int)c2);
				if (text5 == null)
				{
					text5 = ".notdef";
				}
				this.differences[k] = text5;
				this.UnicodeDifferences[k] = c2;
				this.widths[k] = this.GetRawWidth((int)c2, text5);
				this.charBBoxes[k] = this.GetRawCharBBox((int)c2, text5);
			}
		}

		// Token: 0x0600086C RID: 2156
		internal abstract int GetRawWidth(int c, string name);

		// Token: 0x0600086D RID: 2157
		public abstract int GetKerning(int char1, int char2);

		// Token: 0x0600086E RID: 2158
		public abstract bool SetKerning(int char1, int char2, int kern);

		// Token: 0x0600086F RID: 2159 RVA: 0x0002C5E4 File Offset: 0x0002B5E4
		public virtual int GetWidth(int char1)
		{
			if (!this.fastWinansi)
			{
				int num = 0;
				byte[] array = this.ConvertToBytes((int)((ushort)char1));
				for (int i = 0; i < array.Length; i++)
				{
					num += this.widths[(int)(byte.MaxValue & array[i])];
				}
				return num;
			}
			if (char1 < 128 || (char1 >= 160 && char1 <= 255))
			{
				return this.widths[char1];
			}
			return this.widths[PdfEncodings.winansi[char1]];
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0002C65C File Offset: 0x0002B65C
		public virtual int GetWidth(string text)
		{
			int num = 0;
			if (this.fastWinansi)
			{
				int length = text.Length;
				for (int i = 0; i < length; i++)
				{
					char c = text[i];
					if (c < '\u0080' || (c >= '\u00a0' && c <= 'ÿ'))
					{
						num += this.widths[(int)c];
					}
					else
					{
						num += this.widths[PdfEncodings.winansi[(int)c]];
					}
				}
				return num;
			}
			byte[] array = this.ConvertToBytes(text);
			for (int j = 0; j < array.Length; j++)
			{
				num += this.widths[(int)(byte.MaxValue & array[j])];
			}
			return num;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0002C6FC File Offset: 0x0002B6FC
		public int GetDescent(string text)
		{
			int num = 0;
			char[] array = text.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				int[] charBBox = this.GetCharBBox((int)array[i]);
				if (charBBox != null && charBBox[1] < num)
				{
					num = charBBox[1];
				}
			}
			return num;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0002C738 File Offset: 0x0002B738
		public int GetAscent(string text)
		{
			int num = 0;
			char[] array = text.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				int[] charBBox = this.GetCharBBox((int)array[i]);
				if (charBBox != null && charBBox[3] > num)
				{
					num = charBBox[3];
				}
			}
			return num;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0002C774 File Offset: 0x0002B774
		public float GetDescentPoint(string text, float fontSize)
		{
			return (float)this.GetDescent(text) * 0.001f * fontSize;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0002C786 File Offset: 0x0002B786
		public float GetAscentPoint(string text, float fontSize)
		{
			return (float)this.GetAscent(text) * 0.001f * fontSize;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0002C798 File Offset: 0x0002B798
		public float GetWidthPointKerned(string text, float fontSize)
		{
			float num = (float)this.GetWidth(text) * 0.001f * fontSize;
			if (!this.HasKernPairs())
			{
				return num;
			}
			int num2 = text.Length - 1;
			int num3 = 0;
			char[] array = text.ToCharArray();
			for (int i = 0; i < num2; i++)
			{
				num3 += this.GetKerning((int)array[i], (int)array[i + 1]);
			}
			return num + (float)num3 * 0.001f * fontSize;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0002C801 File Offset: 0x0002B801
		public float GetWidthPoint(string text, float fontSize)
		{
			return (float)this.GetWidth(text) * 0.001f * fontSize;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0002C813 File Offset: 0x0002B813
		public float GetWidthPoint(int char1, float fontSize)
		{
			return (float)this.GetWidth(char1) * 0.001f * fontSize;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0002C828 File Offset: 0x0002B828
		internal virtual byte[] ConvertToBytes(string text)
		{
			if (this.directTextToByte)
			{
				return PdfEncodings.ConvertToBytes(text, null);
			}
			if (this.specialMap == null)
			{
				return PdfEncodings.ConvertToBytes(text, this.encoding);
			}
			byte[] array = new byte[text.Length];
			int num = 0;
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				char key = text[i];
				if (this.specialMap.ContainsKey((int)key))
				{
					array[num++] = (byte)this.specialMap[(int)key];
				}
			}
			if (num < length)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, 0, array2, 0, num);
				return array2;
			}
			return array;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0002C8C4 File Offset: 0x0002B8C4
		internal virtual byte[] ConvertToBytes(int char1)
		{
			if (this.directTextToByte)
			{
				return PdfEncodings.ConvertToBytes((char)char1, null);
			}
			if (this.specialMap == null)
			{
				return PdfEncodings.ConvertToBytes((char)char1, this.encoding);
			}
			if (this.specialMap.ContainsKey(char1))
			{
				return new byte[]
				{
					(byte)this.specialMap[char1]
				};
			}
			return new byte[0];
		}

		// Token: 0x0600087A RID: 2170
		internal abstract void WriteFont(PdfWriter writer, PdfIndirectReference piRef, object[] oParams);

		// Token: 0x0600087B RID: 2171
		public abstract PdfStream GetFullFontStream();

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0002C925 File Offset: 0x0002B925
		public string Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x0600087D RID: 2173
		public abstract float GetFontDescriptor(int key, float fontSize);

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0002C92D File Offset: 0x0002B92D
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x0002C935 File Offset: 0x0002B935
		public int FontType
		{
			get
			{
				return this.fontType;
			}
			set
			{
				this.fontType = value;
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0002C93E File Offset: 0x0002B93E
		public bool IsEmbedded()
		{
			return this.embedded;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0002C946 File Offset: 0x0002B946
		public bool IsFontSpecific()
		{
			return this.fontSpecific;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0002C950 File Offset: 0x0002B950
		internal static string CreateSubsetPrefix()
		{
			char[] array = new char[7];
			lock (BaseFont.random)
			{
				for (int i = 0; i < 6; i++)
				{
					array[i] = (char)BaseFont.random.Next(65, 91);
				}
			}
			array[6] = '+';
			return new string(array);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0002C9B4 File Offset: 0x0002B9B4
		internal char GetUnicodeDifferences(int index)
		{
			return this.unicodeDifferences[index];
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000884 RID: 2180
		// (set) Token: 0x06000885 RID: 2181
		public abstract string PostscriptFontName { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000886 RID: 2182
		public abstract string[][] FullFontName { get; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000887 RID: 2183
		public abstract string[][] AllNameEntries { get; }

		// Token: 0x06000888 RID: 2184 RVA: 0x0002C9C0 File Offset: 0x0002B9C0
		public static string[][] GetFullFontName(string name, string encoding, byte[] ttfAfm)
		{
			string baseName = BaseFont.GetBaseName(name);
			BaseFont baseFont;
			if (baseName.ToLower(CultureInfo.InvariantCulture).EndsWith(".ttf") || baseName.ToLower(CultureInfo.InvariantCulture).EndsWith(".otf") || baseName.ToLower(CultureInfo.InvariantCulture).IndexOf(".ttc,") > 0)
			{
				baseFont = new TrueTypeFont(name, "Cp1252", false, ttfAfm, true, false);
			}
			else
			{
				baseFont = BaseFont.CreateFont(name, encoding, false, false, ttfAfm, null);
			}
			return baseFont.FullFontName;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0002CA40 File Offset: 0x0002BA40
		public static object[] GetAllFontNames(string name, string encoding, byte[] ttfAfm)
		{
			string baseName = BaseFont.GetBaseName(name);
			BaseFont baseFont;
			if (baseName.ToLower(CultureInfo.InvariantCulture).EndsWith(".ttf") || baseName.ToLower(CultureInfo.InvariantCulture).EndsWith(".otf") || baseName.ToLower(CultureInfo.InvariantCulture).IndexOf(".ttc,") > 0)
			{
				baseFont = new TrueTypeFont(name, "Cp1252", false, ttfAfm, true, false);
			}
			else
			{
				baseFont = BaseFont.CreateFont(name, encoding, false, false, ttfAfm, null);
			}
			return new object[]
			{
				baseFont.PostscriptFontName,
				baseFont.FamilyFontName,
				baseFont.FullFontName
			};
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0002CAE0 File Offset: 0x0002BAE0
		public static string[][] GetAllNameEntries(string name, string encoding, byte[] ttfAfm)
		{
			string baseName = BaseFont.GetBaseName(name);
			BaseFont baseFont;
			if (baseName.ToLower(CultureInfo.InvariantCulture).EndsWith(".ttf") || baseName.ToLower(CultureInfo.InvariantCulture).EndsWith(".otf") || baseName.ToLower(CultureInfo.InvariantCulture).IndexOf(".ttc,") > 0)
			{
				baseFont = new TrueTypeFont(name, "Cp1252", false, ttfAfm, true, false);
			}
			else
			{
				baseFont = BaseFont.CreateFont(name, encoding, false, false, ttfAfm, null);
			}
			return baseFont.AllNameEntries;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600088B RID: 2187
		public abstract string[][] FamilyFontName { get; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0002CB60 File Offset: 0x0002BB60
		public virtual string[] CodePagesSupported
		{
			get
			{
				return new string[0];
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0002CB68 File Offset: 0x0002BB68
		public static string[] EnumerateTTCNames(string ttcFile)
		{
			return new EnumerateTTC(ttcFile).Names;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0002CB75 File Offset: 0x0002BB75
		public static string[] EnumerateTTCNames(byte[] ttcArray)
		{
			return new EnumerateTTC(ttcArray).Names;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0002CB82 File Offset: 0x0002BB82
		public int[] Widths
		{
			get
			{
				return this.widths;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0002CB8A File Offset: 0x0002BB8A
		public string[] Differences
		{
			get
			{
				return this.differences;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x0002CB92 File Offset: 0x0002BB92
		public char[] UnicodeDifferences
		{
			get
			{
				return this.unicodeDifferences;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0002CBA3 File Offset: 0x0002BBA3
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x0002CB9A File Offset: 0x0002BB9A
		public bool ForceWidthsOutput
		{
			get
			{
				return this.forceWidthsOutput;
			}
			set
			{
				this.forceWidthsOutput = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0002CBB4 File Offset: 0x0002BBB4
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0002CBAB File Offset: 0x0002BBAB
		public bool DirectTextToByte
		{
			get
			{
				return this.directTextToByte;
			}
			set
			{
				this.directTextToByte = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0002CBC5 File Offset: 0x0002BBC5
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x0002CBBC File Offset: 0x0002BBBC
		public bool Subset
		{
			get
			{
				return this.subset;
			}
			set
			{
				this.subset = value;
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0002CBD0 File Offset: 0x0002BBD0
		public static void AddToResourceSearch(object obj)
		{
			lock (BaseFont.resourceSearch)
			{
				if (obj is Assembly)
				{
					BaseFont.resourceSearch.Add(obj);
				}
				else if (obj is string)
				{
					string path = (string)obj;
					if (Directory.Exists(path) || File.Exists(path))
					{
						BaseFont.resourceSearch.Add(obj);
					}
				}
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0002CC44 File Offset: 0x0002BC44
		public static Stream GetResourceStream(string key)
		{
			Stream stream = null;
			try
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				stream = executingAssembly.GetManifestResourceStream(key);
			}
			catch
			{
			}
			if (stream != null)
			{
				return stream;
			}
			int count;
			lock (BaseFont.resourceSearch)
			{
				count = BaseFont.resourceSearch.Count;
			}
			for (int i = 0; i < count; i++)
			{
				object obj3;
				lock (BaseFont.resourceSearch)
				{
					obj3 = BaseFont.resourceSearch[i];
				}
				try
				{
					if (obj3 is Assembly)
					{
						stream = ((Assembly)obj3).GetManifestResourceStream(key);
						if (stream != null)
						{
							return stream;
						}
					}
					else if (obj3 is string)
					{
						string text = (string)obj3;
						try
						{
							stream = Assembly.LoadFrom(text).GetManifestResourceStream(key);
						}
						catch
						{
						}
						if (stream != null)
						{
							return stream;
						}
						string text2 = key.Replace('.', '/');
						string path = Path.Combine(text, text2);
						if (File.Exists(path))
						{
							return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
						}
						int num = text2.LastIndexOf('/');
						if (num >= 0)
						{
							text2 = text2.Substring(0, num) + "." + text2.Substring(num + 1);
							path = Path.Combine(text, text2);
							if (File.Exists(path))
							{
								return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
							}
						}
					}
				}
				catch
				{
				}
			}
			return stream;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0002CDE8 File Offset: 0x0002BDE8
		public virtual int GetUnicodeEquivalent(int c)
		{
			return c;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0002CDEB File Offset: 0x0002BDEB
		public virtual int GetCidCode(int c)
		{
			return c;
		}

		// Token: 0x0600089C RID: 2204
		public abstract bool HasKernPairs();

		// Token: 0x0600089D RID: 2205 RVA: 0x0002CDF0 File Offset: 0x0002BDF0
		public virtual bool CharExists(int c)
		{
			byte[] array = this.ConvertToBytes(c);
			return array.Length > 0;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0002CE0C File Offset: 0x0002BE0C
		public virtual bool SetCharAdvance(int c, int advance)
		{
			byte[] array = this.ConvertToBytes(c);
			if (array.Length == 0)
			{
				return false;
			}
			this.widths[(int)(byte.MaxValue & array[0])] = advance;
			return true;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0002CE3C File Offset: 0x0002BE3C
		private static void AddFont(PRIndirectReference fontRef, IntHashtable hits, List<object[]> fonts)
		{
			PdfObject pdfObject = PdfReader.GetPdfObject(fontRef);
			if (pdfObject == null || !pdfObject.IsDictionary())
			{
				return;
			}
			PdfDictionary pdfDictionary = (PdfDictionary)pdfObject;
			PdfName asName = pdfDictionary.GetAsName(PdfName.SUBTYPE);
			if (!PdfName.TYPE1.Equals(asName) && !PdfName.TRUETYPE.Equals(asName) && !PdfName.TYPE0.Equals(asName))
			{
				return;
			}
			PdfName asName2 = pdfDictionary.GetAsName(PdfName.BASEFONT);
			fonts.Add(new object[]
			{
				PdfName.DecodeName(asName2.ToString()),
				fontRef
			});
			hits[fontRef.Number] = 1;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0002CED4 File Offset: 0x0002BED4
		private static void RecourseFonts(PdfDictionary page, IntHashtable hits, List<object[]> fonts, int level)
		{
			level++;
			if (level > 50)
			{
				return;
			}
			if (page == null)
			{
				return;
			}
			PdfDictionary asDict = page.GetAsDict(PdfName.RESOURCES);
			if (asDict == null)
			{
				return;
			}
			PdfDictionary asDict2 = asDict.GetAsDict(PdfName.FONT);
			if (asDict2 != null)
			{
				foreach (PdfName key in asDict2.Keys)
				{
					PdfObject pdfObject = asDict2.Get(key);
					if (pdfObject != null && pdfObject.IsIndirect())
					{
						int number = ((PRIndirectReference)pdfObject).Number;
						if (!hits.ContainsKey(number))
						{
							BaseFont.AddFont((PRIndirectReference)pdfObject, hits, fonts);
						}
					}
				}
			}
			PdfDictionary asDict3 = asDict.GetAsDict(PdfName.XOBJECT);
			if (asDict3 != null)
			{
				foreach (PdfName key2 in asDict3.Keys)
				{
					PdfObject directObject = asDict3.GetDirectObject(key2);
					if (directObject is PdfDictionary)
					{
						BaseFont.RecourseFonts((PdfDictionary)directObject, hits, fonts, level);
					}
				}
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0002CFF8 File Offset: 0x0002BFF8
		public static List<object[]> GetDocumentFonts(PdfReader reader)
		{
			IntHashtable hits = new IntHashtable();
			List<object[]> list = new List<object[]>();
			int numberOfPages = reader.NumberOfPages;
			for (int i = 1; i <= numberOfPages; i++)
			{
				BaseFont.RecourseFonts(reader.GetPageN(i), hits, list, 1);
			}
			return list;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002D034 File Offset: 0x0002C034
		public static List<object[]> GetDocumentFonts(PdfReader reader, int page)
		{
			IntHashtable hits = new IntHashtable();
			List<object[]> list = new List<object[]>();
			BaseFont.RecourseFonts(reader.GetPageN(page), hits, list, 1);
			return list;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0002D060 File Offset: 0x0002C060
		public virtual int[] GetCharBBox(int c)
		{
			byte[] array = this.ConvertToBytes(c);
			if (array.Length == 0)
			{
				return null;
			}
			return this.charBBoxes[(int)(array[0] & byte.MaxValue)];
		}

		// Token: 0x060008A4 RID: 2212
		protected abstract int[] GetRawCharBBox(int c, string name);

		// Token: 0x060008A5 RID: 2213 RVA: 0x0002D08C File Offset: 0x0002C08C
		public void CorrectArabicAdvance()
		{
			for (char c = 'ً'; c <= '٘'; c += '\u0001')
			{
				this.SetCharAdvance((int)c, 0);
			}
			this.SetCharAdvance(1648, 0);
			for (char c2 = 'ۖ'; c2 <= 'ۜ'; c2 += '\u0001')
			{
				this.SetCharAdvance((int)c2, 0);
			}
			for (char c3 = '۟'; c3 <= 'ۤ'; c3 += '\u0001')
			{
				this.SetCharAdvance((int)c3, 0);
			}
			for (char c4 = 'ۧ'; c4 <= 'ۨ'; c4 += '\u0001')
			{
				this.SetCharAdvance((int)c4, 0);
			}
			for (char c5 = '۪'; c5 <= 'ۭ'; c5 += '\u0001')
			{
				this.SetCharAdvance((int)c5, 0);
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0002D141 File Offset: 0x0002C141
		public void AddSubsetRange(int[] range)
		{
			if (this.subsetRanges == null)
			{
				this.subsetRanges = new List<int[]>();
			}
			this.subsetRanges.Add(range);
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0002D17C File Offset: 0x0002C17C
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x0002D162 File Offset: 0x0002C162
		public int CompressionLevel
		{
			get
			{
				return this.compressionLevel;
			}
			set
			{
				if (value < 0 || value > 9)
				{
					this.compressionLevel = -1;
					return;
				}
				this.compressionLevel = value;
			}
		}

		// Token: 0x040006F3 RID: 1779
		public const string COURIER = "Courier";

		// Token: 0x040006F4 RID: 1780
		public const string COURIER_BOLD = "Courier-Bold";

		// Token: 0x040006F5 RID: 1781
		public const string COURIER_OBLIQUE = "Courier-Oblique";

		// Token: 0x040006F6 RID: 1782
		public const string COURIER_BOLDOBLIQUE = "Courier-BoldOblique";

		// Token: 0x040006F7 RID: 1783
		public const string HELVETICA = "Helvetica";

		// Token: 0x040006F8 RID: 1784
		public const string HELVETICA_BOLD = "Helvetica-Bold";

		// Token: 0x040006F9 RID: 1785
		public const string HELVETICA_OBLIQUE = "Helvetica-Oblique";

		// Token: 0x040006FA RID: 1786
		public const string HELVETICA_BOLDOBLIQUE = "Helvetica-BoldOblique";

		// Token: 0x040006FB RID: 1787
		public const string SYMBOL = "Symbol";

		// Token: 0x040006FC RID: 1788
		public const string TIMES_ROMAN = "Times-Roman";

		// Token: 0x040006FD RID: 1789
		public const string TIMES_BOLD = "Times-Bold";

		// Token: 0x040006FE RID: 1790
		public const string TIMES_ITALIC = "Times-Italic";

		// Token: 0x040006FF RID: 1791
		public const string TIMES_BOLDITALIC = "Times-BoldItalic";

		// Token: 0x04000700 RID: 1792
		public const string ZAPFDINGBATS = "ZapfDingbats";

		// Token: 0x04000701 RID: 1793
		public const int ASCENT = 1;

		// Token: 0x04000702 RID: 1794
		public const int CAPHEIGHT = 2;

		// Token: 0x04000703 RID: 1795
		public const int DESCENT = 3;

		// Token: 0x04000704 RID: 1796
		public const int ITALICANGLE = 4;

		// Token: 0x04000705 RID: 1797
		public const int BBOXLLX = 5;

		// Token: 0x04000706 RID: 1798
		public const int BBOXLLY = 6;

		// Token: 0x04000707 RID: 1799
		public const int BBOXURX = 7;

		// Token: 0x04000708 RID: 1800
		public const int BBOXURY = 8;

		// Token: 0x04000709 RID: 1801
		public const int AWT_ASCENT = 9;

		// Token: 0x0400070A RID: 1802
		public const int AWT_DESCENT = 10;

		// Token: 0x0400070B RID: 1803
		public const int AWT_LEADING = 11;

		// Token: 0x0400070C RID: 1804
		public const int AWT_MAXADVANCE = 12;

		// Token: 0x0400070D RID: 1805
		public const int UNDERLINE_POSITION = 13;

		// Token: 0x0400070E RID: 1806
		public const int UNDERLINE_THICKNESS = 14;

		// Token: 0x0400070F RID: 1807
		public const int STRIKETHROUGH_POSITION = 15;

		// Token: 0x04000710 RID: 1808
		public const int STRIKETHROUGH_THICKNESS = 16;

		// Token: 0x04000711 RID: 1809
		public const int SUBSCRIPT_SIZE = 17;

		// Token: 0x04000712 RID: 1810
		public const int SUBSCRIPT_OFFSET = 18;

		// Token: 0x04000713 RID: 1811
		public const int SUPERSCRIPT_SIZE = 19;

		// Token: 0x04000714 RID: 1812
		public const int SUPERSCRIPT_OFFSET = 20;

		// Token: 0x04000715 RID: 1813
		public const int WEIGHT_CLASS = 21;

		// Token: 0x04000716 RID: 1814
		public const int WIDTH_CLASS = 22;

		// Token: 0x04000717 RID: 1815
		public const int FONT_TYPE_T1 = 0;

		// Token: 0x04000718 RID: 1816
		public const int FONT_TYPE_TT = 1;

		// Token: 0x04000719 RID: 1817
		public const int FONT_TYPE_CJK = 2;

		// Token: 0x0400071A RID: 1818
		public const int FONT_TYPE_TTUNI = 3;

		// Token: 0x0400071B RID: 1819
		public const int FONT_TYPE_DOCUMENT = 4;

		// Token: 0x0400071C RID: 1820
		public const int FONT_TYPE_T3 = 5;

		// Token: 0x0400071D RID: 1821
		public const string IDENTITY_H = "Identity-H";

		// Token: 0x0400071E RID: 1822
		public const string IDENTITY_V = "Identity-V";

		// Token: 0x0400071F RID: 1823
		public const string CP1250 = "Cp1250";

		// Token: 0x04000720 RID: 1824
		public const string CP1252 = "Cp1252";

		// Token: 0x04000721 RID: 1825
		public const string CP1257 = "Cp1257";

		// Token: 0x04000722 RID: 1826
		public const string WINANSI = "Cp1252";

		// Token: 0x04000723 RID: 1827
		public const string MACROMAN = "MacRoman";

		// Token: 0x04000724 RID: 1828
		public const bool EMBEDDED = true;

		// Token: 0x04000725 RID: 1829
		public const bool NOT_EMBEDDED = false;

		// Token: 0x04000726 RID: 1830
		public const bool CACHED = true;

		// Token: 0x04000727 RID: 1831
		public const bool NOT_CACHED = false;

		// Token: 0x04000728 RID: 1832
		public const string RESOURCE_PATH = "iTextSharp.text.pdf.fonts.";

		// Token: 0x04000729 RID: 1833
		public const char CID_NEWLINE = '翿';

		// Token: 0x0400072A RID: 1834
		public const string notdef = ".notdef";

		// Token: 0x0400072B RID: 1835
		public static readonly int[] CHAR_RANGE_LATIN = new int[]
		{
			0,
			383,
			8192,
			8303,
			8352,
			8399,
			64256,
			64262
		};

		// Token: 0x0400072C RID: 1836
		public static readonly int[] CHAR_RANGE_ARABIC = new int[]
		{
			0,
			127,
			1536,
			1663,
			8352,
			8399,
			64336,
			64511,
			65136,
			65279
		};

		// Token: 0x0400072D RID: 1837
		public static readonly int[] CHAR_RANGE_HEBREW = new int[]
		{
			0,
			127,
			1424,
			1535,
			8352,
			8399,
			64285,
			64335
		};

		// Token: 0x0400072E RID: 1838
		public static readonly int[] CHAR_RANGE_CYRILLIC = new int[]
		{
			0,
			127,
			1024,
			1327,
			8192,
			8303,
			8352,
			8399
		};

		// Token: 0x0400072F RID: 1839
		protected List<int[]> subsetRanges;

		// Token: 0x04000730 RID: 1840
		internal int fontType;

		// Token: 0x04000731 RID: 1841
		protected int[] widths = new int[256];

		// Token: 0x04000732 RID: 1842
		protected string[] differences = new string[256];

		// Token: 0x04000733 RID: 1843
		protected char[] unicodeDifferences = new char[256];

		// Token: 0x04000734 RID: 1844
		protected int[][] charBBoxes = new int[256][];

		// Token: 0x04000735 RID: 1845
		protected string encoding;

		// Token: 0x04000736 RID: 1846
		protected bool embedded;

		// Token: 0x04000737 RID: 1847
		protected int compressionLevel = -1;

		// Token: 0x04000738 RID: 1848
		protected bool fontSpecific = true;

		// Token: 0x04000739 RID: 1849
		protected static Dictionary<string, BaseFont> fontCache = new Dictionary<string, BaseFont>();

		// Token: 0x0400073A RID: 1850
		protected static Dictionary<string, PdfName> BuiltinFonts14 = new Dictionary<string, PdfName>();

		// Token: 0x0400073B RID: 1851
		protected bool forceWidthsOutput;

		// Token: 0x0400073C RID: 1852
		protected bool directTextToByte;

		// Token: 0x0400073D RID: 1853
		protected bool subset = true;

		// Token: 0x0400073E RID: 1854
		protected bool fastWinansi;

		// Token: 0x0400073F RID: 1855
		protected IntHashtable specialMap;

		// Token: 0x04000740 RID: 1856
		protected internal static List<object> resourceSearch = new List<object>();

		// Token: 0x04000741 RID: 1857
		private static Random random = new Random();

		// Token: 0x020000E6 RID: 230
		internal class StreamFont : PdfStream
		{
			// Token: 0x060008A9 RID: 2217 RVA: 0x0002D184 File Offset: 0x0002C184
			internal StreamFont(byte[] contents, int[] lengths, int compressionLevel)
			{
				this.bytes = contents;
				base.Put(PdfName.LENGTH, new PdfNumber(this.bytes.Length));
				for (int i = 0; i < lengths.Length; i++)
				{
					base.Put(new PdfName("Length" + (i + 1)), new PdfNumber(lengths[i]));
				}
				base.FlateCompress(compressionLevel);
			}

			// Token: 0x060008AA RID: 2218 RVA: 0x0002D1F0 File Offset: 0x0002C1F0
			internal StreamFont(byte[] contents, string subType, int compressionLevel)
			{
				this.bytes = contents;
				base.Put(PdfName.LENGTH, new PdfNumber(this.bytes.Length));
				if (subType != null)
				{
					base.Put(PdfName.SUBTYPE, new PdfName(subType));
				}
				base.FlateCompress(compressionLevel);
			}
		}
	}
}

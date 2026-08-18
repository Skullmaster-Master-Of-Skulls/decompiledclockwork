using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.util;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000596 RID: 1430
	internal class CJKFont : BaseFont
	{
		// Token: 0x060030DE RID: 12510 RVA: 0x0012DA98 File Offset: 0x0012CA98
		private static void LoadProperties()
		{
			if (CJKFont.propertiesLoaded)
			{
				return;
			}
			lock (CJKFont.allFonts)
			{
				if (!CJKFont.propertiesLoaded)
				{
					try
					{
						Stream resourceStream = BaseFont.GetResourceStream("iTextSharp.text.pdf.fonts.cjkfonts.properties");
						CJKFont.cjkFonts.Load(resourceStream);
						resourceStream.Close();
						resourceStream = BaseFont.GetResourceStream("iTextSharp.text.pdf.fonts.cjkencodings.properties");
						CJKFont.cjkEncodings.Load(resourceStream);
						resourceStream.Close();
					}
					catch
					{
						CJKFont.cjkFonts = new Properties();
						CJKFont.cjkEncodings = new Properties();
					}
					CJKFont.propertiesLoaded = true;
				}
			}
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x0012DB40 File Offset: 0x0012CB40
		internal CJKFont(string fontName, string enc, bool emb)
		{
			CJKFont.LoadProperties();
			base.FontType = 2;
			string baseName = BaseFont.GetBaseName(fontName);
			if (!CJKFont.IsCJKFont(baseName, enc))
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("font.1.with.2.encoding.is.not.a.cjk.font", fontName, enc));
			}
			if (baseName.Length < fontName.Length)
			{
				this.style = fontName.Substring(baseName.Length);
				fontName = baseName;
			}
			this.fontName = fontName;
			this.encoding = "UNICODEBIGUNMARKED";
			this.vertical = enc.EndsWith("V");
			this.CMap = enc;
			if (enc.StartsWith("Identity-"))
			{
				this.cidDirect = true;
				string text = CJKFont.cjkFonts[fontName];
				text = text.Substring(0, text.IndexOf('_'));
				char[] array;
				lock (CJKFont.allCMaps)
				{
					CJKFont.allCMaps.TryGetValue(text, out array);
				}
				if (array == null)
				{
					array = CJKFont.ReadCMap(text);
					if (array == null)
					{
						throw new DocumentException(MessageLocalization.GetComposedMessage("the.cmap.1.does.not.exist.as.a.resource", text));
					}
					array[32767] = '\n';
					lock (CJKFont.allCMaps)
					{
						CJKFont.allCMaps[text] = array;
					}
				}
				this.translationMap = array;
			}
			else
			{
				char[] array2;
				lock (CJKFont.allCMaps)
				{
					CJKFont.allCMaps.TryGetValue(enc, out array2);
				}
				if (array2 == null)
				{
					string text2 = CJKFont.cjkEncodings[enc];
					if (text2 == null)
					{
						throw new DocumentException(MessageLocalization.GetComposedMessage("the.resource.cjkencodings.properties.does.not.contain.the.encoding.1", enc));
					}
					StringTokenizer stringTokenizer = new StringTokenizer(text2);
					string text3 = stringTokenizer.NextToken();
					lock (CJKFont.allCMaps)
					{
						CJKFont.allCMaps.TryGetValue(text3, out array2);
					}
					if (array2 == null)
					{
						array2 = CJKFont.ReadCMap(text3);
						lock (CJKFont.allCMaps)
						{
							CJKFont.allCMaps[text3] = array2;
						}
					}
					if (stringTokenizer.HasMoreTokens())
					{
						string name = stringTokenizer.NextToken();
						char[] array3 = CJKFont.ReadCMap(name);
						for (int i = 0; i < 65536; i++)
						{
							if (array3[i] == '\0')
							{
								array3[i] = array2[i];
							}
						}
						lock (CJKFont.allCMaps)
						{
							CJKFont.allCMaps[enc] = array3;
						}
						array2 = array3;
					}
				}
				this.translationMap = array2;
			}
			lock (CJKFont.allFonts)
			{
				CJKFont.allFonts.TryGetValue(fontName, out this.fontDesc);
			}
			CJKFont.allFonts.TryGetValue(fontName, out this.fontDesc);
			if (this.fontDesc == null)
			{
				this.fontDesc = CJKFont.ReadFontProperties(fontName);
				lock (CJKFont.allFonts)
				{
					CJKFont.allFonts[fontName] = this.fontDesc;
				}
			}
			this.hMetrics = (IntHashtable)this.fontDesc["W"];
			this.vMetrics = (IntHashtable)this.fontDesc["W2"];
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x0012DEBC File Offset: 0x0012CEBC
		public static bool IsCJKFont(string fontName, string enc)
		{
			CJKFont.LoadProperties();
			string text = CJKFont.cjkFonts[fontName];
			return text != null && (enc.Equals("Identity-H") || enc.Equals("Identity-V") || text.IndexOf("_" + enc + "_") >= 0);
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x0012DF18 File Offset: 0x0012CF18
		public override int GetWidth(int char1)
		{
			int num = char1;
			if (!this.cidDirect)
			{
				num = (int)this.translationMap[num];
			}
			int num2;
			if (this.vertical)
			{
				num2 = this.vMetrics[num];
			}
			else
			{
				num2 = this.hMetrics[num];
			}
			if (num2 > 0)
			{
				return num2;
			}
			return 1000;
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x0012DF68 File Offset: 0x0012CF68
		public override int GetWidth(string text)
		{
			int num = 0;
			foreach (int num2 in text)
			{
				if (!this.cidDirect)
				{
					num2 = (int)this.translationMap[num2];
				}
				int num3;
				if (this.vertical)
				{
					num3 = this.vMetrics[num2];
				}
				else
				{
					num3 = this.hMetrics[num2];
				}
				if (num3 > 0)
				{
					num += num3;
				}
				else
				{
					num += 1000;
				}
			}
			return num;
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x0012DFD8 File Offset: 0x0012CFD8
		internal override int GetRawWidth(int c, string name)
		{
			return 0;
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x0012DFDB File Offset: 0x0012CFDB
		public override int GetKerning(int char1, int char2)
		{
			return 0;
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x0012DFE0 File Offset: 0x0012CFE0
		private PdfDictionary GetFontDescriptor()
		{
			PdfDictionary pdfDictionary = new PdfDictionary(PdfName.FONTDESCRIPTOR);
			pdfDictionary.Put(PdfName.ASCENT, new PdfLiteral((string)this.fontDesc["Ascent"]));
			pdfDictionary.Put(PdfName.CAPHEIGHT, new PdfLiteral((string)this.fontDesc["CapHeight"]));
			pdfDictionary.Put(PdfName.DESCENT, new PdfLiteral((string)this.fontDesc["Descent"]));
			pdfDictionary.Put(PdfName.FLAGS, new PdfLiteral((string)this.fontDesc["Flags"]));
			pdfDictionary.Put(PdfName.FONTBBOX, new PdfLiteral((string)this.fontDesc["FontBBox"]));
			pdfDictionary.Put(PdfName.FONTNAME, new PdfName(this.fontName + this.style));
			pdfDictionary.Put(PdfName.ITALICANGLE, new PdfLiteral((string)this.fontDesc["ItalicAngle"]));
			pdfDictionary.Put(PdfName.STEMV, new PdfLiteral((string)this.fontDesc["StemV"]));
			PdfDictionary pdfDictionary2 = new PdfDictionary();
			pdfDictionary2.Put(PdfName.PANOSE, new PdfString((string)this.fontDesc["Panose"], null));
			pdfDictionary.Put(PdfName.STYLE, pdfDictionary2);
			return pdfDictionary;
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x0012E158 File Offset: 0x0012D158
		private PdfDictionary GetCIDFont(PdfIndirectReference fontDescriptor, IntHashtable cjkTag)
		{
			PdfDictionary pdfDictionary = new PdfDictionary(PdfName.FONT);
			pdfDictionary.Put(PdfName.SUBTYPE, PdfName.CIDFONTTYPE0);
			pdfDictionary.Put(PdfName.BASEFONT, new PdfName(this.fontName + this.style));
			pdfDictionary.Put(PdfName.FONTDESCRIPTOR, fontDescriptor);
			int[] keys = cjkTag.ToOrderedKeys();
			string text = CJKFont.ConvertToHCIDMetrics(keys, this.hMetrics);
			if (text != null)
			{
				pdfDictionary.Put(PdfName.W, new PdfLiteral(text));
			}
			if (this.vertical)
			{
				text = CJKFont.ConvertToVCIDMetrics(keys, this.vMetrics, this.hMetrics);
				if (text != null)
				{
					pdfDictionary.Put(PdfName.W2, new PdfLiteral(text));
				}
			}
			else
			{
				pdfDictionary.Put(PdfName.DW, new PdfNumber(1000));
			}
			PdfDictionary pdfDictionary2 = new PdfDictionary();
			pdfDictionary2.Put(PdfName.REGISTRY, new PdfString((string)this.fontDesc["Registry"], null));
			pdfDictionary2.Put(PdfName.ORDERING, new PdfString((string)this.fontDesc["Ordering"], null));
			pdfDictionary2.Put(PdfName.SUPPLEMENT, new PdfLiteral((string)this.fontDesc["Supplement"]));
			pdfDictionary.Put(PdfName.CIDSYSTEMINFO, pdfDictionary2);
			return pdfDictionary;
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x0012E2A0 File Offset: 0x0012D2A0
		private PdfDictionary GetFontBaseType(PdfIndirectReference CIDFont)
		{
			PdfDictionary pdfDictionary = new PdfDictionary(PdfName.FONT);
			pdfDictionary.Put(PdfName.SUBTYPE, PdfName.TYPE0);
			string text = this.fontName;
			if (this.style.Length > 0)
			{
				text = text + "-" + this.style.Substring(1);
			}
			text = text + "-" + this.CMap;
			pdfDictionary.Put(PdfName.BASEFONT, new PdfName(text));
			pdfDictionary.Put(PdfName.ENCODING, new PdfName(this.CMap));
			pdfDictionary.Put(PdfName.DESCENDANTFONTS, new PdfArray(CIDFont));
			return pdfDictionary;
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x0012E340 File Offset: 0x0012D340
		internal override void WriteFont(PdfWriter writer, PdfIndirectReference piref, object[] parms)
		{
			IntHashtable cjkTag = (IntHashtable)parms[0];
			PdfIndirectReference pdfIndirectReference = null;
			PdfObject pdfObject = this.GetFontDescriptor();
			if (pdfObject != null)
			{
				PdfIndirectObject pdfIndirectObject = writer.AddToBody(pdfObject);
				pdfIndirectReference = pdfIndirectObject.IndirectReference;
			}
			pdfObject = this.GetCIDFont(pdfIndirectReference, cjkTag);
			if (pdfObject != null)
			{
				PdfIndirectObject pdfIndirectObject = writer.AddToBody(pdfObject);
				pdfIndirectReference = pdfIndirectObject.IndirectReference;
			}
			pdfObject = this.GetFontBaseType(pdfIndirectReference);
			writer.AddToBody(pdfObject, piref);
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x0012E3A1 File Offset: 0x0012D3A1
		public override PdfStream GetFullFontStream()
		{
			return null;
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x0012E3A4 File Offset: 0x0012D3A4
		private float GetDescNumber(string name)
		{
			return (float)int.Parse((string)this.fontDesc[name]);
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x0012E3C0 File Offset: 0x0012D3C0
		private float GetBBox(int idx)
		{
			string str = (string)this.fontDesc["FontBBox"];
			StringTokenizer stringTokenizer = new StringTokenizer(str, " []\r\n\t\f");
			string s = stringTokenizer.NextToken();
			for (int i = 0; i < idx; i++)
			{
				s = stringTokenizer.NextToken();
			}
			return (float)int.Parse(s);
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x0012E410 File Offset: 0x0012D410
		public override float GetFontDescriptor(int key, float fontSize)
		{
			switch (key)
			{
			case 1:
			case 9:
				return this.GetDescNumber("Ascent") * fontSize / 1000f;
			case 2:
				return this.GetDescNumber("CapHeight") * fontSize / 1000f;
			case 3:
			case 10:
				return this.GetDescNumber("Descent") * fontSize / 1000f;
			case 4:
				return this.GetDescNumber("ItalicAngle");
			case 5:
				return fontSize * this.GetBBox(0) / 1000f;
			case 6:
				return fontSize * this.GetBBox(1) / 1000f;
			case 7:
				return fontSize * this.GetBBox(2) / 1000f;
			case 8:
				return fontSize * this.GetBBox(3) / 1000f;
			case 11:
				return 0f;
			case 12:
				return fontSize * (this.GetBBox(2) - this.GetBBox(0)) / 1000f;
			default:
				return 0f;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x060030ED RID: 12525 RVA: 0x0012E507 File Offset: 0x0012D507
		// (set) Token: 0x060030EE RID: 12526 RVA: 0x0012E50F File Offset: 0x0012D50F
		public override string PostscriptFontName
		{
			get
			{
				return this.fontName;
			}
			set
			{
				this.fontName = value;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x060030EF RID: 12527 RVA: 0x0012E518 File Offset: 0x0012D518
		public override string[][] FullFontName
		{
			get
			{
				return new string[][]
				{
					new string[]
					{
						"",
						"",
						"",
						this.fontName
					}
				};
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x0012E55C File Offset: 0x0012D55C
		public override string[][] AllNameEntries
		{
			get
			{
				return new string[][]
				{
					new string[]
					{
						"4",
						"",
						"",
						"",
						this.fontName
					}
				};
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060030F1 RID: 12529 RVA: 0x0012E5A5 File Offset: 0x0012D5A5
		public override string[][] FamilyFontName
		{
			get
			{
				return this.FullFontName;
			}
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x0012E5B0 File Offset: 0x0012D5B0
		internal static char[] ReadCMap(string name)
		{
			Stream stream = null;
			try
			{
				name += ".cmap";
				stream = BaseFont.GetResourceStream("iTextSharp.text.pdf.fonts." + name);
				char[] array = new char[65536];
				for (int i = 0; i < 65536; i++)
				{
					array[i] = (char)((stream.ReadByte() << 8) + stream.ReadByte());
				}
				return array;
			}
			catch
			{
			}
			finally
			{
				try
				{
					stream.Close();
				}
				catch
				{
				}
			}
			return null;
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x0012E64C File Offset: 0x0012D64C
		internal static IntHashtable CreateMetric(string s)
		{
			IntHashtable intHashtable = new IntHashtable();
			StringTokenizer stringTokenizer = new StringTokenizer(s);
			while (stringTokenizer.HasMoreTokens())
			{
				int key = int.Parse(stringTokenizer.NextToken());
				intHashtable[key] = int.Parse(stringTokenizer.NextToken());
			}
			return intHashtable;
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x0012E690 File Offset: 0x0012D690
		internal static string ConvertToHCIDMetrics(int[] keys, IntHashtable h)
		{
			if (keys.Length == 0)
			{
				return null;
			}
			int num = 0;
			int num2 = 0;
			int i;
			for (i = 0; i < keys.Length; i++)
			{
				num = keys[i];
				num2 = h[num];
				if (num2 != 0)
				{
					i++;
					break;
				}
			}
			if (num2 == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			stringBuilder.Append(num);
			int num3 = 0;
			for (int j = i; j < keys.Length; j++)
			{
				int num4 = keys[j];
				int num5 = h[num4];
				if (num5 != 0)
				{
					switch (num3)
					{
					case 0:
						if (num4 == num + 1 && num5 == num2)
						{
							num3 = 2;
						}
						else if (num4 == num + 1)
						{
							num3 = 1;
							stringBuilder.Append('[').Append(num2);
						}
						else
						{
							stringBuilder.Append('[').Append(num2).Append(']').Append(num4);
						}
						break;
					case 1:
						if (num4 == num + 1 && num5 == num2)
						{
							num3 = 2;
							stringBuilder.Append(']').Append(num);
						}
						else if (num4 == num + 1)
						{
							stringBuilder.Append(' ').Append(num2);
						}
						else
						{
							num3 = 0;
							stringBuilder.Append(' ').Append(num2).Append(']').Append(num4);
						}
						break;
					case 2:
						if (num4 != num + 1 || num5 != num2)
						{
							stringBuilder.Append(' ').Append(num).Append(' ').Append(num2).Append(' ').Append(num4);
							num3 = 0;
						}
						break;
					}
					num2 = num5;
					num = num4;
				}
			}
			switch (num3)
			{
			case 0:
				stringBuilder.Append('[').Append(num2).Append("]]");
				break;
			case 1:
				stringBuilder.Append(' ').Append(num2).Append("]]");
				break;
			case 2:
				stringBuilder.Append(' ').Append(num).Append(' ').Append(num2).Append(']');
				break;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x0012E89C File Offset: 0x0012D89C
		internal static string ConvertToVCIDMetrics(int[] keys, IntHashtable v, IntHashtable h)
		{
			if (keys.Length == 0)
			{
				return null;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int i;
			for (i = 0; i < keys.Length; i++)
			{
				num = keys[i];
				num2 = v[num];
				if (num2 != 0)
				{
					i++;
					break;
				}
				num3 = h[num];
			}
			if (num2 == 0)
			{
				return null;
			}
			if (num3 == 0)
			{
				num3 = 1000;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			stringBuilder.Append(num);
			int num4 = 0;
			for (int j = i; j < keys.Length; j++)
			{
				int num5 = keys[j];
				int num6 = v[num5];
				if (num6 != 0)
				{
					int num7 = h[num];
					if (num7 == 0)
					{
						num7 = 1000;
					}
					switch (num4)
					{
					case 0:
						if (num5 == num + 1 && num6 == num2 && num7 == num3)
						{
							num4 = 2;
						}
						else
						{
							stringBuilder.Append(' ').Append(num).Append(' ').Append(-num2).Append(' ').Append(num3 / 2).Append(' ').Append(880).Append(' ').Append(num5);
						}
						break;
					case 2:
						if (num5 != num + 1 || num6 != num2 || num7 != num3)
						{
							stringBuilder.Append(' ').Append(num).Append(' ').Append(-num2).Append(' ').Append(num3 / 2).Append(' ').Append(880).Append(' ').Append(num5);
							num4 = 0;
						}
						break;
					}
					num2 = num6;
					num = num5;
					num3 = num7;
				}
			}
			stringBuilder.Append(' ').Append(num).Append(' ').Append(-num2).Append(' ').Append(num3 / 2).Append(' ').Append(880).Append(" ]");
			return stringBuilder.ToString();
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x0012EA8C File Offset: 0x0012DA8C
		internal static Dictionary<string, object> ReadFontProperties(string name)
		{
			try
			{
				name += ".properties";
				Stream resourceStream = BaseFont.GetResourceStream("iTextSharp.text.pdf.fonts." + name);
				Properties properties = new Properties();
				properties.Load(resourceStream);
				resourceStream.Close();
				IntHashtable value = CJKFont.CreateMetric(properties["W"]);
				properties.Remove("W");
				IntHashtable value2 = CJKFont.CreateMetric(properties["W2"]);
				properties.Remove("W2");
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				foreach (string key in properties.Keys)
				{
					dictionary[key] = properties[key];
				}
				dictionary["W"] = value;
				dictionary["W2"] = value2;
				return dictionary;
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x0012EB90 File Offset: 0x0012DB90
		public override int GetUnicodeEquivalent(int c)
		{
			if (this.cidDirect)
			{
				return (int)this.translationMap[c];
			}
			return c;
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x0012EBA4 File Offset: 0x0012DBA4
		public override int GetCidCode(int c)
		{
			if (this.cidDirect)
			{
				return c;
			}
			return (int)this.translationMap[c];
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x0012EBB8 File Offset: 0x0012DBB8
		public override bool HasKernPairs()
		{
			return false;
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x0012EBBB File Offset: 0x0012DBBB
		public override bool CharExists(int c)
		{
			return this.translationMap[c] != '\0';
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x0012EBCB File Offset: 0x0012DBCB
		public override bool SetCharAdvance(int c, int advance)
		{
			return false;
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x0012EBCE File Offset: 0x0012DBCE
		public override bool SetKerning(int char1, int char2, int kern)
		{
			return false;
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x0012EBD1 File Offset: 0x0012DBD1
		public override int[] GetCharBBox(int c)
		{
			return null;
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x0012EBD4 File Offset: 0x0012DBD4
		protected override int[] GetRawCharBBox(int c, string name)
		{
			return null;
		}

		// Token: 0x04002182 RID: 8578
		internal const string CJK_ENCODING = "UNICODEBIGUNMARKED";

		// Token: 0x04002183 RID: 8579
		private const int FIRST = 0;

		// Token: 0x04002184 RID: 8580
		private const int BRACKET = 1;

		// Token: 0x04002185 RID: 8581
		private const int SERIAL = 2;

		// Token: 0x04002186 RID: 8582
		private const int V1Y = 880;

		// Token: 0x04002187 RID: 8583
		internal static Properties cjkFonts = new Properties();

		// Token: 0x04002188 RID: 8584
		internal static Properties cjkEncodings = new Properties();

		// Token: 0x04002189 RID: 8585
		internal static Dictionary<string, char[]> allCMaps = new Dictionary<string, char[]>();

		// Token: 0x0400218A RID: 8586
		internal static Dictionary<string, Dictionary<string, object>> allFonts = new Dictionary<string, Dictionary<string, object>>();

		// Token: 0x0400218B RID: 8587
		private static bool propertiesLoaded = false;

		// Token: 0x0400218C RID: 8588
		private string fontName;

		// Token: 0x0400218D RID: 8589
		private string style = "";

		// Token: 0x0400218E RID: 8590
		private string CMap;

		// Token: 0x0400218F RID: 8591
		private bool cidDirect;

		// Token: 0x04002190 RID: 8592
		private char[] translationMap;

		// Token: 0x04002191 RID: 8593
		private IntHashtable vMetrics;

		// Token: 0x04002192 RID: 8594
		private IntHashtable hMetrics;

		// Token: 0x04002193 RID: 8595
		private Dictionary<string, object> fontDesc;

		// Token: 0x04002194 RID: 8596
		private bool vertical;
	}
}

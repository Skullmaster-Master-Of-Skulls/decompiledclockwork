using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000588 RID: 1416
	internal class TrueTypeFontUnicode : TrueTypeFont, IComparer<int[]>
	{
		// Token: 0x06003021 RID: 12321 RVA: 0x00129844 File Offset: 0x00128844
		internal TrueTypeFontUnicode(string ttFile, string enc, bool emb, byte[] ttfAfm, bool forceRead)
		{
			string baseName = iTextSharp.text.pdf.BaseFont.GetBaseName(ttFile);
			string ttcname = TrueTypeFont.GetTTCName(baseName);
			if (baseName.Length < ttFile.Length)
			{
				this.style = ttFile.Substring(baseName.Length);
			}
			this.encoding = enc;
			this.embedded = emb;
			this.fileName = ttcname;
			this.ttcIndex = "";
			if (ttcname.Length < baseName.Length)
			{
				this.ttcIndex = baseName.Substring(ttcname.Length + 1);
			}
			base.FontType = 3;
			if ((!this.fileName.ToLower(CultureInfo.InvariantCulture).EndsWith(".ttf") && !this.fileName.ToLower(CultureInfo.InvariantCulture).EndsWith(".otf") && !this.fileName.ToLower(CultureInfo.InvariantCulture).EndsWith(".ttc")) || (!enc.Equals("Identity-H") && !enc.Equals("Identity-V")) || !emb)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("1.2.is.not.a.ttf.font.file", this.fileName, this.style));
			}
			base.Process(ttfAfm, forceRead);
			if (this.os_2.fsType == 2)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("1.cannot.be.embedded.due.to.licensing.restrictions", this.fileName + this.style));
			}
			if ((this.cmap31 == null && !this.fontSpecific) || (this.cmap10 == null && this.fontSpecific))
			{
				this.directTextToByte = true;
			}
			if (this.fontSpecific)
			{
				this.fontSpecific = false;
				string encoding = this.encoding;
				this.encoding = "";
				base.CreateEncoding();
				this.encoding = encoding;
				this.fontSpecific = true;
			}
			this.vertical = enc.EndsWith("V");
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x00129A0C File Offset: 0x00128A0C
		public override int GetWidth(int char1)
		{
			if (this.vertical)
			{
				return 1000;
			}
			if (!this.fontSpecific)
			{
				return this.GetRawWidth(char1, this.encoding);
			}
			if ((char1 & 65280) == 0 || (char1 & 65280) == 61440)
			{
				return this.GetRawWidth(char1 & 255, null);
			}
			return 0;
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x00129A64 File Offset: 0x00128A64
		public override int GetWidth(string text)
		{
			if (this.vertical)
			{
				return text.Length * 1000;
			}
			int num = 0;
			if (this.fontSpecific)
			{
				char[] array = text.ToCharArray();
				int num2 = array.Length;
				for (int i = 0; i < num2; i++)
				{
					char c = array[i];
					if ((c & '＀') == '\0' || (c & '＀') == '')
					{
						num += this.GetRawWidth((int)(c & 'ÿ'), null);
					}
				}
			}
			else
			{
				int length = text.Length;
				for (int j = 0; j < length; j++)
				{
					if (Utilities.IsSurrogatePair(text, j))
					{
						num += this.GetRawWidth(Utilities.ConvertToUtf32(text, j), this.encoding);
						j++;
					}
					else
					{
						num += this.GetRawWidth((int)text[j], this.encoding);
					}
				}
			}
			return num;
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x00129B34 File Offset: 0x00128B34
		private PdfStream GetToUnicode(object[] metrics)
		{
			if (metrics.Length == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder("/CIDInit /ProcSet findresource begin\n12 dict begin\nbegincmap\n/CIDSystemInfo\n<< /Registry (TTX+0)\n/Ordering (T42UV)\n/Supplement 0\n>> def\n/CMapName /TTX+0 def\n/CMapType 2 def\n1 begincodespacerange\n<0000><FFFF>\nendcodespacerange\n");
			int num = 0;
			for (int i = 0; i < metrics.Length; i++)
			{
				if (num == 0)
				{
					if (i != 0)
					{
						stringBuilder.Append("endbfrange\n");
					}
					num = Math.Min(100, metrics.Length - i);
					stringBuilder.Append(num).Append(" beginbfrange\n");
				}
				num--;
				int[] array = (int[])metrics[i];
				string value = TrueTypeFontUnicode.ToHex(array[0]);
				stringBuilder.Append(value).Append(value).Append(TrueTypeFontUnicode.ToHex(array[2])).Append('\n');
			}
			stringBuilder.Append("endbfrange\nendcmap\nCMapName currentdict /CMap defineresource pop\nend end\n");
			string text = stringBuilder.ToString();
			PdfStream pdfStream = new PdfStream(PdfEncodings.ConvertToBytes(text, null));
			pdfStream.FlateCompress(this.compressionLevel);
			return pdfStream;
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x00129C04 File Offset: 0x00128C04
		internal static string ToHex(int n)
		{
			if (n < 65536)
			{
				return "<" + Convert.ToString(n, 16).PadLeft(4, '0') + ">";
			}
			n -= 65536;
			int value = n / 1024 + 55296;
			int value2 = n % 1024 + 56320;
			return "[<" + Convert.ToString(value, 16).PadLeft(4, '0') + Convert.ToString(value2, 16).PadLeft(4, '0') + ">]";
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x00129C90 File Offset: 0x00128C90
		private PdfDictionary GetCIDFontType2(PdfIndirectReference fontDescriptor, string subsetPrefix, object[] metrics)
		{
			PdfDictionary pdfDictionary = new PdfDictionary(PdfName.FONT);
			if (this.cff)
			{
				pdfDictionary.Put(PdfName.SUBTYPE, PdfName.CIDFONTTYPE0);
				pdfDictionary.Put(PdfName.BASEFONT, new PdfName(subsetPrefix + this.fontName + "-" + this.encoding));
			}
			else
			{
				pdfDictionary.Put(PdfName.SUBTYPE, PdfName.CIDFONTTYPE2);
				pdfDictionary.Put(PdfName.BASEFONT, new PdfName(subsetPrefix + this.fontName));
			}
			pdfDictionary.Put(PdfName.FONTDESCRIPTOR, fontDescriptor);
			if (!this.cff)
			{
				pdfDictionary.Put(PdfName.CIDTOGIDMAP, PdfName.IDENTITY);
			}
			PdfDictionary pdfDictionary2 = new PdfDictionary();
			pdfDictionary2.Put(PdfName.REGISTRY, new PdfString("Adobe"));
			pdfDictionary2.Put(PdfName.ORDERING, new PdfString("Identity"));
			pdfDictionary2.Put(PdfName.SUPPLEMENT, new PdfNumber(0));
			pdfDictionary.Put(PdfName.CIDSYSTEMINFO, pdfDictionary2);
			if (!this.vertical)
			{
				pdfDictionary.Put(PdfName.DW, new PdfNumber(1000));
				StringBuilder stringBuilder = new StringBuilder("[");
				int num = -10;
				bool flag = true;
				foreach (int[] array in metrics)
				{
					if (array[1] != 1000)
					{
						int num2 = array[0];
						if (num2 == num + 1)
						{
							stringBuilder.Append(' ').Append(array[1]);
						}
						else
						{
							if (!flag)
							{
								stringBuilder.Append(']');
							}
							flag = false;
							stringBuilder.Append(num2).Append('[').Append(array[1]);
						}
						num = num2;
					}
				}
				if (stringBuilder.Length > 1)
				{
					stringBuilder.Append("]]");
					pdfDictionary.Put(PdfName.W, new PdfLiteral(stringBuilder.ToString()));
				}
			}
			return pdfDictionary;
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x00129E58 File Offset: 0x00128E58
		private PdfDictionary GetFontBaseType(PdfIndirectReference descendant, string subsetPrefix, PdfIndirectReference toUnicode)
		{
			PdfDictionary pdfDictionary = new PdfDictionary(PdfName.FONT);
			pdfDictionary.Put(PdfName.SUBTYPE, PdfName.TYPE0);
			if (this.cff)
			{
				pdfDictionary.Put(PdfName.BASEFONT, new PdfName(subsetPrefix + this.fontName + "-" + this.encoding));
			}
			else
			{
				pdfDictionary.Put(PdfName.BASEFONT, new PdfName(subsetPrefix + this.fontName));
			}
			pdfDictionary.Put(PdfName.ENCODING, new PdfName(this.encoding));
			pdfDictionary.Put(PdfName.DESCENDANTFONTS, new PdfArray(descendant));
			if (toUnicode != null)
			{
				pdfDictionary.Put(PdfName.TOUNICODE, toUnicode);
			}
			return pdfDictionary;
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x00129F04 File Offset: 0x00128F04
		public int Compare(int[] o1, int[] o2)
		{
			int num = o1[0];
			int num2 = o2[0];
			if (num < num2)
			{
				return -1;
			}
			if (num == num2)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x00129F28 File Offset: 0x00128F28
		internal override void WriteFont(PdfWriter writer, PdfIndirectReference piref, object[] parms)
		{
			Dictionary<int, int[]> dictionary = (Dictionary<int, int[]>)parms[0];
			base.AddRangeUni(dictionary, true, this.subset);
			int[][] array = new int[dictionary.Count][];
			dictionary.Values.CopyTo(array, 0);
			Array.Sort<int[]>(array, this);
			PdfIndirectReference cidset = null;
			if (writer.PDFXConformance == 3 || writer.PDFXConformance == 4)
			{
				PdfStream pdfStream;
				if (array.Length == 0)
				{
					pdfStream = new PdfStream(new byte[]
					{
						128
					});
				}
				else
				{
					int num = array[array.Length - 1][0];
					byte[] array2 = new byte[num / 8 + 1];
					int length = array.GetLength(0);
					for (int i = 0; i < length; i++)
					{
						int num2 = array[i][0];
						byte[] array3 = array2;
						int num3 = num2 / 8;
						array3[num3] |= TrueTypeFontUnicode.rotbits[num2 % 8];
					}
					pdfStream = new PdfStream(array2);
					pdfStream.FlateCompress(this.compressionLevel);
				}
				cidset = writer.AddToBody(pdfStream).IndirectReference;
			}
			PdfObject pdfObject;
			PdfIndirectObject pdfIndirectObject;
			PdfIndirectReference indirectReference;
			if (this.cff)
			{
				byte[] array4 = base.ReadCffFont();
				if (this.subset || this.subsetRanges != null)
				{
					CFFFontSubset cfffontSubset = new CFFFontSubset(new RandomAccessFileOrArray(array4), dictionary);
					array4 = cfffontSubset.Process(cfffontSubset.GetNames()[0]);
				}
				pdfObject = new BaseFont.StreamFont(array4, "CIDFontType0C", this.compressionLevel);
				pdfIndirectObject = writer.AddToBody(pdfObject);
				indirectReference = pdfIndirectObject.IndirectReference;
			}
			else
			{
				byte[] array5;
				if (this.subset || this.directoryOffset != 0)
				{
					TrueTypeFontSubSet trueTypeFontSubSet = new TrueTypeFontSubSet(this.fileName, new RandomAccessFileOrArray(this.rf), dictionary, this.directoryOffset, false, false);
					array5 = trueTypeFontSubSet.Process();
				}
				else
				{
					array5 = base.GetFullFont();
				}
				int[] lengths = new int[]
				{
					array5.Length
				};
				pdfObject = new BaseFont.StreamFont(array5, lengths, this.compressionLevel);
				pdfIndirectObject = writer.AddToBody(pdfObject);
				indirectReference = pdfIndirectObject.IndirectReference;
			}
			string subsetPrefix = "";
			if (this.subset)
			{
				subsetPrefix = iTextSharp.text.pdf.BaseFont.CreateSubsetPrefix();
			}
			PdfDictionary fontDescriptor = base.GetFontDescriptor(indirectReference, subsetPrefix, cidset);
			pdfIndirectObject = writer.AddToBody(fontDescriptor);
			indirectReference = pdfIndirectObject.IndirectReference;
			pdfObject = this.GetCIDFontType2(indirectReference, subsetPrefix, array);
			pdfIndirectObject = writer.AddToBody(pdfObject);
			indirectReference = pdfIndirectObject.IndirectReference;
			pdfObject = this.GetToUnicode(array);
			PdfIndirectReference toUnicode = null;
			if (pdfObject != null)
			{
				pdfIndirectObject = writer.AddToBody(pdfObject);
				toUnicode = pdfIndirectObject.IndirectReference;
			}
			pdfObject = this.GetFontBaseType(indirectReference, subsetPrefix, toUnicode);
			writer.AddToBody(pdfObject, piref);
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x0012A193 File Offset: 0x00129193
		public override PdfStream GetFullFontStream()
		{
			if (this.cff)
			{
				return new BaseFont.StreamFont(base.ReadCffFont(), "CIDFontType0C", this.compressionLevel);
			}
			return base.GetFullFontStream();
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x0012A1BA File Offset: 0x001291BA
		internal override byte[] ConvertToBytes(string text)
		{
			return null;
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x0012A1BD File Offset: 0x001291BD
		internal override byte[] ConvertToBytes(int char1)
		{
			return null;
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x0012A1C0 File Offset: 0x001291C0
		public override int[] GetMetricsTT(int c)
		{
			if (this.cmapExt != null)
			{
				int[] result;
				this.cmapExt.TryGetValue(c, out result);
				return result;
			}
			Dictionary<int, int[]> dictionary;
			if (this.fontSpecific)
			{
				dictionary = this.cmap10;
			}
			else
			{
				dictionary = this.cmap31;
			}
			if (dictionary == null)
			{
				return null;
			}
			if (!this.fontSpecific)
			{
				int[] result;
				dictionary.TryGetValue(c, out result);
				return result;
			}
			if (((long)c & (long)((ulong)-256)) == 0L || ((long)c & (long)((ulong)-256)) == 61440L)
			{
				int[] result;
				dictionary.TryGetValue(c & 255, out result);
				return result;
			}
			return null;
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x0012A24A File Offset: 0x0012924A
		public override bool CharExists(int c)
		{
			return this.GetMetricsTT(c) != null;
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x0012A25C File Offset: 0x0012925C
		public override bool SetCharAdvance(int c, int advance)
		{
			int[] metricsTT = this.GetMetricsTT(c);
			if (metricsTT == null)
			{
				return false;
			}
			metricsTT[1] = advance;
			return true;
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x0012A27C File Offset: 0x0012927C
		public override int[] GetCharBBox(int c)
		{
			if (this.bboxes == null)
			{
				return null;
			}
			int[] metricsTT = this.GetMetricsTT(c);
			if (metricsTT == null)
			{
				return null;
			}
			return this.bboxes[metricsTT[0]];
		}

		// Token: 0x0400210F RID: 8463
		private bool vertical;

		// Token: 0x04002110 RID: 8464
		private static readonly byte[] rotbits = new byte[]
		{
			128,
			64,
			32,
			16,
			8,
			4,
			2,
			1
		};
	}
}

using System;
using System.Collections.Generic;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200015C RID: 348
	public class Type3Font : BaseFont
	{
		// Token: 0x06000D05 RID: 3333 RVA: 0x00047DB0 File Offset: 0x00046DB0
		public Type3Font(PdfWriter writer, char[] chars, bool colorized) : this(writer, colorized)
		{
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00047DBC File Offset: 0x00046DBC
		public Type3Font(PdfWriter writer, bool colorized)
		{
			this.writer = writer;
			this.colorized = colorized;
			this.fontType = 5;
			this.usedSlot = new bool[256];
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00047E20 File Offset: 0x00046E20
		public PdfContentByte DefineGlyph(char c, float wx, float llx, float lly, float urx, float ury)
		{
			if (c == '\0' || c > 'ÿ')
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.char.1.doesn.t.belong.in.this.type3.font", (int)c));
			}
			this.usedSlot[(int)c] = true;
			Type3Glyph type3Glyph;
			this.char2glyph.TryGetValue(c, out type3Glyph);
			if (type3Glyph != null)
			{
				return type3Glyph;
			}
			this.widths3[(int)c] = (int)wx;
			if (!this.colorized)
			{
				if (float.IsNaN(this.llx))
				{
					this.llx = llx;
					this.lly = lly;
					this.urx = urx;
					this.ury = ury;
				}
				else
				{
					this.llx = Math.Min(this.llx, llx);
					this.lly = Math.Min(this.lly, lly);
					this.urx = Math.Max(this.urx, urx);
					this.ury = Math.Max(this.ury, ury);
				}
			}
			type3Glyph = new Type3Glyph(this.writer, this.pageResources, wx, llx, lly, urx, ury, this.colorized);
			this.char2glyph[c] = type3Glyph;
			return type3Glyph;
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00047F28 File Offset: 0x00046F28
		public override string[][] FamilyFontName
		{
			get
			{
				return this.FullFontName;
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00047F30 File Offset: 0x00046F30
		public override float GetFontDescriptor(int key, float fontSize)
		{
			return 0f;
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00047F38 File Offset: 0x00046F38
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
						""
					}
				};
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00047F78 File Offset: 0x00046F78
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
						""
					}
				};
			}
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00047FC0 File Offset: 0x00046FC0
		public override int GetKerning(int char1, int char2)
		{
			return 0;
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00047FC3 File Offset: 0x00046FC3
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x00047FCA File Offset: 0x00046FCA
		public override string PostscriptFontName
		{
			get
			{
				return "";
			}
			set
			{
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00047FCC File Offset: 0x00046FCC
		protected override int[] GetRawCharBBox(int c, string name)
		{
			return null;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00047FCF File Offset: 0x00046FCF
		internal override int GetRawWidth(int c, string name)
		{
			return 0;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00047FD2 File Offset: 0x00046FD2
		public override bool HasKernPairs()
		{
			return false;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00047FD5 File Offset: 0x00046FD5
		public override bool SetKerning(int char1, int char2, int kern)
		{
			return false;
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00047FD8 File Offset: 0x00046FD8
		internal override void WriteFont(PdfWriter writer, PdfIndirectReference piRef, object[] oParams)
		{
			if (this.writer != writer)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("type3.font.used.with.the.wrong.pdfwriter"));
			}
			int num = 0;
			while (num < this.usedSlot.Length && !this.usedSlot[num])
			{
				num++;
			}
			if (num == this.usedSlot.Length)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("no.glyphs.defined.for.type3.font"));
			}
			int num2 = this.usedSlot.Length - 1;
			while (num2 >= num && !this.usedSlot[num2])
			{
				num2--;
			}
			int[] array = new int[num2 - num + 1];
			int[] array2 = new int[num2 - num + 1];
			int num3 = 0;
			int num4 = 0;
			int i = num;
			while (i <= num2)
			{
				if (this.usedSlot[i])
				{
					array2[num3++] = i;
					array[num4] = this.widths3[i];
				}
				i++;
				num4++;
			}
			PdfArray pdfArray = new PdfArray();
			PdfDictionary pdfDictionary = new PdfDictionary();
			int num5 = -1;
			for (int j = 0; j < num3; j++)
			{
				int num6 = array2[j];
				if (num6 > num5)
				{
					num5 = num6;
					pdfArray.Add(new PdfNumber(num5));
				}
				num5++;
				int num7 = array2[j];
				string text = GlyphList.UnicodeToName(num7);
				if (text == null)
				{
					text = "a" + num7;
				}
				PdfName pdfName = new PdfName(text);
				pdfArray.Add(pdfName);
				Type3Glyph type3Glyph;
				this.char2glyph.TryGetValue((char)num7, out type3Glyph);
				PdfStream pdfStream = new PdfStream(type3Glyph.ToPdf(null));
				pdfStream.FlateCompress(this.compressionLevel);
				PdfIndirectReference indirectReference = writer.AddToBody(pdfStream).IndirectReference;
				pdfDictionary.Put(pdfName, indirectReference);
			}
			PdfDictionary pdfDictionary2 = new PdfDictionary(PdfName.FONT);
			pdfDictionary2.Put(PdfName.SUBTYPE, PdfName.TYPE3);
			if (this.colorized)
			{
				pdfDictionary2.Put(PdfName.FONTBBOX, new PdfRectangle(0f, 0f, 0f, 0f));
			}
			else
			{
				pdfDictionary2.Put(PdfName.FONTBBOX, new PdfRectangle(this.llx, this.lly, this.urx, this.ury));
			}
			PdfDictionary pdfDictionary3 = pdfDictionary2;
			PdfName fontmatrix = PdfName.FONTMATRIX;
			float[] array3 = new float[6];
			array3[0] = 0.001f;
			array3[3] = 0.001f;
			pdfDictionary3.Put(fontmatrix, new PdfArray(array3));
			pdfDictionary2.Put(PdfName.CHARPROCS, writer.AddToBody(pdfDictionary).IndirectReference);
			PdfDictionary pdfDictionary4 = new PdfDictionary();
			pdfDictionary4.Put(PdfName.DIFFERENCES, pdfArray);
			pdfDictionary2.Put(PdfName.ENCODING, writer.AddToBody(pdfDictionary4).IndirectReference);
			pdfDictionary2.Put(PdfName.FIRSTCHAR, new PdfNumber(num));
			pdfDictionary2.Put(PdfName.LASTCHAR, new PdfNumber(num2));
			pdfDictionary2.Put(PdfName.WIDTHS, writer.AddToBody(new PdfArray(array)).IndirectReference);
			if (this.pageResources.HasResources())
			{
				pdfDictionary2.Put(PdfName.RESOURCES, writer.AddToBody(this.pageResources.Resources).IndirectReference);
			}
			writer.AddToBody(pdfDictionary2, piRef);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x000482DE File Offset: 0x000472DE
		public override PdfStream GetFullFontStream()
		{
			return null;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x000482E4 File Offset: 0x000472E4
		internal override byte[] ConvertToBytes(string text)
		{
			char[] array = text.ToCharArray();
			byte[] array2 = new byte[array.Length];
			int num = 0;
			foreach (char c in array)
			{
				if (this.CharExists((int)c))
				{
					array2[num++] = (byte)c;
				}
			}
			if (array2.Length == num)
			{
				return array2;
			}
			byte[] array3 = new byte[num];
			Array.Copy(array2, 0, array3, 0, num);
			return array3;
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00048348 File Offset: 0x00047348
		internal override byte[] ConvertToBytes(int char1)
		{
			if (this.CharExists(char1))
			{
				return new byte[]
				{
					(byte)char1
				};
			}
			return new byte[0];
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00048372 File Offset: 0x00047372
		public override int GetWidth(int char1)
		{
			if (!this.widths3.ContainsKey(char1))
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.char.1.is.not.defined.in.a.type3.font", char1));
			}
			return this.widths3[char1];
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x000483A4 File Offset: 0x000473A4
		public override int GetWidth(string text)
		{
			char[] array = text.ToCharArray();
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				num += this.GetWidth((int)array[i]);
			}
			return num;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x000483D5 File Offset: 0x000473D5
		public override int[] GetCharBBox(int c)
		{
			return null;
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x000483D8 File Offset: 0x000473D8
		public override bool CharExists(int c)
		{
			return c > 0 && c < 256 && this.usedSlot[c];
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x000483F0 File Offset: 0x000473F0
		public override bool SetCharAdvance(int c, int advance)
		{
			return false;
		}

		// Token: 0x040009C9 RID: 2505
		private bool[] usedSlot;

		// Token: 0x040009CA RID: 2506
		private IntHashtable widths3 = new IntHashtable();

		// Token: 0x040009CB RID: 2507
		private Dictionary<char, Type3Glyph> char2glyph = new Dictionary<char, Type3Glyph>();

		// Token: 0x040009CC RID: 2508
		private PdfWriter writer;

		// Token: 0x040009CD RID: 2509
		private float llx = float.NaN;

		// Token: 0x040009CE RID: 2510
		private float lly;

		// Token: 0x040009CF RID: 2511
		private float urx;

		// Token: 0x040009D0 RID: 2512
		private float ury;

		// Token: 0x040009D1 RID: 2513
		private PageResources pageResources = new PageResources();

		// Token: 0x040009D2 RID: 2514
		private bool colorized;
	}
}

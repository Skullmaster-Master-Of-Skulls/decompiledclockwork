using System;
using System.Collections;
using Telerik.Pdf;
using Telerik.Pdf.Gdi;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Render.Pdf.Fonts
{
	// Token: 0x02001698 RID: 5784
	internal class Type2CIDFont : CIDFont, IFontDescriptor, IDisposable
	{
		// Token: 0x0600DF42 RID: 57154 RVA: 0x00318F43 File Offset: 0x00317143
		public Type2CIDFont(FontProperties properties)
		{
			this.properties = properties;
			this.baseFontName = properties.FaceName.Replace(" ", "-");
			this.usedGlyphs = new SortedList();
			this.ObtainFontMetrics();
		}

		// Token: 0x0600DF43 RID: 57155 RVA: 0x00318F80 File Offset: 0x00317180
		private void ObtainFontMetrics()
		{
			this.dc = new GdiDeviceContent();
			GdiFont gdiFont = GdiFont.CreateDesignFont(this.properties.FaceName, this.properties.IsBold, this.properties.IsItalic, this.dc);
			this.unicodeRanges = new GdiUnicodeRanges(this.dc);
			this.metrics = gdiFont.GetMetrics(this.dc);
		}

		// Token: 0x0600DF44 RID: 57156 RVA: 0x00318FE8 File Offset: 0x003171E8
		~Type2CIDFont()
		{
			this.Dispose(false);
		}

		// Token: 0x1700446D RID: 17517
		// (get) Token: 0x0600DF45 RID: 57157 RVA: 0x00319018 File Offset: 0x00317218
		public override string CidBaseFont
		{
			get
			{
				return this.baseFontName;
			}
		}

		// Token: 0x1700446E RID: 17518
		// (get) Token: 0x0600DF46 RID: 57158 RVA: 0x00319020 File Offset: 0x00317220
		public override PdfWArray WArray
		{
			get
			{
				IList keyList = this.usedGlyphs.GetKeyList();
				int[] subsetWidthsArray = this.GetSubsetWidthsArray(keyList);
				PdfWArray pdfWArray = new PdfWArray((int)keyList[0]);
				pdfWArray.AddEntry(subsetWidthsArray);
				return pdfWArray;
			}
		}

		// Token: 0x1700446F RID: 17519
		// (get) Token: 0x0600DF47 RID: 57159 RVA: 0x0031905B File Offset: 0x0031725B
		public override IDictionary CMapEntries
		{
			get
			{
				return (IDictionary)this.usedGlyphs.Clone();
			}
		}

		// Token: 0x0600DF48 RID: 57160 RVA: 0x00319070 File Offset: 0x00317270
		private int[] GetSubsetWidthsArray(IList indicies)
		{
			int num = (int)indicies[0];
			int num2 = (int)indicies[indicies.Count - 1];
			int[] array = new int[num2 - num + 1];
			Array.Clear(array, 0, array.Length);
			int firstChar = this.metrics.FirstChar;
			foreach (object obj in this.usedGlyphs)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				char c = (char)dictionaryEntry.Value;
				int num3 = (int)dictionaryEntry.Key;
				array[num3 - num] = this.widths[num3];
			}
			return array;
		}

		// Token: 0x17004470 RID: 17520
		// (get) Token: 0x0600DF49 RID: 57161 RVA: 0x00319138 File Offset: 0x00317338
		public override PdfFontSubTypeEnum SubType
		{
			get
			{
				return PdfFontSubTypeEnum.CIDFontType2;
			}
		}

		// Token: 0x17004471 RID: 17521
		// (get) Token: 0x0600DF4A RID: 57162 RVA: 0x0031913B File Offset: 0x0031733B
		public override string FontName
		{
			get
			{
				return this.baseFontName;
			}
		}

		// Token: 0x17004472 RID: 17522
		// (get) Token: 0x0600DF4B RID: 57163 RVA: 0x00319143 File Offset: 0x00317343
		public override string Encoding
		{
			get
			{
				return "Identity-H";
			}
		}

		// Token: 0x17004473 RID: 17523
		// (get) Token: 0x0600DF4C RID: 57164 RVA: 0x0031914A File Offset: 0x0031734A
		public override IFontDescriptor Descriptor
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17004474 RID: 17524
		// (get) Token: 0x0600DF4D RID: 57165 RVA: 0x0031914D File Offset: 0x0031734D
		public override bool MultiByteFont
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600DF4E RID: 57166 RVA: 0x00319150 File Offset: 0x00317350
		public override int MapCharacter(char c)
		{
			int num = this.unicodeRanges.MapCharacter(c);
			this.AddGlyphToCharMapping(num, c);
			return num;
		}

		// Token: 0x0600DF4F RID: 57167 RVA: 0x00319173 File Offset: 0x00317373
		protected virtual void AddGlyphToCharMapping(int glyphIndex, char c)
		{
			if (!this.usedGlyphs.ContainsKey(glyphIndex))
			{
				this.usedGlyphs.Add(glyphIndex, c);
			}
		}

		// Token: 0x17004475 RID: 17525
		// (get) Token: 0x0600DF50 RID: 57168 RVA: 0x0031919F File Offset: 0x0031739F
		public override int Ascender
		{
			get
			{
				return this.metrics.Ascent;
			}
		}

		// Token: 0x17004476 RID: 17526
		// (get) Token: 0x0600DF51 RID: 57169 RVA: 0x003191AC File Offset: 0x003173AC
		public override int Descender
		{
			get
			{
				return this.metrics.Descent;
			}
		}

		// Token: 0x17004477 RID: 17527
		// (get) Token: 0x0600DF52 RID: 57170 RVA: 0x003191B9 File Offset: 0x003173B9
		public override int CapHeight
		{
			get
			{
				return this.metrics.CapHeight;
			}
		}

		// Token: 0x17004478 RID: 17528
		// (get) Token: 0x0600DF53 RID: 57171 RVA: 0x003191C6 File Offset: 0x003173C6
		public override int FirstChar
		{
			get
			{
				return this.metrics.FirstChar;
			}
		}

		// Token: 0x17004479 RID: 17529
		// (get) Token: 0x0600DF54 RID: 57172 RVA: 0x003191D3 File Offset: 0x003173D3
		public override int LastChar
		{
			get
			{
				return this.metrics.LastChar;
			}
		}

		// Token: 0x0600DF55 RID: 57173 RVA: 0x003191E0 File Offset: 0x003173E0
		public override int GetWidth(int charIndex)
		{
			this.EnsureWidthsArray();
			return this.widths[charIndex];
		}

		// Token: 0x1700447A RID: 17530
		// (get) Token: 0x0600DF56 RID: 57174 RVA: 0x003191F0 File Offset: 0x003173F0
		public override int[] Widths
		{
			get
			{
				this.EnsureWidthsArray();
				return this.widths;
			}
		}

		// Token: 0x0600DF57 RID: 57175 RVA: 0x003191FE File Offset: 0x003173FE
		protected void EnsureWidthsArray()
		{
			if (this.widths == null)
			{
				this.widths = this.metrics.GetWidths();
			}
		}

		// Token: 0x1700447B RID: 17531
		// (get) Token: 0x0600DF58 RID: 57176 RVA: 0x00319219 File Offset: 0x00317419
		public int Flags
		{
			get
			{
				return this.metrics.Flags;
			}
		}

		// Token: 0x1700447C RID: 17532
		// (get) Token: 0x0600DF59 RID: 57177 RVA: 0x00319226 File Offset: 0x00317426
		public int[] FontBBox
		{
			get
			{
				return this.metrics.BoundingBox;
			}
		}

		// Token: 0x1700447D RID: 17533
		// (get) Token: 0x0600DF5A RID: 57178 RVA: 0x00319233 File Offset: 0x00317433
		public int ItalicAngle
		{
			get
			{
				return this.metrics.ItalicAngle;
			}
		}

		// Token: 0x1700447E RID: 17534
		// (get) Token: 0x0600DF5B RID: 57179 RVA: 0x00319240 File Offset: 0x00317440
		public int StemV
		{
			get
			{
				return this.metrics.StemV;
			}
		}

		// Token: 0x1700447F RID: 17535
		// (get) Token: 0x0600DF5C RID: 57180 RVA: 0x0031924D File Offset: 0x0031744D
		public bool HasKerningInfo
		{
			get
			{
				if (this.kerning == null)
				{
					this.kerning = this.metrics.KerningPairs;
				}
				return this.kerning.Count != 0;
			}
		}

		// Token: 0x17004480 RID: 17536
		// (get) Token: 0x0600DF5D RID: 57181 RVA: 0x00319279 File Offset: 0x00317479
		public bool IsEmbeddable
		{
			get
			{
				return this.metrics.IsEmbeddable;
			}
		}

		// Token: 0x17004481 RID: 17537
		// (get) Token: 0x0600DF5E RID: 57182 RVA: 0x00319286 File Offset: 0x00317486
		public bool IsSubsettable
		{
			get
			{
				return this.metrics.IsSubsettable;
			}
		}

		// Token: 0x17004482 RID: 17538
		// (get) Token: 0x0600DF5F RID: 57183 RVA: 0x00319293 File Offset: 0x00317493
		public virtual byte[] FontData
		{
			get
			{
				return this.metrics.GetFontData();
			}
		}

		// Token: 0x17004483 RID: 17539
		// (get) Token: 0x0600DF60 RID: 57184 RVA: 0x003192A0 File Offset: 0x003174A0
		public GdiKerningPairs KerningInfo
		{
			get
			{
				if (this.kerning == null)
				{
					this.kerning = this.metrics.KerningPairs;
				}
				return this.kerning;
			}
		}

		// Token: 0x0600DF61 RID: 57185 RVA: 0x003192C1 File Offset: 0x003174C1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600DF62 RID: 57186 RVA: 0x003192D0 File Offset: 0x003174D0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.dc != null)
			{
				this.dc.Dispose();
			}
		}

		// Token: 0x04004060 RID: 16480
		public const string IdentityHEncoding = "Identity-H";

		// Token: 0x04004061 RID: 16481
		protected GdiDeviceContent dc;

		// Token: 0x04004062 RID: 16482
		protected GdiFontMetrics metrics;

		// Token: 0x04004063 RID: 16483
		protected GdiKerningPairs kerning;

		// Token: 0x04004064 RID: 16484
		protected int[] widths;

		// Token: 0x04004065 RID: 16485
		protected string baseFontName;

		// Token: 0x04004066 RID: 16486
		protected FontProperties properties;

		// Token: 0x04004067 RID: 16487
		protected SortedList usedGlyphs;

		// Token: 0x04004068 RID: 16488
		protected GdiUnicodeRanges unicodeRanges;
	}
}

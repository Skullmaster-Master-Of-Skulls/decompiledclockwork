using System;
using Telerik.Pdf.Gdi;
using Telerik.Web.Apoc.Render.Pdf;
using Telerik.Web.Apoc.Render.Pdf.Fonts;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015E9 RID: 5609
	internal class FontState
	{
		// Token: 0x0600DA87 RID: 55943 RVA: 0x002FDC2C File Offset: 0x002FBE2C
		public FontState(FontInfo fontInfo, string fontFamily, string fontStyle, string fontWeight, int fontSize, int fontVariant)
		{
			this.fontInfo = fontInfo;
			this.fontFamily = fontFamily;
			this.fontStyle = fontStyle;
			this.fontWeight = fontWeight;
			this.fontSize = fontSize;
			this.fontName = fontInfo.FontLookup(fontFamily, fontStyle, fontWeight);
			this.metric = fontInfo.GetMetricsFor(this.fontName);
			this.fontVariant = fontVariant;
			this.letterSpacing = 0;
		}

		// Token: 0x0600DA88 RID: 55944 RVA: 0x002FDC95 File Offset: 0x002FBE95
		public FontState(FontInfo fontInfo, string fontFamily, string fontStyle, string fontWeight, int fontSize, int fontVariant, int letterSpacing) : this(fontInfo, fontFamily, fontStyle, fontWeight, fontSize, fontVariant)
		{
			this.letterSpacing = letterSpacing;
		}

		// Token: 0x17004309 RID: 17161
		// (get) Token: 0x0600DA89 RID: 55945 RVA: 0x002FDCAE File Offset: 0x002FBEAE
		public int Ascender
		{
			get
			{
				return this.metric.Ascender * this.fontSize / 1000;
			}
		}

		// Token: 0x1700430A RID: 17162
		// (get) Token: 0x0600DA8A RID: 55946 RVA: 0x002FDCC8 File Offset: 0x002FBEC8
		public int LetterSpacing
		{
			get
			{
				return this.letterSpacing;
			}
		}

		// Token: 0x1700430B RID: 17163
		// (get) Token: 0x0600DA8B RID: 55947 RVA: 0x002FDCD0 File Offset: 0x002FBED0
		public int CapHeight
		{
			get
			{
				return this.metric.CapHeight * this.fontSize / 1000;
			}
		}

		// Token: 0x1700430C RID: 17164
		// (get) Token: 0x0600DA8C RID: 55948 RVA: 0x002FDCEA File Offset: 0x002FBEEA
		public int Descender
		{
			get
			{
				return this.metric.Descender * this.fontSize / 1000;
			}
		}

		// Token: 0x1700430D RID: 17165
		// (get) Token: 0x0600DA8D RID: 55949 RVA: 0x002FDD04 File Offset: 0x002FBF04
		public string FontName
		{
			get
			{
				return this.fontName;
			}
		}

		// Token: 0x1700430E RID: 17166
		// (get) Token: 0x0600DA8E RID: 55950 RVA: 0x002FDD0C File Offset: 0x002FBF0C
		public int FontSize
		{
			get
			{
				return this.fontSize;
			}
		}

		// Token: 0x1700430F RID: 17167
		// (get) Token: 0x0600DA8F RID: 55951 RVA: 0x002FDD14 File Offset: 0x002FBF14
		public string FontWeight
		{
			get
			{
				return this.fontWeight;
			}
		}

		// Token: 0x17004310 RID: 17168
		// (get) Token: 0x0600DA90 RID: 55952 RVA: 0x002FDD1C File Offset: 0x002FBF1C
		public string FontFamily
		{
			get
			{
				return this.fontFamily;
			}
		}

		// Token: 0x17004311 RID: 17169
		// (get) Token: 0x0600DA91 RID: 55953 RVA: 0x002FDD24 File Offset: 0x002FBF24
		public string FontStyle
		{
			get
			{
				return this.fontStyle;
			}
		}

		// Token: 0x17004312 RID: 17170
		// (get) Token: 0x0600DA92 RID: 55954 RVA: 0x002FDD2C File Offset: 0x002FBF2C
		public int FontVariant
		{
			get
			{
				return this.fontVariant;
			}
		}

		// Token: 0x17004313 RID: 17171
		// (get) Token: 0x0600DA93 RID: 55955 RVA: 0x002FDD34 File Offset: 0x002FBF34
		public FontInfo FontInfo
		{
			get
			{
				return this.fontInfo;
			}
		}

		// Token: 0x17004314 RID: 17172
		// (get) Token: 0x0600DA94 RID: 55956 RVA: 0x002FDD3C File Offset: 0x002FBF3C
		public GdiKerningPairs Kerning
		{
			get
			{
				IFontDescriptor descriptor = this.metric.Descriptor;
				if (descriptor != null && descriptor.HasKerningInfo)
				{
					return descriptor.KerningInfo;
				}
				return GdiKerningPairs.Empty;
			}
		}

		// Token: 0x0600DA95 RID: 55957 RVA: 0x002FDD6C File Offset: 0x002FBF6C
		public int GetWidth(int charId)
		{
			return this.letterSpacing + this.metric.GetWidth(charId) * this.fontSize / 1000;
		}

		// Token: 0x0600DA96 RID: 55958 RVA: 0x002FDD90 File Offset: 0x002FBF90
		public int MapCharacter(char c)
		{
			if (this.metric is Font)
			{
				return ((Font)this.metric).MapCharacter(c);
			}
			int num = CodePointMapping.GetMapping("WinAnsiEncoding").MapCharacter(c);
			if (num != 0)
			{
				return num;
			}
			return 35;
		}

		// Token: 0x04003CD3 RID: 15571
		private FontInfo fontInfo;

		// Token: 0x04003CD4 RID: 15572
		private string fontName;

		// Token: 0x04003CD5 RID: 15573
		private int fontSize;

		// Token: 0x04003CD6 RID: 15574
		private string fontFamily;

		// Token: 0x04003CD7 RID: 15575
		private string fontStyle;

		// Token: 0x04003CD8 RID: 15576
		private string fontWeight;

		// Token: 0x04003CD9 RID: 15577
		private int fontVariant;

		// Token: 0x04003CDA RID: 15578
		private IFontMetric metric;

		// Token: 0x04003CDB RID: 15579
		private int letterSpacing;
	}
}

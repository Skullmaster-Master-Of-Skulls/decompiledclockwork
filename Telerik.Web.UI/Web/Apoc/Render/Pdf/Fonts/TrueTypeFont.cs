using System;
using Telerik.Pdf;
using Telerik.Pdf.Gdi;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Render.Pdf.Fonts
{
	// Token: 0x02001697 RID: 5783
	internal class TrueTypeFont : Font, IFontDescriptor, IDisposable
	{
		// Token: 0x0600DF25 RID: 57125 RVA: 0x00318CCF File Offset: 0x00316ECF
		public TrueTypeFont(FontProperties properties)
		{
			this.properties = properties;
			this.ObtainFontMetrics();
		}

		// Token: 0x0600DF26 RID: 57126 RVA: 0x00318CF4 File Offset: 0x00316EF4
		private void ObtainFontMetrics()
		{
			this.dc = new GdiDeviceContent();
			GdiFont gdiFont = GdiFont.CreateDesignFont(this.properties.FaceName, this.properties.IsBold, this.properties.IsItalic, this.dc);
			this.metrics = gdiFont.GetMetrics(this.dc);
		}

		// Token: 0x17004457 RID: 17495
		// (get) Token: 0x0600DF27 RID: 57127 RVA: 0x00318D4C File Offset: 0x00316F4C
		public PdfArray Array
		{
			get
			{
				PdfArray pdfArray = new PdfArray();
				pdfArray.AddArray(this.Widths);
				return pdfArray;
			}
		}

		// Token: 0x17004458 RID: 17496
		// (get) Token: 0x0600DF28 RID: 57128 RVA: 0x00318D6C File Offset: 0x00316F6C
		public override PdfFontSubTypeEnum SubType
		{
			get
			{
				return PdfFontSubTypeEnum.TrueType;
			}
		}

		// Token: 0x17004459 RID: 17497
		// (get) Token: 0x0600DF29 RID: 57129 RVA: 0x00318D70 File Offset: 0x00316F70
		public override string FontName
		{
			get
			{
				if (this.properties.IsBoldItalic)
				{
					return string.Format("{0},BoldItalic", this.properties.FaceName);
				}
				if (this.properties.IsBold)
				{
					return string.Format("{0},Bold", this.properties.FaceName);
				}
				if (this.properties.IsItalic)
				{
					return string.Format("{0},Italic", this.properties.FaceName);
				}
				return this.properties.FaceName;
			}
		}

		// Token: 0x1700445A RID: 17498
		// (get) Token: 0x0600DF2A RID: 57130 RVA: 0x00318DF1 File Offset: 0x00316FF1
		public override PdfFontTypeEnum Type
		{
			get
			{
				return PdfFontTypeEnum.TrueType;
			}
		}

		// Token: 0x1700445B RID: 17499
		// (get) Token: 0x0600DF2B RID: 57131 RVA: 0x00318DF4 File Offset: 0x00316FF4
		public override string Encoding
		{
			get
			{
				return "WinAnsiEncoding";
			}
		}

		// Token: 0x1700445C RID: 17500
		// (get) Token: 0x0600DF2C RID: 57132 RVA: 0x00318DFB File Offset: 0x00316FFB
		public override IFontDescriptor Descriptor
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700445D RID: 17501
		// (get) Token: 0x0600DF2D RID: 57133 RVA: 0x00318DFE File Offset: 0x00316FFE
		public override bool MultiByteFont
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600DF2E RID: 57134 RVA: 0x00318E01 File Offset: 0x00317001
		public override int MapCharacter(char c)
		{
			if (c > 'ÿ')
			{
				return this.FirstChar;
			}
			return this.mapping.MapCharacter(c);
		}

		// Token: 0x1700445E RID: 17502
		// (get) Token: 0x0600DF2F RID: 57135 RVA: 0x00318E1E File Offset: 0x0031701E
		public override int Ascender
		{
			get
			{
				return this.metrics.Ascent;
			}
		}

		// Token: 0x1700445F RID: 17503
		// (get) Token: 0x0600DF30 RID: 57136 RVA: 0x00318E2B File Offset: 0x0031702B
		public override int Descender
		{
			get
			{
				return this.metrics.Descent;
			}
		}

		// Token: 0x17004460 RID: 17504
		// (get) Token: 0x0600DF31 RID: 57137 RVA: 0x00318E38 File Offset: 0x00317038
		public override int CapHeight
		{
			get
			{
				return this.metrics.CapHeight;
			}
		}

		// Token: 0x17004461 RID: 17505
		// (get) Token: 0x0600DF32 RID: 57138 RVA: 0x00318E45 File Offset: 0x00317045
		public override int FirstChar
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17004462 RID: 17506
		// (get) Token: 0x0600DF33 RID: 57139 RVA: 0x00318E48 File Offset: 0x00317048
		public override int LastChar
		{
			get
			{
				return 255;
			}
		}

		// Token: 0x0600DF34 RID: 57140 RVA: 0x00318E4F File Offset: 0x0031704F
		public override int GetWidth(int charIndex)
		{
			this.EnsureWidthsArray();
			return this.widths[charIndex];
		}

		// Token: 0x17004463 RID: 17507
		// (get) Token: 0x0600DF35 RID: 57141 RVA: 0x00318E5F File Offset: 0x0031705F
		public override int[] Widths
		{
			get
			{
				this.EnsureWidthsArray();
				return this.widths;
			}
		}

		// Token: 0x0600DF36 RID: 57142 RVA: 0x00318E6D File Offset: 0x0031706D
		private void EnsureWidthsArray()
		{
			if (this.widths == null)
			{
				this.widths = this.metrics.GetAnsiWidths();
			}
		}

		// Token: 0x17004464 RID: 17508
		// (get) Token: 0x0600DF37 RID: 57143 RVA: 0x00318E88 File Offset: 0x00317088
		public int Flags
		{
			get
			{
				return this.metrics.Flags;
			}
		}

		// Token: 0x17004465 RID: 17509
		// (get) Token: 0x0600DF38 RID: 57144 RVA: 0x00318E95 File Offset: 0x00317095
		public int[] FontBBox
		{
			get
			{
				return this.metrics.BoundingBox;
			}
		}

		// Token: 0x17004466 RID: 17510
		// (get) Token: 0x0600DF39 RID: 57145 RVA: 0x00318EA2 File Offset: 0x003170A2
		public int ItalicAngle
		{
			get
			{
				return this.metrics.ItalicAngle;
			}
		}

		// Token: 0x17004467 RID: 17511
		// (get) Token: 0x0600DF3A RID: 57146 RVA: 0x00318EAF File Offset: 0x003170AF
		public int StemV
		{
			get
			{
				return this.metrics.StemV;
			}
		}

		// Token: 0x17004468 RID: 17512
		// (get) Token: 0x0600DF3B RID: 57147 RVA: 0x00318EBC File Offset: 0x003170BC
		public bool HasKerningInfo
		{
			get
			{
				if (this.kerning == null)
				{
					this.kerning = this.metrics.AnsiKerningPairs;
				}
				return this.kerning.Count != 0;
			}
		}

		// Token: 0x17004469 RID: 17513
		// (get) Token: 0x0600DF3C RID: 57148 RVA: 0x00318EE8 File Offset: 0x003170E8
		public bool IsEmbeddable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700446A RID: 17514
		// (get) Token: 0x0600DF3D RID: 57149 RVA: 0x00318EEB File Offset: 0x003170EB
		public bool IsSubsettable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700446B RID: 17515
		// (get) Token: 0x0600DF3E RID: 57150 RVA: 0x00318EEE File Offset: 0x003170EE
		public byte[] FontData
		{
			get
			{
				return this.metrics.GetFontData();
			}
		}

		// Token: 0x1700446C RID: 17516
		// (get) Token: 0x0600DF3F RID: 57151 RVA: 0x00318EFB File Offset: 0x003170FB
		public GdiKerningPairs KerningInfo
		{
			get
			{
				if (this.kerning == null)
				{
					this.kerning = this.metrics.AnsiKerningPairs;
				}
				return this.kerning;
			}
		}

		// Token: 0x0600DF40 RID: 57152 RVA: 0x00318F1C File Offset: 0x0031711C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600DF41 RID: 57153 RVA: 0x00318F2B File Offset: 0x0031712B
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.dc != null)
			{
				this.dc.Dispose();
			}
		}

		// Token: 0x04004059 RID: 16473
		public const string WinAnsiEncoding = "WinAnsiEncoding";

		// Token: 0x0400405A RID: 16474
		private CodePointMapping mapping = CodePointMapping.GetMapping("WinAnsiEncoding");

		// Token: 0x0400405B RID: 16475
		private GdiDeviceContent dc;

		// Token: 0x0400405C RID: 16476
		private GdiFontMetrics metrics;

		// Token: 0x0400405D RID: 16477
		private GdiKerningPairs kerning;

		// Token: 0x0400405E RID: 16478
		private int[] widths;

		// Token: 0x0400405F RID: 16479
		protected FontProperties properties;
	}
}

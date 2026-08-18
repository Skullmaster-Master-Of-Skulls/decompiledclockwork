using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000051 RID: 81
	public class PdfGState : PdfDictionary
	{
		// Token: 0x17000051 RID: 81
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000B3B6 File Offset: 0x0000A3B6
		public bool OverPrintStroking
		{
			set
			{
				base.Put(PdfName.OP, value ? PdfBoolean.PDFTRUE : PdfBoolean.PDFFALSE);
			}
		}

		// Token: 0x17000052 RID: 82
		// (set) Token: 0x0600023A RID: 570 RVA: 0x0000B3D2 File Offset: 0x0000A3D2
		public bool OverPrintNonStroking
		{
			set
			{
				base.Put(PdfName.op_, value ? PdfBoolean.PDFTRUE : PdfBoolean.PDFFALSE);
			}
		}

		// Token: 0x17000053 RID: 83
		// (set) Token: 0x0600023B RID: 571 RVA: 0x0000B3EE File Offset: 0x0000A3EE
		public int OverPrintMode
		{
			set
			{
				base.Put(PdfName.OPM, new PdfNumber((value == 0) ? 0 : 1));
			}
		}

		// Token: 0x17000054 RID: 84
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000B407 File Offset: 0x0000A407
		public float StrokeOpacity
		{
			set
			{
				base.Put(PdfName.CA, new PdfNumber(value));
			}
		}

		// Token: 0x17000055 RID: 85
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0000B41A File Offset: 0x0000A41A
		public float FillOpacity
		{
			set
			{
				base.Put(PdfName.ca_, new PdfNumber(value));
			}
		}

		// Token: 0x17000056 RID: 86
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000B42D File Offset: 0x0000A42D
		public bool AlphaIsShape
		{
			set
			{
				base.Put(PdfName.AIS, value ? PdfBoolean.PDFTRUE : PdfBoolean.PDFFALSE);
			}
		}

		// Token: 0x17000057 RID: 87
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0000B449 File Offset: 0x0000A449
		public bool TextKnockout
		{
			set
			{
				base.Put(PdfName.TK, value ? PdfBoolean.PDFTRUE : PdfBoolean.PDFFALSE);
			}
		}

		// Token: 0x17000058 RID: 88
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000B465 File Offset: 0x0000A465
		public PdfName BlendMode
		{
			set
			{
				base.Put(PdfName.BM, value);
			}
		}

		// Token: 0x17000059 RID: 89
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000B473 File Offset: 0x0000A473
		public PdfName RenderingIntent
		{
			set
			{
				base.Put(PdfName.RI, value);
			}
		}

		// Token: 0x04000103 RID: 259
		public static PdfName BM_NORMAL = new PdfName("Normal");

		// Token: 0x04000104 RID: 260
		public static PdfName BM_COMPATIBLE = new PdfName("Compatible");

		// Token: 0x04000105 RID: 261
		public static PdfName BM_MULTIPLY = new PdfName("Multiply");

		// Token: 0x04000106 RID: 262
		public static PdfName BM_SCREEN = new PdfName("Screen");

		// Token: 0x04000107 RID: 263
		public static PdfName BM_OVERLAY = new PdfName("Overlay");

		// Token: 0x04000108 RID: 264
		public static PdfName BM_DARKEN = new PdfName("Darken");

		// Token: 0x04000109 RID: 265
		public static PdfName BM_LIGHTEN = new PdfName("Lighten");

		// Token: 0x0400010A RID: 266
		public static PdfName BM_COLORDODGE = new PdfName("ColorDodge");

		// Token: 0x0400010B RID: 267
		public static PdfName BM_COLORBURN = new PdfName("ColorBurn");

		// Token: 0x0400010C RID: 268
		public static PdfName BM_HARDLIGHT = new PdfName("HardLight");

		// Token: 0x0400010D RID: 269
		public static PdfName BM_SOFTLIGHT = new PdfName("SoftLight");

		// Token: 0x0400010E RID: 270
		public static PdfName BM_DIFFERENCE = new PdfName("Difference");

		// Token: 0x0400010F RID: 271
		public static PdfName BM_EXCLUSION = new PdfName("Exclusion");
	}
}

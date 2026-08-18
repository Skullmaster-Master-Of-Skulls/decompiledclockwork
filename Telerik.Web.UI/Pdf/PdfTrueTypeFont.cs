using System;

namespace Telerik.Pdf
{
	// Token: 0x0200166F RID: 5743
	public class PdfTrueTypeFont : PdfFont
	{
		// Token: 0x0600DE2B RID: 56875 RVA: 0x00308E7C File Offset: 0x0030707C
		public PdfTrueTypeFont(PdfObjectId objectId, string fontName, string baseFont) : base(fontName, objectId)
		{
			base[PdfName.Names.Subtype] = PdfName.Names.TrueType;
			base[PdfName.Names.BaseFont] = new PdfName(baseFont);
			base[PdfName.Names.FirstChar] = new PdfNumeric(0m);
			base[PdfName.Names.LastChar] = new PdfNumeric(255m);
		}

		// Token: 0x170043F4 RID: 17396
		// (set) Token: 0x0600DE2C RID: 56876 RVA: 0x00308EE2 File Offset: 0x003070E2
		public PdfName Encoding
		{
			set
			{
				base[PdfName.Names.Encoding] = value;
			}
		}

		// Token: 0x170043F5 RID: 17397
		// (set) Token: 0x0600DE2D RID: 56877 RVA: 0x00308EF0 File Offset: 0x003070F0
		public PdfFontDescriptor Descriptor
		{
			set
			{
				base[PdfName.Names.FontDescriptor] = value.GetReference();
			}
		}

		// Token: 0x170043F6 RID: 17398
		// (set) Token: 0x0600DE2E RID: 56878 RVA: 0x00308F03 File Offset: 0x00307103
		public PdfNumeric FirstChar
		{
			set
			{
				base[PdfName.Names.FirstChar] = value;
			}
		}

		// Token: 0x170043F7 RID: 17399
		// (set) Token: 0x0600DE2F RID: 56879 RVA: 0x00308F11 File Offset: 0x00307111
		public PdfNumeric LastChar
		{
			set
			{
				base[PdfName.Names.LastChar] = value;
			}
		}

		// Token: 0x170043F8 RID: 17400
		// (set) Token: 0x0600DE30 RID: 56880 RVA: 0x00308F1F File Offset: 0x0030711F
		public PdfArray Widths
		{
			set
			{
				base[PdfName.Names.Widths] = value;
			}
		}
	}
}

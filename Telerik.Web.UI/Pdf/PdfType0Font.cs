using System;

namespace Telerik.Pdf
{
	// Token: 0x02001670 RID: 5744
	public class PdfType0Font : PdfFont
	{
		// Token: 0x0600DE31 RID: 56881 RVA: 0x00308F2D File Offset: 0x0030712D
		public PdfType0Font(PdfObjectId objectId, string fontName, string baseFont) : base(fontName, objectId)
		{
			base[PdfName.Names.Subtype] = PdfName.Names.Type0;
			base[PdfName.Names.BaseFont] = new PdfName(baseFont);
		}

		// Token: 0x170043F9 RID: 17401
		// (set) Token: 0x0600DE32 RID: 56882 RVA: 0x00308F58 File Offset: 0x00307158
		public PdfCMap ToUnicode
		{
			set
			{
				base[PdfName.Names.ToUnicode] = value.GetReference();
			}
		}

		// Token: 0x170043FA RID: 17402
		// (set) Token: 0x0600DE33 RID: 56883 RVA: 0x00308F6C File Offset: 0x0030716C
		public PdfCIDFont Descendant
		{
			set
			{
				PdfArray pdfArray = new PdfArray();
				pdfArray.Add(value.GetReference());
				base[PdfName.Names.DescendantFonts] = pdfArray;
			}
		}

		// Token: 0x170043FB RID: 17403
		// (set) Token: 0x0600DE34 RID: 56884 RVA: 0x00308F98 File Offset: 0x00307198
		public PdfName Encoding
		{
			set
			{
				base[PdfName.Names.Encoding] = value;
			}
		}
	}
}

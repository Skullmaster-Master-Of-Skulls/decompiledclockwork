using System;

namespace Telerik.Pdf
{
	// Token: 0x02001671 RID: 5745
	public sealed class PdfType1Font : PdfFont
	{
		// Token: 0x0600DE35 RID: 56885 RVA: 0x00308FA6 File Offset: 0x003071A6
		public PdfType1Font(PdfObjectId objectId, string fontName, string baseFont) : base(fontName, objectId)
		{
			base[PdfName.Names.Subtype] = PdfName.Names.Type1;
			base[PdfName.Names.BaseFont] = new PdfName(baseFont);
		}

		// Token: 0x170043FC RID: 17404
		// (set) Token: 0x0600DE36 RID: 56886 RVA: 0x00308FD1 File Offset: 0x003071D1
		public PdfName Encoding
		{
			set
			{
				base[PdfName.Names.Encoding] = value;
			}
		}
	}
}

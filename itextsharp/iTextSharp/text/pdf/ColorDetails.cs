using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000460 RID: 1120
	public class ColorDetails
	{
		// Token: 0x0600261E RID: 9758 RVA: 0x000E6425 File Offset: 0x000E5425
		internal ColorDetails(PdfName colorName, PdfIndirectReference indirectReference, PdfSpotColor scolor)
		{
			this.colorName = colorName;
			this.indirectReference = indirectReference;
			this.spotcolor = scolor;
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600261F RID: 9759 RVA: 0x000E6442 File Offset: 0x000E5442
		internal PdfIndirectReference IndirectReference
		{
			get
			{
				return this.indirectReference;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002620 RID: 9760 RVA: 0x000E644A File Offset: 0x000E544A
		internal PdfName ColorName
		{
			get
			{
				return this.colorName;
			}
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x000E6452 File Offset: 0x000E5452
		internal PdfObject GetSpotColor(PdfWriter writer)
		{
			return this.spotcolor.GetSpotObject(writer);
		}

		// Token: 0x04001A77 RID: 6775
		private PdfIndirectReference indirectReference;

		// Token: 0x04001A78 RID: 6776
		private PdfName colorName;

		// Token: 0x04001A79 RID: 6777
		private PdfSpotColor spotcolor;
	}
}

using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000326 RID: 806
	public class PdfBorderArray : PdfArray
	{
		// Token: 0x06001D48 RID: 7496 RVA: 0x000AFE5F File Offset: 0x000AEE5F
		public PdfBorderArray(float hRadius, float vRadius, float width) : this(hRadius, vRadius, width, null)
		{
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x000AFE6B File Offset: 0x000AEE6B
		public PdfBorderArray(float hRadius, float vRadius, float width, PdfDashPattern dash) : base(new PdfNumber(hRadius))
		{
			this.Add(new PdfNumber(vRadius));
			this.Add(new PdfNumber(width));
			if (dash != null)
			{
				this.Add(dash);
			}
		}
	}
}

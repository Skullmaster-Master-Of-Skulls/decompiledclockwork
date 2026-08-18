using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000325 RID: 805
	public class PdfNull : PdfObject
	{
		// Token: 0x06001D45 RID: 7493 RVA: 0x000AFE3E File Offset: 0x000AEE3E
		public PdfNull() : base(8, "null")
		{
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x000AFE4C File Offset: 0x000AEE4C
		public override string ToString()
		{
			return "null";
		}

		// Token: 0x0400142C RID: 5164
		public static PdfNull PDFNULL = new PdfNull();
	}
}

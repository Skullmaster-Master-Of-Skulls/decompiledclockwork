using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000C9 RID: 201
	public interface IPdfOCG
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600070A RID: 1802
		PdfIndirectReference Ref { get; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600070B RID: 1803
		PdfObject PdfObject { get; }
	}
}

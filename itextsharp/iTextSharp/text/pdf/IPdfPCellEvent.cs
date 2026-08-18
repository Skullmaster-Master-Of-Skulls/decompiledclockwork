using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000169 RID: 361
	public interface IPdfPCellEvent
	{
		// Token: 0x06000DB2 RID: 3506
		void CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases);
	}
}

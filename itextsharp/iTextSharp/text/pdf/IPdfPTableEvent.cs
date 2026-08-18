using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200032D RID: 813
	public interface IPdfPTableEvent
	{
		// Token: 0x06001D77 RID: 7543
		void TableLayout(PdfPTable table, float[][] widths, float[] heights, int headerRows, int rowStart, PdfContentByte[] canvases);
	}
}

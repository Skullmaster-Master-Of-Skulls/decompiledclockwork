using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf.events
{
	// Token: 0x0200032E RID: 814
	public class PdfPTableEventForwarder : IPdfPTableEvent
	{
		// Token: 0x06001D78 RID: 7544 RVA: 0x000B1065 File Offset: 0x000B0065
		public void AddTableEvent(IPdfPTableEvent eventa)
		{
			this.events.Add(eventa);
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x000B1074 File Offset: 0x000B0074
		public void TableLayout(PdfPTable table, float[][] widths, float[] heights, int headerRows, int rowStart, PdfContentByte[] canvases)
		{
			foreach (IPdfPTableEvent pdfPTableEvent in this.events)
			{
				pdfPTableEvent.TableLayout(table, widths, heights, headerRows, rowStart, canvases);
			}
		}

		// Token: 0x0400143F RID: 5183
		protected List<IPdfPTableEvent> events = new List<IPdfPTableEvent>();
	}
}

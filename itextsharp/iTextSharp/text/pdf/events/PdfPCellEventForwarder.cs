using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf.events
{
	// Token: 0x0200016A RID: 362
	public class PdfPCellEventForwarder : IPdfPCellEvent
	{
		// Token: 0x06000DB3 RID: 3507 RVA: 0x0004AB4E File Offset: 0x00049B4E
		public void AddCellEvent(IPdfPCellEvent eventa)
		{
			this.events.Add(eventa);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0004AB5C File Offset: 0x00049B5C
		public void CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases)
		{
			foreach (IPdfPCellEvent pdfPCellEvent in this.events)
			{
				pdfPCellEvent.CellLayout(cell, position, canvases);
			}
		}

		// Token: 0x04000A0B RID: 2571
		protected List<IPdfPCellEvent> events = new List<IPdfPCellEvent>();
	}
}

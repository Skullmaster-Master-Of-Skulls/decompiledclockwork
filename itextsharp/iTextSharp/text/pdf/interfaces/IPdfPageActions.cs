using System;

namespace iTextSharp.text.pdf.interfaces
{
	// Token: 0x020000D6 RID: 214
	public interface IPdfPageActions
	{
		// Token: 0x06000764 RID: 1892
		void SetPageAction(PdfName actionType, PdfAction action);

		// Token: 0x17000183 RID: 387
		// (set) Token: 0x06000765 RID: 1893
		int Duration { set; }

		// Token: 0x17000184 RID: 388
		// (set) Token: 0x06000766 RID: 1894
		PdfTransition Transition { set; }
	}
}

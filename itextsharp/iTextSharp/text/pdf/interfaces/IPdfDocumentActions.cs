using System;

namespace iTextSharp.text.pdf.interfaces
{
	// Token: 0x020000D5 RID: 213
	public interface IPdfDocumentActions
	{
		// Token: 0x06000761 RID: 1889
		void SetOpenAction(string name);

		// Token: 0x06000762 RID: 1890
		void SetOpenAction(PdfAction action);

		// Token: 0x06000763 RID: 1891
		void SetAdditionalAction(PdfName actionType, PdfAction action);
	}
}

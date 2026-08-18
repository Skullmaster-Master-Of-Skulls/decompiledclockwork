using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200021E RID: 542
	public interface IPdfPageEvent
	{
		// Token: 0x06001515 RID: 5397
		void OnOpenDocument(PdfWriter writer, Document document);

		// Token: 0x06001516 RID: 5398
		void OnStartPage(PdfWriter writer, Document document);

		// Token: 0x06001517 RID: 5399
		void OnEndPage(PdfWriter writer, Document document);

		// Token: 0x06001518 RID: 5400
		void OnCloseDocument(PdfWriter writer, Document document);

		// Token: 0x06001519 RID: 5401
		void OnParagraph(PdfWriter writer, Document document, float paragraphPosition);

		// Token: 0x0600151A RID: 5402
		void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition);

		// Token: 0x0600151B RID: 5403
		void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title);

		// Token: 0x0600151C RID: 5404
		void OnChapterEnd(PdfWriter writer, Document document, float paragraphPosition);

		// Token: 0x0600151D RID: 5405
		void OnSection(PdfWriter writer, Document document, float paragraphPosition, int depth, Paragraph title);

		// Token: 0x0600151E RID: 5406
		void OnSectionEnd(PdfWriter writer, Document document, float paragraphPosition);

		// Token: 0x0600151F RID: 5407
		void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, string text);
	}
}

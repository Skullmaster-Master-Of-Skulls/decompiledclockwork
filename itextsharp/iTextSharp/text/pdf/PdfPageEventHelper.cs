using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200045B RID: 1115
	public class PdfPageEventHelper : IPdfPageEvent
	{
		// Token: 0x060025A5 RID: 9637 RVA: 0x000E3FE7 File Offset: 0x000E2FE7
		public virtual void OnOpenDocument(PdfWriter writer, Document document)
		{
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000E3FE9 File Offset: 0x000E2FE9
		public virtual void OnStartPage(PdfWriter writer, Document document)
		{
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000E3FEB File Offset: 0x000E2FEB
		public virtual void OnEndPage(PdfWriter writer, Document document)
		{
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000E3FED File Offset: 0x000E2FED
		public virtual void OnCloseDocument(PdfWriter writer, Document document)
		{
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000E3FEF File Offset: 0x000E2FEF
		public virtual void OnParagraph(PdfWriter writer, Document document, float paragraphPosition)
		{
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000E3FF1 File Offset: 0x000E2FF1
		public virtual void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition)
		{
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000E3FF3 File Offset: 0x000E2FF3
		public virtual void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title)
		{
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000E3FF5 File Offset: 0x000E2FF5
		public virtual void OnChapterEnd(PdfWriter writer, Document document, float position)
		{
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x000E3FF7 File Offset: 0x000E2FF7
		public virtual void OnSection(PdfWriter writer, Document document, float paragraphPosition, int depth, Paragraph title)
		{
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x000E3FF9 File Offset: 0x000E2FF9
		public virtual void OnSectionEnd(PdfWriter writer, Document document, float position)
		{
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x000E3FFB File Offset: 0x000E2FFB
		public virtual void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, string text)
		{
		}
	}
}

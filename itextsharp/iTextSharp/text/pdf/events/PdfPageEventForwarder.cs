using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf.events
{
	// Token: 0x0200021F RID: 543
	public class PdfPageEventForwarder : IPdfPageEvent
	{
		// Token: 0x06001520 RID: 5408 RVA: 0x0007696A File Offset: 0x0007596A
		public void AddPageEvent(IPdfPageEvent eventa)
		{
			this.events.Add(eventa);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00076978 File Offset: 0x00075978
		public virtual void OnOpenDocument(PdfWriter writer, Document document)
		{
			foreach (IPdfPageEvent pdfPageEvent in this.events)
			{
				pdfPageEvent.OnOpenDocument(writer, document);
			}
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x000769CC File Offset: 0x000759CC
		public virtual void OnStartPage(PdfWriter writer, Document document)
		{
			foreach (IPdfPageEvent pdfPageEvent in this.events)
			{
				pdfPageEvent.OnStartPage(writer, document);
			}
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00076A20 File Offset: 0x00075A20
		public virtual void OnEndPage(PdfWriter writer, Document document)
		{
			foreach (IPdfPageEvent pdfPageEvent in this.events)
			{
				pdfPageEvent.OnEndPage(writer, document);
			}
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x00076A74 File Offset: 0x00075A74
		public virtual void OnCloseDocument(PdfWriter writer, Document document)
		{
			foreach (IPdfPageEvent pdfPageEvent in this.events)
			{
				pdfPageEvent.OnCloseDocument(writer, document);
			}
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x00076AC8 File Offset: 0x00075AC8
		public virtual void OnParagraph(PdfWriter writer, Document document, float paragraphPosition)
		{
			foreach (IPdfPageEvent pdfPageEvent in this.events)
			{
				pdfPageEvent.OnParagraph(writer, document, paragraphPosition);
			}
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00076B20 File Offset: 0x00075B20
		public virtual void OnParagraphEnd(PdfWriter writer, Document document, float paragraphPosition)
		{
			foreach (IPdfPageEvent pdfPageEvent in this.events)
			{
				pdfPageEvent.OnParagraphEnd(writer, document, paragraphPosition);
			}
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x00076B78 File Offset: 0x00075B78
		public virtual void OnChapter(PdfWriter writer, Document document, float paragraphPosition, Paragraph title)
		{
			foreach (IPdfPageEvent pdfPageEvent in this.events)
			{
				pdfPageEvent.OnChapter(writer, document, paragraphPosition, title);
			}
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00076BD0 File Offset: 0x00075BD0
		public virtual void OnChapterEnd(PdfWriter writer, Document document, float position)
		{
			foreach (IPdfPageEvent pdfPageEvent in this.events)
			{
				pdfPageEvent.OnChapterEnd(writer, document, position);
			}
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00076C28 File Offset: 0x00075C28
		public virtual void OnSection(PdfWriter writer, Document document, float paragraphPosition, int depth, Paragraph title)
		{
			foreach (IPdfPageEvent pdfPageEvent in this.events)
			{
				pdfPageEvent.OnSection(writer, document, paragraphPosition, depth, title);
			}
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00076C84 File Offset: 0x00075C84
		public virtual void OnSectionEnd(PdfWriter writer, Document document, float position)
		{
			foreach (IPdfPageEvent pdfPageEvent in this.events)
			{
				pdfPageEvent.OnSectionEnd(writer, document, position);
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00076CDC File Offset: 0x00075CDC
		public virtual void OnGenericTag(PdfWriter writer, Document document, Rectangle rect, string text)
		{
			foreach (IPdfPageEvent pdfPageEvent in this.events)
			{
				pdfPageEvent.OnGenericTag(writer, document, rect, text);
			}
		}

		// Token: 0x04000E42 RID: 3650
		protected List<IPdfPageEvent> events = new List<IPdfPageEvent>();
	}
}

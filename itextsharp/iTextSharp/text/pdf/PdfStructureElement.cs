using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200045A RID: 1114
	public class PdfStructureElement : PdfDictionary
	{
		// Token: 0x0600259F RID: 9631 RVA: 0x000E3ED4 File Offset: 0x000E2ED4
		public PdfStructureElement(PdfStructureElement parent, PdfName structureType)
		{
			this.top = parent.top;
			this.Init(parent, structureType);
			this.parent = parent;
			base.Put(PdfName.P, parent.reference);
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000E3F08 File Offset: 0x000E2F08
		public PdfStructureElement(PdfStructureTreeRoot parent, PdfName structureType)
		{
			this.top = parent;
			this.Init(parent, structureType);
			base.Put(PdfName.P, parent.Reference);
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x000E3F30 File Offset: 0x000E2F30
		private void Init(PdfDictionary parent, PdfName structureType)
		{
			PdfObject pdfObject = parent.Get(PdfName.K);
			if (pdfObject != null && !pdfObject.IsArray())
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("the.parent.has.already.another.function"));
			}
			PdfArray pdfArray;
			if (pdfObject == null)
			{
				pdfArray = new PdfArray();
				parent.Put(PdfName.K, pdfArray);
			}
			else
			{
				pdfArray = (PdfArray)pdfObject;
			}
			pdfArray.Add(this);
			base.Put(PdfName.S, structureType);
			this.reference = this.top.Writer.PdfIndirectReference;
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000E3FAE File Offset: 0x000E2FAE
		public PdfDictionary Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000E3FB6 File Offset: 0x000E2FB6
		internal void SetPageMark(int page, int mark)
		{
			if (mark >= 0)
			{
				base.Put(PdfName.K, new PdfNumber(mark));
			}
			this.top.SetPageMark(page, this.reference);
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x000E3FDF File Offset: 0x000E2FDF
		public PdfIndirectReference Reference
		{
			get
			{
				return this.reference;
			}
		}

		// Token: 0x04001A36 RID: 6710
		private PdfStructureElement parent;

		// Token: 0x04001A37 RID: 6711
		private PdfStructureTreeRoot top;

		// Token: 0x04001A38 RID: 6712
		private PdfIndirectReference reference;
	}
}

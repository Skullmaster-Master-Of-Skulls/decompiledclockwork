using System;

namespace Telerik.Pdf
{
	// Token: 0x0200166A RID: 5738
	public class PdfPage : PdfDictionary
	{
		// Token: 0x0600DE12 RID: 56850 RVA: 0x00308968 File Offset: 0x00306B68
		public PdfPage(PdfResources resources, PdfContentStream contents, int pagewidth, int pageheight, PdfObjectId objectId) : base(objectId)
		{
			base[PdfName.Names.Type] = PdfName.Names.Page;
			base[PdfName.Names.Resources] = resources.GetReference();
			base[PdfName.Names.Contents] = contents.GetReference();
			PdfArray pdfArray = new PdfArray();
			pdfArray.Add(new PdfNumeric(0m));
			pdfArray.Add(new PdfNumeric(0m));
			pdfArray.Add(new PdfNumeric(pagewidth));
			pdfArray.Add(new PdfNumeric(pageheight));
			base[PdfName.Names.MediaBox] = pdfArray;
		}

		// Token: 0x0600DE13 RID: 56851 RVA: 0x00308A0A File Offset: 0x00306C0A
		public void SetParent(PdfPageTree parent)
		{
			base[PdfName.Names.Parent] = parent.GetReference();
		}

		// Token: 0x0600DE14 RID: 56852 RVA: 0x00308A1D File Offset: 0x00306C1D
		public void SetAnnotList(PdfAnnotList annotList)
		{
			base[PdfName.Names.Annots] = annotList.GetReference();
		}
	}
}

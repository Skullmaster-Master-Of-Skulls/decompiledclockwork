using System;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x02000282 RID: 642
	public class ImageRenderInfo
	{
		// Token: 0x0600184A RID: 6218 RVA: 0x0008C944 File Offset: 0x0008B944
		public ImageRenderInfo(Matrix ctm, PdfIndirectReference refi)
		{
			this.ctm = ctm;
			this.refi = refi;
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0008C95C File Offset: 0x0008B95C
		public PdfImageObject GetImage()
		{
			PRStream stream = (PRStream)PdfReader.GetPdfObject(this.refi);
			return new PdfImageObject(stream);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0008C980 File Offset: 0x0008B980
		public Vector GetStartPoint()
		{
			return new Vector(0f, 0f, 1f).Cross(this.ctm);
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0008C9A1 File Offset: 0x0008B9A1
		public float GetArea()
		{
			return this.ctm.GetDeterminant();
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x0008C9AE File Offset: 0x0008B9AE
		public PdfIndirectReference GetRef()
		{
			return this.refi;
		}

		// Token: 0x04001057 RID: 4183
		private Matrix ctm;

		// Token: 0x04001058 RID: 4184
		private PdfIndirectReference refi;
	}
}

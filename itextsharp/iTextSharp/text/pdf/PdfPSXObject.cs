using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020005D2 RID: 1490
	public class PdfPSXObject : PdfTemplate
	{
		// Token: 0x0600335E RID: 13150 RVA: 0x0013E9A0 File Offset: 0x0013D9A0
		protected PdfPSXObject()
		{
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x0013E9A8 File Offset: 0x0013D9A8
		public PdfPSXObject(PdfWriter wr) : base(wr)
		{
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x0013E9B4 File Offset: 0x0013D9B4
		internal override PdfStream GetFormXObject(int compressionLevel)
		{
			PdfStream pdfStream = new PdfStream(this.content.ToByteArray());
			pdfStream.Put(PdfName.TYPE, PdfName.XOBJECT);
			pdfStream.Put(PdfName.SUBTYPE, PdfName.PS);
			pdfStream.FlateCompress(compressionLevel);
			return pdfStream;
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06003361 RID: 13153 RVA: 0x0013E9FC File Offset: 0x0013D9FC
		public override PdfContentByte Duplicate
		{
			get
			{
				return new PdfPSXObject
				{
					writer = this.writer,
					pdf = this.pdf,
					thisReference = this.thisReference,
					pageResources = this.pageResources,
					separator = this.separator
				};
			}
		}
	}
}

using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000CB RID: 203
	public class PdfFormXObject : PdfStream
	{
		// Token: 0x06000714 RID: 1812 RVA: 0x00025888 File Offset: 0x00024888
		internal PdfFormXObject(PdfTemplate template, int compressionLevel)
		{
			base.Put(PdfName.TYPE, PdfName.XOBJECT);
			base.Put(PdfName.SUBTYPE, PdfName.FORM);
			base.Put(PdfName.RESOURCES, template.Resources);
			base.Put(PdfName.BBOX, new PdfRectangle(template.BoundingBox));
			base.Put(PdfName.FORMTYPE, PdfFormXObject.ONE);
			PdfArray matrix = template.Matrix;
			if (template.Layer != null)
			{
				base.Put(PdfName.OC, template.Layer.Ref);
			}
			if (template.Group != null)
			{
				base.Put(PdfName.GROUP, template.Group);
			}
			if (matrix == null)
			{
				base.Put(PdfName.MATRIX, PdfFormXObject.MATRIX);
			}
			else
			{
				base.Put(PdfName.MATRIX, matrix);
			}
			this.bytes = template.ToPdf(null);
			base.Put(PdfName.LENGTH, new PdfNumber(this.bytes.Length));
			base.FlateCompress(compressionLevel);
		}

		// Token: 0x0400060F RID: 1551
		public static PdfNumber ZERO = new PdfNumber(0);

		// Token: 0x04000610 RID: 1552
		public static PdfNumber ONE = new PdfNumber(1);

		// Token: 0x04000611 RID: 1553
		public static PdfLiteral MATRIX = new PdfLiteral("[1 0 0 1 0 0]");
	}
}

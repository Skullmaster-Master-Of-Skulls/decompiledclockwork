using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x020005D3 RID: 1491
	public class PdfPattern : PdfStream
	{
		// Token: 0x06003362 RID: 13154 RVA: 0x0013EA4C File Offset: 0x0013DA4C
		internal PdfPattern(PdfPatternPainter painter) : this(painter, -1)
		{
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x0013EA58 File Offset: 0x0013DA58
		internal PdfPattern(PdfPatternPainter painter, int compressionLevel)
		{
			PdfNumber value = new PdfNumber(1);
			PdfArray matrix = painter.Matrix;
			if (matrix != null)
			{
				base.Put(PdfName.MATRIX, matrix);
			}
			base.Put(PdfName.TYPE, PdfName.PATTERN);
			base.Put(PdfName.BBOX, new PdfRectangle(painter.BoundingBox));
			base.Put(PdfName.RESOURCES, painter.Resources);
			base.Put(PdfName.TILINGTYPE, value);
			base.Put(PdfName.PATTERNTYPE, value);
			if (painter.IsStencil())
			{
				base.Put(PdfName.PAINTTYPE, new PdfNumber(2));
			}
			else
			{
				base.Put(PdfName.PAINTTYPE, value);
			}
			base.Put(PdfName.XSTEP, new PdfNumber(painter.XStep));
			base.Put(PdfName.YSTEP, new PdfNumber(painter.YStep));
			this.bytes = painter.ToPdf(null);
			base.Put(PdfName.LENGTH, new PdfNumber(this.bytes.Length));
			base.FlateCompress(compressionLevel);
		}
	}
}

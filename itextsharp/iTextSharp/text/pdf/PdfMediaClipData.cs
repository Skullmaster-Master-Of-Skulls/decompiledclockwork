using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x0200058B RID: 1419
	public class PdfMediaClipData : PdfDictionary
	{
		// Token: 0x06003058 RID: 12376 RVA: 0x0012B56C File Offset: 0x0012A56C
		internal PdfMediaClipData(string file, PdfFileSpecification fs, string mimeType)
		{
			base.Put(PdfName.TYPE, new PdfName("MediaClip"));
			base.Put(PdfName.S, new PdfName("MCD"));
			base.Put(PdfName.N, new PdfString("Media clip for " + file));
			base.Put(new PdfName("CT"), new PdfString(mimeType));
			PdfDictionary pdfDictionary = new PdfDictionary();
			pdfDictionary.Put(new PdfName("TF"), new PdfString("TEMPACCESS"));
			base.Put(new PdfName("P"), pdfDictionary);
			base.Put(PdfName.D, fs.Reference);
		}
	}
}

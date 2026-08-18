using System;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000050 RID: 80
	public class PdfRendition : PdfDictionary
	{
		// Token: 0x06000238 RID: 568 RVA: 0x0000B360 File Offset: 0x0000A360
		public PdfRendition(string file, PdfFileSpecification fs, string mimeType)
		{
			base.Put(PdfName.S, new PdfName("MR"));
			base.Put(PdfName.N, new PdfString("Rendition for " + file));
			base.Put(PdfName.C, new PdfMediaClipData(file, fs, mimeType));
		}
	}
}

using System;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x02000164 RID: 356
	public static class PdfTextExtractor
	{
		// Token: 0x06000D88 RID: 3464 RVA: 0x0004A224 File Offset: 0x00049224
		public static string GetTextFromPage(PdfReader reader, int pageNumber, ITextExtractionStrategy strategy)
		{
			PdfReaderContentParser pdfReaderContentParser = new PdfReaderContentParser(reader);
			return pdfReaderContentParser.ProcessContent<ITextExtractionStrategy>(pageNumber, strategy).GetResultantText();
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0004A245 File Offset: 0x00049245
		public static string GetTextFromPage(PdfReader reader, int pageNumber)
		{
			return PdfTextExtractor.GetTextFromPage(reader, pageNumber, new LocationTextExtractionStrategy());
		}
	}
}

using System;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x020004A4 RID: 1188
	public class PdfReaderContentParser
	{
		// Token: 0x06002834 RID: 10292 RVA: 0x000F27DA File Offset: 0x000F17DA
		public PdfReaderContentParser(PdfReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x000F27EC File Offset: 0x000F17EC
		public E ProcessContent<E>(int pageNumber, E renderListener) where E : IRenderListener
		{
			PdfDictionary pageN = this.reader.GetPageN(pageNumber);
			PdfDictionary asDict = pageN.GetAsDict(PdfName.RESOURCES);
			PdfContentStreamProcessor pdfContentStreamProcessor = new PdfContentStreamProcessor(renderListener);
			pdfContentStreamProcessor.ProcessContent(ContentByteUtils.GetContentBytesForPage(this.reader, pageNumber), asDict);
			return renderListener;
		}

		// Token: 0x04001B97 RID: 7063
		private PdfReader reader;
	}
}

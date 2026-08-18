using System;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x02000055 RID: 85
	public interface IXObjectDoHandler
	{
		// Token: 0x06000298 RID: 664
		void HandleXObject(PdfContentStreamProcessor processor, PdfStream stream, PdfIndirectReference refi);
	}
}

using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x02000057 RID: 87
	public interface IContentOperator
	{
		// Token: 0x0600029D RID: 669
		void Invoke(PdfContentStreamProcessor processor, PdfLiteral oper, List<PdfObject> operands);
	}
}

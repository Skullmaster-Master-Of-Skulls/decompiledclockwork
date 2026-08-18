using System;

namespace iTextSharp.text.pdf.interfaces
{
	// Token: 0x020000D9 RID: 217
	public interface IPdfAnnotations
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600076C RID: 1900
		PdfAcroForm AcroForm { get; }

		// Token: 0x0600076D RID: 1901
		void AddAnnotation(PdfAnnotation annot);

		// Token: 0x0600076E RID: 1902
		void AddCalculationOrder(PdfFormField annot);

		// Token: 0x17000188 RID: 392
		// (set) Token: 0x0600076F RID: 1903
		int SigFlags { set; }
	}
}

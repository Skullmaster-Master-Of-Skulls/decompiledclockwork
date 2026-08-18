using System;

namespace iTextSharp.text.pdf.interfaces
{
	// Token: 0x020000D4 RID: 212
	public interface IPdfVersion
	{
		// Token: 0x17000182 RID: 386
		// (set) Token: 0x0600075D RID: 1885
		char PdfVersion { set; }

		// Token: 0x0600075E RID: 1886
		void SetAtLeastPdfVersion(char version);

		// Token: 0x0600075F RID: 1887
		void SetPdfVersion(PdfName version);

		// Token: 0x06000760 RID: 1888
		void AddDeveloperExtension(PdfDeveloperExtension de);
	}
}

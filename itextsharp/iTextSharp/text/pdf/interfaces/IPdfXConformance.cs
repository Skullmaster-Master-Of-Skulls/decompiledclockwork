using System;

namespace iTextSharp.text.pdf.interfaces
{
	// Token: 0x020000D7 RID: 215
	public interface IPdfXConformance
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000768 RID: 1896
		// (set) Token: 0x06000767 RID: 1895
		int PDFXConformance { get; set; }

		// Token: 0x06000769 RID: 1897
		bool IsPdfX();
	}
}

using System;

namespace iTextSharp.text.pdf.interfaces
{
	// Token: 0x020000D3 RID: 211
	public interface IPdfViewerPreferences
	{
		// Token: 0x17000181 RID: 385
		// (set) Token: 0x0600075B RID: 1883
		int ViewerPreferences { set; }

		// Token: 0x0600075C RID: 1884
		void AddViewerPreference(PdfName key, PdfObject value);
	}
}

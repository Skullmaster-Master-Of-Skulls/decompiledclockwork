using System;

namespace Telerik.Pdf
{
	// Token: 0x02001672 RID: 5746
	public class PdfUri : PdfDictionary, IPdfAction
	{
		// Token: 0x0600DE37 RID: 56887 RVA: 0x00308FDF File Offset: 0x003071DF
		public PdfUri(string uri)
		{
			base[PdfName.Names.Type] = PdfName.Names.Action;
			base[PdfName.Names.S] = PdfName.Names.URI;
			base[PdfName.Names.URI] = new PdfString(uri);
		}

		// Token: 0x0600DE38 RID: 56888 RVA: 0x00309018 File Offset: 0x00307218
		public PdfObject GetAction()
		{
			return this;
		}
	}
}

using System;

namespace Telerik.Pdf
{
	// Token: 0x0200165F RID: 5727
	public class PdfInternalLink : IPdfAction
	{
		// Token: 0x0600DDED RID: 56813 RVA: 0x00307774 File Offset: 0x00305974
		public PdfInternalLink(PdfObjectReference goToReference)
		{
			this.goToReference = goToReference;
		}

		// Token: 0x0600DDEE RID: 56814 RVA: 0x00307783 File Offset: 0x00305983
		public PdfObject GetAction()
		{
			return this.goToReference;
		}

		// Token: 0x04003F23 RID: 16163
		private PdfObjectReference goToReference;
	}
}

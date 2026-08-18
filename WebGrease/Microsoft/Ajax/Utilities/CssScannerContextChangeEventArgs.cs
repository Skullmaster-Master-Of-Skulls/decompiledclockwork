using System;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000055 RID: 85
	internal class CssScannerContextChangeEventArgs : EventArgs
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00019700 File Offset: 0x00017900
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x00019708 File Offset: 0x00017908
		public string FileContext { get; private set; }

		// Token: 0x06000553 RID: 1363 RVA: 0x00019711 File Offset: 0x00017911
		public CssScannerContextChangeEventArgs(string fileContext)
		{
			this.FileContext = fileContext;
		}
	}
}

using System;

namespace Telerik.Web.UI
{
	// Token: 0x020008AB RID: 2219
	public interface ISpreadsheetAdapterFactory
	{
		// Token: 0x0600526A RID: 21098
		ISpreadsheetAdapter CreateAdapter();

		// Token: 0x17001AFE RID: 6910
		// (get) Token: 0x0600526B RID: 21099
		// (set) Token: 0x0600526C RID: 21100
		ISpreadsheet Owner { get; set; }
	}
}

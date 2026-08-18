using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200043F RID: 1087
	public interface IDataBoundListControl : IDataBoundControl
	{
		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06003491 RID: 13457
		DataKeyArray DataKeys { get; }

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06003492 RID: 13458
		DataKey SelectedDataKey { get; }

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06003493 RID: 13459
		// (set) Token: 0x06003494 RID: 13460
		int SelectedIndex { get; set; }

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06003495 RID: 13461
		// (set) Token: 0x06003496 RID: 13462
		string[] ClientIDRowSuffix { get; set; }

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06003497 RID: 13463
		// (set) Token: 0x06003498 RID: 13464
		bool EnablePersistedSelection { get; set; }
	}
}

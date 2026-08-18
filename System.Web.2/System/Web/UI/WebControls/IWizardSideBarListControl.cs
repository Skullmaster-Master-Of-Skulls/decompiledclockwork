using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200044B RID: 1099
	internal interface IWizardSideBarListControl
	{
		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x0600351A RID: 13594
		// (set) Token: 0x0600351B RID: 13595
		object DataSource { get; set; }

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x0600351C RID: 13596
		IEnumerable Items { get; }

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x0600351D RID: 13597
		// (set) Token: 0x0600351E RID: 13598
		ITemplate ItemTemplate { get; set; }

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x0600351F RID: 13599
		// (set) Token: 0x06003520 RID: 13600
		int SelectedIndex { get; set; }

		// Token: 0x140000AC RID: 172
		// (add) Token: 0x06003521 RID: 13601
		// (remove) Token: 0x06003522 RID: 13602
		event CommandEventHandler ItemCommand;

		// Token: 0x140000AD RID: 173
		// (add) Token: 0x06003523 RID: 13603
		// (remove) Token: 0x06003524 RID: 13604
		event EventHandler<WizardSideBarListControlItemEventArgs> ItemDataBound;

		// Token: 0x06003525 RID: 13605
		void DataBind();
	}
}

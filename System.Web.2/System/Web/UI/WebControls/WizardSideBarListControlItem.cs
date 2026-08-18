using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200051A RID: 1306
	internal sealed class WizardSideBarListControlItem
	{
		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06004239 RID: 16953 RVA: 0x000D8625 File Offset: 0x000D6825
		// (set) Token: 0x0600423A RID: 16954 RVA: 0x000D862D File Offset: 0x000D682D
		public object DataItem { get; private set; }

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x0600423B RID: 16955 RVA: 0x000D8636 File Offset: 0x000D6836
		// (set) Token: 0x0600423C RID: 16956 RVA: 0x000D863E File Offset: 0x000D683E
		public ListItemType ItemType { get; private set; }

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x0600423D RID: 16957 RVA: 0x000D8647 File Offset: 0x000D6847
		// (set) Token: 0x0600423E RID: 16958 RVA: 0x000D864F File Offset: 0x000D684F
		public int ItemIndex { get; private set; }

		// Token: 0x0600423F RID: 16959 RVA: 0x000D8658 File Offset: 0x000D6858
		public WizardSideBarListControlItem(object dataItem, ListItemType itemType, int itemIndex, Control container)
		{
			this.DataItem = dataItem;
			this.ItemType = itemType;
			this.ItemIndex = itemIndex;
			this._container = container;
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x000D867D File Offset: 0x000D687D
		internal Control FindControl(string id)
		{
			return this._container.FindControl(id);
		}

		// Token: 0x0400255B RID: 9563
		private Control _container;
	}
}

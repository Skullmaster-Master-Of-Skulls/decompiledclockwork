using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x02000608 RID: 1544
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RadMultiSelectClientState
	{
		// Token: 0x0600383A RID: 14394 RVA: 0x000B9475 File Offset: 0x000B7675
		public RadMultiSelectClientState()
		{
			this.Enabled = true;
			this.Value = new List<object>();
			this.Text = string.Empty;
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x0600383B RID: 14395 RVA: 0x000B949A File Offset: 0x000B769A
		// (set) Token: 0x0600383C RID: 14396 RVA: 0x000B94A2 File Offset: 0x000B76A2
		public List<MultiSelectClientStateDataItem> SelectedItems { get; set; }

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x0600383D RID: 14397 RVA: 0x000B94AB File Offset: 0x000B76AB
		// (set) Token: 0x0600383E RID: 14398 RVA: 0x000B94B3 File Offset: 0x000B76B3
		public List<MultiSelectClientStateDataItem> DeselectedItems { get; set; }

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x0600383F RID: 14399 RVA: 0x000B94BC File Offset: 0x000B76BC
		// (set) Token: 0x06003840 RID: 14400 RVA: 0x000B94C4 File Offset: 0x000B76C4
		public List<MultiSelectClientStateDataItem> SelectedDataItems { get; set; }

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06003841 RID: 14401 RVA: 0x000B94CD File Offset: 0x000B76CD
		// (set) Token: 0x06003842 RID: 14402 RVA: 0x000B94D5 File Offset: 0x000B76D5
		public IEnumerable<object> Value { get; set; }

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06003843 RID: 14403 RVA: 0x000B94DE File Offset: 0x000B76DE
		// (set) Token: 0x06003844 RID: 14404 RVA: 0x000B94E6 File Offset: 0x000B76E6
		public string Text { get; set; }

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06003845 RID: 14405 RVA: 0x000B94EF File Offset: 0x000B76EF
		// (set) Token: 0x06003846 RID: 14406 RVA: 0x000B94F7 File Offset: 0x000B76F7
		public bool Enabled { get; set; }
	}
}

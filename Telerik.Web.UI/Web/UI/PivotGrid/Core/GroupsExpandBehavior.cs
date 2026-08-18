using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006EA RID: 1770
	public class GroupsExpandBehavior : IItemExpandBehavior<ExpandBehaviorParameters>
	{
		// Token: 0x06003F00 RID: 16128 RVA: 0x000C8851 File Offset: 0x000C6A51
		public GroupsExpandBehavior()
		{
			this.Expanded = true;
			this.UpToLevel = int.MaxValue;
		}

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x06003F01 RID: 16129 RVA: 0x000C886B File Offset: 0x000C6A6B
		// (set) Token: 0x06003F02 RID: 16130 RVA: 0x000C8873 File Offset: 0x000C6A73
		public int UpToLevel { get; set; }

		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x06003F03 RID: 16131 RVA: 0x000C887C File Offset: 0x000C6A7C
		// (set) Token: 0x06003F04 RID: 16132 RVA: 0x000C8884 File Offset: 0x000C6A84
		public bool Expanded { get; set; }

		// Token: 0x06003F05 RID: 16133 RVA: 0x000C8890 File Offset: 0x000C6A90
		public bool IsExpanded(ExpandBehaviorParameters parameter)
		{
			IGroup item = parameter.Item;
			if (item.Level >= this.UpToLevel)
			{
				return !this.Expanded;
			}
			return this.Expanded;
		}
	}
}

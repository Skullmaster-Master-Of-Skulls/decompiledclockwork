using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001210 RID: 4624
	public class TreeListDeselectEventArgs : TreeListCommandEventArgs
	{
		// Token: 0x0600BF21 RID: 48929 RVA: 0x002A553B File Offset: 0x002A373B
		public TreeListDeselectEventArgs(TreeListItem item, object commandSource, object argument) : base(item, commandSource, "Deselect", argument)
		{
		}

		// Token: 0x0600BF22 RID: 48930 RVA: 0x002A554C File Offset: 0x002A374C
		public override void ExecuteCommand(object source)
		{
			RadTreeList ownerTreeList = this.Item.OwnerTreeList;
			if (this.Canceled)
			{
				return;
			}
			TreeListDataItem treeListDataItem = this.Item as TreeListDataItem;
			if (treeListDataItem != null)
			{
				treeListDataItem.Selected = false;
			}
		}
	}
}

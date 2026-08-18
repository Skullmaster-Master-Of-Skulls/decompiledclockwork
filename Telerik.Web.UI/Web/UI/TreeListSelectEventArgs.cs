using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200121B RID: 4635
	public class TreeListSelectEventArgs : TreeListCommandEventArgs
	{
		// Token: 0x0600BF4E RID: 48974 RVA: 0x002A5A14 File Offset: 0x002A3C14
		public TreeListSelectEventArgs(TreeListItem item, object commandSource, object argument) : base(item, commandSource, "Select", argument)
		{
		}

		// Token: 0x0600BF4F RID: 48975 RVA: 0x002A5A24 File Offset: 0x002A3C24
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
				treeListDataItem.Selected = true;
			}
			ownerTreeList.CallOnSelectedIndexChanged(EventArgs.Empty);
		}
	}
}

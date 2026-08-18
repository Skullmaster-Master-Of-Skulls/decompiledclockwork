using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02001211 RID: 4625
	public class TreeListExpandCollapseEventArgs : TreeListCommandEventArgs
	{
		// Token: 0x0600BF23 RID: 48931 RVA: 0x002A5584 File Offset: 0x002A3784
		public TreeListExpandCollapseEventArgs(TreeListItem item, object commandSource, object argument) : base(item, commandSource, "ExpandCollapse", argument)
		{
		}

		// Token: 0x0600BF24 RID: 48932 RVA: 0x002A5594 File Offset: 0x002A3794
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
				if (this.Item.OwnerTreeList.AllowLoadOnDemand && this.Item.OwnerTreeList.ExpandedIndexes.Contains(treeListDataItem.HierarchyIndex))
				{
					Hashtable hashtable = new Hashtable();
					foreach (string text in this.Item.OwnerTreeList.DataKeyNames)
					{
						hashtable[text] = treeListDataItem.GetDataKeyValue(text);
					}
					if (this.Item.OwnerTreeList.LoadOnDemandContext.ItemNeedsToBeExpanded(hashtable, null))
					{
						treeListDataItem.Expanded = !treeListDataItem.Expanded;
					}
					else
					{
						this.Item.OwnerTreeList.ExpandedIndexes.Remove(treeListDataItem.HierarchyIndex);
						treeListDataItem.Expanded = true;
					}
				}
				else
				{
					treeListDataItem.Expanded = !treeListDataItem.Expanded;
				}
				ownerTreeList._treeListInitializedExpandCollapseIndexes.Add(treeListDataItem.HierarchyIndex);
			}
			if (!ownerTreeList.EnableViewState)
			{
				ownerTreeList.DataSource = null;
			}
			TreeListRebindReason rebindReason = TreeListRebindReason.PostBackEvent;
			ownerTreeList.ObtainDataSource(rebindReason);
			ownerTreeList.DataBind();
		}
	}
}

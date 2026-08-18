using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001248 RID: 4680
	internal class TreeListSelectedItemDecorator : TreeListItemDecorator
	{
		// Token: 0x0600C0F0 RID: 49392 RVA: 0x002AFDDB File Offset: 0x002ADFDB
		public TreeListSelectedItemDecorator(TreeListItem item) : base(item)
		{
		}

		// Token: 0x0600C0F1 RID: 49393 RVA: 0x002AFDE4 File Offset: 0x002ADFE4
		protected override void SetItemStyle(RadTreeList owner)
		{
			TableItemStyle tableItemStyle = new TableItemStyle();
			TreeListDataItem treeListDataItem = base.Item as TreeListDataItem;
			if (treeListDataItem != null)
			{
				if (treeListDataItem.IsAlternatingItem())
				{
					tableItemStyle.CopyFrom(owner.AlternatingItemStyle);
				}
				else
				{
					tableItemStyle.CopyFrom(owner.ItemStyle);
				}
			}
			tableItemStyle.MergeWith(owner.SelectedItemStyle);
			if (!string.IsNullOrEmpty(owner.SelectedItemStyle.CssClass))
			{
				tableItemStyle.CssClass = string.Format("{0} {1}", tableItemStyle.CssClass, owner.SelectedItemStyle.CssClass);
			}
			base.Item.MergeStyle(tableItemStyle);
			if (base.Item is TreeListDataItem)
			{
				base.Item.Attributes["id"] = this.GetRowID();
			}
		}
	}
}

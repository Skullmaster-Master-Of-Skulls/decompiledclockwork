using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001245 RID: 4677
	internal class TreeListNoRecordsItemDecorator : TreeListItemDecorator
	{
		// Token: 0x0600C0E7 RID: 49383 RVA: 0x002AFB1E File Offset: 0x002ADD1E
		public TreeListNoRecordsItemDecorator(TreeListItem item) : base(item)
		{
		}

		// Token: 0x0600C0E8 RID: 49384 RVA: 0x002AFB27 File Offset: 0x002ADD27
		protected override void SetItemStyle(RadTreeList owner)
		{
			base.Item.MergeStyle(owner.ItemStyle);
		}

		// Token: 0x0600C0E9 RID: 49385 RVA: 0x002AFB3C File Offset: 0x002ADD3C
		public override void DecorateItem(RadTreeList owner, TreeListColumn[] columnArray)
		{
			this.SetItemStyle(owner);
			int num = columnArray.Length;
			foreach (TreeListColumn treeListColumn in columnArray)
			{
				if (!treeListColumn.Visible || !treeListColumn.Display)
				{
					num--;
				}
			}
			((TreeListNoRecordsItem)base.Item).NoRecordContentCell.ColumnSpan = owner.MostNestedIndex + num + 1;
			((TreeListNoRecordsItem)base.Item).NoRecordContentCell.CssClass = "rtlROut";
		}
	}
}

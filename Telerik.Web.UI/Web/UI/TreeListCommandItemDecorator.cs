using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000962 RID: 2402
	internal class TreeListCommandItemDecorator : TreeListItemDecorator
	{
		// Token: 0x06005B75 RID: 23413 RVA: 0x00116BD3 File Offset: 0x00114DD3
		public TreeListCommandItemDecorator(TreeListItem item) : base(item)
		{
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x00116BDC File Offset: 0x00114DDC
		protected override void SetItemStyle(RadTreeList owner)
		{
			base.Item.MergeStyle(owner.CommandItemStyle);
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x00116BF0 File Offset: 0x00114DF0
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
			((TreeListCommandItem)base.Item).CommandItemContentCell.ColumnSpan = (owner.HasStaticHeaders ? 0 : (owner.MostNestedIndex + num + 1));
			((TreeListCommandItem)base.Item).CommandItemContentCell.CssClass = "rtlCommandCell";
		}
	}
}

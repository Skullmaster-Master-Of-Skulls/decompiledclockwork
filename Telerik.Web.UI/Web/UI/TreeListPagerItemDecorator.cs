using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001246 RID: 4678
	internal class TreeListPagerItemDecorator : TreeListItemDecorator
	{
		// Token: 0x0600C0EA RID: 49386 RVA: 0x002AFBB5 File Offset: 0x002ADDB5
		public TreeListPagerItemDecorator(TreeListItem item) : base(item)
		{
		}

		// Token: 0x0600C0EB RID: 49387 RVA: 0x002AFBBE File Offset: 0x002ADDBE
		protected override void SetItemStyle(RadTreeList owner)
		{
			base.Item.MergeStyle(owner.PagerStyle);
		}

		// Token: 0x0600C0EC RID: 49388 RVA: 0x002AFBD4 File Offset: 0x002ADDD4
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
			((TreeListPagerItem)base.Item).PagerContentCell.ColumnSpan = ((owner.HasStaticHeaders && owner.RenderMode == RenderMode.Lightweight) ? 0 : (owner.MostNestedIndex + num + 1));
			((TreeListPagerItem)base.Item).PagerContentCell.CssClass = "rtlPagerCell";
		}
	}
}

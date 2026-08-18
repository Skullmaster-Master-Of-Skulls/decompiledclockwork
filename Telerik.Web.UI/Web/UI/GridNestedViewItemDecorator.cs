using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001121 RID: 4385
	internal class GridNestedViewItemDecorator : GridItemDecorator
	{
		// Token: 0x0600B346 RID: 45894 RVA: 0x0027086A File Offset: 0x0026EA6A
		public GridNestedViewItemDecorator(GridItem item) : base(item)
		{
		}

		// Token: 0x0600B347 RID: 45895 RVA: 0x00270874 File Offset: 0x0026EA74
		public override void SetItemVisibility(GridTableView owner, GridColumn[] columnArray)
		{
			int num = 0;
			foreach (GridColumn gridColumn in columnArray)
			{
				if (gridColumn is GridExpandColumn && gridColumn == base.Item.OwnerTableView.GetAutoExpandColumn() && !gridColumn.Visible)
				{
					base.Item.Cells[num].Visible = false;
				}
				else
				{
					num++;
				}
			}
			if (base.Item != null && base.Item is GridNestedViewItem && ((GridNestedViewItem)base.Item).ParentItem.Visible && (base.Item.OwnerTableView.HierarchyLoadMode == GridChildLoadMode.Client || ((GridNestedViewItem)base.Item).ParentItem.ConditionalExpanded))
			{
				base.Item.Visible = true;
			}
		}

		// Token: 0x0600B348 RID: 45896 RVA: 0x0027093C File Offset: 0x0026EB3C
		public override void DecorateItem(GridTableView owner, GridColumn[] columnArray)
		{
			int num = 0;
			int i = 0;
			while (i < columnArray.Length)
			{
				GridColumn gridColumn = columnArray[i];
				if (gridColumn is GridGroupSplitterColumn)
				{
					if (string.IsNullOrEmpty(base.Item.OwnerTableView.OwnerGrid.RuntimeSkin))
					{
						base.Item.Cells[num].MergeStyle(base.Item.OwnerTableView.RenderGroupHeaderItemStyle);
						base.AddColorToStyle(base.Item.Cells[num].Style, "border-bottom-color", base.Item.OwnerTableView.RenderGroupHeaderItemStyle.BackColor);
						base.AddColorToStyle(base.Item.Cells[num].Style, "border-top-color", base.Item.OwnerTableView.RenderGroupHeaderItemStyle.BackColor);
						base.AddColorToStyle(base.Item.Cells[num].Style, "border-left-color", base.Item.OwnerTableView.RenderGroupHeaderItemStyle.BorderColor);
						base.AddColorToStyle(base.Item.Cells[num].Style, "border-right-color", base.Item.OwnerTableView.RenderGroupHeaderItemStyle.BorderColor);
					}
					else
					{
						base.Item.Cells[num].CssClass = "rgGroupCol";
					}
					base.Item.Cells[num].Height = Unit.Empty;
				}
				if (!(gridColumn is GridExpandColumn) || gridColumn != base.Item.OwnerTableView.GetAutoExpandColumn())
				{
					goto IL_246;
				}
				if (gridColumn.Visible)
				{
					base.Item.Cells[num].Style.Add("border-right-width", "0");
					int index = num + 1;
					base.Item.Cells[index].Style.Add("border-left-width", "0");
					base.Item.Cells[index].Style.Add("overflow", "visible");
					base.Item.Cells[num].CssClass = "rgExpandCol";
					goto IL_246;
				}
				base.Item.Cells[num].Visible = false;
				IL_24A:
				i++;
				continue;
				IL_246:
				num++;
				goto IL_24A;
			}
			GridNestedViewItem gridNestedViewItem = base.Item as GridNestedViewItem;
			GridDataItem gridDataItem = null;
			if (gridNestedViewItem != null)
			{
				gridDataItem = gridNestedViewItem.ParentItem;
			}
			if ((base.Item != null && !base.Item.Display) || (gridDataItem != null && !gridDataItem.Expanded && gridDataItem.ConditionalExpanded))
			{
				base.Item.Style["display"] = "none";
			}
		}
	}
}

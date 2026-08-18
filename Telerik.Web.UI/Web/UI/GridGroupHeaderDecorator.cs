using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001123 RID: 4387
	internal class GridGroupHeaderDecorator : GridItemDecorator
	{
		// Token: 0x0600B34D RID: 45901 RVA: 0x00270EB0 File Offset: 0x0026F0B0
		public GridGroupHeaderDecorator(GridItem item) : base(item)
		{
		}

		// Token: 0x0600B34E RID: 45902 RVA: 0x00270EB9 File Offset: 0x0026F0B9
		public override void SetItemVisibility(GridTableView owner, GridColumn[] columnArray)
		{
		}

		// Token: 0x0600B34F RID: 45903 RVA: 0x00270EBC File Offset: 0x0026F0BC
		public override void DecorateItem(GridTableView owner, GridColumn[] columnArray)
		{
			this.SetItemStyle(owner);
			int num = 0;
			Color backColor = base.Item.OwnerTableView.RenderGroupHeaderItemStyle.BackColor;
			Color borderColor = base.Item.OwnerTableView.RenderGroupHeaderItemStyle.BorderColor;
			foreach (GridColumn gridColumn in columnArray)
			{
				GridGroupSplitterColumn gridGroupSplitterColumn = gridColumn as GridGroupSplitterColumn;
				if (gridGroupSplitterColumn != null)
				{
					gridColumn.PrepareCell(base.Item.Cells[num], base.Item);
					if (gridGroupSplitterColumn.CorrespondingExpression.Index == base.Item.GroupLevel)
					{
						if (string.IsNullOrEmpty(owner.OwnerGrid.RuntimeSkin))
						{
							base.AddColorToStyle(base.Item.Cells[num].Style, "border-bottom-color", backColor);
							base.AddColorToStyle(base.Item.Cells[num].Style, "border-top-color", borderColor);
							base.AddColorToStyle(base.Item.Cells[num].Style, "border-left-color", borderColor);
							base.AddColorToStyle(base.Item.Cells[num].Style, "border-right-color", backColor);
						}
						base.AddColorToStyle(base.Item.Cells[num + 1].Style, "border-bottom-color", borderColor);
						base.AddColorToStyle(base.Item.Cells[num + 1].Style, "border-top-color", borderColor);
						base.AddColorToStyle(base.Item.Cells[num + 1].Style, "border-left-color", backColor);
						base.AddColorToStyle(base.Item.Cells[num + 1].Style, "border-right-color", borderColor);
						break;
					}
				}
				if (string.IsNullOrEmpty(owner.OwnerGrid.RuntimeSkin))
				{
					base.AddColorToStyle(base.Item.Cells[num].Style, "border-bottom-color", backColor);
					base.AddColorToStyle(base.Item.Cells[num].Style, "border-top-color", backColor);
					base.AddColorToStyle(base.Item.Cells[num].Style, "border-left-color", borderColor);
					base.AddColorToStyle(base.Item.Cells[num].Style, "border-right-color", borderColor);
				}
				num++;
			}
			if (base.Item != null && !base.Item.Display)
			{
				base.Item.Style["display"] = "none";
			}
			TableCell dataCell = ((GridGroupHeaderItem)base.Item).DataCell;
			if (owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				GridGroupHeaderItem gridGroupHeaderItem = dataCell.Parent as GridGroupHeaderItem;
				if (gridGroupHeaderItem != null && gridGroupHeaderItem.OwnerTableView.GroupHeaderTemplate != null)
				{
					return;
				}
				if (dataCell.Controls.Count == 0)
				{
					string[] array = dataCell.Text.Split(new char[]
					{
						':'
					});
					if (array.Length == 2 && !owner.OwnerGrid.IsExporting)
					{
						dataCell.Text = string.Format("<span class='rgGroupHeaderText'>{0}:{1}</span>", array[0], array[1]);
					}
				}
				return;
			}
			else if (base.Item.OwnerTableView.OwnerGrid.ClientSettings.Scrolling.AllowScroll && base.Item.OwnerTableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders)
			{
				dataCell.Style["border-right"] = "0";
				if (dataCell.Controls.Count == 0)
				{
					dataCell.Text = string.Format("<div style=\"width:100%;height:100%;white-space:nowrap;position:relative;{1}\"><div style=\"position:absolute;\"><div style=\"position:relative;\">{0}</div></div></div>", dataCell.Text, base.Item.OwnerTableView.OwnerGrid.EmptySkin() ? "top:-0.6em" : "");
					return;
				}
				if (base.Item.OwnerTableView.OwnerGrid.EmptySkin())
				{
					dataCell.Controls.AddAt(0, new LiteralControl("<div style=\"width:100%;height:100%;white-space:nowrap;position:relative;top:-0.6em\"><div style=\"position:absolute;\"><div style=\"position:relative;\">"));
				}
				else
				{
					dataCell.Controls.AddAt(0, new LiteralControl("<div style=\"width:100%;height:100%;white-space:nowrap;position:relative;\"><div style=\"position:absolute;\"><div style=\"position:relative;\">"));
				}
				dataCell.Controls.Add(new LiteralControl("</div></div></div>"));
				return;
			}
			else
			{
				GridGroupHeaderItem gridGroupHeaderItem2 = dataCell.Parent as GridGroupHeaderItem;
				if (gridGroupHeaderItem2 != null && gridGroupHeaderItem2.OwnerTableView.GroupHeaderTemplate != null)
				{
					return;
				}
				if (dataCell.Controls.Count != 0)
				{
					dataCell.Controls.AddAt(0, new LiteralControl("<p>"));
					dataCell.Controls.Add(new LiteralControl("</p>"));
					return;
				}
				string[] array2 = dataCell.Text.Split(new char[]
				{
					':'
				});
				if (owner.OwnerGrid.IsExporting)
				{
					return;
				}
				if (owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile && array2.Length == 2)
				{
					dataCell.Text = string.Format("<p><span class='rgGroupHeaderText'>{0}:</span>{1}</p>", array2[0], array2[1]);
					return;
				}
				dataCell.Text = string.Format("<p>{0}</p>", dataCell.Text);
				return;
			}
		}

		// Token: 0x0600B350 RID: 45904 RVA: 0x002713DB File Offset: 0x0026F5DB
		public override void SetItemStyle(GridTableView owner)
		{
			base.Item.MergeStyle(base.Item.OwnerTableView.RenderGroupHeaderItemStyle);
		}
	}
}

using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200076C RID: 1900
	public abstract class PivotGridHeaderItem : PivotGridDataItem
	{
		// Token: 0x0600430F RID: 17167 RVA: 0x000D17E0 File Offset: 0x000CF9E0
		public PivotGridHeaderItem(RadPivotGrid ownerPivotGrid, PivotGridItemType itemType, bool isDataBinding) : base(ownerPivotGrid, itemType, isDataBinding)
		{
		}

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x06004310 RID: 17168
		protected abstract string ExpandCollapseButtonIDPrefix { get; }

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x06004311 RID: 17169
		protected abstract string HeaderLabelIDPrefix { get; }

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x06004312 RID: 17170
		protected abstract string CommandArgPrefix { get; }

		// Token: 0x06004313 RID: 17171 RVA: 0x000D17EC File Offset: 0x000CF9EC
		internal void InitializeExpandCollapseButton(PivotGridModelCell modelCell, PivotGridHeaderCell cell)
		{
			if (modelCell.ShouldCreateExpandCollapseButton)
			{
				Button button = null;
				LinkButton linkButton = null;
				if (base.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					linkButton = base.CreateExpandCollapseLightweightButton(this.ExpandCollapseButtonIDPrefix + modelCell.Slot.ToString() + "_" + modelCell.GroupLevel.ToString());
					linkButton.CommandArgument = this.CommandArgPrefix + modelCell.Slot.ToString() + "_" + modelCell.GroupLevel.ToString();
				}
				else
				{
					button = base.CreateExpandCollapseButton(this.ExpandCollapseButtonIDPrefix + modelCell.Slot.ToString() + "_" + modelCell.GroupLevel.ToString());
					button.CommandArgument = this.CommandArgPrefix + modelCell.Slot.ToString() + "_" + modelCell.GroupLevel.ToString();
				}
				PivotGridClientMessages clientMessages = base.OwnerPivotGrid.ClientSettings.ClientMessages;
				if (modelCell.IsCollapsed)
				{
					if (base.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Lightweight)
					{
						linkButton.CssClass = "rpgIcon rpgExpandIcon";
						if (!string.IsNullOrEmpty(clientMessages.ExpandButtonToolTip))
						{
							linkButton.ToolTip = clientMessages.ExpandButtonToolTip;
							if (base.OwnerPivotGrid.EnableAriaSupport)
							{
								linkButton.Attributes.Add("aria-label", linkButton.ToolTip);
							}
						}
					}
					else
					{
						button.CssClass = "rpgExpand";
						if (!string.IsNullOrEmpty(clientMessages.ExpandButtonToolTip))
						{
							button.ToolTip = clientMessages.ExpandButtonToolTip;
						}
					}
				}
				else if (base.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					linkButton.CssClass = "rpgIcon rpgCollapseIcon";
					if (!string.IsNullOrEmpty(clientMessages.CollapseButtonToolTip))
					{
						linkButton.ToolTip = clientMessages.CollapseButtonToolTip;
						if (base.OwnerPivotGrid.EnableAriaSupport)
						{
							linkButton.Attributes.Add("aria-label", linkButton.ToolTip);
						}
					}
				}
				else
				{
					button.CssClass = "rpgCollapse";
					if (!string.IsNullOrEmpty(clientMessages.CollapseButtonToolTip))
					{
						button.ToolTip = clientMessages.CollapseButtonToolTip;
					}
				}
				if (base.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					cell.Controls.Add(linkButton);
					return;
				}
				cell.Controls.Add(button);
			}
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x000D1A30 File Offset: 0x000CFC30
		internal void InitializeExpandCollapseLabel(PivotGridModelCell modelCell, PivotGridHeaderCell cell)
		{
			if (!cell.HasInstantiatedTemplate && modelCell.ShouldCreateExpandCollapseButton)
			{
				Literal literal = new Literal();
				cell.Controls.Add(literal);
				literal.ID = this.HeaderLabelIDPrefix + modelCell.Slot.ToString() + "_" + modelCell.GroupLevel.ToString();
			}
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x000D1A91 File Offset: 0x000CFC91
		internal void SetSpansOnCell(PivotGridModelCell modelCell, PivotGridHeaderCell cell)
		{
			if (modelCell.RowSpan > 1)
			{
				cell.RowSpan = modelCell.RowSpan;
			}
			if (modelCell.ColSpan > 1)
			{
				cell.ColumnSpan = modelCell.ColSpan;
			}
		}

		// Token: 0x06004316 RID: 17174 RVA: 0x000D1ABD File Offset: 0x000CFCBD
		internal void CopyProperties(PivotGridModelCell modelCell, PivotGridHeaderCell cell)
		{
			cell.HasChildren = modelCell.HasChildren;
			cell.IsTotalCell = modelCell.IsTotalCell;
			cell.IsGrandTotalCell = modelCell.IsGrandTotalCell;
			cell.Slot = modelCell.Slot;
			cell.GroupLevel = modelCell.GroupLevel;
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x000D1AFC File Offset: 0x000CFCFC
		protected override void SetCellText(PivotGridCell cell, string text)
		{
			PivotGridHeaderCell pivotGridHeaderCell = cell as PivotGridHeaderCell;
			Literal literal = cell.FindControl(this.HeaderLabelIDPrefix + pivotGridHeaderCell.Slot.ToString() + "_" + pivotGridHeaderCell.GroupLevel.ToString()) as Literal;
			if (literal != null)
			{
				literal.Text = text;
				return;
			}
			if (cell.Controls.Count == 0)
			{
				if (string.IsNullOrEmpty(text) && !base.OwnerPivotGrid.RenderEmptyStringInDataCells)
				{
					text = "&nbsp;";
				}
				pivotGridHeaderCell.Text = text;
			}
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x000D1B84 File Offset: 0x000CFD84
		protected override string GetFormatString(PivotGridField field, bool isTotalCell, bool isGrandTotalCell)
		{
			string result = string.Empty;
			if (field != null)
			{
				result = field.DataFormatString;
				if (isTotalCell && !string.IsNullOrEmpty(field.TotalFormatString))
				{
					result = field.TotalFormatString;
				}
			}
			return result;
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x000D1BBC File Offset: 0x000CFDBC
		protected string FormatDataValue(PivotGridHeaderCell hCell)
		{
			string result = hCell.DataItem.ToString();
			string formatString = this.GetFormatString(hCell.Field, hCell.IsTotalCell, hCell.IsGrandTotalCell);
			if (string.Empty != formatString)
			{
				result = string.Format(formatString, hCell.DataItem);
			}
			return result;
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x000D1C0C File Offset: 0x000CFE0C
		protected void OnCellDataBinding(object sender, EventArgs e)
		{
			PivotGridHeaderCell pivotGridHeaderCell = sender as PivotGridHeaderCell;
			string text = this.FormatDataValue(pivotGridHeaderCell);
			if (base.OwnerPivotGrid.EnableToolTips)
			{
				if (pivotGridHeaderCell.IsGrandTotalCell)
				{
					pivotGridHeaderCell.ToolTip = text;
				}
				else
				{
					pivotGridHeaderCell.ToolTip = pivotGridHeaderCell.GetToolTipString();
				}
			}
			if (!pivotGridHeaderCell.HasInstantiatedTemplate)
			{
				this.SetCellText(pivotGridHeaderCell, text);
			}
		}
	}
}

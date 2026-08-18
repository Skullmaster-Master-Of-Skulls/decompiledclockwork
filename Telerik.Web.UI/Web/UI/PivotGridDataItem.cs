using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI.PivotGrid.Core.Aggregates;

namespace Telerik.Web.UI
{
	// Token: 0x0200076B RID: 1899
	public class PivotGridDataItem : PivotGridItem, IDataItemContainer, INamingContainer
	{
		// Token: 0x170015CF RID: 5583
		// (get) Token: 0x060042F6 RID: 17142 RVA: 0x000D0E2A File Offset: 0x000CF02A
		// (set) Token: 0x060042F7 RID: 17143 RVA: 0x000D0E32 File Offset: 0x000CF032
		internal int? FirstGrandTotalCellIndex { get; set; }

		// Token: 0x170015D0 RID: 5584
		// (get) Token: 0x060042F8 RID: 17144 RVA: 0x000D0E3B File Offset: 0x000CF03B
		// (set) Token: 0x060042F9 RID: 17145 RVA: 0x000D0E43 File Offset: 0x000CF043
		internal int? FirstGrandTotalRowIndex { get; set; }

		// Token: 0x060042FA RID: 17146 RVA: 0x000D0E4C File Offset: 0x000CF04C
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public PivotGridDataItem(RadPivotGrid ownerPivotGrid, PivotGridItemType itemType, bool isDataBinding) : base(ownerPivotGrid, itemType, isDataBinding)
		{
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x000D0E57 File Offset: 0x000CF057
		public PivotGridDataItem(RadPivotGrid ownerPivotGrid, int displayIndex, bool isDataBinding) : this(ownerPivotGrid, PivotGridItemType.Item, isDataBinding)
		{
			this.DisplayIndex = displayIndex;
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x000D0E69 File Offset: 0x000CF069
		protected virtual PivotGridCell CreateCell()
		{
			return new PivotGridDataCell(base.OwnerPivotGrid);
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x000D0EA4 File Offset: 0x000CF0A4
		internal virtual void InitializeCell(PivotGridCell pivotGridCell, PivotGridModelCellBase modelBaseCell, PivotGridDataItem row, bool dataBinding)
		{
			PivotGridDataCell pivotGridDataCell = pivotGridCell as PivotGridDataCell;
			PivotGridModelDataCell modelCell = modelBaseCell as PivotGridModelDataCell;
			if (!dataBinding)
			{
				PivotGridField pivotGridField = (from f in base.OwnerPivotGrid.Fields
				where f.UniqueName == modelCell.FieldName && !f.IsHidden
				select f).FirstOrDefault<PivotGridField>();
				if (pivotGridField != null)
				{
					pivotGridDataCell.Field = (modelCell.Field = pivotGridField);
				}
			}
			PivotGridAggregateField pivotGridAggregateField = modelCell.Field as PivotGridAggregateField;
			row.CallOnCellCreated(pivotGridDataCell);
			if (pivotGridAggregateField != null)
			{
				if (modelCell.CellType == PivotGridDataCellType.DataCell && pivotGridAggregateField.CellTemplate != null)
				{
					pivotGridDataCell.TemplateType = TemplateType.Aggregate;
				}
				else if (modelCell.CellType == PivotGridDataCellType.RowTotalDataCell && pivotGridAggregateField.RowTotalCellTemplate != null)
				{
					pivotGridDataCell.TemplateType = TemplateType.AggregateRowTotal;
				}
				else if (modelCell.CellType == PivotGridDataCellType.ColumnTotalDataCell && pivotGridAggregateField.ColumnTotalCellTemplate != null)
				{
					pivotGridDataCell.TemplateType = TemplateType.AggregateColumnTotal;
				}
				else if (modelCell.CellType == PivotGridDataCellType.RowAndColumnTotal && pivotGridAggregateField.RowAndColumnTotalCellTemplate != null)
				{
					pivotGridDataCell.TemplateType = TemplateType.AggregateRowAndColumnTotal;
				}
				else if ((modelCell.CellType == PivotGridDataCellType.RowGrandTotalDataCell || modelCell.CellType == PivotGridDataCellType.RowGrandTotalColumnTotal) && pivotGridAggregateField.RowGrandTotalCellTemplate != null)
				{
					pivotGridDataCell.TemplateType = TemplateType.AggregateRowGrandTotal;
				}
				else if ((modelCell.CellType == PivotGridDataCellType.ColumnGrandTotalDataCell || modelCell.CellType == PivotGridDataCellType.ColumnGrandTotalRowTotal) && pivotGridAggregateField.ColumnGrandTotalCellTemplate != null)
				{
					pivotGridDataCell.TemplateType = TemplateType.AggregateColumnGrandTotal;
				}
				else if (modelCell.CellType == PivotGridDataCellType.RowAndColumnGrandTotal && pivotGridAggregateField.RowAndColumnGrandTotalCellTemplate != null)
				{
					pivotGridDataCell.TemplateType = TemplateType.AggregateRowAndColumnGrandTotal;
				}
			}
			if (dataBinding)
			{
				if (modelCell.DisplayValueAsKpi)
				{
					if (base.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Lightweight)
					{
						this.InitializeLightweightKpiControls(modelCell, pivotGridDataCell);
					}
					else
					{
						this.InitializeKpiControls(modelCell, pivotGridDataCell);
					}
				}
				else
				{
					pivotGridDataCell.FormattedValue = modelCell.FormattedValue;
				}
				pivotGridDataCell.DataItem = ((modelCell.Name == null) ? null : modelCell.Name);
				pivotGridDataCell.Field = ((modelCell.Field == null) ? null : modelCell.Field);
				pivotGridDataCell.ParentRowIndexes = modelCell.RowIndexes;
				pivotGridDataCell.ParentColumnIndexes = modelCell.ColumnIndexes;
			}
			else if (modelCell.DisplayValueAsKpi)
			{
				if (base.OwnerPivotGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					this.InitializeLightweightKpiControls(modelCell, pivotGridDataCell);
				}
				else
				{
					this.InitializeKpiControls(modelCell, pivotGridDataCell);
				}
			}
			pivotGridDataCell.CellType = modelCell.CellType;
			if (pivotGridDataCell.TemplateType != TemplateType.None)
			{
				pivotGridDataCell.Text = string.Empty;
			}
			if (pivotGridDataCell.TemplateType == TemplateType.Aggregate)
			{
				pivotGridAggregateField.CellTemplate.InstantiateIn(pivotGridDataCell);
			}
			else if (pivotGridDataCell.TemplateType == TemplateType.AggregateColumnGrandTotal)
			{
				pivotGridAggregateField.ColumnGrandTotalCellTemplate.InstantiateIn(pivotGridDataCell);
			}
			else if (pivotGridDataCell.TemplateType == TemplateType.AggregateRowGrandTotal)
			{
				pivotGridAggregateField.RowGrandTotalCellTemplate.InstantiateIn(pivotGridDataCell);
			}
			else if (pivotGridDataCell.TemplateType == TemplateType.AggregateColumnTotal)
			{
				pivotGridAggregateField.ColumnTotalCellTemplate.InstantiateIn(pivotGridDataCell);
			}
			else if (pivotGridDataCell.TemplateType == TemplateType.AggregateRowTotal)
			{
				pivotGridAggregateField.RowTotalCellTemplate.InstantiateIn(pivotGridDataCell);
			}
			else if (pivotGridDataCell.TemplateType == TemplateType.AggregateRowAndColumnTotal)
			{
				pivotGridAggregateField.RowAndColumnTotalCellTemplate.InstantiateIn(pivotGridDataCell);
			}
			else if (pivotGridDataCell.TemplateType == TemplateType.AggregateRowGrandTotal)
			{
				pivotGridAggregateField.RowGrandTotalCellTemplate.InstantiateIn(pivotGridDataCell);
			}
			else if (pivotGridDataCell.TemplateType == TemplateType.AggregateColumnGrandTotal)
			{
				pivotGridAggregateField.ColumnGrandTotalCellTemplate.InstantiateIn(pivotGridDataCell);
			}
			else if (pivotGridDataCell.TemplateType == TemplateType.AggregateRowAndColumnGrandTotal)
			{
				pivotGridAggregateField.RowAndColumnGrandTotalCellTemplate.InstantiateIn(pivotGridDataCell);
			}
			if (pivotGridDataCell.TemplateType != TemplateType.None)
			{
				pivotGridDataCell.HasInstantiatedTemplate = true;
			}
			pivotGridDataCell.DataBinding += this.cell_DataBinding;
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x000D1260 File Offset: 0x000CF460
		internal void Initialize(PivotGridModelRowBase item)
		{
			int num = 0;
			foreach (PivotGridModelCellBase pivotGridModelCellBase in item.Cells)
			{
				PivotGridCell pivotGridCell = this.CreateCell();
				PivotGridDataCell pivotGridDataCell = pivotGridCell as PivotGridDataCell;
				if (pivotGridDataCell != null)
				{
					pivotGridDataCell.RowIndex = this.DisplayIndex;
					pivotGridDataCell.ColumnIndex = num;
					num++;
				}
				pivotGridModelCellBase.DataCell = pivotGridCell;
				this.Cells.Add(pivotGridCell);
				this.InitializeCell(pivotGridCell, pivotGridModelCellBase, this, this.IsDataBinding);
			}
			this.CallOnItemCreated();
			base.OwnerPivotGrid.Items.Add(this);
			if (this.IsDataBinding)
			{
				this.DataBind();
				foreach (object obj in this.Cells)
				{
					PivotGridCell pivotGridCell2 = obj as PivotGridCell;
					if (pivotGridCell2 != null)
					{
						this.CallOnCellDataBound(pivotGridCell2);
					}
				}
				this.CallOnItemDataBound();
			}
		}

		// Token: 0x060042FF RID: 17151 RVA: 0x000D1380 File Offset: 0x000CF580
		protected Button CreateExpandCollapseButton(string id)
		{
			return new Button
			{
				ID = id,
				Text = " ",
				CommandName = "ExpandCollapse",
				CausesValidation = false
			};
		}

		// Token: 0x06004300 RID: 17152 RVA: 0x000D13B8 File Offset: 0x000CF5B8
		protected LinkButton CreateExpandCollapseLightweightButton(string id)
		{
			return new LinkButton
			{
				ID = id,
				Text = " ",
				CommandName = "ExpandCollapse",
				CausesValidation = false
			};
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x000D13F0 File Offset: 0x000CF5F0
		private void InitializeKpiControls(PivotGridModelDataCell modelCell, PivotGridCell cell)
		{
			Label label = new Label();
			string arg = (modelCell.KpiType == PivotGridKpiType.Trend) ? "Trend" : "Status";
			switch (modelCell.KpiIndicator)
			{
			case PivotGridKpiValue.Down:
				label.Attributes.Add("class", string.Format("rpgKpi{0}Down", arg));
				label.Text = "-1";
				break;
			case PivotGridKpiValue.NoChange:
				label.Attributes.Add("class", string.Format("rpgKpi{0}Par", arg));
				label.Text = "0";
				break;
			case PivotGridKpiValue.Up:
				label.Attributes.Add("class", string.Format("rpgKpi{0}Up", arg));
				label.Text = "1";
				break;
			case PivotGridKpiValue.NA:
				label.Attributes.Add("class", "rpgKpiNA");
				label.Text = "NA";
				break;
			}
			cell.Controls.Add(label);
		}

		// Token: 0x06004302 RID: 17154 RVA: 0x000D14E4 File Offset: 0x000CF6E4
		private void InitializeLightweightKpiControls(PivotGridModelDataCell modelCell, PivotGridCell cell)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			string arg = (modelCell.KpiType == PivotGridKpiType.Trend) ? "Trend" : "Status";
			switch (modelCell.KpiIndicator)
			{
			case PivotGridKpiValue.Down:
				htmlGenericControl.Attributes.Add("class", string.Format("rpgIcon rpgKpi{0}DownIcon", arg));
				break;
			case PivotGridKpiValue.NoChange:
				htmlGenericControl.Attributes.Add("class", string.Format("rpgIcon rpgKpi{0}ParIcon", arg));
				break;
			case PivotGridKpiValue.Up:
				htmlGenericControl.Attributes.Add("class", string.Format("rpgIcon rpgKpi{0}UpIcon", arg));
				break;
			case PivotGridKpiValue.NA:
				htmlGenericControl.Attributes.Add("class", "rpgIcon rpgKpiNAIcon");
				break;
			}
			cell.Controls.Add(htmlGenericControl);
		}

		// Token: 0x06004303 RID: 17155 RVA: 0x000D15AC File Offset: 0x000CF7AC
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		protected string FormatDataValue(PivotGridDataCell dataCell)
		{
			object dataItem = dataCell.DataItem;
			string formattedValue = dataCell.FormattedValue;
			string result = formattedValue;
			if (dataItem == null || dataItem == DBNull.Value)
			{
				result = base.OwnerPivotGrid.EmptyValue;
			}
			else if (dataItem is AggregateError)
			{
				result = base.OwnerPivotGrid.ErrorValue;
			}
			else
			{
				string formatString = this.GetFormatString(dataCell.Field, dataCell.IsTotalCell, dataCell.IsGrandTotalCell);
				if (formatString == string.Empty)
				{
					if (string.IsNullOrEmpty(formattedValue))
					{
						result = dataItem.ToString();
					}
				}
				else
				{
					result = string.Format(formatString, dataItem);
				}
			}
			return result;
		}

		// Token: 0x06004304 RID: 17156 RVA: 0x000D1638 File Offset: 0x000CF838
		protected virtual string GetFormatString(PivotGridField field, bool isTotalCell, bool isGrandTotalCell)
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
			PivotGridAggregateField pivotGridAggregateField = field as PivotGridAggregateField;
			if (pivotGridAggregateField != null && isGrandTotalCell && !string.IsNullOrEmpty(pivotGridAggregateField.GrandTotalAggregateFormatString))
			{
				result = pivotGridAggregateField.GrandTotalAggregateFormatString;
			}
			return result;
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x000D1690 File Offset: 0x000CF890
		private void cell_DataBinding(object sender, EventArgs e)
		{
			string text = string.Empty;
			PivotGridDataCell pivotGridDataCell = sender as PivotGridDataCell;
			if (pivotGridDataCell != null)
			{
				text = this.FormatDataValue(pivotGridDataCell);
				if (base.OwnerPivotGrid.EnableToolTips)
				{
					pivotGridDataCell.ToolTip = pivotGridDataCell.GetToolTipString(text);
				}
			}
			if (!pivotGridDataCell.HasInstantiatedTemplate)
			{
				this.SetCellText(sender as PivotGridCell, text);
			}
		}

		// Token: 0x06004306 RID: 17158 RVA: 0x000D16E4 File Offset: 0x000CF8E4
		protected virtual void SetCellText(PivotGridCell cell, string text)
		{
			if (cell.Controls.Count == 0)
			{
				if (string.IsNullOrEmpty(text) && !base.OwnerPivotGrid.RenderEmptyStringInDataCells)
				{
					text = "&nbsp;";
				}
				cell.Text = text;
			}
		}

		// Token: 0x170015D1 RID: 5585
		// (get) Token: 0x06004307 RID: 17159 RVA: 0x000D1716 File Offset: 0x000CF916
		// (set) Token: 0x06004308 RID: 17160 RVA: 0x000D171E File Offset: 0x000CF91E
		public int DataItemIndex { get; internal set; }

		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x06004309 RID: 17161 RVA: 0x000D1727 File Offset: 0x000CF927
		// (set) Token: 0x0600430A RID: 17162 RVA: 0x000D172F File Offset: 0x000CF92F
		public int DisplayIndex { get; protected set; }

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x0600430B RID: 17163 RVA: 0x000D1738 File Offset: 0x000CF938
		// (set) Token: 0x0600430C RID: 17164 RVA: 0x000D173F File Offset: 0x000CF93F
		public bool Selected
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x000D1746 File Offset: 0x000CF946
		protected void SetSelected(bool isSelected)
		{
			if (isSelected)
			{
				base.ItemType = PivotGridItemType.Selected;
			}
			else
			{
				base.ItemType = PivotGridItemType.Item;
			}
			this.SetupDecorator();
		}

		// Token: 0x170015D4 RID: 5588
		// (get) Token: 0x0600430E RID: 17166 RVA: 0x000D1764 File Offset: 0x000CF964
		public object DataItem
		{
			get
			{
				ArrayList arrayList = new ArrayList(this.Cells.Count);
				foreach (object obj in this.Cells)
				{
					PivotGridCell pivotGridCell = obj as PivotGridCell;
					if (pivotGridCell != null)
					{
						arrayList.Add(pivotGridCell.DataItem);
					}
				}
				return arrayList;
			}
		}
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020019F3 RID: 6643
	public class GridCalculatedColumn : GridColumn, IGridDataColumn
	{
		// Token: 0x06010125 RID: 65829 RVA: 0x0039BE54 File Offset: 0x0039A054
		public override GridColumn Clone()
		{
			GridCalculatedColumn gridCalculatedColumn = new GridCalculatedColumn();
			gridCalculatedColumn.CopyBaseProperties(this);
			return gridCalculatedColumn;
		}

		// Token: 0x06010126 RID: 65830 RVA: 0x0039BE70 File Offset: 0x0039A070
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridCalculatedColumn gridCalculatedColumn = (GridCalculatedColumn)fromColumn;
			this.AllowFiltering = gridCalculatedColumn.AllowFiltering;
			this.AllowSorting = gridCalculatedColumn.AllowSorting;
			this.Expression = gridCalculatedColumn.Expression;
			this.Aggregate = gridCalculatedColumn.Aggregate;
			this.DataFields = gridCalculatedColumn.DataFields;
			this.DataFormatString = gridCalculatedColumn.DataFormatString;
			this.FooterAggregateFormatString = gridCalculatedColumn.FooterAggregateFormatString;
		}

		// Token: 0x06010127 RID: 65831 RVA: 0x0039BEDF File Offset: 0x0039A0DF
		internal override string GetSortExpression()
		{
			if (!string.IsNullOrEmpty(this.SortExpression))
			{
				return this.SortExpression;
			}
			if (!string.IsNullOrEmpty(this.UniqueName) && this.AllowSorting)
			{
				return this.GetResultFieldName();
			}
			return "";
		}

		// Token: 0x17004D9B RID: 19867
		// (get) Token: 0x06010128 RID: 65832 RVA: 0x0039BF18 File Offset: 0x0039A118
		// (set) Token: 0x06010129 RID: 65833 RVA: 0x0039BF41 File Offset: 0x0039A141
		[NotifyParentProperty(true)]
		[Category("Data")]
		[DefaultValue(typeof(GridAggregateFunction), "None")]
		[Description("GridBoundColumn aggregate function")]
		public virtual GridAggregateFunction Aggregate
		{
			get
			{
				object obj = base.ViewState["Aggregate"];
				if (obj != null)
				{
					return (GridAggregateFunction)obj;
				}
				return GridAggregateFunction.None;
			}
			set
			{
				base.ViewState["Aggregate"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004D9C RID: 19868
		// (get) Token: 0x0601012A RID: 65834 RVA: 0x0039BF60 File Offset: 0x0039A160
		// (set) Token: 0x0601012B RID: 65835 RVA: 0x0039BFC4 File Offset: 0x0039A1C4
		[Category("Behavior")]
		[DefaultValue("")]
		[Localizable(true)]
		[Description("Sets or gets format string for the footer/group footer aggregate.")]
		[NotifyParentProperty(true)]
		public virtual string FooterAggregateFormatString
		{
			get
			{
				object obj = base.ViewState["FooterAggregateFormatString"];
				if (obj == null)
				{
					return string.Empty;
				}
				if (base.Owner != null && base.Owner.OwnerGrid.ExportSettings.SuppressColumnDataFormatStrings && base.Owner.OwnerGrid.IsExporting)
				{
					return "{0}";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["FooterAggregateFormatString"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x0601012C RID: 65836 RVA: 0x0039BFE0 File Offset: 0x0039A1E0
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			GridGroupFooterItem gridGroupFooterItem = inItem as GridGroupFooterItem;
			if (gridGroupFooterItem == null || gridGroupFooterItem.OwnerTableView.GroupFooterTemplate == null)
			{
				base.InitializeCell(cell, columnIndex, inItem);
				if ((inItem is GridFooterItem || gridGroupFooterItem != null) && this.Aggregate != GridAggregateFunction.None)
				{
					cell.DataBinding += this.footerCell_DataBinding;
				}
				if (inItem.IsDataBound && !(inItem is GridEditFormInsertItem) && !(inItem is GridDataInsertItem) && (this.DataFields.Length != 0 || this.Expression.Length != 0))
				{
					cell.DataBinding += this.cell_DataBinding;
				}
				return;
			}
			if (base.Owner._resolvedDataSource is GridEnumerableFromViewState)
			{
				return;
			}
			if (gridGroupFooterItem != null && this.Aggregate != GridAggregateFunction.None)
			{
				if (base.Owner.OwnerGrid.IsDesignMode)
				{
					return;
				}
				if (this.Aggregate == GridAggregateFunction.Custom)
				{
					GridCustomAggregateEventArgs gridCustomAggregateEventArgs = new GridCustomAggregateEventArgs((GridItem)cell.Parent, this, "");
					base.Owner.OwnerGrid.CallOnCustomAggregate(gridCustomAggregateEventArgs);
					this.PopulateAggragateInGroupFooter(cell, gridCustomAggregateEventArgs.Result);
				}
				if (base.Owner.OwnerGrid.EnableLinqExpressions)
				{
					this.ApplyAggregates35(cell, string.Empty);
					return;
				}
				this.ApplyAggregates(cell, string.Empty);
			}
		}

		// Token: 0x0601012D RID: 65837 RVA: 0x0039C11C File Offset: 0x0039A31C
		private bool PopulateAggragateInGroupFooter(TableCell cell, object result)
		{
			GridGroupFooterItem gridGroupFooterItem = cell.Parent as GridGroupFooterItem;
			if (gridGroupFooterItem != null && gridGroupFooterItem.OwnerTableView.GroupFooterTemplate != null)
			{
				string resultFieldName = this.GetResultFieldName();
				gridGroupFooterItem.AggregatesValues.Add(resultFieldName, result);
				return true;
			}
			return false;
		}

		// Token: 0x0601012E RID: 65838 RVA: 0x0039C15C File Offset: 0x0039A35C
		private void footerCell_DataBinding(object sender, EventArgs e)
		{
			if (base.Owner.OwnerGrid.IsDesignMode)
			{
				return;
			}
			if (base.Owner.ShowFooter || base.Owner.OwnerGrid.ShowFooter || base.Owner.ShowGroupFooter)
			{
				TableCell tableCell = (TableCell)sender;
				string footerText = string.IsNullOrEmpty(this.FooterText) ? string.Format("{0} : ", this.Aggregate.ToString()) : this.FooterText;
				if (!string.IsNullOrEmpty(this.FooterAggregateFormatString) || !string.IsNullOrEmpty(this.DataFormatString))
				{
					footerText = "";
				}
				if (this.Aggregate == GridAggregateFunction.Custom)
				{
					GridCustomAggregateEventArgs gridCustomAggregateEventArgs = new GridCustomAggregateEventArgs((GridItem)tableCell.Parent, this, "");
					base.Owner.OwnerGrid.CallOnCustomAggregate(gridCustomAggregateEventArgs);
					tableCell.Text = this.FormatCellText(footerText, gridCustomAggregateEventArgs.Result);
					return;
				}
				if (base.Owner.OwnerGrid.EnableLinqExpressions)
				{
					this.ApplyAggregates35(tableCell, footerText);
					return;
				}
				this.ApplyAggregates(tableCell, footerText);
			}
		}

		// Token: 0x0601012F RID: 65839 RVA: 0x0039C26C File Offset: 0x0039A46C
		private string FormatCellText(string footerText, object aggregateResult)
		{
			if (base.Owner != null && base.Owner.OwnerGrid.IsExporting && base.Owner.OwnerGrid.ExportSettings.SuppressColumnDataFormatStrings)
			{
				return aggregateResult.ToString();
			}
			if (!string.IsNullOrEmpty(this.FooterAggregateFormatString))
			{
				try
				{
					return string.Format(this.FooterAggregateFormatString, aggregateResult);
				}
				catch
				{
					throw new FormatException(string.Format("Invalid FooterAggregateFormatString for column with UniqueName \"{0}\"", this.UniqueName));
				}
			}
			if (!string.IsNullOrEmpty(this.DataFormatString))
			{
				try
				{
					return string.Format(this.DataFormatString, aggregateResult);
				}
				catch
				{
					throw new FormatException(string.Format("Invalid FooterAggregateFormatString for column with UniqueName \"{0}\"", this.UniqueName));
				}
			}
			return string.Format("{0}{1}", footerText, aggregateResult);
		}

		// Token: 0x06010130 RID: 65840 RVA: 0x0039C344 File Offset: 0x0039A544
		private void ApplyAggregates(TableCell cell, string footerText)
		{
			object obj = null;
			string key = string.Format("GroupedResult{0}", ((GridItem)cell.Parent).GroupLevel);
			DataTable dataTable = (DataTable)((GridEnumerableFromDataView)base.Owner._resolvedDataSource).GroupingDataSet.ExtendedProperties[key];
			if (dataTable == null)
			{
				dataTable = ((GridEnumerableFromDataView)base.Owner._resolvedDataSource)._dataView.Table;
			}
			string filterExpression = base.Owner.FilterExpression;
			string resultFieldName = this.GetResultFieldName();
			if (this.Aggregate == GridAggregateFunction.Count)
			{
				obj = dataTable.DefaultView.Count;
				cell.Text = this.FormatCellText(footerText, dataTable.DefaultView.Count);
			}
			if (this.Aggregate == GridAggregateFunction.First && dataTable.Rows.Count > 0)
			{
				obj = dataTable.DefaultView[0][resultFieldName];
				cell.Text = this.FormatCellText(footerText, dataTable.DefaultView[0][resultFieldName]);
			}
			if (this.Aggregate == GridAggregateFunction.Last && dataTable.Rows.Count > 0)
			{
				obj = dataTable.DefaultView[dataTable.DefaultView.Count - 1][resultFieldName];
				cell.Text = this.FormatCellText(footerText, dataTable.DefaultView[dataTable.DefaultView.Count - 1][resultFieldName]);
			}
			if (this.Aggregate == GridAggregateFunction.Max && dataTable.Rows.Count > 0)
			{
				obj = dataTable.Compute(string.Format("Max({0})", resultFieldName), filterExpression);
				cell.Text = this.FormatCellText(footerText, obj);
			}
			if (this.Aggregate == GridAggregateFunction.Min && dataTable.Rows.Count > 0)
			{
				obj = dataTable.Compute(string.Format("Min({0})", resultFieldName), filterExpression);
				cell.Text = this.FormatCellText(footerText, obj);
			}
			if (this.Aggregate == GridAggregateFunction.Sum && dataTable.Rows.Count > 0)
			{
				obj = dataTable.Compute(string.Format("Sum({0})", resultFieldName), filterExpression);
				cell.Text = this.FormatCellText(footerText, obj);
			}
			if (this.Aggregate == GridAggregateFunction.Avg && dataTable.Rows.Count > 0)
			{
				obj = dataTable.Compute(string.Format("Avg({0})", resultFieldName), filterExpression);
				cell.Text = this.FormatCellText(footerText, obj);
			}
			if (this.Aggregate == GridAggregateFunction.CountDistinct && dataTable.Rows.Count > 0)
			{
				obj = GridBoundColumn.GetDistinctCount(dataTable, resultFieldName);
				cell.Text = this.FormatCellText(footerText, obj);
			}
			this.PopulateAggragateInGroupFooter(cell, obj);
		}

		// Token: 0x06010131 RID: 65841 RVA: 0x0039C5D4 File Offset: 0x0039A7D4
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		private void ApplyAggregates35(TableCell cell, string footerText)
		{
			string key = string.Format("GroupedResult{0}", ((GridItem)cell.Parent).GroupLevel);
			DataTable dataTable = (DataTable)((GridEnumerableFromDataView)base.Owner._resolvedDataSource).GroupingDataSet.ExtendedProperties[key];
			string resultFieldName = this.GetResultFieldName();
			object aggregate;
			if (dataTable != null)
			{
				IEnumerable<DataRow> enumerable = dataTable.AsEnumerable();
				IQueryable<DataRow> queryable = enumerable.AsQueryable<DataRow>();
				aggregate = GridBoundColumn.GetAggregate(enumerable, queryable, resultFieldName, base.DataType, this.Aggregate);
			}
			else
			{
				if (base.Owner.originalEnumerable == null)
				{
					this.ApplyAggregates(cell, footerText);
					return;
				}
				aggregate = GridBoundColumn.GetAggregate(base.Owner.originalEnumerable, base.Owner.originalQueryable, resultFieldName, base.DataType, this.Aggregate);
			}
			if (this.PopulateAggragateInGroupFooter(cell, aggregate))
			{
				return;
			}
			cell.Text = this.FormatCellText(footerText, aggregate);
		}

		// Token: 0x06010132 RID: 65842 RVA: 0x0039C6B8 File Offset: 0x0039A8B8
		private void cell_DataBinding(object sender, EventArgs e)
		{
			string text = "";
			TableCell tableCell = (TableCell)sender;
			GridItem bindingParentItem = GridColumn.GetBindingParentItem(tableCell);
			if (base.Owner.OwnerGrid.EnableLinqExpressions && base.Owner.originalEnumerable != null)
			{
				if (!(bindingParentItem.DataItem is GridInsertionObject))
				{
					text = this.FormatDataValue(DataBinder.Eval(bindingParentItem.DataItem, this.GetResultFieldName()));
				}
			}
			else if (base.DesignMode)
			{
				text = "GridCalculatedColumn";
			}
			else
			{
				DataTable table = ((GridEnumerableFromDataView)base.Owner._resolvedDataSource)._dataView.Table;
				object obj = bindingParentItem.DataItem;
				if (base.Owner.relatedRows.First<KeyValuePair<object, Dictionary<string, object>>>().Key.GetType() == typeof(int) && obj.GetType() == typeof(DataRowView))
				{
					obj = bindingParentItem.ItemIndex;
				}
				Dictionary<string, object> dictionary = base.Owner.relatedRows[obj];
				text = this.FormatDataValue(dictionary[this.UniqueName]);
			}
			tableCell.Text = text;
		}

		// Token: 0x06010133 RID: 65843 RVA: 0x0039C7DC File Offset: 0x0039A9DC
		protected virtual string FormatDataValue(object dataValue)
		{
			string result = "&nbsp;";
			if (dataValue == null || dataValue == DBNull.Value)
			{
				return result;
			}
			if (this.DataFormatString.Length == 0)
			{
				return dataValue.ToString();
			}
			return string.Format(this.DataFormatString, dataValue);
		}

		// Token: 0x06010134 RID: 65844 RVA: 0x0039C81C File Offset: 0x0039AA1C
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		internal string FormatExpression()
		{
			string result = string.Empty;
			try
			{
				if (base.Owner.OwnerGrid.EnableLinqExpressions && base.Owner.originalEnumerable != null)
				{
					List<string> list = new List<string>();
					foreach (string fieldName in this.DataFields)
					{
						string arg = GridDataTableFromEnumerable.TransformDataFieldName(fieldName, base.Owner.originalQueryable.ElementType);
						list.Add(string.Format("{0}({1})", base.DataType.ToString().Split(new char[]
						{
							'.'
						})[1], arg));
					}
					result = string.Format(this.Expression, list.ToArray());
				}
				else
				{
					result = string.Format(this.Expression, this.DataFields).Replace("\"", "'");
				}
			}
			catch (Exception)
			{
				throw new FormatException("Illegal Expression for column: " + this.UniqueName);
			}
			return result;
		}

		// Token: 0x06010135 RID: 65845 RVA: 0x0039C924 File Offset: 0x0039AB24
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object[])")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		internal string FormatExpression(List<string> types)
		{
			string result = string.Empty;
			try
			{
				List<string> list = new List<string>();
				int num = 0;
				foreach (string fieldName in this.DataFields)
				{
					string arg = GridDataTableFromEnumerable.TransformDataFieldName(fieldName, base.Owner.originalQueryable.ElementType);
					list.Add(string.Format("{0}({1})", types[num], arg));
					num++;
				}
				result = string.Format(this.Expression, list.ToArray());
			}
			catch (Exception)
			{
				throw new FormatException("Illegal Expression for column: " + this.UniqueName);
			}
			return result;
		}

		// Token: 0x17004D9D RID: 19869
		// (get) Token: 0x06010136 RID: 65846 RVA: 0x0039C9D4 File Offset: 0x0039ABD4
		// (set) Token: 0x06010137 RID: 65847 RVA: 0x0039C9FD File Offset: 0x0039ABFD
		[NotifyParentProperty(true)]
		[Description("AllowFiltering")]
		[Category("Behavior")]
		[DefaultValue(true)]
		public virtual bool AllowFiltering
		{
			get
			{
				object obj = base.ViewState["_af"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["_af"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004D9E RID: 19870
		// (get) Token: 0x06010138 RID: 65848 RVA: 0x0039CA1C File Offset: 0x0039AC1C
		// (set) Token: 0x06010139 RID: 65849 RVA: 0x0039CA80 File Offset: 0x0039AC80
		[Localizable(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("DataFormatString")]
		[NotifyParentProperty(true)]
		public virtual string DataFormatString
		{
			get
			{
				object obj = base.ViewState["DataFormatString"];
				if (obj == null)
				{
					return string.Empty;
				}
				if (base.Owner != null && base.Owner.OwnerGrid.ExportSettings.SuppressColumnDataFormatStrings && base.Owner.OwnerGrid.IsExporting)
				{
					return "{0}";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["DataFormatString"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004D9F RID: 19871
		// (get) Token: 0x0601013A RID: 65850 RVA: 0x0039CA9C File Offset: 0x0039AC9C
		// (set) Token: 0x0601013B RID: 65851 RVA: 0x0039CAC5 File Offset: 0x0039ACC5
		[DefaultValue(true)]
		[Description("AllowSorting")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public virtual bool AllowSorting
		{
			get
			{
				object obj = base.ViewState["_as"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["_as"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x0601013C RID: 65852 RVA: 0x0039CAE3 File Offset: 0x0039ACE3
		public override bool SupportsFiltering()
		{
			return this.AllowFiltering;
		}

		// Token: 0x0601013D RID: 65853 RVA: 0x0039CAEB File Offset: 0x0039ACEB
		protected override string GetFilterDataField()
		{
			return this.GetResultFieldName();
		}

		// Token: 0x0601013E RID: 65854 RVA: 0x0039CAF4 File Offset: 0x0039ACF4
		public override string GetDefaultGroupByExpression()
		{
			string resultFieldName = this.GetResultFieldName();
			return string.Concat(new string[]
			{
				resultFieldName,
				" [",
				this.HeaderText,
				"] Group By ",
				resultFieldName
			});
		}

		// Token: 0x0601013F RID: 65855 RVA: 0x0039CB36 File Offset: 0x0039AD36
		internal string GetResultFieldName()
		{
			return this.UniqueName + "Result";
		}

		// Token: 0x17004DA0 RID: 19872
		// (get) Token: 0x06010140 RID: 65856 RVA: 0x0039CB48 File Offset: 0x0039AD48
		// (set) Token: 0x06010141 RID: 65857 RVA: 0x0039CB76 File Offset: 0x0039AD76
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Description("DataFields")]
		[Category("Data")]
		[TypeConverter(typeof(GridStringArrayConverter))]
		public virtual string[] DataFields
		{
			get
			{
				object obj = base.ViewState["DataFields"];
				if (obj != null)
				{
					return (string[])obj;
				}
				return new string[0];
			}
			set
			{
				base.ViewState["DataFields"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004DA1 RID: 19873
		// (get) Token: 0x06010142 RID: 65858 RVA: 0x0039CB90 File Offset: 0x0039AD90
		// (set) Token: 0x06010143 RID: 65859 RVA: 0x0039CBBD File Offset: 0x0039ADBD
		[NotifyParentProperty(true)]
		[Description("Expression")]
		[Category("Data")]
		[DefaultValue("")]
		public virtual string Expression
		{
			get
			{
				object obj = base.ViewState["Expression"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["Expression"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x06010144 RID: 65860 RVA: 0x0039CBD8 File Offset: 0x0039ADD8
		public override IDictionary GetCustomPropertyDataFields(object dataItemInstance)
		{
			Hashtable hashtable = new Hashtable();
			foreach (string dataField in this.DataFields)
			{
				GridColumn.AddSubPropertyFieldInfo(hashtable, dataField, dataItemInstance);
			}
			return hashtable;
		}

		// Token: 0x06010145 RID: 65861 RVA: 0x0039CC0D File Offset: 0x0039AE0D
		public string GetActiveDataField()
		{
			return this.GetFilterDataField();
		}
	}
}

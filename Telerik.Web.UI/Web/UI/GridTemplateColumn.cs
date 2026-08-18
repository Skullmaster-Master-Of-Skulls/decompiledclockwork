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
	// Token: 0x020010BF RID: 4287
	public class GridTemplateColumn : GridEditableColumn
	{
		// Token: 0x1700388D RID: 14477
		// (get) Token: 0x0600AF18 RID: 44824 RVA: 0x0025E8EC File Offset: 0x0025CAEC
		// (set) Token: 0x0600AF19 RID: 44825 RVA: 0x0025E919 File Offset: 0x0025CB19
		[Description("DataField")]
		[Category("Data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string DataField
		{
			get
			{
				object obj = base.ViewState["_df"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["_df"] = value;
			}
		}

		// Token: 0x1700388E RID: 14478
		// (get) Token: 0x0600AF1A RID: 44826 RVA: 0x0025E92C File Offset: 0x0025CB2C
		// (set) Token: 0x0600AF1B RID: 44827 RVA: 0x0025E955 File Offset: 0x0025CB55
		[DefaultValue(typeof(GridAggregateFunction), "None")]
		[Description("GridTemplateColumn aggregate function")]
		[Category("Data")]
		[NotifyParentProperty(true)]
		public virtual GridAggregateFunction Aggregate
		{
			get
			{
				object obj = base.ViewState["GTCAggregate"];
				if (obj != null)
				{
					return (GridAggregateFunction)obj;
				}
				return GridAggregateFunction.None;
			}
			set
			{
				base.ViewState["GTCAggregate"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x1700388F RID: 14479
		// (get) Token: 0x0600AF1C RID: 44828 RVA: 0x0025E974 File Offset: 0x0025CB74
		// (set) Token: 0x0600AF1D RID: 44829 RVA: 0x0025E9D8 File Offset: 0x0025CBD8
		[Localizable(true)]
		[DefaultValue("")]
		[Category("Behavior")]
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

		// Token: 0x17003890 RID: 14480
		// (get) Token: 0x0600AF1E RID: 44830 RVA: 0x0025E9F1 File Offset: 0x0025CBF1
		// (set) Token: 0x0600AF1F RID: 44831 RVA: 0x0025EA11 File Offset: 0x0025CC11
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		[Description("Gets or sets the HTML template of a RadGrid client item template cell.")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual string ClientItemTemplate
		{
			get
			{
				return (base.ViewState["ClientItemTemplate"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ClientItemTemplate"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x0600AF20 RID: 44832 RVA: 0x0025EA2A File Offset: 0x0025CC2A
		protected override string GetFilterDataField()
		{
			return this.DataField;
		}

		// Token: 0x0600AF21 RID: 44833 RVA: 0x0025EA32 File Offset: 0x0025CC32
		public override bool SupportsFiltering()
		{
			return this.AllowFiltering;
		}

		// Token: 0x0600AF22 RID: 44834 RVA: 0x0025EA3A File Offset: 0x0025CC3A
		public override bool IsBoundToFieldName(string name)
		{
			return string.Compare(this.DataField, name, true) == 0;
		}

		// Token: 0x0600AF23 RID: 44835 RVA: 0x0025EA4C File Offset: 0x0025CC4C
		protected override void SetCurrentFilterValueToControl(TableCell cell)
		{
			base.SetCurrentFilterValueToControl(cell);
			if (!string.IsNullOrEmpty(this.CurrentFilterValue))
			{
				if (cell.Controls[0] is TextBox)
				{
					(cell.Controls[0] as TextBox).Text = this.CurrentFilterValue;
				}
				if (cell.Controls[0] is CheckBox)
				{
					(cell.Controls[0] as CheckBox).Checked = bool.Parse(this.CurrentFilterValue);
				}
			}
		}

		// Token: 0x0600AF24 RID: 44836 RVA: 0x0025EAD0 File Offset: 0x0025CCD0
		protected override string GetCurrentFilterValueFromControl(TableCell cell)
		{
			if (cell.Controls[0] is TextBox)
			{
				return (cell.Controls[0] as TextBox).Text;
			}
			if (cell.Controls[0] is CheckBox)
			{
				return (cell.Controls[0] as CheckBox).Checked.ToString();
			}
			return this.CurrentFilterValue;
		}

		// Token: 0x0600AF25 RID: 44837 RVA: 0x0025EB40 File Offset: 0x0025CD40
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			GridGroupFooterItem gridGroupFooterItem = inItem as GridGroupFooterItem;
			if (gridGroupFooterItem == null || gridGroupFooterItem.OwnerTableView.GroupFooterTemplate == null)
			{
				if (!this.InitializeTemplatesFirst)
				{
					base.InitializeCell(cell, columnIndex, inItem);
					if (inItem.IsInEditMode)
					{
						this.CurrentColumnEditor.InitializeInControl(cell);
					}
				}
				ITemplate template = null;
				switch (inItem.ItemType)
				{
				case GridItemType.Footer:
					template = this.footerTemplate;
					goto IL_180;
				case GridItemType.Header:
					template = this.headerTemplate;
					goto IL_180;
				}
				if (inItem.IsDataBound)
				{
					if (inItem.IsInEditMode && this.InsertItemTemplate != null && inItem is IGridInsertItem)
					{
						template = ((!base.IsReadOnly(inItem)) ? this.InsertItemTemplate : this.itemTemplate);
					}
					else if (inItem.IsInEditMode && this.editItemTemplate != null)
					{
						template = ((!base.IsReadOnly(inItem)) ? this.editItemTemplate : this.itemTemplate);
					}
					else
					{
						template = this.itemTemplate;
					}
				}
				IL_180:
				if (template != null)
				{
					cell.Text = string.Empty;
					if (base.Owner.OwnerGrid.IsExporting)
					{
						Control control = new Control();
						template.InstantiateIn(control);
						base.Owner.ClearTableViewScriptControls(control);
						for (int i = control.Controls.Count - 1; i >= 0; i--)
						{
							cell.Controls.AddAt(0, control.Controls[i]);
						}
					}
					else
					{
						template.InstantiateIn(cell);
					}
				}
				if (this.InitializeTemplatesFirst)
				{
					base.InitializeCell(cell, columnIndex, inItem);
					if (inItem.IsInEditMode)
					{
						this.CurrentColumnEditor.InitializeInControl(cell);
					}
				}
				if ((inItem is GridFooterItem || gridGroupFooterItem != null) && this.Aggregate != GridAggregateFunction.None)
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

		// Token: 0x0600AF26 RID: 44838 RVA: 0x0025ED90 File Offset: 0x0025CF90
		private bool PopulateAggragateInGroupFooter(TableCell cell, object result)
		{
			GridGroupFooterItem gridGroupFooterItem = cell.Parent as GridGroupFooterItem;
			if (gridGroupFooterItem != null && gridGroupFooterItem.OwnerTableView.GroupFooterTemplate != null)
			{
				if (!gridGroupFooterItem.AggregatesValues.Contains(this.DataField))
				{
					gridGroupFooterItem.AggregatesValues.Add(this.DataField, result);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600AF27 RID: 44839 RVA: 0x0025EDE4 File Offset: 0x0025CFE4
		private void cell_DataBinding(object sender, EventArgs e)
		{
			if (base.Owner.OwnerGrid.IsDesignMode)
			{
				return;
			}
			if ((base.Owner.ShowFooter || base.Owner.OwnerGrid.ShowFooter || base.Owner.ShowGroupFooter) && this.footerTemplate == null)
			{
				TableCell tableCell = (TableCell)sender;
				string footerText = string.IsNullOrEmpty(this.FooterText) ? string.Format("{0} : ", this.Aggregate.ToString()) : this.FooterText;
				if (!string.IsNullOrEmpty(this.FooterAggregateFormatString))
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

		// Token: 0x0600AF28 RID: 44840 RVA: 0x0025EEF4 File Offset: 0x0025D0F4
		private string FormatCellText(string footerText, object aggregateResult)
		{
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
			return string.Format("{0}{1}", footerText, aggregateResult);
		}

		// Token: 0x0600AF29 RID: 44841 RVA: 0x0025EF54 File Offset: 0x0025D154
		private void ApplyAggregates(TableCell cell, string footerText)
		{
			object obj = null;
			if (base.Owner._resolvedDataSource == null)
			{
				return;
			}
			string key = string.Format("GroupedResult{0}", ((GridItem)cell.Parent).GroupLevel);
			DataTable dataTable = (DataTable)((GridEnumerableFromDataView)base.Owner._resolvedDataSource).GroupingDataSet.ExtendedProperties[key];
			if (dataTable == null)
			{
				dataTable = ((GridEnumerableFromDataView)base.Owner._resolvedDataSource)._dataView.Table;
			}
			string filterExpression = base.Owner.FilterExpression;
			string arg = (this.DataField.IndexOf("[") == -1) ? string.Format("[{0}]", this.DataField) : this.DataField;
			if (this.Aggregate == GridAggregateFunction.Count)
			{
				obj = dataTable.DefaultView.Count;
				cell.Text = this.FormatCellText(footerText, dataTable.DefaultView.Count);
			}
			if (this.Aggregate == GridAggregateFunction.First && dataTable.DefaultView.Count > 0)
			{
				obj = dataTable.DefaultView[0][this.DataField];
				cell.Text = this.FormatCellText(footerText, dataTable.DefaultView[0][this.DataField]);
			}
			if (this.Aggregate == GridAggregateFunction.Last && dataTable.DefaultView.Count > 0)
			{
				obj = dataTable.DefaultView[dataTable.DefaultView.Count - 1][this.DataField];
				cell.Text = this.FormatCellText(footerText, dataTable.DefaultView[dataTable.DefaultView.Count - 1][this.DataField]);
			}
			if (this.Aggregate == GridAggregateFunction.Max && dataTable.DefaultView.Count > 0)
			{
				obj = dataTable.Compute(string.Format("Max({0})", arg), filterExpression);
				cell.Text = this.FormatCellText(footerText, obj);
			}
			if (this.Aggregate == GridAggregateFunction.Min && dataTable.DefaultView.Count > 0)
			{
				obj = dataTable.Compute(string.Format("Min({0})", arg), filterExpression);
				cell.Text = this.FormatCellText(footerText, obj);
			}
			if (this.Aggregate == GridAggregateFunction.Sum && dataTable.DefaultView.Count > 0)
			{
				obj = dataTable.Compute(string.Format("Sum({0})", arg), filterExpression);
				cell.Text = this.FormatCellText(footerText, obj);
			}
			if (this.Aggregate == GridAggregateFunction.Avg && dataTable.DefaultView.Count > 0)
			{
				obj = dataTable.Compute(string.Format("Avg({0})", arg), filterExpression);
				cell.Text = this.FormatCellText(footerText, obj);
			}
			if (this.Aggregate == GridAggregateFunction.CountDistinct && dataTable.DefaultView.Count > 0)
			{
				obj = GridBoundColumn.GetDistinctCount(dataTable, this.DataField);
				cell.Text = this.FormatCellText(footerText, obj);
			}
			this.PopulateAggragateInGroupFooter(cell, obj);
		}

		// Token: 0x0600AF2A RID: 44842 RVA: 0x0025F228 File Offset: 0x0025D428
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		private void ApplyAggregates35(TableCell cell, string footerText)
		{
			string key = string.Format("GroupedResult{0}", ((GridItem)cell.Parent).GroupLevel);
			DataTable dataTable = (DataTable)((GridEnumerableFromDataView)base.Owner._resolvedDataSource).GroupingDataSet.ExtendedProperties[key];
			object obj = null;
			if (dataTable != null)
			{
				IEnumerable<DataRow> enumerable = dataTable.AsEnumerable();
				IQueryable<DataRow> queryable = enumerable.AsQueryable<DataRow>();
				obj = GridBoundColumn.GetAggregate(enumerable, queryable, this.DataField, base.DataType, this.Aggregate);
			}
			else if (base.Owner.PagingManager.DataSourceCount > 0)
			{
				if (base.Owner.originalEnumerable == null)
				{
					this.ApplyAggregates(cell, footerText);
					return;
				}
				obj = GridBoundColumn.GetAggregate(base.Owner.originalEnumerable, base.Owner.originalQueryable, this.DataField, base.DataType, this.Aggregate);
			}
			if (this.PopulateAggragateInGroupFooter(cell, obj))
			{
				return;
			}
			cell.Text = this.FormatCellText(footerText, obj);
		}

		// Token: 0x17003891 RID: 14481
		// (get) Token: 0x0600AF2B RID: 44843 RVA: 0x0025F31C File Offset: 0x0025D51C
		// (set) Token: 0x0600AF2C RID: 44844 RVA: 0x0025F324 File Offset: 0x0025D524
		[TemplateContainer(typeof(GridItem), BindingDirection.TwoWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue(null)]
		[Description("Gets or sets the ItemTemplate, which is rendered in the control in edit mode.")]
		public virtual ITemplate EditItemTemplate
		{
			get
			{
				return this.editItemTemplate;
			}
			set
			{
				this.editItemTemplate = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003892 RID: 14482
		// (get) Token: 0x0600AF2D RID: 44845 RVA: 0x0025F333 File Offset: 0x0025D533
		// (set) Token: 0x0600AF2E RID: 44846 RVA: 0x0025F33B File Offset: 0x0025D53B
		[TemplateContainer(typeof(GridItem), BindingDirection.TwoWay)]
		[Browsable(false)]
		[DefaultValue(null)]
		[Description("Gets or sets the ItemTemplate, which is rendered in the control in insert mode.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate InsertItemTemplate
		{
			get
			{
				return this._insertItemTemplate;
			}
			set
			{
				this._insertItemTemplate = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003893 RID: 14483
		// (get) Token: 0x0600AF2F RID: 44847 RVA: 0x0025F34A File Offset: 0x0025D54A
		// (set) Token: 0x0600AF30 RID: 44848 RVA: 0x0025F352 File Offset: 0x0025D552
		[Browsable(false)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(GridItem))]
		[Description("Gets or sets the Controls, which will be rendered in the footer of the template column.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this.footerTemplate;
			}
			set
			{
				this.footerTemplate = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003894 RID: 14484
		// (get) Token: 0x0600AF31 RID: 44849 RVA: 0x0025F361 File Offset: 0x0025D561
		// (set) Token: 0x0600AF32 RID: 44850 RVA: 0x0025F369 File Offset: 0x0025D569
		[Description("Gets or sets the Controls, which will be rendered in the header of the template column.")]
		[TemplateContainer(typeof(GridItem), BindingDirection.TwoWay)]
		[DefaultValue(null)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.headerTemplate;
			}
			set
			{
				this.headerTemplate = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003895 RID: 14485
		// (get) Token: 0x0600AF33 RID: 44851 RVA: 0x0025F378 File Offset: 0x0025D578
		// (set) Token: 0x0600AF34 RID: 44852 RVA: 0x0025F380 File Offset: 0x0025D580
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(GridItem), BindingDirection.TwoWay)]
		[DefaultValue(null)]
		[Description("Gets or sets the ItemTemplate, which is rendered in the control in normal (non-Edit) mode.")]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return this.itemTemplate;
			}
			set
			{
				this.itemTemplate = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x0600AF35 RID: 44853 RVA: 0x0025F390 File Offset: 0x0025D590
		public override IDictionary GetCustomPropertyDataFields(object dataItemInstance)
		{
			Hashtable hashtable = new Hashtable();
			if (!string.IsNullOrEmpty(this.SortExpression))
			{
				GridColumn.AddSubPropertyFieldInfo(hashtable, this.SortExpression, dataItemInstance);
			}
			else if (!string.IsNullOrEmpty(this.DataField))
			{
				GridColumn.AddSubPropertyFieldInfo(hashtable, this.DataField, dataItemInstance);
			}
			return hashtable;
		}

		// Token: 0x0600AF36 RID: 44854 RVA: 0x0025F3DA File Offset: 0x0025D5DA
		protected override string GenerateUniqueName()
		{
			return base.GenerateUniqueNameBase("TemplateColumn");
		}

		// Token: 0x0600AF37 RID: 44855 RVA: 0x0025F3E8 File Offset: 0x0025D5E8
		public override GridColumn Clone()
		{
			GridTemplateColumn gridTemplateColumn = new GridTemplateColumn();
			gridTemplateColumn.CopyBaseProperties(this);
			return gridTemplateColumn;
		}

		// Token: 0x17003896 RID: 14486
		// (get) Token: 0x0600AF38 RID: 44856 RVA: 0x0025F403 File Offset: 0x0025D603
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool IsEditable
		{
			get
			{
				return (this.EditItemTemplate != null || this.InsertItemTemplate != null) && !this.ReadOnly && base.InsertVisiblityMode != GridColumnVisibilityMode.AlwaysHidden;
			}
		}

		// Token: 0x17003897 RID: 14487
		// (get) Token: 0x0600AF39 RID: 44857 RVA: 0x0025F42C File Offset: 0x0025D62C
		// (set) Token: 0x0600AF3A RID: 44858 RVA: 0x0025F455 File Offset: 0x0025D655
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool InitializeTemplatesFirst
		{
			get
			{
				object obj = base.ViewState["_itf"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (!value)
				{
					base.ViewState["_itf"] = false;
					return;
				}
				base.ViewState["_itf"] = null;
			}
		}

		// Token: 0x0600AF3B RID: 44859 RVA: 0x0025F484 File Offset: 0x0025D684
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridTemplateColumn gridTemplateColumn = (GridTemplateColumn)fromColumn;
			this.DataField = gridTemplateColumn.DataField;
			this.AutoPostBackOnFilter = fromColumn.AutoPostBackOnFilter;
			this.Aggregate = gridTemplateColumn.Aggregate;
			this.EditItemTemplate = gridTemplateColumn.EditItemTemplate;
			this.InsertItemTemplate = gridTemplateColumn.InsertItemTemplate;
			this.ClientItemTemplate = gridTemplateColumn.ClientItemTemplate;
			this.FooterTemplate = gridTemplateColumn.FooterTemplate;
			this.HeaderTemplate = gridTemplateColumn.HeaderTemplate;
			this.ItemTemplate = gridTemplateColumn.ItemTemplate;
			this.InitializeTemplatesFirst = gridTemplateColumn.InitializeTemplatesFirst;
			this._bindingsDescription = gridTemplateColumn._bindingsDescription;
			this.FooterAggregateFormatString = gridTemplateColumn.FooterAggregateFormatString;
		}

		// Token: 0x0600AF3C RID: 44860 RVA: 0x0025F52F File Offset: 0x0025D72F
		protected override IGridColumnEditor CreateDefaultColumnEditor()
		{
			return new GridTemplateColumnEditor();
		}

		// Token: 0x0600AF3D RID: 44861 RVA: 0x0025F536 File Offset: 0x0025D736
		protected override void ColumnEditorChange(IGridColumnEditor newValue)
		{
			if (!(newValue is GridTemplateColumnEditor))
			{
				throw new GridColumnEditorException(this.ToString() + " accepts only editor of type: " + typeof(GridTemplateColumnEditor).ToString());
			}
			base.ColumnEditorChange(newValue);
		}

		// Token: 0x0600AF3E RID: 44862 RVA: 0x0025F56C File Offset: 0x0025D76C
		public override void FillValues(IDictionary newValues, GridEditableItem editableItem)
		{
			GridTemplateColumnEditor gridTemplateColumnEditor = (GridTemplateColumnEditor)editableItem.EditManager.GetColumnEditor(this);
			Control containerControl = gridTemplateColumnEditor.ContainerControl;
			if (containerControl == null)
			{
				return;
			}
			IBindableTemplate bindableTemplate;
			if (editableItem.IsInEditMode && editableItem is IGridInsertItem && this.InsertItemTemplate != null)
			{
				bindableTemplate = (IBindableTemplate)this.InsertItemTemplate;
			}
			else if (editableItem.IsInEditMode)
			{
				bindableTemplate = (IBindableTemplate)this.EditItemTemplate;
			}
			else
			{
				bindableTemplate = (IBindableTemplate)this.ItemTemplate;
			}
			if (bindableTemplate == null)
			{
				return;
			}
			bool convertEmptyStringToNull = base.ConvertEmptyStringToNull;
			foreach (object obj in bindableTemplate.ExtractValues(containerControl.BindingContainer))
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				object value = dictionaryEntry.Value;
				string text = value as string;
				if (convertEmptyStringToNull && text != null && text.Length == 0)
				{
					newValues[dictionaryEntry.Key] = null;
				}
				else
				{
					newValues[dictionaryEntry.Key] = value;
				}
			}
		}

		// Token: 0x04002E26 RID: 11814
		private ITemplate editItemTemplate;

		// Token: 0x04002E27 RID: 11815
		private ITemplate footerTemplate;

		// Token: 0x04002E28 RID: 11816
		private ITemplate headerTemplate;

		// Token: 0x04002E29 RID: 11817
		private ITemplate itemTemplate;

		// Token: 0x04002E2A RID: 11818
		private GridTemplateColumn.BindingDescriptionCollection _bindingsDescription;

		// Token: 0x04002E2B RID: 11819
		private ITemplate _insertItemTemplate;

		// Token: 0x020010C0 RID: 4288
		internal class BindingDescriptionCollection
		{
		}
	}
}

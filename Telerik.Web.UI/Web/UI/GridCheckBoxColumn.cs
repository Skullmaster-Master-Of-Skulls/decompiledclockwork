using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010A9 RID: 4265
	public class GridCheckBoxColumn : GridEditableColumn
	{
		// Token: 0x0600ADA1 RID: 44449 RVA: 0x00257015 File Offset: 0x00255215
		public GridCheckBoxColumn()
		{
			base.DataType = typeof(bool);
		}

		// Token: 0x1700381F RID: 14367
		// (get) Token: 0x0600ADA2 RID: 44450 RVA: 0x00257030 File Offset: 0x00255230
		// (set) Token: 0x0600ADA3 RID: 44451 RVA: 0x0025705D File Offset: 0x0025525D
		[Localizable(true)]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string DataField
		{
			get
			{
				object obj = base.ViewState["_df"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["_df"] = value;
				base.UpdateUniqueNameIfDefault(value);
				this.OnColumnChanged();
			}
		}

		// Token: 0x0600ADA4 RID: 44452 RVA: 0x00257080 File Offset: 0x00255280
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			base.InitializeCell(cell, columnIndex, inItem);
			if (inItem.IsDataBound)
			{
				this.CurrentColumnEditor.InitializeInControl(cell);
				if (cell.Controls.Count > 0 && cell.Controls[0] is CheckBox)
				{
					CheckBox checkBox = (CheckBox)cell.Controls[0];
					checkBox.Enabled = (!base.IsReadOnly(inItem) && inItem.IsInEditMode);
				}
				inItem.CellDataBound += this.OnDataBindColumn;
			}
		}

		// Token: 0x0600ADA5 RID: 44453 RVA: 0x00257107 File Offset: 0x00255307
		public override string GetDefaultGroupByExpression()
		{
			if (string.IsNullOrEmpty(this.DataField))
			{
				return base.GetDefaultGroupByExpression();
			}
			return this.DataField + " Group By " + this.DataField;
		}

		// Token: 0x0600ADA6 RID: 44454 RVA: 0x00257134 File Offset: 0x00255334
		private void OnDataBindColumn(object sender, GridCellDataBoundEventArgs args)
		{
			if (args.Column != this)
			{
				return;
			}
			GridItem gridItem = (GridItem)sender;
			if (!gridItem.IsDataBound)
			{
				return;
			}
			object dataItem = gridItem.DataItem;
			if (dataItem == null)
			{
				return;
			}
			if (this.DataField == null || this.DataField.Length == 0)
			{
				return;
			}
			if (base.DesignMode)
			{
				return;
			}
			if (args.Cell == null)
			{
				return;
			}
			this.BoolEditor.InitializeFromControl(args.Cell);
			object obj = null;
			if (dataItem.GetType().FullName == "Microsoft.SharePoint.WebControls.SPDataSourceViewResultItem" || dataItem.GetType().FullName == "Microsoft.SharePoint.SPListItem")
			{
				obj = base.Owner.GetSPViewFieldValue<object>(dataItem, this.DataField);
			}
			else
			{
				try
				{
					obj = DataBinder.Eval(dataItem, this.DataField);
				}
				catch
				{
					if (dataItem is ICustomTypeDescriptor)
					{
						obj = GridPropertyEvaluator.GetPropertyValue(dataItem, this.DataField, DBNull.Value);
					}
					else
					{
						GridPropertyEvaluator gridPropertyEvaluator = new GridPropertyEvaluator();
						obj = gridPropertyEvaluator.GetCachedPropertyValue(dataItem, this.DataField, DBNull.Value);
					}
				}
			}
			if (obj is bool)
			{
				this.BoolEditor.Value = (bool)obj;
				return;
			}
			if (obj != null && !string.IsNullOrEmpty(obj.ToString()))
			{
				if (this.StringTrueValue != this.StringFalseValue)
				{
					this.BoolEditor.Value = (this.StringTrueValue.Trim() == obj.ToString().Trim());
					return;
				}
				this.BoolEditor.Value = bool.Parse(obj.ToString());
			}
		}

		// Token: 0x17003820 RID: 14368
		// (get) Token: 0x0600ADA7 RID: 44455 RVA: 0x002572B8 File Offset: 0x002554B8
		// (set) Token: 0x0600ADA8 RID: 44456 RVA: 0x002572E5 File Offset: 0x002554E5
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("String representation of the true value in your data column")]
		public virtual string StringTrueValue
		{
			get
			{
				object obj = base.ViewState["_stv"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["_stv"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003821 RID: 14369
		// (get) Token: 0x0600ADA9 RID: 44457 RVA: 0x00257300 File Offset: 0x00255500
		// (set) Token: 0x0600ADAA RID: 44458 RVA: 0x0025732D File Offset: 0x0025552D
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("String representation of the false value in your data column")]
		public virtual string StringFalseValue
		{
			get
			{
				object obj = base.ViewState["_sfv"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["_sfv"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x0600ADAB RID: 44459 RVA: 0x00257348 File Offset: 0x00255548
		public override string EvaluateFilterExpression(GridFilteringItem filteringItem)
		{
			if (!this.SupportsFiltering())
			{
				return string.Empty;
			}
			TableCell cell = filteringItem[this.UniqueName];
			string text = this.GetCurrentFilterValueFromControl(cell);
			if (this.StringTrueValue != this.StringFalseValue)
			{
				text = ((text == true.ToString()) ? this.StringTrueValue : this.StringFalseValue);
			}
			string value = base.Owner.OwnerGrid.EnableLinqExpressions ? ")||(" : ") OR (";
			if (this.ListOfFilterValues != null && this.ListOfFilterValues.Length > 0)
			{
				if (this.CurrentFilterFunction == GridKnownFunction.EqualTo)
				{
					GridFilterFunction gridFilterFunction = new GridFilterFunction(this.CurrentFilterFunction);
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("(");
					stringBuilder.Append(gridFilterFunction.GetFunctionString(this.GetFilterDataField(), this.ListOfFilterValues[0], base.DataType, filteringItem.OwnerTableView));
					for (int i = 1; i < this.ListOfFilterValues.Length; i++)
					{
						string functionString = gridFilterFunction.GetFunctionString(this.GetFilterDataField(), this.ListOfFilterValues[i], base.DataType, filteringItem.OwnerTableView);
						if (!string.IsNullOrEmpty(functionString))
						{
							stringBuilder.Append(value);
							stringBuilder.Append(functionString);
						}
					}
					stringBuilder.Append(")");
					return stringBuilder.ToString();
				}
				if (this.CurrentFilterFunction == GridKnownFunction.NoFilter)
				{
					this.ListOfFilterValues = null;
				}
			}
			if (string.IsNullOrEmpty(text) && !base.FunctionTakesNoArguments(this.CurrentFilterFunction))
			{
				return "";
			}
			GridFilterFunction gridFilterFunction2 = new GridFilterFunction(this.CurrentFilterFunction);
			return gridFilterFunction2.GetFunctionString(this.GetFilterDataField(), text, (this.StringTrueValue != this.StringFalseValue) ? typeof(string) : base.DataType, filteringItem.OwnerTableView);
		}

		// Token: 0x0600ADAC RID: 44460 RVA: 0x00257514 File Offset: 0x00255714
		protected override IGridColumnEditor CreateDefaultColumnEditor()
		{
			return new GridCheckBoxColumnEditor();
		}

		// Token: 0x17003822 RID: 14370
		// (get) Token: 0x0600ADAD RID: 44461 RVA: 0x0025751B File Offset: 0x0025571B
		private GridBoolColumnEditor BoolEditor
		{
			get
			{
				return this.CurrentColumnEditor as GridBoolColumnEditor;
			}
		}

		// Token: 0x0600ADAE RID: 44462 RVA: 0x00257528 File Offset: 0x00255728
		protected override void ColumnEditorChange(IGridColumnEditor newValue)
		{
			if (!(newValue is GridBoolColumnEditor))
			{
				throw new GridColumnEditorException(this.ToString() + " accepts only editor of type: " + typeof(GridBoolColumnEditor).ToString());
			}
			base.ColumnEditorChange(newValue);
		}

		// Token: 0x0600ADAF RID: 44463 RVA: 0x0025755E File Offset: 0x0025575E
		internal override string GetSortExpression()
		{
			if (string.IsNullOrEmpty(this.SortExpression) && !string.IsNullOrEmpty(this.DataField) && this.AllowSorting)
			{
				return this.DataField;
			}
			return base.GetSortExpression();
		}

		// Token: 0x0600ADB0 RID: 44464 RVA: 0x0025758F File Offset: 0x0025578F
		public override bool SupportsFiltering()
		{
			return this.AllowFiltering;
		}

		// Token: 0x17003823 RID: 14371
		// (get) Token: 0x0600ADB1 RID: 44465 RVA: 0x00257597 File Offset: 0x00255797
		protected override bool Sortable
		{
			get
			{
				return this.AllowSorting;
			}
		}

		// Token: 0x17003824 RID: 14372
		// (get) Token: 0x0600ADB2 RID: 44466 RVA: 0x002575A0 File Offset: 0x002557A0
		// (set) Token: 0x0600ADB3 RID: 44467 RVA: 0x002575CD File Offset: 0x002557CD
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("The ToolTip that will be applied to every item CheckBox control.")]
		public virtual string ToolTip
		{
			get
			{
				object obj = base.ViewState["ToolTip"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["ToolTip"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x0600ADB4 RID: 44468 RVA: 0x002575E6 File Offset: 0x002557E6
		protected override string GetFilterDataField()
		{
			return this.DataField;
		}

		// Token: 0x0600ADB5 RID: 44469 RVA: 0x002575EE File Offset: 0x002557EE
		protected override string GenerateUniqueName()
		{
			return base.GenerateUniqueNameBase(this.DataField);
		}

		// Token: 0x0600ADB6 RID: 44470 RVA: 0x002575FC File Offset: 0x002557FC
		public override bool IsBoundToFieldName(string name)
		{
			return string.Compare(this.DataField, name, true) == 0;
		}

		// Token: 0x17003825 RID: 14373
		// (get) Token: 0x0600ADB7 RID: 44471 RVA: 0x0025760E File Offset: 0x0025580E
		public override bool IsEditable
		{
			get
			{
				return !this.ReadOnly;
			}
		}

		// Token: 0x0600ADB8 RID: 44472 RVA: 0x0025761C File Offset: 0x0025581C
		public override void FillValues(IDictionary newValues, GridEditableItem editableItem)
		{
			GridBoolColumnEditor gridBoolColumnEditor = (GridBoolColumnEditor)editableItem.EditManager.GetColumnEditor(this);
			if (this.StringTrueValue != this.StringFalseValue)
			{
				newValues[this.DataField] = (gridBoolColumnEditor.Value ? this.StringTrueValue : this.StringFalseValue);
				return;
			}
			newValues[this.DataField] = gridBoolColumnEditor.Value;
		}

		// Token: 0x0600ADB9 RID: 44473 RVA: 0x00257688 File Offset: 0x00255888
		public override IDictionary GetCustomPropertyDataFields(object dataItemInstance)
		{
			Hashtable hashtable = new Hashtable();
			GridColumn.AddSubPropertyFieldInfo(hashtable, this.DataField, dataItemInstance);
			return hashtable;
		}

		// Token: 0x0600ADBA RID: 44474 RVA: 0x002576AC File Offset: 0x002558AC
		public override GridColumn Clone()
		{
			GridCheckBoxColumn gridCheckBoxColumn = new GridCheckBoxColumn();
			gridCheckBoxColumn.CopyBaseProperties(this);
			return gridCheckBoxColumn;
		}

		// Token: 0x0600ADBB RID: 44475 RVA: 0x002576C8 File Offset: 0x002558C8
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridCheckBoxColumn gridCheckBoxColumn = (GridCheckBoxColumn)fromColumn;
			this.DataField = gridCheckBoxColumn.DataField;
			this.AutoPostBackOnFilter = gridCheckBoxColumn.AutoPostBackOnFilter;
			this.ToolTip = gridCheckBoxColumn.ToolTip;
			this.StringTrueValue = gridCheckBoxColumn.StringTrueValue;
			this.StringFalseValue = gridCheckBoxColumn.StringFalseValue;
		}
	}
}

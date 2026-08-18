using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010BD RID: 4285
	public class GridHyperLinkColumn : GridColumn, IGridDataColumn
	{
		// Token: 0x0600AEEB RID: 44779 RVA: 0x0025DF4C File Offset: 0x0025C14C
		protected virtual string FormatDataNavigateUrlValue(object[] dataUrlValues)
		{
			for (int i = 0; i < dataUrlValues.Length; i++)
			{
				if (dataUrlValues[i] == null || dataUrlValues[i] == DBNull.Value)
				{
					dataUrlValues[i] = string.Empty;
				}
			}
			string dataNavigateUrlFormatString = this.DataNavigateUrlFormatString;
			if (dataNavigateUrlFormatString.Length == 0)
			{
				return dataUrlValues[0].ToString();
			}
			string result = string.Empty;
			try
			{
				result = string.Format(dataNavigateUrlFormatString, dataUrlValues);
			}
			catch (Exception)
			{
				throw new FormatException("Illegal DataNavigateUrlFormatString for column: " + this.UniqueName);
			}
			return result;
		}

		// Token: 0x0600AEEC RID: 44780 RVA: 0x0025DFD0 File Offset: 0x0025C1D0
		protected virtual string FormatDataTextValue(object dataTextValue)
		{
			string empty = string.Empty;
			if (base.Owner != null && base.Owner.OwnerGrid.IsExporting && base.Owner.OwnerGrid.ExportSettings.SuppressColumnDataFormatStrings)
			{
				return dataTextValue.ToString();
			}
			if (dataTextValue == null || dataTextValue == DBNull.Value)
			{
				return empty;
			}
			string dataTextFormatString = this.DataTextFormatString;
			if (dataTextFormatString.Length == 0)
			{
				return dataTextValue.ToString();
			}
			return string.Format(dataTextFormatString, dataTextValue);
		}

		// Token: 0x17003882 RID: 14466
		// (get) Token: 0x0600AEED RID: 44781 RVA: 0x0025E045 File Offset: 0x0025C245
		internal GridHyperLinkColumnDataEvaluator Evaluator
		{
			get
			{
				if (this._evaluator == null)
				{
					this._evaluator = new GridHyperLinkColumnDataEvaluator(this);
					base.Owner.DataBinding += this.OwnerGrid_DataBinding;
				}
				return this._evaluator;
			}
		}

		// Token: 0x0600AEEE RID: 44782 RVA: 0x0025E078 File Offset: 0x0025C278
		private void OwnerGrid_DataBinding(object sender, EventArgs e)
		{
			this.Evaluator.ClearCache();
		}

		// Token: 0x0600AEEF RID: 44783 RVA: 0x0025E088 File Offset: 0x0025C288
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			base.InitializeCell(cell, columnIndex, inItem);
			if (inItem.IsDataBound)
			{
				HyperLink hyperLink = new HyperLink();
				hyperLink.Text = this.Text;
				hyperLink.NavigateUrl = this.NavigateUrl;
				hyperLink.ImageUrl = this.ImageUrl;
				hyperLink.Target = this.Target;
				if (this.DataNavigateUrlFields.Length > 0 || !string.IsNullOrEmpty(this.DataTextField))
				{
					hyperLink.DataBinding += this.OnDataBindColumn;
				}
				cell.Controls.Add(hyperLink);
			}
		}

		// Token: 0x0600AEF0 RID: 44784 RVA: 0x0025E114 File Offset: 0x0025C314
		private void OnDataBindColumn(object sender, EventArgs e)
		{
			HyperLink hyperLink = (HyperLink)sender;
			GridItem bindingParentItem = GridColumn.GetBindingParentItem(hyperLink);
			object dataItem = bindingParentItem.DataItem;
			object dataTextFieldValue = this.Evaluator.GetDataTextFieldValue(dataItem);
			object[] dataUrlFieldValues = this.Evaluator.GetDataUrlFieldValues(dataItem);
			if (dataTextFieldValue != null)
			{
				hyperLink.Text = (hyperLink.ToolTip = this.FormatDataTextValue(dataTextFieldValue));
			}
			else if (base.DesignMode && !string.IsNullOrEmpty(this.DataTextField))
			{
				hyperLink.Text = "HyperLinkColumn";
			}
			if (dataUrlFieldValues != null && dataUrlFieldValues.Length > 0)
			{
				hyperLink.NavigateUrl = this.FormatDataNavigateUrlValue(dataUrlFieldValues);
				return;
			}
			if (base.DesignMode && this.DataNavigateUrlFields.Length > 0)
			{
				hyperLink.NavigateUrl = "url";
			}
		}

		// Token: 0x0600AEF1 RID: 44785 RVA: 0x0025E1C8 File Offset: 0x0025C3C8
		public override void PrepareCell(TableCell cell, GridItem item)
		{
			base.PrepareCell(cell, item);
			if (item is GridDataItem && cell.Controls.Count > 0 && cell.Controls[0] is HyperLink && string.IsNullOrEmpty((cell.Controls[0] as HyperLink).Text) && string.IsNullOrEmpty(this.ImageUrl))
			{
				cell.Controls.Add(new LiteralControl("&nbsp;"));
			}
		}

		// Token: 0x17003883 RID: 14467
		// (get) Token: 0x0600AEF2 RID: 44786 RVA: 0x0025E248 File Offset: 0x0025C448
		// (set) Token: 0x0600AEF3 RID: 44787 RVA: 0x0025E276 File Offset: 0x0025C476
		[NotifyParentProperty(true)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[TypeConverter(typeof(GridStringArrayConverter))]
		[Description("HyperLinkColumn_DataNavigateUrlFields")]
		[Category("Data")]
		[DefaultValue("")]
		public virtual string[] DataNavigateUrlFields
		{
			get
			{
				object obj = base.ViewState["DataNavigateUrlFields"];
				if (obj != null)
				{
					return (string[])obj;
				}
				return new string[0];
			}
			set
			{
				base.ViewState["DataNavigateUrlFields"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003884 RID: 14468
		// (get) Token: 0x0600AEF4 RID: 44788 RVA: 0x0025E290 File Offset: 0x0025C490
		// (set) Token: 0x0600AEF5 RID: 44789 RVA: 0x0025E2BD File Offset: 0x0025C4BD
		[Description("The formatting applied to the value bound to the NavigateUrl property.")]
		[NotifyParentProperty(true)]
		[Category("Data")]
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string DataNavigateUrlFormatString
		{
			get
			{
				object obj = base.ViewState["DataNavigateUrlFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataNavigateUrlFormatString"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003885 RID: 14469
		// (get) Token: 0x0600AEF6 RID: 44790 RVA: 0x0025E2D8 File Offset: 0x0025C4D8
		// (set) Token: 0x0600AEF7 RID: 44791 RVA: 0x0025E305 File Offset: 0x0025C505
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Data")]
		[Description("HyperLinkColumn_DataTextField")]
		public virtual string DataTextField
		{
			get
			{
				object obj = base.ViewState["DataTextField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataTextField"] = value;
				base.UpdateUniqueNameIfDefault(value);
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003886 RID: 14470
		// (get) Token: 0x0600AEF8 RID: 44792 RVA: 0x0025E328 File Offset: 0x0025C528
		// (set) Token: 0x0600AEF9 RID: 44793 RVA: 0x0025E38C File Offset: 0x0025C58C
		[Description("The formatting applied to the value bound to the Text property.")]
		[Category("Data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string DataTextFormatString
		{
			get
			{
				object obj = base.ViewState["DataTextFormatString"];
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
				base.ViewState["DataTextFormatString"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003887 RID: 14471
		// (get) Token: 0x0600AEFA RID: 44794 RVA: 0x0025E3A8 File Offset: 0x0025C5A8
		// (set) Token: 0x0600AEFB RID: 44795 RVA: 0x0025E3D5 File Offset: 0x0025C5D5
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Behavior")]
		[Localizable(true)]
		[Description("HyperLinkColumn_NavigateUrl")]
		public virtual string NavigateUrl
		{
			get
			{
				object obj = base.ViewState["NavigateUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["NavigateUrl"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003888 RID: 14472
		// (get) Token: 0x0600AEFC RID: 44796 RVA: 0x0025E3EE File Offset: 0x0025C5EE
		// (set) Token: 0x0600AEFD RID: 44797 RVA: 0x0025E40E File Offset: 0x0025C60E
		[Localizable(true)]
		[DefaultValue("")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value specifying the ImageUrl property of the HyperLink controls in data cells.")]
		public virtual string ImageUrl
		{
			get
			{
				return (base.ViewState["ImageUrl"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003889 RID: 14473
		// (get) Token: 0x0600AEFE RID: 44798 RVA: 0x0025E428 File Offset: 0x0025C628
		// (set) Token: 0x0600AEFF RID: 44799 RVA: 0x0025E455 File Offset: 0x0025C655
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Localizable(true)]
		[DefaultValue("")]
		[Description("HyperLinkColumn_Target")]
		public virtual string Target
		{
			get
			{
				object obj = base.ViewState["Target"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["Target"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x1700388A RID: 14474
		// (get) Token: 0x0600AF00 RID: 44800 RVA: 0x0025E470 File Offset: 0x0025C670
		// (set) Token: 0x0600AF01 RID: 44801 RVA: 0x0025E49D File Offset: 0x0025C69D
		[Localizable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("HyperLinkColumn_Text")]
		[NotifyParentProperty(true)]
		public virtual string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["Text"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x1700388B RID: 14475
		// (get) Token: 0x0600AF02 RID: 44802 RVA: 0x0025E4B8 File Offset: 0x0025C6B8
		// (set) Token: 0x0600AF03 RID: 44803 RVA: 0x0025E4E1 File Offset: 0x0025C6E1
		[DefaultValue(true)]
		[Description("AllowFiltering")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
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

		// Token: 0x1700388C RID: 14476
		// (get) Token: 0x0600AF04 RID: 44804 RVA: 0x0025E500 File Offset: 0x0025C700
		// (set) Token: 0x0600AF05 RID: 44805 RVA: 0x0025E529 File Offset: 0x0025C729
		[Description("AllowSorting")]
		[DefaultValue(true)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public virtual bool AllowSorting
		{
			get
			{
				object obj = base.ViewState["_as"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["_as"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x0600AF06 RID: 44806 RVA: 0x0025E547 File Offset: 0x0025C747
		internal override string GetSortExpression()
		{
			if (string.IsNullOrEmpty(this.SortExpression) && !string.IsNullOrEmpty(this.DataTextField) && this.AllowSorting)
			{
				return this.DataTextField;
			}
			return base.GetSortExpression();
		}

		// Token: 0x0600AF07 RID: 44807 RVA: 0x0025E578 File Offset: 0x0025C778
		public override bool SupportsFiltering()
		{
			return this.AllowFiltering;
		}

		// Token: 0x0600AF08 RID: 44808 RVA: 0x0025E580 File Offset: 0x0025C780
		protected override string GetFilterDataField()
		{
			return this.DataTextField;
		}

		// Token: 0x0600AF09 RID: 44809 RVA: 0x0025E588 File Offset: 0x0025C788
		public override string GetDefaultGroupByExpression()
		{
			return this.DataTextField + " Group By " + this.DataTextField;
		}

		// Token: 0x0600AF0A RID: 44810 RVA: 0x0025E5A0 File Offset: 0x0025C7A0
		public override bool IsBoundToFieldName(string name)
		{
			if (string.IsNullOrEmpty(this.DataTextField))
			{
				return this.IsBoundToFieldName(this.DataNavigateUrlFields, name);
			}
			return string.Compare(this.DataTextField, name, true) == 0;
		}

		// Token: 0x0600AF0B RID: 44811 RVA: 0x0025E5D0 File Offset: 0x0025C7D0
		public bool IsBoundToFieldName(string[] urlFields, string name)
		{
			bool result = false;
			for (int i = 0; i < urlFields.Length; i++)
			{
				if (string.Compare(urlFields[i], name, true) == 0)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600AF0C RID: 44812 RVA: 0x0025E5FE File Offset: 0x0025C7FE
		protected override string GenerateUniqueName()
		{
			return base.GenerateUniqueNameBase(this.DataTextField);
		}

		// Token: 0x0600AF0D RID: 44813 RVA: 0x0025E60C File Offset: 0x0025C80C
		public override IDictionary GetCustomPropertyDataFields(object dataItemInstance)
		{
			Hashtable hashtable = new Hashtable();
			foreach (string dataField in this.DataNavigateUrlFields)
			{
				GridColumn.AddSubPropertyFieldInfo(hashtable, dataField, dataItemInstance);
			}
			if (!hashtable.ContainsKey(this.DataTextField))
			{
				GridColumn.AddSubPropertyFieldInfo(hashtable, this.DataTextField, dataItemInstance);
			}
			return hashtable;
		}

		// Token: 0x0600AF0E RID: 44814 RVA: 0x0025E65C File Offset: 0x0025C85C
		public override GridColumn Clone()
		{
			GridHyperLinkColumn gridHyperLinkColumn = new GridHyperLinkColumn();
			gridHyperLinkColumn.CopyBaseProperties(this);
			return gridHyperLinkColumn;
		}

		// Token: 0x0600AF0F RID: 44815 RVA: 0x0025E678 File Offset: 0x0025C878
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridHyperLinkColumn gridHyperLinkColumn = (GridHyperLinkColumn)fromColumn;
			this.DataNavigateUrlFields = gridHyperLinkColumn.DataNavigateUrlFields;
			this.DataNavigateUrlFormatString = gridHyperLinkColumn.DataNavigateUrlFormatString;
			this.DataTextField = gridHyperLinkColumn.DataTextField;
			this.DataTextFormatString = gridHyperLinkColumn.DataTextFormatString;
			this.NavigateUrl = gridHyperLinkColumn.NavigateUrl;
			this.ImageUrl = gridHyperLinkColumn.ImageUrl;
			this.Target = gridHyperLinkColumn.Target;
			this.Text = gridHyperLinkColumn.Text;
			this.AllowFiltering = gridHyperLinkColumn.AllowFiltering;
		}

		// Token: 0x0600AF10 RID: 44816 RVA: 0x0025E6FF File Offset: 0x0025C8FF
		public string GetActiveDataField()
		{
			return this.GetFilterDataField();
		}

		// Token: 0x04002E23 RID: 11811
		private GridHyperLinkColumnDataEvaluator _evaluator;
	}
}

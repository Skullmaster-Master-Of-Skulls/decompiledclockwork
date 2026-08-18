using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020011F7 RID: 4599
	public class TreeListCalculatedColumn : TreeListColumn
	{
		// Token: 0x17003D33 RID: 15667
		// (get) Token: 0x0600BDC3 RID: 48579 RVA: 0x002A0A14 File Offset: 0x0029EC14
		// (set) Token: 0x0600BDC4 RID: 48580 RVA: 0x002A0A3D File Offset: 0x0029EC3D
		[Category("Data")]
		[Description("TreeListBoundColumn aggregate function")]
		[DefaultValue(typeof(TreeListAggregateFunction), "None")]
		[NotifyParentProperty(true)]
		public virtual TreeListAggregateFunction Aggregate
		{
			get
			{
				object obj = base.ViewState["Aggregate"];
				if (obj != null)
				{
					return (TreeListAggregateFunction)obj;
				}
				return TreeListAggregateFunction.None;
			}
			set
			{
				base.ViewState["Aggregate"] = value;
			}
		}

		// Token: 0x17003D34 RID: 15668
		// (get) Token: 0x0600BDC5 RID: 48581 RVA: 0x002A0A58 File Offset: 0x0029EC58
		// (set) Token: 0x0600BDC6 RID: 48582 RVA: 0x002A0A86 File Offset: 0x0029EC86
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(GridStringArrayConverter))]
		[Description("DataFields")]
		[Category("Data")]
		[DefaultValue("")]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
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
			}
		}

		// Token: 0x17003D35 RID: 15669
		// (get) Token: 0x0600BDC7 RID: 48583 RVA: 0x002A0A9C File Offset: 0x0029EC9C
		// (set) Token: 0x0600BDC8 RID: 48584 RVA: 0x002A0AC9 File Offset: 0x0029ECC9
		[NotifyParentProperty(true)]
		[Category("Data")]
		[Description("Expression")]
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
			}
		}

		// Token: 0x17003D36 RID: 15670
		// (get) Token: 0x0600BDC9 RID: 48585 RVA: 0x002A0ADC File Offset: 0x0029ECDC
		// (set) Token: 0x0600BDCA RID: 48586 RVA: 0x002A0B09 File Offset: 0x0029ED09
		[NotifyParentProperty(true)]
		[Description("DataFormatString")]
		[Localizable(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		public virtual string DataFormatString
		{
			get
			{
				object obj = base.ViewState["DataFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataFormatString"] = value;
			}
		}

		// Token: 0x17003D37 RID: 15671
		// (get) Token: 0x0600BDCB RID: 48587 RVA: 0x002A0B1C File Offset: 0x0029ED1C
		// (set) Token: 0x0600BDCC RID: 48588 RVA: 0x002A0B45 File Offset: 0x0029ED45
		[Description("AllowSorting")]
		[DefaultValue(true)]
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
			}
		}

		// Token: 0x17003D38 RID: 15672
		// (get) Token: 0x0600BDCD RID: 48589 RVA: 0x002A0B5D File Offset: 0x0029ED5D
		protected override bool Sortable
		{
			get
			{
				return this.AllowSorting;
			}
		}

		// Token: 0x0600BDCE RID: 48590 RVA: 0x002A0B65 File Offset: 0x0029ED65
		protected override string GetSortExpression()
		{
			if (string.IsNullOrEmpty(this.SortExpression) && !string.IsNullOrEmpty(this.UniqueName) && this.AllowSorting)
			{
				return string.Format("{0}Result", this.UniqueName);
			}
			return base.GetSortExpression();
		}

		// Token: 0x0600BDCF RID: 48591 RVA: 0x002A0BA0 File Offset: 0x0029EDA0
		protected override void InitializeFooterCells(TableCell cell, int columnIndex, TreeListFooterItem inItem)
		{
			if (inItem.HierarchyIndex != null && inItem.OwnerTreeList.CalculatedAggregates != null)
			{
				string key = inItem.HierarchyIndex.LevelIndex.ToString() + inItem.HierarchyIndex.NestedLevel.ToString();
				if (inItem.OwnerTreeList.CalculatedAggregates.ContainsKey(key))
				{
					string key2 = this.UniqueName + "Result";
					if (inItem.OwnerTreeList.CalculatedAggregates[key].ContainsKey(key2))
					{
						cell.Text = string.Format("{0}{1}", this.FooterText, inItem.OwnerTreeList.CalculatedAggregates[key][key2].ToString());
					}
				}
			}
		}

		// Token: 0x17003D39 RID: 15673
		// (get) Token: 0x0600BDD0 RID: 48592 RVA: 0x002A0C6C File Offset: 0x0029EE6C
		// (set) Token: 0x0600BDD1 RID: 48593 RVA: 0x002A0C9E File Offset: 0x0029EE9E
		[DefaultValue(typeof(string))]
		[TypeConverter(typeof(GridDataTypeConverter))]
		[NotifyParentProperty(true)]
		public Type DataType
		{
			get
			{
				object obj = base.ViewState["DataType"];
				if (obj == null)
				{
					obj = typeof(string);
				}
				return (Type)obj;
			}
			set
			{
				if (!GridDataTypeConverter.SupportedTypes.Contains(value) && !value.IsEnum)
				{
					throw new GridNotSupportedException("Specified column DataType is not supported " + value.ToString());
				}
				base.ViewState["DataType"] = value;
			}
		}

		// Token: 0x17003D3A RID: 15674
		// (get) Token: 0x0600BDD2 RID: 48594 RVA: 0x002A0CDC File Offset: 0x0029EEDC
		// (set) Token: 0x0600BDD3 RID: 48595 RVA: 0x002A0D09 File Offset: 0x0029EF09
		[Description("Sets or gets format string for the footer aggregate.")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string FooterAggregateFormatString
		{
			get
			{
				object obj = base.ViewState["FooterAggregateFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["FooterAggregateFormatString"] = value;
			}
		}

		// Token: 0x0600BDD4 RID: 48596 RVA: 0x002A0D1C File Offset: 0x0029EF1C
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

		// Token: 0x0600BDD5 RID: 48597 RVA: 0x002A0D5C File Offset: 0x0029EF5C
		protected override void InitializeDataCells(TableCell cell, int columnIndex, TreeListDataItem inItem)
		{
			cell.DataBinding += this.OnColumnDataCellBinding;
		}

		// Token: 0x0600BDD6 RID: 48598 RVA: 0x002A0D74 File Offset: 0x0029EF74
		protected virtual void OnColumnDataCellBinding(object sender, EventArgs e)
		{
			TableCell tableCell = (TableCell)sender;
			TreeListDataItem treeListDataItem = (TreeListDataItem)TreeListColumn.GetBindingParentItem(tableCell);
			object dataItem = treeListDataItem.DataItem;
			string text;
			if (base.Owner.IsDesignMode)
			{
				text = "TreeListCalculatedColumn";
			}
			else
			{
				string key = string.Format("{0}Result", this.UniqueName);
				object dataValue = treeListDataItem.SourceItem.CalculatedColumns[key];
				text = this.FormatDataValue(dataValue);
			}
			tableCell.Text = text;
		}
	}
}

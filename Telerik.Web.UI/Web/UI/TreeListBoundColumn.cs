using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020011FC RID: 4604
	public class TreeListBoundColumn : TreeListEditableColumn
	{
		// Token: 0x17003D53 RID: 15699
		// (get) Token: 0x0600BE19 RID: 48665 RVA: 0x002A1D14 File Offset: 0x0029FF14
		// (set) Token: 0x0600BE1A RID: 48666 RVA: 0x002A1D41 File Offset: 0x0029FF41
		[Category("Behavior")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003D54 RID: 15700
		// (get) Token: 0x0600BE1B RID: 48667 RVA: 0x002A1D54 File Offset: 0x0029FF54
		// (set) Token: 0x0600BE1C RID: 48668 RVA: 0x002A1D81 File Offset: 0x0029FF81
		[Description("Sets or gets format string for the footer/group footer aggregate.")]
		[Category("Behavior")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
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

		// Token: 0x17003D55 RID: 15701
		// (get) Token: 0x0600BE1D RID: 48669 RVA: 0x002A1D94 File Offset: 0x0029FF94
		// (set) Token: 0x0600BE1E RID: 48670 RVA: 0x002A1DBD File Offset: 0x0029FFBD
		[Description("TreeListBoundColumn aggregate function")]
		[DefaultValue(typeof(TreeListAggregateFunction), "None")]
		[NotifyParentProperty(true)]
		[Category("Data")]
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

		// Token: 0x17003D56 RID: 15702
		// (get) Token: 0x0600BE1F RID: 48671 RVA: 0x002A1DD8 File Offset: 0x0029FFD8
		// (set) Token: 0x0600BE20 RID: 48672 RVA: 0x002A1E05 File Offset: 0x002A0005
		[Description("Sets or gets default text when column is empty")]
		[DefaultValue("&nbsp;")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public virtual string EmptyDataText
		{
			get
			{
				object obj = base.ViewState["EmptyDataText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "&nbsp;";
			}
			set
			{
				base.ViewState["EmptyDataText"] = value;
			}
		}

		// Token: 0x17003D57 RID: 15703
		// (get) Token: 0x0600BE21 RID: 48673 RVA: 0x002A1E18 File Offset: 0x002A0018
		// (set) Token: 0x0600BE22 RID: 48674 RVA: 0x002A1E41 File Offset: 0x002A0041
		[Localizable(true)]
		[Description("Sets or gets whether cell content must be encoded.")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public virtual bool HtmlEncode
		{
			get
			{
				object obj = base.ViewState["HtmlEncode"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["HtmlEncode"] = value;
			}
		}

		// Token: 0x0600BE23 RID: 48675 RVA: 0x002A1E59 File Offset: 0x002A0059
		protected virtual object ConvertValueIfEmpty(string value)
		{
			if (this.ConvertEmptyStringToNull && string.IsNullOrEmpty(value))
			{
				return null;
			}
			return value;
		}

		// Token: 0x17003D58 RID: 15704
		// (get) Token: 0x0600BE24 RID: 48676 RVA: 0x002A1E70 File Offset: 0x002A0070
		// (set) Token: 0x0600BE25 RID: 48677 RVA: 0x002A1E9E File Offset: 0x002A009E
		[Description("Convert the emty values to null when extracting values during data editing operations.")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public bool ConvertEmptyStringToNull
		{
			get
			{
				object obj = base.ViewState["ConvertEmptyStringToNull"];
				if (obj == null)
				{
					obj = true;
				}
				return (bool)obj;
			}
			set
			{
				base.ViewState["ConvertEmptyStringToNull"] = value;
			}
		}

		// Token: 0x0600BE26 RID: 48678 RVA: 0x002A1EB8 File Offset: 0x002A00B8
		protected override void OnColumnDataCellBinding(object sender, EventArgs e)
		{
			TableCell tableCell = (TableCell)sender;
			TreeListDataItem treeListDataItem = (TreeListDataItem)TreeListColumn.GetBindingParentItem(tableCell);
			object dataItem = treeListDataItem.DataItem;
			object dataValue = null;
			string text = string.Empty;
			if (!string.IsNullOrEmpty(base.DataField) && base.TryExtractDataValue(dataItem, base.DataField, out dataValue))
			{
				text = this.FormatDataValue(dataValue, treeListDataItem);
			}
			if (string.IsNullOrEmpty(text))
			{
				text = this.EmptyDataText;
			}
			tableCell.Text = text;
		}

		// Token: 0x0600BE27 RID: 48679 RVA: 0x002A1F29 File Offset: 0x002A0129
		protected override void InitializeFooterCells(TableCell cell, int columnIndex, TreeListFooterItem inItem)
		{
			if (this.Aggregate != TreeListAggregateFunction.None)
			{
				base.InitializeFooterCells(cell, columnIndex, inItem);
				return;
			}
			cell.Text = this.FooterText;
		}

		// Token: 0x0600BE28 RID: 48680 RVA: 0x002A1F4C File Offset: 0x002A014C
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		protected virtual string FormatDataValue(object dataValue, TreeListDataItem item)
		{
			if (dataValue == null || dataValue == DBNull.Value)
			{
				return string.Empty;
			}
			if (this.HtmlEncode && !string.IsNullOrEmpty(dataValue.ToString()))
			{
				return HttpUtility.HtmlEncode(dataValue.ToString());
			}
			if (this.DataFormatString.Length == 0)
			{
				return dataValue.ToString();
			}
			return string.Format(this.DataFormatString, dataValue);
		}

		// Token: 0x0600BE29 RID: 48681 RVA: 0x002A1FAB File Offset: 0x002A01AB
		public override ITreeListColumnEditor CreateDefaultColumnEditor()
		{
			return new TreeListTextBoxColumnEditor(this);
		}

		// Token: 0x0600BE2A RID: 48682 RVA: 0x002A1FB4 File Offset: 0x002A01B4
		protected override object GetColumnValueFromDataCell(TableCell cell)
		{
			string text = cell.Text;
			if (string.Equals(text.Trim(), this.EmptyDataText, StringComparison.InvariantCultureIgnoreCase))
			{
				return string.Empty;
			}
			return this.ConvertValueIfEmpty(text);
		}
	}
}

using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020011FA RID: 4602
	public abstract class TreeListDataColumn : TreeListColumn
	{
		// Token: 0x17003D47 RID: 15687
		// (get) Token: 0x0600BDF7 RID: 48631 RVA: 0x002A16EC File Offset: 0x0029F8EC
		// (set) Token: 0x0600BDF8 RID: 48632 RVA: 0x002A1719 File Offset: 0x0029F919
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string DataField
		{
			get
			{
				object obj = base.ViewState["DataField"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return obj.ToString();
			}
			set
			{
				base.ViewState["DataField"] = value;
			}
		}

		// Token: 0x17003D48 RID: 15688
		// (get) Token: 0x0600BDF9 RID: 48633 RVA: 0x002A172C File Offset: 0x0029F92C
		// (set) Token: 0x0600BDFA RID: 48634 RVA: 0x002A1755 File Offset: 0x0029F955
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("AllowSorting")]
		[Category("Behavior")]
		public virtual bool AllowSorting
		{
			get
			{
				object obj = base.ViewState["AllowSorting"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["AllowSorting"] = value;
			}
		}

		// Token: 0x17003D49 RID: 15689
		// (get) Token: 0x0600BDFB RID: 48635 RVA: 0x002A176D File Offset: 0x0029F96D
		protected override bool Sortable
		{
			get
			{
				return this.AllowSorting;
			}
		}

		// Token: 0x0600BDFC RID: 48636 RVA: 0x002A1775 File Offset: 0x0029F975
		protected override string GetSortExpression()
		{
			if (string.IsNullOrEmpty(this.SortExpression) && !string.IsNullOrEmpty(this.DataField) && this.AllowSorting)
			{
				return this.DataField;
			}
			return base.GetSortExpression();
		}

		// Token: 0x17003D4A RID: 15690
		// (get) Token: 0x0600BDFD RID: 48637 RVA: 0x002A17A8 File Offset: 0x0029F9A8
		// (set) Token: 0x0600BDFE RID: 48638 RVA: 0x002A17DC File Offset: 0x0029F9DC
		[DefaultValue(typeof(string))]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(GridDataTypeConverter))]
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
				value = TreeListTypeHelper.GetNonNullableType(value);
				if (!GridDataTypeConverter.SupportedTypes.Contains(value) && !value.IsEnum)
				{
					throw new NotSupportedException("Specified column DataType is not supported " + value.ToString());
				}
				base.ViewState["DataType"] = value;
			}
		}

		// Token: 0x17003D4B RID: 15691
		// (get) Token: 0x0600BDFF RID: 48639 RVA: 0x002A182D File Offset: 0x0029FA2D
		internal bool DataTypeIsSet
		{
			get
			{
				return base.ViewState["DataType"] != null;
			}
		}

		// Token: 0x0600BE00 RID: 48640 RVA: 0x002A1845 File Offset: 0x0029FA45
		protected override void InitializeDataCells(TableCell cell, int columnIndex, TreeListDataItem inItem)
		{
			cell.DataBinding += this.OnColumnDataCellBinding;
		}

		// Token: 0x0600BE01 RID: 48641 RVA: 0x002A185A File Offset: 0x0029FA5A
		protected override void InitializeFooterCells(TableCell cell, int columnIndex, TreeListFooterItem inItem)
		{
			base.InitializeFooterCells(cell, columnIndex, inItem);
		}

		// Token: 0x0600BE02 RID: 48642
		protected abstract void OnColumnDataCellBinding(object sender, EventArgs e);
	}
}

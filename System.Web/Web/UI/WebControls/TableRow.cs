using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004E6 RID: 1254
	[ToolboxItem(false)]
	[DefaultProperty("Cells")]
	[ParseChildren(true, "Cells")]
	[Bindable(false)]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TableRow : WebControl
	{
		// Token: 0x06003CEB RID: 15595 RVA: 0x00100396 File Offset: 0x000FF396
		public TableRow() : base(HtmlTextWriterTag.Tr)
		{
			base.PreventAutoID();
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06003CEC RID: 15596 RVA: 0x001003A6 File Offset: 0x000FF3A6
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[MergableProperty(false)]
		[WebSysDescription("TableRow_Cells")]
		public virtual TableCellCollection Cells
		{
			get
			{
				if (this.cells == null)
				{
					this.cells = new TableCellCollection(this);
				}
				return this.cells;
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06003CED RID: 15597 RVA: 0x001003C2 File Offset: 0x000FF3C2
		// (set) Token: 0x06003CEE RID: 15598 RVA: 0x001003DE File Offset: 0x000FF3DE
		[DefaultValue(HorizontalAlign.NotSet)]
		[WebSysDescription("TableItem_HorizontalAlign")]
		[WebCategory("Layout")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				return ((TableItemStyle)base.ControlStyle).HorizontalAlign;
			}
			set
			{
				((TableItemStyle)base.ControlStyle).HorizontalAlign = value;
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06003CEF RID: 15599 RVA: 0x001003F4 File Offset: 0x000FF3F4
		// (set) Token: 0x06003CF0 RID: 15600 RVA: 0x00100420 File Offset: 0x000FF420
		[WebSysDescription("TableRow_TableSection")]
		[DefaultValue(TableRowSection.TableBody)]
		[WebCategory("Accessibility")]
		public virtual TableRowSection TableSection
		{
			get
			{
				object obj = this.ViewState["TableSection"];
				if (obj != null)
				{
					return (TableRowSection)obj;
				}
				return TableRowSection.TableBody;
			}
			set
			{
				if (value < TableRowSection.TableHeader || value > TableRowSection.TableFooter)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["TableSection"] = value;
				if (value != TableRowSection.TableBody)
				{
					Control parent = this.Parent;
					if (parent != null)
					{
						Table table = parent as Table;
						if (table != null)
						{
							table.HasRowSections = true;
						}
					}
				}
			}
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06003CF1 RID: 15601 RVA: 0x00100475 File Offset: 0x000FF475
		// (set) Token: 0x06003CF2 RID: 15602 RVA: 0x00100491 File Offset: 0x000FF491
		[DefaultValue(VerticalAlign.NotSet)]
		[WebCategory("Layout")]
		[WebSysDescription("TableItem_VerticalAlign")]
		public virtual VerticalAlign VerticalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return VerticalAlign.NotSet;
				}
				return ((TableItemStyle)base.ControlStyle).VerticalAlign;
			}
			set
			{
				((TableItemStyle)base.ControlStyle).VerticalAlign = value;
			}
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x001004A4 File Offset: 0x000FF4A4
		protected override Style CreateControlStyle()
		{
			return new TableItemStyle(this.ViewState);
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x001004B1 File Offset: 0x000FF4B1
		protected override ControlCollection CreateControlCollection()
		{
			return new TableRow.CellControlCollection(this);
		}

		// Token: 0x0400274B RID: 10059
		private TableCellCollection cells;

		// Token: 0x020004E7 RID: 1255
		protected class CellControlCollection : ControlCollection
		{
			// Token: 0x06003CF5 RID: 15605 RVA: 0x001004B9 File Offset: 0x000FF4B9
			internal CellControlCollection(Control owner) : base(owner)
			{
			}

			// Token: 0x06003CF6 RID: 15606 RVA: 0x001004C4 File Offset: 0x000FF4C4
			public override void Add(Control child)
			{
				if (child is TableCell)
				{
					base.Add(child);
					return;
				}
				throw new ArgumentException(SR.GetString("Cannot_Have_Children_Of_Type", new object[]
				{
					"TableRow",
					child.GetType().Name.ToString(CultureInfo.InvariantCulture)
				}));
			}

			// Token: 0x06003CF7 RID: 15607 RVA: 0x00100518 File Offset: 0x000FF518
			public override void AddAt(int index, Control child)
			{
				if (child is TableCell)
				{
					base.AddAt(index, child);
					return;
				}
				throw new ArgumentException(SR.GetString("Cannot_Have_Children_Of_Type", new object[]
				{
					"TableRow",
					child.GetType().Name.ToString(CultureInfo.InvariantCulture)
				}));
			}
		}
	}
}

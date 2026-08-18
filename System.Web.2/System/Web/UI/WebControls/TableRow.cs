using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004EC RID: 1260
	[Bindable(false)]
	[DefaultProperty("Cells")]
	[ParseChildren(true, "Cells")]
	[ToolboxItem(false)]
	[Designer("System.Web.UI.Design.WebControls.PreviewControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class TableRow : WebControl
	{
		// Token: 0x06003ECF RID: 16079 RVA: 0x000CA288 File Offset: 0x000C8488
		public TableRow() : base(HtmlTextWriterTag.Tr)
		{
			base.PreventAutoID();
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06003ED0 RID: 16080 RVA: 0x000CA298 File Offset: 0x000C8498
		[MergableProperty(false)]
		[WebSysDescription("TableRow_Cells")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
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

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06003ED1 RID: 16081 RVA: 0x000C9850 File Offset: 0x000C7A50
		// (set) Token: 0x06003ED2 RID: 16082 RVA: 0x000C986C File Offset: 0x000C7A6C
		[WebCategory("Layout")]
		[DefaultValue(HorizontalAlign.NotSet)]
		[WebSysDescription("TableItem_HorizontalAlign")]
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

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06003ED3 RID: 16083 RVA: 0x000853AC File Offset: 0x000835AC
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return this.RenderingCompatibility < VersionUtil.Framework40;
			}
		}

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06003ED4 RID: 16084 RVA: 0x000CA2B4 File Offset: 0x000C84B4
		// (set) Token: 0x06003ED5 RID: 16085 RVA: 0x000CA2E0 File Offset: 0x000C84E0
		[WebCategory("Accessibility")]
		[DefaultValue(TableRowSection.TableBody)]
		[WebSysDescription("TableRow_TableSection")]
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

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06003ED6 RID: 16086 RVA: 0x000C98FD File Offset: 0x000C7AFD
		// (set) Token: 0x06003ED7 RID: 16087 RVA: 0x000C9919 File Offset: 0x000C7B19
		[WebCategory("Layout")]
		[DefaultValue(VerticalAlign.NotSet)]
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

		// Token: 0x06003ED8 RID: 16088 RVA: 0x000C9AE5 File Offset: 0x000C7CE5
		protected override Style CreateControlStyle()
		{
			return new TableItemStyle(this.ViewState);
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x000CA335 File Offset: 0x000C8535
		protected override ControlCollection CreateControlCollection()
		{
			return new TableRow.CellControlCollection(this);
		}

		// Token: 0x0400241C RID: 9244
		private TableCellCollection cells;

		// Token: 0x020009C8 RID: 2504
		protected class CellControlCollection : ControlCollection
		{
			// Token: 0x06006C63 RID: 27747 RVA: 0x00061D30 File Offset: 0x0005FF30
			internal CellControlCollection(Control owner) : base(owner)
			{
			}

			// Token: 0x06006C64 RID: 27748 RVA: 0x00183BF4 File Offset: 0x00181DF4
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

			// Token: 0x06006C65 RID: 27749 RVA: 0x00183C48 File Offset: 0x00181E48
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

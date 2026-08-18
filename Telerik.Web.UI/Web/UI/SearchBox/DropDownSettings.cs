using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SearchBox
{
	// Token: 0x02000EF1 RID: 3825
	public sealed class DropDownSettings : ObjectWithState, IDisposable
	{
		// Token: 0x060090F5 RID: 37109 RVA: 0x0020A276 File Offset: 0x00208476
		internal DropDownSettings(StateBag ownerViewState) : base("DropDownSettings", ownerViewState)
		{
		}

		// Token: 0x17002DE7 RID: 11751
		// (get) Token: 0x060090F6 RID: 37110 RVA: 0x0020A284 File Offset: 0x00208484
		// (set) Token: 0x060090F7 RID: 37111 RVA: 0x0020A2A9 File Offset: 0x002084A9
		[TypeConverter(typeof(UnitConverter))]
		[DefaultValue(typeof(Unit), "")]
		[Description("The width of the DropDown area")]
		public Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x17002DE8 RID: 11752
		// (get) Token: 0x060090F8 RID: 37112 RVA: 0x0020A2C1 File Offset: 0x002084C1
		// (set) Token: 0x060090F9 RID: 37113 RVA: 0x0020A2E6 File Offset: 0x002084E6
		[Description("The height of the DropDown area")]
		[TypeConverter(typeof(UnitConverter))]
		[DefaultValue(typeof(Unit), "")]
		public Unit Height
		{
			get
			{
				return (Unit)(base.ViewState["Height"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x17002DE9 RID: 11753
		// (get) Token: 0x060090FA RID: 37114 RVA: 0x0020A2FE File Offset: 0x002084FE
		// (set) Token: 0x060090FB RID: 37115 RVA: 0x0020A31E File Offset: 0x0020851E
		[Description("Css class of the dropdown")]
		[DefaultValue("")]
		[Category("Appearance")]
		public string CssClass
		{
			get
			{
				return ((string)base.ViewState["CssClass"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["CssClass"] = value;
			}
		}

		// Token: 0x17002DEA RID: 11754
		// (get) Token: 0x060090FC RID: 37116 RVA: 0x0020A331 File Offset: 0x00208531
		// (set) Token: 0x060090FD RID: 37117 RVA: 0x0020A351 File Offset: 0x00208551
		[Category("Client")]
		[DefaultValue("")]
		[Description("Gets or sets the HTML template of the drop down item when added on the client.")]
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public string ClientTemplate
		{
			get
			{
				return (base.ViewState["ClientTemplate"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ClientTemplate"] = value;
			}
		}

		// Token: 0x17002DEB RID: 11755
		// (get) Token: 0x060090FE RID: 37118 RVA: 0x0020A364 File Offset: 0x00208564
		// (set) Token: 0x060090FF RID: 37119 RVA: 0x0020A36C File Offset: 0x0020856C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TemplateContainer(typeof(DropDownItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Bindable(false)]
		[Browsable(false)]
		public ITemplate ItemTemplate
		{
			get
			{
				return this._itemTemplate;
			}
			set
			{
				this._itemTemplate = value;
			}
		}

		// Token: 0x17002DEC RID: 11756
		// (get) Token: 0x06009100 RID: 37120 RVA: 0x0020A375 File Offset: 0x00208575
		// (set) Token: 0x06009101 RID: 37121 RVA: 0x0020A37D File Offset: 0x0020857D
		[Bindable(false)]
		[TemplateContainer(typeof(RadSearchBox))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public ITemplate HeaderTemplate { get; set; }

		// Token: 0x17002DED RID: 11757
		// (get) Token: 0x06009102 RID: 37122 RVA: 0x0020A386 File Offset: 0x00208586
		// (set) Token: 0x06009103 RID: 37123 RVA: 0x0020A38E File Offset: 0x0020858E
		[Browsable(false)]
		[TemplateContainer(typeof(RadSearchBox))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Bindable(false)]
		public ITemplate FooterTemplate { get; set; }

		// Token: 0x17002DEE RID: 11758
		// (get) Token: 0x06009104 RID: 37124 RVA: 0x0020A397 File Offset: 0x00208597
		[Browsable(false)]
		public WebControl Header
		{
			get
			{
				if (this._header == null)
				{
					this._header = new WebControl(HtmlTextWriterTag.Div);
				}
				return this._header;
			}
		}

		// Token: 0x17002DEF RID: 11759
		// (get) Token: 0x06009105 RID: 37125 RVA: 0x0020A3B4 File Offset: 0x002085B4
		[Browsable(false)]
		public WebControl Footer
		{
			get
			{
				if (this._footer == null)
				{
					this._footer = new WebControl(HtmlTextWriterTag.Div);
				}
				return this._footer;
			}
		}

		// Token: 0x06009106 RID: 37126 RVA: 0x0020A3D4 File Offset: 0x002085D4
		public void Dispose()
		{
			if (!this._disposed)
			{
				if (this._header != null)
				{
					this._header.Dispose();
					this._header = null;
				}
				if (this._footer != null)
				{
					this._footer.Dispose();
					this._footer = null;
				}
			}
			GC.SuppressFinalize(this);
			this._disposed = true;
		}

		// Token: 0x0400292E RID: 10542
		private bool _disposed;

		// Token: 0x0400292F RID: 10543
		private WebControl _header;

		// Token: 0x04002930 RID: 10544
		private WebControl _footer;

		// Token: 0x04002931 RID: 10545
		private ITemplate _itemTemplate;
	}
}

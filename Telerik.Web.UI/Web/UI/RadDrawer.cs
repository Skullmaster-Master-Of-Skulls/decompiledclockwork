using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.Drawer;

namespace Telerik.Web.UI
{
	// Token: 0x02000042 RID: 66
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadDrawer))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadDrawer))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadDrawer))]
	[RequiredScript(typeof(Html5Drawer))]
	[ParseChildren(ChildrenAsProperties = true)]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ClientScriptResource("Telerik.Web.UI.RadDrawer", "Telerik.Web.UI.Drawer.Scripts.RadDrawer.js")]
	[EmbeddedSkin("Drawer", typeof(RadDrawer))]
	[EmbeddedSkin("Drawer", "Default", typeof(RadDrawer))]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Classic, typeof(RadDrawer))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Lightweight, typeof(RadDrawer))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Mobile, typeof(RadDrawer))]
	public class RadDrawer : RadWebControl, INamingContainer
	{
		// Token: 0x06000218 RID: 536 RVA: 0x00005C44 File Offset: 0x00003E44
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "initialize", this.ClientEvents.OnInitialize);
			RadWebControl.DescribeEvent(descriptor, "load", this.ClientEvents.OnLoad);
			RadWebControl.DescribeEvent(descriptor, "show", this.ClientEvents.OnShow);
			RadWebControl.DescribeEvent(descriptor, "hide", this.ClientEvents.OnHide);
			RadWebControl.DescribeEvent(descriptor, "itemClick", this.ClientEvents.OnItemClick);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00005CC6 File Offset: 0x00003EC6
		public RadDrawer()
		{
			this.RegisterJSConverters();
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600021A RID: 538 RVA: 0x00005CDF File Offset: 0x00003EDF
		// (set) Token: 0x0600021B RID: 539 RVA: 0x00005D00 File Offset: 0x00003F00
		[DefaultValue(false)]
		public bool Navigatable
		{
			get
			{
				return (bool)(this.ViewState["Navigatable"] ?? false);
			}
			set
			{
				this.ViewState["Navigatable"] = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00005D18 File Offset: 0x00003F18
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00005D39 File Offset: 0x00003F39
		[DefaultValue(DrawerPositionType.Left)]
		public DrawerPositionType Position
		{
			get
			{
				return (DrawerPositionType)(this.ViewState["Position"] ?? DrawerPositionType.Left);
			}
			set
			{
				this.ViewState["Position"] = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00005D51 File Offset: 0x00003F51
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00005D72 File Offset: 0x00003F72
		[DefaultValue(DrawerModeType.Overlay)]
		public DrawerModeType Mode
		{
			get
			{
				return (DrawerModeType)(this.ViewState["Mode"] ?? DrawerModeType.Overlay);
			}
			set
			{
				this.ViewState["Mode"] = value;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00005D8A File Offset: 0x00003F8A
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00005DAA File Offset: 0x00003FAA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue("")]
		[Browsable(true)]
		[Bindable(true)]
		public string ItemsTemplate
		{
			get
			{
				return (string)(this.ViewState["ItemsTemplate"] ?? "");
			}
			set
			{
				this.ViewState["ItemsTemplate"] = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00005DBD File Offset: 0x00003FBD
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00005DE6 File Offset: 0x00003FE6
		[DefaultValue(0.0)]
		public double DrawerWidth
		{
			get
			{
				return (double)(this.ViewState["DrawerWidth"] ?? 0.0);
			}
			set
			{
				this.ViewState["DrawerWidth"] = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00005DFE File Offset: 0x00003FFE
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00005E27 File Offset: 0x00004027
		[DefaultValue(0.0)]
		public double MinHeight
		{
			get
			{
				return (double)(this.ViewState["MinHeight"] ?? 0.0);
			}
			set
			{
				this.ViewState["MinHeight"] = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00005E3F File Offset: 0x0000403F
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00005E60 File Offset: 0x00004060
		[DefaultValue(false)]
		public bool Mini
		{
			get
			{
				return (bool)(this.ViewState["Mini"] ?? false);
			}
			set
			{
				this.ViewState["Mini"] = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00005E78 File Offset: 0x00004078
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Mini MiniSettings
		{
			get
			{
				if (this._mini == null)
				{
					this._mini = new Mini();
				}
				return this._mini;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00005E93 File Offset: 0x00004093
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00005EB4 File Offset: 0x000040B4
		[DefaultValue(true)]
		public bool SwipeToOpen
		{
			get
			{
				return (bool)(this.ViewState["SwipeToOpen"] ?? true);
			}
			set
			{
				this.ViewState["SwipeToOpen"] = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00005ECC File Offset: 0x000040CC
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public DrawerClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new DrawerClientEvents();
				}
				return this._clientEvents;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00005EE7 File Offset: 0x000040E7
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00005F08 File Offset: 0x00004108
		[DefaultValue(false)]
		[Category("Appearance")]
		[Description("Specifies whether the outer borders will be displayed.")]
		public bool ShowBorders
		{
			get
			{
				return (bool)(this.ViewState["ShowBorders"] ?? false);
			}
			set
			{
				this.ViewState["ShowBorders"] = value;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00005F20 File Offset: 0x00004120
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("_skin", base.RuntimeSkin);
			descriptor.AddScriptProperty("_options", this.serializer.Serialize(this));
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00005F51 File Offset: 0x00004151
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptObjectBuilder.RegisterCssReferences(this);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00005F60 File Offset: 0x00004160
		private void RegisterJSConverters()
		{
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new RadDrawerConverter(),
				new MiniConverter()
			};
			this.serializer.RegisterConverters(converters);
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00005F97 File Offset: 0x00004197
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00005F9B File Offset: 0x0000419B
		protected override string CssClassFormatString
		{
			get
			{
				return "RadDrawer RadDrawer_{0}";
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00005FA2 File Offset: 0x000041A2
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this._contentContainer = new SingleTemplateContainer(this);
			this._contentContainer.ID = "Content";
			this.Controls.Add(this._contentContainer);
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00005FD7 File Offset: 0x000041D7
		[Browsable(false)]
		public SingleTemplateContainer ContentContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._contentContainer;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00005FE5 File Offset: 0x000041E5
		// (set) Token: 0x06000236 RID: 566 RVA: 0x00005FF8 File Offset: 0x000041F8
		[Bindable(false)]
		[Browsable(false)]
		[TemplateInstance(TemplateInstance.Single)]
		[TemplateContainer(typeof(SingleTemplateContainer))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ITemplate ContentTemplate
		{
			get
			{
				this.EnsureChildControls();
				return this.ContentContainer.Template;
			}
			set
			{
				this.EnsureChildControls();
				this.ContentContainer.Template = value;
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000600C File Offset: 0x0000420C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.ClientEvents).LoadViewState(array[num++]);
			((IStateManager)this.MiniSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006054 File Offset: 0x00004254
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState(),
				((IStateManager)this.MiniSettings).SaveViewState()
			};
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006090 File Offset: 0x00004290
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ClientEvents).TrackViewState();
			((IStateManager)this.MiniSettings).TrackViewState();
		}

		// Token: 0x04000046 RID: 70
		private Mini _mini;

		// Token: 0x04000047 RID: 71
		private DrawerClientEvents _clientEvents;

		// Token: 0x04000048 RID: 72
		private readonly AdvancedJavaScriptSerializer serializer = new AdvancedJavaScriptSerializer();

		// Token: 0x04000049 RID: 73
		private SingleTemplateContainer _contentContainer;
	}
}

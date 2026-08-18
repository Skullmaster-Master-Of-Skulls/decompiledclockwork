using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Xml.Serialization;
using Telerik.Licensing;
using Telerik.Web.UI.Menu.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x0200089B RID: 2203
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Mobile, typeof(RadButton))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadContextMenu))]
	[ParseChildren(true)]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxData("<{0}:RadContextMenu Runat=server></{0}:RadContextMenu>")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[PersistChildren(false)]
	[XmlRoot("Menu")]
	[ClientScriptResource("Telerik.Web.UI.RadContextMenu", "Telerik.Web.UI.Menu.ContextMenu.RadContextMenuScripts.js", LoadOrder = 7)]
	[Designer("Telerik.Web.Design.RadContextMenuDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LightweightRendering]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(RadContextMenu))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	public class RadContextMenu : RadMenu
	{
		// Token: 0x060051DD RID: 20957 RVA: 0x000FF3A0 File Offset: 0x000FD5A0
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "enableSelection", this.EnableSelection, false);
			base.DescribeProperty<ItemFlow>(descriptor, "_flow", this.Flow, ItemFlow.Vertical);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060051DE RID: 20958 RVA: 0x000FF3D0 File Offset: 0x000FD5D0
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "hidden", this.OnClientHidden);
			RadDataBoundControl.DescribeEvent(descriptor, "hiding", this.OnClientHiding);
			RadDataBoundControl.DescribeEvent(descriptor, "showing", this.OnClientShowing);
			RadDataBoundControl.DescribeEvent(descriptor, "shown", this.OnClientShown);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x17001AD3 RID: 6867
		// (get) Token: 0x060051DF RID: 20959 RVA: 0x000FF428 File Offset: 0x000FD628
		[DefaultValue(null)]
		[Editor("Telerik.Web.Design.ContextMenuTargetComponentEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public virtual ContextMenuTargetCollection Targets
		{
			get
			{
				if (this._targets == null)
				{
					this._targets = new ContextMenuTargetCollection(this);
				}
				return this._targets;
			}
		}

		// Token: 0x17001AD4 RID: 6868
		// (get) Token: 0x060051E0 RID: 20960 RVA: 0x000FF444 File Offset: 0x000FD644
		// (set) Token: 0x060051E1 RID: 20961 RVA: 0x000FF464 File Offset: 0x000FD664
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("showing")]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the context menu is to be displayed.")]
		public virtual string OnClientShowing
		{
			get
			{
				return (string)(this.ViewState["OnClientShowing"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientShowing"] = value;
			}
		}

		// Token: 0x17001AD5 RID: 6869
		// (get) Token: 0x060051E2 RID: 20962 RVA: 0x000FF477 File Offset: 0x000FD677
		// (set) Token: 0x060051E3 RID: 20963 RVA: 0x000FF497 File Offset: 0x000FD697
		[ClientPropertyName("shown")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the context menu is displayed.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public virtual string OnClientShown
		{
			get
			{
				return (string)(this.ViewState["OnClientShown"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientShown"] = value;
			}
		}

		// Token: 0x17001AD6 RID: 6870
		// (get) Token: 0x060051E4 RID: 20964 RVA: 0x000FF4AA File Offset: 0x000FD6AA
		// (set) Token: 0x060051E5 RID: 20965 RVA: 0x000FF4CA File Offset: 0x000FD6CA
		[ClientPropertyName("hiding")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Description("The name of the javascript function called when the context menu is to be hidden.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		public virtual string OnClientHiding
		{
			get
			{
				return (string)(this.ViewState["OnClientHiding"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientHiding"] = value;
			}
		}

		// Token: 0x17001AD7 RID: 6871
		// (get) Token: 0x060051E6 RID: 20966 RVA: 0x000FF4DD File Offset: 0x000FD6DD
		// (set) Token: 0x060051E7 RID: 20967 RVA: 0x000FF4FD File Offset: 0x000FD6FD
		[ClientPropertyName("hidden")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[Description("The name of the javascript function called when the context menu is hidden.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientHidden
		{
			get
			{
				return (string)(this.ViewState["OnClientHidden"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientHidden"] = value;
			}
		}

		// Token: 0x17001AD8 RID: 6872
		// (get) Token: 0x060051E8 RID: 20968 RVA: 0x000FF510 File Offset: 0x000FD710
		// (set) Token: 0x060051E9 RID: 20969 RVA: 0x000FF531 File Offset: 0x000FD731
		[ClientPropertyName("_flow")]
		[DefaultValue(ItemFlow.Vertical)]
		[Browsable(false)]
		[ClientControlProperty]
		public override ItemFlow Flow
		{
			get
			{
				return (ItemFlow)(this.ViewState["Flow"] ?? ItemFlow.Vertical);
			}
			set
			{
				base.Flow = value;
			}
		}

		// Token: 0x17001AD9 RID: 6873
		// (get) Token: 0x060051EA RID: 20970 RVA: 0x000FF53A File Offset: 0x000FD73A
		// (set) Token: 0x060051EB RID: 20971 RVA: 0x000FF55B File Offset: 0x000FD75B
		[Category("Behavior")]
		[ClientPropertyName("enableSelection")]
		[DefaultValue(false)]
		[ClientControlProperty]
		[Description("a value indicating if the currently selected item will be tracked and highlighted")]
		public override bool EnableSelection
		{
			get
			{
				return (bool)(this.ViewState["EnableSelection"] ?? false);
			}
			set
			{
				this.ViewState["EnableSelection"] = value;
			}
		}

		// Token: 0x17001ADA RID: 6874
		// (get) Token: 0x060051EC RID: 20972 RVA: 0x000FF573 File Offset: 0x000FD773
		protected internal override bool SupportsAdaptiveRendering
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x000FF576 File Offset: 0x000FD776
		protected internal override IRenderer CreateControlRenderer()
		{
			return RendererFactory.CreateContextMenuRenderer(this);
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x000FF57E File Offset: 0x000FD77E
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x000FF58C File Offset: 0x000FD78C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x000FF5A0 File Offset: 0x000FD7A0
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			this.DescribeTargets(descriptor);
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x000FF5B0 File Offset: 0x000FD7B0
		protected virtual void DescribeTargets(IScriptDescriptor descriptor)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new ContextMenuTargetConverter()
			});
			this.ResolveControlTargetIds();
			descriptor.AddScriptProperty("targets", javaScriptSerializer.Serialize(this.Targets));
		}

		// Token: 0x060051F2 RID: 20978 RVA: 0x000FF5F8 File Offset: 0x000FD7F8
		protected virtual void ResolveControlTargetIds()
		{
			for (int i = 0; i < this.Targets.Count; i++)
			{
				if (this.Targets[i] is ContextMenuControlTarget)
				{
					ContextMenuControlTarget contextMenuControlTarget = (ContextMenuControlTarget)this.Targets[i];
					Control control = ChildControlHelper.FindControlRecursive(this, contextMenuControlTarget.ControlID, null);
					if (control != null)
					{
						contextMenuControlTarget.ControlID = control.ClientID;
					}
				}
			}
		}

		// Token: 0x17001ADB RID: 6875
		// (get) Token: 0x060051F3 RID: 20979 RVA: 0x000FF65D File Offset: 0x000FD85D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override KeyboardNavigationSettings KeyboardNavigationSettings
		{
			get
			{
				throw new InvalidOperationException("ContextMenu does not support KeyboardNavigationSettings");
			}
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x000FF669 File Offset: 0x000FD869
		protected virtual void LoadTargetsViewState(object[] viewState)
		{
			((IStateManager)this.Targets).LoadViewState(viewState[1]);
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x000FF679 File Offset: 0x000FD879
		protected virtual object SaveTargetsViewState()
		{
			return ((IStateManager)this.Targets).SaveViewState();
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x000FF686 File Offset: 0x000FD886
		protected virtual void TrackTargetsViewState()
		{
			((IStateManager)this.Targets).TrackViewState();
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x000FF694 File Offset: 0x000FD894
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			this.LoadTargetsViewState(array);
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x000FF6B8 File Offset: 0x000FD8B8
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList
			{
				base.SaveViewState(),
				this.SaveTargetsViewState()
			};
			return arrayList.ToArray();
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x000FF6ED File Offset: 0x000FD8ED
		protected override void TrackViewState()
		{
			base.TrackViewState();
			this.TrackTargetsViewState();
		}

		// Token: 0x04001407 RID: 5127
		private ContextMenuTargetCollection _targets;
	}
}

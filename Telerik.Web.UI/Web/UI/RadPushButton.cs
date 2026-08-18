using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.ButtonBase;
using Telerik.Web.UI.ButtonJavaScriptSerialization;
using Telerik.Web.UI.ButtonNS;
using Telerik.Web.UI.ButtonNS.JavaScriptSerialization;
using Telerik.Web.UI.ButtonRendering;

namespace Telerik.Web.UI
{
	// Token: 0x020000EE RID: 238
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(PostBackButtonBase))]
	[ClientScriptResource("Telerik.Web.UI.RadPushButton", "Telerik.Web.UI.Button.RadButtonScripts.js")]
	[EmbeddedSkin("Button")]
	[EmbeddedSkin("Button", "Default")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadButton))]
	[TelerikToolboxCategory("Navigation")]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	[SupportsEventValidation]
	[Designer("Telerik.Web.Design.RadPushButtonDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxBitmap(typeof(RadPushButton), "Telerik.Web.UI.Button.png")]
	[ToolboxData("<{0}:RadPushButton runat=\"server\" Text=\"RadPushButton\"></{0}:RadPushButton>")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	public class RadPushButton : PostBackButtonBase, IButtonControl, IPostBackEventHandler, INamingContainer, IJavaScriptConverterProvider
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x000242CB File Offset: 0x000224CB
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "primary", this.Primary, false);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x000242E7 File Offset: 0x000224E7
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x000242F0 File Offset: 0x000224F0
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptSerializer javaScriptSerializer = JavaScriptSerializeProvider.CreateSerializer(this);
			descriptor.AddScriptProperty("iconData", javaScriptSerializer.Serialize(this.Icon));
			descriptor.AddProperty("_hasIcon", this.HasIcon);
			descriptor.AddScriptProperty("confirmSettings", javaScriptSerializer.Serialize(this.ConfirmSettings));
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00024358 File Offset: 0x00022558
		public virtual IEnumerable<JavaScriptConverter> GetJsConverters()
		{
			return new JavaScriptConverter[]
			{
				new RadButtonConfirmSettingsConverter(),
				new ButtonIconConverter
				{
					ResolveUrl = ((string url) => base.ResolveUrl(url))
				}
			};
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00024391 File Offset: 0x00022591
		private void ClearTemplate()
		{
			this.Controls.Clear();
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002439E File Offset: 0x0002259E
		private void ApplyTemplate()
		{
			if (this._contentTemplate != null)
			{
				this._contentTemplate.InstantiateIn(this);
			}
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000243B4 File Offset: 0x000225B4
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			this.Primary = (clientState.ContainsKey("primary") && (bool)clientState["primary"]);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x000243E4 File Offset: 0x000225E4
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Icon).LoadViewState(array[1]);
			((IStateManager)this.ConfirmSettings).LoadViewState(array[2]);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x00024420 File Offset: 0x00022620
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Icon).SaveViewState(),
				((IStateManager)this.ConfirmSettings).SaveViewState()
			};
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0002445C File Offset: 0x0002265C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Icon).TrackViewState();
			((IStateManager)this.ConfirmSettings).TrackViewState();
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0002447A File Offset: 0x0002267A
		protected override IRenderer CreateControlRenderer()
		{
			return RendererFactory.GetRenderer(this);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00024482 File Offset: 0x00022682
		protected override void Render(HtmlTextWriter writer)
		{
			this.RegisterForEventValidation();
			base.Render(writer);
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00024491 File Offset: 0x00022691
		internal bool HasIcon
		{
			get
			{
				return !this.IsTemplateInitialized && this.Icon.ShowIcon;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x000244A8 File Offset: 0x000226A8
		internal bool IsTemplateInitialized
		{
			get
			{
				this.EnsureChildControls();
				return this.ContentTemplate != null || this.Controls.Count > 0;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x000244C8 File Offset: 0x000226C8
		public override string ButtonName
		{
			get
			{
				return "RadPushButton";
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x000244CF File Offset: 0x000226CF
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x000244F0 File Offset: 0x000226F0
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("Gets/Sets the primary appearance of the button.")]
		[ClientControlProperty]
		[ClientPropertyName("primary")]
		public bool Primary
		{
			get
			{
				return (bool)(this.ViewState["Primary"] ?? false);
			}
			set
			{
				this.ViewState["Primary"] = value;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00024508 File Offset: 0x00022708
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x00024510 File Offset: 0x00022710
		[TemplateContainer(typeof(RadPushButton))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets or sets the template for the Button control.")]
		public ITemplate ContentTemplate
		{
			get
			{
				return this._contentTemplate;
			}
			set
			{
				this._contentTemplate = value;
				this.ClearTemplate();
				this.ApplyTemplate();
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00024528 File Offset: 0x00022728
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the object that controls the Primary and Secondary Icon related properties.")]
		[MergableProperty(false)]
		[DefaultValue(null)]
		public ButtonIcon Icon
		{
			get
			{
				ButtonIcon result;
				if ((result = this._icon) == null)
				{
					result = (this._icon = new ButtonIcon());
				}
				return result;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0002454D File Offset: 0x0002274D
		[Description("Gets the object that controls the built-in confirmation dialog properties.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public RadButtonConfirmSettings ConfirmSettings
		{
			get
			{
				if (this._confirmSettings == null)
				{
					this._confirmSettings = new RadButtonConfirmSettings();
				}
				return this._confirmSettings;
			}
		}

		// Token: 0x04000272 RID: 626
		private ITemplate _contentTemplate;

		// Token: 0x04000273 RID: 627
		private ButtonIcon _icon;

		// Token: 0x04000274 RID: 628
		private RadButtonConfirmSettings _confirmSettings;
	}
}

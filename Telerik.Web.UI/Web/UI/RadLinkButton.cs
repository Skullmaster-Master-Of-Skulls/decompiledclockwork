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
using Telerik.Web.UI.ButtonRendering;

namespace Telerik.Web.UI
{
	// Token: 0x020000DB RID: 219
	[Designer("Telerik.Web.Design.RadLinkButtonDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[EmbeddedSkin("Button")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadButton))]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	[SupportsEventValidation]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadLinkButton), "Telerik.Web.UI.Button.png")]
	[ToolboxData("<{0}:RadLinkButton runat=\"server\" Text=\"RadLinkButton\"></{0}:RadLinkButton>")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(RadButtonBase))]
	[ClientScriptResource("Telerik.Web.UI.RadLinkButton", "Telerik.Web.UI.Button.RadButtonScripts.js")]
	[EmbeddedSkin("Button", "Default")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadButton))]
	public class RadLinkButton : RadButtonBase, INamingContainer
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x0001EE50 File Offset: 0x0001D050
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			JavaScriptSerializer jsserializer = this.GetJSSerializer();
			descriptor.AddScriptProperty("iconData", jsserializer.Serialize(this.Icon));
			descriptor.AddProperty("_hasIcon", this.HasIcon);
			descriptor.AddScriptProperty("confirmSettings", jsserializer.Serialize(this.ConfirmSettings));
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001EEB0 File Offset: 0x0001D0B0
		protected JavaScriptSerializer GetJSSerializer()
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(this.GetJsConverters());
			return javaScriptSerializer;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001EEDC File Offset: 0x0001D0DC
		protected virtual IEnumerable<JavaScriptConverter> GetJsConverters()
		{
			return new JavaScriptConverter[]
			{
				new ButtonIconConverter
				{
					ResolveUrl = ((string url) => base.ResolveUrl(url))
				},
				new RadButtonConfirmSettingsConverter()
			};
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001EF15 File Offset: 0x0001D115
		private void ClearTemplate()
		{
			this.Controls.Clear();
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001EF22 File Offset: 0x0001D122
		private void ApplyTemplate()
		{
			if (this._contentTemplate != null)
			{
				this._contentTemplate.InstantiateIn(this);
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001EF38 File Offset: 0x0001D138
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Icon).LoadViewState(array[1]);
			((IStateManager)this.ConfirmSettings).LoadViewState(array[2]);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001EF74 File Offset: 0x0001D174
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Icon).SaveViewState(),
				((IStateManager)this.ConfirmSettings).SaveViewState()
			};
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Icon).TrackViewState();
			((IStateManager)this.ConfirmSettings).TrackViewState();
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0001EFCE File Offset: 0x0001D1CE
		protected override IRenderer CreateControlRenderer()
		{
			return RendererFactory.GetRenderer(this);
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0001EFD6 File Offset: 0x0001D1D6
		internal bool HasIcon
		{
			get
			{
				return !this.IsTemplateInitialized && this.Icon.ShowIcon;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x0001EFED File Offset: 0x0001D1ED
		internal bool IsTemplateInitialized
		{
			get
			{
				this.EnsureChildControls();
				return this.ContentTemplate != null || this.Controls.Count > 0;
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001F010 File Offset: 0x0001D210
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			this.NavigateUrl = (string)clientState["navigateUrl"];
			this.Target = (((string)clientState["target"]) ?? string.Empty);
			this.Primary = (clientState.ContainsKey("primary") && (bool)clientState["primary"]);
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0001F07F File Offset: 0x0001D27F
		public override string ButtonName
		{
			get
			{
				return "RadLinkButton";
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x0001F086 File Offset: 0x0001D286
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x0001F0A7 File Offset: 0x0001D2A7
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

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0001F0BF File Offset: 0x0001D2BF
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0001F0DF File Offset: 0x0001D2DF
		[Bindable(true)]
		[ClientControlProperty]
		[DefaultValue("")]
		[Category("Action")]
		[Description("Gets or sets the URL to link to when the RadButton control is clicked.")]
		[UrlProperty]
		public string NavigateUrl
		{
			get
			{
				return (this.ViewState["NavigateUrl"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0001F0F2 File Offset: 0x0001D2F2
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x0001F112 File Offset: 0x0001D312
		[DefaultValue("")]
		[ClientControlProperty]
		[Description("Gets or sets the target window or frame in which to display the Web page content linked to when the RadButton control is clicked.")]
		[TypeConverter(typeof(TargetConverter))]
		[Category("Behavior")]
		[ClientPropertyName("target")]
		public string Target
		{
			get
			{
				return ((string)this.ViewState["Target"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0001F125 File Offset: 0x0001D325
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x0001F12D File Offset: 0x0001D32D
		[Description("Gets or sets the template for the Button control.")]
		[TemplateContainer(typeof(RadLinkButton))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0001F144 File Offset: 0x0001D344
		[Description("Gets the object that controls the Primary and Secondary Icon related properties.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[MergableProperty(false)]
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

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0001F169 File Offset: 0x0001D369
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[MergableProperty(false)]
		[Description("Gets the object that controls the built-in confirmation dialog properties.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x0600085C RID: 2140 RVA: 0x0001F184 File Offset: 0x0001D384
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "navigateUrl", base.ResolveClientUrl(this.NavigateUrl), "");
			base.DescribeProperty<bool>(descriptor, "primary", this.Primary, false);
			base.DescribeProperty<string>(descriptor, "target", this.Target, "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0001F1DF File Offset: 0x0001D3DF
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x040001F7 RID: 503
		private ITemplate _contentTemplate;

		// Token: 0x040001F8 RID: 504
		private ButtonIcon _icon;

		// Token: 0x040001F9 RID: 505
		private RadButtonConfirmSettings _confirmSettings;
	}
}

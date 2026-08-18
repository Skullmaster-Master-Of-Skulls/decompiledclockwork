using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Common;
using Telerik.Web.UI.Notification;
using Telerik.Web.UI.Notification.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x02000EB7 RID: 3767
	[ClientScriptResource("Telerik.Web.UI.RadNotification", "Telerik.Web.UI.Notification.RadNotification.js")]
	[Description("Telerik Notification component")]
	[ToolboxData("<{0}:RadNotification runat=\"server\"></{0}:RadNotification>")]
	[ToolboxBitmap(typeof(RadNotification), "Telerik.Web.UI.Notification.png")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadNotification))]
	[Designer("Telerik.Web.Design.RadNotificationDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadNotification))]
	[RequiredScript(typeof(PopupBehavior))]
	[RequiredScript(typeof(AnimationScripts))]
	[ParseChildren(ChildrenAsProperties = true)]
	[TelerikToolboxCategory("Container")]
	[RequiredScript(typeof(jQueryPlugins))]
	[LightweightRendering]
	[EmbeddedSkin("Notification", "Default")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("Notification")]
	public class RadNotification : RadWebControl, INamingContainer
	{
		// Token: 0x17002D5F RID: 11615
		// (get) Token: 0x06008F69 RID: 36713 RVA: 0x002058D9 File Offset: 0x00203AD9
		private bool SimpleContent
		{
			get
			{
				return (this.ContentTemplate == null || !this.ContentContainer.HasControls()) && !this.WebServiceSet;
			}
		}

		// Token: 0x17002D60 RID: 11616
		// (get) Token: 0x06008F6A RID: 36714 RVA: 0x002058FB File Offset: 0x00203AFB
		private bool WebServiceSet
		{
			get
			{
				return this.WebMethodName != string.Empty && this.WebMethodPath != string.Empty;
			}
		}

		// Token: 0x06008F6B RID: 36715 RVA: 0x00205924 File Offset: 0x00203B24
		protected override void OnInit(EventArgs e)
		{
			if (!base.DesignMode)
			{
				this.audioFormat = RadNotification.GetSupportedAudioFormat(this.Context);
			}
			base.OnInit(e);
			this.EnsureChildControls();
			if (!this.WebServiceSet)
			{
				this._xmlPanel.ServiceRequest += this.XmlPanelServiceRequest;
			}
		}

		// Token: 0x06008F6C RID: 36716 RVA: 0x00205978 File Offset: 0x00203B78
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this._contextMenu != null)
			{
				this._contextMenu.EnableEmbeddedBaseStylesheet = this.EnableEmbeddedBaseStylesheet;
				this._contextMenu.EnableEmbeddedSkins = this.EnableEmbeddedSkins;
				this._contextMenu.EnableAjaxSkinRendering = this.EnableAjaxSkinRendering;
				if (base.IsSkinSet)
				{
					this._contextMenu.Skin = base.RuntimeSkin;
				}
				this._contextMenu.EnableRoundedCorners = this.EnableRoundedCorners;
				this._contextMenu.EnableShadows = this.EnableShadow;
			}
		}

		// Token: 0x06008F6D RID: 36717 RVA: 0x00205A04 File Offset: 0x00203C04
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (this.Page.Form != null)
			{
				descriptor.AddProperty("formID", this.Page.Form.ClientID);
			}
			descriptor.AddProperty("_audioUrl", this.GetAudioUrl());
			descriptor.AddProperty("_audioMimeType", this.audioFormat.MimeType);
		}

		// Token: 0x06008F6E RID: 36718 RVA: 0x00205A68 File Offset: 0x00203C68
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this._xmlPanel == null)
			{
				this._xmlPanel = new RadXmlHttpPanel();
				this._xmlPanel.ID = "XmlPanel";
				this._xmlPanel.RenderMode = XmlHttpPanelRenderMode.Block;
				this._xmlPanel.EnableClientScriptEvaluation = true;
				this.Controls.Add(this._xmlPanel);
			}
			if (this._contentContainer == null)
			{
				this._contentContainer = new SingleTemplateContainer(this);
				this._contentContainer.ID = "C";
				this._contentContainer.Attributes.Add("class", "rnContentWrapper");
				this._xmlPanel.Controls.Add(this._contentContainer);
			}
			if (this._contextMenu == null)
			{
				this._contextMenu = new RadNotificationContextMenu();
				this._contextMenu.ID = "TitleMenu";
				this.Controls.Add(this._contextMenu);
			}
			if (this._hiddenState == null)
			{
				this._hiddenState = new HiddenField();
				this._hiddenState.ID = "hiddenState";
				this._xmlPanel.Controls.Add(this._hiddenState);
			}
		}

		// Token: 0x06008F6F RID: 36719 RVA: 0x00205B88 File Offset: 0x00203D88
		private void XmlPanelServiceRequest(object sender, RadXmlHttpPanelEventArgs e)
		{
			this.EnsureChildControls();
			if (this.SimpleContent)
			{
				this._contentContainer.SetRenderMethodDelegate(new RenderMethod(this.RenderSimpleContent));
			}
			this.OnCallbackUpdate(new RadNotificationEventArgs(e.Value));
		}

		// Token: 0x06008F70 RID: 36720 RVA: 0x00205BC0 File Offset: 0x00203DC0
		protected override IRenderer CreateControlRenderer()
		{
			return RendererFactory.GetRenderer(this);
		}

		// Token: 0x17002D61 RID: 11617
		// (get) Token: 0x06008F71 RID: 36721 RVA: 0x00205BC8 File Offset: 0x00203DC8
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06008F72 RID: 36722 RVA: 0x00205BCC File Offset: 0x00203DCC
		protected override Style CreateControlStyle()
		{
			Style result = base.CreateControlStyle();
			if (!base.DesignMode)
			{
				base.Style.Add("display", "none");
			}
			return result;
		}

		// Token: 0x06008F73 RID: 36723 RVA: 0x00205C07 File Offset: 0x00203E07
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			BaseClass.RenderVersionStamp(writer);
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			(this.Renderer as BaseRenderer).RenderPopupElement(writer, delegate(HtmlTextWriter w)
			{
				this.<>n__FabricatedMethod1(w);
			});
		}

		// Token: 0x06008F74 RID: 36724 RVA: 0x00205C46 File Offset: 0x00203E46
		protected void RenderSimpleContent(HtmlTextWriter writer, Control container)
		{
			(this.Renderer as BaseRenderer).RenderSimpleContent(writer);
		}

		// Token: 0x06008F75 RID: 36725 RVA: 0x00205C59 File Offset: 0x00203E59
		internal void RenderSimpleContentContainer(HtmlTextWriter writer)
		{
			if (this._contentDiv == null)
			{
				this.CreateContentDiv();
			}
			this._contentDiv.InnerHtml = this.Text;
			this._contentDiv.RenderControl(writer);
		}

		// Token: 0x06008F76 RID: 36726 RVA: 0x00205C88 File Offset: 0x00203E88
		private void CreateContentDiv()
		{
			this._contentDiv = new HtmlGenericControl("div");
			this._contentDiv.Attributes.Add("id", this.ClientID + "_simpleContentDiv");
			this._contentDiv.Attributes.Add("class", "rnContent");
		}

		// Token: 0x06008F77 RID: 36727 RVA: 0x00205CE4 File Offset: 0x00203EE4
		protected string GetAudioUrl()
		{
			string text = this.ShowSound.ToLower();
			string result = this.ShowSound;
			if (!string.IsNullOrEmpty(text) && text != "none")
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				string resourceName = string.Format("Telerik.Web.UI.Notification.EmbeddedSounds.{0}.{1}", text, this.audioFormat.FileExtension);
				if (executingAssembly.GetManifestResourceInfo(resourceName) != null)
				{
					char c = (this.AudioHandlerUrl.IndexOf('?') == -1) ? '?' : '&';
					string handlerNotificationAudio = RadNotification.HandlerNotificationAudio;
					result = base.ResolveUrl(string.Format("{0}{1}type={2}&sound={3}", new object[]
					{
						this.AudioHandlerUrl,
						c,
						handlerNotificationAudio,
						text
					}));
				}
			}
			return result;
		}

		// Token: 0x06008F78 RID: 36728 RVA: 0x00205DA0 File Offset: 0x00203FA0
		public virtual string GetIconUrl(string iconName)
		{
			if (Path.HasExtension(iconName) && iconName.StartsWith("~"))
			{
				return this.GetResolvedIconUrl(iconName);
			}
			if (!this.EnableEmbeddedSkins)
			{
				return iconName;
			}
			string text = iconName.ToLower();
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string resourceName = string.Format("Telerik.Web.UI.Skins.{0}.Notification.{1}.png", base.RuntimeSkin, text);
			if (executingAssembly.GetManifestResourceInfo(resourceName) != null)
			{
				return this.Page.ClientScript.GetWebResourceUrl(typeof(RadNotification), resourceName);
			}
			string resourceName2 = string.Format("Telerik.Web.UI.Skins.Common.Notification.{0}.png", text);
			if (executingAssembly.GetManifestResourceInfo(resourceName2) != null)
			{
				return this.Page.ClientScript.GetWebResourceUrl(typeof(RadNotification), resourceName2);
			}
			return iconName;
		}

		// Token: 0x06008F79 RID: 36729 RVA: 0x00205E4A File Offset: 0x0020404A
		public virtual string GetResolvedIconUrl(string iconUrl)
		{
			return base.ResolveUrl(iconUrl);
		}

		// Token: 0x06008F7A RID: 36730 RVA: 0x00205E54 File Offset: 0x00204054
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			string text = this.ClientID + "_rnMenuIcon";
			if (this.ShowTitleMenu && this._contextMenu != null && !this._contextMenu.titleTargetIsAdded(text))
			{
				ContextMenuElementTarget contextMenuElementTarget = new ContextMenuElementTarget();
				contextMenuElementTarget.ElementID = text;
				this._contextMenu.Targets.Add(contextMenuElementTarget);
			}
			if (this.SimpleContent && this.LoadContentOn == NotificationLoad.PageLoad)
			{
				this.CreateContentDiv();
				this._contentContainer.SetRenderMethodDelegate(new RenderMethod(this.RenderSimpleContent));
			}
			this.SetRenderModeChildRadControls();
		}

		// Token: 0x06008F7B RID: 36731 RVA: 0x00205EE7 File Offset: 0x002040E7
		private void SetRenderModeChildRadControls()
		{
			this.SetRenderModeToChildControl(this._contextMenu);
		}

		// Token: 0x06008F7C RID: 36732 RVA: 0x00205EF5 File Offset: 0x002040F5
		private void SetRenderModeToChildControl(ISkinnableControl control)
		{
			if (control != null)
			{
				control.RenderMode = this.RenderMode;
			}
		}

		// Token: 0x06008F7D RID: 36733 RVA: 0x00205F06 File Offset: 0x00204106
		public void Show()
		{
			this.Show("null");
		}

		// Token: 0x06008F7E RID: 36734 RVA: 0x00205F14 File Offset: 0x00204114
		public void Show(string text)
		{
			string text2 = Guid.NewGuid().ToString().GetHashCode().ToString("x");
			string text3 = this.ClientID + text2;
			string script = string.Format("\r\n                  <script type='text/javascript' id='{2}'>\r\n                     function {0}()\r\n                     {{\r\n                      var n = $find('{1}');\r\n                      var t = '{3}';\r\n                      if(t != 'null') n.set_text(t);\r\n                       n.show();\r\n                       Sys.Application.remove_load({0});\r\n                       var scriptBlock = document.getElementById('{2}');\r\n\t\t\t\t\t   if(scriptBlock) \r\n\t\t\t\t\t   {{\r\n                           var parent = scriptBlock.parentNode;\r\n                           if(parent) parent.removeChild(scriptBlock);\r\n                       }}\r\n                  }};\r\n                  Sys.Application.add_load({0});</script>", new object[]
			{
				text3,
				this.ClientID,
				text2,
				text
			});
			ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), text2, script, false);
		}

		// Token: 0x06008F7F RID: 36735 RVA: 0x00205F9C File Offset: 0x0020419C
		internal static AudioFormat GetSupportedAudioFormat(HttpContext context)
		{
			HttpBrowserCapabilities browser = context.Request.Browser;
			bool flag = browser.IsBrowser("IE") || browser.IsBrowser("InternetExplorer");
			if (flag && browser.MajorVersion > 8)
			{
				return new AudioFormat(AudioFormats.Mp3);
			}
			return new AudioFormat(AudioFormats.Wave);
		}

		// Token: 0x17002D62 RID: 11618
		// (get) Token: 0x06008F80 RID: 36736 RVA: 0x00205FEA File Offset: 0x002041EA
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17002D63 RID: 11619
		// (get) Token: 0x06008F81 RID: 36737 RVA: 0x00205FEE File Offset: 0x002041EE
		protected override string CssClassFormatString
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17002D64 RID: 11620
		// (get) Token: 0x06008F82 RID: 36738 RVA: 0x00205FF5 File Offset: 0x002041F5
		// (set) Token: 0x06008F83 RID: 36739 RVA: 0x00206008 File Offset: 0x00204208
		[TemplateInstance(TemplateInstance.Single)]
		[Browsable(false)]
		[Bindable(false)]
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

		// Token: 0x17002D65 RID: 11621
		// (get) Token: 0x06008F84 RID: 36740 RVA: 0x0020601C File Offset: 0x0020421C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets the context title menu")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadNotificationContextMenu NotificationMenu
		{
			get
			{
				this.EnsureChildControls();
				return this._contextMenu;
			}
		}

		// Token: 0x17002D66 RID: 11622
		// (get) Token: 0x06008F85 RID: 36741 RVA: 0x0020602A File Offset: 0x0020422A
		[Browsable(false)]
		public SingleTemplateContainer ContentContainer
		{
			get
			{
				this.EnsureChildControls();
				return this._contentContainer;
			}
		}

		// Token: 0x17002D67 RID: 11623
		// (get) Token: 0x06008F86 RID: 36742 RVA: 0x00206038 File Offset: 0x00204238
		// (set) Token: 0x06008F87 RID: 36743 RVA: 0x0020604B File Offset: 0x0020424B
		[DefaultValue("")]
		[Description("Specifies the web method name in the web service used to populate content.")]
		public string WebMethodName
		{
			get
			{
				this.EnsureChildControls();
				return this._xmlPanel.WebMethodName;
			}
			set
			{
				this.EnsureChildControls();
				this._xmlPanel.WebMethodName = value;
			}
		}

		// Token: 0x17002D68 RID: 11624
		// (get) Token: 0x06008F88 RID: 36744 RVA: 0x0020605F File Offset: 0x0020425F
		// (set) Token: 0x06008F89 RID: 36745 RVA: 0x00206072 File Offset: 0x00204272
		[UrlProperty]
		[DefaultValue("")]
		[Description("Specifies the path to the web service used to populate content.")]
		public string WebMethodPath
		{
			get
			{
				this.EnsureChildControls();
				return this._xmlPanel.WebMethodPath;
			}
			set
			{
				this.EnsureChildControls();
				this._xmlPanel.WebMethodPath = value;
			}
		}

		// Token: 0x17002D69 RID: 11625
		// (get) Token: 0x06008F8A RID: 36746 RVA: 0x00206086 File Offset: 0x00204286
		// (set) Token: 0x06008F8B RID: 36747 RVA: 0x00206099 File Offset: 0x00204299
		[Description("Specifies the request method for WCF Service used to populate content GET, POST, PUT, DELETE.")]
		[DefaultValue(XmlHttpPanelWcfRequestMethod.GET)]
		public XmlHttpPanelWcfRequestMethod WcfRequestMethod
		{
			get
			{
				this.EnsureChildControls();
				return this._xmlPanel.WcfRequestMethod;
			}
			set
			{
				this.EnsureChildControls();
				this._xmlPanel.WcfRequestMethod = value;
			}
		}

		// Token: 0x17002D6A RID: 11626
		// (get) Token: 0x06008F8C RID: 36748 RVA: 0x002060AD File Offset: 0x002042AD
		// (set) Token: 0x06008F8D RID: 36749 RVA: 0x002060C0 File Offset: 0x002042C0
		[DefaultValue("")]
		[Description("Specifies the virtual path of the WCF Service used to populate content")]
		[UrlProperty]
		public string WcfServicePath
		{
			get
			{
				this.EnsureChildControls();
				return this._xmlPanel.WcfServicePath;
			}
			set
			{
				this.EnsureChildControls();
				this._xmlPanel.WcfServicePath = value;
			}
		}

		// Token: 0x17002D6B RID: 11627
		// (get) Token: 0x06008F8E RID: 36750 RVA: 0x002060D4 File Offset: 0x002042D4
		// (set) Token: 0x06008F8F RID: 36751 RVA: 0x002060E7 File Offset: 0x002042E7
		[DefaultValue("")]
		[Description("Specifies he WCF Service method used to populate content.")]
		public string WcfServiceMethod
		{
			get
			{
				this.EnsureChildControls();
				return this._xmlPanel.WcfServiceMethod;
			}
			set
			{
				this.EnsureChildControls();
				this._xmlPanel.WcfServiceMethod = value;
			}
		}

		// Token: 0x17002D6C RID: 11628
		// (get) Token: 0x06008F90 RID: 36752 RVA: 0x002060FB File Offset: 0x002042FB
		// (set) Token: 0x06008F91 RID: 36753 RVA: 0x0020611C File Offset: 0x0020431C
		[Category("Behavior")]
		[DefaultValue(NotificationLoad.PageLoad)]
		[ClientControlProperty]
		[Description("Specifies when the content should be loaded.")]
		public NotificationLoad LoadContentOn
		{
			get
			{
				return (NotificationLoad)(this.ViewState["LoadContentOn"] ?? NotificationLoad.PageLoad);
			}
			set
			{
				this.ViewState["LoadContentOn"] = value;
			}
		}

		// Token: 0x17002D6D RID: 11629
		// (get) Token: 0x06008F92 RID: 36754 RVA: 0x00206134 File Offset: 0x00204334
		// (set) Token: 0x06008F93 RID: 36755 RVA: 0x00206154 File Offset: 0x00204354
		[Category("Behavior")]
		[Description("Specifies the URL of the HTTPHandler that serves the notification sound.")]
		[DefaultValue("~/Telerik.Web.UI.WebResource.axd")]
		public string AudioHandlerUrl
		{
			get
			{
				return ((string)this.ViewState["AudioHandlerUrl"]) ?? "~/Telerik.Web.UI.WebResource.axd";
			}
			set
			{
				if (!VirtualPathUtility.IsAppRelative(value))
				{
					throw WebResource.GetHttpHandlerUrlNotAppRelative();
				}
				this.ViewState["AudioHandlerUrl"] = value;
			}
		}

		// Token: 0x17002D6E RID: 11630
		// (get) Token: 0x06008F94 RID: 36756 RVA: 0x00206175 File Offset: 0x00204375
		// (set) Token: 0x06008F95 RID: 36757 RVA: 0x00206196 File Offset: 0x00204396
		[ClientControlProperty]
		[DefaultValue(0)]
		[Description("Specifies the interval after which the notification will automatically show.")]
		[Category("Behavior")]
		public int ShowInterval
		{
			get
			{
				return (int)(this.ViewState["ShowInterval"] ?? 0);
			}
			set
			{
				this.ViewState["ShowInterval"] = value;
			}
		}

		// Token: 0x17002D6F RID: 11631
		// (get) Token: 0x06008F96 RID: 36758 RVA: 0x002061AE File Offset: 0x002043AE
		// (set) Token: 0x06008F97 RID: 36759 RVA: 0x002061CF File Offset: 0x002043CF
		[Description("Specifies the interval after which the notification will automatically update the content.")]
		[ClientControlProperty]
		[DefaultValue(0)]
		[Category("Behavior")]
		public int UpdateInterval
		{
			get
			{
				return (int)(this.ViewState["UpdateInterval"] ?? 0);
			}
			set
			{
				this.ViewState["UpdateInterval"] = value;
			}
		}

		// Token: 0x17002D70 RID: 11632
		// (get) Token: 0x06008F98 RID: 36760 RVA: 0x002061E7 File Offset: 0x002043E7
		// (set) Token: 0x06008F99 RID: 36761 RVA: 0x0020620C File Offset: 0x0020440C
		[Description("Specifies the delay after which the notification will hide if not explicitly closed.")]
		[DefaultValue(3000)]
		[Category("Behavior")]
		[ClientControlProperty]
		public int AutoCloseDelay
		{
			get
			{
				return (int)(this.ViewState["AutoCloseDelay"] ?? 3000);
			}
			set
			{
				this.ViewState["AutoCloseDelay"] = value;
			}
		}

		// Token: 0x17002D71 RID: 11633
		// (get) Token: 0x06008F9A RID: 36762 RVA: 0x00206224 File Offset: 0x00204424
		// (set) Token: 0x06008F9B RID: 36763 RVA: 0x00206245 File Offset: 0x00204445
		[ClientControlProperty]
		[Bindable(true)]
		[Description("Specifies whether the notification has a visible titlebar.")]
		[DefaultValue(true)]
		[Browsable(true)]
		[Category("Behavior")]
		public bool VisibleTitlebar
		{
			get
			{
				return (bool)(this.ViewState["VisibleTitlebar"] ?? true);
			}
			set
			{
				this.ViewState["VisibleTitlebar"] = value;
			}
		}

		// Token: 0x17002D72 RID: 11634
		// (get) Token: 0x06008F9C RID: 36764 RVA: 0x0020625D File Offset: 0x0020445D
		// (set) Token: 0x06008F9D RID: 36765 RVA: 0x0020627D File Offset: 0x0020447D
		[Description("Gets or sets the title icon")]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[UrlProperty]
		[DefaultValue("info")]
		[Bindable(true)]
		[Browsable(true)]
		[Category("Behavior")]
		public string TitleIcon
		{
			get
			{
				return (string)(this.ViewState["TitleIcon"] ?? "info");
			}
			set
			{
				this.ViewState["TitleIcon"] = value;
			}
		}

		// Token: 0x17002D73 RID: 11635
		// (get) Token: 0x06008F9E RID: 36766 RVA: 0x00206290 File Offset: 0x00204490
		// (set) Token: 0x06008F9F RID: 36767 RVA: 0x002062B0 File Offset: 0x002044B0
		[Browsable(true)]
		[Category("Behavior")]
		[Description("Gets or sets the content icon")]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Bindable(true)]
		[DefaultValue("info")]
		[UrlProperty]
		public string ContentIcon
		{
			get
			{
				return (string)(this.ViewState["ContentIcon"] ?? "info");
			}
			set
			{
				this.ViewState["ContentIcon"] = value;
			}
		}

		// Token: 0x17002D74 RID: 11636
		// (get) Token: 0x06008FA0 RID: 36768 RVA: 0x002062C3 File Offset: 0x002044C3
		// (set) Token: 0x06008FA1 RID: 36769 RVA: 0x002062E3 File Offset: 0x002044E3
		[Bindable(true)]
		[Description("Gets or sets the sound to be played on show")]
		[UrlProperty]
		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue("none")]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		public string ShowSound
		{
			get
			{
				return (string)(this.ViewState["ShowSound"] ?? "none");
			}
			set
			{
				this.ViewState["ShowSound"] = value;
			}
		}

		// Token: 0x17002D75 RID: 11637
		// (get) Token: 0x06008FA2 RID: 36770 RVA: 0x002062F6 File Offset: 0x002044F6
		// (set) Token: 0x06008FA3 RID: 36771 RVA: 0x00206317 File Offset: 0x00204517
		[ClientControlProperty]
		[Description("Gets or sets whether the close [X] button should be visible")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(true)]
		public bool ShowCloseButton
		{
			get
			{
				return (bool)(this.ViewState["ShowCloseButton"] ?? true);
			}
			set
			{
				this.ViewState["ShowCloseButton"] = value;
			}
		}

		// Token: 0x17002D76 RID: 11638
		// (get) Token: 0x06008FA4 RID: 36772 RVA: 0x0020632F File Offset: 0x0020452F
		// (set) Token: 0x06008FA5 RID: 36773 RVA: 0x0020634F File Offset: 0x0020454F
		[Browsable(true)]
		[Description("Gets or sets whether the tooltip for the close button")]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue("Close")]
		public string CloseButtonToolTip
		{
			get
			{
				return (string)(this.ViewState["CloseButtonToolTip"] ?? "Close");
			}
			set
			{
				this.ViewState["CloseButtonToolTip"] = value;
			}
		}

		// Token: 0x17002D77 RID: 11639
		// (get) Token: 0x06008FA6 RID: 36774 RVA: 0x00206362 File Offset: 0x00204562
		// (set) Token: 0x06008FA7 RID: 36775 RVA: 0x00206383 File Offset: 0x00204583
		[Bindable(true)]
		[ClientControlProperty]
		[Browsable(true)]
		[Description("Gets or sets whether the icon for the title menu should be visible")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool ShowTitleMenu
		{
			get
			{
				return (bool)(this.ViewState["ShowTitleMenu"] ?? false);
			}
			set
			{
				this.ViewState["ShowTitleMenu"] = value;
			}
		}

		// Token: 0x17002D78 RID: 11640
		// (get) Token: 0x06008FA8 RID: 36776 RVA: 0x0020639B File Offset: 0x0020459B
		// (set) Token: 0x06008FA9 RID: 36777 RVA: 0x002063BB File Offset: 0x002045BB
		[Browsable(true)]
		[Description("Gets or sets  the content of the the tooltip for the title menu button")]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue("Menu")]
		public string TitleMenuToolTip
		{
			get
			{
				return (string)(this.ViewState["TitleMenuToolTip"] ?? "Menu");
			}
			set
			{
				this.ViewState["TitleMenuToolTip"] = value;
			}
		}

		// Token: 0x17002D79 RID: 11641
		// (get) Token: 0x06008FAA RID: 36778 RVA: 0x002063CE File Offset: 0x002045CE
		// (set) Token: 0x06008FAB RID: 36779 RVA: 0x002063F0 File Offset: 0x002045F0
		[DefaultValue(NotificationPosition.BottomRight)]
		[ClientControlProperty]
		[Category("Layout")]
		[Description("Get/Set the top and left position of the notification relative to the browser")]
		public NotificationPosition Position
		{
			get
			{
				return (NotificationPosition)(this.ViewState["Position"] ?? NotificationPosition.BottomRight);
			}
			set
			{
				this.ViewState["Position"] = value;
			}
		}

		// Token: 0x17002D7A RID: 11642
		// (get) Token: 0x06008FAC RID: 36780 RVA: 0x00206408 File Offset: 0x00204608
		// (set) Token: 0x06008FAD RID: 36781 RVA: 0x00206429 File Offset: 0x00204629
		[Category("Behavior")]
		[DefaultValue(NotificationAnimation.None)]
		[ClientControlProperty]
		[Description("Get/Set the animation effect of the notification")]
		public NotificationAnimation Animation
		{
			get
			{
				return (NotificationAnimation)(this.ViewState["Animation"] ?? NotificationAnimation.None);
			}
			set
			{
				this.ViewState["Animation"] = value;
			}
		}

		// Token: 0x17002D7B RID: 11643
		// (get) Token: 0x06008FAE RID: 36782 RVA: 0x00206441 File Offset: 0x00204641
		// (set) Token: 0x06008FAF RID: 36783 RVA: 0x00206466 File Offset: 0x00204666
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Sets/gets the duration of the animation in milliseconds.")]
		[DefaultValue(500)]
		public int AnimationDuration
		{
			get
			{
				return (int)(this.ViewState["AnimationDuration"] ?? 500);
			}
			set
			{
				this.ViewState["AnimationDuration"] = value;
			}
		}

		// Token: 0x17002D7C RID: 11644
		// (get) Token: 0x06008FB0 RID: 36784 RVA: 0x0020647E File Offset: 0x0020467E
		// (set) Token: 0x06008FB1 RID: 36785 RVA: 0x0020649F File Offset: 0x0020469F
		[Description("Get/Set the notification's horizontal offset. Works in cooperation with the Position property.")]
		[ClientControlProperty]
		[DefaultValue(0)]
		[Category("Behavior")]
		public int OffsetX
		{
			get
			{
				return (int)(this.ViewState["OffsetX"] ?? 0);
			}
			set
			{
				this.ViewState["OffsetX"] = value;
			}
		}

		// Token: 0x17002D7D RID: 11645
		// (get) Token: 0x06008FB2 RID: 36786 RVA: 0x002064B7 File Offset: 0x002046B7
		// (set) Token: 0x06008FB3 RID: 36787 RVA: 0x002064D8 File Offset: 0x002046D8
		[ClientControlProperty]
		[DefaultValue(0)]
		[Category("Behavior")]
		[Description("Get/Set the notification's vertical offset. Works in cooperation with the Position property.")]
		public int OffsetY
		{
			get
			{
				return (int)(this.ViewState["OffsetY"] ?? 0);
			}
			set
			{
				this.ViewState["OffsetY"] = value;
			}
		}

		// Token: 0x17002D7E RID: 11646
		// (get) Token: 0x06008FB4 RID: 36788 RVA: 0x002064F0 File Offset: 0x002046F0
		// (set) Token: 0x06008FB5 RID: 36789 RVA: 0x00206511 File Offset: 0x00204711
		[Browsable(true)]
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Specifies whether the notification will open automatically when the aspx page is loaded on the client.")]
		[Bindable(true)]
		[DefaultValue(false)]
		public bool VisibleOnPageLoad
		{
			get
			{
				return (bool)(this.ViewState["VisibleOnPageLoad"] ?? false);
			}
			set
			{
				this.ViewState["VisibleOnPageLoad"] = value;
			}
		}

		// Token: 0x17002D7F RID: 11647
		// (get) Token: 0x06008FB6 RID: 36790 RVA: 0x00206529 File Offset: 0x00204729
		// (set) Token: 0x06008FB7 RID: 36791 RVA: 0x0020654A File Offset: 0x0020474A
		[ClientControlProperty]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Specifies whether the notification will create an overlay element to ensure it will be displayed over a heavy weight element.")]
		public bool Overlay
		{
			get
			{
				return (bool)(this.ViewState["Overlay"] ?? false);
			}
			set
			{
				this.ViewState["Overlay"] = value;
			}
		}

		// Token: 0x17002D80 RID: 11648
		// (get) Token: 0x06008FB8 RID: 36792 RVA: 0x00206562 File Offset: 0x00204762
		// (set) Token: 0x06008FB9 RID: 36793 RVA: 0x00206583 File Offset: 0x00204783
		[Bindable(true)]
		[Browsable(true)]
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Gets or sets a value indicating whether the notification is pinned (when true it does not scroll with the page)")]
		public bool Pinned
		{
			get
			{
				return (bool)(this.ViewState["Pinned"] ?? true);
			}
			set
			{
				this.ViewState["Pinned"] = value;
			}
		}

		// Token: 0x17002D81 RID: 11649
		// (get) Token: 0x06008FBA RID: 36794 RVA: 0x0020659B File Offset: 0x0020479B
		// (set) Token: 0x06008FBB RID: 36795 RVA: 0x002065C0 File Offset: 0x002047C0
		[ClientControlProperty]
		[DefaultValue(typeof(Unit), "")]
		[TypeConverter(typeof(UnitConverter))]
		[Category("Behavior")]
		[Description("Get/Set the Width of the notification in pixels")]
		public override Unit Width
		{
			get
			{
				return (Unit)(this.ViewState["Width"] ?? Unit.Empty);
			}
			set
			{
				this.ViewState["Width"] = value;
			}
		}

		// Token: 0x17002D82 RID: 11650
		// (get) Token: 0x06008FBC RID: 36796 RVA: 0x002065D8 File Offset: 0x002047D8
		// (set) Token: 0x06008FBD RID: 36797 RVA: 0x002065FD File Offset: 0x002047FD
		[Category("Behavior")]
		[DefaultValue(typeof(Unit), "")]
		[TypeConverter(typeof(UnitConverter))]
		[ClientControlProperty]
		[Description("Get/Set the Height of the notification in pixels")]
		public override Unit Height
		{
			get
			{
				return (Unit)(this.ViewState["Height"] ?? Unit.Empty);
			}
			set
			{
				this.ViewState["Height"] = value;
			}
		}

		// Token: 0x17002D83 RID: 11651
		// (get) Token: 0x06008FBE RID: 36798 RVA: 0x00206615 File Offset: 0x00204815
		// (set) Token: 0x06008FBF RID: 36799 RVA: 0x00206635 File Offset: 0x00204835
		[Description("Get/Set the Text that will appear in the notification (if there is no ContentTemplate used).")]
		[DefaultValue("")]
		[Category("Behavior")]
		[ClientControlProperty]
		public string Text
		{
			get
			{
				return (string)(this.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17002D84 RID: 11652
		// (get) Token: 0x06008FC0 RID: 36800 RVA: 0x00206648 File Offset: 0x00204848
		// (set) Token: 0x06008FC1 RID: 36801 RVA: 0x00206668 File Offset: 0x00204868
		[Category("Behavior")]
		[DefaultValue("")]
		[ClientControlProperty]
		[Description("Get/Set the Title that will appear in the notification titlebar")]
		public string Title
		{
			get
			{
				return (string)(this.ViewState["Title"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x17002D85 RID: 11653
		// (get) Token: 0x06008FC2 RID: 36802 RVA: 0x0020667B File Offset: 0x0020487B
		// (set) Token: 0x06008FC3 RID: 36803 RVA: 0x0020669B File Offset: 0x0020489B
		[ClientControlProperty]
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Get/Set the an optional Value to pass.")]
		public string Value
		{
			get
			{
				return (string)(this.ViewState["Value"] ?? string.Empty);
			}
			set
			{
				this.ViewState["Value"] = value;
				this.EnsureChildControls();
				this._hiddenState.Value = value;
			}
		}

		// Token: 0x17002D86 RID: 11654
		// (get) Token: 0x06008FC4 RID: 36804 RVA: 0x002066C0 File Offset: 0x002048C0
		// (set) Token: 0x06008FC5 RID: 36805 RVA: 0x002066E1 File Offset: 0x002048E1
		[Description("Gets or sets a value indicating whether the notification should stay on the screen when hovered (autoclose is delayed until the mouse goes outside).")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(true)]
		[ClientControlProperty]
		public bool KeepOnMouseOver
		{
			get
			{
				return (bool)(this.ViewState["KeepOnMouseOver"] ?? true);
			}
			set
			{
				this.ViewState["KeepOnMouseOver"] = value;
			}
		}

		// Token: 0x17002D87 RID: 11655
		// (get) Token: 0x06008FC6 RID: 36806 RVA: 0x002066F9 File Offset: 0x002048F9
		// (set) Token: 0x06008FC7 RID: 36807 RVA: 0x0020671A File Offset: 0x0020491A
		[ClientPropertyName("enableAriaSupport")]
		[ClientControlProperty]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("When set to true enables support for WAI-ARIA")]
		public bool EnableAriaSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableAriaSupport"] ?? false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x17002D88 RID: 11656
		// (get) Token: 0x06008FC8 RID: 36808 RVA: 0x00206732 File Offset: 0x00204932
		// (set) Token: 0x06008FC9 RID: 36809 RVA: 0x00206753 File Offset: 0x00204953
		[Browsable(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Gets or sets a value indicating whether the notification is enabled")]
		public override bool Enabled
		{
			get
			{
				return (bool)(this.ViewState["Enabled"] ?? true);
			}
			set
			{
				this.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x17002D89 RID: 11657
		// (get) Token: 0x06008FCA RID: 36810 RVA: 0x0020676B File Offset: 0x0020496B
		// (set) Token: 0x06008FCB RID: 36811 RVA: 0x0020678C File Offset: 0x0020498C
		[DefaultValue(false)]
		[Bindable(true)]
		[Category("Behavior")]
		[Browsable(true)]
		[Description("Gets or sets a value indicating whether the notification should have rounded corners")]
		public bool EnableRoundedCorners
		{
			get
			{
				return (bool)(this.ViewState["EnableRoundedCorners"] ?? false);
			}
			set
			{
				this.ViewState["EnableRoundedCorners"] = value;
			}
		}

		// Token: 0x17002D8A RID: 11658
		// (get) Token: 0x06008FCC RID: 36812 RVA: 0x002067A4 File Offset: 0x002049A4
		// (set) Token: 0x06008FCD RID: 36813 RVA: 0x002067C5 File Offset: 0x002049C5
		[Browsable(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Gets or sets a value indicating whether the notification should have shadow")]
		public bool EnableShadow
		{
			get
			{
				return (bool)(this.ViewState["EnableShadow"] ?? false);
			}
			set
			{
				this.ViewState["EnableShadow"] = value;
			}
		}

		// Token: 0x17002D8B RID: 11659
		// (get) Token: 0x06008FCE RID: 36814 RVA: 0x002067DD File Offset: 0x002049DD
		// (set) Token: 0x06008FCF RID: 36815 RVA: 0x002067FE File Offset: 0x002049FE
		[DefaultValue(NotificationScrolling.Default)]
		[ClientControlProperty]
		[Category("Layout")]
		public NotificationScrolling ContentScrolling
		{
			get
			{
				return (NotificationScrolling)(this.ViewState["ContentScrolling"] ?? NotificationScrolling.Default);
			}
			set
			{
				this.ViewState["ContentScrolling"] = value;
			}
		}

		// Token: 0x17002D8C RID: 11660
		// (get) Token: 0x06008FD0 RID: 36816 RVA: 0x00206816 File Offset: 0x00204A16
		// (set) Token: 0x06008FD1 RID: 36817 RVA: 0x00206838 File Offset: 0x00204A38
		[Description("Specifies what should be the notification opacity.")]
		[Category("Appearance")]
		[DefaultValue(100)]
		[ClientControlProperty]
		public int Opacity
		{
			get
			{
				return (int)(this.ViewState["Opacity"] ?? 100);
			}
			set
			{
				if (value < 0 || value > 100)
				{
					throw new ArgumentOutOfRangeException("Opacity", "The Opacity value should be between 0 and 100");
				}
				this.ViewState["Opacity"] = value;
			}
		}

		// Token: 0x17002D8D RID: 11661
		// (get) Token: 0x06008FD2 RID: 36818 RVA: 0x00206876 File Offset: 0x00204A76
		// (set) Token: 0x06008FD3 RID: 36819 RVA: 0x00206896 File Offset: 0x00204A96
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("showing")]
		[Description("Specifies the name of the client-side event handler that is called before the RadNotification shows")]
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

		// Token: 0x17002D8E RID: 11662
		// (get) Token: 0x06008FD4 RID: 36820 RVA: 0x002068A9 File Offset: 0x00204AA9
		// (set) Token: 0x06008FD5 RID: 36821 RVA: 0x002068C9 File Offset: 0x00204AC9
		[Description("Specifies the name of the client-side event handler that is called just after the RadNotification is shown")]
		[ClientPropertyName("shown")]
		[DefaultValue("")]
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

		// Token: 0x17002D8F RID: 11663
		// (get) Token: 0x06008FD6 RID: 36822 RVA: 0x002068DC File Offset: 0x00204ADC
		// (set) Token: 0x06008FD7 RID: 36823 RVA: 0x002068FC File Offset: 0x00204AFC
		[DefaultValue("")]
		[Description("The name of the javascript function called when the notification is to be hidden.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("hiding")]
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

		// Token: 0x17002D90 RID: 11664
		// (get) Token: 0x06008FD8 RID: 36824 RVA: 0x0020690F File Offset: 0x00204B0F
		// (set) Token: 0x06008FD9 RID: 36825 RVA: 0x0020692F File Offset: 0x00204B2F
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("hidden")]
		[Category("Client-side events")]
		[Description("The name of the javascript function called when the notification is hidden.")]
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

		// Token: 0x17002D91 RID: 11665
		// (get) Token: 0x06008FDA RID: 36826 RVA: 0x00206942 File Offset: 0x00204B42
		// (set) Token: 0x06008FDB RID: 36827 RVA: 0x00206962 File Offset: 0x00204B62
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("The name of the javascript function called when the content of the notification is to be updated.")]
		[ClientPropertyName("updating")]
		[Category("Client-side events")]
		public virtual string OnClientUpdating
		{
			get
			{
				return (string)(this.ViewState["OnClientUpdating"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientUpdating"] = value;
			}
		}

		// Token: 0x17002D92 RID: 11666
		// (get) Token: 0x06008FDC RID: 36828 RVA: 0x00206975 File Offset: 0x00204B75
		// (set) Token: 0x06008FDD RID: 36829 RVA: 0x00206995 File Offset: 0x00204B95
		[Description("The name of the javascript function called when the content of the notification is updated.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("updated")]
		[Category("Client-side events")]
		public string OnClientUpdated
		{
			get
			{
				return (string)(this.ViewState["OnClientUpdated"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientUpdated"] = value;
			}
		}

		// Token: 0x17002D93 RID: 11667
		// (get) Token: 0x06008FDE RID: 36830 RVA: 0x002069A8 File Offset: 0x00204BA8
		// (set) Token: 0x06008FDF RID: 36831 RVA: 0x002069C8 File Offset: 0x00204BC8
		[ClientControlEvent]
		[ClientPropertyName("updateError")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Specifies the name of the client-side event handler that is called when the call to the WebService or the callback is interrupted by an error")]
		public virtual string OnClientUpdateError
		{
			get
			{
				return ((string)this.ViewState["OnClientUpdateError"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientUpdateError"] = value;
			}
		}

		// Token: 0x1400015C RID: 348
		// (add) Token: 0x06008FE0 RID: 36832 RVA: 0x002069DB File Offset: 0x00204BDB
		// (remove) Token: 0x06008FE1 RID: 36833 RVA: 0x002069EE File Offset: 0x00204BEE
		public virtual event RadNotificationEventHandler CallbackUpdate
		{
			add
			{
				base.Events.AddHandler(RadNotification.CallbackUpdateEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadNotification.CallbackUpdateEvent, value);
			}
		}

		// Token: 0x06008FE2 RID: 36834 RVA: 0x00206A04 File Offset: 0x00204C04
		[Category("Action")]
		protected virtual void OnCallbackUpdate(RadNotificationEventArgs e)
		{
			RadNotificationEventHandler radNotificationEventHandler = (RadNotificationEventHandler)base.Events[RadNotification.CallbackUpdateEvent];
			if (radNotificationEventHandler != null)
			{
				radNotificationEventHandler(this, e);
			}
		}

		// Token: 0x06008FE3 RID: 36835 RVA: 0x00206A34 File Offset: 0x00204C34
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<NotificationAnimation>(descriptor, "animation", this.Animation, NotificationAnimation.None);
			base.DescribeProperty<int>(descriptor, "animationDuration", this.AnimationDuration, 500);
			base.DescribeProperty<int>(descriptor, "autoCloseDelay", this.AutoCloseDelay, 3000);
			base.DescribeProperty<NotificationScrolling>(descriptor, "contentScrolling", this.ContentScrolling, NotificationScrolling.Default);
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeProperty<string>(descriptor, "height", this.Height.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<bool>(descriptor, "keepOnMouseOver", this.KeepOnMouseOver, true);
			base.DescribeProperty<NotificationLoad>(descriptor, "loadContentOn", this.LoadContentOn, NotificationLoad.PageLoad);
			base.DescribeProperty<int>(descriptor, "offsetX", this.OffsetX, 0);
			base.DescribeProperty<int>(descriptor, "offsetY", this.OffsetY, 0);
			base.DescribeProperty<int>(descriptor, "opacity", this.Opacity, 100);
			base.DescribeProperty<bool>(descriptor, "overlay", this.Overlay, false);
			base.DescribeProperty<bool>(descriptor, "pinned", this.Pinned, true);
			base.DescribeProperty<NotificationPosition>(descriptor, "position", this.Position, NotificationPosition.BottomRight);
			base.DescribeProperty<bool>(descriptor, "showCloseButton", this.ShowCloseButton, true);
			base.DescribeProperty<int>(descriptor, "showInterval", this.ShowInterval, 0);
			base.DescribeProperty<bool>(descriptor, "showTitleMenu", this.ShowTitleMenu, false);
			base.DescribeProperty<string>(descriptor, "text", this.Text, "");
			base.DescribeProperty<string>(descriptor, "title", this.Title, "");
			base.DescribeProperty<int>(descriptor, "updateInterval", this.UpdateInterval, 0);
			base.DescribeProperty<string>(descriptor, "value", this.Value, "");
			base.DescribeProperty<bool>(descriptor, "visibleOnPageLoad", this.VisibleOnPageLoad, false);
			base.DescribeProperty<bool>(descriptor, "visibleTitlebar", this.VisibleTitlebar, true);
			base.DescribeProperty<string>(descriptor, "width", this.Width.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06008FE4 RID: 36836 RVA: 0x00206C48 File Offset: 0x00204E48
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "hidden", this.OnClientHidden);
			RadWebControl.DescribeEvent(descriptor, "hiding", this.OnClientHiding);
			RadWebControl.DescribeEvent(descriptor, "showing", this.OnClientShowing);
			RadWebControl.DescribeEvent(descriptor, "shown", this.OnClientShown);
			RadWebControl.DescribeEvent(descriptor, "updated", this.OnClientUpdated);
			RadWebControl.DescribeEvent(descriptor, "updateError", this.OnClientUpdateError);
			RadWebControl.DescribeEvent(descriptor, "updating", this.OnClientUpdating);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06008FE8 RID: 36840 RVA: 0x00206CD3 File Offset: 0x00204ED3
		// Note: this type is marked as 'beforefieldinit'.
		static RadNotification()
		{
			RadNotification.CallbackUpdateEvent = new object();
			RadNotification.HandlerNotificationAudio = "nah";
		}

		// Token: 0x04002804 RID: 10244
		internal const string HandlerUrl = "~/Telerik.Web.UI.WebResource.axd";

		// Token: 0x04002805 RID: 10245
		private SingleTemplateContainer _contentContainer;

		// Token: 0x04002806 RID: 10246
		private RadNotificationContextMenu _contextMenu;

		// Token: 0x04002807 RID: 10247
		private RadXmlHttpPanel _xmlPanel;

		// Token: 0x04002808 RID: 10248
		private HtmlGenericControl _contentDiv;

		// Token: 0x04002809 RID: 10249
		private HiddenField _hiddenState;

		// Token: 0x0400280A RID: 10250
		private AudioFormat audioFormat;

		// Token: 0x0400280C RID: 10252
		internal static string HandlerNotificationAudio;
	}
}

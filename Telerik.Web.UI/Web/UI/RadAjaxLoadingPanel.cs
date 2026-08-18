using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02000FDC RID: 4060
	[ClientScriptResource("Telerik.Web.UI.RadAjaxLoadingPanel", "Telerik.Web.UI.Common.Navigation.OverlayScript.js")]
	[EmbeddedSkin("Ajax", "Default")]
	[EmbeddedSkin("Ajax")]
	[RequiredScript(typeof(Core))]
	[RequiredScript(typeof(jQuery))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[ClientScriptResource("Telerik.Web.UI.RadAjaxLoadingPanel", "Telerik.Web.UI.Ajax.Ajax.js")]
	[Designer("Telerik.Web.Design.RadAjaxLoadingPanelDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxData("<{0}:RadAjaxLoadingPanel Runat=\"server\" Skin=\"Default\"></{0}:RadAjaxLoadingPanel>")]
	[ToolboxBitmap(typeof(RadAjaxLoadingPanel), "Telerik.Web.UI.Ajax.png")]
	[TelerikToolboxCategory("Miscellaneous")]
	public class RadAjaxLoadingPanel : Panel, IScriptControl, IControlResolver, ISkinnableControl, IControl
	{
		// Token: 0x06009DA7 RID: 40359 RVA: 0x00232A29 File Offset: 0x00230C29
		public RadAjaxLoadingPanel()
		{
			this.EnsureLicensing();
		}

		// Token: 0x06009DA8 RID: 40360 RVA: 0x00232A38 File Offset: 0x00230C38
		private void EnsureLicensing()
		{
			if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
			{
				try
				{
					LicenseManager.Validate(base.GetType());
				}
				catch
				{
				}
			}
		}

		// Token: 0x170031D1 RID: 12753
		// (get) Token: 0x06009DA9 RID: 40361 RVA: 0x00232A70 File Offset: 0x00230C70
		// (set) Token: 0x06009DAA RID: 40362 RVA: 0x00232A9B File Offset: 0x00230C9B
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Whether to register with the ScriptManager control on the page")]
		public virtual bool RegisterWithScriptManager
		{
			get
			{
				return this.ViewState["RegisterWithScriptManager"] == null || (bool)this.ViewState["RegisterWithScriptManager"];
			}
			set
			{
				this.ViewState["RegisterWithScriptManager"] = value;
			}
		}

		// Token: 0x170031D2 RID: 12754
		// (get) Token: 0x06009DAB RID: 40363 RVA: 0x00232AB3 File Offset: 0x00230CB3
		// (set) Token: 0x06009DAC RID: 40364 RVA: 0x00232AD4 File Offset: 0x00230CD4
		[Description("Specifies the rendering mode of the control")]
		[NotifyParentProperty(true)]
		[DefaultValue(RenderMode.Classic)]
		[Category("Appearance")]
		[ClientControlProperty]
		public RenderMode RenderMode
		{
			get
			{
				return (RenderMode)(this.ViewState["RenderingMode"] ?? RenderMode.Classic);
			}
			set
			{
				this.ViewState["RenderingMode"] = value;
			}
		}

		// Token: 0x170031D3 RID: 12755
		// (get) Token: 0x06009DAD RID: 40365 RVA: 0x00232AEC File Offset: 0x00230CEC
		[Description("Returns resolved RenderMode should the original value was Auto")]
		public virtual RenderMode ResolvedRenderMode
		{
			get
			{
				RenderMode renderMode = this.RenderMode;
				if (renderMode == RenderMode.Auto)
				{
					renderMode = this.PreferredRenderMode(RenderModeBrowserAdaptor.Instance);
				}
				return renderMode;
			}
		}

		// Token: 0x06009DAE RID: 40366 RVA: 0x00232B10 File Offset: 0x00230D10
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (ScriptManager.GetCurrent(this.Page) == null && this.RegisterWithScriptManager)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The control with ID '{0}' requires a ScriptManager on the page. The ScriptManager must appear before any controls that need it.", new object[]
				{
					this.ID
				}));
			}
		}

		// Token: 0x170031D4 RID: 12756
		// (get) Token: 0x06009DAF RID: 40367 RVA: 0x00232B5F File Offset: 0x00230D5F
		// (set) Token: 0x06009DB0 RID: 40368 RVA: 0x00232B8F File Offset: 0x00230D8F
		[Category("Appearance")]
		[Description("Whether to register the scripts automatically")]
		[DefaultValue(true)]
		public virtual bool EnableEmbeddedScripts
		{
			get
			{
				if (this.ViewState["EnableEmbeddedScripts"] == null)
				{
					return BaseClass.GetGlobalEnableEmbeddedScripts(this);
				}
				return (bool)this.ViewState["EnableEmbeddedScripts"];
			}
			set
			{
				this.ViewState["EnableEmbeddedScripts"] = value;
			}
		}

		// Token: 0x170031D5 RID: 12757
		// (get) Token: 0x06009DB1 RID: 40369 RVA: 0x00232BA7 File Offset: 0x00230DA7
		protected ScriptManager ScriptManager
		{
			get
			{
				if (this._scriptManager == null)
				{
					this._scriptManager = ScriptRegistrar.GetScriptManager(this);
				}
				return this._scriptManager;
			}
		}

		// Token: 0x170031D6 RID: 12758
		// (get) Token: 0x06009DB2 RID: 40370 RVA: 0x00232BC3 File Offset: 0x00230DC3
		internal virtual bool ShouldRegisterCssReferences
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06009DB3 RID: 40371 RVA: 0x00232BC8 File Offset: 0x00230DC8
		protected virtual void RegisterCssReferences()
		{
			RadStyleSheetManager current = RadStyleSheetManager.GetCurrent(this.Page);
			if (this.RegisterWithScriptManager && current == null)
			{
				SkinRegistrar.RegisterCssReferences(this);
				return;
			}
			if (current != null)
			{
				current.RegisterSkinnableControl(this);
			}
		}

		// Token: 0x06009DB4 RID: 40372 RVA: 0x00232BFD File Offset: 0x00230DFD
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.ControlPreRender();
		}

		// Token: 0x06009DB5 RID: 40373 RVA: 0x00232C0C File Offset: 0x00230E0C
		protected virtual void ControlPreRender()
		{
			this._preRenderCalled = true;
			this.ConfigureCombinedScriptFile();
			this.ConfigureCombinedBaseSkinFile();
			if (this.RegisterWithScriptManager)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<RadAjaxLoadingPanel>(this);
			}
			base.EnsureID();
			if (this.ShouldRegisterCssReferences)
			{
				this.RegisterCssReferences();
			}
		}

		// Token: 0x06009DB6 RID: 40374 RVA: 0x00232C59 File Offset: 0x00230E59
		protected void ConfigureCombinedScriptFile()
		{
			if (this.EnableEmbeddedScripts)
			{
				this.EnableEmbeddedScripts = !RadScriptManager.IsCombinedScriptEnabled(this.Page);
			}
		}

		// Token: 0x06009DB7 RID: 40375 RVA: 0x00232C77 File Offset: 0x00230E77
		protected void ConfigureCombinedBaseSkinFile()
		{
			if (this.EnableEmbeddedBaseStylesheet)
			{
				this.EnableEmbeddedBaseStylesheet = !RadStyleSheetManager.IsCombinedBaseSkinEnabled(this.Page);
			}
		}

		// Token: 0x170031D7 RID: 12759
		// (get) Token: 0x06009DB8 RID: 40376 RVA: 0x00232C95 File Offset: 0x00230E95
		protected virtual string CssClassFormatString
		{
			get
			{
				if (string.IsNullOrEmpty(this.RuntimeSkin))
				{
					return "";
				}
				return "RadAjax RadAjax_{0}";
			}
		}

		// Token: 0x06009DB9 RID: 40377 RVA: 0x00232CB0 File Offset: 0x00230EB0
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddStyleAttribute("display", "none");
			string cssClass = this.CssClass;
			this.CssClass = string.Format(this.CssClassFormatString + " " + cssClass, this.RuntimeSkin).Trim();
			if (this.CssClass == " ")
			{
				this.CssClass = string.Empty;
			}
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x06009DBA RID: 40378 RVA: 0x00232D28 File Offset: 0x00230F28
		protected override void Render(HtmlTextWriter writer)
		{
			if (writer is Html32TextWriter)
			{
				writer = new HtmlTextWriter(writer);
			}
			if (!this._preRenderCalled && !this.RegisterWithScriptManager)
			{
				this.ControlPreRender();
			}
			if (!this.RegisterWithScriptManager)
			{
				this.RenderScriptsNoScriptManager(writer);
			}
			base.Render(writer);
			if (!base.DesignMode && this.RegisterWithScriptManager)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
				return;
			}
			if (!this.RegisterWithScriptManager)
			{
				this.RenderDescriptorsNoScriptManager(writer);
			}
		}

		// Token: 0x06009DBB RID: 40379 RVA: 0x00232DA4 File Offset: 0x00230FA4
		protected virtual void RenderScriptsNoScriptManager(HtmlTextWriter writer)
		{
			string controlScripts = ControlRenderer.GetControlScripts(this);
			if (!string.IsNullOrEmpty(controlScripts))
			{
				writer.Write("<input type=\"hidden\"/>");
			}
			writer.WriteLine(controlScripts);
		}

		// Token: 0x06009DBC RID: 40380 RVA: 0x00232DD4 File Offset: 0x00230FD4
		protected virtual void RenderDescriptorsNoScriptManager(HtmlTextWriter writer)
		{
			string controlDescriptors = ControlRenderer.GetControlDescriptors(this);
			writer.WriteLine(controlDescriptors);
		}

		// Token: 0x06009DBD RID: 40381 RVA: 0x00232DF0 File Offset: 0x00230FF0
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.RuntimeSkin))
			{
				string value = string.Format("raDiv{0}", (this.BackgroundPosition != AjaxLoadingPanelBackgroundPosition.Center) ? (" ra" + this.BackgroundPosition.ToString()) : "");
				writer.AddAttribute("class", value);
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			base.RenderContents(writer);
			if (!string.IsNullOrEmpty(this.RuntimeSkin))
			{
				writer.RenderEndTag();
				writer.AddAttribute("class", string.Format("raColor{0}", this.EnableSkinTransparency ? " raTransp" : ""));
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.RenderEndTag();
			}
		}

		// Token: 0x170031D8 RID: 12760
		// (get) Token: 0x06009DBE RID: 40382 RVA: 0x00232EA4 File Offset: 0x002310A4
		// (set) Token: 0x06009DBF RID: 40383 RVA: 0x00232ED2 File Offset: 0x002310D2
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets transparency in percentage. Default value is 0 percents.")]
		public int Transparency
		{
			get
			{
				object obj = this.ViewState["Transparency"];
				if (obj == null)
				{
					obj = 0;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["Transparency"] = value;
			}
		}

		// Token: 0x170031D9 RID: 12761
		// (get) Token: 0x06009DC0 RID: 40384 RVA: 0x00232EEC File Offset: 0x002310EC
		// (set) Token: 0x06009DC1 RID: 40385 RVA: 0x00232F1A File Offset: 0x0023111A
		[Description("Gets or sets transparency of the loading panel without affecting the icon. Default value is 0 percents.")]
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		public int BackgroundTransparency
		{
			get
			{
				object obj = this.ViewState["BackgroundTransparency"];
				if (obj == null)
				{
					obj = 0;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["BackgroundTransparency"] = value;
			}
		}

		// Token: 0x170031DA RID: 12762
		// (get) Token: 0x06009DC2 RID: 40386 RVA: 0x00232F34 File Offset: 0x00231134
		// (set) Token: 0x06009DC3 RID: 40387 RVA: 0x00232F62 File Offset: 0x00231162
		[NotifyParentProperty(true)]
		[Description("Defines whether the transparency set in the skin will be applied. Default value is True.")]
		[DefaultValue(true)]
		public bool EnableSkinTransparency
		{
			get
			{
				object obj = this.ViewState["EnableSkinTransparency"];
				if (obj == null)
				{
					obj = true;
				}
				return (bool)obj;
			}
			set
			{
				this.ViewState["EnableSkinTransparency"] = value;
			}
		}

		// Token: 0x170031DB RID: 12763
		// (get) Token: 0x06009DC4 RID: 40388 RVA: 0x00232F7C File Offset: 0x0023117C
		// (set) Token: 0x06009DC5 RID: 40389 RVA: 0x00232FAE File Offset: 0x002311AE
		[Description("Gets or sets the z-index of the loading panel. Default value is 90,000.")]
		[DefaultValue(90000)]
		[NotifyParentProperty(true)]
		public int ZIndex
		{
			get
			{
				object obj = this.ViewState["ZIndex"];
				if (obj == null)
				{
					obj = 90000;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["ZIndex"] = value;
			}
		}

		// Token: 0x170031DC RID: 12764
		// (get) Token: 0x06009DC6 RID: 40390 RVA: 0x00232FC8 File Offset: 0x002311C8
		// (set) Token: 0x06009DC7 RID: 40391 RVA: 0x00232FF6 File Offset: 0x002311F6
		[DefaultValue(AjaxLoadingPanelBackgroundPosition.Center)]
		[Description("Gets or sets the position of the skin background image.")]
		[NotifyParentProperty(true)]
		public AjaxLoadingPanelBackgroundPosition BackgroundPosition
		{
			get
			{
				object obj = this.ViewState["BackgroundPosition"];
				if (obj == null)
				{
					obj = AjaxLoadingPanelBackgroundPosition.Center;
				}
				return (AjaxLoadingPanelBackgroundPosition)obj;
			}
			set
			{
				this.ViewState["BackgroundPosition"] = value;
			}
		}

		// Token: 0x170031DD RID: 12765
		// (get) Token: 0x06009DC8 RID: 40392 RVA: 0x0023300E File Offset: 0x0023120E
		// (set) Token: 0x06009DC9 RID: 40393 RVA: 0x0023304D File Offset: 0x0023124D
		[Description("Specified whether the loading panel stays sticky.")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public bool IsSticky
		{
			get
			{
				if (this.ViewState["IsSticky"] == null)
				{
					this.ViewState["IsSticky"] = false;
				}
				return (bool)this.ViewState["IsSticky"];
			}
			set
			{
				this.ViewState["IsSticky"] = value;
			}
		}

		// Token: 0x170031DE RID: 12766
		// (get) Token: 0x06009DCA RID: 40394 RVA: 0x00233065 File Offset: 0x00231265
		// (set) Token: 0x06009DCB RID: 40395 RVA: 0x00233090 File Offset: 0x00231290
		[ClientControlProperty]
		[Description("Whether the loading panel will be displayed over the entire page")]
		[DefaultValue(false)]
		[Category("Appearance")]
		public virtual bool Modal
		{
			get
			{
				return this.ViewState["Modal"] != null && (bool)this.ViewState["Modal"];
			}
			set
			{
				this.ViewState["Modal"] = value;
			}
		}

		// Token: 0x170031DF RID: 12767
		// (get) Token: 0x06009DCC RID: 40396 RVA: 0x002330A8 File Offset: 0x002312A8
		// (set) Token: 0x06009DCD RID: 40397 RVA: 0x002330E7 File Offset: 0x002312E7
		[Description("Specifies the intial delay time in milliseconds before the loading panel is shown.")]
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		public int InitialDelayTime
		{
			get
			{
				if (this.ViewState["InitialDelayTime"] == null)
				{
					this.ViewState["InitialDelayTime"] = 0;
				}
				return (int)this.ViewState["InitialDelayTime"];
			}
			set
			{
				this.ViewState["InitialDelayTime"] = value;
			}
		}

		// Token: 0x170031E0 RID: 12768
		// (get) Token: 0x06009DCE RID: 40398 RVA: 0x002330FF File Offset: 0x002312FF
		// (set) Token: 0x06009DCF RID: 40399 RVA: 0x0023313E File Offset: 0x0023133E
		[DefaultValue(0)]
		[Description("Specifies the minimum display time in milliseconds before the loading panel is hidden.")]
		[NotifyParentProperty(true)]
		public int MinDisplayTime
		{
			get
			{
				if (this.ViewState["MinDisplayTime"] == null)
				{
					this.ViewState["MinDisplayTime"] = 0;
				}
				return (int)this.ViewState["MinDisplayTime"];
			}
			set
			{
				this.ViewState["MinDisplayTime"] = value;
			}
		}

		// Token: 0x170031E1 RID: 12769
		// (get) Token: 0x06009DD0 RID: 40400 RVA: 0x00233158 File Offset: 0x00231358
		// (set) Token: 0x06009DD1 RID: 40401 RVA: 0x00233186 File Offset: 0x00231386
		[DefaultValue(0)]
		[Description("Gets or sets animation duration in milliseconds. Default value is 0, i.e. no animation.")]
		[NotifyParentProperty(true)]
		public int AnimationDuration
		{
			get
			{
				object obj = this.ViewState["AnimationDuration"];
				if (obj == null)
				{
					obj = 0;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["AnimationDuration"] = value;
			}
		}

		// Token: 0x170031E2 RID: 12770
		// (get) Token: 0x06009DD2 RID: 40402 RVA: 0x002331A0 File Offset: 0x002313A0
		// (set) Token: 0x06009DD3 RID: 40403 RVA: 0x002331DA File Offset: 0x002313DA
		[Browsable(true)]
		[Description("Gets or sets a value indicating whether the loading panel will create an overlay element to ensure popups are over a flash element or Java applet.")]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool Overlay
		{
			get
			{
				bool? flag = this.ViewState["Overlay"] as bool?;
				return flag != null && flag.Value;
			}
			set
			{
				this.ViewState["Overlay"] = value;
			}
		}

		// Token: 0x170031E3 RID: 12771
		// (get) Token: 0x06009DD4 RID: 40404 RVA: 0x002331F2 File Offset: 0x002313F2
		// (set) Token: 0x06009DD5 RID: 40405 RVA: 0x00233221 File Offset: 0x00231421
		[ClientPropertyName("showing")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[ClientControlEvent]
		[Description(" ")]
		public string OnClientShowing
		{
			get
			{
				if (this.ViewState["OnClientShowing"] != null)
				{
					return (string)this.ViewState["OnClientShowing"];
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnClientShowing"] = value;
			}
		}

		// Token: 0x170031E4 RID: 12772
		// (get) Token: 0x06009DD6 RID: 40406 RVA: 0x00233234 File Offset: 0x00231434
		// (set) Token: 0x06009DD7 RID: 40407 RVA: 0x00233263 File Offset: 0x00231463
		[ClientControlEvent]
		[ClientPropertyName("hiding")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description(" ")]
		public string OnClientHiding
		{
			get
			{
				if (this.ViewState["OnClientHiding"] != null)
				{
					return (string)this.ViewState["OnClientHiding"];
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["OnClientHiding"] = value;
			}
		}

		// Token: 0x170031E5 RID: 12773
		// (get) Token: 0x06009DD8 RID: 40408 RVA: 0x00233276 File Offset: 0x00231476
		// (set) Token: 0x06009DD9 RID: 40409 RVA: 0x00233297 File Offset: 0x00231497
		[DefaultValue(false)]
		[Description("When set to true enables support for WAI-ARIA")]
		[Category("Behavior")]
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

		// Token: 0x06009DDA RID: 40410 RVA: 0x002332B0 File Offset: 0x002314B0
		public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptControlDescriptor = new RadControlScriptDescriptor("Telerik.Web.UI.RadAjaxLoadingPanel", this.ClientID);
			scriptControlDescriptor.AddProperty("minDisplayTime", this.MinDisplayTime);
			scriptControlDescriptor.AddProperty("initialDelayTime", this.InitialDelayTime);
			scriptControlDescriptor.AddProperty("isSticky", this.IsSticky);
			if (this.Transparency != 0)
			{
				scriptControlDescriptor.AddProperty("transparency", this.Transparency);
			}
			if (this.BackgroundTransparency != 0)
			{
				scriptControlDescriptor.AddProperty("backgroundTransparency", this.BackgroundTransparency);
			}
			scriptControlDescriptor.AddProperty("uniqueID", this.UniqueID);
			scriptControlDescriptor.AddProperty("zIndex", this.ZIndex);
			scriptControlDescriptor.AddProperty("skin", this.RuntimeSkin);
			if (this.Modal)
			{
				scriptControlDescriptor.AddProperty("_modal", this.Modal);
			}
			if (this.Overlay)
			{
				scriptControlDescriptor.AddProperty("_overlay", this.Overlay);
			}
			if (this.AnimationDuration > 0)
			{
				scriptControlDescriptor.AddProperty("animationDuration", this.AnimationDuration);
			}
			if (this.EnableAriaSupport)
			{
				scriptControlDescriptor.AddProperty("_enableAriaSupport", this.EnableAriaSupport);
			}
			string[] array = new string[]
			{
				"Showing",
				"Hiding"
			};
			foreach (string text in array)
			{
				string text2 = (string)DataBinder.GetPropertyValue(this, string.Format("OnClient{0}", text));
				if (!string.IsNullOrEmpty(text2))
				{
					scriptControlDescriptor.AddEvent(Regex.Replace(text, "^[A-Z]", new MatchEvaluator(RadAjaxLoadingPanel.ToLower)), text2);
				}
			}
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x06009DDB RID: 40411 RVA: 0x00233482 File Offset: 0x00231682
		internal static string ToLower(Match m)
		{
			return m.ToString().ToLower();
		}

		// Token: 0x06009DDC RID: 40412 RVA: 0x0023348F File Offset: 0x0023168F
		public IEnumerable<ScriptReference> GetScriptReferences()
		{
			if (!this.EnableEmbeddedScripts)
			{
				return null;
			}
			return ScriptRegistrar.GetScriptReferences(this);
		}

		// Token: 0x06009DDD RID: 40413 RVA: 0x002334A4 File Offset: 0x002316A4
		public static string GetWebResourceUrl(Page page, string webResourceName)
		{
			string webResourceUrl = page.ClientScript.GetWebResourceUrl(typeof(RadAjaxLoadingPanel), webResourceName);
			return webResourceUrl.Replace("&t", "&amp;t");
		}

		// Token: 0x06009DDE RID: 40414 RVA: 0x002334D8 File Offset: 0x002316D8
		Control IControlResolver.ResolveControl(string controlId)
		{
			return this.FindControl(controlId);
		}

		// Token: 0x170031E6 RID: 12774
		// (get) Token: 0x06009DDF RID: 40415 RVA: 0x002334E1 File Offset: 0x002316E1
		// (set) Token: 0x06009DE0 RID: 40416 RVA: 0x00233510 File Offset: 0x00231710
		[DefaultValue("")]
		[Description("Specifies the skin that will be used by the control")]
		[TypeConverter("Telerik.Web.Design.SkinTypeConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public virtual string Skin
		{
			get
			{
				if (this.ViewState["Skin"] == null)
				{
					return "";
				}
				return (string)this.ViewState["Skin"];
			}
			set
			{
				this.ViewState["Skin"] = value;
			}
		}

		// Token: 0x170031E7 RID: 12775
		// (get) Token: 0x06009DE1 RID: 40417 RVA: 0x00233523 File Offset: 0x00231723
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsSkinSet
		{
			get
			{
				return this.ViewState["Skin"] != null;
			}
		}

		// Token: 0x170031E8 RID: 12776
		// (get) Token: 0x06009DE2 RID: 40418 RVA: 0x0023353B File Offset: 0x0023173B
		// (set) Token: 0x06009DE3 RID: 40419 RVA: 0x0023356B File Offset: 0x0023176B
		[Description("Whether to register the selected skin automatically")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Category("Appearance")]
		public virtual bool EnableEmbeddedSkins
		{
			get
			{
				if (this.ViewState["EnableEmbeddedSkins"] == null)
				{
					return BaseClass.GetGlobalEnableEmbeddedSkins(this);
				}
				return (bool)this.ViewState["EnableEmbeddedSkins"];
			}
			set
			{
				this.ViewState["EnableEmbeddedSkins"] = value;
			}
		}

		// Token: 0x170031E9 RID: 12777
		// (get) Token: 0x06009DE4 RID: 40420 RVA: 0x00233583 File Offset: 0x00231783
		// (set) Token: 0x06009DE5 RID: 40421 RVA: 0x002335B3 File Offset: 0x002317B3
		[Description("Whether to register the base control skin file automatically")]
		[Category("Appearance")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public virtual bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				if (this.ViewState["EnableEmbeddedBaseStylesheet"] == null)
				{
					return BaseClass.GetGlobalEnableEmbeddedBaseStylesheet(this);
				}
				return (bool)this.ViewState["EnableEmbeddedBaseStylesheet"];
			}
			set
			{
				this.ViewState["EnableEmbeddedBaseStylesheet"] = value;
			}
		}

		// Token: 0x170031EA RID: 12778
		// (get) Token: 0x06009DE6 RID: 40422 RVA: 0x002335CB File Offset: 0x002317CB
		protected internal string RuntimeSkin
		{
			get
			{
				return SkinRegistrar.GetRuntimeSkin(this);
			}
		}

		// Token: 0x170031EB RID: 12779
		// (get) Token: 0x06009DE7 RID: 40423 RVA: 0x002335D3 File Offset: 0x002317D3
		// (set) Token: 0x06009DE8 RID: 40424 RVA: 0x002335DB File Offset: 0x002317DB
		string ISkinnableControl.AjaxCssRegistrations
		{
			get
			{
				return this._ajaxCssRegistrations;
			}
			set
			{
				this._ajaxCssRegistrations = value;
			}
		}

		// Token: 0x170031EC RID: 12780
		// (get) Token: 0x06009DE9 RID: 40425 RVA: 0x002335E4 File Offset: 0x002317E4
		// (set) Token: 0x06009DEA RID: 40426 RVA: 0x0023360F File Offset: 0x0023180F
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Category("Appearance")]
		[Description("Whether to register the skin CSS during Ajax requests")]
		public virtual bool EnableAjaxSkinRendering
		{
			get
			{
				return this.ViewState["EnableAjaxSkinRendering"] == null || (bool)this.ViewState["EnableAjaxSkinRendering"];
			}
			set
			{
				this.ViewState["EnableAjaxSkinRendering"] = value;
			}
		}

		// Token: 0x06009DEB RID: 40427 RVA: 0x00233627 File Offset: 0x00231827
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual List<string> GetEmbeddedSkinNames()
		{
			return SkinRegistrar.GetEmbeddedSkinNames(base.GetType());
		}

		// Token: 0x06009DEC RID: 40428 RVA: 0x00233634 File Offset: 0x00231834
		public virtual RenderMode PreferredRenderMode(RenderModeBrowserAdaptor browser)
		{
			return RenderMode.Classic;
		}

		// Token: 0x06009DED RID: 40429 RVA: 0x00233637 File Offset: 0x00231837
		public string GetSkinSuffix()
		{
			return "";
		}

		// Token: 0x06009DEE RID: 40430 RVA: 0x0023363E File Offset: 0x0023183E
		protected virtual void DescribeComponent(IScriptDescriptor descriptor)
		{
			ScriptObjectBuilder.DescribeComponent(this, descriptor, this, this);
		}

		// Token: 0x06009DEF RID: 40431 RVA: 0x00233649 File Offset: 0x00231849
		void IControl.DescribeComponent(IScriptDescriptor descriptor)
		{
			this.DescribeComponent(descriptor);
		}

		// Token: 0x06009DF0 RID: 40432 RVA: 0x00233652 File Offset: 0x00231852
		void IControl.EnsureChildControlsCreated()
		{
			this.EnsureChildControls();
		}

		// Token: 0x04002C6C RID: 11372
		private ScriptManager _scriptManager;

		// Token: 0x04002C6D RID: 11373
		private string _ajaxCssRegistrations;

		// Token: 0x04002C6E RID: 11374
		private bool _preRenderCalled;
	}
}

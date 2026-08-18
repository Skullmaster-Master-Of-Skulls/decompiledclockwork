using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.Analytics;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02000061 RID: 97
	[RequiredScript(typeof(Core))]
	[ClientScriptResource("Telerik.Web.UI.RadWebControl", "Telerik.Web.UI.Common.Core.js")]
	public abstract class RadDataBoundControl : DataBoundControl, IScriptControl, IControlResolver, IPostBackDataHandler, ISkinnableControl, IControl, IFeatureGroup
	{
		// Token: 0x060002EC RID: 748 RVA: 0x00007B70 File Offset: 0x00005D70
		public RadDataBoundControl()
		{
			this.EnsureLicensing();
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00007B90 File Offset: 0x00005D90
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00007BC8 File Offset: 0x00005DC8
		[Category("Misc")]
		[DefaultValue("")]
		public virtual string FeatureGroupID
		{
			get
			{
				if (base.DesignMode)
				{
					return string.Empty;
				}
				return ((string)(this.ViewState["FeatureGroupID"] ?? this.ID)) ?? string.Empty;
			}
			set
			{
				this.ViewState["FeatureGroupID"] = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00007BDC File Offset: 0x00005DDC
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00007C05 File Offset: 0x00005E05
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Whether to register with the ScriptManager control on the page")]
		public virtual bool RegisterWithScriptManager
		{
			get
			{
				object obj = this.ViewState["RegisterWithScriptManager"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["RegisterWithScriptManager"] = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x00007C1D File Offset: 0x00005E1D
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00007C3D File Offset: 0x00005E3D
		[TypeConverter("Telerik.Web.Design.SkinTypeConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[NotifyParentProperty(true)]
		[Description("Specifies the skin that will be used by the control")]
		[DefaultValue("Default")]
		[Category("Appearance")]
		public virtual string Skin
		{
			get
			{
				return (string)(this.ViewState["Skin"] ?? "Default");
			}
			set
			{
				this.ViewState["Skin"] = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00007C50 File Offset: 0x00005E50
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Returns true if ripple effect should be added")]
		protected virtual bool EnableRippleEffect
		{
			get
			{
				return this.RuntimeSkin == "Material";
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00007C62 File Offset: 0x00005E62
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public bool IsSkinSet
		{
			get
			{
				return this.ViewState["Skin"] != null;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00007C7A File Offset: 0x00005E7A
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00007CAA File Offset: 0x00005EAA
		[Description("Whether to register the scripts automatically")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
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

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00007CC2 File Offset: 0x00005EC2
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00007CF2 File Offset: 0x00005EF2
		[Category("Behavior")]
		[Description("Whether to register the selected skin automatically")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
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

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00007D0A File Offset: 0x00005F0A
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00007D3A File Offset: 0x00005F3A
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Description("Whether to register the base control skin file automatically")]
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

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00007D52 File Offset: 0x00005F52
		// (set) Token: 0x060002FC RID: 764 RVA: 0x00007D72 File Offset: 0x00005F72
		[Description("Gets or sets the ODataDataSource used for data binding.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Data")]
		public virtual string ODataDataSourceID
		{
			get
			{
				return ((string)this.ViewState["ODataDataSourceID"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ODataDataSourceID"] = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00007D85 File Offset: 0x00005F85
		// (set) Token: 0x060002FE RID: 766 RVA: 0x00007DA5 File Offset: 0x00005FA5
		[Category("Data")]
		[DefaultValue("")]
		public virtual string ClientDataSourceID
		{
			get
			{
				return (string)(this.ViewState["ClientDataSourceID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ClientDataSourceID"] = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00007DB8 File Offset: 0x00005FB8
		// (set) Token: 0x06000300 RID: 768 RVA: 0x00007DD8 File Offset: 0x00005FD8
		[Category("Data")]
		[DefaultValue("")]
		public virtual string DataModelID
		{
			get
			{
				return (string)(this.ViewState["DataModelID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["DataModelID"] = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00007DEB File Offset: 0x00005FEB
		protected internal string RuntimeSkin
		{
			get
			{
				return SkinRegistrar.GetRuntimeSkin(this);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00007DF3 File Offset: 0x00005FF3
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00007E1E File Offset: 0x0000601E
		[Description("Whether to register the skin CSS during Ajax requests")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Category("Appearance")]
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

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00007E36 File Offset: 0x00006036
		[ClientControlProperty]
		[Browsable(false)]
		public string ClientStateFieldID
		{
			get
			{
				return this.ClientID + "_ClientState";
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00007E48 File Offset: 0x00006048
		// (set) Token: 0x06000306 RID: 774 RVA: 0x00007E69 File Offset: 0x00006069
		[Description("Specifies the rendering mode of the control")]
		[Category("Appearance")]
		[DefaultValue(RenderMode.Classic)]
		public virtual RenderMode RenderMode
		{
			get
			{
				return (RenderMode)(this.ViewState["RenderMode"] ?? RenderMode.Classic);
			}
			set
			{
				this.ViewState["RenderMode"] = value;
				this._renderModeSet = true;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00007E88 File Offset: 0x00006088
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00007EE4 File Offset: 0x000060E4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Returns resolved RenderMode should the original value was Auto")]
		public virtual RenderMode ResolvedRenderMode
		{
			get
			{
				if (!base.DesignMode)
				{
					if (this.ViewState["ResolvedRenderMode"] == null || this.ViewState.IsItemDirty("RenderMode"))
					{
						this.ResolvedRenderMode = this.ResolveRenderMode();
					}
					return (RenderMode)this.ViewState["ResolvedRenderMode"];
				}
				return RenderMode.Classic;
			}
			private set
			{
				this.ViewState["ResolvedRenderMode"] = value;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00007EFC File Offset: 0x000060FC
		protected virtual RenderMode ResolveRenderMode()
		{
			RenderMode renderMode = this.SupportsRenderingMode ? this.RenderMode : RenderMode.Classic;
			if (renderMode == RenderMode.Classic)
			{
				return renderMode;
			}
			RenderModeBrowserAdaptor instance = RenderModeBrowserAdaptor.Instance;
			if (this.CanRenderInMode(instance, renderMode))
			{
				return renderMode;
			}
			return this.PreferredRenderMode(RenderModeBrowserAdaptor.Instance);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00007F3E File Offset: 0x0000613E
		protected internal virtual bool CanRenderInMode(RenderModeBrowserAdaptor browser, RenderMode mode)
		{
			if (mode == RenderMode.Native)
			{
				return this.SupportsNativeRendering;
			}
			if (mode == RenderMode.Mobile)
			{
				return this.SupportsAdaptiveRendering;
			}
			return mode == RenderMode.Lightweight && browser.IsModernBrowser && this.SupportsLightweightRendering;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00007F6A File Offset: 0x0000616A
		protected internal virtual RenderMode PreferredRenderMode(RenderModeBrowserAdaptor browser)
		{
			if (this.RenderMode != RenderMode.Auto && !this.CanRenderInMode(browser, RenderMode.Lightweight))
			{
				return RenderMode.Classic;
			}
			if (this.SupportsAdaptiveRendering && browser.IsMobileDevice)
			{
				return RenderMode.Mobile;
			}
			if (this.CanRenderInMode(browser, RenderMode.Lightweight))
			{
				return RenderMode.Lightweight;
			}
			return RenderMode.Classic;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00007FA0 File Offset: 0x000061A0
		protected internal virtual string GetSkinSuffix()
		{
			if (!this.SupportsRenderingMode)
			{
				return string.Empty;
			}
			string renderingModeString = RenderModeHelper.GetRenderingModeString(this.ResolvedRenderMode);
			if (!(renderingModeString == "Classic"))
			{
				return renderingModeString;
			}
			return string.Empty;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00007FDB File Offset: 0x000061DB
		protected internal virtual bool SupportsAdaptiveRendering
		{
			get
			{
				return RenderModesCache.GetAdaptiveTypes().ContainsOrInheritsFromType(base.GetType());
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00007FED File Offset: 0x000061ED
		protected internal virtual bool SupportsNativeRendering
		{
			get
			{
				return RenderModesCache.GetNativeTypes().ContainsOrInheritsFromType(base.GetType());
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00007FFF File Offset: 0x000061FF
		protected internal virtual bool SupportsLightweightRendering
		{
			get
			{
				return RenderModesCache.GetLightweightTypes().ContainsOrInheritsFromType(base.GetType());
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00008011 File Offset: 0x00006211
		protected virtual string CssClassFormatString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00008018 File Offset: 0x00006218
		protected virtual string DefaultCssClass
		{
			get
			{
				return "Default";
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000801F File Offset: 0x0000621F
		internal virtual bool SupportsOData
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00008022 File Offset: 0x00006222
		protected internal virtual bool SupportsRenderingMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00008025 File Offset: 0x00006225
		protected virtual bool IsRenderModeSet
		{
			get
			{
				return this._renderModeSet;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000802D File Offset: 0x0000622D
		protected virtual bool IsBoundUsingOData
		{
			get
			{
				return !string.IsNullOrEmpty(this.ODataDataSourceID);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000803D File Offset: 0x0000623D
		protected virtual bool IsBoundUsingClientDataSource
		{
			get
			{
				return !string.IsNullOrEmpty(this.ClientDataSourceID);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000804D File Offset: 0x0000624D
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00008056 File Offset: 0x00006256
		public override System.Version RenderingCompatibility
		{
			get
			{
				return new System.Version(3, 5);
			}
			set
			{
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00008058 File Offset: 0x00006258
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00008060 File Offset: 0x00006260
		[Description("This property is overridden in order to support controls which implement INamingContainer")]
		[DefaultValue(ClientIDMode.AutoID)]
		[NotifyParentProperty(true)]
		public override ClientIDMode ClientIDMode
		{
			get
			{
				return this.ClientIDModeValue;
			}
			set
			{
				if (this.ClientIDModeValue != value)
				{
					base.ClearEffectiveClientIDMode();
					base.ClearCachedClientID();
				}
				this.ClientIDModeValue = value;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00008080 File Offset: 0x00006280
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.CssClass;
			this.CssClass = string.Format(this.CssClassFormatString + " " + cssClass, this.RuntimeSkin).Trim();
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000080C9 File Offset: 0x000062C9
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.ControlPreRender();
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000080D8 File Offset: 0x000062D8
		protected virtual void ControlPreRender()
		{
			this.ConfigureCombinedScriptFile();
			this.ConfigureCombinedBaseSkinFile();
			this.RegisterScriptControl();
			base.EnsureID();
			this.RegisterCssReferences();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000080F8 File Offset: 0x000062F8
		protected void ConfigureCombinedScriptFile()
		{
			if (this.EnableEmbeddedScripts)
			{
				this.EnableEmbeddedScripts = !RadScriptManager.IsCombinedScriptEnabled(this.Page);
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00008116 File Offset: 0x00006316
		protected void ConfigureCombinedBaseSkinFile()
		{
			if (this.EnableEmbeddedBaseStylesheet)
			{
				this.EnableEmbeddedBaseStylesheet = !RadStyleSheetManager.IsCombinedBaseSkinEnabled(this.Page);
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00008134 File Offset: 0x00006334
		protected virtual void RegisterScriptControl()
		{
			if (this.RegisterWithScriptManager)
			{
				this.ScriptManager.RegisterScriptControl<RadDataBoundControl>(this);
			}
			else
			{
				this.EnsureChildControls();
				ControlRenderer.EnsureChildControlsAreNotRegistered(this);
			}
			this.Page.RegisterRequiresPostBack(this);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00008164 File Offset: 0x00006364
		protected virtual void RegisterCssReferences()
		{
			RadStyleSheetManager current = RadStyleSheetManager.GetCurrent(this.Page);
			if (current == null)
			{
				SkinRegistrar.RegisterCssReferences(this);
				return;
			}
			current.RegisterSkinnableControl(this);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000818E File Offset: 0x0000638E
		protected virtual void RegisterScriptDescriptors()
		{
			if (this.RegisterWithScriptManager && !this.Described)
			{
				this.ScriptManager.RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000081AC File Offset: 0x000063AC
		protected virtual void LoadClientState(Dictionary<string, object> clientState)
		{
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000081AE File Offset: 0x000063AE
		protected virtual string SaveClientState()
		{
			return null;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000325 RID: 805 RVA: 0x000081B1 File Offset: 0x000063B1
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

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000326 RID: 806 RVA: 0x000081CD File Offset: 0x000063CD
		protected RadScriptManager RadScriptManager
		{
			get
			{
				return this.ScriptManager as RadScriptManager;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000327 RID: 807 RVA: 0x000081DA File Offset: 0x000063DA
		protected RadStyleSheetManager RadStyleSheetManager
		{
			get
			{
				return RadStyleSheetManager.GetCurrent(this.Page);
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000081E8 File Offset: 0x000063E8
		public virtual void RenderClientStateField(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientStateFieldID);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.ClientStateFieldID);
			string value = this.SaveClientState();
			if (!string.IsNullOrEmpty(value))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Value, value);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00008244 File Offset: 0x00006444
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
			BaseClass.RenderVersionStamp(writer);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00008253 File Offset: 0x00006453
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			this.RenderClientStateField(writer);
			BaseClass.RenderAjaxCssReferences(this, writer);
			base.RenderEndTag(writer);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000826C File Offset: 0x0000646C
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this.RegisterWithScriptManager)
			{
				this.ControlPreRender();
				this.EnsureChildControls();
				this.RenderScriptsNoScriptManager(writer);
			}
			base.Render(writer);
			if (!base.DesignMode)
			{
				this.RegisterScriptDescriptors();
			}
			if (!this.RegisterWithScriptManager)
			{
				this.RenderDescriptorsNoScriptManager(writer);
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000082B8 File Offset: 0x000064B8
		protected virtual void RenderScriptsNoScriptManager(HtmlTextWriter writer)
		{
			string controlScripts = ControlRenderer.GetControlScripts(this);
			if (!string.IsNullOrEmpty(controlScripts))
			{
				writer.Write("<input type=\"hidden\"/>");
			}
			writer.WriteLine(controlScripts);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000082E8 File Offset: 0x000064E8
		protected virtual void RenderDescriptorsNoScriptManager(HtmlTextWriter writer)
		{
			string controlDescriptors = ControlRenderer.GetControlDescriptors(this);
			writer.WriteLine(controlDescriptors);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00008303 File Offset: 0x00006503
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			base.RenderContents(writer);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00008313 File Offset: 0x00006513
		protected void ApplyConditionalRendering(HtmlTextWriter writer)
		{
			if (this.RenderMode == RenderMode.Classic)
			{
				this.RenderClassic(writer);
				return;
			}
			if (this.RenderMode == RenderMode.Lightweight)
			{
				this.RenderLite(writer);
				return;
			}
			if (this.RenderMode == RenderMode.Native)
			{
				this.RenderNative(writer);
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00008347 File Offset: 0x00006547
		protected virtual void RenderClassic(HtmlTextWriter writer)
		{
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00008349 File Offset: 0x00006549
		protected virtual void RenderLite(HtmlTextWriter writer)
		{
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000834B File Offset: 0x0000654B
		protected virtual void RenderNative(HtmlTextWriter writer)
		{
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00008350 File Offset: 0x00006550
		protected internal virtual void InitializeRenderMode()
		{
			if (!this.IsRenderModeSet)
			{
				if (RenderModeConfigurationReader.Instance.HasGlobalKey())
				{
					this.RenderMode = RenderModeConfigurationReader.Instance.GetRenderMode(this);
				}
				if (RenderModeConfigurationReader.Instance.HasKey(base.GetType()))
				{
					this.RenderMode = RenderModeConfigurationReader.Instance.GetRenderMode(base.GetType(), this);
				}
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000083AB File Offset: 0x000065AB
		protected override void OnInit(EventArgs e)
		{
			if (this.SupportsRenderingMode)
			{
				this.InitializeRenderMode();
			}
			base.OnInit(e);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x000083C2 File Offset: 0x000065C2
		protected virtual void RenderTrialMessage(HtmlTextWriter writer)
		{
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000083C4 File Offset: 0x000065C4
		protected virtual void DescribeComponent(IScriptDescriptor descriptor)
		{
			this._describedProperties.Clear();
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			if (this.IsBoundUsingOData)
			{
				descriptor.AddScriptProperty("odataClientSettings", javaScriptSerializer.Serialize(ODataClientSettings.FromHierarhicalControl<RadDataBoundControl>(this)));
			}
			if (this.EnableRippleEffect)
			{
				descriptor.AddProperty("_enableRippleEffect", this.EnableRippleEffect);
			}
			this.DescribeClientProperties(descriptor);
			this.DescribeClientEvents(descriptor);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000842D File Offset: 0x0000662D
		protected void DescribeRenderingMode(IScriptDescriptor descriptor)
		{
			if (this.ResolvedRenderMode != RenderMode.Classic)
			{
				descriptor.AddProperty("_renderMode", this.ResolvedRenderMode);
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000844E File Offset: 0x0000664E
		protected void DescribeProperty<T>(IScriptDescriptor descriptor, string name, T value, T defaultValue)
		{
			if (this._describedProperties.ContainsKey(name))
			{
				return;
			}
			this._describedProperties.Add(name, null);
			if (!EqualityComparer<T>.Default.Equals(value, defaultValue))
			{
				descriptor.AddProperty(name, value);
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00008488 File Offset: 0x00006688
		protected static void DescribeEvent(IScriptDescriptor descriptor, string name, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			descriptor.AddEvent(name, value);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000849B File Offset: 0x0000669B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual List<string> GetEmbeddedSkinNames()
		{
			return SkinRegistrar.GetEmbeddedSkinNames(base.GetType());
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000084A8 File Offset: 0x000066A8
		protected internal virtual IRenderer CreateControlRenderer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600033C RID: 828 RVA: 0x000084AF File Offset: 0x000066AF
		protected internal virtual IRenderer Renderer
		{
			get
			{
				if (this._renderer == null)
				{
					this._renderer = this.CreateControlRenderer();
				}
				return this._renderer;
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000084CB File Offset: 0x000066CB
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			this.Described = true;
			return this.GetScriptDescriptors();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000084DA File Offset: 0x000066DA
		protected virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			return ScriptRegistrar.GetScriptDescriptors(this);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000084E2 File Offset: 0x000066E2
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return this.GetScriptReferences();
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000084EC File Offset: 0x000066EC
		protected virtual IEnumerable<ScriptReference> GetScriptReferences()
		{
			List<ScriptReference> list = new List<ScriptReference>();
			if (this.EnableEmbeddedScripts)
			{
				list.AddRange(ScriptRegistrar.GetScriptReferences(this));
				list.AddRange(this.GetViewScriptReference());
			}
			return list;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00008580 File Offset: 0x00006780
		protected virtual IEnumerable<ScriptReference> GetViewScriptReference()
		{
			RenderMode resolvedMode = this.ResolvedRenderMode;
			IOrderedEnumerable<ViewDescriptorAttribute> orderedEnumerable = from p in RenderModesCache.GetViewDescriptors()
			where p.RenderMode == resolvedMode && (p.Type == this.GetType() || p.Type.IsAssignableFrom(this.GetType()))
			orderby p.LoadOrder
			select p;
			List<ScriptReference> list = new List<ScriptReference>();
			string fullName = Assembly.GetExecutingAssembly().FullName;
			foreach (ViewDescriptorAttribute viewDescriptorAttribute in orderedEnumerable)
			{
				list.Add(new ScriptReference(viewDescriptorAttribute.ScriptResource, fullName));
			}
			return list;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00008640 File Offset: 0x00006840
		Control IControlResolver.ResolveControl(string controlId)
		{
			return this.FindControl(controlId);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000864C File Offset: 0x0000684C
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[this.ClientStateFieldID];
			if (!string.IsNullOrEmpty(text))
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				Dictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(text) as Dictionary<string, object>;
				if (dictionary != null)
				{
					this.LoadClientState(dictionary);
				}
			}
			return false;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000868C File Offset: 0x0000688C
		protected virtual void RaisePostDataChangedEvent()
		{
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000868E File Offset: 0x0000688E
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00008698 File Offset: 0x00006898
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000086A0 File Offset: 0x000068A0
		void IControl.DescribeComponent(IScriptDescriptor descriptor)
		{
			this.DescribeComponent(descriptor);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000086A9 File Offset: 0x000068A9
		void IControl.EnsureChildControlsCreated()
		{
			this.EnsureChildControls();
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000349 RID: 841 RVA: 0x000086B1 File Offset: 0x000068B1
		// (set) Token: 0x0600034A RID: 842 RVA: 0x000086B9 File Offset: 0x000068B9
		string ISkinnableControl.Skin
		{
			get
			{
				return this.Skin;
			}
			set
			{
				this.Skin = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600034B RID: 843 RVA: 0x000086C2 File Offset: 0x000068C2
		// (set) Token: 0x0600034C RID: 844 RVA: 0x000086CA File Offset: 0x000068CA
		bool ISkinnableControl.EnableEmbeddedSkins
		{
			get
			{
				return this.EnableEmbeddedSkins;
			}
			set
			{
				this.EnableEmbeddedSkins = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000086D3 File Offset: 0x000068D3
		// (set) Token: 0x0600034E RID: 846 RVA: 0x000086DB File Offset: 0x000068DB
		bool ISkinnableControl.EnableEmbeddedBaseStylesheet
		{
			get
			{
				return this.EnableEmbeddedBaseStylesheet;
			}
			set
			{
				this.EnableEmbeddedBaseStylesheet = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600034F RID: 847 RVA: 0x000086E4 File Offset: 0x000068E4
		// (set) Token: 0x06000350 RID: 848 RVA: 0x000086EC File Offset: 0x000068EC
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

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000351 RID: 849 RVA: 0x000086F5 File Offset: 0x000068F5
		// (set) Token: 0x06000352 RID: 850 RVA: 0x000086FD File Offset: 0x000068FD
		bool ISkinnableControl.EnableAjaxSkinRendering
		{
			get
			{
				return this.EnableAjaxSkinRendering;
			}
			set
			{
				this.EnableAjaxSkinRendering = value;
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00008706 File Offset: 0x00006906
		RenderMode ISkinnableControl.PreferredRenderMode(RenderModeBrowserAdaptor browser)
		{
			return this.PreferredRenderMode(browser);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000870F File Offset: 0x0000690F
		string ISkinnableControl.GetSkinSuffix()
		{
			return this.GetSkinSuffix();
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00008717 File Offset: 0x00006917
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000871F File Offset: 0x0000691F
		[NotifyParentProperty(true)]
		public override string AccessKey
		{
			get
			{
				return base.AccessKey;
			}
			set
			{
				base.AccessKey = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00008728 File Offset: 0x00006928
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00008730 File Offset: 0x00006930
		[NotifyParentProperty(true)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00008739 File Offset: 0x00006939
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00008741 File Offset: 0x00006941
		[NotifyParentProperty(true)]
		public override Color BorderColor
		{
			get
			{
				return base.BorderColor;
			}
			set
			{
				base.BorderColor = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000874A File Offset: 0x0000694A
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00008752 File Offset: 0x00006952
		[NotifyParentProperty(true)]
		public override BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000875B File Offset: 0x0000695B
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00008763 File Offset: 0x00006963
		[NotifyParentProperty(true)]
		public override Unit BorderWidth
		{
			get
			{
				return base.BorderWidth;
			}
			set
			{
				base.BorderWidth = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000876C File Offset: 0x0000696C
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00008774 File Offset: 0x00006974
		[NotifyParentProperty(true)]
		public override string CssClass
		{
			get
			{
				return base.CssClass;
			}
			set
			{
				base.CssClass = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000877D File Offset: 0x0000697D
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00008785 File Offset: 0x00006985
		[NotifyParentProperty(true)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000878E File Offset: 0x0000698E
		// (set) Token: 0x06000364 RID: 868 RVA: 0x00008796 File Offset: 0x00006996
		[NotifyParentProperty(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000879F File Offset: 0x0000699F
		// (set) Token: 0x06000366 RID: 870 RVA: 0x000087A7 File Offset: 0x000069A7
		[NotifyParentProperty(true)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000087B0 File Offset: 0x000069B0
		[NotifyParentProperty(true)]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000368 RID: 872 RVA: 0x000087B8 File Offset: 0x000069B8
		// (set) Token: 0x06000369 RID: 873 RVA: 0x000087C0 File Offset: 0x000069C0
		[NotifyParentProperty(true)]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600036A RID: 874 RVA: 0x000087C9 File Offset: 0x000069C9
		// (set) Token: 0x0600036B RID: 875 RVA: 0x000087D1 File Offset: 0x000069D1
		[NotifyParentProperty(true)]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600036C RID: 876 RVA: 0x000087DA File Offset: 0x000069DA
		// (set) Token: 0x0600036D RID: 877 RVA: 0x000087E2 File Offset: 0x000069E2
		[NotifyParentProperty(true)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600036E RID: 878 RVA: 0x000087EB File Offset: 0x000069EB
		// (set) Token: 0x0600036F RID: 879 RVA: 0x000087F3 File Offset: 0x000069F3
		[NotifyParentProperty(true)]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000370 RID: 880 RVA: 0x000087FC File Offset: 0x000069FC
		// (set) Token: 0x06000371 RID: 881 RVA: 0x00008804 File Offset: 0x00006A04
		[NotifyParentProperty(true)]
		public override short TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000880D File Offset: 0x00006A0D
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00008815 File Offset: 0x00006A15
		[NotifyParentProperty(true)]
		public override string ToolTip
		{
			get
			{
				return base.ToolTip;
			}
			set
			{
				base.ToolTip = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000881E File Offset: 0x00006A1E
		// (set) Token: 0x06000375 RID: 885 RVA: 0x00008826 File Offset: 0x00006A26
		[NotifyParentProperty(true)]
		public override string DataMember
		{
			get
			{
				return base.DataMember;
			}
			set
			{
				base.DataMember = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000882F File Offset: 0x00006A2F
		// (set) Token: 0x06000377 RID: 887 RVA: 0x00008837 File Offset: 0x00006A37
		[NotifyParentProperty(true)]
		public override object DataSource
		{
			get
			{
				return base.DataSource;
			}
			set
			{
				base.DataSource = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00008840 File Offset: 0x00006A40
		// (set) Token: 0x06000379 RID: 889 RVA: 0x00008848 File Offset: 0x00006A48
		[NotifyParentProperty(true)]
		public override string DataSourceID
		{
			get
			{
				return base.DataSourceID;
			}
			set
			{
				base.DataSourceID = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600037A RID: 890 RVA: 0x00008851 File Offset: 0x00006A51
		// (set) Token: 0x0600037B RID: 891 RVA: 0x00008859 File Offset: 0x00006A59
		[NotifyParentProperty(true)]
		public override bool EnableViewState
		{
			get
			{
				return base.EnableViewState;
			}
			set
			{
				base.EnableViewState = value;
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00008864 File Offset: 0x00006A64
		protected T GetViewStateValue<T>(string key, T defaultValue)
		{
			object obj = this.ViewState[key];
			if (obj == null)
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000888C File Offset: 0x00006A8C
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

		// Token: 0x0600037E RID: 894 RVA: 0x000088C4 File Offset: 0x00006AC4
		protected internal virtual void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			this.DescribeProperty<string>(descriptor, "clientStateFieldID", this.ClientStateFieldID, null);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000088D9 File Offset: 0x00006AD9
		protected internal virtual void DescribeClientEvents(IScriptDescriptor descriptor)
		{
		}

		// Token: 0x0400005E RID: 94
		private IRenderer _renderer;

		// Token: 0x0400005F RID: 95
		private ScriptManager _scriptManager;

		// Token: 0x04000060 RID: 96
		private bool _renderModeSet;

		// Token: 0x04000061 RID: 97
		private ClientIDMode ClientIDModeValue = ClientIDMode.AutoID;

		// Token: 0x04000062 RID: 98
		private Dictionary<string, object> _describedProperties = new Dictionary<string, object>();

		// Token: 0x04000063 RID: 99
		private bool Described;

		// Token: 0x04000064 RID: 100
		private string _ajaxCssRegistrations;
	}
}

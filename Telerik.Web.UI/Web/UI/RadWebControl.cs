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
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x0200000A RID: 10
	[RequiredScript(typeof(Core))]
	[ClientScriptResource("Telerik.Web.UI.RadWebControl", "Telerik.Web.UI.Common.Core.js")]
	public abstract class RadWebControl : WebControl, IScriptControl, IControlResolver, IPostBackDataHandler, ISkinnableControl, IControl
	{
		// Token: 0x0600004F RID: 79 RVA: 0x0000255F File Offset: 0x0000075F
		public RadWebControl()
		{
			this.EnsureLicensing();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000257F File Offset: 0x0000077F
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000025A0 File Offset: 0x000007A0
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Whether to register with the ScriptManager control on the page")]
		public virtual bool RegisterWithScriptManager
		{
			get
			{
				return (bool)(this.ViewState["RegisterWithScriptManager"] ?? true);
			}
			set
			{
				this.ViewState["RegisterWithScriptManager"] = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000025B8 File Offset: 0x000007B8
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000025E7 File Offset: 0x000007E7
		[DefaultValue("Default")]
		[Category("Appearance")]
		[TypeConverter("Telerik.Web.Design.SkinTypeConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[Description("Specifies the skin that will be used by the control")]
		[NotifyParentProperty(true)]
		public virtual string Skin
		{
			get
			{
				if (this.ViewState["Skin"] == null)
				{
					return "Default";
				}
				return (string)this.ViewState["Skin"];
			}
			set
			{
				this.ViewState["Skin"] = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000025FA File Offset: 0x000007FA
		[Description("Returns true if ripple effect should be added")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual bool EnableRippleEffect
		{
			get
			{
				return this.RuntimeSkin == "Material";
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000260C File Offset: 0x0000080C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsSkinSet
		{
			get
			{
				return this.ViewState["Skin"] != null;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002624 File Offset: 0x00000824
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002654 File Offset: 0x00000854
		[Description("Whether to register the scripts automatically")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000266C File Offset: 0x0000086C
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000269C File Offset: 0x0000089C
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

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000026B4 File Offset: 0x000008B4
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000026E4 File Offset: 0x000008E4
		[Description("Whether to register the base control skin file automatically")]
		[DefaultValue(true)]
		[Category("Appearance")]
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000026FC File Offset: 0x000008FC
		protected internal string RuntimeSkin
		{
			get
			{
				return SkinRegistrar.GetRuntimeSkin(this);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002704 File Offset: 0x00000904
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000272F File Offset: 0x0000092F
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("Whether to register the skin CSS during Ajax requests")]
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

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002747 File Offset: 0x00000947
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string ClientStateFieldID
		{
			get
			{
				return this.ClientID + "_ClientState";
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002759 File Offset: 0x00000959
		// (set) Token: 0x06000061 RID: 97 RVA: 0x0000277A File Offset: 0x0000097A
		[NotifyParentProperty(true)]
		[DefaultValue(RenderMode.Classic)]
		[Description("Specifies the rendering mode of the control")]
		[Category("Appearance")]
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

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000279C File Offset: 0x0000099C
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000027F8 File Offset: 0x000009F8
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

		// Token: 0x06000064 RID: 100 RVA: 0x00002810 File Offset: 0x00000A10
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
			return this.PreferredRenderMode(instance);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000284E File Offset: 0x00000A4E
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

		// Token: 0x06000066 RID: 102 RVA: 0x0000287A File Offset: 0x00000A7A
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

		// Token: 0x06000067 RID: 103 RVA: 0x000028B0 File Offset: 0x00000AB0
		protected internal virtual string GetSkinSuffix()
		{
			if (!this.SupportsRenderingMode)
			{
				return "";
			}
			string renderingModeString = RenderModeHelper.GetRenderingModeString(this.ResolvedRenderMode);
			if (!(renderingModeString == "Classic"))
			{
				return renderingModeString;
			}
			return string.Empty;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000028EB File Offset: 0x00000AEB
		protected internal virtual bool SupportsAdaptiveRendering
		{
			get
			{
				return RenderModesCache.GetAdaptiveTypes().ContainsOrInheritsFromType(base.GetType());
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000028FD File Offset: 0x00000AFD
		protected internal virtual bool SupportsNativeRendering
		{
			get
			{
				return RenderModesCache.GetNativeTypes().ContainsOrInheritsFromType(base.GetType());
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000290F File Offset: 0x00000B0F
		protected internal virtual bool SupportsLightweightRendering
		{
			get
			{
				return RenderModesCache.GetLightweightTypes().ContainsOrInheritsFromType(base.GetType());
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002921 File Offset: 0x00000B21
		protected virtual string CssClassFormatString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002928 File Offset: 0x00000B28
		protected internal virtual bool SupportsRenderingMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000292B File Offset: 0x00000B2B
		protected virtual bool IsRenderModeSet
		{
			get
			{
				return this._renderModeSet;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002933 File Offset: 0x00000B33
		internal virtual string DefaultCssClass
		{
			get
			{
				return "Default";
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000293A File Offset: 0x00000B3A
		internal virtual bool ShouldRegisterCssReferences
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000293D File Offset: 0x00000B3D
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00002946 File Offset: 0x00000B46
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002948 File Offset: 0x00000B48
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002950 File Offset: 0x00000B50
		[Description("This property is overridden in order to support controls which implement INamingContainer")]
		[DefaultValue(ClientIDMode.AutoID)]
		[NotifyParentProperty(true)]
		public override ClientIDMode ClientIDMode
		{
			get
			{
				return this._clientIDModeValue;
			}
			set
			{
				if (this._clientIDModeValue != value)
				{
					base.ClearEffectiveClientIDMode();
					base.ClearCachedClientID();
				}
				this._clientIDModeValue = value;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002970 File Offset: 0x00000B70
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.CssClass;
			this.CssClass = string.Format(this.CssClassFormatString + " " + cssClass, this.RuntimeSkin).Trim();
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000029B9 File Offset: 0x00000BB9
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.ControlPreRender();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000029C8 File Offset: 0x00000BC8
		protected virtual void ControlPreRender()
		{
			this.ConfigureCombinedScriptFile();
			this.ConfigureCombinedBaseSkinFile();
			this.RegisterScriptControl();
			base.EnsureID();
			if (this.ShouldRegisterCssReferences)
			{
				this.RegisterCssReferences();
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000029F0 File Offset: 0x00000BF0
		protected void ConfigureCombinedScriptFile()
		{
			if (this.EnableEmbeddedScripts)
			{
				this.EnableEmbeddedScripts = !RadScriptManager.IsCombinedScriptEnabled(this.Page);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002A0E File Offset: 0x00000C0E
		protected void ConfigureCombinedBaseSkinFile()
		{
			if (this.EnableEmbeddedBaseStylesheet)
			{
				this.EnableEmbeddedBaseStylesheet = !RadStyleSheetManager.IsCombinedBaseSkinEnabled(this.Page);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002A2C File Offset: 0x00000C2C
		protected virtual void RegisterScriptControl()
		{
			if (this.RegisterWithScriptManager)
			{
				this.ScriptManager.RegisterScriptControl<RadWebControl>(this);
				this.Page.RegisterRequiresPostBack(this);
				return;
			}
			this.EnsureChildControls();
			ControlRenderer.EnsureChildControlsAreNotRegistered(this);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002A5C File Offset: 0x00000C5C
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

		// Token: 0x0600007B RID: 123 RVA: 0x00002A86 File Offset: 0x00000C86
		protected virtual void LoadClientState(Dictionary<string, object> clientState)
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002A88 File Offset: 0x00000C88
		protected virtual string SaveClientState()
		{
			return null;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002A8B File Offset: 0x00000C8B
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002AA7 File Offset: 0x00000CA7
		protected RadScriptManager RadScriptManager
		{
			get
			{
				return this.ScriptManager as RadScriptManager;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002AB4 File Offset: 0x00000CB4
		protected RadStyleSheetManager RadStyleSheetManager
		{
			get
			{
				return RadStyleSheetManager.GetCurrent(this.Page);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002AC4 File Offset: 0x00000CC4
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

		// Token: 0x06000081 RID: 129 RVA: 0x00002B20 File Offset: 0x00000D20
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
			BaseClass.RenderVersionStamp(writer);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002B2F File Offset: 0x00000D2F
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			this.RenderClientStateField(writer);
			BaseClass.RenderAjaxCssReferences(this, writer);
			base.RenderEndTag(writer);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002B48 File Offset: 0x00000D48
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

		// Token: 0x06000084 RID: 132 RVA: 0x00002B94 File Offset: 0x00000D94
		protected virtual void RenderScriptsNoScriptManager(HtmlTextWriter writer)
		{
			string controlScripts = ControlRenderer.GetControlScripts(this);
			if (!string.IsNullOrEmpty(controlScripts))
			{
				writer.Write("<input type=\"hidden\"/>");
			}
			writer.WriteLine(controlScripts);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002BC4 File Offset: 0x00000DC4
		protected virtual void RenderDescriptorsNoScriptManager(HtmlTextWriter writer)
		{
			string controlDescriptors = ControlRenderer.GetControlDescriptors(this);
			writer.WriteLine(controlDescriptors);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002BDF File Offset: 0x00000DDF
		protected virtual void RegisterScriptDescriptors()
		{
			if (this.RegisterWithScriptManager && !this.Described)
			{
				this.ScriptManager.RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002BFD File Offset: 0x00000DFD
		protected virtual void RenderTrialMessage(HtmlTextWriter writer)
		{
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002BFF File Offset: 0x00000DFF
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			base.RenderContents(writer);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002C10 File Offset: 0x00000E10
		protected void ApplyConditionalRendering(HtmlTextWriter writer)
		{
			RenderMode resolvedRenderMode = this.ResolvedRenderMode;
			if (resolvedRenderMode == RenderMode.Classic)
			{
				this.RenderClassic(writer);
				return;
			}
			if (resolvedRenderMode == RenderMode.Lightweight)
			{
				this.RenderLite(writer);
				return;
			}
			if (resolvedRenderMode == RenderMode.Native)
			{
				this.RenderNative(writer);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002C47 File Offset: 0x00000E47
		protected virtual void RenderClassic(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002C49 File Offset: 0x00000E49
		protected virtual void RenderLite(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002C4B File Offset: 0x00000E4B
		protected virtual void RenderNative(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002C50 File Offset: 0x00000E50
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

		// Token: 0x0600008E RID: 142 RVA: 0x00002CAB File Offset: 0x00000EAB
		protected override void OnInit(EventArgs e)
		{
			if (this.SupportsRenderingMode)
			{
				this.InitializeRenderMode();
			}
			base.OnInit(e);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002CC4 File Offset: 0x00000EC4
		protected virtual void DescribeComponent(IScriptDescriptor descriptor)
		{
			this._describedProperties.Clear();
			if (!this.Enabled)
			{
				descriptor.AddProperty("enabled", this.Enabled);
			}
			if (this.EnableRippleEffect)
			{
				descriptor.AddProperty("_enableRippleEffect", this.EnableRippleEffect);
			}
			this.DescribeClientProperties(descriptor);
			this.DescribeClientEvents(descriptor);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002D26 File Offset: 0x00000F26
		protected void DescribeRenderMode(IScriptDescriptor descriptor)
		{
			if (this.RenderMode != RenderMode.Classic)
			{
				descriptor.AddProperty("_renderMode", this.ResolvedRenderMode);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002D47 File Offset: 0x00000F47
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

		// Token: 0x06000092 RID: 146 RVA: 0x00002D84 File Offset: 0x00000F84
		protected void DescribeIDReferenceProperty(IScriptDescriptor descriptor, string name, string value)
		{
			if (this._describedProperties.ContainsKey(name))
			{
				return;
			}
			this._describedProperties.Add(name, null);
			if (!string.IsNullOrEmpty(value))
			{
				Control control = ((IControlResolver)this).ResolveControl(value);
				string value2 = (control != null) ? control.ClientID : value;
				descriptor.AddProperty(name, value2);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002DD4 File Offset: 0x00000FD4
		protected static void DescribeEvent(IScriptDescriptor descriptor, string name, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			descriptor.AddEvent(name, value);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002DE7 File Offset: 0x00000FE7
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual List<string> GetEmbeddedSkinNames()
		{
			return SkinRegistrar.GetEmbeddedSkinNames(base.GetType());
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002DF4 File Offset: 0x00000FF4
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			this.Described = true;
			return this.GetScriptDescriptors();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002E03 File Offset: 0x00001003
		protected virtual IRenderer CreateControlRenderer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002E0A File Offset: 0x0000100A
		protected virtual IRenderer Renderer
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

		// Token: 0x06000098 RID: 152 RVA: 0x00002E26 File Offset: 0x00001026
		protected virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			return ScriptRegistrar.GetScriptDescriptors(this);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002E2E File Offset: 0x0000102E
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return this.GetScriptReferences();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002E38 File Offset: 0x00001038
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

		// Token: 0x0600009B RID: 155 RVA: 0x00002ECC File Offset: 0x000010CC
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

		// Token: 0x0600009C RID: 156 RVA: 0x00002F8C File Offset: 0x0000118C
		Control IControlResolver.ResolveControl(string controlId)
		{
			return this.FindControl(controlId);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00002F98 File Offset: 0x00001198
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

		// Token: 0x0600009E RID: 158 RVA: 0x00002FD8 File Offset: 0x000011D8
		protected virtual void RaisePostDataChangedEvent()
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002FDA File Offset: 0x000011DA
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002FE4 File Offset: 0x000011E4
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002FEC File Offset: 0x000011EC
		RenderMode ISkinnableControl.PreferredRenderMode(RenderModeBrowserAdaptor browser)
		{
			return this.PreferredRenderMode(browser);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002FF5 File Offset: 0x000011F5
		string ISkinnableControl.GetSkinSuffix()
		{
			return this.GetSkinSuffix();
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002FFD File Offset: 0x000011FD
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003005 File Offset: 0x00001205
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000300E File Offset: 0x0000120E
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00003016 File Offset: 0x00001216
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000301F File Offset: 0x0000121F
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00003027 File Offset: 0x00001227
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

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003030 File Offset: 0x00001230
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003038 File Offset: 0x00001238
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

		// Token: 0x060000AB RID: 171 RVA: 0x00003041 File Offset: 0x00001241
		void IControl.DescribeComponent(IScriptDescriptor descriptor)
		{
			this.DescribeComponent(descriptor);
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000304A File Offset: 0x0000124A
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003052 File Offset: 0x00001252
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

		// Token: 0x060000AE RID: 174 RVA: 0x0000305B File Offset: 0x0000125B
		void IControl.EnsureChildControlsCreated()
		{
			this.EnsureChildControls();
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003063 File Offset: 0x00001263
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000306B File Offset: 0x0000126B
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003074 File Offset: 0x00001274
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000307C File Offset: 0x0000127C
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003085 File Offset: 0x00001285
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000308D File Offset: 0x0000128D
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003096 File Offset: 0x00001296
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000309E File Offset: 0x0000129E
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000030A7 File Offset: 0x000012A7
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000030AF File Offset: 0x000012AF
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000030B8 File Offset: 0x000012B8
		// (set) Token: 0x060000BA RID: 186 RVA: 0x000030C0 File Offset: 0x000012C0
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000030C9 File Offset: 0x000012C9
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000030D1 File Offset: 0x000012D1
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000030DA File Offset: 0x000012DA
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000030E2 File Offset: 0x000012E2
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000030EB File Offset: 0x000012EB
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000030F3 File Offset: 0x000012F3
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000030FC File Offset: 0x000012FC
		[NotifyParentProperty(true)]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003104 File Offset: 0x00001304
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x0000310C File Offset: 0x0000130C
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003115 File Offset: 0x00001315
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x0000311D File Offset: 0x0000131D
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

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003126 File Offset: 0x00001326
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x0000312E File Offset: 0x0000132E
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003137 File Offset: 0x00001337
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x0000313F File Offset: 0x0000133F
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003148 File Offset: 0x00001348
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003150 File Offset: 0x00001350
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003159 File Offset: 0x00001359
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00003161 File Offset: 0x00001361
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

		// Token: 0x060000CE RID: 206 RVA: 0x0000316C File Offset: 0x0000136C
		protected T GetViewStateValue<T>(string key, T defaultValue)
		{
			object obj = this.ViewState[key];
			if (obj == null)
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003194 File Offset: 0x00001394
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

		// Token: 0x060000D0 RID: 208 RVA: 0x000031CC File Offset: 0x000013CC
		protected internal virtual void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			this.DescribeProperty<string>(descriptor, "clientStateFieldID", this.ClientStateFieldID, null);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000031E1 File Offset: 0x000013E1
		protected internal virtual void DescribeClientEvents(IScriptDescriptor descriptor)
		{
		}

		// Token: 0x04000003 RID: 3
		private IRenderer _renderer;

		// Token: 0x04000004 RID: 4
		private ScriptManager _scriptManager;

		// Token: 0x04000005 RID: 5
		private bool _renderModeSet;

		// Token: 0x04000006 RID: 6
		private ClientIDMode _clientIDModeValue = ClientIDMode.AutoID;

		// Token: 0x04000007 RID: 7
		internal Dictionary<string, object> _describedProperties = new Dictionary<string, object>();

		// Token: 0x04000008 RID: 8
		private bool Described;

		// Token: 0x04000009 RID: 9
		private string _ajaxCssRegistrations;
	}
}

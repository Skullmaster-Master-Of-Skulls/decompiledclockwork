using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.Analytics;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x020001FE RID: 510
	[RequiredScript(typeof(Core))]
	[ClientScriptResource("Telerik.Web.UI.RadWebControl", "Telerik.Web.UI.Common.Core.js")]
	public abstract class RadCompositeDataBoundControl : CompositeDataBoundControl, IScriptControl, IControlResolver, IPostBackDataHandler, ISkinnableControl, IControl, IFeatureGroup
	{
		// Token: 0x060011B7 RID: 4535 RVA: 0x0004054D File Offset: 0x0003E74D
		public RadCompositeDataBoundControl()
		{
			this.EnsureLicensing();
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0004056D File Offset: 0x0003E76D
		protected virtual bool IsRenderModeSet
		{
			get
			{
				return this._renderModeSet;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x00040575 File Offset: 0x0003E775
		protected internal virtual bool SupportsRenderingMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x00040578 File Offset: 0x0003E778
		// (set) Token: 0x060011BB RID: 4539 RVA: 0x000405B0 File Offset: 0x0003E7B0
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
				return (string)((this.ViewState["FeatureGroupID"] ?? this.ID) ?? string.Empty);
			}
			set
			{
				this.ViewState["FeatureGroupID"] = value;
			}
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000405C3 File Offset: 0x0003E7C3
		protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x000405CA File Offset: 0x0003E7CA
		// (set) Token: 0x060011BE RID: 4542 RVA: 0x000405EB File Offset: 0x0003E7EB
		[Category("Behavior")]
		[Description("Whether to register with the ScriptManager control on the page")]
		[DefaultValue(true)]
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

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x00040603 File Offset: 0x0003E803
		// (set) Token: 0x060011C0 RID: 4544 RVA: 0x00040632 File Offset: 0x0003E832
		[Category("Appearance")]
		[TypeConverter("Telerik.Web.Design.SkinTypeConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[NotifyParentProperty(true)]
		[Description("Specifies the skin that will be used by the control")]
		[DefaultValue("Default")]
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

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x00040645 File Offset: 0x0003E845
		[Description("Returns true if ripple effect should be added")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual bool EnableRippleEffect
		{
			get
			{
				return this.RuntimeSkin == "Material";
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x00040657 File Offset: 0x0003E857
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

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0004066F File Offset: 0x0003E86F
		// (set) Token: 0x060011C4 RID: 4548 RVA: 0x0004069F File Offset: 0x0003E89F
		[DefaultValue(true)]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("Whether to register the scripts automatically")]
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

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x000406B7 File Offset: 0x0003E8B7
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x000406E7 File Offset: 0x0003E8E7
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Whether to register the selected skin automatically")]
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

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x000406FF File Offset: 0x0003E8FF
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x0004072F File Offset: 0x0003E92F
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("Whether to register the base control skin file automatically")]
		[DefaultValue(true)]
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

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00040747 File Offset: 0x0003E947
		internal string RuntimeSkin
		{
			get
			{
				return SkinRegistrar.GetRuntimeSkin(this);
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0004074F File Offset: 0x0003E94F
		// (set) Token: 0x060011CB RID: 4555 RVA: 0x0004077A File Offset: 0x0003E97A
		[Category("Appearance")]
		[DefaultValue(true)]
		[Description("Whether to register the skin CSS during Ajax requests")]
		[NotifyParentProperty(true)]
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

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x00040792 File Offset: 0x0003E992
		protected internal virtual bool SupportsAdaptiveRendering
		{
			get
			{
				return RenderModesCache.GetAdaptiveTypes().ContainsOrInheritsFromType(base.GetType());
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x000407A4 File Offset: 0x0003E9A4
		protected internal virtual bool SupportsNativeRendering
		{
			get
			{
				return RenderModesCache.GetNativeTypes().ContainsOrInheritsFromType(base.GetType());
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x000407B6 File Offset: 0x0003E9B6
		protected internal virtual bool SupportsLightweightRendering
		{
			get
			{
				return RenderModesCache.GetLightweightTypes().ContainsOrInheritsFromType(base.GetType());
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000407C8 File Offset: 0x0003E9C8
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

		// Token: 0x060011D0 RID: 4560 RVA: 0x000407FD File Offset: 0x0003E9FD
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

		// Token: 0x060011D1 RID: 4561 RVA: 0x00040829 File Offset: 0x0003EA29
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

		// Token: 0x060011D2 RID: 4562 RVA: 0x0004085D File Offset: 0x0003EA5D
		protected virtual void RenderClassic(HtmlTextWriter writer)
		{
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0004085F File Offset: 0x0003EA5F
		protected virtual void RenderLite(HtmlTextWriter writer)
		{
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00040861 File Offset: 0x0003EA61
		protected virtual void RenderNative(HtmlTextWriter writer)
		{
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00040864 File Offset: 0x0003EA64
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

		// Token: 0x060011D6 RID: 4566 RVA: 0x000408BF File Offset: 0x0003EABF
		protected override void OnInit(EventArgs e)
		{
			if (this.SupportsRenderingMode)
			{
				this.InitializeRenderMode();
			}
			base.OnInit(e);
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x000408D6 File Offset: 0x0003EAD6
		// (set) Token: 0x060011D8 RID: 4568 RVA: 0x000408F7 File Offset: 0x0003EAF7
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("Specifies the rendering mode of the control")]
		[DefaultValue(RenderMode.Classic)]
		public RenderMode RenderMode
		{
			get
			{
				return (RenderMode)(this.ViewState["RenderingMode"] ?? RenderMode.Classic);
			}
			set
			{
				this.ViewState["RenderingMode"] = value;
				this._renderModeSet = true;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x00040918 File Offset: 0x0003EB18
		// (set) Token: 0x060011DA RID: 4570 RVA: 0x00040974 File Offset: 0x0003EB74
		[ClientPropertyName("renderMode")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Returns resolved RenderMode should the original value was Auto")]
		[ClientControlProperty]
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

		// Token: 0x060011DB RID: 4571 RVA: 0x0004098C File Offset: 0x0003EB8C
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

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060011DC RID: 4572 RVA: 0x000409CA File Offset: 0x0003EBCA
		[ClientControlProperty]
		[Browsable(false)]
		public string ClientStateFieldID
		{
			get
			{
				return this.ClientID + "_ClientState";
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x000409DC File Offset: 0x0003EBDC
		protected virtual string CssClassFormatString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x000409E3 File Offset: 0x0003EBE3
		protected virtual string DefaultCssClass
		{
			get
			{
				return "Default";
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x000409EA File Offset: 0x0003EBEA
		// (set) Token: 0x060011E0 RID: 4576 RVA: 0x000409F3 File Offset: 0x0003EBF3
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

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x000409F5 File Offset: 0x0003EBF5
		// (set) Token: 0x060011E2 RID: 4578 RVA: 0x000409FD File Offset: 0x0003EBFD
		[DefaultValue(ClientIDMode.AutoID)]
		[Description("This property is overridden in order to support controls which implement INamingContainer")]
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

		// Token: 0x060011E3 RID: 4579 RVA: 0x00040A1C File Offset: 0x0003EC1C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.CssClass;
			this.CssClass = string.Format(this.CssClassFormatString + " " + cssClass, this.RuntimeSkin);
			if (this.CssClass == " ")
			{
				this.CssClass = string.Empty;
			}
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00040A7D File Offset: 0x0003EC7D
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.ControlPreRender();
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00040A8C File Offset: 0x0003EC8C
		protected virtual void ControlPreRender()
		{
			this.ConfigureCombinedScriptFile();
			this.ConfigureCombinedBaseSkinFile();
			this.RegisterScriptControl();
			base.EnsureID();
			if (!string.IsNullOrEmpty(this.Skin))
			{
				this.RegisterCssReferences();
			}
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00040AB9 File Offset: 0x0003ECB9
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this.RegisterWithScriptManager)
			{
				this.ControlPreRender();
				this.EnsureChildControls();
				this.RenderScriptsNoScriptManager(writer);
			}
			base.Render(writer);
			if (!this.RegisterWithScriptManager)
			{
				this.RenderDescriptorsNoScriptManager(writer);
			}
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00040AEC File Offset: 0x0003ECEC
		protected virtual void RenderScriptsNoScriptManager(HtmlTextWriter writer)
		{
			string controlScripts = ControlRenderer.GetControlScripts(this);
			if (!string.IsNullOrEmpty(controlScripts))
			{
				writer.Write("<input type=\"hidden\"/>");
			}
			writer.WriteLine(controlScripts);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00040B1C File Offset: 0x0003ED1C
		protected virtual void RenderDescriptorsNoScriptManager(HtmlTextWriter writer)
		{
			string controlDescriptors = ControlRenderer.GetControlDescriptors(this);
			writer.WriteLine(controlDescriptors);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00040B37 File Offset: 0x0003ED37
		protected virtual void RegisterScriptControl()
		{
			if (this.RegisterWithScriptManager)
			{
				this.ScriptManager.RegisterScriptControl<RadCompositeDataBoundControl>(this);
			}
			this.Page.RegisterRequiresPostBack(this);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00040B5C File Offset: 0x0003ED5C
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

		// Token: 0x060011EB RID: 4587 RVA: 0x00040B86 File Offset: 0x0003ED86
		protected virtual void RegisterScriptDescriptors()
		{
			if (this.RegisterWithScriptManager && !this.Described)
			{
				this.ScriptManager.RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00040BA4 File Offset: 0x0003EDA4
		protected virtual bool LoadClientState(Dictionary<string, object> clientState)
		{
			return false;
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00040BA7 File Offset: 0x0003EDA7
		protected virtual string SaveClientState()
		{
			return null;
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x00040BAA File Offset: 0x0003EDAA
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

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x00040BC6 File Offset: 0x0003EDC6
		protected RadScriptManager RadScriptManager
		{
			get
			{
				return this.ScriptManager as RadScriptManager;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x00040BD3 File Offset: 0x0003EDD3
		protected RadStyleSheetManager RadStyleSheetManager
		{
			get
			{
				return RadStyleSheetManager.GetCurrent(this.Page);
			}
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00040BE0 File Offset: 0x0003EDE0
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

		// Token: 0x060011F2 RID: 4594 RVA: 0x00040C3C File Offset: 0x0003EE3C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			if (!base.DesignMode)
			{
				this.RegisterScriptDescriptors();
				this.RenderClientStateField(writer);
				BaseClass.RenderAjaxCssReferences(this, writer);
			}
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00040C61 File Offset: 0x0003EE61
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
			BaseClass.RenderVersionStamp(writer);
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x00040C70 File Offset: 0x0003EE70
		protected virtual void DescribeComponent(IScriptDescriptor descriptor)
		{
			this._describedProperties.Clear();
			if (this.EnableRippleEffect)
			{
				descriptor.AddProperty("_enableRippleEffect", this.EnableRippleEffect);
			}
			this.DescribeClientProperties(descriptor);
			this.DescribeClientEvents(descriptor);
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00040CA9 File Offset: 0x0003EEA9
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

		// Token: 0x060011F6 RID: 4598 RVA: 0x00040CE3 File Offset: 0x0003EEE3
		protected static void DescribeEvent(IScriptDescriptor descriptor, string name, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			descriptor.AddEvent(name, value);
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00040CF6 File Offset: 0x0003EEF6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual List<string> GetEmbeddedSkinNames()
		{
			return SkinRegistrar.GetEmbeddedSkinNames(base.GetType());
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00040D03 File Offset: 0x0003EF03
		protected void ConfigureCombinedScriptFile()
		{
			if (this.EnableEmbeddedScripts)
			{
				this.EnableEmbeddedScripts = !RadScriptManager.IsCombinedScriptEnabled(this.Page);
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00040D21 File Offset: 0x0003EF21
		protected void ConfigureCombinedBaseSkinFile()
		{
			if (this.EnableEmbeddedBaseStylesheet)
			{
				this.EnableEmbeddedBaseStylesheet = !RadStyleSheetManager.IsCombinedBaseSkinEnabled(this.Page);
			}
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00040D40 File Offset: 0x0003EF40
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

		// Token: 0x060011FB RID: 4603 RVA: 0x00040D7B File Offset: 0x0003EF7B
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			this.Described = true;
			return this.GetScriptDescriptors();
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00040D8A File Offset: 0x0003EF8A
		protected virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			return ScriptRegistrar.GetScriptDescriptors(this);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00040D92 File Offset: 0x0003EF92
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return this.GetScriptReferences();
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00040D9C File Offset: 0x0003EF9C
		protected virtual IEnumerable<ScriptReference> GetScriptReferences()
		{
			List<ScriptReference> list = new List<ScriptReference>();
			if (this.EnableEmbeddedScripts)
			{
				list.AddRange(ScriptRegistrar.GetScriptReferences(this));
			}
			return list;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00040DC4 File Offset: 0x0003EFC4
		Control IControlResolver.ResolveControl(string controlId)
		{
			return this.FindControl(controlId);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00040DD0 File Offset: 0x0003EFD0
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[this.ClientStateFieldID];
			if (!string.IsNullOrEmpty(text))
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				Dictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(text) as Dictionary<string, object>;
				if (dictionary != null)
				{
					return this.LoadClientState(dictionary);
				}
			}
			return false;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00040E11 File Offset: 0x0003F011
		protected virtual void RaisePostDataChangedEvent()
		{
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00040E13 File Offset: 0x0003F013
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x00040E1D File Offset: 0x0003F01D
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x00040E25 File Offset: 0x0003F025
		void IControl.EnsureChildControlsCreated()
		{
			this.EnsureChildControls();
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00040E2D File Offset: 0x0003F02D
		void IControl.DescribeComponent(IScriptDescriptor descriptor)
		{
			this.DescribeComponent(descriptor);
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x00040E36 File Offset: 0x0003F036
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x00040E3E File Offset: 0x0003F03E
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

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x00040E47 File Offset: 0x0003F047
		// (set) Token: 0x06001209 RID: 4617 RVA: 0x00040E4F File Offset: 0x0003F04F
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

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x00040E58 File Offset: 0x0003F058
		// (set) Token: 0x0600120B RID: 4619 RVA: 0x00040E60 File Offset: 0x0003F060
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

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00040E69 File Offset: 0x0003F069
		// (set) Token: 0x0600120D RID: 4621 RVA: 0x00040E71 File Offset: 0x0003F071
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

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x00040E7A File Offset: 0x0003F07A
		// (set) Token: 0x0600120F RID: 4623 RVA: 0x00040E82 File Offset: 0x0003F082
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

		// Token: 0x06001210 RID: 4624 RVA: 0x00040E8B File Offset: 0x0003F08B
		string ISkinnableControl.GetSkinSuffix()
		{
			return this.GetSkinSuffix();
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00040E93 File Offset: 0x0003F093
		RenderMode ISkinnableControl.PreferredRenderMode(RenderModeBrowserAdaptor browser)
		{
			return this.PreferredRenderMode(browser);
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x00040E9C File Offset: 0x0003F09C
		// (set) Token: 0x06001213 RID: 4627 RVA: 0x00040EA4 File Offset: 0x0003F0A4
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

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x00040EAD File Offset: 0x0003F0AD
		// (set) Token: 0x06001215 RID: 4629 RVA: 0x00040EB5 File Offset: 0x0003F0B5
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

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00040EBE File Offset: 0x0003F0BE
		// (set) Token: 0x06001217 RID: 4631 RVA: 0x00040EC6 File Offset: 0x0003F0C6
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

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x00040ECF File Offset: 0x0003F0CF
		// (set) Token: 0x06001219 RID: 4633 RVA: 0x00040ED7 File Offset: 0x0003F0D7
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

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x00040EE0 File Offset: 0x0003F0E0
		// (set) Token: 0x0600121B RID: 4635 RVA: 0x00040EE8 File Offset: 0x0003F0E8
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

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x00040EF1 File Offset: 0x0003F0F1
		// (set) Token: 0x0600121D RID: 4637 RVA: 0x00040EF9 File Offset: 0x0003F0F9
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

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x00040F02 File Offset: 0x0003F102
		// (set) Token: 0x0600121F RID: 4639 RVA: 0x00040F0A File Offset: 0x0003F10A
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

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x00040F13 File Offset: 0x0003F113
		// (set) Token: 0x06001221 RID: 4641 RVA: 0x00040F1B File Offset: 0x0003F11B
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

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00040F24 File Offset: 0x0003F124
		// (set) Token: 0x06001223 RID: 4643 RVA: 0x00040F2C File Offset: 0x0003F12C
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

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00040F35 File Offset: 0x0003F135
		[NotifyParentProperty(true)]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00040F3D File Offset: 0x0003F13D
		// (set) Token: 0x06001226 RID: 4646 RVA: 0x00040F45 File Offset: 0x0003F145
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

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00040F4E File Offset: 0x0003F14E
		// (set) Token: 0x06001228 RID: 4648 RVA: 0x00040F56 File Offset: 0x0003F156
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

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00040F5F File Offset: 0x0003F15F
		// (set) Token: 0x0600122A RID: 4650 RVA: 0x00040F67 File Offset: 0x0003F167
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

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00040F70 File Offset: 0x0003F170
		// (set) Token: 0x0600122C RID: 4652 RVA: 0x00040F78 File Offset: 0x0003F178
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

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x00040F81 File Offset: 0x0003F181
		// (set) Token: 0x0600122E RID: 4654 RVA: 0x00040F89 File Offset: 0x0003F189
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

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x00040F92 File Offset: 0x0003F192
		// (set) Token: 0x06001230 RID: 4656 RVA: 0x00040F9A File Offset: 0x0003F19A
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

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x00040FA3 File Offset: 0x0003F1A3
		// (set) Token: 0x06001232 RID: 4658 RVA: 0x00040FAB File Offset: 0x0003F1AB
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

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x00040FB4 File Offset: 0x0003F1B4
		// (set) Token: 0x06001234 RID: 4660 RVA: 0x00040FBC File Offset: 0x0003F1BC
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

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x00040FC5 File Offset: 0x0003F1C5
		// (set) Token: 0x06001236 RID: 4662 RVA: 0x00040FCD File Offset: 0x0003F1CD
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

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x00040FD6 File Offset: 0x0003F1D6
		// (set) Token: 0x06001238 RID: 4664 RVA: 0x00040FDE File Offset: 0x0003F1DE
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

		// Token: 0x06001239 RID: 4665 RVA: 0x00040FE8 File Offset: 0x0003F1E8
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

		// Token: 0x0600123A RID: 4666 RVA: 0x00041020 File Offset: 0x0003F220
		protected internal virtual void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			this.DescribeProperty<string>(descriptor, "clientStateFieldID", this.ClientStateFieldID, null);
			this.DescribeProperty<RenderMode>(descriptor, "renderMode", this.ResolvedRenderMode, RenderMode.Auto);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00041048 File Offset: 0x0003F248
		protected internal virtual void DescribeClientEvents(IScriptDescriptor descriptor)
		{
		}

		// Token: 0x0400050E RID: 1294
		private ScriptManager _scriptManager;

		// Token: 0x0400050F RID: 1295
		private bool _renderModeSet;

		// Token: 0x04000510 RID: 1296
		private ClientIDMode ClientIDModeValue = ClientIDMode.AutoID;

		// Token: 0x04000511 RID: 1297
		private Dictionary<string, object> _describedProperties = new Dictionary<string, object>();

		// Token: 0x04000512 RID: 1298
		private bool Described;

		// Token: 0x04000513 RID: 1299
		private string _ajaxCssRegistrations;
	}
}

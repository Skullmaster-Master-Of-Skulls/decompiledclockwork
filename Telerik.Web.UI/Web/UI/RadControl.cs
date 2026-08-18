using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02001908 RID: 6408
	[RequiredScript(typeof(Core))]
	[ClientScriptResource("Telerik.Web.UI.RadControl", "Telerik.Web.UI.Common.Core.js")]
	public abstract class RadControl : Control, IScriptControl, IControlResolver, ISkinnableControl, IControl
	{
		// Token: 0x0600F873 RID: 63603 RVA: 0x0038266B File Offset: 0x0038086B
		public RadControl()
		{
			this.EnsureLicensing();
		}

		// Token: 0x17004B10 RID: 19216
		// (get) Token: 0x0600F874 RID: 63604 RVA: 0x00382679 File Offset: 0x00380879
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

		// Token: 0x17004B11 RID: 19217
		// (get) Token: 0x0600F875 RID: 63605 RVA: 0x00382695 File Offset: 0x00380895
		// (set) Token: 0x0600F876 RID: 63606 RVA: 0x003826B6 File Offset: 0x003808B6
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

		// Token: 0x17004B12 RID: 19218
		// (get) Token: 0x0600F877 RID: 63607 RVA: 0x003826CE File Offset: 0x003808CE
		// (set) Token: 0x0600F878 RID: 63608 RVA: 0x003826FD File Offset: 0x003808FD
		[Description("Specifies the skin that will be used by the control")]
		[NotifyParentProperty(true)]
		[DefaultValue("Default")]
		[Category("Appearance")]
		[TypeConverter("Telerik.Web.Design.SkinTypeConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
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

		// Token: 0x17004B13 RID: 19219
		// (get) Token: 0x0600F879 RID: 63609 RVA: 0x00382710 File Offset: 0x00380910
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsSkinSet
		{
			get
			{
				return this.ViewState["Skin"] != null;
			}
		}

		// Token: 0x17004B14 RID: 19220
		// (get) Token: 0x0600F87A RID: 63610 RVA: 0x00382728 File Offset: 0x00380928
		// (set) Token: 0x0600F87B RID: 63611 RVA: 0x00382758 File Offset: 0x00380958
		[Description("Whether to register the scripts automatically")]
		[DefaultValue(true)]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17004B15 RID: 19221
		// (get) Token: 0x0600F87C RID: 63612 RVA: 0x00382770 File Offset: 0x00380970
		// (set) Token: 0x0600F87D RID: 63613 RVA: 0x003827A0 File Offset: 0x003809A0
		[Category("Appearance")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
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

		// Token: 0x17004B16 RID: 19222
		// (get) Token: 0x0600F87E RID: 63614 RVA: 0x003827B8 File Offset: 0x003809B8
		// (set) Token: 0x0600F87F RID: 63615 RVA: 0x003827E8 File Offset: 0x003809E8
		[DefaultValue(true)]
		[Description("Whether to register the base control skin file automatically")]
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

		// Token: 0x17004B17 RID: 19223
		// (get) Token: 0x0600F880 RID: 63616 RVA: 0x00382800 File Offset: 0x00380A00
		protected internal string RuntimeSkin
		{
			get
			{
				return SkinRegistrar.GetRuntimeSkin(this);
			}
		}

		// Token: 0x17004B18 RID: 19224
		// (get) Token: 0x0600F881 RID: 63617 RVA: 0x00382808 File Offset: 0x00380A08
		// (set) Token: 0x0600F882 RID: 63618 RVA: 0x00382833 File Offset: 0x00380A33
		[Description("Whether to register the skin CSS during Ajax requests")]
		[DefaultValue(true)]
		[Category("Appearance")]
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

		// Token: 0x17004B19 RID: 19225
		// (get) Token: 0x0600F883 RID: 63619 RVA: 0x0038284B File Offset: 0x00380A4B
		// (set) Token: 0x0600F884 RID: 63620 RVA: 0x0038286C File Offset: 0x00380A6C
		[Category("Appearance")]
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[Description("Specifies the rendering mode of the control")]
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

		// Token: 0x0600F885 RID: 63621 RVA: 0x0038288C File Offset: 0x00380A8C
		protected internal virtual void InitializeRenderMode()
		{
			if (!this._renderModeSet)
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

		// Token: 0x0600F886 RID: 63622 RVA: 0x003828E7 File Offset: 0x00380AE7
		protected override void OnInit(EventArgs e)
		{
			if (this.SupportsRenderingMode)
			{
				this.InitializeRenderMode();
			}
			base.OnInit(e);
		}

		// Token: 0x17004B1A RID: 19226
		// (get) Token: 0x0600F887 RID: 63623 RVA: 0x00382900 File Offset: 0x00380B00
		// (set) Token: 0x0600F888 RID: 63624 RVA: 0x0038295C File Offset: 0x00380B5C
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

		// Token: 0x0600F889 RID: 63625 RVA: 0x00382974 File Offset: 0x00380B74
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

		// Token: 0x0600F88A RID: 63626 RVA: 0x003829B6 File Offset: 0x00380BB6
		RenderMode ISkinnableControl.PreferredRenderMode(RenderModeBrowserAdaptor browser)
		{
			return this.PreferredRenderMode(browser);
		}

		// Token: 0x0600F88B RID: 63627 RVA: 0x003829BF File Offset: 0x00380BBF
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

		// Token: 0x17004B1B RID: 19227
		// (get) Token: 0x0600F88C RID: 63628 RVA: 0x003829EB File Offset: 0x00380BEB
		protected internal virtual bool SupportsAdaptiveRendering
		{
			get
			{
				return RenderModesCache.GetAdaptiveTypes().ContainsOrInheritsFromType(base.GetType());
			}
		}

		// Token: 0x17004B1C RID: 19228
		// (get) Token: 0x0600F88D RID: 63629 RVA: 0x003829FD File Offset: 0x00380BFD
		protected internal virtual bool SupportsNativeRendering
		{
			get
			{
				return RenderModesCache.GetNativeTypes().ContainsOrInheritsFromType(base.GetType());
			}
		}

		// Token: 0x17004B1D RID: 19229
		// (get) Token: 0x0600F88E RID: 63630 RVA: 0x00382A0F File Offset: 0x00380C0F
		protected internal virtual bool SupportsLightweightRendering
		{
			get
			{
				return RenderModesCache.GetLightweightTypes().ContainsOrInheritsFromType(base.GetType());
			}
		}

		// Token: 0x17004B1E RID: 19230
		// (get) Token: 0x0600F88F RID: 63631 RVA: 0x00382A21 File Offset: 0x00380C21
		protected virtual string CssClassFormatString
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17004B1F RID: 19231
		// (get) Token: 0x0600F890 RID: 63632 RVA: 0x00382A28 File Offset: 0x00380C28
		protected internal virtual bool SupportsRenderingMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004B20 RID: 19232
		// (get) Token: 0x0600F891 RID: 63633 RVA: 0x00382A2B File Offset: 0x00380C2B
		internal virtual string DefaultCssClass
		{
			get
			{
				return "Default";
			}
		}

		// Token: 0x17004B21 RID: 19233
		// (get) Token: 0x0600F892 RID: 63634 RVA: 0x00382A32 File Offset: 0x00380C32
		internal virtual bool ShouldRegisterCssReferences
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600F893 RID: 63635 RVA: 0x00382A35 File Offset: 0x00380C35
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.ControlPreRender();
		}

		// Token: 0x0600F894 RID: 63636 RVA: 0x00382A44 File Offset: 0x00380C44
		protected virtual void ControlPreRender()
		{
			this.RegisterScriptControl();
			base.EnsureID();
			if (this.ShouldRegisterCssReferences)
			{
				this.RegisterCssReferences();
			}
		}

		// Token: 0x0600F895 RID: 63637 RVA: 0x00382A60 File Offset: 0x00380C60
		protected virtual void RegisterScriptControl()
		{
			if (this.RegisterWithScriptManager)
			{
				this.ScriptManager.RegisterScriptControl<RadControl>(this);
				this.Page.RegisterRequiresPostBack(this);
				return;
			}
			this.EnsureChildControls();
			ControlRenderer.EnsureChildControlsAreNotRegistered(this);
		}

		// Token: 0x0600F896 RID: 63638 RVA: 0x00382A90 File Offset: 0x00380C90
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

		// Token: 0x0600F897 RID: 63639 RVA: 0x00382ABA File Offset: 0x00380CBA
		protected virtual void LoadClientState(Dictionary<string, object> clientState)
		{
		}

		// Token: 0x0600F898 RID: 63640 RVA: 0x00382ABC File Offset: 0x00380CBC
		protected virtual string SaveClientState()
		{
			return null;
		}

		// Token: 0x0600F899 RID: 63641 RVA: 0x00382AC0 File Offset: 0x00380CC0
		protected override void Render(HtmlTextWriter writer)
		{
			if (!this.RegisterWithScriptManager)
			{
				this.ControlPreRender();
				this.EnsureChildControls();
				this.RenderScriptsNoScriptManager(writer);
			}
			base.Render(writer);
			BaseClass.RenderAjaxCssReferences(this, writer);
			if (!base.DesignMode)
			{
				this.RegisterScriptDescriptors();
			}
			if (!this.RegisterWithScriptManager)
			{
				this.RenderDescriptorsNoScriptManager(writer);
			}
		}

		// Token: 0x0600F89A RID: 63642 RVA: 0x00382B14 File Offset: 0x00380D14
		protected virtual void RenderScriptsNoScriptManager(HtmlTextWriter writer)
		{
			string controlScripts = ControlRenderer.GetControlScripts(this);
			if (!string.IsNullOrEmpty(controlScripts))
			{
				writer.Write("<input type=\"hidden\"/>");
			}
			writer.WriteLine(controlScripts);
		}

		// Token: 0x0600F89B RID: 63643 RVA: 0x00382B44 File Offset: 0x00380D44
		protected virtual void RenderDescriptorsNoScriptManager(HtmlTextWriter writer)
		{
			string controlDescriptors = ControlRenderer.GetControlDescriptors(this);
			writer.WriteLine(controlDescriptors);
		}

		// Token: 0x0600F89C RID: 63644 RVA: 0x00382B5F File Offset: 0x00380D5F
		protected virtual void RegisterScriptDescriptors()
		{
			if (this.RegisterWithScriptManager && !this.Described)
			{
				this.ScriptManager.RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x0600F89D RID: 63645 RVA: 0x00382B7D File Offset: 0x00380D7D
		protected virtual void DescribeComponent(IScriptDescriptor descriptor)
		{
			ScriptObjectBuilder.DescribeComponent(this, descriptor, this, this);
		}

		// Token: 0x0600F89E RID: 63646 RVA: 0x00382B88 File Offset: 0x00380D88
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual List<string> GetEmbeddedSkinNames()
		{
			return SkinRegistrar.GetEmbeddedSkinNames(base.GetType());
		}

		// Token: 0x0600F89F RID: 63647 RVA: 0x00382B95 File Offset: 0x00380D95
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			this.Described = true;
			return this.GetScriptDescriptors();
		}

		// Token: 0x0600F8A0 RID: 63648 RVA: 0x00382BA4 File Offset: 0x00380DA4
		string ISkinnableControl.GetSkinSuffix()
		{
			string renderingModeString = RenderModeHelper.GetRenderingModeString(this.ResolvedRenderMode);
			if (!(renderingModeString == "Classic"))
			{
				return renderingModeString;
			}
			return string.Empty;
		}

		// Token: 0x0600F8A1 RID: 63649 RVA: 0x00382BD1 File Offset: 0x00380DD1
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

		// Token: 0x0600F8A2 RID: 63650 RVA: 0x00382C06 File Offset: 0x00380E06
		protected virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			return ScriptRegistrar.GetScriptDescriptors(this);
		}

		// Token: 0x0600F8A3 RID: 63651 RVA: 0x00382C0E File Offset: 0x00380E0E
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return this.GetScriptReferences();
		}

		// Token: 0x0600F8A4 RID: 63652 RVA: 0x00382C18 File Offset: 0x00380E18
		protected virtual IEnumerable<ScriptReference> GetScriptReferences()
		{
			List<ScriptReference> list = new List<ScriptReference>();
			if (this.EnableEmbeddedScripts)
			{
				list.AddRange(ScriptRegistrar.GetScriptReferences(this));
			}
			return list;
		}

		// Token: 0x0600F8A5 RID: 63653 RVA: 0x00382C40 File Offset: 0x00380E40
		Control IControlResolver.ResolveControl(string controlId)
		{
			return this.FindControl(controlId);
		}

		// Token: 0x17004B22 RID: 19234
		// (get) Token: 0x0600F8A6 RID: 63654 RVA: 0x00382C49 File Offset: 0x00380E49
		// (set) Token: 0x0600F8A7 RID: 63655 RVA: 0x00382C51 File Offset: 0x00380E51
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

		// Token: 0x17004B23 RID: 19235
		// (get) Token: 0x0600F8A8 RID: 63656 RVA: 0x00382C5A File Offset: 0x00380E5A
		// (set) Token: 0x0600F8A9 RID: 63657 RVA: 0x00382C62 File Offset: 0x00380E62
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

		// Token: 0x17004B24 RID: 19236
		// (get) Token: 0x0600F8AA RID: 63658 RVA: 0x00382C6B File Offset: 0x00380E6B
		// (set) Token: 0x0600F8AB RID: 63659 RVA: 0x00382C73 File Offset: 0x00380E73
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

		// Token: 0x17004B25 RID: 19237
		// (get) Token: 0x0600F8AC RID: 63660 RVA: 0x00382C7C File Offset: 0x00380E7C
		// (set) Token: 0x0600F8AD RID: 63661 RVA: 0x00382C84 File Offset: 0x00380E84
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

		// Token: 0x0600F8AE RID: 63662 RVA: 0x00382C8D File Offset: 0x00380E8D
		void IControl.DescribeComponent(IScriptDescriptor descriptor)
		{
			this.DescribeComponent(descriptor);
		}

		// Token: 0x0600F8AF RID: 63663 RVA: 0x00382C96 File Offset: 0x00380E96
		void IControl.EnsureChildControlsCreated()
		{
			this.EnsureChildControls();
		}

		// Token: 0x17004B26 RID: 19238
		// (get) Token: 0x0600F8B0 RID: 63664 RVA: 0x00382C9E File Offset: 0x00380E9E
		// (set) Token: 0x0600F8B1 RID: 63665 RVA: 0x00382CA6 File Offset: 0x00380EA6
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

		// Token: 0x0600F8B2 RID: 63666 RVA: 0x00382CB0 File Offset: 0x00380EB0
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

		// Token: 0x040046C6 RID: 18118
		private ScriptManager _scriptManager;

		// Token: 0x040046C7 RID: 18119
		private bool _renderModeSet;

		// Token: 0x040046C8 RID: 18120
		private bool Described;

		// Token: 0x040046C9 RID: 18121
		private string _ajaxCssRegistrations;
	}
}

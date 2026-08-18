using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02000456 RID: 1110
	[ParseChildren(true)]
	[LightweightRendering]
	[ClientScriptResource("Telerik.Web.UI.RadDialogOpener", "Telerik.Web.UI.Dialogs.RadDialogOpenerScripts.js")]
	[RequiredScript(typeof(jQueryPlugins))]
	[ToolboxBitmap(typeof(RadDialogOpener), "Telerik.Web.UI.DialogOpener.png")]
	[AdaptiveRendering]
	[ToolboxData("<{0}:RadDialogOpener Runat=server></{0}:RadDialogOpener>")]
	[Description("Telerik RadDialogOpener")]
	[EmbeddedSkin("FormDecorator")]
	[Designer("Telerik.Web.Design.RadDialogOpenerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[PersistChildren(false)]
	[ToolboxItem(false)]
	public class RadDialogOpener : RadWebControl, ISkinnableControl, IControl, INamingContainer
	{
		// Token: 0x060027F3 RID: 10227 RVA: 0x00081AE8 File Offset: 0x0007FCE8
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "additionalQueryString", this.AdditionalQueryString, "");
			base.DescribeProperty<bool>(descriptor, "enableTelerikManagers", this.EnableTelerikManagers, false);
			base.DescribeProperty<string>(descriptor, "handlerUrl", base.ResolveClientUrl(this.HandlerUrl), null);
			base.DescribeProperty<bool>(descriptor, "useClassicDialogs", this.UseClassicDialogs, false);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x00081B52 File Offset: 0x0007FD52
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "close", this.OnClientClose);
			RadWebControl.DescribeEvent(descriptor, "open", this.OnClientOpen);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x060027F5 RID: 10229 RVA: 0x00081B7D File Offset: 0x0007FD7D
		internal override bool ShouldRegisterCssReferences
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x060027F6 RID: 10230 RVA: 0x00081B80 File Offset: 0x0007FD80
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x00081B84 File Offset: 0x0007FD84
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.Window.ID = "Window";
			this.Controls.Add(this.Window);
			this.Window.ShowContentDuringLoad = false;
			this.Window.DestroyOnClose = false;
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x00081BD0 File Offset: 0x0007FDD0
		protected override Style CreateControlStyle()
		{
			Style result = base.CreateControlStyle();
			base.Style.Add("display", "none");
			return result;
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x060027F9 RID: 10233 RVA: 0x00081BFC File Offset: 0x0007FDFC
		private string DialogUniqueID
		{
			get
			{
				if (this.ViewState["DialogUniqueID"] == null)
				{
					this.ViewState["DialogUniqueID"] = Guid.NewGuid().ToString();
				}
				return (string)this.ViewState["DialogUniqueID"];
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x060027FA RID: 10234 RVA: 0x00081C54 File Offset: 0x0007FE54
		public RadWindow Window
		{
			get
			{
				if (this._window == null)
				{
					this._window = new RadWindow
					{
						RenderMode = this.RenderMode
					};
				}
				return this._window;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x060027FB RID: 10235 RVA: 0x00081C88 File Offset: 0x0007FE88
		// (set) Token: 0x060027FC RID: 10236 RVA: 0x00081CA9 File Offset: 0x0007FEA9
		[DefaultValue(false)]
		[Description("When set to True, tells the dialog opener to use RadScriptManager and RadStyleSheetManager when loading an .ascx dialog file.")]
		[ClientControlProperty]
		[Category("Behavior")]
		public bool EnableTelerikManagers
		{
			get
			{
				return (bool)(this.ViewState["EnableTelerikManagers"] ?? false);
			}
			set
			{
				this.ViewState["EnableTelerikManagers"] = value;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x00081CC1 File Offset: 0x0007FEC1
		// (set) Token: 0x060027FE RID: 10238 RVA: 0x00081CE1 File Offset: 0x0007FEE1
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Gets or sets an additional querystring appended to the dialog URL.")]
		[DefaultValue("")]
		public string AdditionalQueryString
		{
			get
			{
				return ((string)this.ViewState["AdditionalQueryString"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["AdditionalQueryString"] = value;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x060027FF RID: 10239 RVA: 0x00081CF4 File Offset: 0x0007FEF4
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Gets the DialogDefinitionDictionary, containing the DialogDefinitions of the managed dialogs.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Behavior")]
		[Bindable(true)]
		public DialogDefinitionDictionary DialogDefinitions
		{
			get
			{
				if (this._dialogDefinitions == null)
				{
					this._dialogDefinitions = new DialogDefinitionDictionary();
				}
				return this._dialogDefinitions;
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06002800 RID: 10240 RVA: 0x00081D10 File Offset: 0x0007FF10
		// (set) Token: 0x06002801 RID: 10241 RVA: 0x00081D3D File Offset: 0x0007FF3D
		[Category("Behavior")]
		[Description("Gets the fully qualified type name of the DialogParametersProvider that the RadDialogOpener uses.")]
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.Attribute)]
		public string DialogParametersProviderTypeName
		{
			get
			{
				string text = this.ViewState["DialogParametersProviderTypeName"] as string;
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["DialogParametersProviderTypeName"] = value;
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06002802 RID: 10242 RVA: 0x00081D50 File Offset: 0x0007FF50
		// (set) Token: 0x06002803 RID: 10243 RVA: 0x00081D70 File Offset: 0x0007FF70
		[Category("Behavior")]
		[Bindable(true)]
		[ClientControlProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Description("Gets or sets the URL which the AJAX call will be made to. Check the help for more information.")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[UrlProperty]
		public string HandlerUrl
		{
			get
			{
				return ((string)this.ViewState["HandlerUrl"]) ?? "Telerik.Web.UI.DialogHandler.aspx";
			}
			set
			{
				this.ViewState["HandlerUrl"] = value;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06002804 RID: 10244 RVA: 0x00081D84 File Offset: 0x0007FF84
		// (set) Token: 0x06002805 RID: 10245 RVA: 0x00081DAD File Offset: 0x0007FFAD
		[Bindable(true)]
		[DefaultValue(false)]
		[ClientControlProperty]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Gets or sets a value, indicating if classic windows will be used for opening a dialog.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool UseClassicDialogs
		{
			get
			{
				object obj = this.ViewState["UseClassicDialogs"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["UseClassicDialogs"] = value;
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06002806 RID: 10246 RVA: 0x00081DC5 File Offset: 0x0007FFC5
		// (set) Token: 0x06002807 RID: 10247 RVA: 0x00081DF4 File Offset: 0x0007FFF4
		[Browsable(true)]
		[Obsolete("This property is not used. Set the language from the dialog parameters.")]
		[Bindable(true)]
		[Description("Gets or sets the localization language for the user interface.")]
		[DefaultValue("en-US")]
		[Category("Appearance")]
		public string Language
		{
			get
			{
				if (this.ViewState["Language"] != null)
				{
					return (string)this.ViewState["Language"];
				}
				return "en-US";
			}
			set
			{
				this.ViewState["Language"] = value;
			}
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x00081E07 File Offset: 0x00080007
		// (set) Token: 0x06002809 RID: 10249 RVA: 0x00081E0F File Offset: 0x0008000F
		[TypeConverter("Telerik.Web.Design.SkinTypeConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("Specifies the skin that will be used by the control")]
		[DefaultValue("Default")]
		public override string Skin
		{
			get
			{
				return base.Skin;
			}
			set
			{
				this.Window.Skin = value;
				base.Skin = value;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x0600280A RID: 10250 RVA: 0x00081E24 File Offset: 0x00080024
		// (set) Token: 0x0600280B RID: 10251 RVA: 0x00081E27 File Offset: 0x00080027
		bool ISkinnableControl.EnableEmbeddedBaseStylesheet
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x0600280C RID: 10252 RVA: 0x00081E29 File Offset: 0x00080029
		// (set) Token: 0x0600280D RID: 10253 RVA: 0x00081E31 File Offset: 0x00080031
		[Description("Whether to register the selected skin automatically")]
		[Category("Appearance")]
		[DefaultValue(true)]
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return base.EnableEmbeddedBaseStylesheet;
			}
			set
			{
				this.Window.EnableEmbeddedBaseStylesheet = value;
				base.EnableEmbeddedBaseStylesheet = value;
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x0600280E RID: 10254 RVA: 0x00081E46 File Offset: 0x00080046
		// (set) Token: 0x0600280F RID: 10255 RVA: 0x00081E4E File Offset: 0x0008004E
		[Category("Appearance")]
		[Description("Whether to register the selected skin automatically")]
		[DefaultValue(true)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return base.EnableEmbeddedSkins;
			}
			set
			{
				this.Window.EnableEmbeddedSkins = value;
				base.EnableEmbeddedSkins = value;
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x00081E63 File Offset: 0x00080063
		// (set) Token: 0x06002811 RID: 10257 RVA: 0x00081E6B File Offset: 0x0008006B
		[DefaultValue(true)]
		[Category("Appearance")]
		[Description("Whether to register the embedded scripts automatically")]
		public override bool EnableEmbeddedScripts
		{
			get
			{
				return base.EnableEmbeddedScripts;
			}
			set
			{
				this.Window.EnableEmbeddedScripts = value;
				base.EnableEmbeddedScripts = value;
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06002812 RID: 10258 RVA: 0x00081E80 File Offset: 0x00080080
		// (set) Token: 0x06002813 RID: 10259 RVA: 0x00081E88 File Offset: 0x00080088
		public override bool EnableAjaxSkinRendering
		{
			get
			{
				return base.EnableAjaxSkinRendering;
			}
			set
			{
				this.Window.EnableAjaxSkinRendering = value;
				base.EnableAjaxSkinRendering = value;
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x00081E9D File Offset: 0x0008009D
		// (set) Token: 0x06002815 RID: 10261 RVA: 0x00081EBD File Offset: 0x000800BD
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("open")]
		public string OnClientOpen
		{
			get
			{
				return ((string)this.ViewState["OnClientOpen"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientOpen"] = value;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x00081ED0 File Offset: 0x000800D0
		// (set) Token: 0x06002817 RID: 10263 RVA: 0x00081EF0 File Offset: 0x000800F0
		[ClientPropertyName("close")]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientClose
		{
			get
			{
				return ((string)this.ViewState["OnClientClose"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientClose"] = value;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06002818 RID: 10264 RVA: 0x00081F03 File Offset: 0x00080103
		// (set) Token: 0x06002819 RID: 10265 RVA: 0x00081F23 File Offset: 0x00080123
		[DefaultValue("")]
		[Category("Appearance")]
		[UrlProperty("*.css")]
		public string DialogsCssFile
		{
			get
			{
				return ((string)this.ViewState["DialogsCssFile"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DialogsCssFile"] = value;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x0600281A RID: 10266 RVA: 0x00081F36 File Offset: 0x00080136
		// (set) Token: 0x0600281B RID: 10267 RVA: 0x00081F56 File Offset: 0x00080156
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		[DefaultValue("")]
		[UrlProperty("*.js")]
		[Category("Behavior")]
		public string DialogsScriptFile
		{
			get
			{
				return ((string)this.ViewState["DialogsScriptFile"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DialogsScriptFile"] = value;
			}
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x00081F6C File Offset: 0x0008016C
		protected string GetEncryptedProviderTypeName()
		{
			HmacEnabledCryptoService service = DialogHashService.GetService();
			return service.Encrypt(this.DialogParametersProviderTypeName);
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x0600281D RID: 10269 RVA: 0x00081F8B File Offset: 0x0008018B
		// (set) Token: 0x0600281E RID: 10270 RVA: 0x00081F98 File Offset: 0x00080198
		[Category("Behavior")]
		[DefaultValue(WindowAnimation.None)]
		public WindowAnimation Animation
		{
			get
			{
				return this.Window.Animation;
			}
			set
			{
				this.Window.Animation = value;
			}
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x00081FA6 File Offset: 0x000801A6
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.EnsureChildControls();
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x00081FB8 File Offset: 0x000801B8
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (!string.IsNullOrEmpty(this.DialogParametersProviderTypeName))
			{
				this.GetDialogParametersProvider().StoreAllParameters(this.DialogUniqueID, this.DialogDefinitions.AllDialogParameters);
				descriptor.AddProperty("_dialogParametersProviderTypeName", this.GetEncryptedProviderTypeName());
			}
			if (!string.IsNullOrEmpty(this.DialogsCssFile))
			{
				foreach (string key in this.DialogDefinitions.Keys)
				{
					this.DialogDefinitions[key].Parameters["DialogsCssFile"] = this.DialogsCssFile;
				}
			}
			if (!string.IsNullOrEmpty(this.DialogsScriptFile))
			{
				foreach (string key2 in this.DialogDefinitions.Keys)
				{
					this.DialogDefinitions[key2].Parameters["DialogsScriptFile"] = this.DialogsScriptFile;
				}
			}
			if (this.EnableTelerikManagers)
			{
				this.SerializeManagerParameters();
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new DialogDefinitionConverter(string.IsNullOrEmpty(this.DialogParametersProviderTypeName))
			});
			descriptor.AddScriptProperty("_dialogDefinitions", javaScriptSerializer.Serialize(this.DialogDefinitions));
			descriptor.AddProperty("_dialogUniqueID", this.DialogUniqueID);
			descriptor.AddProperty("skin", base.RuntimeSkin);
			descriptor.AddComponentProperty("container", this.Window.ClientID);
			descriptor.AddProperty("_renderMode", this.ResolvedRenderMode);
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x00082184 File Offset: 0x00080384
		private DialogParametersProvider GetDialogParametersProvider()
		{
			return (DialogParametersProvider)Activator.CreateInstance(Type.GetType(this.DialogParametersProviderTypeName), new object[]
			{
				this.Page
			});
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000821B8 File Offset: 0x000803B8
		private void SerializeManagerParameters()
		{
			if (this.Page == null)
			{
				return;
			}
			ScriptManager current = ScriptManager.GetCurrent(this.Page);
			RadScriptManager radScriptManager = current as RadScriptManager;
			if (current != null && radScriptManager != null)
			{
				string value = radScriptManager.SerializeScriptManagerProperties();
				foreach (string key in this.DialogDefinitions.Keys)
				{
					this.DialogDefinitions[key].Parameters["ScriptManagerProperties"] = value;
				}
			}
			RadStyleSheetManager current2 = RadStyleSheetManager.GetCurrent(this.Page);
			if (current2 != null)
			{
				string value2 = current2.SerializeStyleSheetManagerProperties();
				foreach (string key2 in this.DialogDefinitions.Keys)
				{
					this.DialogDefinitions[key2].Parameters["StyleManagerProperties"] = value2;
				}
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06002823 RID: 10275 RVA: 0x000822CC File Offset: 0x000804CC
		// (set) Token: 0x06002824 RID: 10276 RVA: 0x000822D4 File Offset: 0x000804D4
		[NotifyParentProperty(true)]
		[DefaultValue(RenderMode.Classic)]
		[Category("Appearance")]
		[Description("Specifies the rendering mode of the control")]
		public override RenderMode RenderMode
		{
			get
			{
				return base.RenderMode;
			}
			set
			{
				base.RenderMode = value;
				if (base.ChildControlsCreated)
				{
					this.SetRenderModeChildRadControls();
				}
			}
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x000822EC File Offset: 0x000804EC
		private void SetRenderModeChildRadControls()
		{
			if (this._window != null)
			{
				this._window.RenderMode = this.RenderMode;
				if (this.RenderMode == RenderMode.Mobile)
				{
					this._window.InitialBehaviors |= WindowBehaviors.Maximize;
					this._window.CssClass = RadDialogOpener.MobileDialogCssClass;
					this._window.VisibleTitlebar = false;
				}
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06002826 RID: 10278 RVA: 0x0008234B File Offset: 0x0008054B
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000A28 RID: 2600
		private DialogDefinitionDictionary _dialogDefinitions;

		// Token: 0x04000A29 RID: 2601
		private RadWindow _window;

		// Token: 0x04000A2A RID: 2602
		internal static readonly string MobileDialogCssClass = "reMobileDialog";
	}
}

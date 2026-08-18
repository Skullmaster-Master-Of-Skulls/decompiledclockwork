using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI.Spell.Localization;

namespace Telerik.Web.UI.Spell
{
	// Token: 0x02001B50 RID: 6992
	[ClientScriptResource("Telerik.Web.UI.Spell.SpellDialog", "Telerik.Web.UI.Spell.SpellDialog.js")]
	[ToolboxItem(false)]
	[EmbeddedSkin("Spell")]
	[LightweightRendering]
	[RequiredScript(typeof(SpellCheckService))]
	[RequiredScript(typeof(jQueryPlugins))]
	public class SpellDialog : DialogControl, ILocalizableControl, IClientParameterConsumer, INamingContainer
	{
		// Token: 0x170052B8 RID: 21176
		// (get) Token: 0x06010EFB RID: 69371 RVA: 0x003BFDF1 File Offset: 0x003BDFF1
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170052B9 RID: 21177
		// (get) Token: 0x06010EFC RID: 69372 RVA: 0x003BFDF4 File Offset: 0x003BDFF4
		private SpellDialogParameters SpellDialogParameters
		{
			get
			{
				return this._spellDialogParameters;
			}
		}

		// Token: 0x170052BA RID: 21178
		// (get) Token: 0x06010EFD RID: 69373 RVA: 0x003BFDFC File Offset: 0x003BDFFC
		[Description("Holds the Spell Localization strings for the RadSpell dialog (loaded from RadSpell.Dialog.resx).")]
		public SpellDialogStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new SpellDialogStrings(new LocalizationProvider("RadSpell.Dialog", this, this.LocalizationPath));
				}
				return this._localization;
			}
		}

		// Token: 0x170052BB RID: 21179
		// (get) Token: 0x06010EFE RID: 69374 RVA: 0x003BFE28 File Offset: 0x003BE028
		internal override bool ShouldRegisterCssReferences
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170052BC RID: 21180
		// (get) Token: 0x06010EFF RID: 69375 RVA: 0x003BFE2B File Offset: 0x003BE02B
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170052BD RID: 21181
		// (get) Token: 0x06010F00 RID: 69376 RVA: 0x003BFE2E File Offset: 0x003BE02E
		// (set) Token: 0x06010F01 RID: 69377 RVA: 0x003BFE4E File Offset: 0x003BE04E
		[Description("Gets or sets a string containing the localization path for the .resx RadSpell files.")]
		[Category("Appearance")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["LocalizationPath"] = value;
			}
		}

		// Token: 0x170052BE RID: 21182
		// (get) Token: 0x06010F02 RID: 69378 RVA: 0x003BFE61 File Offset: 0x003BE061
		// (set) Token: 0x06010F03 RID: 69379 RVA: 0x003BFE86 File Offset: 0x003BE086
		[Category("Appearance")]
		[DefaultValue("en-US")]
		[Description("Gets or sets a string containing the localization language for the RadSpell Dialog.")]
		public string Language
		{
			get
			{
				return ((string)this.ViewState["Language"]) ?? CultureInfo.CurrentUICulture.Name;
			}
			set
			{
				this.ViewState["Language"] = value;
				this._culture = ((value == null) ? null : CultureInfo.GetCultureInfo(value));
			}
		}

		// Token: 0x170052BF RID: 21183
		// (get) Token: 0x06010F04 RID: 69380 RVA: 0x003BFEAB File Offset: 0x003BE0AB
		CultureInfo ILocalizableControl.Culture
		{
			get
			{
				return this._culture;
			}
		}

		// Token: 0x06010F05 RID: 69381 RVA: 0x003BFEB4 File Offset: 0x003BE0B4
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this._spellDialogParameters = new SpellDialogParameters(base.DialogParameters);
			this.LocalizationPath = (string)base.DialogParameters["LocalizationPath"];
			this.Language = (string)base.DialogParameters["Language"];
			this.EnableEmbeddedSkins = (bool)base.DialogParameters["EnableEmbeddedSkins"];
			this.EnableEmbeddedBaseStylesheet = (bool)base.DialogParameters["EnableEmbeddedBaseStylesheet"];
			this.RenderMode = (RenderMode)base.DialogParameters["RenderMode"];
		}

		// Token: 0x06010F06 RID: 69382 RVA: 0x003BFF60 File Offset: 0x003BE160
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			this.SpellDialogParameters.DictionaryPath = this.GetDictionaryPath(this.SpellDialogParameters.DictionaryPath);
			descriptor.AddProperty("serviceUrl", this.SpellDialogParameters.AjaxUrl);
			this.SpellDialogParameters.AjaxUrl = this.GetAjaxUrl(this.SpellDialogParameters.AjaxUrl);
			descriptor.AddProperty("serviceConfiguration", this.SpellDialogParameters.Serialize());
			descriptor.AddProperty("localization", this.Localization);
		}

		// Token: 0x06010F07 RID: 69383 RVA: 0x003BFFEC File Offset: 0x003BE1EC
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.Controls.Add(this.CreateButtonsSection());
			this.Controls.Add(this.CreateInnerTitle("NotInDictionary", SpellDialog.textAreaID));
			this.Controls.Add(this.CreateTextDisplayerDiv());
			this.Controls.Add(SpellDialog.CreateTextDisplayerTextArea());
			this.Controls.Add(this.CreateInnerTitle("Suggestions", SpellDialog.suggestionsID));
			this.Controls.Add(SpellDialog.CreateSuggestionsList());
			this.Controls.Add(this.CreateUndoCancelButtonsSection());
			this.Controls.Add(this.CreateRadFormDecorator());
		}

		// Token: 0x06010F08 RID: 69384 RVA: 0x003C0099 File Offset: 0x003BE299
		protected override void OnPreRender(EventArgs e)
		{
			this.CssClass = this.GetSkinRelativeStyleName("RadSpell RadSpell_{0}");
			base.OnPreRender(e);
		}

		// Token: 0x06010F09 RID: 69385 RVA: 0x003C00B3 File Offset: 0x003BE2B3
		private string GetSkinRelativeStyleName(string style)
		{
			return string.Format(style, base.RuntimeSkin);
		}

		// Token: 0x06010F0A RID: 69386 RVA: 0x003C00C1 File Offset: 0x003BE2C1
		private string GetAjaxUrl(string ajaxUrl)
		{
			if (string.IsNullOrEmpty(ajaxUrl))
			{
				ajaxUrl = "Telerik.Web.UI.SpellCheckHandler.axd";
			}
			return this.Page.ResolveUrl(ajaxUrl);
		}

		// Token: 0x06010F0B RID: 69387 RVA: 0x003C00E0 File Offset: 0x003BE2E0
		private string GetDictionaryPath(string dictionaryPath1)
		{
			string text = dictionaryPath1;
			if (string.IsNullOrEmpty(text))
			{
				text = "~/App_Data/RadSpell/";
			}
			if (!text.StartsWith("\\\\", StringComparison.OrdinalIgnoreCase) && text.IndexOf(":", StringComparison.OrdinalIgnoreCase) != 1)
			{
				text = this.Context.Request.MapPath(text);
			}
			return text;
		}

		// Token: 0x06010F0C RID: 69388 RVA: 0x003C0130 File Offset: 0x003BE330
		private HtmlControl CreateListItemButton(string localizationKey)
		{
			HtmlControl child = this.CreateButton(localizationKey);
			return new HtmlGenericControl("li")
			{
				Controls = 
				{
					child
				}
			};
		}

		// Token: 0x06010F0D RID: 69389 RVA: 0x003C0160 File Offset: 0x003BE360
		private HtmlControl CreateButton(string localizationKey)
		{
			HtmlButton htmlButton = new HtmlButton();
			htmlButton.ID = localizationKey;
			htmlButton.InnerHtml = this.Localization.GetString(localizationKey);
			htmlButton.Style.Add(HtmlTextWriterStyle.Width, "100%");
			htmlButton.Attributes.Add("onclick", "return false;");
			return htmlButton;
		}

		// Token: 0x06010F0E RID: 69390 RVA: 0x003C01B4 File Offset: 0x003BE3B4
		private HtmlControl CreateButtonsSection()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("ul");
			htmlGenericControl.Attributes["class"] = "SpellOptions AuxOptions";
			htmlGenericControl.Style.Add(HtmlTextWriterStyle.MarginTop, "50px");
			htmlGenericControl.Controls.Add(this.CreateListItemButton("Ignore"));
			htmlGenericControl.Controls.Add(this.CreateListItemButton("IgnoreAll"));
			if (this.SpellDialogParameters.AllowAddCustom)
			{
				htmlGenericControl.Controls.Add(this.CreateListItemButton("AddCustom"));
			}
			htmlGenericControl.Controls.Add(this.CreateListItemButton("Change"));
			htmlGenericControl.Controls.Add(this.CreateListItemButton("ChangeAll"));
			return htmlGenericControl;
		}

		// Token: 0x06010F0F RID: 69391 RVA: 0x003C0270 File Offset: 0x003BE470
		private HtmlControl CreateDialogTitle()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("h3");
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("em");
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("span");
			htmlGenericControl2.Controls.Add(htmlGenericControl3);
			htmlGenericControl3.InnerHtml = this.Localization.GetString("Title");
			return htmlGenericControl;
		}

		// Token: 0x06010F10 RID: 69392 RVA: 0x003C02D0 File Offset: 0x003BE4D0
		private HtmlControl CreateInnerTitle(string localizationKey, string labelFor)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("h4");
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("label");
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			htmlGenericControl2.Attributes.Add("for", ChildControlHelper.GetChildElementId(this, labelFor));
			htmlGenericControl2.InnerHtml = this.Localization.GetString(localizationKey);
			return htmlGenericControl;
		}

		// Token: 0x06010F11 RID: 69393 RVA: 0x003C032C File Offset: 0x003BE52C
		private HtmlControl CreateTextDisplayerDiv()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.ID = "TextContainer";
			htmlGenericControl.InnerHtml = this.Localization.GetString("ProgressMessage");
			htmlGenericControl.Attributes["class"] = "RichTextView";
			htmlGenericControl.Attributes["tabIndex"] = "1";
			htmlGenericControl.Style.Add(HtmlTextWriterStyle.Height, "100px");
			return htmlGenericControl;
		}

		// Token: 0x06010F12 RID: 69394 RVA: 0x003C03A4 File Offset: 0x003BE5A4
		private static HtmlControl CreateTextDisplayerTextArea()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("textarea");
			htmlGenericControl.ID = SpellDialog.textAreaID;
			htmlGenericControl.Attributes["tabIndex"] = "1";
			htmlGenericControl.Style.Add(HtmlTextWriterStyle.Height, "100px");
			return htmlGenericControl;
		}

		// Token: 0x06010F13 RID: 69395 RVA: 0x003C03F0 File Offset: 0x003BE5F0
		private static HtmlControl CreateSuggestionsList()
		{
			HtmlSelect htmlSelect = new HtmlSelect();
			htmlSelect.ID = SpellDialog.suggestionsID;
			htmlSelect.Size = 9;
			htmlSelect.Attributes["tabIndex"] = "2";
			htmlSelect.Style.Add(HtmlTextWriterStyle.Height, "100px");
			return htmlSelect;
		}

		// Token: 0x06010F14 RID: 69396 RVA: 0x003C0440 File Offset: 0x003BE640
		private HtmlControl CreateUndoCancelButtonsSection()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("p");
			htmlGenericControl.Attributes["class"] = "SpellOptions MainOptions";
			htmlGenericControl.Controls.Add(this.CreateButton("Undo"));
			htmlGenericControl.Controls.Add(this.CreateButton("Cancel"));
			return htmlGenericControl;
		}

		// Token: 0x06010F15 RID: 69397 RVA: 0x003C049C File Offset: 0x003BE69C
		private RadFormDecorator CreateRadFormDecorator()
		{
			return new RadFormDecorator
			{
				DecoratedControls = (FormDecoratorDecoratedControls.CheckBoxes | FormDecoratorDecoratedControls.RadioButtons | FormDecoratorDecoratedControls.Buttons | FormDecoratorDecoratedControls.Scrollbars | FormDecoratorDecoratedControls.Select),
				ID = "spellDecorator",
				EnableEmbeddedBaseStylesheet = (bool)base.DialogParameters["EnableEmbeddedBaseStylesheet"],
				EnableEmbeddedSkins = (bool)base.DialogParameters["EnableEmbeddedSkins"],
				EnableEmbeddedScripts = this.EnableEmbeddedScripts,
				Skin = base.RuntimeSkin,
				RenderMode = this.RenderMode
			};
		}

		// Token: 0x04004BA7 RID: 19367
		private SpellDialogParameters _spellDialogParameters;

		// Token: 0x04004BA8 RID: 19368
		private SpellDialogStrings _localization;

		// Token: 0x04004BA9 RID: 19369
		private CultureInfo _culture;

		// Token: 0x04004BAA RID: 19370
		private static readonly string textAreaID = "TextEditor";

		// Token: 0x04004BAB RID: 19371
		private static readonly string suggestionsID = "Suggestions";
	}
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.Design;
using Telerik.Web.UI.Spell;

namespace Telerik.Web.UI
{
	// Token: 0x0200088E RID: 2190
	[ClientScriptResource("Telerik.Web.UI.RadSpell", "Telerik.Web.UI.Spell.RadSpell.js")]
	[ToolboxBitmap(typeof(RadSpell), "Telerik.Web.UI.Spell.png")]
	[EmbeddedSkin("Spell")]
	[EmbeddedSkin("Spell", "Default")]
	[TelerikToolboxCategory("Miscellaneous")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[Designer("Telerik.Web.Design.RadSpellDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ValidationProperty("SpellChecked")]
	[Description("Telerik RadSpell")]
	[LightweightRendering]
	[ToolboxData("<{0}:RadSpell Runat=server></{0}:RadSpell>")]
	public class RadSpell : RadWebControl, ISkinnableControl, IControl, INamingContainer
	{
		// Token: 0x17001A89 RID: 6793
		// (get) Token: 0x060050FC RID: 20732 RVA: 0x000FC3F3 File Offset: 0x000FA5F3
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001A8A RID: 6794
		// (get) Token: 0x060050FD RID: 20733 RVA: 0x000FC3F6 File Offset: 0x000FA5F6
		// (set) Token: 0x060050FE RID: 20734 RVA: 0x000FC3FE File Offset: 0x000FA5FE
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[Description("Specifies the rendering mode of the control")]
		[DefaultValue(RenderMode.Classic)]
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
					this.SetSpellDialogSize();
				}
			}
		}

		// Token: 0x060050FF RID: 20735 RVA: 0x000FC41B File Offset: 0x000FA61B
		private void SetRenderModeChildRadControls()
		{
			this._dialogOpener.RenderMode = this.RenderMode;
			this._spellDialogDefinition.Parameters["RenderMode"] = this.RenderMode;
		}

		// Token: 0x06005100 RID: 20736 RVA: 0x000FC44E File Offset: 0x000FA64E
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "clientTextSource", this.ClientTextSource, "");
			base.DescribeProperty<string>(descriptor, "dictionaryLanguage", this.DictionaryLanguage, "en-US");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x000FC488 File Offset: 0x000FA688
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "clientCheckCancelled", this.OnClientCheckCancelled);
			RadWebControl.DescribeEvent(descriptor, "clientCheckFinished", this.OnClientCheckFinished);
			RadWebControl.DescribeEvent(descriptor, "clientCheckStarted", this.OnClientCheckStarted);
			RadWebControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06005102 RID: 20738 RVA: 0x000FC4E0 File Offset: 0x000FA6E0
		private void ResetDialogDefinition()
		{
			if (!string.IsNullOrEmpty(this.DialogTypeName))
			{
				this._spellDialogDefinition = new DialogDefinition(Type.GetType(this.DialogTypeName), new DialogParameters());
			}
			else if (!string.IsNullOrEmpty(this.DialogVirtualPath))
			{
				this._spellDialogDefinition = new DialogDefinition(this.DialogVirtualPath, new DialogParameters());
			}
			else
			{
				this._spellDialogDefinition = new DialogDefinition(typeof(SpellDialog), new DialogParameters());
			}
			this._spellDialogDefinition.Behaviors = (WindowBehaviors.Close | WindowBehaviors.Move);
			this._spellDialogDefinition.Modal = true;
			this._spellDialogDefinition.VisibleTitlebar = true;
			this._spellDialogDefinition.VisibleStatusbar = false;
			this._spellDialogDefinition.Parameters["AjaxUrl"] = this.AjaxUrl;
			this._spellDialogDefinition.Parameters["AllowAddCustom"] = this.AllowAddCustom;
			this._spellDialogDefinition.Parameters["CustomDictionarySourceTypeName"] = this.CustomDictionarySourceTypeName;
			this._spellDialogDefinition.Parameters["CustomDictionarySuffix"] = this.CustomDictionarySuffix;
			this._spellDialogDefinition.Parameters["DictionaryLanguage"] = this.DictionaryLanguage;
			this._spellDialogDefinition.Parameters["Language"] = this.Language;
			this._spellDialogDefinition.Parameters["DictionaryPath"] = this.DictionaryPath;
			this._spellDialogDefinition.Parameters["EditDistance"] = this.EditDistance;
			this._spellDialogDefinition.Parameters["FragmentIgnoreOptions"] = this.FragmentIgnoreOptions;
			this._spellDialogDefinition.Parameters["SpellCheckProviderTypeName"] = this.SpellCheckProviderTypeName;
			this._spellDialogDefinition.Parameters["SpellCheckProvider"] = this.SpellCheckProvider;
			this._spellDialogDefinition.Parameters["WordIgnoreOptions"] = this.WordIgnoreOptions;
			this._spellDialogDefinition.Parameters["LocalizationPath"] = this.LocalizationPath;
			this._spellDialogDefinition.Parameters["RenderMode"] = this.RenderMode;
			this._spellDialogDefinition.Parameters["EnableEmbeddedSkins"] = this._dialogOpener.EnableEmbeddedSkins;
			this._spellDialogDefinition.Parameters["EnableEmbeddedBaseStylesheet"] = this._dialogOpener.EnableEmbeddedBaseStylesheet;
			this._dialogOpener.DialogDefinitions["SpellCheckDialog"] = this._spellDialogDefinition;
		}

		// Token: 0x06005103 RID: 20739 RVA: 0x000FC784 File Offset: 0x000FA984
		private void SetSpellDialogSize()
		{
			if (this.ResolvedRenderMode == RenderMode.Classic)
			{
				this._spellDialogDefinition.Width = 488;
				this._spellDialogDefinition.Height = 361;
				return;
			}
			string[] array = new string[]
			{
				"MetroTouch",
				"BlackMetroTouch",
				"Bootstrap",
				"Material"
			};
			if (Array.IndexOf<string>(array, base.RuntimeSkin) > -1)
			{
				this._spellDialogDefinition.Width = 505;
				this._spellDialogDefinition.Height = 390;
				return;
			}
			this._spellDialogDefinition.Width = 500;
			this._spellDialogDefinition.Height = 400;
		}

		// Token: 0x06005104 RID: 20740 RVA: 0x000FC854 File Offset: 0x000FAA54
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string a = postCollection[this.UniqueID];
			this._spellChecked = (a == "true");
			return base.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x000FC888 File Offset: 0x000FAA88
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddComponentProperty("dialogOpener", this._dialogOpener.ClientID);
			if (this.ControlsToCheck.Length > 0)
			{
				List<string> list = new List<string>();
				foreach (string controlId in this.ControlsToCheck)
				{
					list.Add(this.GetTargetControlClientID(controlId));
				}
				descriptor.AddProperty("controlsToCheck", list);
				return;
			}
			descriptor.AddProperty("_controlToCheck", this.GetTargetControlClientID(this.ControlToCheck));
		}

		// Token: 0x06005106 RID: 20742 RVA: 0x000FC910 File Offset: 0x000FAB10
		private string GetTargetControlClientID(string controlId)
		{
			string text = controlId;
			if (!string.IsNullOrEmpty(text) && !this.IsClientID)
			{
				Control control = this.NamingContainer.FindControl(controlId);
				if (control == null)
				{
					throw new ArgumentNullException(string.Format("Cannot find a server control with ID={0}. If you need to specify a client-side element ID, please set IsClientID to true.", controlId));
				}
				text = control.ClientID;
			}
			return text;
		}

		// Token: 0x06005107 RID: 20743 RVA: 0x000FC958 File Offset: 0x000FAB58
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				this.EnsureChildControls();
			}
			else
			{
				this.RenderSpellCheckedField(writer);
			}
			base.RenderContents(writer);
		}

		// Token: 0x06005108 RID: 20744 RVA: 0x000FC978 File Offset: 0x000FAB78
		private void RenderSpellCheckedField(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "SpellChecked");
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x000FC9C6 File Offset: 0x000FABC6
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			if (base.IsSkinSet)
			{
				this._dialogOpener.Skin = base.RuntimeSkin;
			}
			this.SetSpellDialogSize();
		}

		// Token: 0x0600510A RID: 20746 RVA: 0x000FC9ED File Offset: 0x000FABED
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.EnsureChildControls();
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x000FC9FC File Offset: 0x000FABFC
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this._dialogOpener == null)
			{
				this._dialogOpener = new RadDialogOpener();
				this._dialogOpener.ID = "dialogOpener";
				this._dialogOpener.EnableEmbeddedScripts = this.EnableEmbeddedScripts;
				this._dialogOpener.EnableAjaxSkinRendering = this.EnableAjaxSkinRendering;
				this._dialogOpener.RenderMode = this.RenderMode;
				this._dialogOpener.Window.EnableShadow = true;
				this._dialogOpener.Window.RenderMode = this.RenderMode;
				this.ResetDialogDefinition();
			}
			this.Controls.Add(this._dialogOpener);
			Control control = this.CreateLanguageDropdown();
			if (control != null)
			{
				this.Controls.Add(control);
			}
			Control control2 = this.CreateSpellCheckButton();
			if (this.TabIndex != 0)
			{
				(control2 as HtmlControl).Attributes["tabindex"] = this.TabIndex.ToString();
			}
			if (control2 != null)
			{
				this.Controls.Add(control2);
			}
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x000FCAFC File Offset: 0x000FACFC
		private Control CreateLanguageDropdown()
		{
			string[] supportedLanguages = this.SupportedLanguages;
			if (supportedLanguages.Length > 2 && supportedLanguages.Length % 2 == 0)
			{
				DropDownList dropDownList = new DropDownList();
				dropDownList.ID = "Language";
				dropDownList.CssClass = this.GetSkinRelativeStyleName("RadSpell_{0} rscSelect");
				string dictionaryLanguage = this.DictionaryLanguage;
				for (int i = 0; i < supportedLanguages.Length; i += 2)
				{
					ListItem listItem = new ListItem(supportedLanguages[i + 1], supportedLanguages[i]);
					if (dictionaryLanguage == listItem.Value)
					{
						listItem.Selected = true;
					}
					dropDownList.Items.Add(listItem);
				}
				return dropDownList;
			}
			return null;
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x000FCB8C File Offset: 0x000FAD8C
		private Control CreateSpellCheckButton()
		{
			HtmlControl htmlControl;
			switch (this.ButtonType)
			{
			case ButtonType.None:
				return null;
			case ButtonType.ImageButton:
				htmlControl = this.CreateLinkButton();
				htmlControl.Attributes["class"] = this.GetSkinRelativeStyleName("RadSpell_{0} rscLinkImg");
				break;
			case ButtonType.LinkButton:
				htmlControl = this.CreateLinkButton();
				htmlControl.Attributes["class"] = this.GetSkinRelativeStyleName("RadSpell_{0} rscLink");
				break;
			case ButtonType.PushButton:
				htmlControl = new HtmlInputButton();
				htmlControl.Attributes["title"] = this.ButtonText;
				(htmlControl as HtmlInputButton).Value = this.ButtonText;
				htmlControl.Attributes["onclick"] = "return false;";
				break;
			default:
				throw new ArgumentException("Invalid ButtonType value.");
			}
			htmlControl.ID = "SpellCheck";
			return htmlControl;
		}

		// Token: 0x0600510E RID: 20750 RVA: 0x000FCC60 File Offset: 0x000FAE60
		private HtmlAnchor CreateLinkButton()
		{
			HtmlAnchor htmlAnchor = new HtmlAnchor();
			htmlAnchor.InnerHtml = (htmlAnchor.Title = this.ButtonText);
			htmlAnchor.HRef = "javascript:void(0);";
			return htmlAnchor;
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x000FCC94 File Offset: 0x000FAE94
		private string GetSkinRelativeStyleName(string style)
		{
			return string.Format(style, base.RuntimeSkin);
		}

		// Token: 0x17001A8B RID: 6795
		// (get) Token: 0x06005110 RID: 20752 RVA: 0x000FCCA2 File Offset: 0x000FAEA2
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17001A8C RID: 6796
		// (get) Token: 0x06005111 RID: 20753 RVA: 0x000FCCA6 File Offset: 0x000FAEA6
		// (set) Token: 0x06005112 RID: 20754 RVA: 0x000FCCB9 File Offset: 0x000FAEB9
		[Description("Gets or sets the URL for the spell dialog handler.")]
		[DefaultValue("Telerik.Web.UI.DialogHandler.aspx")]
		[Category("Behavior")]
		public string HandlerUrl
		{
			get
			{
				this.EnsureChildControls();
				return this._dialogOpener.HandlerUrl;
			}
			set
			{
				this.EnsureChildControls();
				this._dialogOpener.HandlerUrl = value;
			}
		}

		// Token: 0x17001A8D RID: 6797
		// (get) Token: 0x06005113 RID: 20755 RVA: 0x000FCCCD File Offset: 0x000FAECD
		// (set) Token: 0x06005114 RID: 20756 RVA: 0x000FCCE0 File Offset: 0x000FAEE0
		[DefaultValue("")]
		[Category("Appearance")]
		[UrlProperty("*.css")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		public string DialogsCssFile
		{
			get
			{
				this.EnsureChildControls();
				return this._dialogOpener.DialogsCssFile;
			}
			set
			{
				this.EnsureChildControls();
				this._dialogOpener.DialogsCssFile = value;
			}
		}

		// Token: 0x17001A8E RID: 6798
		// (get) Token: 0x06005115 RID: 20757 RVA: 0x000FCCF4 File Offset: 0x000FAEF4
		// (set) Token: 0x06005116 RID: 20758 RVA: 0x000FCD07 File Offset: 0x000FAF07
		[DefaultValue("")]
		[Editor(typeof(UrlEditor), typeof(UITypeEditor))]
		[Category("Behavior")]
		[UrlProperty("*.js")]
		public string DialogsScriptFile
		{
			get
			{
				this.EnsureChildControls();
				return this._dialogOpener.DialogsScriptFile;
			}
			set
			{
				this.EnsureChildControls();
				this._dialogOpener.DialogsScriptFile = value;
			}
		}

		// Token: 0x17001A8F RID: 6799
		// (get) Token: 0x06005117 RID: 20759 RVA: 0x000FCD1B File Offset: 0x000FAF1B
		// (set) Token: 0x06005118 RID: 20760 RVA: 0x000FCD2E File Offset: 0x000FAF2E
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Gets or sets an additional querystring appended to the dialog URL.")]
		public string AdditionalQueryString
		{
			get
			{
				this.EnsureChildControls();
				return this._dialogOpener.AdditionalQueryString;
			}
			set
			{
				this.EnsureChildControls();
				this._dialogOpener.AdditionalQueryString = value;
			}
		}

		// Token: 0x17001A90 RID: 6800
		// (get) Token: 0x06005119 RID: 20761 RVA: 0x000FCD42 File Offset: 0x000FAF42
		// (set) Token: 0x0600511A RID: 20762 RVA: 0x000FCD6D File Offset: 0x000FAF6D
		[DefaultValue(true)]
		[Description("Gets or sets the value indicating whether the spell will allow adding custom words.")]
		[Category("Behavior")]
		public bool AllowAddCustom
		{
			get
			{
				return this.ViewState["AllowAddCustom"] == null || (bool)this.ViewState["AllowAddCustom"];
			}
			set
			{
				this.SetDialogParameter("AllowAddCustom", value);
			}
		}

		// Token: 0x17001A91 RID: 6801
		// (get) Token: 0x0600511B RID: 20763 RVA: 0x000FCD80 File Offset: 0x000FAF80
		// (set) Token: 0x0600511C RID: 20764 RVA: 0x000FCDA0 File Offset: 0x000FAFA0
		[Category("Behavior")]
		[DefaultValue("Telerik.Web.UI.SpellCheckHandler.axd")]
		[Description("Gets or sets the URL which the AJAX call will be made to. Check the help for more information.")]
		public string AjaxUrl
		{
			get
			{
				return ((string)this.ViewState["AjaxUrl"]) ?? "Telerik.Web.UI.SpellCheckHandler.axd";
			}
			set
			{
				this.SetDialogParameter("AjaxUrl", value);
			}
		}

		// Token: 0x17001A92 RID: 6802
		// (get) Token: 0x0600511D RID: 20765 RVA: 0x000FCDAE File Offset: 0x000FAFAE
		// (set) Token: 0x0600511E RID: 20766 RVA: 0x000FCDCE File Offset: 0x000FAFCE
		[DefaultValue("Spell Check")]
		[Localizable(true)]
		[Category("Appearance")]
		[Description("Gets or sets the text of the button that will start the spellcheck. This property is localizable.")]
		public string ButtonText
		{
			get
			{
				return ((string)this.ViewState["ButtonText"]) ?? "Spell Check";
			}
			set
			{
				this.ViewState["ButtonText"] = value;
				base.ChildControlsCreated = false;
			}
		}

		// Token: 0x17001A93 RID: 6803
		// (get) Token: 0x0600511F RID: 20767 RVA: 0x000FCDE8 File Offset: 0x000FAFE8
		// (set) Token: 0x06005120 RID: 20768 RVA: 0x000FCE13 File Offset: 0x000FB013
		[Description("Gets or sets the type of the button that will start the spellcheck.")]
		[Category("Appearance")]
		[DefaultValue(ButtonType.PushButton)]
		public ButtonType ButtonType
		{
			get
			{
				if (this.ViewState["ButtonType"] != null)
				{
					return (ButtonType)this.ViewState["ButtonType"];
				}
				return ButtonType.PushButton;
			}
			set
			{
				this.ViewState["ButtonType"] = value;
				base.ChildControlsCreated = false;
			}
		}

		// Token: 0x17001A94 RID: 6804
		// (get) Token: 0x06005121 RID: 20769 RVA: 0x000FCE34 File Offset: 0x000FB034
		// (set) Token: 0x06005122 RID: 20770 RVA: 0x000FCE66 File Offset: 0x000FB066
		[DefaultValue("")]
		[ClientControlProperty]
		[Description("Specifies the class of the client side text source object.  It has to provide two methods: GetText() and SetText(newValue).")]
		[Category("Behavior")]
		public string ClientTextSource
		{
			get
			{
				string text = ((string)this.ViewState["ClientTextSource"]) ?? string.Empty;
				return text.Trim();
			}
			set
			{
				this.ViewState["ClientTextSource"] = value;
			}
		}

		// Token: 0x17001A95 RID: 6805
		// (get) Token: 0x06005123 RID: 20771 RVA: 0x000FCE7C File Offset: 0x000FB07C
		// (set) Token: 0x06005124 RID: 20772 RVA: 0x000FCEAE File Offset: 0x000FB0AE
		[IDReferenceProperty]
		[Description("The ID of the control to check.")]
		[Category("Behavior")]
		[DefaultValue("")]
		[TypeConverter(typeof(ControlIDConverter))]
		public string ControlToCheck
		{
			get
			{
				string text = ((string)this.ViewState["ControlToCheck"]) ?? string.Empty;
				return text.Trim();
			}
			set
			{
				this.ViewState["ControlToCheck"] = value;
			}
		}

		// Token: 0x17001A96 RID: 6806
		// (get) Token: 0x06005125 RID: 20773 RVA: 0x000FCEC1 File Offset: 0x000FB0C1
		// (set) Token: 0x06005126 RID: 20774 RVA: 0x000FCEE2 File Offset: 0x000FB0E2
		[Category("Behavior")]
		[TypeConverter(typeof(ListConverter))]
		[DefaultValue(typeof(string[]), "")]
		public string[] ControlsToCheck
		{
			get
			{
				return ((string[])this.ViewState["ControlsToCheck"]) ?? new string[0];
			}
			set
			{
				this.ViewState["ControlsToCheck"] = value;
			}
		}

		// Token: 0x17001A97 RID: 6807
		// (get) Token: 0x06005127 RID: 20775 RVA: 0x000FCEF5 File Offset: 0x000FB0F5
		// (set) Token: 0x06005128 RID: 20776 RVA: 0x000FCF15 File Offset: 0x000FB115
		[Description("Gets or sets the fully qualified type name that will be used to store and read the custom dictionary")]
		[Category("Behavior")]
		[DefaultValue("")]
		public string CustomDictionarySourceTypeName
		{
			get
			{
				return ((string)this.ViewState["CustomDictionarySourceTypeName"]) ?? string.Empty;
			}
			set
			{
				this.SetDialogParameter("CustomDictionarySourceTypeName", value);
			}
		}

		// Token: 0x17001A98 RID: 6808
		// (get) Token: 0x06005129 RID: 20777 RVA: 0x000FCF23 File Offset: 0x000FB123
		// (set) Token: 0x0600512A RID: 20778 RVA: 0x000FCF43 File Offset: 0x000FB143
		[DefaultValue("-Custom")]
		[Description("The suffix for the custom dictionary files (filenames are Language + CustomDictionarySuffix + '.txt').")]
		[Category("Behavior")]
		public string CustomDictionarySuffix
		{
			get
			{
				return ((string)this.ViewState["CustomDictionarySuffix"]) ?? "-Custom";
			}
			set
			{
				this.SetDialogParameter("CustomDictionarySuffix", value);
			}
		}

		// Token: 0x17001A99 RID: 6809
		// (get) Token: 0x0600512B RID: 20779 RVA: 0x000FCF51 File Offset: 0x000FB151
		// (set) Token: 0x0600512C RID: 20780 RVA: 0x000FCF71 File Offset: 0x000FB171
		[Description("Gets or sets the assembly qualified name of the SpellDialog type.")]
		[Category("Behavior")]
		[DefaultValue("")]
		public string DialogTypeName
		{
			get
			{
				return ((string)this.ViewState["DialogTypeName"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DialogTypeName"] = value;
				this.ResetDialogDefinition();
			}
		}

		// Token: 0x17001A9A RID: 6810
		// (get) Token: 0x0600512D RID: 20781 RVA: 0x000FCF8A File Offset: 0x000FB18A
		// (set) Token: 0x0600512E RID: 20782 RVA: 0x000FCFAA File Offset: 0x000FB1AA
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Gets or sets the virtual path of the UserControl that represents the SpellDialog.")]
		public string DialogVirtualPath
		{
			get
			{
				return ((string)this.ViewState["DialogVirtualPath"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["DialogVirtualPath"] = value;
				this.ResetDialogDefinition();
			}
		}

		// Token: 0x17001A9B RID: 6811
		// (get) Token: 0x0600512F RID: 20783 RVA: 0x000FCFC3 File Offset: 0x000FB1C3
		// (set) Token: 0x06005130 RID: 20784 RVA: 0x000FCFE3 File Offset: 0x000FB1E3
		[Description("The dictionary language for the spellchecker.")]
		[ClientControlProperty]
		[DefaultValue("en-US")]
		[Category("Behavior")]
		public string DictionaryLanguage
		{
			get
			{
				return ((string)this.ViewState["DictionaryLanguage"]) ?? "en-US";
			}
			set
			{
				this.SetDialogParameter("DictionaryLanguage", value);
			}
		}

		// Token: 0x17001A9C RID: 6812
		// (get) Token: 0x06005131 RID: 20785 RVA: 0x000FCFF1 File Offset: 0x000FB1F1
		// (set) Token: 0x06005132 RID: 20786 RVA: 0x000FD011 File Offset: 0x000FB211
		[DefaultValue("")]
		[Description("The default path for the dictionary files.")]
		[Category("Behavior")]
		public string DictionaryPath
		{
			get
			{
				return ((string)this.ViewState["DictionaryPath"]) ?? string.Empty;
			}
			set
			{
				this.SetDialogParameter("DictionaryPath", value);
			}
		}

		// Token: 0x17001A9D RID: 6813
		// (get) Token: 0x06005133 RID: 20787 RVA: 0x000FD01F File Offset: 0x000FB21F
		// (set) Token: 0x06005134 RID: 20788 RVA: 0x000FD04A File Offset: 0x000FB24A
		[Category("Behavior")]
		[Description("Specifies the edit distance. If you increase the value, the checking speed decreases but more suggestions are presented.")]
		[DefaultValue(1)]
		public int EditDistance
		{
			get
			{
				if (this.ViewState["EditDistance"] != null)
				{
					return (int)this.ViewState["EditDistance"];
				}
				return 1;
			}
			set
			{
				this.SetDialogParameter("EditDistance", value);
			}
		}

		// Token: 0x17001A9E RID: 6814
		// (get) Token: 0x06005135 RID: 20789 RVA: 0x000FD05D File Offset: 0x000FB25D
		// (set) Token: 0x06005136 RID: 20790 RVA: 0x000FD088 File Offset: 0x000FB288
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Ignore selectd text fragments: file names, URL's, email addresses.")]
		[Category("Behavior")]
		[DefaultValue(FragmentIgnoreOptions.All)]
		public FragmentIgnoreOptions FragmentIgnoreOptions
		{
			get
			{
				if (this.ViewState["FragmentIgnoreOptions"] != null)
				{
					return (FragmentIgnoreOptions)this.ViewState["FragmentIgnoreOptions"];
				}
				return FragmentIgnoreOptions.All;
			}
			set
			{
				this.SetDialogParameter("FragmentIgnoreOptions", value);
			}
		}

		// Token: 0x17001A9F RID: 6815
		// (get) Token: 0x06005137 RID: 20791 RVA: 0x000FD09B File Offset: 0x000FB29B
		// (set) Token: 0x06005138 RID: 20792 RVA: 0x000FD0C6 File Offset: 0x000FB2C6
		[DefaultValue(false)]
		[Description("Specifies whether the ControlToCheck property provides a client element ID or a server side control ID.")]
		[Category("Behavior")]
		public bool IsClientID
		{
			get
			{
				return this.ViewState["IsClientID"] != null && (bool)this.ViewState["IsClientID"];
			}
			set
			{
				this.ViewState["IsClientID"] = value;
			}
		}

		// Token: 0x17001AA0 RID: 6816
		// (get) Token: 0x06005139 RID: 20793 RVA: 0x000FD0DE File Offset: 0x000FB2DE
		// (set) Token: 0x0600513A RID: 20794 RVA: 0x000FD0FE File Offset: 0x000FB2FE
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("Gets or sets the localization language for the user interface.")]
		[Bindable(true)]
		public string Language
		{
			get
			{
				return ((string)this.ViewState["Language"]) ?? "";
			}
			set
			{
				this.SetDialogParameter("Language", value);
			}
		}

		// Token: 0x17001AA1 RID: 6817
		// (get) Token: 0x0600513B RID: 20795 RVA: 0x000FD10C File Offset: 0x000FB30C
		[Description("Gets a value indicating if the target control has been spellchecked.")]
		[Browsable(false)]
		public bool SpellChecked
		{
			get
			{
				return this._spellChecked;
			}
		}

		// Token: 0x17001AA2 RID: 6818
		// (get) Token: 0x0600513C RID: 20796 RVA: 0x000FD114 File Offset: 0x000FB314
		// (set) Token: 0x0600513D RID: 20797 RVA: 0x000FD134 File Offset: 0x000FB334
		[Category("Behavior")]
		[Description("Specifies a custom spell check provider type (fully qualified name).")]
		[DefaultValue("")]
		public string SpellCheckProviderTypeName
		{
			get
			{
				return ((string)this.ViewState["SpellCheckProviderTypeName"]) ?? string.Empty;
			}
			set
			{
				this.SetDialogParameter("SpellCheckProviderTypeName", value);
			}
		}

		// Token: 0x17001AA3 RID: 6819
		// (get) Token: 0x0600513E RID: 20798 RVA: 0x000FD142 File Offset: 0x000FB342
		// (set) Token: 0x0600513F RID: 20799 RVA: 0x000FD16D File Offset: 0x000FB36D
		[DefaultValue(SpellCheckProvider.PhoneticProvider)]
		[Description("Specifies the spellcecking algorithm which will be used by RadSpell.")]
		[Category("Behavior")]
		public SpellCheckProvider SpellCheckProvider
		{
			get
			{
				if (this.ViewState["SpellCheckProvider"] != null)
				{
					return (SpellCheckProvider)this.ViewState["SpellCheckProvider"];
				}
				return SpellCheckProvider.PhoneticProvider;
			}
			set
			{
				this.SetDialogParameter("SpellCheckProvider", value);
			}
		}

		// Token: 0x17001AA4 RID: 6820
		// (get) Token: 0x06005140 RID: 20800 RVA: 0x000FD180 File Offset: 0x000FB380
		// (set) Token: 0x06005141 RID: 20801 RVA: 0x000FD1BE File Offset: 0x000FB3BE
		[DefaultValue("en-US,English")]
		[TypeConverter(typeof(ListConverter))]
		[Category("Behavior")]
		public string[] SupportedLanguages
		{
			get
			{
				string[] result;
				if ((result = (string[])this.ViewState["SupportedLanguages"]) == null)
				{
					result = new string[]
					{
						"en-US",
						"English"
					};
				}
				return result;
			}
			set
			{
				this.ViewState["SupportedLanguages"] = value;
				base.ChildControlsCreated = false;
			}
		}

		// Token: 0x17001AA5 RID: 6821
		// (get) Token: 0x06005142 RID: 20802 RVA: 0x000FD1D8 File Offset: 0x000FB3D8
		// (set) Token: 0x06005143 RID: 20803 RVA: 0x000FD203 File Offset: 0x000FB403
		[Category("Behavior")]
		[DefaultValue(WordIgnoreOptions.RepeatedWords)]
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the value used to configure the spellchecker engine to ignore words containing: UPPERCASE, some CaPitaL letters, numbers; or to ignore repeated words (very very).")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public WordIgnoreOptions WordIgnoreOptions
		{
			get
			{
				if (this.ViewState["WordIgnoreOptions"] != null)
				{
					return (WordIgnoreOptions)this.ViewState["WordIgnoreOptions"];
				}
				return WordIgnoreOptions.RepeatedWords;
			}
			set
			{
				this.SetDialogParameter("WordIgnoreOptions", value);
			}
		}

		// Token: 0x17001AA6 RID: 6822
		// (get) Token: 0x06005144 RID: 20804 RVA: 0x000FD216 File Offset: 0x000FB416
		// (set) Token: 0x06005145 RID: 20805 RVA: 0x000FD236 File Offset: 0x000FB436
		[Category("Behavior")]
		[Description("Gets or sets the name of the client-side function that will be called when the spell control is initialized on the page.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("load")]
		public string OnClientLoad
		{
			get
			{
				return ((string)this.ViewState["OnClientLoad"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x17001AA7 RID: 6823
		// (get) Token: 0x06005146 RID: 20806 RVA: 0x000FD249 File Offset: 0x000FB449
		// (set) Token: 0x06005147 RID: 20807 RVA: 0x000FD269 File Offset: 0x000FB469
		[Description("Gets or sets the name of the client-side function that will be called when the spell check starts.")]
		[Category("Behavior")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("clientCheckStarted")]
		public string OnClientCheckStarted
		{
			get
			{
				return ((string)this.ViewState["OnClientCheckStarted"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientCheckStarted"] = value;
			}
		}

		// Token: 0x17001AA8 RID: 6824
		// (get) Token: 0x06005148 RID: 20808 RVA: 0x000FD27C File Offset: 0x000FB47C
		// (set) Token: 0x06005149 RID: 20809 RVA: 0x000FD29C File Offset: 0x000FB49C
		[ClientPropertyName("clientCheckFinished")]
		[ClientControlEvent]
		[Description("Specifies the name of the client side function that will be called when the spell check is finished.")]
		[Category("Behavior")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientCheckFinished
		{
			get
			{
				return ((string)this.ViewState["OnClientCheckFinished"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientCheckFinished"] = value;
			}
		}

		// Token: 0x17001AA9 RID: 6825
		// (get) Token: 0x0600514A RID: 20810 RVA: 0x000FD2AF File Offset: 0x000FB4AF
		// (set) Token: 0x0600514B RID: 20811 RVA: 0x000FD2CF File Offset: 0x000FB4CF
		[ClientPropertyName("clientCheckCancelled")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Specifies the name of the client side function that will be called when the user cancels the spell check.")]
		public string OnClientCheckCancelled
		{
			get
			{
				return ((string)this.ViewState["OnClientCheckCancelled"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientCheckCancelled"] = value;
			}
		}

		// Token: 0x17001AAA RID: 6826
		// (get) Token: 0x0600514C RID: 20812 RVA: 0x000FD2E2 File Offset: 0x000FB4E2
		// (set) Token: 0x0600514D RID: 20813 RVA: 0x000FD2F5 File Offset: 0x000FB4F5
		[Description("Specifies the name of the client side function that will be called after the spell check dialog closes.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string OnClientDialogClosed
		{
			get
			{
				this.EnsureChildControls();
				return this._dialogOpener.OnClientClose;
			}
			set
			{
				this.EnsureChildControls();
				this._dialogOpener.OnClientClose = value;
			}
		}

		// Token: 0x17001AAB RID: 6827
		// (get) Token: 0x0600514E RID: 20814 RVA: 0x000FD309 File Offset: 0x000FB509
		// (set) Token: 0x0600514F RID: 20815 RVA: 0x000FD311 File Offset: 0x000FB511
		[TypeConverter("Telerik.Web.Design.SkinTypeConverter, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
		[Description("Specifies the skin that will be used by the control")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[DefaultValue("Default")]
		public override string Skin
		{
			get
			{
				return base.Skin;
			}
			set
			{
				if (base.ChildControlsCreated)
				{
					this._dialogOpener.Skin = value;
					this.SetSpellDialogSize();
				}
				base.Skin = value;
			}
		}

		// Token: 0x17001AAC RID: 6828
		// (get) Token: 0x06005150 RID: 20816 RVA: 0x000FD334 File Offset: 0x000FB534
		// (set) Token: 0x06005151 RID: 20817 RVA: 0x000FD33C File Offset: 0x000FB53C
		[NotifyParentProperty(true)]
		[Description("Whether to register the skin CSS during Ajax requests")]
		[Category("Appearance")]
		[DefaultValue(true)]
		public override bool EnableAjaxSkinRendering
		{
			get
			{
				return base.EnableAjaxSkinRendering;
			}
			set
			{
				this.EnsureChildControls();
				this._dialogOpener.EnableAjaxSkinRendering = value;
				base.EnableAjaxSkinRendering = value;
			}
		}

		// Token: 0x17001AAD RID: 6829
		// (get) Token: 0x06005152 RID: 20818 RVA: 0x000FD357 File Offset: 0x000FB557
		// (set) Token: 0x06005153 RID: 20819 RVA: 0x000FD35F File Offset: 0x000FB55F
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("Whether to output the control scripts automatically")]
		[DefaultValue(true)]
		public override bool EnableEmbeddedScripts
		{
			get
			{
				return base.EnableEmbeddedScripts;
			}
			set
			{
				this.SetDialogParameter("EnableEmbeddedScripts", value);
				this._dialogOpener.EnableEmbeddedScripts = value;
				base.EnableEmbeddedScripts = value;
			}
		}

		// Token: 0x17001AAE RID: 6830
		// (get) Token: 0x06005154 RID: 20820 RVA: 0x000FD385 File Offset: 0x000FB585
		// (set) Token: 0x06005155 RID: 20821 RVA: 0x000FD3A8 File Offset: 0x000FB5A8
		[DefaultValue("")]
		[Description("Gets or sets a value indicating where the spell will look for its .resx localization files.")]
		[Category("Misc")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.SetDialogParameter("LocalizationPath", text);
			}
		}

		// Token: 0x17001AAF RID: 6831
		// (get) Token: 0x06005156 RID: 20822 RVA: 0x000FD3F6 File Offset: 0x000FB5F6
		// (set) Token: 0x06005157 RID: 20823 RVA: 0x000FD3F9 File Offset: 0x000FB5F9
		bool ISkinnableControl.EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17001AB0 RID: 6832
		// (get) Token: 0x06005158 RID: 20824 RVA: 0x000FD3FB File Offset: 0x000FB5FB
		// (set) Token: 0x06005159 RID: 20825 RVA: 0x000FD408 File Offset: 0x000FB608
		[NotifyParentProperty(true)]
		[Description("Whether to register the selected skin automatically")]
		[Category("Appearance")]
		[DefaultValue(true)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return this._dialogOpener.EnableEmbeddedSkins;
			}
			set
			{
				this.SetDialogParameter("EnableEmbeddedSkins", value);
				this._dialogOpener.EnableEmbeddedSkins = value;
			}
		}

		// Token: 0x17001AB1 RID: 6833
		// (get) Token: 0x0600515A RID: 20826 RVA: 0x000FD427 File Offset: 0x000FB627
		// (set) Token: 0x0600515B RID: 20827 RVA: 0x000FD42A File Offset: 0x000FB62A
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

		// Token: 0x17001AB2 RID: 6834
		// (get) Token: 0x0600515C RID: 20828 RVA: 0x000FD42C File Offset: 0x000FB62C
		// (set) Token: 0x0600515D RID: 20829 RVA: 0x000FD439 File Offset: 0x000FB639
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("Whether to register the base control skin file automatically")]
		[Category("Appearance")]
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return this._dialogOpener.EnableEmbeddedBaseStylesheet;
			}
			set
			{
				this.SetDialogParameter("EnableEmbeddedBaseStylesheet", value);
				this._dialogOpener.EnableEmbeddedBaseStylesheet = value;
			}
		}

		// Token: 0x17001AB3 RID: 6835
		// (get) Token: 0x0600515E RID: 20830 RVA: 0x000FD458 File Offset: 0x000FB658
		// (set) Token: 0x0600515F RID: 20831 RVA: 0x000FD479 File Offset: 0x000FB679
		[DefaultValue(0)]
		[Description("The tabindex of the RadSpell SpellCheck Button.")]
		[Category("Misc")]
		public override short TabIndex
		{
			get
			{
				return (short)(this.ViewState["TabIndex"] ?? 0);
			}
			set
			{
				this.ViewState["TabIndex"] = value;
			}
		}

		// Token: 0x06005160 RID: 20832 RVA: 0x000FD491 File Offset: 0x000FB691
		private void SetDialogParameter(string key, object value)
		{
			this.ViewState[key] = value;
			this.EnsureChildControls();
			this._spellDialogDefinition.Parameters[key] = value;
		}

		// Token: 0x040013FB RID: 5115
		private bool _spellChecked;

		// Token: 0x040013FC RID: 5116
		private RadDialogOpener _dialogOpener;

		// Token: 0x040013FD RID: 5117
		private DialogDefinition _spellDialogDefinition;
	}
}

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x02000293 RID: 659
	[ClientScriptResource("Telerik.Web.UI.Editor.FindReplaceMobile", "Telerik.Web.UI.Editor.FindReplace.js")]
	[ToolboxItem(false)]
	[RequiredScript(typeof(jQueryPlugins))]
	[ClientScriptResource("Telerik.Web.UI.Editor.FindReplaceMobile", "Telerik.Web.UI.Editor.RadEditor.js")]
	public class FindReplaceMobile : RadWebControl
	{
		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x0004ED58 File Offset: 0x0004CF58
		// (set) Token: 0x0600178E RID: 6030 RVA: 0x0004ED60 File Offset: 0x0004CF60
		public DialogsStrings Localization { get; set; }

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x0600178F RID: 6031 RVA: 0x0004ED69 File Offset: 0x0004CF69
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x0004ED6D File Offset: 0x0004CF6D
		protected override string CssClassFormatString
		{
			get
			{
				return "reFindOverlay";
			}
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x0004ED74 File Offset: 0x0004CF74
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this._findTextBox = this.CreateTextBox("findTextBox", "search", this.Localize("FindAndReplace_Find", "Find"));
			this._replaceTextBox = this.CreateTextBox("replaceTextBox", "text", this.Localize("FindAndReplace_Replace", "Replace"));
			this._findPreviousBtn = this.CreateButton("findUpBtn", "rbHeadWest", "rbArrowHeadWest");
			this._findNextBtn = this.CreateButton("findDownBtn", "rbHeadEast", "rbArrowHeadEast");
			this._settingsBtn = this.CreateButton("settingsBtn", "rbGear", "rbGearIcon");
			this._replaceLink = this.CreateLink("replaceLink", this.Localize("FindAndReplace_Replace", "Replace"));
			this._replaceAllLink = this.CreateLink("replaceAllLink", this.Localize("Common_All", "All"));
			HtmlControl htmlControl = this.CreateRow();
			htmlControl.Controls.Add(this._findTextBox);
			htmlControl.Controls.Add(this._findPreviousBtn);
			htmlControl.Controls.Add(this._findNextBtn);
			htmlControl.Controls.Add(this._settingsBtn);
			HtmlControl htmlControl2 = this.CreateRow();
			htmlControl2.Style.Add("display", "none");
			htmlControl2.Controls.Add(this._replaceTextBox);
			HtmlControl htmlControl3 = this.CreateLinkWrapper();
			htmlControl3.Controls.Add(this._replaceLink);
			htmlControl3.Controls.Add(this.CreateSeparator());
			htmlControl3.Controls.Add(this._replaceAllLink);
			htmlControl2.Controls.Add(htmlControl3);
			this.Controls.Add(htmlControl);
			this.Controls.Add(htmlControl2);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0004EF3C File Offset: 0x0004D13C
		private HtmlControl CreateRow()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", "t-hbox");
			return htmlGenericControl;
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0004EF6C File Offset: 0x0004D16C
		private HtmlControl CreateLinkWrapper()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", "reReplaceButtons");
			return htmlGenericControl;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0004EF9C File Offset: 0x0004D19C
		private HtmlInputText CreateTextBox(string id, string type, string placeholder)
		{
			HtmlInputText htmlInputText = new HtmlInputText(type);
			htmlInputText.ID = id;
			htmlInputText.Attributes.Add("class", "t-flex");
			htmlInputText.Attributes.Add("placeholder", placeholder);
			return htmlInputText;
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0004EFE0 File Offset: 0x0004D1E0
		private RadButton CreateButton(string id, string cssClass, string iconCssClass)
		{
			return new RadButton
			{
				Skin = this.Skin,
				ID = id,
				CssClass = cssClass,
				AutoPostBack = false,
				RenderMode = RenderMode.Lightweight,
				Icon = 
				{
					PrimaryIconCssClass = iconCssClass
				}
			};
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0004F028 File Offset: 0x0004D228
		private HtmlAnchor CreateLink(string id, string text)
		{
			return new HtmlAnchor
			{
				ID = id,
				HRef = "javascript: void 0;",
				InnerText = text
			};
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0004F058 File Offset: 0x0004D258
		private HtmlGenericControl CreateSeparator()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
			htmlGenericControl.InnerText = " | ";
			htmlGenericControl.Attributes.Add("class", "reSpace");
			return htmlGenericControl;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0004F091 File Offset: 0x0004D291
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0004F0A8 File Offset: 0x0004D2A8
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddElementProperty("findInput", this._findTextBox.ClientID);
			descriptor.AddElementProperty("replaceInput", this._replaceTextBox.ClientID);
			descriptor.AddComponentProperty("findPreviousBtn", this._findPreviousBtn.ClientID);
			descriptor.AddComponentProperty("findNextBtn", this._findNextBtn.ClientID);
			descriptor.AddComponentProperty("settingsBtn", this._settingsBtn.ClientID);
			descriptor.AddElementProperty("replaceLink", this._replaceLink.ClientID);
			descriptor.AddElementProperty("replaceAllLink", this._replaceAllLink.ClientID);
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x0004F156 File Offset: 0x0004D356
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x0004F159 File Offset: 0x0004D359
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0004F15C File Offset: 0x0004D35C
		public override void RenderClientStateField(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0004F15E File Offset: 0x0004D35E
		private string Localize(string key, string defaultValue)
		{
			if (this.Localization == null)
			{
				return defaultValue;
			}
			return this.Localization.GetString(key);
		}

		// Token: 0x0400061D RID: 1565
		internal const string CSS_CLASS_FORMAT = "reFindOverlay {0}";

		// Token: 0x0400061E RID: 1566
		private HtmlInputText _findTextBox;

		// Token: 0x0400061F RID: 1567
		private HtmlInputText _replaceTextBox;

		// Token: 0x04000620 RID: 1568
		private RadButton _findPreviousBtn;

		// Token: 0x04000621 RID: 1569
		private RadButton _findNextBtn;

		// Token: 0x04000622 RID: 1570
		private RadButton _settingsBtn;

		// Token: 0x04000623 RID: 1571
		private HtmlAnchor _replaceLink;

		// Token: 0x04000624 RID: 1572
		private HtmlAnchor _replaceAllLink;
	}
}

using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Editor.DialogControls;

namespace Telerik.Web.UI.Dialogs
{
	// Token: 0x0200134E RID: 4942
	[ClientScriptResource(ResourcePath = "Telerik.Web.UI.Common.Core.js")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Mobile, typeof(UserControlResources))]
	[ToolboxItem(false)]
	[RequiredCss("Telerik.Web.UI.Skins.Widgets.css", RenderMode.Classic, typeof(UserControlResources))]
	[RequiredCss("Telerik.Web.UI.Skins.Widgets.css", RenderMode.Lightweight, typeof(UserControlResources))]
	[RequiredCss("Telerik.Web.UI.Skins.Widgets.css", RenderMode.Mobile, typeof(UserControlResources))]
	[EmbeddedSkin("Widgets", "Default")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(UserControlResources))]
	public class UserControlResources : RadWebControl, ILocalizableControl
	{
		// Token: 0x17004265 RID: 16997
		// (get) Token: 0x0600CE74 RID: 52852 RVA: 0x002DEAF4 File Offset: 0x002DCCF4
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17004266 RID: 16998
		// (get) Token: 0x0600CE75 RID: 52853 RVA: 0x002DEAF8 File Offset: 0x002DCCF8
		// (set) Token: 0x0600CE76 RID: 52854 RVA: 0x002DEB2C File Offset: 0x002DCD2C
		[Description("Gets or sets a string containing the localization language for the RadEditor UI.")]
		[Category("Appearance")]
		[MergableProperty(true)]
		public string Language
		{
			get
			{
				if (this.ViewState["Language"] == null)
				{
					return CultureInfo.CurrentUICulture.Name;
				}
				return (string)this.ViewState["Language"];
			}
			set
			{
				this.ViewState["Language"] = value;
				this._culture = ((value == null) ? null : CultureInfo.GetCultureInfo(value));
			}
		}

		// Token: 0x17004267 RID: 16999
		// (get) Token: 0x0600CE77 RID: 52855 RVA: 0x002DEB51 File Offset: 0x002DCD51
		CultureInfo ILocalizableControl.Culture
		{
			get
			{
				return this._culture;
			}
		}

		// Token: 0x17004268 RID: 17000
		// (get) Token: 0x0600CE78 RID: 52856 RVA: 0x002DEB59 File Offset: 0x002DCD59
		// (set) Token: 0x0600CE79 RID: 52857 RVA: 0x002DEB61 File Offset: 0x002DCD61
		public DialogLocalizationStrings Localization
		{
			get
			{
				return this._localization;
			}
			set
			{
				this._localization = value;
			}
		}

		// Token: 0x0600CE7A RID: 52858 RVA: 0x002DEB6C File Offset: 0x002DCD6C
		protected virtual string GetLocalizationScript()
		{
			StringBuilder stringBuilder = new StringBuilder("<script type=\"text/javascript\">");
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("//<![CDATA[");
			string key = "localizationDefine";
			if (this.Page == null || !this.Page.ClientScript.IsClientScriptBlockRegistered(this.Page.GetType(), key))
			{
				if (this.Page != null)
				{
					this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), key, "", false);
				}
				stringBuilder.AppendLine("if (typeof(localization) == \"undefined\")\r\n{\r\n\tvar localization = {\r\n\t\t_fill : function(newStrings)\r\n\t\t{\r\n\t\t\tfor (var key in newStrings)\r\n\t\t\t{\r\n\t\t\t\tthis[key] = newStrings[key];\r\n\t\t\t}\r\n\t\t},\r\n\t\tsetTitle : function(elementId, resourceKey) {\r\n\t\t\t$get(elementId).title = this[resourceKey];\r\n\t\t}\r\n\t};\r\n}");
			}
			string key2 = "localizationCommon";
			if (this.Page == null || !this.Page.ClientScript.IsClientScriptBlockRegistered(this.Page.GetType(), key2))
			{
				if (this.Page != null)
				{
					this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), key2, "", false);
				}
				stringBuilder.Append("localization._fill({");
				foreach (string text in this.Localization.GetStringKeys())
				{
					if (text.StartsWith("Common_"))
					{
						string text2 = text;
						if (text2.IndexOf('_') != -1)
						{
							text2 = text2.Substring(text2.IndexOf('_') + 1);
						}
						stringBuilder.AppendFormat("{0}:\"{1}\",", text2, this.Localization.GetJavaScriptString(text2));
					}
				}
				if (stringBuilder[stringBuilder.Length - 1] == ',')
				{
					stringBuilder.Length--;
				}
				stringBuilder.AppendLine("});");
			}
			stringBuilder.Append("localization._fill({");
			foreach (string text3 in this.Localization.GetStringKeys())
			{
				if (!text3.StartsWith("Common_"))
				{
					string text4 = text3;
					if (text4.IndexOf('_') != -1)
					{
						text4 = text4.Substring(text4.IndexOf('_') + 1);
					}
					stringBuilder.AppendFormat("{0}:\"{1}\",", text4, this.Localization.GetJavaScriptString(text4));
				}
			}
			if (stringBuilder[stringBuilder.Length - 1] == ',')
			{
				stringBuilder.Length--;
			}
			stringBuilder.AppendLine("});\r\n//]]>\r\n</script>");
			return stringBuilder.ToString();
		}

		// Token: 0x0600CE7B RID: 52859 RVA: 0x002DEDE8 File Offset: 0x002DCFE8
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this._localizationContainer = new Literal();
			this.Controls.Add(this._localizationContainer);
		}

		// Token: 0x0600CE7C RID: 52860 RVA: 0x002DEE0C File Offset: 0x002DD00C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.EnsureChildControls();
			this._localizationContainer.Text = this.GetLocalizationScript();
		}

		// Token: 0x04003720 RID: 14112
		private CultureInfo _culture;

		// Token: 0x04003721 RID: 14113
		private DialogLocalizationStrings _localization;

		// Token: 0x04003722 RID: 14114
		private Literal _localizationContainer;
	}
}

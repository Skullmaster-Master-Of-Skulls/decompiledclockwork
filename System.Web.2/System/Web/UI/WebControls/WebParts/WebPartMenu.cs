using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Web.Handlers;
using System.Web.Util;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005A6 RID: 1446
	internal sealed class WebPartMenu
	{
		// Token: 0x06004936 RID: 18742 RVA: 0x000F3182 File Offset: 0x000F1382
		public WebPartMenu(IWebPartMenuUser menuUser)
		{
			this._menuUser = menuUser;
		}

		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x06004937 RID: 18743 RVA: 0x000F3191 File Offset: 0x000F1391
		private static string DefaultCheckImageUrl
		{
			get
			{
				if (WebPartMenu._defaultCheckImageUrl == null)
				{
					WebPartMenu._defaultCheckImageUrl = AssemblyResourceLoader.GetWebResourceUrl(typeof(WebPartMenu), "WebPartMenu_Check.gif");
				}
				return WebPartMenu._defaultCheckImageUrl;
			}
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x000F31B8 File Offset: 0x000F13B8
		private void RegisterStartupScript(string clientID)
		{
			string value = string.Empty;
			string value2 = string.Empty;
			Style itemStyle = this._menuUser.ItemStyle;
			if (itemStyle != null)
			{
				value = itemStyle.GetStyleAttributes(this._menuUser.UrlResolver).Value;
			}
			Style itemHoverStyle = this._menuUser.ItemHoverStyle;
			if (itemHoverStyle != null)
			{
				value2 = itemHoverStyle.GetStyleAttributes(this._menuUser.UrlResolver).Value;
			}
			string text = string.Empty;
			string text2 = string.Empty;
			Style labelHoverStyle = this._menuUser.LabelHoverStyle;
			if (labelHoverStyle != null)
			{
				Color foreColor = labelHoverStyle.ForeColor;
				if (!foreColor.IsEmpty)
				{
					text = ColorTranslator.ToHtml(foreColor);
				}
				text2 = labelHoverStyle.RegisteredCssClass;
			}
			string script = string.Concat(new string[]
			{
				"\r\n<script type=\"text/javascript\">\r\nvar menu",
				clientID,
				" = new WebPartMenu(document.getElementById('",
				clientID,
				"'), document.getElementById('",
				clientID,
				"Popup'), document.getElementById('",
				clientID,
				"Menu'));\r\nmenu",
				clientID,
				".itemStyle = '",
				Util.QuoteJScriptString(value),
				"';\r\nmenu",
				clientID,
				".itemHoverStyle = '",
				Util.QuoteJScriptString(value2),
				"';\r\nmenu",
				clientID,
				".labelHoverColor = '",
				text,
				"';\r\nmenu",
				clientID,
				".labelHoverClassName = '",
				text2,
				"';\r\n</script>\r\n"
			});
			if (this._menuUser.Page != null)
			{
				this._menuUser.Page.ClientScript.RegisterStartupScript((Control)this._menuUser, typeof(WebPartMenu), clientID, script, false);
				IScriptManager scriptManager = this._menuUser.Page.ScriptManager;
				if (scriptManager != null && scriptManager.SupportsPartialRendering)
				{
					scriptManager.RegisterDispose((Control)this._menuUser, "document.getElementById('" + clientID + "').__menu.Dispose();");
				}
			}
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x000F33A0 File Offset: 0x000F15A0
		private void RegisterStyle(Style style)
		{
			if (style != null && !style.IsEmpty)
			{
				string clientID = this._menuUser.ClientID;
				string str = "__Menu_";
				int cssStyleIndex = this._cssStyleIndex;
				this._cssStyleIndex = cssStyleIndex + 1;
				string text = clientID + str + cssStyleIndex.ToString(NumberFormatInfo.InvariantInfo);
				this._menuUser.Page.Header.StyleSheet.CreateStyleRule(style, this._menuUser.UrlResolver, "." + text);
				style.SetRegisteredCssClass(text);
			}
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x000F3422 File Offset: 0x000F1622
		public void RegisterStyles()
		{
			this.RegisterStyle(this._menuUser.LabelStyle);
			this.RegisterStyle(this._menuUser.LabelHoverStyle);
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x000F3446 File Offset: 0x000F1646
		public void Render(HtmlTextWriter writer, string clientID)
		{
			this.RenderLabel(writer, clientID, null);
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x000F3451 File Offset: 0x000F1651
		public void Render(HtmlTextWriter writer, ICollection verbs, string clientID, WebPart associatedWebPart, WebPartManager webPartManager)
		{
			this.RegisterStartupScript(clientID);
			this.RenderLabel(writer, clientID, associatedWebPart);
			this.RenderMenuPopup(writer, verbs, clientID, associatedWebPart, webPartManager);
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x000F3474 File Offset: 0x000F1674
		private void RenderLabel(HtmlTextWriter writer, string clientID, WebPart associatedWebPart)
		{
			this._menuUser.OnBeginRender(writer);
			if (associatedWebPart != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, clientID);
				Style labelStyle = this._menuUser.LabelStyle;
				if (labelStyle != null)
				{
					labelStyle.AddAttributesToRender(writer, this._menuUser as WebControl);
				}
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.Cursor, "hand");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "inline-block");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "1px");
			writer.AddStyleAttribute(HtmlTextWriterStyle.TextDecoration, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			string labelImageUrl = this._menuUser.LabelImageUrl;
			string labelText = this._menuUser.LabelText;
			if (!string.IsNullOrEmpty(labelImageUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Src, labelImageUrl);
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, (!string.IsNullOrEmpty(labelText)) ? labelText : SR.GetString("WebPartMenu_DefaultDropDownAlternateText"), true);
				writer.AddStyleAttribute("vertical-align", "middle");
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.Write("&nbsp;");
			}
			if (!string.IsNullOrEmpty(labelText))
			{
				writer.Write(labelText);
				writer.Write("&nbsp;");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Id, clientID + "Popup");
			string popupImageUrl = this._menuUser.PopupImageUrl;
			if (!string.IsNullOrEmpty(popupImageUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Src, popupImageUrl);
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, (!string.IsNullOrEmpty(labelText)) ? labelText : SR.GetString("WebPartMenu_DefaultDropDownAlternateText"), true);
				writer.AddStyleAttribute("vertical-align", "middle");
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}
			else
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.FontFamily, "Marlett");
				writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "8pt");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write("u");
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
			this._menuUser.OnEndRender(writer);
		}

		// Token: 0x0600493E RID: 18750 RVA: 0x000F3648 File Offset: 0x000F1848
		private void RenderMenuPopup(HtmlTextWriter writer, ICollection verbs, string clientID, WebPart associatedWebPart, WebPartManager webPartManager)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, clientID + "Menu");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			bool flag = true;
			WebPartMenuStyle menuPopupStyle = this._menuUser.MenuPopupStyle;
			if (menuPopupStyle != null)
			{
				menuPopupStyle.AddAttributesToRender(writer, this._menuUser as WebControl);
				flag = menuPopupStyle.Width.IsEmpty;
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
				writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1");
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderCollapse, "collapse");
			}
			if (flag)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			bool isEnabled = associatedWebPart.Zone.IsEnabled;
			foreach (object obj in verbs)
			{
				WebPartVerb webPartVerb = (WebPartVerb)obj;
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				string text;
				if (associatedWebPart != null)
				{
					text = string.Format(CultureInfo.CurrentCulture, webPartVerb.Description, new object[]
					{
						associatedWebPart.DisplayTitle
					});
				}
				else
				{
					text = webPartVerb.Description;
				}
				if (text.Length != 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Title, text);
				}
				bool flag2 = isEnabled && webPartVerb.Enabled;
				if (webPartVerb is WebPartHelpVerb)
				{
					string value = ((IUrlResolutionService)associatedWebPart).ResolveClientUrl(associatedWebPart.HelpUrl);
					writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0)");
					if (flag2)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Onclick, string.Concat(new string[]
						{
							"document.body.__wpm.ShowHelp('",
							Util.QuoteJScriptString(value),
							"', ",
							((int)associatedWebPart.HelpMode).ToString(CultureInfo.InvariantCulture),
							")"
						}));
					}
				}
				else if (webPartVerb is WebPartExportVerb)
				{
					string exportUrl = webPartManager.GetExportUrl(associatedWebPart);
					writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0)");
					if (flag2)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "document.body.__wpm.ExportWebPart('" + Util.QuoteJScriptString(exportUrl) + ((associatedWebPart.ExportMode == WebPartExportMode.All) ? "', true, false)" : "', false, false)"));
					}
				}
				else
				{
					string postBackTarget = this._menuUser.PostBackTarget;
					writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0)");
					if (flag2)
					{
						string eventArgument = webPartVerb.EventArgument;
						if (associatedWebPart != null)
						{
							eventArgument = webPartVerb.GetEventArgument(associatedWebPart.ID);
						}
						string text2 = null;
						if (!string.IsNullOrEmpty(eventArgument))
						{
							text2 = string.Concat(new string[]
							{
								"document.body.__wpm.SubmitPage('",
								Util.QuoteJScriptString(postBackTarget),
								"', '",
								Util.QuoteJScriptString(eventArgument),
								"');"
							});
							this._menuUser.Page.ClientScript.RegisterForEventValidation(postBackTarget, eventArgument);
						}
						string text3 = null;
						if (!string.IsNullOrEmpty(webPartVerb.ClientClickHandler))
						{
							text3 = "document.body.__wpm.Execute('" + Util.QuoteJScriptString(Util.EnsureEndWithSemiColon(webPartVerb.ClientClickHandler)) + "')";
						}
						string text4 = string.Empty;
						if (text2 != null && text3 != null)
						{
							text4 = string.Concat(new string[]
							{
								"if(",
								text3,
								"){",
								text2,
								"}"
							});
						}
						else if (text2 != null)
						{
							text4 = text2;
						}
						else if (text3 != null)
						{
							text4 = text3;
						}
						if (webPartVerb is WebPartCloseVerb)
						{
							ProviderConnectionPointCollection providerConnectionPoints = webPartManager.GetProviderConnectionPoints(associatedWebPart);
							if (providerConnectionPoints != null && providerConnectionPoints.Count > 0 && webPartManager.Connections.ContainsProvider(associatedWebPart))
							{
								text4 = "if(document.body.__wpmCloseProviderWarning.length == 0 || confirm(document.body.__wpmCloseProviderWarning)){" + text4 + "}";
							}
						}
						else if (webPartVerb is WebPartDeleteVerb)
						{
							text4 = "if(document.body.__wpmDeleteWarning.length == 0 || confirm(document.body.__wpmDeleteWarning)){" + text4 + "}";
						}
						writer.AddAttribute(HtmlTextWriterAttribute.Onclick, text4);
					}
				}
				string text5 = "menuItem";
				if (!webPartVerb.Enabled)
				{
					if (associatedWebPart.Zone.RenderingCompatibility < VersionUtil.Framework40)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
					}
					else if (!string.IsNullOrEmpty(WebControl.DisabledCssClass))
					{
						text5 = WebControl.DisabledCssClass + " " + text5;
					}
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text5);
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				string text6 = webPartVerb.ImageUrl;
				if (text6.Length != 0)
				{
					text6 = this._menuUser.UrlResolver.ResolveClientUrl(text6);
				}
				else if (webPartVerb.Checked)
				{
					text6 = this._menuUser.CheckImageUrl;
					if (text6.Length == 0)
					{
						text6 = WebPartMenu.DefaultCheckImageUrl;
					}
				}
				else
				{
					text6 = webPartManager.SpacerImageUrl;
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Src, text6);
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, text, true);
				writer.AddAttribute(HtmlTextWriterAttribute.Width, "16");
				writer.AddAttribute(HtmlTextWriterAttribute.Height, "16");
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "none");
				writer.AddStyleAttribute("vertical-align", "middle");
				if (webPartVerb.Checked)
				{
					Style checkImageStyle = this._menuUser.CheckImageStyle;
					if (checkImageStyle != null)
					{
						checkImageStyle.AddAttributesToRender(writer, this._menuUser as WebControl);
					}
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
				writer.Write("&nbsp;");
				writer.Write(webPartVerb.Text);
				writer.Write("&nbsp;");
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x04002797 RID: 10135
		private static string _defaultCheckImageUrl;

		// Token: 0x04002798 RID: 10136
		private int _cssStyleIndex;

		// Token: 0x04002799 RID: 10137
		private IWebPartMenuUser _menuUser;
	}
}

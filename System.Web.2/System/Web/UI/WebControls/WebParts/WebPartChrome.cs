using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000581 RID: 1409
	public class WebPartChrome
	{
		// Token: 0x0600475A RID: 18266 RVA: 0x000EA958 File Offset: 0x000E8B58
		public WebPartChrome(WebPartZoneBase zone, WebPartManager manager)
		{
			if (zone == null)
			{
				throw new ArgumentNullException("zone");
			}
			this._zone = zone;
			this._page = zone.Page;
			this._designMode = zone.DesignMode;
			this._manager = manager;
			if (this._designMode)
			{
				this._personalizationEnabled = true;
			}
			else
			{
				this._personalizationEnabled = (manager != null && manager.Personalization.IsModifiable);
			}
			if (manager != null)
			{
				this._personalizationScope = manager.Personalization.Scope;
				return;
			}
			this._personalizationScope = PersonalizationScope.Shared;
		}

		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x0600475B RID: 18267 RVA: 0x000EA9E3 File Offset: 0x000E8BE3
		private WebPartConnectionCollection Connections
		{
			get
			{
				if (this._connections == null)
				{
					this._connections = this._manager.Connections;
				}
				return this._connections;
			}
		}

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x0600475C RID: 18268 RVA: 0x000EAA04 File Offset: 0x000E8C04
		protected bool DragDropEnabled
		{
			get
			{
				return this.Zone.DragDropEnabled;
			}
		}

		// Token: 0x1700150E RID: 5390
		// (get) Token: 0x0600475D RID: 18269 RVA: 0x000EAA11 File Offset: 0x000E8C11
		protected WebPartManager WebPartManager
		{
			get
			{
				return this._manager;
			}
		}

		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x0600475E RID: 18270 RVA: 0x000EAA19 File Offset: 0x000E8C19
		protected WebPartZoneBase Zone
		{
			get
			{
				return this._zone;
			}
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x000EAA24 File Offset: 0x000E8C24
		private Style CreateChromeStyleNoBorder(Style partChromeStyle)
		{
			Style style = new Style();
			style.CopyFrom(this.Zone.PartChromeStyle);
			if (style.BorderStyle != BorderStyle.NotSet)
			{
				style.BorderStyle = BorderStyle.NotSet;
			}
			if (style.BorderWidth != Unit.Empty)
			{
				style.BorderWidth = Unit.Empty;
			}
			if (style.BorderColor != Color.Empty)
			{
				style.BorderColor = Color.Empty;
			}
			return style;
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x000EAA94 File Offset: 0x000E8C94
		private Style CreateChromeStyleWithBorder(Style partChromeStyle)
		{
			Style style = new Style();
			style.CopyFrom(partChromeStyle);
			if (style.BorderStyle == BorderStyle.NotSet)
			{
				style.BorderStyle = BorderStyle.Solid;
			}
			if (style.BorderWidth == Unit.Empty)
			{
				style.BorderWidth = Unit.Pixel(1);
			}
			if (style.BorderColor == Color.Empty)
			{
				style.BorderColor = Color.Black;
			}
			return style;
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x000EAAFC File Offset: 0x000E8CFC
		private Style CreateTitleTextStyle(Style partTitleStyle)
		{
			Style style = new Style();
			if (partTitleStyle.ForeColor != Color.Empty)
			{
				style.ForeColor = partTitleStyle.ForeColor;
			}
			style.Font.CopyFrom(partTitleStyle.Font);
			return style;
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x000EAB40 File Offset: 0x000E8D40
		private Style CreateTitleStyleWithoutFontOrAlign(Style partTitleStyle)
		{
			Style style = new Style();
			style.CopyFrom(partTitleStyle);
			style.Font.Reset();
			if (style.ForeColor != Color.Empty)
			{
				style.ForeColor = Color.Empty;
			}
			return style;
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x000EAB84 File Offset: 0x000E8D84
		protected virtual Style CreateWebPartChromeStyle(WebPart webPart, PartChromeType chromeType)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			if (chromeType < PartChromeType.Default || chromeType > PartChromeType.BorderOnly)
			{
				throw new ArgumentOutOfRangeException("chromeType");
			}
			Style style;
			if (chromeType == PartChromeType.BorderOnly || chromeType == PartChromeType.TitleAndBorder)
			{
				if (this._chromeStyleWithBorder == null)
				{
					this._chromeStyleWithBorder = this.CreateChromeStyleWithBorder(this.Zone.PartChromeStyle);
				}
				style = this._chromeStyleWithBorder;
			}
			else
			{
				if (this._chromeStyleNoBorder == null)
				{
					this._chromeStyleNoBorder = this.CreateChromeStyleNoBorder(this.Zone.PartChromeStyle);
				}
				style = this._chromeStyleNoBorder;
			}
			if (this.WebPartManager != null && webPart == this.WebPartManager.SelectedWebPart)
			{
				Style style2 = new Style();
				style2.CopyFrom(style);
				style2.CopyFrom(this.Zone.SelectedPartChromeStyle);
				return style2;
			}
			return style;
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x000EAC40 File Offset: 0x000E8E40
		private string GenerateDescriptionText(WebPart webPart)
		{
			string text = webPart.DisplayTitle;
			string description = webPart.Description;
			if (!string.IsNullOrEmpty(description))
			{
				text = text + " - " + description;
			}
			return text;
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x000EAC74 File Offset: 0x000E8E74
		private string GenerateTitleText(WebPart webPart)
		{
			string text = webPart.DisplayTitle;
			string subtitle = webPart.Subtitle;
			if (!string.IsNullOrEmpty(subtitle))
			{
				text = text + " - " + subtitle;
			}
			return text;
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x000EACA5 File Offset: 0x000E8EA5
		protected string GetWebPartChromeClientID(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			return webPart.WholePartID;
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x000EACBB File Offset: 0x000E8EBB
		protected string GetWebPartTitleClientID(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			return webPart.TitleBarID;
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x000EACD1 File Offset: 0x000E8ED1
		protected virtual WebPartVerbCollection GetWebPartVerbs(WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			return this.Zone.VerbsForWebPart(webPart);
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x000EACF0 File Offset: 0x000E8EF0
		protected virtual WebPartVerbCollection FilterWebPartVerbs(WebPartVerbCollection verbs, WebPart webPart)
		{
			if (verbs == null)
			{
				throw new ArgumentNullException("verbs");
			}
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			WebPartVerbCollection webPartVerbCollection = new WebPartVerbCollection();
			foreach (object obj in verbs)
			{
				WebPartVerb webPartVerb = (WebPartVerb)obj;
				if (this.ShouldRenderVerb(webPartVerb, webPart))
				{
					webPartVerbCollection.Add(webPartVerb);
				}
			}
			return webPartVerbCollection;
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x000EAD74 File Offset: 0x000E8F74
		private void RegisterStyle(Style style)
		{
			if (!style.IsEmpty)
			{
				string clientID = this.Zone.ClientID;
				string str = "_";
				int cssStyleIndex = this._cssStyleIndex;
				this._cssStyleIndex = cssStyleIndex + 1;
				string text = clientID + str + cssStyleIndex.ToString(NumberFormatInfo.InvariantInfo);
				this._page.Header.StyleSheet.CreateStyleRule(style, this.Zone, "." + text);
				style.SetRegisteredCssClass(text);
			}
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x000EADEC File Offset: 0x000E8FEC
		public virtual void PerformPreRender()
		{
			if (this._page != null && this._page.SupportsStyleSheets)
			{
				Style partChromeStyle = this.Zone.PartChromeStyle;
				Style partTitleStyle = this.Zone.PartTitleStyle;
				this._chromeStyleWithBorder = this.CreateChromeStyleWithBorder(partChromeStyle);
				this.RegisterStyle(this._chromeStyleWithBorder);
				this._chromeStyleNoBorder = this.CreateChromeStyleNoBorder(partChromeStyle);
				this.RegisterStyle(this._chromeStyleNoBorder);
				this._titleTextStyle = this.CreateTitleTextStyle(partTitleStyle);
				this.RegisterStyle(this._titleTextStyle);
				this._titleStyleWithoutFontOrAlign = this.CreateTitleStyleWithoutFontOrAlign(partTitleStyle);
				this.RegisterStyle(this._titleStyleWithoutFontOrAlign);
				if (this.Zone.RenderClientScript && this.Zone.WebPartVerbRenderMode == WebPartVerbRenderMode.Menu && this.Zone.Menu != null)
				{
					this.Zone.Menu.RegisterStyles();
				}
			}
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x000EAEC8 File Offset: 0x000E90C8
		protected virtual void RenderPartContents(HtmlTextWriter writer, WebPart webPart)
		{
			if (!string.IsNullOrEmpty(webPart.ConnectErrorMessage))
			{
				if (!this.Zone.ErrorStyle.IsEmpty)
				{
					this.Zone.ErrorStyle.AddAttributesToRender(writer, this.Zone);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.WriteEncodedText(webPart.ConnectErrorMessage);
				writer.RenderEndTag();
				return;
			}
			webPart.RenderControl(writer);
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x000EAF30 File Offset: 0x000E9130
		private void RenderTitleBar(HtmlTextWriter writer, WebPart webPart)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			int num = 1;
			bool showTitleIcons = this.Zone.ShowTitleIcons;
			string value = null;
			if (showTitleIcons)
			{
				value = webPart.TitleIconImageUrl;
				if (!string.IsNullOrEmpty(value))
				{
					num++;
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
					this.RenderTitleIcon(writer, webPart);
					writer.RenderEndTag();
				}
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			TableItemStyle partTitleStyle = this.Zone.PartTitleStyle;
			if (!partTitleStyle.Wrap)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
			}
			HorizontalAlign horizontalAlign = partTitleStyle.HorizontalAlign;
			if (horizontalAlign != HorizontalAlign.NotSet)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(HorizontalAlign));
				writer.AddAttribute(HtmlTextWriterAttribute.Align, converter.ConvertToString(horizontalAlign).ToLower(CultureInfo.InvariantCulture));
			}
			VerticalAlign verticalAlign = partTitleStyle.VerticalAlign;
			if (verticalAlign != VerticalAlign.NotSet)
			{
				TypeConverter converter2 = TypeDescriptor.GetConverter(typeof(VerticalAlign));
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, converter2.ConvertToString(verticalAlign).ToLower(CultureInfo.InvariantCulture));
			}
			if (this.Zone.RenderClientScript)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.GetWebPartTitleClientID(webPart));
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (showTitleIcons && !string.IsNullOrEmpty(value))
			{
				writer.Write("&nbsp;");
			}
			this.RenderTitleText(writer, webPart);
			writer.RenderEndTag();
			this.RenderVerbsInTitleBar(writer, webPart, num);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x000EB0BE File Offset: 0x000E92BE
		private void RenderTitleIcon(HtmlTextWriter writer, WebPart webPart)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Src, this.Zone.ResolveClientUrl(webPart.TitleIconImageUrl));
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.GenerateDescriptionText(webPart));
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}

		// Token: 0x0600476F RID: 18287 RVA: 0x000EB0F8 File Offset: 0x000E92F8
		private void RenderTitleText(HtmlTextWriter writer, WebPart webPart)
		{
			if (this._titleTextStyle == null)
			{
				this._titleTextStyle = this.CreateTitleTextStyle(this.Zone.PartTitleStyle);
			}
			if (!this._titleTextStyle.IsEmpty)
			{
				this._titleTextStyle.AddAttributesToRender(writer, this.Zone);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Title, this.GenerateDescriptionText(webPart), true);
			string titleUrl = webPart.TitleUrl;
			string text = this.GenerateTitleText(webPart);
			if (!string.IsNullOrEmpty(titleUrl) && !this.DragDropEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, this.Zone.ResolveClientUrl(titleUrl));
				writer.RenderBeginTag(HtmlTextWriterTag.A);
			}
			else
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			writer.WriteEncodedText(text);
			writer.RenderEndTag();
			writer.Write("&nbsp;");
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x000EB1B0 File Offset: 0x000E93B0
		private void RenderVerb(HtmlTextWriter writer, WebPart webPart, WebPartVerb verb)
		{
			bool flag = this.Zone.IsEnabled && verb.Enabled;
			ButtonType titleBarVerbButtonType = this.Zone.TitleBarVerbButtonType;
			WebControl webControl;
			if (verb == this.Zone.HelpVerb)
			{
				string text = this.Zone.ResolveClientUrl(webPart.HelpUrl);
				if (titleBarVerbButtonType == ButtonType.Button)
				{
					ZoneButton zoneButton = new ZoneButton(this.Zone, null);
					if (flag)
					{
						if (this.Zone.RenderClientScript)
						{
							zoneButton.OnClientClick = string.Concat(new string[]
							{
								"__wpm.ShowHelp('",
								Util.QuoteJScriptString(text),
								"', ",
								((int)webPart.HelpMode).ToString(CultureInfo.InvariantCulture),
								");return;"
							});
						}
						else if (webPart.HelpMode != WebPartHelpMode.Navigate)
						{
							zoneButton.OnClientClick = "window.open('" + Util.QuoteJScriptString(text) + "', '_blank', 'scrollbars=yes,resizable=yes,status=no,toolbar=no,menubar=no,location=no');return;";
						}
						else
						{
							zoneButton.OnClientClick = "window.location.href='" + Util.QuoteJScriptString(text) + "';return;";
						}
					}
					zoneButton.Text = verb.Text;
					webControl = zoneButton;
				}
				else
				{
					HyperLink hyperLink = new HyperLink();
					switch (webPart.HelpMode)
					{
					case WebPartHelpMode.Modal:
						if (this.Zone.RenderClientScript)
						{
							hyperLink.NavigateUrl = "javascript:__wpm.ShowHelp('" + Util.QuoteJScriptString(text) + "', 0)";
							goto IL_17E;
						}
						break;
					case WebPartHelpMode.Modeless:
						break;
					case WebPartHelpMode.Navigate:
						hyperLink.NavigateUrl = text;
						goto IL_17E;
					default:
						goto IL_17E;
					}
					hyperLink.NavigateUrl = text;
					hyperLink.Target = "_blank";
					IL_17E:
					hyperLink.Text = verb.Text;
					if (titleBarVerbButtonType == ButtonType.Image)
					{
						hyperLink.ImageUrl = verb.ImageUrl;
					}
					webControl = hyperLink;
				}
			}
			else if (verb == this.Zone.ExportVerb)
			{
				string exportUrl = this._manager.GetExportUrl(webPart);
				if (titleBarVerbButtonType == ButtonType.Button)
				{
					ZoneButton zoneButton2 = new ZoneButton(this.Zone, string.Empty);
					zoneButton2.Text = verb.Text;
					if (flag)
					{
						if (webPart.ExportMode == WebPartExportMode.All && this._personalizationScope == PersonalizationScope.User)
						{
							if (this.Zone.RenderClientScript)
							{
								zoneButton2.OnClientClick = "__wpm.ExportWebPart('" + Util.QuoteJScriptString(exportUrl) + "', true, false);return false;";
							}
							else
							{
								zoneButton2.OnClientClick = "if(__wpmExportWarning.length == 0 || confirm(__wpmExportWarning)){window.location='" + Util.QuoteJScriptString(exportUrl) + "';}return false;";
							}
						}
						else
						{
							zoneButton2.OnClientClick = "window.location='" + Util.QuoteJScriptString(exportUrl) + "';return false;";
						}
					}
					webControl = zoneButton2;
				}
				else
				{
					HyperLink hyperLink2 = new HyperLink();
					hyperLink2.Text = verb.Text;
					if (titleBarVerbButtonType == ButtonType.Image)
					{
						hyperLink2.ImageUrl = verb.ImageUrl;
					}
					hyperLink2.NavigateUrl = exportUrl;
					if (webPart.ExportMode == WebPartExportMode.All)
					{
						if (this.Zone.RenderClientScript)
						{
							hyperLink2.Attributes.Add("onclick", "return __wpm.ExportWebPart('', true, true)");
						}
						else
						{
							string value = "return (__wpmExportWarning.length == 0 || confirm(__wpmExportWarning))";
							hyperLink2.Attributes.Add("onclick", value);
						}
					}
					webControl = hyperLink2;
				}
			}
			else
			{
				string eventArgument = verb.GetEventArgument(webPart.ID);
				string clientClickHandler = verb.ClientClickHandler;
				if (titleBarVerbButtonType == ButtonType.Button)
				{
					ZoneButton zoneButton3 = new ZoneButton(this.Zone, eventArgument);
					zoneButton3.Text = verb.Text;
					if (!string.IsNullOrEmpty(clientClickHandler) && flag)
					{
						zoneButton3.OnClientClick = clientClickHandler;
					}
					webControl = zoneButton3;
				}
				else
				{
					ZoneLinkButton zoneLinkButton = new ZoneLinkButton(this.Zone, eventArgument);
					zoneLinkButton.Text = verb.Text;
					if (titleBarVerbButtonType == ButtonType.Image)
					{
						zoneLinkButton.ImageUrl = verb.ImageUrl;
					}
					if (!string.IsNullOrEmpty(clientClickHandler) && flag)
					{
						zoneLinkButton.OnClientClick = clientClickHandler;
					}
					webControl = zoneLinkButton;
				}
				if (this._manager != null && flag)
				{
					if (verb == this.Zone.CloseVerb)
					{
						ProviderConnectionPointCollection providerConnectionPoints = this._manager.GetProviderConnectionPoints(webPart);
						if (providerConnectionPoints != null && providerConnectionPoints.Count > 0 && this.Connections.ContainsProvider(webPart))
						{
							string value2 = "if (__wpmCloseProviderWarning.length >= 0 && !confirm(__wpmCloseProviderWarning)) { return false; }";
							webControl.Attributes.Add("onclick", value2);
						}
					}
					else if (verb == this.Zone.DeleteVerb)
					{
						string value3 = "if (__wpmDeleteWarning.length >= 0 && !confirm(__wpmDeleteWarning)) { return false; }";
						webControl.Attributes.Add("onclick", value3);
					}
				}
			}
			webControl.ApplyStyle(this.Zone.TitleBarVerbStyle);
			webControl.ToolTip = string.Format(CultureInfo.CurrentCulture, verb.Description, new object[]
			{
				webPart.DisplayTitle
			});
			webControl.Enabled = verb.Enabled;
			webControl.Page = this._page;
			webControl.RenderControl(writer);
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x000EB61C File Offset: 0x000E981C
		private void RenderVerbs(HtmlTextWriter writer, WebPart webPart, WebPartVerbCollection verbs)
		{
			if (verbs == null)
			{
				throw new ArgumentNullException("verbs");
			}
			WebPartVerb webPartVerb = null;
			foreach (object obj in verbs)
			{
				WebPartVerb webPartVerb2 = (WebPartVerb)obj;
				if (webPartVerb != null && (this.VerbRenderedAsLinkButton(webPartVerb2) || this.VerbRenderedAsLinkButton(webPartVerb)))
				{
					writer.Write("&nbsp;");
				}
				this.RenderVerb(writer, webPart, webPartVerb2);
				webPartVerb = webPartVerb2;
			}
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x000EB6A4 File Offset: 0x000E98A4
		private void RenderVerbsInTitleBar(HtmlTextWriter writer, WebPart webPart, int colspan)
		{
			WebPartVerbCollection webPartVerbCollection = this.GetWebPartVerbs(webPart);
			webPartVerbCollection = this.FilterWebPartVerbs(webPartVerbCollection, webPart);
			if (webPartVerbCollection != null && webPartVerbCollection.Count > 0)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
				colspan++;
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				if (this.Zone.RenderClientScript && this.Zone.WebPartVerbRenderMode == WebPartVerbRenderMode.Menu && this.Zone.Menu != null)
				{
					if (this._designMode)
					{
						this.Zone.Menu.Render(writer, webPart.WholePartID + "Verbs");
					}
					else
					{
						this.Zone.Menu.Render(writer, webPartVerbCollection, webPart.WholePartID + "Verbs", webPart, this.WebPartManager);
					}
				}
				else
				{
					this.RenderVerbs(writer, webPart, webPartVerbCollection);
				}
				writer.RenderEndTag();
			}
		}

		// Token: 0x06004773 RID: 18291 RVA: 0x000EB77C File Offset: 0x000E997C
		public virtual void RenderWebPart(HtmlTextWriter writer, WebPart webPart)
		{
			if (webPart == null)
			{
				throw new ArgumentNullException("webPart");
			}
			bool flag = this.Zone.LayoutOrientation == Orientation.Vertical;
			PartChromeType effectiveChromeType = this.Zone.GetEffectiveChromeType(webPart);
			Style style = this.CreateWebPartChromeStyle(webPart, effectiveChromeType);
			if (!style.IsEmpty)
			{
				style.AddAttributesToRender(writer, this.Zone);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "2");
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			if (flag)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			}
			else if (webPart.ChromeState != PartChromeState.Minimized)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
			}
			if (this.Zone.RenderClientScript)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.GetWebPartChromeClientID(webPart));
			}
			if (!this._designMode && webPart.Hidden && this.WebPartManager != null && !this.WebPartManager.DisplayMode.ShowHiddenWebParts)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			if (effectiveChromeType == PartChromeType.TitleOnly || effectiveChromeType == PartChromeType.TitleAndBorder)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				if (this._titleStyleWithoutFontOrAlign == null)
				{
					this._titleStyleWithoutFontOrAlign = this.CreateTitleStyleWithoutFontOrAlign(this.Zone.PartTitleStyle);
				}
				if (!this._titleStyleWithoutFontOrAlign.IsEmpty)
				{
					this._titleStyleWithoutFontOrAlign.AddAttributesToRender(writer, this.Zone);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				this.RenderTitleBar(writer, webPart);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			if (webPart.ChromeState == PartChromeState.Minimized)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (!flag)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
				writer.AddAttribute(HtmlTextWriterAttribute.Valign, "top");
			}
			Style partStyle = this.Zone.PartStyle;
			if (!partStyle.IsEmpty)
			{
				partStyle.AddAttributesToRender(writer, this.Zone);
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, this.Zone.PartChromePadding.ToString());
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			this.RenderPartContents(writer, webPart);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06004774 RID: 18292 RVA: 0x000EB984 File Offset: 0x000E9B84
		private bool ShouldRenderVerb(WebPartVerb verb, WebPart webPart)
		{
			if (verb == null)
			{
				return false;
			}
			if (!verb.Visible)
			{
				return false;
			}
			if (verb == this.Zone.CloseVerb && (!this._personalizationEnabled || !webPart.AllowClose || !this.Zone.AllowLayoutChange))
			{
				return false;
			}
			if (verb == this.Zone.ConnectVerb && this.WebPartManager != null)
			{
				if (this.WebPartManager.DisplayMode != WebPartManager.ConnectDisplayMode || webPart == this.WebPartManager.SelectedWebPart || !webPart.AllowConnect)
				{
					return false;
				}
				ConsumerConnectionPointCollection enabledConsumerConnectionPoints = this.WebPartManager.GetEnabledConsumerConnectionPoints(webPart);
				ProviderConnectionPointCollection enabledProviderConnectionPoints = this.WebPartManager.GetEnabledProviderConnectionPoints(webPart);
				if ((enabledConsumerConnectionPoints == null || enabledConsumerConnectionPoints.Count == 0) && (enabledProviderConnectionPoints == null || enabledProviderConnectionPoints.Count == 0))
				{
					return false;
				}
			}
			return (verb != this.Zone.DeleteVerb || (this._personalizationEnabled && this.Zone.AllowLayoutChange && !webPart.IsStatic && (!webPart.IsShared || this._personalizationScope != PersonalizationScope.User) && (this.WebPartManager == null || this.WebPartManager.DisplayMode.AllowPageDesign))) && (verb != this.Zone.EditVerb || this.WebPartManager == null || (this.WebPartManager.DisplayMode == WebPartManager.EditDisplayMode && webPart != this.WebPartManager.SelectedWebPart)) && (verb != this.Zone.HelpVerb || !string.IsNullOrEmpty(webPart.HelpUrl)) && (verb != this.Zone.MinimizeVerb || (this._personalizationEnabled && webPart.ChromeState != PartChromeState.Minimized && webPart.AllowMinimize && this.Zone.AllowLayoutChange)) && (verb != this.Zone.RestoreVerb || (this._personalizationEnabled && webPart.ChromeState != PartChromeState.Normal && this.Zone.AllowLayoutChange)) && (verb != this.Zone.ExportVerb || (this._personalizationEnabled && webPart.ExportMode != WebPartExportMode.None));
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x000EBB6F File Offset: 0x000E9D6F
		private bool VerbRenderedAsLinkButton(WebPartVerb verb)
		{
			return this.Zone.TitleBarVerbButtonType == ButtonType.Link || string.IsNullOrEmpty(verb.ImageUrl);
		}

		// Token: 0x040026EC RID: 9964
		private const string titleSeparator = " - ";

		// Token: 0x040026ED RID: 9965
		private const string descriptionSeparator = " - ";

		// Token: 0x040026EE RID: 9966
		private WebPartManager _manager;

		// Token: 0x040026EF RID: 9967
		private WebPartConnectionCollection _connections;

		// Token: 0x040026F0 RID: 9968
		private WebPartZoneBase _zone;

		// Token: 0x040026F1 RID: 9969
		private Page _page;

		// Token: 0x040026F2 RID: 9970
		private bool _designMode;

		// Token: 0x040026F3 RID: 9971
		private bool _personalizationEnabled;

		// Token: 0x040026F4 RID: 9972
		private PersonalizationScope _personalizationScope;

		// Token: 0x040026F5 RID: 9973
		private Style _chromeStyleWithBorder;

		// Token: 0x040026F6 RID: 9974
		private Style _chromeStyleNoBorder;

		// Token: 0x040026F7 RID: 9975
		private Style _titleTextStyle;

		// Token: 0x040026F8 RID: 9976
		private Style _titleStyleWithoutFontOrAlign;

		// Token: 0x040026F9 RID: 9977
		private int _cssStyleIndex;
	}
}

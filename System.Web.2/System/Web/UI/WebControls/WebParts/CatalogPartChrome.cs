using System;
using System.Drawing;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200052C RID: 1324
	public class CatalogPartChrome
	{
		// Token: 0x06004319 RID: 17177 RVA: 0x000DCA0D File Offset: 0x000DAC0D
		public CatalogPartChrome(CatalogZoneBase zone)
		{
			if (zone == null)
			{
				throw new ArgumentNullException("zone");
			}
			this._zone = zone;
			this._page = zone.Page;
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x0600431A RID: 17178 RVA: 0x000DCA36 File Offset: 0x000DAC36
		protected CatalogZoneBase Zone
		{
			get
			{
				return this._zone;
			}
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x000DCA40 File Offset: 0x000DAC40
		protected virtual Style CreateCatalogPartChromeStyle(CatalogPart catalogPart, PartChromeType chromeType)
		{
			if (catalogPart == null)
			{
				throw new ArgumentNullException("catalogPart");
			}
			if (chromeType < PartChromeType.Default || chromeType > PartChromeType.BorderOnly)
			{
				throw new ArgumentOutOfRangeException("chromeType");
			}
			if (chromeType == PartChromeType.BorderOnly || chromeType == PartChromeType.TitleAndBorder)
			{
				if (this._chromeStyleWithBorder == null)
				{
					Style style = new Style();
					style.CopyFrom(this.Zone.PartChromeStyle);
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
					this._chromeStyleWithBorder = style;
				}
				return this._chromeStyleWithBorder;
			}
			if (this._chromeStyleNoBorder == null)
			{
				Style style2 = new Style();
				style2.CopyFrom(this.Zone.PartChromeStyle);
				if (style2.BorderStyle != BorderStyle.NotSet)
				{
					style2.BorderStyle = BorderStyle.NotSet;
				}
				if (style2.BorderWidth != Unit.Empty)
				{
					style2.BorderWidth = Unit.Empty;
				}
				if (style2.BorderColor != Color.Empty)
				{
					style2.BorderColor = Color.Empty;
				}
				this._chromeStyleNoBorder = style2;
			}
			return this._chromeStyleNoBorder;
		}

		// Token: 0x0600431C RID: 17180 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void PerformPreRender()
		{
		}

		// Token: 0x0600431D RID: 17181 RVA: 0x000DCB64 File Offset: 0x000DAD64
		public virtual void RenderCatalogPart(HtmlTextWriter writer, CatalogPart catalogPart)
		{
			if (catalogPart == null)
			{
				throw new ArgumentNullException("catalogPart");
			}
			PartChromeType effectiveChromeType = this.Zone.GetEffectiveChromeType(catalogPart);
			Style style = this.CreateCatalogPartChromeStyle(catalogPart, effectiveChromeType);
			if (!style.IsEmpty)
			{
				style.AddAttributesToRender(writer, this.Zone);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
			writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "2");
			writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			if (effectiveChromeType == PartChromeType.TitleOnly || effectiveChromeType == PartChromeType.TitleAndBorder)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				Style partTitleStyle = this.Zone.PartTitleStyle;
				if (!partTitleStyle.IsEmpty)
				{
					partTitleStyle.AddAttributesToRender(writer, this.Zone);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				this.RenderTitle(writer, catalogPart);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			if (catalogPart.ChromeState != PartChromeState.Minimized)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				Style partStyle = this.Zone.PartStyle;
				if (!partStyle.IsEmpty)
				{
					partStyle.AddAttributesToRender(writer, this.Zone);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Td);
				this.RenderPartContents(writer, catalogPart);
				this.RenderItems(writer, catalogPart);
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x000DCC8C File Offset: 0x000DAE8C
		private void RenderItem(HtmlTextWriter writer, WebPartDescription webPartDescription)
		{
			string text = webPartDescription.Description;
			if (string.IsNullOrEmpty(text))
			{
				text = webPartDescription.Title;
			}
			this.RenderItemCheckBox(writer, webPartDescription.ID);
			writer.Write("&nbsp;");
			if (this.Zone.ShowCatalogIcons)
			{
				string catalogIconImageUrl = webPartDescription.CatalogIconImageUrl;
				if (!string.IsNullOrEmpty(catalogIconImageUrl))
				{
					this.RenderItemIcon(writer, catalogIconImageUrl, text);
					writer.Write("&nbsp;");
				}
			}
			this.RenderItemText(writer, webPartDescription.ID, webPartDescription.Title, text);
			writer.WriteBreak();
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x000DCD14 File Offset: 0x000DAF14
		private void RenderItemCheckBox(HtmlTextWriter writer, string value)
		{
			this.Zone.EditUIStyle.AddAttributesToRender(writer, this.Zone);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.Zone.GetCheckBoxID(value));
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.Zone.CheckBoxName);
			writer.AddAttribute(HtmlTextWriterAttribute.Value, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
			if (this._page != null)
			{
				this._page.ClientScript.RegisterForEventValidation(this.Zone.CheckBoxName);
			}
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x000DCDA8 File Offset: 0x000DAFA8
		private void RenderItemIcon(HtmlTextWriter writer, string iconUrl, string description)
		{
			new Image
			{
				AlternateText = description,
				ImageUrl = iconUrl,
				BorderStyle = BorderStyle.None,
				Page = this._page
			}.RenderControl(writer);
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x000DCDE4 File Offset: 0x000DAFE4
		private void RenderItemText(HtmlTextWriter writer, string value, string text, string description)
		{
			this.Zone.LabelStyle.AddAttributesToRender(writer, this.Zone);
			writer.AddAttribute(HtmlTextWriterAttribute.For, this.Zone.GetCheckBoxID(value));
			writer.AddAttribute(HtmlTextWriterAttribute.Title, description, true);
			writer.RenderBeginTag(HtmlTextWriterTag.Label);
			writer.WriteEncodedText(text);
			writer.RenderEndTag();
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x000DCE3C File Offset: 0x000DB03C
		private void RenderItems(HtmlTextWriter writer, CatalogPart catalogPart)
		{
			WebPartDescriptionCollection availableWebPartDescriptions = catalogPart.GetAvailableWebPartDescriptions();
			if (availableWebPartDescriptions != null)
			{
				foreach (object obj in availableWebPartDescriptions)
				{
					WebPartDescription webPartDescription = (WebPartDescription)obj;
					this.RenderItem(writer, webPartDescription);
				}
			}
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x000DCE9C File Offset: 0x000DB09C
		protected virtual void RenderPartContents(HtmlTextWriter writer, CatalogPart catalogPart)
		{
			if (catalogPart == null)
			{
				throw new ArgumentNullException("catalogPart");
			}
			catalogPart.RenderControl(writer);
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x000DCEB4 File Offset: 0x000DB0B4
		private void RenderTitle(HtmlTextWriter writer, CatalogPart catalogPart)
		{
			new Label
			{
				Text = catalogPart.DisplayTitle,
				ToolTip = catalogPart.Description,
				Page = this._page
			}.RenderControl(writer);
		}

		// Token: 0x040025C1 RID: 9665
		private CatalogZoneBase _zone;

		// Token: 0x040025C2 RID: 9666
		private Page _page;

		// Token: 0x040025C3 RID: 9667
		private Style _chromeStyleWithBorder;

		// Token: 0x040025C4 RID: 9668
		private Style _chromeStyleNoBorder;
	}
}

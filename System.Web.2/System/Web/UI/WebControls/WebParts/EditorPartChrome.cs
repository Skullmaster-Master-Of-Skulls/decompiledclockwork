using System;
using System.Drawing;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000539 RID: 1337
	public class EditorPartChrome
	{
		// Token: 0x06004448 RID: 17480 RVA: 0x000E21E2 File Offset: 0x000E03E2
		public EditorPartChrome(EditorZoneBase zone)
		{
			if (zone == null)
			{
				throw new ArgumentNullException("zone");
			}
			this._zone = zone;
		}

		// Token: 0x17001413 RID: 5139
		// (get) Token: 0x06004449 RID: 17481 RVA: 0x000E21FF File Offset: 0x000E03FF
		protected EditorZoneBase Zone
		{
			get
			{
				return this._zone;
			}
		}

		// Token: 0x0600444A RID: 17482 RVA: 0x000E2208 File Offset: 0x000E0408
		protected virtual Style CreateEditorPartChromeStyle(EditorPart editorPart, PartChromeType chromeType)
		{
			if (editorPart == null)
			{
				throw new ArgumentNullException("editorPart");
			}
			if (chromeType < PartChromeType.Default || chromeType > PartChromeType.BorderOnly)
			{
				throw new ArgumentOutOfRangeException("chromeType");
			}
			if (chromeType == PartChromeType.BorderOnly || chromeType == PartChromeType.TitleAndBorder)
			{
				return this.Zone.PartChromeStyle;
			}
			if (this._chromeStyleNoBorder == null)
			{
				Style style = new Style();
				style.CopyFrom(this.Zone.PartChromeStyle);
				if (style.BorderStyle != BorderStyle.None)
				{
					style.BorderStyle = BorderStyle.None;
				}
				if (style.BorderWidth != Unit.Empty)
				{
					style.BorderWidth = Unit.Empty;
				}
				if (style.BorderColor != Color.Empty)
				{
					style.BorderColor = Color.Empty;
				}
				this._chromeStyleNoBorder = style;
			}
			return this._chromeStyleNoBorder;
		}

		// Token: 0x0600444B RID: 17483 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void PerformPreRender()
		{
		}

		// Token: 0x0600444C RID: 17484 RVA: 0x000E22C0 File Offset: 0x000E04C0
		public virtual void RenderEditorPart(HtmlTextWriter writer, EditorPart editorPart)
		{
			if (editorPart == null)
			{
				throw new ArgumentNullException("editorPart");
			}
			PartChromeType effectiveChromeType = this.Zone.GetEffectiveChromeType(editorPart);
			Style style = this.CreateEditorPartChromeStyle(editorPart, effectiveChromeType);
			if (!style.IsEmpty)
			{
				style.AddAttributesToRender(writer, this.Zone);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
			if (effectiveChromeType == PartChromeType.TitleAndBorder || effectiveChromeType == PartChromeType.TitleOnly)
			{
				this.RenderTitle(writer, editorPart);
			}
			if (editorPart.ChromeState != PartChromeState.Minimized)
			{
				Style partStyle = this.Zone.PartStyle;
				if (!partStyle.IsEmpty)
				{
					partStyle.AddAttributesToRender(writer, this.Zone);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				this.RenderPartContents(writer, editorPart);
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x000E2364 File Offset: 0x000E0564
		protected virtual void RenderPartContents(HtmlTextWriter writer, EditorPart editorPart)
		{
			string accessKey = editorPart.AccessKey;
			if (!string.IsNullOrEmpty(accessKey))
			{
				editorPart.AccessKey = string.Empty;
			}
			editorPart.RenderControl(writer);
			if (!string.IsNullOrEmpty(accessKey))
			{
				editorPart.AccessKey = accessKey;
			}
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x000E23A4 File Offset: 0x000E05A4
		private void RenderTitle(HtmlTextWriter writer, EditorPart editorPart)
		{
			string displayTitle = editorPart.DisplayTitle;
			if (string.IsNullOrEmpty(displayTitle))
			{
				return;
			}
			TableItemStyle partTitleStyle = this.Zone.PartTitleStyle;
			if (this._titleTextStyle == null)
			{
				Style style = new Style();
				style.CopyFrom(partTitleStyle);
				this._titleTextStyle = style;
			}
			if (!this._titleTextStyle.IsEmpty)
			{
				this._titleTextStyle.AddAttributesToRender(writer, this.Zone);
			}
			string description = editorPart.Description;
			if (!string.IsNullOrEmpty(description))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, description);
			}
			string accessKey = editorPart.AccessKey;
			if (!string.IsNullOrEmpty(accessKey))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, accessKey);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Legend);
			writer.Write(displayTitle);
			writer.RenderEndTag();
		}

		// Token: 0x04002624 RID: 9764
		private EditorZoneBase _zone;

		// Token: 0x04002625 RID: 9765
		private Style _chromeStyleNoBorder;

		// Token: 0x04002626 RID: 9766
		private Style _titleTextStyle;
	}
}

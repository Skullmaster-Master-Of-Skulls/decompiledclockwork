using System;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ButtonRendering.Classic
{
	// Token: 0x020000E1 RID: 225
	internal class ClassicRenderer : ButtonRendererBase
	{
		// Token: 0x06000950 RID: 2384 RVA: 0x000215BD File Offset: 0x0001F7BD
		public ClassicRenderer(RadButton owner) : base(owner)
		{
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x000215C8 File Offset: 0x0001F7C8
		public override string CssClassFormatString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				RadButton owner = base.Owner;
				RadButtonToggleState selectedToggleState = owner.SelectedToggleState;
				bool flag = selectedToggleState != null;
				bool flag2 = owner.ToggleType == ButtonToggleType.CheckBox || owner.ToggleType == ButtonToggleType.Radio;
				if (owner.Image.EnableImageButton || (flag && !string.IsNullOrEmpty(selectedToggleState.ImageUrl)))
				{
					stringBuilder.Append(" rbImageButton");
				}
				else if (owner.ButtonType == RadButtonType.ToggleButton)
				{
					stringBuilder.Append(" rbToggleButton");
					if (!flag2)
					{
						stringBuilder.Append(" rbTextButton");
					}
				}
				else if (owner.ButtonType == RadButtonType.StandardButton)
				{
					string value = string.Empty;
					string value2 = string.Empty;
					if (owner.Height == Unit.Pixel(22) || owner.Height == Unit.Empty)
					{
						value = " rbSkinnedButton";
						value2 = " rbSkinnedButtonChecked";
					}
					else
					{
						value = " rbVerticalButton";
						value2 = " rbVerticalButtonChecked";
					}
					stringBuilder.Append(value);
					if (flag2 && owner.Checked)
					{
						stringBuilder.Append(value2);
					}
					if (owner.EnableBrowserButtonStyle)
					{
						stringBuilder.Append(" rbNativeButton");
					}
				}
				else if (owner.ButtonType == RadButtonType.LinkButton || owner.ButtonType == RadButtonType.SkinnedButton)
				{
					stringBuilder.Append(" rbLinkButton");
					if (owner.ButtonType == RadButtonType.SkinnedButton)
					{
						stringBuilder.Append(" rbRounded");
					}
					if (flag2 && owner.Checked)
					{
						stringBuilder.Append(" rbLinkButtonChecked");
					}
				}
				if (flag && !string.IsNullOrEmpty(selectedToggleState.CssClass))
				{
					stringBuilder.Append(" " + selectedToggleState.CssClass);
				}
				if (owner.ReadOnly && !string.IsNullOrEmpty(owner.ReadOnlyCssClass))
				{
					stringBuilder.Append(" " + owner.ReadOnlyCssClass);
				}
				return "RadButton RadButton_{0}" + stringBuilder.ToString() + ((!owner.IsButtonEnabled || !owner.OriginalEnabled) ? (" " + ("rbDisabled " + owner.DisabledButtonCssClass).Trim()) : "");
			}
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x000217D8 File Offset: 0x0001F9D8
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			RadButton owner = base.Owner;
			if (owner.Image.EnableImageButton)
			{
				string text = (!owner.OriginalEnabled && !string.IsNullOrEmpty(owner.Image.DisabledImageUrl)) ? owner.Image.DisabledImageUrl : owner.Image.ImageUrl;
				if (!string.IsNullOrEmpty(text))
				{
					base.Owner.Style.Add(HtmlTextWriterStyle.BackgroundImage, string.Format("'{0}'", owner.ResolveUrl(text)));
				}
				if (owner.Height != Unit.Empty)
				{
					base.Owner.Style.Add("line-height", owner.Height.ToString(CultureInfo.InvariantCulture));
				}
			}
			Unit left = owner.Height;
			if (owner.ToggleType != ButtonToggleType.None)
			{
				RadButtonToggleState selectedToggleState = owner.SelectedToggleState;
				if (selectedToggleState != null)
				{
					string imageUrl = selectedToggleState.ImageUrl;
					if (!string.IsNullOrEmpty(imageUrl))
					{
						base.Owner.Style.Add(HtmlTextWriterStyle.BackgroundImage, string.Format("'{0}'", owner.ResolveUrl(imageUrl)));
					}
					left = ((selectedToggleState.Height != Unit.Empty) ? selectedToggleState.Height : owner.Height);
					Unit left2 = (selectedToggleState.Width != Unit.Empty) ? selectedToggleState.Width : owner.Width;
					if (left2 != Unit.Empty)
					{
						base.Owner.Style.Add(HtmlTextWriterStyle.Width, left2.ToString(CultureInfo.InvariantCulture));
					}
				}
			}
			if (left != Unit.Empty)
			{
				if (owner.ButtonType != RadButtonType.StandardButton || owner.Image.EnableImageButton)
				{
					base.Owner.Style.Add("line-height", left.ToString(CultureInfo.InvariantCulture));
				}
				base.Owner.Style.Add(HtmlTextWriterStyle.Height, left.ToString(CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x000219B8 File Offset: 0x0001FBB8
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			RadButton owner = base.Owner;
			if (owner.InDesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(owner));
				base.Owner.Style.Add(HtmlTextWriterStyle.Position, "relative");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			if (owner.IsTemplateInitialized)
			{
				base.Owner.Style.Add("width", "0");
				base.Owner.Style.Add("height", "0");
				base.Owner.Style.Add("padding", "0");
				base.Owner.Style.Add("margin", "0");
				base.Owner.Style.Add("display", "inline");
				owner.RenderContentsBase(writer);
			}
			else
			{
				bool flag = owner.Image.EnableImageButton && !owner.Image.IsBackgroundImage;
				RadButtonType buttonType = owner.ButtonType;
				string text = "rbPrimaryIcon";
				bool flag2 = false;
				if (buttonType == RadButtonType.ToggleButton)
				{
					if (owner.ToggleType == ButtonToggleType.Radio)
					{
						flag2 = true;
						if (!owner.HasIconInState)
						{
							text = (owner.Checked ? (text + " rbToggleRadioChecked") : (text + " rbToggleRadio"));
						}
					}
					else if (owner.ToggleType == ButtonToggleType.CheckBox)
					{
						flag2 = true;
						if (!owner.HasIconInState)
						{
							text = (owner.Checked ? (text + " rbToggleCheckboxChecked") : (text + " rbToggleCheckbox"));
						}
					}
				}
				string text2 = string.Empty;
				if (flag)
				{
					text2 = "rbText rbHideElement";
				}
				else if (buttonType == RadButtonType.LinkButton || buttonType == RadButtonType.SkinnedButton || buttonType == RadButtonType.ToggleButton || owner.Image.EnableImageButton)
				{
					text2 = "rbText";
				}
				else if (buttonType == RadButtonType.StandardButton && !owner.EnableBrowserButtonStyle)
				{
					text2 = "rbDecorated";
				}
				if (owner.ToggleType == ButtonToggleType.None || owner.ToggleStates.Count < 2)
				{
					string text3 = string.Empty;
					bool showSecondaryIcon = owner.Icon.ShowSecondaryIcon;
					if (!flag && (flag2 || owner.Icon.ShowPrimaryIcon))
					{
						text3 += " rbPrimary";
						this.RenderIcon(writer, owner.Icon.PrimaryIconUrl, owner.Icon.PrimaryIconTop, owner.Icon.PrimaryIconBottom, owner.Icon.PrimaryIconLeft, owner.Icon.PrimaryIconRight, owner.Icon.PrimaryIconWidth, owner.Icon.PrimaryIconHeight, text, owner.Icon.PrimaryIconCssClass);
					}
					else if (owner.EnableSplitButton && owner.SplitButtonPosition == ButtonPosition.Left)
					{
						text3 += " rbPrimary";
						this.RenderIcon(writer, "rbSplitLeft", owner.SplitButtonCssClass);
					}
					bool flag3 = false;
					if (showSecondaryIcon)
					{
						text3 += " rbSecondary";
					}
					else if (owner.EnableSplitButton && owner.SplitButtonPosition == ButtonPosition.Right)
					{
						text3 += " rbSecondary";
						flag3 = true;
					}
					string cssClass = owner.Image.EnableImageButton ? text2 : (text2 + text3);
					this.RenderText(writer, cssClass);
					if (!flag && showSecondaryIcon)
					{
						this.RenderIcon(writer, owner.Icon.SecondaryIconUrl, owner.Icon.SecondaryIconTop, owner.Icon.SecondaryIconBottom, owner.Icon.SecondaryIconLeft, owner.Icon.SecondaryIconRight, owner.Icon.SecondaryIconWidth, owner.Icon.SecondaryIconHeight, "rbSecondaryIcon", owner.Icon.SecondaryIconCssClass);
					}
					if (flag3)
					{
						this.RenderIcon(writer, "rbSplitRight", owner.SplitButtonCssClass);
					}
				}
				else
				{
					this.RenderToggleState(writer, owner.SelectedToggleState, owner.Icon.ShowPrimaryIcon, owner.Icon.ShowSecondaryIcon, flag2, text, text2);
				}
			}
			if (owner.InDesignMode)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00021D80 File Offset: 0x0001FF80
		private void RenderToggleState(HtmlTextWriter writer, RadButtonToggleState toggleState, bool showPrimary, bool showSecondary, bool isRadioOrCB, string defaultPrimaryIconCssClass, string textElementCssClass1)
		{
			RadButton owner = base.Owner;
			string value = (!string.IsNullOrEmpty(toggleState.ImageUrl)) ? toggleState.ImageUrl : string.Empty;
			bool flag = !toggleState.IsBackgroundImage && !string.IsNullOrEmpty(value);
			string text = (!string.IsNullOrEmpty(toggleState.PrimaryIconUrl)) ? toggleState.PrimaryIconUrl : owner.Icon.PrimaryIconUrl;
			Unit top = (toggleState.PrimaryIconTop != Unit.Empty) ? toggleState.PrimaryIconTop : owner.Icon.PrimaryIconTop;
			Unit bottom = (toggleState.PrimaryIconBottom != Unit.Empty) ? toggleState.PrimaryIconBottom : owner.Icon.PrimaryIconBottom;
			Unit left = (toggleState.PrimaryIconLeft != Unit.Empty) ? toggleState.PrimaryIconLeft : owner.Icon.PrimaryIconLeft;
			Unit right = (toggleState.PrimaryIconRight != Unit.Empty) ? toggleState.PrimaryIconRight : owner.Icon.PrimaryIconRight;
			Unit width = (toggleState.PrimaryIconWidth != Unit.Empty) ? toggleState.PrimaryIconWidth : owner.Icon.PrimaryIconWidth;
			Unit height = (toggleState.PrimaryIconHeight != Unit.Empty) ? toggleState.PrimaryIconHeight : owner.Icon.PrimaryIconHeight;
			string text2 = owner.Icon.PrimaryIconCssClass;
			text2 = ((!string.IsNullOrEmpty(toggleState.PrimaryIconCssClass)) ? (text2 + (" " + toggleState.PrimaryIconCssClass)) : text2);
			string text3 = string.Empty;
			if ((isRadioOrCB && string.IsNullOrEmpty(value)) || showPrimary)
			{
				bool flag2 = !string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text2);
				if (isRadioOrCB || flag2)
				{
					text3 += " rbPrimary";
				}
				if (!isRadioOrCB && (flag || !flag2))
				{
					text2 += " rbHideElement";
				}
				this.RenderIcon(writer, text, top, bottom, left, right, width, height, defaultPrimaryIconCssClass, text2);
			}
			string text4 = (!string.IsNullOrEmpty(toggleState.SecondaryIconUrl)) ? toggleState.SecondaryIconUrl : owner.Icon.SecondaryIconUrl;
			Unit top2 = (toggleState.SecondaryIconTop != Unit.Empty) ? toggleState.SecondaryIconTop : owner.Icon.SecondaryIconTop;
			Unit bottom2 = (toggleState.SecondaryIconBottom != Unit.Empty) ? toggleState.SecondaryIconBottom : owner.Icon.SecondaryIconBottom;
			Unit left2 = (toggleState.SecondaryIconLeft != Unit.Empty) ? toggleState.SecondaryIconLeft : owner.Icon.SecondaryIconLeft;
			Unit right2 = (toggleState.SecondaryIconRight != Unit.Empty) ? toggleState.SecondaryIconRight : owner.Icon.SecondaryIconRight;
			Unit width2 = (toggleState.SecondaryIconWidth != Unit.Empty) ? toggleState.SecondaryIconWidth : owner.Icon.SecondaryIconWidth;
			Unit height2 = (toggleState.SecondaryIconHeight != Unit.Empty) ? toggleState.SecondaryIconHeight : owner.Icon.SecondaryIconHeight;
			string text5 = owner.Icon.SecondaryIconCssClass;
			text5 = ((!string.IsNullOrEmpty(toggleState.SecondaryIconCssClass)) ? (text5 + (" " + toggleState.SecondaryIconCssClass)) : text5);
			bool flag3 = !string.IsNullOrEmpty(text5) || !string.IsNullOrEmpty(text4);
			if (showSecondary && flag3)
			{
				text3 += " rbSecondary";
			}
			if (flag)
			{
				text3 += " rbHideElement";
			}
			string text6 = (!string.IsNullOrEmpty(toggleState.Text)) ? toggleState.Text : owner.Text;
			this.RenderText(writer, textElementCssClass1 + text3, text6);
			if (showSecondary)
			{
				if (flag || !flag3)
				{
					text5 += " rbHideElement";
				}
				this.RenderIcon(writer, text4, top2, bottom2, left2, right2, width2, height2, "rbSecondaryIcon", text5);
			}
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00022160 File Offset: 0x00020360
		private void RenderText(HtmlTextWriter writer, string cssClass)
		{
			this.RenderText(writer, cssClass, base.Owner.Text);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00022178 File Offset: 0x00020378
		private void RenderText(HtmlTextWriter writer, string cssClass, string text)
		{
			RadButton owner = base.Owner;
			RadButtonToggleState selectedToggleState = owner.SelectedToggleState;
			if (!string.IsNullOrEmpty(cssClass))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
			}
			Color foreColor = owner.ForeColor;
			if (foreColor != Color.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Color, ColorTranslator.ToHtml(foreColor));
			}
			this.AddFontStyleAttributes(writer);
			Unit left = (selectedToggleState != null && selectedToggleState.Width != Unit.Empty) ? selectedToggleState.Width : owner.Width;
			if (left != Unit.Empty)
			{
				if (owner.ToggleType != ButtonToggleType.CheckBox && owner.ToggleType != ButtonToggleType.Radio)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
					writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "0");
				}
				string value = "0";
				if (owner.ButtonType != RadButtonType.StandardButton)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "center");
				}
				else if (!owner.EnableBrowserButtonStyle && !this.CssClassFormatString.Contains("rbImageButton"))
				{
					value = "4px";
				}
				writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, value);
			}
			if (owner.ButtonType != RadButtonType.StandardButton || owner.Image.EnableImageButton)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				if (!string.IsNullOrEmpty(text))
				{
					writer.Write(HttpUtility.HtmlEncode(text));
				}
				writer.RenderEndTag();
				return;
			}
			Unit left2 = (selectedToggleState != null && selectedToggleState.Height != Unit.Empty) ? selectedToggleState.Height : owner.Height;
			if (left2 != Unit.Empty)
			{
				if (left2.Type == UnitType.Percentage)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
				}
				else
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, left2.ToString(CultureInfo.InvariantCulture));
				}
			}
			if (owner.IsClientSubmit)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "submit");
			}
			if (!string.IsNullOrEmpty(owner.AccessKey))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, owner.AccessKey);
			}
			PostBackOptions postBackOptions = owner.GetPostBackOptions();
			string uniqueID = owner.UniqueID;
			if (uniqueID != null && (postBackOptions == null || postBackOptions.TargetControl == owner))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID + "_input");
				writer.AddAttribute(HtmlTextWriterAttribute.Id, owner.ClientID + "_input");
			}
			this.AddAttributesBrowserInput(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Value, text);
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000223B4 File Offset: 0x000205B4
		private void AddAttributesBrowserInput(HtmlTextWriter writer)
		{
			RadButton owner = base.Owner;
			if (owner.EnableBrowserButtonStyle)
			{
				Color backColor = owner.BackColor;
				if (!backColor.IsEmpty)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, ColorTranslator.ToHtml(backColor));
				}
				Color borderColor = owner.BorderColor;
				if (!borderColor.IsEmpty)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.BorderColor, ColorTranslator.ToHtml(borderColor));
				}
				BorderStyle borderStyle = owner.BorderStyle;
				Unit borderWidth = owner.BorderWidth;
				if (!borderWidth.IsEmpty)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, borderWidth.ToString(CultureInfo.InvariantCulture));
					if (borderStyle != BorderStyle.NotSet)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, borderStyle.ToString().ToLowerInvariant());
						return;
					}
					if (borderWidth.Value != 0.0)
					{
						writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "solid");
						return;
					}
				}
				else if (borderStyle != BorderStyle.NotSet)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, borderStyle.ToString().ToLowerInvariant());
				}
			}
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0002248C File Offset: 0x0002068C
		private void AddFontStyleAttributes(HtmlTextWriter writer)
		{
			RadButton owner = base.Owner;
			FontInfo font = owner.Font;
			string[] names = font.Names;
			if (names.Length > 0)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.FontFamily, this.FormatStringArray(names, ','));
			}
			FontUnit size = font.Size;
			if (size != FontUnit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, size.ToString(CultureInfo.InvariantCulture));
			}
			if (font.Bold)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, "bold");
			}
			if (font.Italic)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.FontStyle, "italic");
			}
			string text = string.Empty;
			if (font.Underline)
			{
				text += "underline ";
			}
			if (font.Strikeout)
			{
				text += "line-through ";
			}
			if (font.Overline)
			{
				text += "overline";
			}
			if (text.Length > 0)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.TextDecoration, text.Trim());
			}
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00022578 File Offset: 0x00020778
		private string FormatStringArray(string[] array, char delimiter)
		{
			switch (array.Length)
			{
			case 0:
				return string.Empty;
			case 1:
				return array[0];
			default:
				return string.Join(delimiter.ToString(CultureInfo.InvariantCulture), array);
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x000225B8 File Offset: 0x000207B8
		private void RenderIcon(HtmlTextWriter writer, string defaultCssClass, string cssClass)
		{
			this.RenderIcon(writer, string.Empty, Unit.Empty, Unit.Empty, Unit.Empty, Unit.Empty, Unit.Empty, Unit.Empty, defaultCssClass, cssClass);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x000225F4 File Offset: 0x000207F4
		private void RenderIcon(HtmlTextWriter writer, string iconUrl, Unit top, Unit bottom, Unit left, Unit right, Unit width, Unit height, string defaultCssClass, string cssClass)
		{
			writer.WriteBeginTag("span");
			writer.WriteAttribute("class", string.Format("{0} {1}", defaultCssClass, cssClass).Trim());
			CssStyleWriter cssStyleWriter = new CssStyleWriter(writer);
			this.WriteUnitStyle(cssStyleWriter, "top", top);
			this.WriteUnitStyle(cssStyleWriter, "bottom", bottom);
			this.WriteUnitStyle(cssStyleWriter, "left", left);
			this.WriteUnitStyle(cssStyleWriter, "right", right);
			this.WriteUnitStyle(cssStyleWriter, "width", width);
			this.WriteUnitStyle(cssStyleWriter, "height", height);
			if (!string.IsNullOrEmpty(iconUrl))
			{
				cssStyleWriter.AddStyle("background-image", string.Format("url('{0}')", base.Owner.ResolveUrl(iconUrl)));
			}
			cssStyleWriter.WriteAttribute();
			writer.Write('>');
			writer.WriteEndTag("span");
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000226C6 File Offset: 0x000208C6
		private void WriteUnitStyle(CssStyleWriter writer, string name, Unit value)
		{
			if (value != Unit.Empty)
			{
				writer.AddStyle(name, value.ToString());
			}
		}
	}
}

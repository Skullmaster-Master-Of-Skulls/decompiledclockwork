using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI.ListBox.Renderers
{
	// Token: 0x02000578 RID: 1400
	internal class ListBoxClassicRenderer : ListBoxRenderBase
	{
		// Token: 0x060032B1 RID: 12977 RVA: 0x000A67B0 File Offset: 0x000A49B0
		public ListBoxClassicRenderer(RadListBox listBox) : base(listBox)
		{
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x060032B2 RID: 12978 RVA: 0x000A67BC File Offset: 0x000A49BC
		public override string CssClassFormatString
		{
			get
			{
				List<string> list = new List<string>
				{
					"RadListBox",
					"RadListBox_{0}"
				};
				if (base.RequiresButtons)
				{
					list.Add("RadListBoxButtonArea" + base.Owner.ButtonSettings.Position);
				}
				if (!base.Owner.Height.IsEmpty)
				{
					list.Add("RadListBoxScrollable");
				}
				return string.Join(" ", list.ToArray());
			}
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x000A6844 File Offset: 0x000A4A44
		public override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			if (base.Owner.InDesignMode)
			{
				base.RenderDesignTimeHtml(writer);
			}
			if (base.RequiresButtons && base.Owner.ButtonSettings.Position != ListBoxButtonPosition.Bottom)
			{
				this.RenderButtonArea(writer);
			}
			bool flag = base.Owner.HasHeaderTemplate || base.Owner.HasFooterTemplate;
			if (flag)
			{
				if (base.RequiresButtons)
				{
					this.ModifyGroupAccordingToButtonsPosition(writer);
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbTemplateContainer");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbTemplateTable");
				if (base.Owner.ControlContext != null && base.Owner.ControlContext.Request.Browser.Browser == "IE" && base.Owner.ControlContext.Request.Browser.MajorVersion < 8)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
					writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Table);
			}
			if (base.Owner.HasHeaderTemplate)
			{
				this.RenderHeaderFooterTemplate(writer, new Action<HtmlTextWriter>(base.RenderHeader), false);
			}
			if (flag)
			{
				this.RenderHeaderFooterTemplate(writer, new Action<HtmlTextWriter>(this.RenderGroup), true);
			}
			else
			{
				this.RenderGroup(writer);
			}
			if (base.Owner.HasFooterTemplate)
			{
				this.RenderHeaderFooterTemplate(writer, new Action<HtmlTextWriter>(base.RenderFooter), false);
			}
			if (flag)
			{
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			if (base.RequiresButtons && base.Owner.ButtonSettings.Position == ListBoxButtonPosition.Bottom)
			{
				this.RenderButtonArea(writer);
			}
			BaseClass.RenderVersionStamp(writer);
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x000A69EC File Offset: 0x000A4BEC
		private void RenderHeaderFooterTemplate(HtmlTextWriter writer, Action<HtmlTextWriter> renderAction, bool isGroup)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (isGroup)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbGroupCell");
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbTemplateCell");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Td);
			if (isGroup)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbGroupContainer");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			renderAction(writer);
			if (isGroup)
			{
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000A6A5C File Offset: 0x000A4C5C
		private string GetOppositePosition(string position)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{
					"Left",
					"Right"
				},
				{
					"Top",
					"Bottom"
				}
			};
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				if (base.Owner.ButtonSettings.Position.ToString() == keyValuePair.Key)
				{
					return keyValuePair.Value;
				}
				if (base.Owner.ButtonSettings.Position.ToString() == keyValuePair.Value)
				{
					return keyValuePair.Key;
				}
			}
			return position;
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000A6B34 File Offset: 0x000A4D34
		private void RenderButtonArea(HtmlTextWriter writer)
		{
			string text = "rlbButtonArea" + base.Owner.ButtonSettings.Position;
			if (base.Owner.ButtonSettings.IsVertical)
			{
				writer.AddStyleAttribute("width", base.Owner.ButtonSettings.AreaWidth.ToString());
				if (base.Owner.ButtonSettings.VerticalAlign != ListBoxVerticalAlign.Top)
				{
					if (base.Owner.Height.IsEmpty)
					{
						throw new NotSupportedException("The Height of RadListBox should be set in order to use Middle or Bottom vertical alignment.");
					}
					text = text + " rlb" + base.Owner.ButtonSettings.VerticalAlign;
				}
			}
			else
			{
				writer.AddStyleAttribute("height", base.Owner.ButtonSettings.AreaHeight.ToString());
				if (base.Owner.ButtonSettings.HorizontalAlign != ListBoxHorizontalAlign.Left)
				{
					text = text + " rlb" + base.Owner.ButtonSettings.HorizontalAlign;
				}
			}
			writer.AddAttribute("class", text);
			this.RenderBeginButtonWrapperTag(base.Owner.ButtonSettings.Position, writer);
			if (base.Owner.AllowReorder && base.Owner.ButtonSettings.ShowReorder && !base.Owner.EnableLoadOnDemand)
			{
				this.RenderReorderButtons(writer);
			}
			if (base.Owner.AllowDelete && base.Owner.ButtonSettings.ShowDelete)
			{
				this.RenderButton(writer, "rlbDelete", base.Owner.SelectedIndex >= 0, base.Owner.Localization.Delete);
			}
			if (base.Owner.AllowTransfer && base.Owner.ButtonSettings.ShowTransfer)
			{
				this.RenderTransferButtons(writer);
			}
			if (base.Owner.AllowTransfer && base.Owner.ButtonSettings.ShowTransferAll && !base.Owner.EnableLoadOnDemand)
			{
				this.RenderTransferAllButtons(writer);
			}
			this.RenderEndButtonWrapperTag(base.Owner.ButtonSettings.Position, writer);
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000A6D68 File Offset: 0x000A4F68
		private void RenderBeginButtonWrapperTag(ListBoxButtonPosition position, HtmlTextWriter writer)
		{
			switch (position)
			{
			case ListBoxButtonPosition.Right:
			case ListBoxButtonPosition.Left:
				if (base.Owner.ControlContext != null && base.Owner.ControlContext.Request.Browser.Browser == "IE" && base.Owner.ControlContext.Request.Browser.MajorVersion < 8)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
					writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Table);
				writer.Write("<tr><td>");
				return;
			case ListBoxButtonPosition.Bottom:
			case ListBoxButtonPosition.Top:
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				return;
			default:
				return;
			}
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x000A6E14 File Offset: 0x000A5014
		private void RenderEndButtonWrapperTag(ListBoxButtonPosition position, HtmlTextWriter writer)
		{
			switch (position)
			{
			case ListBoxButtonPosition.Right:
			case ListBoxButtonPosition.Left:
				writer.Write("</td></tr>");
				writer.RenderEndTag();
				return;
			case ListBoxButtonPosition.Bottom:
			case ListBoxButtonPosition.Top:
				writer.RenderEndTag();
				return;
			default:
				return;
			}
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x000A6E54 File Offset: 0x000A5054
		private void RenderButton(HtmlTextWriter writer, string cssClass, bool isButtonEnabled, string text)
		{
			string text2 = "rlbButton " + cssClass;
			if (!isButtonEnabled || !base.Owner.IsControlEnabled)
			{
				text2 += "Disabled rlbDisabled";
			}
			if (!base.Owner.ButtonSettings.RenderButtonText)
			{
				text2 += " rlbNoButtonText";
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text2);
			if (isButtonEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Title, text);
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.Write("<span class=\"rlbButtonBL\"><span class=\"rlbButtonBR\"><span class=\"rlbButtonTR\"><span class=\"rlbButtonTL\">");
			writer.AddAttribute("class", "rlbButtonText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			string value = base.Owner.ButtonSettings.RenderButtonText ? text : "&nbsp;";
			writer.Write(value);
			writer.RenderEndTag();
			writer.Write("</span></span></span></span>");
			writer.RenderEndTag();
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000A6F30 File Offset: 0x000A5130
		private void RenderReorderButtons(HtmlTextWriter writer)
		{
			int[] selectedIndices = base.Owner.GetSelectedIndices();
			int num = -1;
			int num2 = -1;
			if (selectedIndices.Length > 0)
			{
				num = selectedIndices[0];
				num2 = selectedIndices[selectedIndices.Length - 1];
			}
			if ((base.Owner.ButtonSettings.ReorderButtons & ListBoxReorderButtons.MoveUp) == ListBoxReorderButtons.MoveUp)
			{
				this.RenderButton(writer, "rlbMoveUp", num > 0, base.Owner.Localization.MoveUp);
			}
			if ((base.Owner.ButtonSettings.ReorderButtons & ListBoxReorderButtons.MoveDown) == ListBoxReorderButtons.MoveDown)
			{
				this.RenderButton(writer, "rlbMoveDown", num2 >= 0 && num2 < base.Owner.Items.Count - 1, base.Owner.Localization.MoveDown);
			}
			if ((base.Owner.ButtonSettings.ReorderButtons & ListBoxReorderButtons.MoveToTop) == ListBoxReorderButtons.MoveToTop)
			{
				this.RenderButton(writer, "rlbMoveToTop", num > 0, base.Owner.Localization.MoveTop);
			}
			if ((base.Owner.ButtonSettings.ReorderButtons & ListBoxReorderButtons.MoveToBottom) == ListBoxReorderButtons.MoveToBottom)
			{
				this.RenderButton(writer, "rlbMoveToBottom", num2 >= 0 && num2 < base.Owner.Items.Count - 1, base.Owner.Localization.MoveBottom);
			}
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000A7064 File Offset: 0x000A5264
		private void RenderTransferButtons(HtmlTextWriter writer)
		{
			string oppositePosition = this.GetOppositePosition(base.Owner.ButtonSettings.Position.ToString());
			string @string = base.Owner.Localization.GetString("To" + base.Owner.ButtonSettings.Position);
			string string2 = base.Owner.Localization.GetString("To" + oppositePosition);
			if ((base.Owner.ButtonSettings.TransferButtons & ListBoxTransferButtons.TransferFrom) == ListBoxTransferButtons.TransferFrom)
			{
				this.RenderButton(writer, "rlbTransferFrom", base.Owner.SelectedIndex > -1 && base.Owner.TransferToListBox != null, @string);
			}
			if ((base.Owner.ButtonSettings.TransferButtons & ListBoxTransferButtons.TransferTo) == ListBoxTransferButtons.TransferTo)
			{
				if (base.Owner.TransferToListBox != null)
				{
					this.RenderButton(writer, "rlbTransferTo", base.Owner.TransferToListBox.SelectedIndex > -1, string2);
					return;
				}
				this.RenderButton(writer, "rlbTransferTo", false, string2);
			}
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000A7174 File Offset: 0x000A5374
		private void RenderTransferAllButtons(HtmlTextWriter writer)
		{
			string oppositePosition = this.GetOppositePosition(base.Owner.ButtonSettings.Position.ToString());
			string @string = base.Owner.Localization.GetString("AllTo" + base.Owner.ButtonSettings.Position);
			string string2 = base.Owner.Localization.GetString("AllTo" + oppositePosition);
			if ((base.Owner.ButtonSettings.TransferButtons & ListBoxTransferButtons.TransferAllFrom) == ListBoxTransferButtons.TransferAllFrom)
			{
				this.RenderButton(writer, "rlbTransferAllFrom", base.Owner.Items.Count > 0 && base.Owner.TransferToListBox != null, @string);
			}
			if ((base.Owner.ButtonSettings.TransferButtons & ListBoxTransferButtons.TransferAllTo) == ListBoxTransferButtons.TransferAllTo)
			{
				if (base.Owner.TransferToListBox != null)
				{
					this.RenderButton(writer, "rlbTransferAllTo", base.Owner.TransferToListBox.Items.Count > 0, string2);
					return;
				}
				this.RenderButton(writer, "rlbTransferAllTo", false, string2);
			}
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x000A728D File Offset: 0x000A548D
		private void ApplyMargin(HtmlTextWriter writer, Unit margin)
		{
			writer.AddStyleAttribute("margin-" + base.Owner.ButtonSettings.Position.ToString().ToLower(), margin.ToString());
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x000A72CC File Offset: 0x000A54CC
		private void RenderGroup(HtmlTextWriter writer)
		{
			if (base.RequiresButtons && !base.Owner.HasFooterTemplate && !base.Owner.HasHeaderTemplate)
			{
				this.ModifyGroupAccordingToButtonsPosition(writer);
			}
			if (base.Owner.TabIndex != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, base.Owner.TabIndex.ToString());
			}
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, base.Owner.AccessKey);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbGroup rlbGroup" + base.Owner.ButtonSettings.Position);
			if (!base.Owner.BorderColor.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.BorderColor, base.Owner.BorderColor.Name);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (!string.IsNullOrEmpty(base.Owner.EmptyMessage) || base.Owner.EmptyMessageTemplate != null)
			{
				base.RenderEmptyMessage(writer);
			}
			if (base.Owner.CheckBoxes && base.Owner.ShowCheckAll)
			{
				base.RenderCheckAllItem(writer);
			}
			if (base.Owner.Items.Count > 0 || base.Owner.EnableLoadOnDemand)
			{
				base.RenderList(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x000A7420 File Offset: 0x000A5620
		private void ModifyGroupAccordingToButtonsPosition(HtmlTextWriter writer)
		{
			switch (base.Owner.ButtonSettings.Position)
			{
			case ListBoxButtonPosition.Right:
			case ListBoxButtonPosition.Left:
				if (base.Owner.Height.IsEmpty && base.Owner.ControlContext != null && !base.Owner.ControlContext.Request.Browser.IsBrowser("WebKit"))
				{
					this.ApplyMargin(writer, base.Owner.ButtonSettings.AreaWidth);
					return;
				}
				if (base.Owner.Height.IsEmpty && base.Owner.InDesignMode)
				{
					this.ApplyMargin(writer, base.Owner.ButtonSettings.AreaWidth);
					return;
				}
				if (!base.Owner.Height.IsEmpty)
				{
					this.ApplyMargin(writer, base.Owner.ButtonSettings.AreaWidth);
					return;
				}
				break;
			case ListBoxButtonPosition.Bottom:
				if (!base.Owner.Height.IsEmpty)
				{
					writer.AddStyleAttribute("bottom", base.Owner.ButtonSettings.AreaHeight.ToString());
				}
				break;
			case ListBoxButtonPosition.Top:
				if (!base.Owner.Height.IsEmpty)
				{
					writer.AddStyleAttribute("top", base.Owner.ButtonSettings.AreaHeight.ToString());
					return;
				}
				break;
			default:
				return;
			}
		}
	}
}

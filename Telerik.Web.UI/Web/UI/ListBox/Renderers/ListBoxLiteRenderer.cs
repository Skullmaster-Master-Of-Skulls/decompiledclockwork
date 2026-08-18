using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI.ListBox.Renderers
{
	// Token: 0x0200057C RID: 1404
	internal class ListBoxLiteRenderer : ListBoxRenderBase
	{
		// Token: 0x060032CF RID: 13007 RVA: 0x000A7C04 File Offset: 0x000A5E04
		public ListBoxLiteRenderer(RadListBox listBox) : base(listBox)
		{
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x060032D0 RID: 13008 RVA: 0x000A7C10 File Offset: 0x000A5E10
		public override string CssClassFormatString
		{
			get
			{
				List<string> list = new List<string>
				{
					"RadListBox",
					"RadListBox_{0}"
				};
				if (base.RequiresButtons && (base.Owner.ButtonSettings.Position == ListBoxButtonPosition.Bottom || base.Owner.ButtonSettings.Position == ListBoxButtonPosition.Top))
				{
					list.Add("RadListBoxButtonArea" + base.Owner.ButtonSettings.Position);
				}
				if (!base.Owner.Height.IsEmpty)
				{
					list.Add("rlbFixedHeight");
				}
				return string.Join(" ", list.ToArray());
			}
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000A7CC0 File Offset: 0x000A5EC0
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
				if (base.RequiresButtons && (base.Owner.ButtonSettings.Position == ListBoxButtonPosition.Left || base.Owner.ButtonSettings.Position == ListBoxButtonPosition.Right))
				{
					writer.AddStyleAttribute("margin-" + base.Owner.ButtonSettings.Position.ToString().ToLower(), base.Owner.ButtonSettings.AreaWidth.ToString());
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbTemplate");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbTemplateContent");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
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

		// Token: 0x060032D2 RID: 13010 RVA: 0x000A7E69 File Offset: 0x000A6069
		private void RenderHeaderFooterTemplate(HtmlTextWriter writer, Action<HtmlTextWriter> renderAction, bool isGroup)
		{
			if (isGroup)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbBody");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			renderAction(writer);
			if (isGroup)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x000A7E94 File Offset: 0x000A6094
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

		// Token: 0x060032D4 RID: 13012 RVA: 0x000A7F6C File Offset: 0x000A616C
		private string GetTransferDirection(string position)
		{
			if (position == "Top")
			{
				return "Up";
			}
			if (position == "Bottom")
			{
				return "Down";
			}
			return position;
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000A7F98 File Offset: 0x000A6198
		private void RenderButtonArea(HtmlTextWriter writer)
		{
			string text = "rlbButtonArea" + base.Owner.ButtonSettings.Position;
			if (base.Owner.ButtonSettings.IsVertical)
			{
				if (base.Owner.ButtonSettings.AreaWidth.ToString() != "30px")
				{
					writer.AddStyleAttribute("width", base.Owner.ButtonSettings.AreaWidth.ToString());
				}
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
				if (base.Owner.ButtonSettings.AreaHeight.ToString() != "30px")
				{
					writer.AddStyleAttribute("height", base.Owner.ButtonSettings.AreaHeight.ToString());
				}
				if (base.Owner.ButtonSettings.HorizontalAlign != ListBoxHorizontalAlign.Left)
				{
					text = text + " rlb" + base.Owner.ButtonSettings.HorizontalAlign;
				}
			}
			writer.AddAttribute("class", text);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			if (base.Owner.AllowReorder && base.Owner.ButtonSettings.ShowReorder && !base.Owner.EnableLoadOnDemand)
			{
				this.RenderReorderButtons(writer);
			}
			if (base.Owner.AllowDelete && base.Owner.ButtonSettings.ShowDelete)
			{
				this.RenderButton(writer, "rlbDelete", "rlbIconDelete", base.Owner.SelectedIndex >= 0, base.Owner.Localization.Delete);
			}
			if (base.Owner.AllowTransfer && base.Owner.ButtonSettings.ShowTransfer)
			{
				this.RenderTransferButtons(writer);
			}
			if (base.Owner.AllowTransfer && base.Owner.ButtonSettings.ShowTransferAll && !base.Owner.EnableLoadOnDemand)
			{
				this.RenderTransferAllButtons(writer);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000A8218 File Offset: 0x000A6418
		private void RenderButton(HtmlTextWriter writer, string cssClass, string liteCssClass, bool isButtonEnabled, string text)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			string text2 = "rlbButton " + cssClass;
			if (!isButtonEnabled || !base.Owner.IsControlEnabled)
			{
				text2 += " rlbDisabled";
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			if (!base.Owner.ButtonSettings.RenderButtonText)
			{
				text2 += " rlbNoButtonText";
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text2);
			writer.AddAttribute(HtmlTextWriterAttribute.Title, text);
			writer.RenderBeginTag(HtmlTextWriterTag.Button);
			writer.AddAttribute("class", "rlbButtonIcon " + liteCssClass);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			writer.AddAttribute("class", "rlbButtonText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			string value = base.Owner.ButtonSettings.RenderButtonText ? text : "&nbsp;";
			writer.Write(value);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000A8310 File Offset: 0x000A6510
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
				this.RenderButton(writer, "rlbMoveUp", "rlbIconMoveUp", num > 0, base.Owner.Localization.MoveUp);
			}
			if ((base.Owner.ButtonSettings.ReorderButtons & ListBoxReorderButtons.MoveDown) == ListBoxReorderButtons.MoveDown)
			{
				this.RenderButton(writer, "rlbMoveDown", "rlbIconMoveDown", num2 >= 0 && num2 < base.Owner.Items.Count - 1, base.Owner.Localization.MoveDown);
			}
			if ((base.Owner.ButtonSettings.ReorderButtons & ListBoxReorderButtons.MoveToTop) == ListBoxReorderButtons.MoveToTop)
			{
				this.RenderButton(writer, "rlbMoveToTop", "rlbIconMoveToTop", num > 0, base.Owner.Localization.MoveTop);
			}
			if ((base.Owner.ButtonSettings.ReorderButtons & ListBoxReorderButtons.MoveToBottom) == ListBoxReorderButtons.MoveToBottom)
			{
				this.RenderButton(writer, "rlbMoveToBottom", "rlbIconMoveToBottom", num2 >= 0 && num2 < base.Owner.Items.Count - 1, base.Owner.Localization.MoveBottom);
			}
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000A8458 File Offset: 0x000A6658
		private void RenderTransferButtons(HtmlTextWriter writer)
		{
			string oppositePosition = this.GetOppositePosition(base.Owner.ButtonSettings.Position.ToString());
			string @string = base.Owner.Localization.GetString("To" + base.Owner.ButtonSettings.Position);
			string string2 = base.Owner.Localization.GetString("To" + oppositePosition);
			if ((base.Owner.ButtonSettings.TransferButtons & ListBoxTransferButtons.TransferFrom) == ListBoxTransferButtons.TransferFrom)
			{
				this.RenderButton(writer, "rlbTransferFrom", "rlbIconTransfer" + this.GetTransferDirection(base.Owner.ButtonSettings.Position.ToString()), base.Owner.SelectedIndex > -1 && base.Owner.TransferToListBox != null, @string);
			}
			if ((base.Owner.ButtonSettings.TransferButtons & ListBoxTransferButtons.TransferTo) == ListBoxTransferButtons.TransferTo)
			{
				if (base.Owner.TransferToListBox != null)
				{
					this.RenderButton(writer, "rlbTransferTo", "rlbIconTransfer" + this.GetTransferDirection(oppositePosition), base.Owner.TransferToListBox.SelectedIndex > -1, string2);
					return;
				}
				this.RenderButton(writer, "rlbTransferTo", "rlbIconTransfer" + this.GetTransferDirection(oppositePosition), false, string2);
			}
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000A85B4 File Offset: 0x000A67B4
		private void RenderTransferAllButtons(HtmlTextWriter writer)
		{
			string oppositePosition = this.GetOppositePosition(base.Owner.ButtonSettings.Position.ToString());
			string @string = base.Owner.Localization.GetString("AllTo" + base.Owner.ButtonSettings.Position);
			string string2 = base.Owner.Localization.GetString("AllTo" + oppositePosition);
			if ((base.Owner.ButtonSettings.TransferButtons & ListBoxTransferButtons.TransferAllFrom) == ListBoxTransferButtons.TransferAllFrom)
			{
				this.RenderButton(writer, "rlbTransferAllFrom", "rlbIconTransferAll" + this.GetTransferDirection(base.Owner.ButtonSettings.Position.ToString()), base.Owner.Items.Count > 0 && base.Owner.TransferToListBox != null, @string);
			}
			if ((base.Owner.ButtonSettings.TransferButtons & ListBoxTransferButtons.TransferAllTo) == ListBoxTransferButtons.TransferAllTo)
			{
				if (base.Owner.TransferToListBox != null)
				{
					this.RenderButton(writer, "rlbTransferAllTo", "rlbIconTransferAll" + this.GetTransferDirection(oppositePosition), base.Owner.TransferToListBox.Items.Count > 0, string2);
					return;
				}
				this.RenderButton(writer, "rlbTransferAllTo", "rlbIconTransferAll" + this.GetTransferDirection(oppositePosition), false, string2);
			}
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000A871C File Offset: 0x000A691C
		private void RenderGroup(HtmlTextWriter writer)
		{
			if (base.RequiresButtons && !base.Owner.HasFooterTemplate && !base.Owner.HasHeaderTemplate && (base.Owner.ButtonSettings.Position == ListBoxButtonPosition.Left || base.Owner.ButtonSettings.Position == ListBoxButtonPosition.Right))
			{
				writer.AddStyleAttribute("margin-" + base.Owner.ButtonSettings.Position.ToString().ToLower(), base.Owner.ButtonSettings.AreaWidth.ToString());
			}
			if (base.Owner.TabIndex != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, base.Owner.TabIndex.ToString());
			}
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, base.Owner.AccessKey);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbGroup");
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
	}
}

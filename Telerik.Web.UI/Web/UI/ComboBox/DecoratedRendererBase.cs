using System;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ComboBox
{
	// Token: 0x02000A14 RID: 2580
	public abstract class DecoratedRendererBase : ComboRendererBase
	{
		// Token: 0x060061D8 RID: 25048 RVA: 0x00170C75 File Offset: 0x0016EE75
		public DecoratedRendererBase(RadComboBox owner) : base(owner)
		{
		}

		// Token: 0x17002011 RID: 8209
		// (get) Token: 0x060061D9 RID: 25049 RVA: 0x00170C7E File Offset: 0x0016EE7E
		protected bool ApplyEmptyMessage
		{
			get
			{
				return string.IsNullOrEmpty(base.Owner.Text) && !string.IsNullOrEmpty(base.Owner.EmptyMessage);
			}
		}

		// Token: 0x060061DA RID: 25050 RVA: 0x00170CA7 File Offset: 0x0016EEA7
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			this.RenderWrapper(writer);
			this.RenderDropDown(writer);
		}

		// Token: 0x060061DB RID: 25051 RVA: 0x00170CBE File Offset: 0x0016EEBE
		protected virtual void RenderWrapper(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060061DC RID: 25052 RVA: 0x00170CC8 File Offset: 0x0016EEC8
		protected override void RenderDropDown(HtmlTextWriter writer)
		{
			writer = new WhiteSpaceStrippingHtmlTextWriter(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcbSlide");
			writer.AddStyleAttribute("z-index", base.Owner.ZIndex.ToString());
			writer.AddStyleAttribute("display", "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, base.Owner.ClientID + "_DropDown");
			string text = "RadComboBoxDropDown RadComboBoxDropDown_" + base.Owner.RuntimeSkin;
			text = text + " " + base.Owner.DropDownCssClass;
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			if (base.Owner.InDesignMode || base.Owner.Browser.IsBrowser("IE"))
			{
				writer.AddStyleAttribute("float", "left");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (base.Owner.HeaderTemplate != null || base.Owner.Header.Controls.Count > 0)
			{
				this.RenderHeader(writer);
			}
			text = "rcbScroll";
			if (base.Owner.InDesignMode || (base.Owner.Browser.IsBrowser("IE") && base.Owner.Browser.Version.Substring(0, 1) == "6"))
			{
				writer.AddStyleAttribute("float", "left");
			}
			text += " rcbWidth";
			if (base.Owner.NoWrap)
			{
				text += " rcbNoWrap";
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (base.Owner.CheckBoxes && base.Owner.EnableCheckAllItemsCheckBox)
			{
				this.RenderCheckAllItemsCheckBox(writer);
			}
			if (base.Owner.Items.Count > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcbList");
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				this.RenderItems(writer);
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
			if (base.Owner.ShowMoreResultsBox)
			{
				this.RenderShowMoreResultBox(writer);
			}
			if (base.Owner.FooterTemplate != null || base.Owner.Footer.Controls.Count > 0)
			{
				this.RenderFooter(writer);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060061DD RID: 25053 RVA: 0x00170F16 File Offset: 0x0016F116
		protected void RenderHeader(HtmlTextWriter writer)
		{
			if (base.Owner.HeaderTemplate != null)
			{
				RadComboBox.ApplyTemplate(base.Owner.Header, base.Owner.HeaderTemplate);
			}
			base.Owner.Header.RenderControl(writer);
		}

		// Token: 0x060061DE RID: 25054 RVA: 0x00170F51 File Offset: 0x0016F151
		protected void RenderFooter(HtmlTextWriter writer)
		{
			if (base.Owner.FooterTemplate != null)
			{
				RadComboBox.ApplyTemplate(base.Owner.Footer, base.Owner.FooterTemplate);
			}
			base.Owner.Footer.RenderControl(writer);
		}

		// Token: 0x060061DF RID: 25055 RVA: 0x00170F8C File Offset: 0x0016F18C
		protected void RenderShowMoreResultBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcbMoreResults");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, base.Owner.ClientID + "_MoreResultsBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, base.Owner.ClientID + "_MoreResultsBoxImage");
			writer.AddAttribute(HtmlTextWriterAttribute.Title, "Show more results");
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "p-icon p-i-arrow-60-down");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060061E0 RID: 25056 RVA: 0x00171034 File Offset: 0x0016F234
		protected void RenderCheckAllItemsCheckBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcbCheckAllItems");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderBeginTag(HtmlTextWriterTag.Label);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rcbCheckAllItemsCheckBox");
			if (base.Owner.Items.Count == base.Owner.CheckedItems.Count && base.Owner.CheckedItems.Count > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
			}
			if (!base.Owner.IsControlEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
			writer.Write(HttpUtility.HtmlEncode(base.Owner.Localization.CheckAllString));
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060061E1 RID: 25057 RVA: 0x0017110C File Offset: 0x0016F30C
		protected Control CreateInput()
		{
			HtmlInputTextWithName htmlInputTextWithName = new HtmlInputTextWithName();
			string text = "rcbInput radPreventDecorate";
			if (this.ApplyEmptyMessage)
			{
				text += string.Format(" {0}", "rcbEmptyMessage");
			}
			if (!string.IsNullOrEmpty(base.Owner.InputCssClass))
			{
				text += " " + base.Owner.InputCssClass;
			}
			htmlInputTextWithName.Attributes["class"] = text;
			this.ApplyAttributsToInput(htmlInputTextWithName);
			return htmlInputTextWithName;
		}

		// Token: 0x060061E2 RID: 25058 RVA: 0x0017118C File Offset: 0x0016F38C
		private void ApplyAttributsToInput(HtmlInputTextWithName input)
		{
			input.Name = base.Owner.UniqueID;
			input.Attributes["type"] = "text";
			input.Attributes["id"] = base.Owner.ClientID + "_Input";
			input.Attributes["value"] = base.Owner.Text;
			if (!string.IsNullOrEmpty(base.Owner.InputTitle))
			{
				input.Attributes["title"] = base.Owner.InputTitle;
			}
			if (this.ApplyEmptyMessage)
			{
				input.Attributes["value"] = base.Owner.EmptyMessage;
			}
			if (base.Owner.InDesignMode || base.Owner.Browser.IsBrowser("IE"))
			{
				input.Attributes["style"] = "display: block;";
			}
			if (base.Owner.ReadOnly)
			{
				input.Attributes["readonly"] = "readonly";
			}
			if (base.Owner.MaxLength > 0)
			{
				input.Attributes["maxlength"] = base.Owner.MaxLength.ToString();
			}
			if (base.Owner.TabIndex != 0)
			{
				input.Attributes["tabindex"] = base.Owner.TabIndex.ToString();
			}
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				input.Attributes["accesskey"] = base.Owner.AccessKey;
			}
			if (!string.IsNullOrEmpty(base.Owner.ToolTip))
			{
				input.Attributes["title"] = base.Owner.ToolTip;
			}
			if (!base.Owner.IsControlEnabled)
			{
				input.Attributes["disabled"] = "disabled";
			}
			if (base.Owner.ForeColor != Color.Empty)
			{
				if (base.Owner.ForeColor.IsNamedColor)
				{
					input.Style["color"] = base.Owner.ForeColor.Name;
				}
				else
				{
					input.Style["color"] = string.Format("rgb({0}, {1}, {2})", base.Owner.ForeColor.R, base.Owner.ForeColor.G, base.Owner.ForeColor.B);
				}
			}
			if (base.Owner.BackColor != Color.Empty)
			{
				if (base.Owner.BackColor.IsNamedColor)
				{
					input.Style["background-color"] = base.Owner.BackColor.Name;
				}
				else
				{
					input.Style["background-color"] = string.Format("rgb({0}, {1}, {2})", base.Owner.BackColor.R, base.Owner.BackColor.G, base.Owner.BackColor.B);
				}
			}
			if (base.Owner.Font.Bold)
			{
				input.Style["font-weight"] = "bold";
			}
			if (base.Owner.Font.Italic)
			{
				input.Style["font-style"] = "italic";
			}
			string text = "";
			if (base.Owner.Font.Strikeout)
			{
				text += "line-through ";
			}
			if (base.Owner.Font.Underline)
			{
				text += "underline ";
			}
			if (base.Owner.Font.Overline)
			{
				text += "overline";
			}
			if (text.Length > 0)
			{
				input.Style["text-decoration"] = text;
			}
			if (base.Owner.Font.Names.Length > 0)
			{
				input.Style["font-family"] = string.Join(",", base.Owner.Font.Names);
			}
			if (base.Owner.Font.Size != FontUnit.Empty)
			{
				input.Style["font-size"] = base.Owner.Font.Size.ToString();
			}
		}
	}
}

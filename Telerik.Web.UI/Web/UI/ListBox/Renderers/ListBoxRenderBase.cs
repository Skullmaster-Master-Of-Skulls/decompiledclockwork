using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.ListBox.Renderers
{
	// Token: 0x02000577 RID: 1399
	internal class ListBoxRenderBase : RendererBase
	{
		// Token: 0x060032A5 RID: 12965 RVA: 0x000A634C File Offset: 0x000A454C
		public ListBoxRenderBase(RadListBox owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x060032A6 RID: 12966 RVA: 0x000A635B File Offset: 0x000A455B
		// (set) Token: 0x060032A7 RID: 12967 RVA: 0x000A6363 File Offset: 0x000A4563
		protected RadListBox Owner { get; set; }

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x060032A8 RID: 12968 RVA: 0x000A636C File Offset: 0x000A456C
		protected bool RequiresButtons
		{
			get
			{
				return (this.Owner.AllowReorder && this.Owner.ButtonSettings.ShowReorder) || (this.Owner.AllowDelete && this.Owner.ButtonSettings.ShowDelete) || (this.Owner.AllowTransfer && this.Owner.ButtonSettings.ShowTransfer) || (this.Owner.AllowTransfer && this.Owner.ButtonSettings.ShowTransferAll);
			}
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x000A63F8 File Offset: 0x000A45F8
		protected void RenderCheckAllItem(HtmlTextWriter writer)
		{
			string text = "rlbCheckAllItems";
			if (!this.Owner.IsControlEnabled)
			{
				text += " rlbDisabled";
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.RenderBeginTag(HtmlTextWriterTag.Label);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbCheckAllItemsCheckBox");
			if (this.Owner.CheckedItems.Count > 0 && this.Owner.Items.Count == this.Owner.CheckedItems.Count)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
			}
			if (!this.Owner.IsControlEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
			writer.Write(HttpUtility.HtmlEncode(this.Owner.Localization.CheckAll));
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x000A64E8 File Offset: 0x000A46E8
		protected void RenderEmptyMessage(HtmlTextWriter writer)
		{
			if (this.Owner.EmptyMessageTemplate == null)
			{
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbEmptyMessage");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			Control control = new Control();
			this.Owner.EmptyMessageTemplate.InstantiateIn(control);
			this.Owner.Controls.Add(control);
			control.RenderControl(writer);
			writer.RenderEndTag();
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x000A655C File Offset: 0x000A475C
		protected void RenderHiddenItem(HtmlTextWriter writer)
		{
			RadListBoxItem radListBoxItem = new RadListBoxItem
			{
				Text = "Test",
				CssClass = "rlbItemHidden"
			};
			this.Owner.Items.Add(radListBoxItem);
			radListBoxItem.RenderControl(writer);
			this.Owner.Items.Remove(radListBoxItem);
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x000A65B0 File Offset: 0x000A47B0
		protected void RenderHeader(HtmlTextWriter writer)
		{
			if (this.Owner.HeaderTemplate != null)
			{
				ListBoxRenderBase.ApplyTemplate(this.Owner.Header, this.Owner.HeaderTemplate);
			}
			this.Owner.Header.RenderControl(writer);
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000A65EB File Offset: 0x000A47EB
		protected void RenderFooter(HtmlTextWriter writer)
		{
			if (this.Owner.FooterTemplate != null)
			{
				ListBoxRenderBase.ApplyTemplate(this.Owner.Footer, this.Owner.FooterTemplate);
			}
			this.Owner.Footer.RenderControl(writer);
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000A6628 File Offset: 0x000A4828
		protected static void ApplyTemplate(WebControl control, ITemplate template)
		{
			DefaultHeaderFooterControl defaultHeaderFooterControl = control as DefaultHeaderFooterControl;
			if (defaultHeaderFooterControl.TemplateInstantiated)
			{
				return;
			}
			int i = control.Controls.Count;
			if (template != null)
			{
				template.InstantiateIn(control);
			}
			while (i > 0)
			{
				control.Controls.Add(control.Controls[0]);
				i--;
			}
			defaultHeaderFooterControl.TemplateInstantiated = true;
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000A6684 File Offset: 0x000A4884
		protected void RenderList(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbList");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			if ((this.Owner.EnableLoadOnDemand && this.Owner.DataSourceItemsCount > 0) || (this.Owner.EnableLoadOnDemand && !string.IsNullOrEmpty(this.Owner.ClientDataSourceID)))
			{
				this.RenderHiddenItem(writer);
			}
			else
			{
				bool flag = false;
				foreach (object obj in this.Owner.Items)
				{
					RadListBoxItem radListBoxItem = (RadListBoxItem)obj;
					if (radListBoxItem.Selected)
					{
						if (flag && this.Owner.SelectionMode == ListBoxSelectionMode.Single)
						{
							throw new HttpException("Cannot have multiple items selected when the SelectionMode is Single.");
						}
						flag = true;
					}
					if (!this.Owner.Enabled)
					{
						radListBoxItem.Enabled = false;
					}
					radListBoxItem.RenderControl(writer);
				}
			}
			writer.RenderEndTag();
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000A677C File Offset: 0x000A497C
		protected void RenderDesignTimeHtml(HtmlTextWriter writer)
		{
			writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this.Owner));
			writer.Write("<style type=\"text/css\">");
			writer.Write("\r\n\t\t.RadListBox\r\n\t\t{\r\n\t\t\tdisplay: inline-block;\r\n\t\t}\r\n\t\t.RadListBox .rlbButtonTL,\r\n\t\t.RadListBox .rlbButtonTR,\r\n\t\t.RadListBox .rlbButtonBL,\r\n\t\t.RadListBox .rlbButtonBR\r\n\t\t{\r\n\t\t\tdisplay: block;\r\n\t\t\tfloat: left;\r\n\t\t}\r\n\t\t.RadListBox .rlbGroup\r\n\t\t{\r\n\t\t\ttop: 0;\r\n\t\t\tleft: 0;\r\n\t\t\tright: auto;\r\n\t\t\tbottom: auto;\r\n\t\t\theight: 100%;\r\n\t\t}\r\n\t\t.RadListBox .rlbItem\r\n\t\t{\r\n\t\t\twhite-space: normal !important;\r\n\t\t}\r\n\t\t.RadListBox .rlbButtonText\r\n\t\t{\r\n\t\t\toverflow: hidden;\r\n\t\t\tline-height: 12px;\r\n\t\t\tfloat: left;\r\n\t\t}\r\n\t\t.RadListBox .rlbButtonAreaLeft .rlbButtonTL,\r\n\t\t.RadListBox .rlbButtonAreaLeft .rlbButtonTR,\r\n\t\t.RadListBox .rlbButtonAreaLeft .rlbButtonBL,\r\n\t\t.RadListBox .rlbButtonAreaLeft .rlbButtonBR,\r\n\t\t.RadListBox .rlbButtonAreaRight .rlbButtonTL,\r\n\t\t.RadListBox .rlbButtonAreaRight .rlbButtonTR,\r\n\t\t.RadListBox .rlbButtonAreaRight .rlbButtonBL,\r\n\t\t.RadListBox .rlbButtonAreaRight .rlbButtonBR\r\n\t\t{\r\n\t\t\twidth: 100%;\r\n\t\t}\r\n\t\t.RadListBox .rlbButtonAreaLeft .rlbButtonTL\r\n\t\t{\r\n\t\t\tpadding: 0;\r\n\t\t}\r\n\t\tdiv.RadListBox .rlbButtonAreaLeft .rlbButton\r\n\t\t{\r\n\t\t\tmargin-right: 15px;\r\n\t\t\tmargin-left: 0;\r\n\t\t}\r\n\t\t.RadListBox .rlbButtonAreaRight .rlbButton\r\n\t\t{\r\n\t\t\tmargin-left: 5px;\r\n\t\t\tmargin-right: 0;\r\n\t\t}\r\n\t\t.RadListBox .rlbButtonAreaTop .rlbButton,\r\n\t\t.RadListBox .rlbButtonAreaBottom .rlbButton\r\n\t\t{\r\n\t\t\tpadding-right: 1px;\r\n\t\t}\r\n");
			writer.Write("</style>");
		}
	}
}

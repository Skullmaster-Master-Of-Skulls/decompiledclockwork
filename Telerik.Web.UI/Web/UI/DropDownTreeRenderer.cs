using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000B37 RID: 2871
	public class DropDownTreeRenderer : IRenderer
	{
		// Token: 0x06006C6A RID: 27754 RVA: 0x00192E9F File Offset: 0x0019109F
		public DropDownTreeRenderer(RadDropDownTree control)
		{
			this._control = control;
		}

		// Token: 0x17002393 RID: 9107
		// (get) Token: 0x06006C6B RID: 27755 RVA: 0x00192EAE File Offset: 0x001910AE
		protected virtual string DropDownTreeCssClassFormatString
		{
			get
			{
				return this.CssClassFormatString;
			}
		}

		// Token: 0x17002394 RID: 9108
		// (get) Token: 0x06006C6C RID: 27756 RVA: 0x00192EB6 File Offset: 0x001910B6
		protected virtual string DropDownTreeIDFormatString
		{
			get
			{
				return "{0}_DropDownTree";
			}
		}

		// Token: 0x17002395 RID: 9109
		// (get) Token: 0x06006C6D RID: 27757 RVA: 0x00192EBD File Offset: 0x001910BD
		public HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06006C6E RID: 27758 RVA: 0x00192EC4 File Offset: 0x001910C4
		public void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this._control.CssClass;
			this._control.CssClass = string.Format("{0}", this._control.CssClass).Trim();
			this._control.CallBaseAddAttributesToRender(writer);
			this._control.CssClass = cssClass;
		}

		// Token: 0x06006C6F RID: 27759 RVA: 0x00192F1C File Offset: 0x0019111C
		public virtual void RenderContents(HtmlTextWriter writer)
		{
			if (this._control.IsDesignMode)
			{
				this.RenderDesignTimeHtml(writer);
				return;
			}
			string arg = this._control.IsControlEnabled ? string.Empty : "rddtDisabled";
			string value = string.Format("{0} {1}", "rddtInner", arg).Trim();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			this.RenderFakeInput(writer);
			this.RenderArrow(writer);
			writer.RenderEndTag();
			this.RenderDropDown(writer);
		}

		// Token: 0x06006C70 RID: 27760 RVA: 0x00192F9C File Offset: 0x0019119C
		private void RenderDesignTimeHtml(HtmlTextWriter writer)
		{
			writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this._control));
			string str = "160px";
			if (!this._control.Width.IsEmpty)
			{
				UnitType type = this._control.Width.Type;
				if (type != UnitType.Pixel)
				{
					if (type == UnitType.Percentage)
					{
						str = this._control.Width.Value + "%";
					}
				}
				else
				{
					str = this._control.Width.Value + "px";
				}
			}
			writer.Write("<style type=\"text/css\">");
			writer.Write(" \r\n                        .RadDropDownTree .rddtInner {\r\n                            border: 1px solid;\r\n                            display: block;\r\n                            height: 16px;\r\n                            padding: 2px 18px 2px 5px;\r\n                            position: relative;\r\n                        }\r\n                        .RadDropDownTree {\r\n                            text-align: left;\r\n                            white-space: nowrap;\r\n                            display:inline-block;\r\n                            width:" + str + ";}");
			writer.Write("</style>");
			writer.Write("\t<div class='RadDropDownTree'>\r\n                    <div class='rddtInner' >\r\n                        <span class='rddrFakeInput rddtEmptyMessage'></span>\r\n                        <span class='rddtIcon'></span>\r\n                    </div>\r\n                </div>");
		}

		// Token: 0x06006C71 RID: 27761 RVA: 0x00193074 File Offset: 0x00191274
		protected virtual void RenderFakeInput(HtmlTextWriter writer)
		{
			string text = "rddtFakeInput";
			if (this._control.Entries.Count == 0)
			{
				text += " rddtEmptyMessage";
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("<!-- &nbsp; -->");
			writer.RenderEndTag();
		}

		// Token: 0x06006C72 RID: 27762 RVA: 0x001930C7 File Offset: 0x001912C7
		protected virtual void RenderArrow(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rddtIcon");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("<!-- &nbsp; -->");
			writer.RenderEndTag();
		}

		// Token: 0x06006C73 RID: 27763 RVA: 0x001930F0 File Offset: 0x001912F0
		protected virtual void RenderDropDown(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rddtSlide");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			string text = (string.Format(this.DropDownTreeCssClassFormatString, this._control.RuntimeSkin) + " " + this._control.DropDownSettings.CssClass).Trim();
			if (this._control.DropDownSettings.AutoWidth == DropDownTreeAutoWidth.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text + " rddtAutoWidth");
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderHeader(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rddtScroll");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderTreeView(writer);
			writer.RenderEndTag();
			this.RenderFooter(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06006C74 RID: 27764 RVA: 0x001931BD File Offset: 0x001913BD
		private void RenderHeader(HtmlTextWriter writer)
		{
			this._control.Header.RenderControl(writer);
		}

		// Token: 0x06006C75 RID: 27765 RVA: 0x001931D0 File Offset: 0x001913D0
		private void RenderFooter(HtmlTextWriter writer)
		{
			this._control.Footer.RenderControl(writer);
		}

		// Token: 0x06006C76 RID: 27766 RVA: 0x001931E3 File Offset: 0x001913E3
		protected virtual void RenderTreeView(HtmlTextWriter writer)
		{
			this._control.EmbeddedTreeRenderer.RenderContents(writer);
		}

		// Token: 0x17002396 RID: 9110
		// (get) Token: 0x06006C77 RID: 27767 RVA: 0x001931F6 File Offset: 0x001913F6
		public string CssClassFormatString
		{
			get
			{
				return "rddtPopup rddtPopup_{0}";
			}
		}

		// Token: 0x04001D2E RID: 7470
		private RadDropDownTree _control;
	}
}

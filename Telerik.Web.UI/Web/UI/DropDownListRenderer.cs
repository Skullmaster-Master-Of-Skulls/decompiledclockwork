using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Common.Helpers;

namespace Telerik.Web.UI
{
	// Token: 0x02000B28 RID: 2856
	public class DropDownListRenderer : IRenderer
	{
		// Token: 0x06006B0E RID: 27406 RVA: 0x0019046A File Offset: 0x0018E66A
		public DropDownListRenderer(RadDropDownList control)
		{
			this._control = control;
		}

		// Token: 0x1700230F RID: 8975
		// (get) Token: 0x06006B0F RID: 27407 RVA: 0x00190479 File Offset: 0x0018E679
		protected virtual string DropDownCssClassFormatString
		{
			get
			{
				return this.CssClassFormatString;
			}
		}

		// Token: 0x17002310 RID: 8976
		// (get) Token: 0x06006B10 RID: 27408 RVA: 0x00190481 File Offset: 0x0018E681
		protected virtual string DropDownIDFormatString
		{
			get
			{
				return "{0}_DropDown";
			}
		}

		// Token: 0x17002311 RID: 8977
		// (get) Token: 0x06006B11 RID: 27409 RVA: 0x00190488 File Offset: 0x0018E688
		public string CssClassFormatString
		{
			get
			{
				return "rddlPopup rddlPopup_{0}";
			}
		}

		// Token: 0x17002312 RID: 8978
		// (get) Token: 0x06006B12 RID: 27410 RVA: 0x0019048F File Offset: 0x0018E68F
		public virtual HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06006B13 RID: 27411 RVA: 0x00190493 File Offset: 0x0018E693
		public virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			this._control.CallBaseAddAttributesToRender(writer);
		}

		// Token: 0x06006B14 RID: 27412 RVA: 0x001904A4 File Offset: 0x0018E6A4
		public virtual void RenderContents(HtmlTextWriter writer)
		{
			if (this._control.InDesignMode)
			{
				this.RenderDesignTimeStyles(writer);
			}
			string arg = this._control.IsControlEnabled ? string.Empty : "rddlDisabled";
			string value = string.Format("{0} {1}", "rddlInner", arg).Trim();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			this.RenderTextArea(writer);
			this.RenderArrow(writer);
			writer.RenderEndTag();
			this.RenderDropDown(writer);
		}

		// Token: 0x06006B15 RID: 27413 RVA: 0x00190524 File Offset: 0x0018E724
		protected virtual void RenderTextArea(HtmlTextWriter writer)
		{
			string text = "rddlFakeInput";
			string value;
			if (string.IsNullOrEmpty(this._control.SelectedText) && this._control.SelectedItem == null)
			{
				value = this._control.DefaultMessage;
				text += " rddlDefaultMessage";
			}
			else
			{
				value = this._control.SelectedText;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(value);
			writer.RenderEndTag();
		}

		// Token: 0x06006B16 RID: 27414 RVA: 0x0019059C File Offset: 0x0018E79C
		protected virtual void RenderArrow(HtmlTextWriter writer)
		{
			if (this._control.ResolvedRenderMode == RenderMode.Lightweight)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rddlSelect");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				IconHelper.RenderIcon(writer, "arrow-60-down");
				writer.RenderEndTag();
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rddlIcon");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("<!-- &nbsp; -->");
			writer.RenderEndTag();
		}

		// Token: 0x06006B17 RID: 27415 RVA: 0x00190604 File Offset: 0x0018E804
		protected virtual void RenderDropDown(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rddlSlide");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			if (this._control.ZIndex != 7000)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, this._control.ZIndex.ToString());
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format(this.DropDownIDFormatString, this._control.ClientID));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format(this.DropDownCssClassFormatString, this._control.RuntimeSkin));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (this._control.EnableVirtualScrolling || this._control.Items.Count > 0)
			{
				this.RenderDropDownContents(writer);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06006B18 RID: 27416 RVA: 0x001906D8 File Offset: 0x0018E8D8
		protected virtual void RenderDropDownContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rddlList");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			bool flag = false;
			if (this._control.EnableVirtualScrolling)
			{
				DropDownListItem dropDownListItem = new DropDownListItem
				{
					Text = "Test"
				};
				dropDownListItem.RenderControl(writer);
			}
			else
			{
				foreach (object obj in this._control.Items)
				{
					DropDownListItem dropDownListItem2 = (DropDownListItem)obj;
					if (dropDownListItem2.Selected)
					{
						if (flag)
						{
							throw new HttpException("Cannot have multiple items selected.");
						}
						flag = true;
					}
					dropDownListItem2.RenderControl(writer);
				}
			}
			writer.RenderEndTag();
		}

		// Token: 0x06006B19 RID: 27417 RVA: 0x0019079C File Offset: 0x0018E99C
		protected virtual void RenderDesignTimeStyles(HtmlTextWriter writer)
		{
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
			writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this._control));
			writer.Write("<style type=\"text/css\">");
			writer.Write(" .RadDropDownList { display:inline-block !important; \r\n                                               width: " + str + " !important; }\r\n                            .rddlFakeInput {\r\n                                    height: 16px !important; \r\n                                    width: 80% !important;}");
			writer.Write("</style>");
		}

		// Token: 0x04001CF3 RID: 7411
		private RadDropDownList _control;
	}
}

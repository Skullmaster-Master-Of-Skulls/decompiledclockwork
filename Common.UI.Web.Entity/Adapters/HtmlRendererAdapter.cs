using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.Entity.Adapters
{
	// Token: 0x02000054 RID: 84
	public static class HtmlRendererAdapter
	{
		// Token: 0x06000268 RID: 616 RVA: 0x00005378 File Offset: 0x00003578
		public static HtmlTextWriter WriteMyText(this HtmlTextWriter writer, string text)
		{
			writer.Write(text);
			return writer;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00005394 File Offset: 0x00003594
		public static HtmlTextWriter RenderMyBeginTag(this HtmlTextWriter writer, bool actuallyRenderThisControl, HtmlTextWriterTag tag, string cssClass = "", Control forControl = null, params MyStyleAttribute[] styles)
		{
			bool flag = !actuallyRenderThisControl;
			HtmlTextWriter result;
			if (flag)
			{
				result = writer;
			}
			else
			{
				result = writer.RenderMyBeginTag(tag, cssClass, forControl, styles);
			}
			return result;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000053C0 File Offset: 0x000035C0
		public static HtmlTextWriter RenderMyBeginTag(this HtmlTextWriter writer, HtmlTextWriterTag tag, string cssClass = "", Control forControl = null, params MyStyleAttribute[] styles)
		{
			bool flag = !string.IsNullOrEmpty(cssClass);
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
			}
			bool flag2 = forControl != null;
			if (flag2)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.For, forControl.ClientID ?? "");
			}
			bool flag3 = styles != null;
			if (flag3)
			{
				foreach (MyStyleAttribute myStyleAttribute in styles)
				{
					writer.AddStyleAttribute(myStyleAttribute.StyleTag, myStyleAttribute.Value);
				}
			}
			writer.RenderBeginTag(tag);
			return writer;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00005450 File Offset: 0x00003650
		public static HtmlTextWriter RenderMyEndTag(this HtmlTextWriter writer, bool actuallyRenderThisTag = true)
		{
			if (actuallyRenderThisTag)
			{
				writer.RenderEndTag();
			}
			return writer;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00005470 File Offset: 0x00003670
		public static HtmlTextWriter RenderMyLabel(this HtmlTextWriter writer, bool actuallyRenderThisControl, string cssClass, Control forControl, string text, params MyStyleAttribute[] styles)
		{
			bool flag = !actuallyRenderThisControl;
			HtmlTextWriter result;
			if (flag)
			{
				result = writer;
			}
			else
			{
				result = writer.RenderMyLabel(cssClass, forControl, text, styles);
			}
			return result;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000549C File Offset: 0x0000369C
		public static HtmlTextWriter RenderMyLabel(this HtmlTextWriter writer, string cssClass, Control forControl, string text, params MyStyleAttribute[] styles)
		{
			bool flag = styles != null;
			if (flag)
			{
				foreach (MyStyleAttribute myStyleAttribute in styles)
				{
					writer.AddStyleAttribute(myStyleAttribute.StyleTag, myStyleAttribute.Value);
				}
			}
			return writer.RenderMyBeginTag(HtmlTextWriterTag.Label, cssClass, forControl, Array.Empty<MyStyleAttribute>()).WriteMyText(text).RenderMyEndTag(true);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00005500 File Offset: 0x00003700
		public static HtmlTextWriter RenderMyHeaderTag(this HtmlTextWriter writer, HtmlTextWriterTag headerTag, string cssClass, string text, params MyStyleAttribute[] styles)
		{
			bool flag = styles != null;
			if (flag)
			{
				foreach (MyStyleAttribute myStyleAttribute in styles)
				{
					writer.AddStyleAttribute(myStyleAttribute.StyleTag, myStyleAttribute.Value);
				}
			}
			return writer.RenderMyBeginTag(headerTag, cssClass, null, Array.Empty<MyStyleAttribute>()).WriteMyText(text).RenderMyEndTag(true);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00005560 File Offset: 0x00003760
		public static HtmlTextWriter RenderMyControl(this HtmlTextWriter writer, bool actuallyRenderThisControl, Control control)
		{
			bool flag = !actuallyRenderThisControl;
			HtmlTextWriter result;
			if (flag)
			{
				result = writer;
			}
			else
			{
				result = writer.RenderMyControl(control);
			}
			return result;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00005588 File Offset: 0x00003788
		public static HtmlTextWriter RenderMyControl(this HtmlTextWriter writer, Control control)
		{
			control.RenderControl(writer);
			return writer;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000055A4 File Offset: 0x000037A4
		public static HtmlTextWriter RenderMyRadioButtonList(this HtmlTextWriter writer, RadioButtonList rbtns, string title)
		{
			writer.RenderMyBeginTag(HtmlTextWriterTag.Fieldset, "form-group", null, new MyStyleAttribute[]
			{
				new MyStyleAttribute(HtmlTextWriterStyle.Display, "table-cell")
			}).RenderMyBeginTag(HtmlTextWriterTag.Legend, "", null, new MyStyleAttribute[]
			{
				new MyStyleAttribute(HtmlTextWriterStyle.MarginBottom, "4px")
			}).RenderMyLabel("", rbtns, title, Array.Empty<MyStyleAttribute>()).RenderMyEndTag(true).RenderMyBeginTag(HtmlTextWriterTag.Div, "radio radiobuttonlist", null, Array.Empty<MyStyleAttribute>()).RenderMyControl(rbtns).RenderMyEndTag(true).RenderMyEndTag(true);
			return writer;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00005638 File Offset: 0x00003838
		public static HtmlTextWriter RenderMyCheckBoxList(this HtmlTextWriter writer, CheckBoxList chks, string title)
		{
			writer.RenderMyBeginTag(HtmlTextWriterTag.Fieldset, "form-group", null, new MyStyleAttribute[]
			{
				new MyStyleAttribute(HtmlTextWriterStyle.Display, "table-cell")
			}).RenderMyBeginTag(HtmlTextWriterTag.Legend, "", null, new MyStyleAttribute[]
			{
				new MyStyleAttribute(HtmlTextWriterStyle.MarginBottom, "4px")
			}).RenderMyLabel("", chks, title, Array.Empty<MyStyleAttribute>()).RenderMyEndTag(true).RenderMyBeginTag(HtmlTextWriterTag.Div, "check checkboxlist", null, Array.Empty<MyStyleAttribute>()).RenderMyControl(chks).RenderMyEndTag(true).RenderMyEndTag(true);
			return writer;
		}
	}
}

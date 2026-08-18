using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ToolBar.Renderers
{
	// Token: 0x02000954 RID: 2388
	internal class ToolBarClassicRenderer : ToolBarRenderBase
	{
		// Token: 0x06005B17 RID: 23319 RVA: 0x001152A0 File Offset: 0x001134A0
		public ToolBarClassicRenderer(RadToolBar toolBar) : base(toolBar)
		{
		}

		// Token: 0x06005B18 RID: 23320 RVA: 0x001152AC File Offset: 0x001134AC
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (string.IsNullOrEmpty(base.Owner.Style["z-index"]))
			{
				base.Owner.Style["z-index"] = 9000.ToString();
			}
			base.Owner.Height = Unit.Empty;
		}

		// Token: 0x06005B19 RID: 23321 RVA: 0x00115308 File Offset: 0x00113508
		public override void RenderContents(HtmlTextWriter writer)
		{
			string text = "rtbOuter";
			if (base.Owner.EnableRoundedCorners)
			{
				text = ToolBarStyles.Combine(new string[]
				{
					text,
					"rtbRoundedCorners"
				});
			}
			if (base.Owner.EnableShadows)
			{
				text = ToolBarStyles.Combine(new string[]
				{
					text,
					"rtbShadows"
				});
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbMiddle");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (base.Owner._toolBarHeight != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, base.Owner._toolBarHeight.ToString());
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbInner");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbUL");
			if (base.Owner.Items.Count > 0)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				base.Owner.BaseRenderChildren(writer);
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x040015F1 RID: 5617
		private const int DefaultZIndex = 9000;
	}
}

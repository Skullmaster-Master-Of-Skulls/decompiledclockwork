using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000C0D RID: 3085
	[ToolboxItem(false)]
	public class OrgChartNotLoadedGroupItemRenderer : WebControl
	{
		// Token: 0x060075B1 RID: 30129 RVA: 0x001B6230 File Offset: 0x001B4430
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			string value = "rocItemWrap rocFirst rocEmptyItemWrap";
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			base.RenderBeginTag(writer);
		}

		// Token: 0x060075B2 RID: 30130 RVA: 0x001B6254 File Offset: 0x001B4454
		protected override void RenderContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocItem rocNotLoadedItem");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			base.RenderContents(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocItemContent");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rocItemText");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.Write("Expand to load items");
			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x17002649 RID: 9801
		// (get) Token: 0x060075B3 RID: 30131 RVA: 0x001B62C4 File Offset: 0x001B44C4
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}
	}
}

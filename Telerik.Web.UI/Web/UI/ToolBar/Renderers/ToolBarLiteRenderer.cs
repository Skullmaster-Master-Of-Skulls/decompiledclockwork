using System;
using System.Web.UI;

namespace Telerik.Web.UI.ToolBar.Renderers
{
	// Token: 0x02000955 RID: 2389
	internal class ToolBarLiteRenderer : ToolBarRenderBase
	{
		// Token: 0x06005B1A RID: 23322 RVA: 0x00115425 File Offset: 0x00113625
		public ToolBarLiteRenderer(RadToolBar toolBar) : base(toolBar)
		{
		}

		// Token: 0x06005B1B RID: 23323 RVA: 0x00115430 File Offset: 0x00113630
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (string.IsNullOrEmpty(base.Owner.Style["z-index"]))
			{
				base.Owner.Style["z-index"] = 5.ToString();
			}
		}

		// Token: 0x06005B1C RID: 23324 RVA: 0x00115477 File Offset: 0x00113677
		public override void RenderContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtbUL");
			if (base.Owner.Items.Count > 0)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Ul);
				base.Owner.BaseRenderChildren(writer);
				writer.RenderEndTag();
			}
		}

		// Token: 0x040015F2 RID: 5618
		private const int DefaultZIndex = 5;
	}
}

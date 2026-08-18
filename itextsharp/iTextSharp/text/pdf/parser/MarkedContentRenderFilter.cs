using System;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x02000329 RID: 809
	public class MarkedContentRenderFilter : RenderFilter
	{
		// Token: 0x06001D54 RID: 7508 RVA: 0x000B03F4 File Offset: 0x000AF3F4
		public MarkedContentRenderFilter(int mcid)
		{
			this.mcid = mcid;
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x000B0403 File Offset: 0x000AF403
		public override bool AllowText(TextRenderInfo renderInfo)
		{
			return renderInfo.HasMcid(this.mcid);
		}

		// Token: 0x0400142E RID: 5166
		private int mcid;
	}
}

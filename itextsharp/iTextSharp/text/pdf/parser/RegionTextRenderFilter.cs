using System;
using System.util;

namespace iTextSharp.text.pdf.parser
{
	// Token: 0x02000327 RID: 807
	public class RegionTextRenderFilter : RenderFilter
	{
		// Token: 0x06001D4A RID: 7498 RVA: 0x000AFEA0 File Offset: 0x000AEEA0
		public RegionTextRenderFilter(RectangleJ filterRect)
		{
			this.filterRect = filterRect;
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x000AFEB0 File Offset: 0x000AEEB0
		public override bool AllowText(TextRenderInfo renderInfo)
		{
			LineSegment baseline = renderInfo.GetBaseline();
			Vector startPoint = baseline.GetStartPoint();
			Vector endPoint = baseline.GetEndPoint();
			float num = startPoint[0];
			float num2 = startPoint[1];
			float num3 = endPoint[0];
			float num4 = endPoint[1];
			return this.filterRect.IntersectsLine((double)num, (double)num2, (double)num3, (double)num4);
		}

		// Token: 0x0400142D RID: 5165
		private RectangleJ filterRect;
	}
}

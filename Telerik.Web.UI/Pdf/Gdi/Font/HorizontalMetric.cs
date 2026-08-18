using System;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001620 RID: 5664
	internal class HorizontalMetric
	{
		// Token: 0x0600DC5B RID: 56411 RVA: 0x00302A64 File Offset: 0x00300C64
		public HorizontalMetric(int advanceWidth, short leftSideBearing)
		{
			this.advanceWidth = advanceWidth;
			this.leftSideBearing = leftSideBearing;
		}

		// Token: 0x0600DC5C RID: 56412 RVA: 0x00302A7A File Offset: 0x00300C7A
		public HorizontalMetric Clone()
		{
			return new HorizontalMetric(this.advanceWidth, this.leftSideBearing);
		}

		// Token: 0x1700436D RID: 17261
		// (get) Token: 0x0600DC5D RID: 56413 RVA: 0x00302A8D File Offset: 0x00300C8D
		public int AdvanceWidth
		{
			get
			{
				return this.advanceWidth;
			}
		}

		// Token: 0x1700436E RID: 17262
		// (get) Token: 0x0600DC5E RID: 56414 RVA: 0x00302A95 File Offset: 0x00300C95
		public short LeftSideBearing
		{
			get
			{
				return this.leftSideBearing;
			}
		}

		// Token: 0x04003DDC RID: 15836
		private int advanceWidth;

		// Token: 0x04003DDD RID: 15837
		private short leftSideBearing;
	}
}

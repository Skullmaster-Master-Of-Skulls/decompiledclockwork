using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001BA RID: 442
	public interface IChartFrameFormat : IChartFillBorder
	{
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001881 RID: 6273
		// (set) Token: 0x06001882 RID: 6274
		RectangleStyleType RectangleStyle { get; set; }

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001883 RID: 6275
		// (set) Token: 0x06001884 RID: 6276
		bool IsBorderCornersRound { get; set; }

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001885 RID: 6277
		IChartBorder Border { get; }

		// Token: 0x06001886 RID: 6278
		void Clear();
	}
}

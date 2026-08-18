using System;

namespace Spire.Xls.Core
{
	// Token: 0x0200021B RID: 539
	public interface IChartShape : IShape, IChart
	{
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06002043 RID: 8259
		// (set) Token: 0x06002044 RID: 8260
		int TopRow { get; set; }

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06002045 RID: 8261
		// (set) Token: 0x06002046 RID: 8262
		int BottomRow { get; set; }

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06002047 RID: 8263
		// (set) Token: 0x06002048 RID: 8264
		int LeftColumn { get; set; }

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06002049 RID: 8265
		// (set) Token: 0x0600204A RID: 8266
		int RightColumn { get; set; }
	}
}

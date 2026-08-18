using System;
using Spire.Xls.Charts;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core
{
	// Token: 0x020001BF RID: 447
	public interface IChartAxis
	{
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001908 RID: 6408
		// (set) Token: 0x06001909 RID: 6409
		string NumberFormat { get; set; }

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x0600190A RID: 6410
		AxisType AxisType { get; }

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x0600190B RID: 6411
		// (set) Token: 0x0600190C RID: 6412
		string Title { get; set; }

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x0600190D RID: 6413
		// (set) Token: 0x0600190E RID: 6414
		int TextRotationAngle { get; set; }

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600190F RID: 6415
		IChartTextArea TitleArea { get; }

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06001910 RID: 6416
		IFont Font { get; }

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06001911 RID: 6417
		IChartGridLine MajorGridLines { get; }

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06001912 RID: 6418
		IChartGridLine MinorGridLines { get; }

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06001913 RID: 6419
		// (set) Token: 0x06001914 RID: 6420
		bool HasMinorGridLines { get; set; }

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06001915 RID: 6421
		// (set) Token: 0x06001916 RID: 6422
		bool HasMajorGridLines { get; set; }

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06001917 RID: 6423
		// (set) Token: 0x06001918 RID: 6424
		TickMarkType MinorTickMark { get; set; }

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001919 RID: 6425
		// (set) Token: 0x0600191A RID: 6426
		TickMarkType MajorTickMark { get; set; }

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x0600191B RID: 6427
		ChartBorder Border { get; }

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x0600191C RID: 6428
		// (set) Token: 0x0600191D RID: 6429
		TickLabelPositionType TickLabelPosition { get; set; }

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x0600191E RID: 6430
		// (set) Token: 0x0600191F RID: 6431
		bool Visible { get; set; }

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001920 RID: 6432
		// (set) Token: 0x06001921 RID: 6433
		AxisTextDirectionType Alignment { get; set; }

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06001922 RID: 6434
		ChartShadow Shadow { get; }

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06001923 RID: 6435
		IFormat3D Chart3DOptions { get; }
	}
}

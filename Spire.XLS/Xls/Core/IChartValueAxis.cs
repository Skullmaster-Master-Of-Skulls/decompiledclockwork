using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001C0 RID: 448
	public interface IChartValueAxis : IChartAxis
	{
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06001924 RID: 6436
		// (set) Token: 0x06001925 RID: 6437
		double MinValue { get; set; }

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001926 RID: 6438
		// (set) Token: 0x06001927 RID: 6439
		double MaxValue { get; set; }

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001928 RID: 6440
		// (set) Token: 0x06001929 RID: 6441
		double MajorUnit { get; set; }

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x0600192A RID: 6442
		// (set) Token: 0x0600192B RID: 6443
		double MinorUnit { get; set; }

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x0600192C RID: 6444
		// (set) Token: 0x0600192D RID: 6445
		double CrossValue { get; set; }

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x0600192E RID: 6446
		// (set) Token: 0x0600192F RID: 6447
		double CrossesAt { get; set; }

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06001930 RID: 6448
		// (set) Token: 0x06001931 RID: 6449
		bool IsAutoMin { get; set; }

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06001932 RID: 6450
		// (set) Token: 0x06001933 RID: 6451
		bool IsAutoMax { get; set; }

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06001934 RID: 6452
		// (set) Token: 0x06001935 RID: 6453
		bool IsAutoMajor { get; set; }

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06001936 RID: 6454
		// (set) Token: 0x06001937 RID: 6455
		bool IsAutoMinor { get; set; }

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001938 RID: 6456
		// (set) Token: 0x06001939 RID: 6457
		bool IsAutoCross { get; set; }

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x0600193A RID: 6458
		// (set) Token: 0x0600193B RID: 6459
		bool IsLogScale { get; set; }

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x0600193C RID: 6460
		// (set) Token: 0x0600193D RID: 6461
		bool IsReverseOrder { get; set; }

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x0600193E RID: 6462
		// (set) Token: 0x0600193F RID: 6463
		bool IsMaxCross { get; set; }
	}
}

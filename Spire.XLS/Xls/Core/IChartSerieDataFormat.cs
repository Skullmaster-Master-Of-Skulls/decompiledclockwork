using System;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x020001C7 RID: 455
	public interface IChartSerieDataFormat : IChartFillBorder
	{
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06001987 RID: 6535
		IChartInterior AreaProperties { get; }

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001988 RID: 6536
		// (set) Token: 0x06001989 RID: 6537
		BaseFormatType BarType { get; set; }

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x0600198A RID: 6538
		// (set) Token: 0x0600198B RID: 6539
		TopFormatType BarTopType { get; set; }

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600198C RID: 6540
		// (set) Token: 0x0600198D RID: 6541
		Color MarkerBackgroundColor { get; set; }

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600198E RID: 6542
		// (set) Token: 0x0600198F RID: 6543
		Color MarkerForegroundColor { get; set; }

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06001990 RID: 6544
		// (set) Token: 0x06001991 RID: 6545
		ChartMarkerType MarkerStyle { get; set; }

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06001992 RID: 6546
		// (set) Token: 0x06001993 RID: 6547
		ExcelColors MarkerForegroundKnownColor { get; set; }

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06001994 RID: 6548
		// (set) Token: 0x06001995 RID: 6549
		ExcelColors MarkerBackgroundKnownColor { get; set; }

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06001996 RID: 6550
		// (set) Token: 0x06001997 RID: 6551
		int MarkerSize { get; set; }

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001998 RID: 6552
		// (set) Token: 0x06001999 RID: 6553
		bool IsAutoMarker { get; set; }

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x0600199A RID: 6554
		// (set) Token: 0x0600199B RID: 6555
		int Percent { get; set; }

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600199C RID: 6556
		// (set) Token: 0x0600199D RID: 6557
		bool Is3DBubbles { get; set; }

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x0600199E RID: 6558
		IChartFormat Options { get; }

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x0600199F RID: 6559
		bool IsMarkerSupported { get; }
	}
}

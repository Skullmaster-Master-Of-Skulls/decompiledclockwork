using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001CD RID: 461
	public interface IChartLegend
	{
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060019CD RID: 6605
		IChartTextArea TextArea { get; }

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060019CE RID: 6606
		// (set) Token: 0x060019CF RID: 6607
		int X { get; set; }

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060019D0 RID: 6608
		// (set) Token: 0x060019D1 RID: 6609
		int Y { get; set; }

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060019D2 RID: 6610
		// (set) Token: 0x060019D3 RID: 6611
		LegendPositionType Position { get; set; }

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060019D4 RID: 6612
		// (set) Token: 0x060019D5 RID: 6613
		bool IsVerticalLegend { get; set; }

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060019D6 RID: 6614
		IChartLegendEntries LegendEntries { get; }

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060019D7 RID: 6615
		// (set) Token: 0x060019D8 RID: 6616
		bool IncludeInLayout { get; set; }

		// Token: 0x060019D9 RID: 6617
		void Clear();

		// Token: 0x060019DA RID: 6618
		void Delete();
	}
}

using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001BD RID: 445
	public interface IChartLegendEntry
	{
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x0600189C RID: 6300
		// (set) Token: 0x0600189D RID: 6301
		bool IsDeleted { get; set; }

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x0600189E RID: 6302
		// (set) Token: 0x0600189F RID: 6303
		bool IsFormatted { get; set; }

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060018A0 RID: 6304
		IChartTextArea TextArea { get; }

		// Token: 0x060018A1 RID: 6305
		void Clear();

		// Token: 0x060018A2 RID: 6306
		void Delete();
	}
}

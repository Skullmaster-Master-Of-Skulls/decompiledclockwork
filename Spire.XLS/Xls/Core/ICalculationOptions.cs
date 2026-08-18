using System;

namespace Spire.Xls.Core
{
	// Token: 0x0200041F RID: 1055
	public interface ICalculationOptions : IExcelApplication
	{
		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06003F07 RID: 16135
		// (set) Token: 0x06003F08 RID: 16136
		int MaximumIteration { get; set; }

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06003F09 RID: 16137
		// (set) Token: 0x06003F0A RID: 16138
		bool RecalcOnSave { get; set; }

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06003F0B RID: 16139
		// (set) Token: 0x06003F0C RID: 16140
		double MaximumChange { get; set; }

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06003F0D RID: 16141
		// (set) Token: 0x06003F0E RID: 16142
		bool IsIterationEnabled { get; set; }

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06003F0F RID: 16143
		// (set) Token: 0x06003F10 RID: 16144
		bool R1C1ReferenceMode { get; set; }

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06003F11 RID: 16145
		// (set) Token: 0x06003F12 RID: 16146
		ExcelCalculationMode CalculationMode { get; set; }
	}
}

using System;

namespace Spire.Xls.Core.Interfaces
{
	// Token: 0x0200000E RID: 14
	public interface IFormat3D
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000D0 RID: 208
		// (set) Token: 0x060000D1 RID: 209
		XLSXChartBevelType BevelTopType { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000D2 RID: 210
		// (set) Token: 0x060000D3 RID: 211
		XLSXChartBevelType BevelBottomType { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000D4 RID: 212
		// (set) Token: 0x060000D5 RID: 213
		XLSXChartMaterialType MaterialType { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000D6 RID: 214
		// (set) Token: 0x060000D7 RID: 215
		XLSXChartLightingType LightingType { get; set; }
	}
}

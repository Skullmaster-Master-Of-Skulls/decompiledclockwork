using System;
using Spire.Xls.Charts;

namespace Spire.Xls.Core
{
	// Token: 0x020005D3 RID: 1491
	public interface IChartFillBorder
	{
		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x0600589E RID: 22686
		bool HasInterior { get; }

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x0600589F RID: 22687
		bool HasLineProperties { get; }

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x060058A0 RID: 22688
		bool HasFormat3D { get; }

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x060058A1 RID: 22689
		bool HasShadow { get; }

		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x060058A2 RID: 22690
		ChartBorder LineProperties { get; }

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x060058A3 RID: 22691
		IChartInterior Interior { get; }

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x060058A4 RID: 22692
		IShapeFill Fill { get; }

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x060058A5 RID: 22693
		Format3D Format3D { get; }

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x060058A6 RID: 22694
		ChartShadow Shadow { get; }
	}
}

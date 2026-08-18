using System;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core
{
	// Token: 0x020005E1 RID: 1505
	public interface IChartPageSetup : IPageSetupBase
	{
		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06005985 RID: 22917
		// (set) Token: 0x06005986 RID: 22918
		bool FitToPagesTall { get; set; }

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06005987 RID: 22919
		// (set) Token: 0x06005988 RID: 22920
		bool FitToPagesWide { get; set; }
	}
}

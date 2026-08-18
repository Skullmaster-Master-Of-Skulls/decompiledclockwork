using System;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x0200001D RID: 29
	internal interface IEntityAdapter
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600020D RID: 525
		// (set) Token: 0x0600020E RID: 526
		DbConnection Connection { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600020F RID: 527
		// (set) Token: 0x06000210 RID: 528
		bool AcceptChangesDuringUpdate { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000211 RID: 529
		// (set) Token: 0x06000212 RID: 530
		int? CommandTimeout { get; set; }

		// Token: 0x06000213 RID: 531
		int Update(IEntityStateManager cache);
	}
}

using System;

namespace System.Data
{
	// Token: 0x02000108 RID: 264
	public interface IDbDataParameter : IDataParameter
	{
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060010C4 RID: 4292
		// (set) Token: 0x060010C5 RID: 4293
		byte Precision { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060010C6 RID: 4294
		// (set) Token: 0x060010C7 RID: 4295
		byte Scale { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060010C8 RID: 4296
		// (set) Token: 0x060010C9 RID: 4297
		int Size { get; set; }
	}
}

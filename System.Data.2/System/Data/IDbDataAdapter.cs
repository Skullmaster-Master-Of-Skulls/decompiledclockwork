using System;

namespace System.Data
{
	// Token: 0x02000107 RID: 263
	public interface IDbDataAdapter : IDataAdapter
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060010BC RID: 4284
		// (set) Token: 0x060010BD RID: 4285
		IDbCommand SelectCommand { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060010BE RID: 4286
		// (set) Token: 0x060010BF RID: 4287
		IDbCommand InsertCommand { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060010C0 RID: 4288
		// (set) Token: 0x060010C1 RID: 4289
		IDbCommand UpdateCommand { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060010C2 RID: 4290
		// (set) Token: 0x060010C3 RID: 4291
		IDbCommand DeleteCommand { get; set; }
	}
}

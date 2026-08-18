using System;

namespace System.Data
{
	// Token: 0x020000BE RID: 190
	public interface IDbDataAdapter : IDataAdapter
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000C91 RID: 3217
		// (set) Token: 0x06000C92 RID: 3218
		IDbCommand SelectCommand { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000C93 RID: 3219
		// (set) Token: 0x06000C94 RID: 3220
		IDbCommand InsertCommand { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000C95 RID: 3221
		// (set) Token: 0x06000C96 RID: 3222
		IDbCommand UpdateCommand { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000C97 RID: 3223
		// (set) Token: 0x06000C98 RID: 3224
		IDbCommand DeleteCommand { get; set; }
	}
}

using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000123 RID: 291
	[StructLayout(LayoutKind.Explicit)]
	internal struct OpoITLValCtx
	{
		// Token: 0x0400097C RID: 2428
		[FieldOffset(0)]
		internal IYMCtx m_ym;

		// Token: 0x0400097D RID: 2429
		[FieldOffset(0)]
		internal IDSCtx m_ds;

		// Token: 0x0400097E RID: 2430
		[FieldOffset(20)]
		internal byte m_type;

		// Token: 0x0400097F RID: 2431
		[FieldOffset(21)]
		internal short m_regid;
	}
}

using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000B6 RID: 182
	internal enum HAEventType
	{
		// Token: 0x040005A4 RID: 1444
		Invalid,
		// Token: 0x040005A5 RID: 1445
		DatabaseDown,
		// Token: 0x040005A6 RID: 1446
		DatabaseUp,
		// Token: 0x040005A7 RID: 1447
		InstanceDown,
		// Token: 0x040005A8 RID: 1448
		InstanceUp,
		// Token: 0x040005A9 RID: 1449
		NodeDown,
		// Token: 0x040005AA RID: 1450
		NodeUp,
		// Token: 0x040005AB RID: 1451
		ServiceDown,
		// Token: 0x040005AC RID: 1452
		ServiceUp,
		// Token: 0x040005AD RID: 1453
		ServiceMemberDown,
		// Token: 0x040005AE RID: 1454
		ServiceMemberUp
	}
}

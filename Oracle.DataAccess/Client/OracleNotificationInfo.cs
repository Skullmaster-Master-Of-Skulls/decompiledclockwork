using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x020000BB RID: 187
	public enum OracleNotificationInfo
	{
		// Token: 0x040005C5 RID: 1477
		Insert = 1,
		// Token: 0x040005C6 RID: 1478
		Delete = 16,
		// Token: 0x040005C7 RID: 1479
		Update = 32,
		// Token: 0x040005C8 RID: 1480
		Startup = 64,
		// Token: 0x040005C9 RID: 1481
		Shutdown = 128,
		// Token: 0x040005CA RID: 1482
		Shutdown_any = 256,
		// Token: 0x040005CB RID: 1483
		Alter = 512,
		// Token: 0x040005CC RID: 1484
		Drop = 1024,
		// Token: 0x040005CD RID: 1485
		End = 2048,
		// Token: 0x040005CE RID: 1486
		Error = 4096
	}
}

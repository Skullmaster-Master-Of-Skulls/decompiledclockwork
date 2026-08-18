using System;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x0200004C RID: 76
	public enum OracleNotificationInfo
	{
		// Token: 0x04000512 RID: 1298
		Insert = 1,
		// Token: 0x04000513 RID: 1299
		Delete = 16,
		// Token: 0x04000514 RID: 1300
		Update = 32,
		// Token: 0x04000515 RID: 1301
		Startup = 64,
		// Token: 0x04000516 RID: 1302
		Shutdown = 128,
		// Token: 0x04000517 RID: 1303
		Shutdown_any = 256,
		// Token: 0x04000518 RID: 1304
		Alter = 512,
		// Token: 0x04000519 RID: 1305
		Drop = 1024,
		// Token: 0x0400051A RID: 1306
		End = 2048,
		// Token: 0x0400051B RID: 1307
		Error = 4096
	}
}

using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001E8 RID: 488
	public enum SqlNotificationSource
	{
		// Token: 0x04001163 RID: 4451
		Data,
		// Token: 0x04001164 RID: 4452
		Timeout,
		// Token: 0x04001165 RID: 4453
		Object,
		// Token: 0x04001166 RID: 4454
		Database,
		// Token: 0x04001167 RID: 4455
		System,
		// Token: 0x04001168 RID: 4456
		Statement,
		// Token: 0x04001169 RID: 4457
		Environment,
		// Token: 0x0400116A RID: 4458
		Execution,
		// Token: 0x0400116B RID: 4459
		Owner,
		// Token: 0x0400116C RID: 4460
		Unknown = -1,
		// Token: 0x0400116D RID: 4461
		Client = -2
	}
}

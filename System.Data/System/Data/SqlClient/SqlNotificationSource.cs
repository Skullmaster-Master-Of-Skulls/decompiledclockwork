using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000303 RID: 771
	public enum SqlNotificationSource
	{
		// Token: 0x04001951 RID: 6481
		Data,
		// Token: 0x04001952 RID: 6482
		Timeout,
		// Token: 0x04001953 RID: 6483
		Object,
		// Token: 0x04001954 RID: 6484
		Database,
		// Token: 0x04001955 RID: 6485
		System,
		// Token: 0x04001956 RID: 6486
		Statement,
		// Token: 0x04001957 RID: 6487
		Environment,
		// Token: 0x04001958 RID: 6488
		Execution,
		// Token: 0x04001959 RID: 6489
		Owner,
		// Token: 0x0400195A RID: 6490
		Unknown = -1,
		// Token: 0x0400195B RID: 6491
		Client = -2
	}
}

using System;

namespace System.Data.SqlClient
{
	// Token: 0x020001E7 RID: 487
	public enum SqlNotificationInfo
	{
		// Token: 0x0400114F RID: 4431
		Truncate,
		// Token: 0x04001150 RID: 4432
		Insert,
		// Token: 0x04001151 RID: 4433
		Update,
		// Token: 0x04001152 RID: 4434
		Delete,
		// Token: 0x04001153 RID: 4435
		Drop,
		// Token: 0x04001154 RID: 4436
		Alter,
		// Token: 0x04001155 RID: 4437
		Restart,
		// Token: 0x04001156 RID: 4438
		Error,
		// Token: 0x04001157 RID: 4439
		Query,
		// Token: 0x04001158 RID: 4440
		Invalid,
		// Token: 0x04001159 RID: 4441
		Options,
		// Token: 0x0400115A RID: 4442
		Isolation,
		// Token: 0x0400115B RID: 4443
		Expired,
		// Token: 0x0400115C RID: 4444
		Resource,
		// Token: 0x0400115D RID: 4445
		PreviousFire,
		// Token: 0x0400115E RID: 4446
		TemplateLimit,
		// Token: 0x0400115F RID: 4447
		Merge,
		// Token: 0x04001160 RID: 4448
		Unknown = -1,
		// Token: 0x04001161 RID: 4449
		AlreadyChanged = -2
	}
}

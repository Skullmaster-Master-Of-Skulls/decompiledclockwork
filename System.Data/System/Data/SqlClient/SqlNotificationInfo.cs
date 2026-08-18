using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000302 RID: 770
	public enum SqlNotificationInfo
	{
		// Token: 0x0400193D RID: 6461
		Truncate,
		// Token: 0x0400193E RID: 6462
		Insert,
		// Token: 0x0400193F RID: 6463
		Update,
		// Token: 0x04001940 RID: 6464
		Delete,
		// Token: 0x04001941 RID: 6465
		Drop,
		// Token: 0x04001942 RID: 6466
		Alter,
		// Token: 0x04001943 RID: 6467
		Restart,
		// Token: 0x04001944 RID: 6468
		Error,
		// Token: 0x04001945 RID: 6469
		Query,
		// Token: 0x04001946 RID: 6470
		Invalid,
		// Token: 0x04001947 RID: 6471
		Options,
		// Token: 0x04001948 RID: 6472
		Isolation,
		// Token: 0x04001949 RID: 6473
		Expired,
		// Token: 0x0400194A RID: 6474
		Resource,
		// Token: 0x0400194B RID: 6475
		PreviousFire,
		// Token: 0x0400194C RID: 6476
		TemplateLimit,
		// Token: 0x0400194D RID: 6477
		Merge,
		// Token: 0x0400194E RID: 6478
		Unknown = -1,
		// Token: 0x0400194F RID: 6479
		AlreadyChanged = -2
	}
}

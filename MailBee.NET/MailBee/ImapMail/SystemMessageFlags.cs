using System;

namespace MailBee.ImapMail
{
	// Token: 0x0200017E RID: 382
	[Flags]
	public enum SystemMessageFlags
	{
		// Token: 0x04000914 RID: 2324
		None = 0,
		// Token: 0x04000915 RID: 2325
		Seen = 1,
		// Token: 0x04000916 RID: 2326
		Answered = 2,
		// Token: 0x04000917 RID: 2327
		Flagged = 4,
		// Token: 0x04000918 RID: 2328
		Deleted = 8,
		// Token: 0x04000919 RID: 2329
		Draft = 16,
		// Token: 0x0400091A RID: 2330
		Recent = 32,
		// Token: 0x0400091B RID: 2331
		CanCreate = 64,
		// Token: 0x0400091C RID: 2332
		Other = 128
	}
}

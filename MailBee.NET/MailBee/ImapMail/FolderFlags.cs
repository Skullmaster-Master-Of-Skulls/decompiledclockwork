using System;

namespace MailBee.ImapMail
{
	// Token: 0x0200017D RID: 381
	[Flags]
	public enum FolderFlags
	{
		// Token: 0x04000903 RID: 2307
		None = 0,
		// Token: 0x04000904 RID: 2308
		Noinferiors = 1,
		// Token: 0x04000905 RID: 2309
		Noselect = 2,
		// Token: 0x04000906 RID: 2310
		Marked = 4,
		// Token: 0x04000907 RID: 2311
		Unmarked = 8,
		// Token: 0x04000908 RID: 2312
		Inbox = 16,
		// Token: 0x04000909 RID: 2313
		Drafts = 32,
		// Token: 0x0400090A RID: 2314
		Sent = 64,
		// Token: 0x0400090B RID: 2315
		Spam = 128,
		// Token: 0x0400090C RID: 2316
		Trash = 256,
		// Token: 0x0400090D RID: 2317
		Starred = 512,
		// Token: 0x0400090E RID: 2318
		AllMail = 1024,
		// Token: 0x0400090F RID: 2319
		Important = 2048,
		// Token: 0x04000910 RID: 2320
		Archive = 4096,
		// Token: 0x04000911 RID: 2321
		HasChildren = 8192,
		// Token: 0x04000912 RID: 2322
		HasNoChildren = 16384
	}
}

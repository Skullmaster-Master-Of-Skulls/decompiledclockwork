using System;

namespace MailBee.ImapMail
{
	// Token: 0x02000180 RID: 384
	[Flags]
	public enum EnvelopeParts
	{
		// Token: 0x04000925 RID: 2341
		Uid = 0,
		// Token: 0x04000926 RID: 2342
		Flags = 1,
		// Token: 0x04000927 RID: 2343
		InternalDate = 2,
		// Token: 0x04000928 RID: 2344
		Rfc822Size = 4,
		// Token: 0x04000929 RID: 2345
		Envelope = 8,
		// Token: 0x0400092A RID: 2346
		BodyStructure = 16,
		// Token: 0x0400092B RID: 2347
		MessagePreview = 32,
		// Token: 0x0400092C RID: 2348
		MailBeeEnvelope = 15,
		// Token: 0x0400092D RID: 2349
		All = 63,
		// Token: 0x0400092E RID: 2350
		GmailMessageID = 64,
		// Token: 0x0400092F RID: 2351
		GmailThreadID = 128,
		// Token: 0x04000930 RID: 2352
		GmailLabels = 256
	}
}

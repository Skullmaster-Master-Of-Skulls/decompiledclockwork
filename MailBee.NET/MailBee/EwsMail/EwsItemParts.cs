using System;

namespace MailBee.EwsMail
{
	// Token: 0x02000521 RID: 1313
	[Flags]
	public enum EwsItemParts
	{
		// Token: 0x04001DCF RID: 7631
		IdOnly = 0,
		// Token: 0x04001DD0 RID: 7632
		GenericItem = 1,
		// Token: 0x04001DD1 RID: 7633
		MailMessageRecipients = 2,
		// Token: 0x04001DD2 RID: 7634
		MailMessageBody = 4,
		// Token: 0x04001DD3 RID: 7635
		MailMessageAttachments = 8,
		// Token: 0x04001DD4 RID: 7636
		MailMessageRawData = 16,
		// Token: 0x04001DD5 RID: 7637
		MailMessageFull = 31
	}
}

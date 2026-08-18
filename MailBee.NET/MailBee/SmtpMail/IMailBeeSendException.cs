using System;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x02000165 RID: 357
	public interface IMailBeeSendException
	{
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000C2E RID: 3118
		MailMessage MailMessage { get; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000C2F RID: 3119
		EmailAddressCollection ActualRecipients { get; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000C30 RID: 3120
		string ActualSenderEmail { get; }
	}
}

using System;

namespace MailBee.SmtpMail
{
	// Token: 0x0200015B RID: 347
	public interface IMailBeeNegativeSmtpResponseException : IMailBeeNegativeTextResponseException
	{
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000C17 RID: 3095
		int ResponseCode { get; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000C18 RID: 3096
		bool IsTransientError { get; }
	}
}

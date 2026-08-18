using System;

namespace MailBee
{
	// Token: 0x02000065 RID: 101
	public interface IMailBeeLoginBadCredentialsException : IMailBeeLoginNegativeResponseException
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000393 RID: 915
		string AccountName { get; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000394 RID: 916
		string Password { get; }
	}
}

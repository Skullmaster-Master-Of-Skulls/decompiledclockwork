using System;

namespace MailBee
{
	// Token: 0x02000064 RID: 100
	public interface IMailBeeLoginBadMethodException : IMailBeeLoginNegativeResponseException
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000392 RID: 914
		AuthenticationMethods BadMethod { get; }
	}
}

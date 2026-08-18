using System;

namespace MailBee.AddressCheck
{
	// Token: 0x02000089 RID: 137
	public enum AddressValidationLevel
	{
		// Token: 0x04000220 RID: 544
		RegexCheck,
		// Token: 0x04000221 RID: 545
		DnsQuery,
		// Token: 0x04000222 RID: 546
		SmtpConnection,
		// Token: 0x04000223 RID: 547
		SendAttempt,
		// Token: 0x04000224 RID: 548
		OK
	}
}

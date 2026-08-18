using System;

namespace MailBee.Security
{
	// Token: 0x0200011F RID: 287
	[Flags]
	public enum MessageVerificationFlags
	{
		// Token: 0x04000737 RID: 1847
		None = 0,
		// Token: 0x04000738 RID: 1848
		MessageTampered = 1,
		// Token: 0x04000739 RID: 1849
		SignatureExpired = 2,
		// Token: 0x0400073A RID: 1850
		SignerAndSenderDoNotMatch = 4,
		// Token: 0x0400073B RID: 1851
		CertificateRevoked = 8,
		// Token: 0x0400073C RID: 1852
		Untrusted = 16,
		// Token: 0x0400073D RID: 1853
		All = 31
	}
}

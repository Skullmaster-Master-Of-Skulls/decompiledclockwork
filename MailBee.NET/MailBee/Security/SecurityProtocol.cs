using System;

namespace MailBee.Security
{
	// Token: 0x02000123 RID: 291
	[Flags]
	public enum SecurityProtocol
	{
		// Token: 0x04000759 RID: 1881
		Auto = 0,
		// Token: 0x0400075A RID: 1882
		Ssl2 = 12,
		// Token: 0x0400075B RID: 1883
		Ssl3 = 48,
		// Token: 0x0400075C RID: 1884
		Tls1 = 192,
		// Token: 0x0400075D RID: 1885
		Tls11 = 768,
		// Token: 0x0400075E RID: 1886
		Tls12 = 3072,
		// Token: 0x0400075F RID: 1887
		TlsAuto = 4032
	}
}

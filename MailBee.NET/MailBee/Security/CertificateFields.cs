using System;

namespace MailBee.Security
{
	// Token: 0x0200011E RID: 286
	[Flags]
	public enum CertificateFields
	{
		// Token: 0x0400072E RID: 1838
		Name = 1,
		// Token: 0x0400072F RID: 1839
		Subject = 2,
		// Token: 0x04000730 RID: 1840
		Issuer = 4,
		// Token: 0x04000731 RID: 1841
		SerialNumber = 8,
		// Token: 0x04000732 RID: 1842
		PublicKey = 16,
		// Token: 0x04000733 RID: 1843
		EmailAddress = 32,
		// Token: 0x04000734 RID: 1844
		Thumbprint = 64,
		// Token: 0x04000735 RID: 1845
		All = 127
	}
}

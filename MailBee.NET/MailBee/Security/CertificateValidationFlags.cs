using System;

namespace MailBee.Security
{
	// Token: 0x02000121 RID: 289
	[Flags]
	public enum CertificateValidationFlags
	{
		// Token: 0x04000743 RID: 1859
		None = 0,
		// Token: 0x04000744 RID: 1860
		IsNotTimeValid = 1,
		// Token: 0x04000745 RID: 1861
		IsNotTimeNested = 2,
		// Token: 0x04000746 RID: 1862
		IsRevoked = 4,
		// Token: 0x04000747 RID: 1863
		IsNotSignatureValid = 8,
		// Token: 0x04000748 RID: 1864
		IsNotValidForUsage = 16,
		// Token: 0x04000749 RID: 1865
		IsUntrustedRoot = 32,
		// Token: 0x0400074A RID: 1866
		RevocationStatusUnknown = 64,
		// Token: 0x0400074B RID: 1867
		IsCyclic = 128,
		// Token: 0x0400074C RID: 1868
		IsPartialChain = 65536,
		// Token: 0x0400074D RID: 1869
		IsNotTimeValidCtl = 131072,
		// Token: 0x0400074E RID: 1870
		IsNotSignatureValidCtl = 262144,
		// Token: 0x0400074F RID: 1871
		IsNotValidForUsageCtl = 524288,
		// Token: 0x04000750 RID: 1872
		NameMismatch = 16777216,
		// Token: 0x04000751 RID: 1873
		All = 17760447
	}
}

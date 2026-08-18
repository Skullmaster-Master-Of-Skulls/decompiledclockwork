using System;

namespace System.Net.Security
{
	// Token: 0x02000593 RID: 1427
	[Flags]
	public enum SslPolicyErrors
	{
		// Token: 0x04002A01 RID: 10753
		None = 0,
		// Token: 0x04002A02 RID: 10754
		RemoteCertificateNotAvailable = 1,
		// Token: 0x04002A03 RID: 10755
		RemoteCertificateNameMismatch = 2,
		// Token: 0x04002A04 RID: 10756
		RemoteCertificateChainErrors = 4
	}
}

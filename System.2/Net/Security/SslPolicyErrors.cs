using System;

namespace System.Net.Security
{
	// Token: 0x02000358 RID: 856
	[Flags]
	[__DynamicallyInvokable]
	public enum SslPolicyErrors
	{
		// Token: 0x04001D08 RID: 7432
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04001D09 RID: 7433
		[__DynamicallyInvokable]
		RemoteCertificateNotAvailable = 1,
		// Token: 0x04001D0A RID: 7434
		[__DynamicallyInvokable]
		RemoteCertificateNameMismatch = 2,
		// Token: 0x04001D0B RID: 7435
		[__DynamicallyInvokable]
		RemoteCertificateChainErrors = 4
	}
}

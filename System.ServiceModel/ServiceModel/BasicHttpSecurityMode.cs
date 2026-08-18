using System;

namespace System.ServiceModel
{
	// Token: 0x02000132 RID: 306
	[__DynamicallyInvokable]
	public enum BasicHttpSecurityMode
	{
		// Token: 0x04000B16 RID: 2838
		[__DynamicallyInvokable]
		None,
		// Token: 0x04000B17 RID: 2839
		[__DynamicallyInvokable]
		Transport,
		// Token: 0x04000B18 RID: 2840
		[__DynamicallyInvokable]
		Message,
		// Token: 0x04000B19 RID: 2841
		[__DynamicallyInvokable]
		TransportWithMessageCredential,
		// Token: 0x04000B1A RID: 2842
		[__DynamicallyInvokable]
		TransportCredentialOnly
	}
}

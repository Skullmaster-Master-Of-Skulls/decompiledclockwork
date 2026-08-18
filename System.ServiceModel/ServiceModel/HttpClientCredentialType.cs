using System;

namespace System.ServiceModel
{
	// Token: 0x02000135 RID: 309
	[__DynamicallyInvokable]
	public enum HttpClientCredentialType
	{
		// Token: 0x04000B29 RID: 2857
		[__DynamicallyInvokable]
		None,
		// Token: 0x04000B2A RID: 2858
		[__DynamicallyInvokable]
		Basic,
		// Token: 0x04000B2B RID: 2859
		[__DynamicallyInvokable]
		Digest,
		// Token: 0x04000B2C RID: 2860
		[__DynamicallyInvokable]
		Ntlm,
		// Token: 0x04000B2D RID: 2861
		[__DynamicallyInvokable]
		Windows,
		// Token: 0x04000B2E RID: 2862
		[__DynamicallyInvokable]
		Certificate,
		// Token: 0x04000B2F RID: 2863
		[__DynamicallyInvokable]
		InheritedFromHost
	}
}

using System;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Security
{
	// Token: 0x02000012 RID: 18
	[TypeForwardedFrom("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public enum X509CertificateValidationMode
	{
		// Token: 0x04000078 RID: 120
		None,
		// Token: 0x04000079 RID: 121
		PeerTrust,
		// Token: 0x0400007A RID: 122
		ChainTrust,
		// Token: 0x0400007B RID: 123
		PeerOrChainTrust,
		// Token: 0x0400007C RID: 124
		Custom
	}
}

using System;

namespace System.Deployment.Internal.CodeSigning
{
	// Token: 0x0200000E RID: 14
	[Flags]
	internal enum CmiManifestVerifyFlags
	{
		// Token: 0x040000C3 RID: 195
		None = 0,
		// Token: 0x040000C4 RID: 196
		RevocationNoCheck = 1,
		// Token: 0x040000C5 RID: 197
		RevocationCheckEndCertOnly = 2,
		// Token: 0x040000C6 RID: 198
		RevocationCheckEntireChain = 4,
		// Token: 0x040000C7 RID: 199
		UrlCacheOnlyRetrieval = 8,
		// Token: 0x040000C8 RID: 200
		LifetimeSigning = 16,
		// Token: 0x040000C9 RID: 201
		TrustMicrosoftRootOnly = 32,
		// Token: 0x040000CA RID: 202
		StrongNameOnly = 65536
	}
}

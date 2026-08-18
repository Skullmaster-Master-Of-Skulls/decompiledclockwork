using System;

namespace System.ServiceModel
{
	// Token: 0x02000167 RID: 359
	[Flags]
	internal enum UnifiedSecurityMode
	{
		// Token: 0x04000BCF RID: 3023
		None = 1,
		// Token: 0x04000BD0 RID: 3024
		Transport = 4,
		// Token: 0x04000BD1 RID: 3025
		Message = 8,
		// Token: 0x04000BD2 RID: 3026
		Both = 16,
		// Token: 0x04000BD3 RID: 3027
		TransportWithMessageCredential = 32,
		// Token: 0x04000BD4 RID: 3028
		TransportCredentialOnly = 64
	}
}

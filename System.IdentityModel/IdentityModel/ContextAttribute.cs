using System;

namespace System.IdentityModel
{
	// Token: 0x0200008C RID: 140
	internal enum ContextAttribute
	{
		// Token: 0x04000413 RID: 1043
		Sizes,
		// Token: 0x04000414 RID: 1044
		Names,
		// Token: 0x04000415 RID: 1045
		Lifespan,
		// Token: 0x04000416 RID: 1046
		DceInfo,
		// Token: 0x04000417 RID: 1047
		StreamSizes,
		// Token: 0x04000418 RID: 1048
		Authority = 6,
		// Token: 0x04000419 RID: 1049
		SessionKey = 9,
		// Token: 0x0400041A RID: 1050
		PackageInfo,
		// Token: 0x0400041B RID: 1051
		NegotiationInfo = 12,
		// Token: 0x0400041C RID: 1052
		Flags = 14,
		// Token: 0x0400041D RID: 1053
		SpecifiedTarget = 27,
		// Token: 0x0400041E RID: 1054
		RemoteCertificate = 83,
		// Token: 0x0400041F RID: 1055
		LocalCertificate,
		// Token: 0x04000420 RID: 1056
		RootStore,
		// Token: 0x04000421 RID: 1057
		IssuerListInfoEx = 89,
		// Token: 0x04000422 RID: 1058
		ConnectionInfo,
		// Token: 0x04000423 RID: 1059
		EapKey
	}
}

using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000473 RID: 1139
	[Flags]
	public enum X509VerificationFlags
	{
		// Token: 0x04002617 RID: 9751
		NoFlag = 0,
		// Token: 0x04002618 RID: 9752
		IgnoreNotTimeValid = 1,
		// Token: 0x04002619 RID: 9753
		IgnoreCtlNotTimeValid = 2,
		// Token: 0x0400261A RID: 9754
		IgnoreNotTimeNested = 4,
		// Token: 0x0400261B RID: 9755
		IgnoreInvalidBasicConstraints = 8,
		// Token: 0x0400261C RID: 9756
		AllowUnknownCertificateAuthority = 16,
		// Token: 0x0400261D RID: 9757
		IgnoreWrongUsage = 32,
		// Token: 0x0400261E RID: 9758
		IgnoreInvalidName = 64,
		// Token: 0x0400261F RID: 9759
		IgnoreInvalidPolicy = 128,
		// Token: 0x04002620 RID: 9760
		IgnoreEndRevocationUnknown = 256,
		// Token: 0x04002621 RID: 9761
		IgnoreCtlSignerRevocationUnknown = 512,
		// Token: 0x04002622 RID: 9762
		IgnoreCertificateAuthorityRevocationUnknown = 1024,
		// Token: 0x04002623 RID: 9763
		IgnoreRootRevocationUnknown = 2048,
		// Token: 0x04002624 RID: 9764
		AllFlags = 4095
	}
}

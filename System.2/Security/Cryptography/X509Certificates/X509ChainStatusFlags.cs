using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x0200046B RID: 1131
	[Flags]
	public enum X509ChainStatusFlags
	{
		// Token: 0x040025E4 RID: 9700
		NoError = 0,
		// Token: 0x040025E5 RID: 9701
		NotTimeValid = 1,
		// Token: 0x040025E6 RID: 9702
		NotTimeNested = 2,
		// Token: 0x040025E7 RID: 9703
		Revoked = 4,
		// Token: 0x040025E8 RID: 9704
		NotSignatureValid = 8,
		// Token: 0x040025E9 RID: 9705
		NotValidForUsage = 16,
		// Token: 0x040025EA RID: 9706
		UntrustedRoot = 32,
		// Token: 0x040025EB RID: 9707
		RevocationStatusUnknown = 64,
		// Token: 0x040025EC RID: 9708
		Cyclic = 128,
		// Token: 0x040025ED RID: 9709
		InvalidExtension = 256,
		// Token: 0x040025EE RID: 9710
		InvalidPolicyConstraints = 512,
		// Token: 0x040025EF RID: 9711
		InvalidBasicConstraints = 1024,
		// Token: 0x040025F0 RID: 9712
		InvalidNameConstraints = 2048,
		// Token: 0x040025F1 RID: 9713
		HasNotSupportedNameConstraint = 4096,
		// Token: 0x040025F2 RID: 9714
		HasNotDefinedNameConstraint = 8192,
		// Token: 0x040025F3 RID: 9715
		HasNotPermittedNameConstraint = 16384,
		// Token: 0x040025F4 RID: 9716
		HasExcludedNameConstraint = 32768,
		// Token: 0x040025F5 RID: 9717
		PartialChain = 65536,
		// Token: 0x040025F6 RID: 9718
		CtlNotTimeValid = 131072,
		// Token: 0x040025F7 RID: 9719
		CtlNotSignatureValid = 262144,
		// Token: 0x040025F8 RID: 9720
		CtlNotValidForUsage = 524288,
		// Token: 0x040025F9 RID: 9721
		OfflineRevocation = 16777216,
		// Token: 0x040025FA RID: 9722
		NoIssuanceChainPolicy = 33554432,
		// Token: 0x040025FB RID: 9723
		ExplicitDistrust = 67108864,
		// Token: 0x040025FC RID: 9724
		HasNotSupportedCriticalExtension = 134217728,
		// Token: 0x040025FD RID: 9725
		HasWeakSignature = 1048576
	}
}

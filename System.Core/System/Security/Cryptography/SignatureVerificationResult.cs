using System;

namespace System.Security.Cryptography
{
	// Token: 0x0200011A RID: 282
	public enum SignatureVerificationResult
	{
		// Token: 0x040006C4 RID: 1732
		Valid,
		// Token: 0x040006C5 RID: 1733
		AssemblyIdentityMismatch,
		// Token: 0x040006C6 RID: 1734
		ContainingSignatureInvalid,
		// Token: 0x040006C7 RID: 1735
		PublicKeyTokenMismatch,
		// Token: 0x040006C8 RID: 1736
		PublisherMismatch,
		// Token: 0x040006C9 RID: 1737
		SystemError = -2146869247,
		// Token: 0x040006CA RID: 1738
		InvalidSignerCertificate,
		// Token: 0x040006CB RID: 1739
		InvalidCountersignature,
		// Token: 0x040006CC RID: 1740
		InvalidCertificateSignature,
		// Token: 0x040006CD RID: 1741
		InvalidTimestamp,
		// Token: 0x040006CE RID: 1742
		BadDigest = -2146869232,
		// Token: 0x040006CF RID: 1743
		BasicConstraintsNotObserved = -2146869223,
		// Token: 0x040006D0 RID: 1744
		UnknownTrustProvider = -2146762751,
		// Token: 0x040006D1 RID: 1745
		UnknownVerificationAction,
		// Token: 0x040006D2 RID: 1746
		BadSignatureFormat,
		// Token: 0x040006D3 RID: 1747
		CertificateNotExplicitlyTrusted,
		// Token: 0x040006D4 RID: 1748
		MissingSignature = -2146762496,
		// Token: 0x040006D5 RID: 1749
		CertificateExpired,
		// Token: 0x040006D6 RID: 1750
		InvalidTimePeriodNesting,
		// Token: 0x040006D7 RID: 1751
		InvalidCertificateRole,
		// Token: 0x040006D8 RID: 1752
		PathLengthConstraintViolated,
		// Token: 0x040006D9 RID: 1753
		UnknownCriticalExtension,
		// Token: 0x040006DA RID: 1754
		CertificateUsageNotAllowed,
		// Token: 0x040006DB RID: 1755
		IssuerChainingError,
		// Token: 0x040006DC RID: 1756
		CertificateMalformed,
		// Token: 0x040006DD RID: 1757
		UntrustedRootCertificate,
		// Token: 0x040006DE RID: 1758
		CouldNotBuildChain,
		// Token: 0x040006DF RID: 1759
		GenericTrustFailure,
		// Token: 0x040006E0 RID: 1760
		CertificateRevoked,
		// Token: 0x040006E1 RID: 1761
		UntrustedTestRootCertificate,
		// Token: 0x040006E2 RID: 1762
		RevocationCheckFailure,
		// Token: 0x040006E3 RID: 1763
		InvalidCertificateUsage = -2146762480,
		// Token: 0x040006E4 RID: 1764
		CertificateExplicitlyDistrusted,
		// Token: 0x040006E5 RID: 1765
		UntrustedCertificationAuthority,
		// Token: 0x040006E6 RID: 1766
		InvalidCertificatePolicy,
		// Token: 0x040006E7 RID: 1767
		InvalidCertificateName
	}
}

using System;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200003F RID: 63
	public abstract class EsfAttributes
	{
		// Token: 0x040000BF RID: 191
		public static readonly DerObjectIdentifier SigPolicyId = PkcsObjectIdentifiers.IdAAEtsSigPolicyID;

		// Token: 0x040000C0 RID: 192
		public static readonly DerObjectIdentifier CommitmentType = PkcsObjectIdentifiers.IdAAEtsCommitmentType;

		// Token: 0x040000C1 RID: 193
		public static readonly DerObjectIdentifier SignerLocation = PkcsObjectIdentifiers.IdAAEtsSignerLocation;

		// Token: 0x040000C2 RID: 194
		public static readonly DerObjectIdentifier SignerAttr = PkcsObjectIdentifiers.IdAAEtsSignerAttr;

		// Token: 0x040000C3 RID: 195
		public static readonly DerObjectIdentifier OtherSigCert = PkcsObjectIdentifiers.IdAAEtsOtherSigCert;

		// Token: 0x040000C4 RID: 196
		public static readonly DerObjectIdentifier ContentTimestamp = PkcsObjectIdentifiers.IdAAEtsContentTimestamp;

		// Token: 0x040000C5 RID: 197
		public static readonly DerObjectIdentifier CertificateRefs = PkcsObjectIdentifiers.IdAAEtsCertificateRefs;

		// Token: 0x040000C6 RID: 198
		public static readonly DerObjectIdentifier RevocationRefs = PkcsObjectIdentifiers.IdAAEtsRevocationRefs;

		// Token: 0x040000C7 RID: 199
		public static readonly DerObjectIdentifier CertValues = PkcsObjectIdentifiers.IdAAEtsCertValues;

		// Token: 0x040000C8 RID: 200
		public static readonly DerObjectIdentifier RevocationValues = PkcsObjectIdentifiers.IdAAEtsRevocationValues;

		// Token: 0x040000C9 RID: 201
		public static readonly DerObjectIdentifier EscTimeStamp = PkcsObjectIdentifiers.IdAAEtsEscTimeStamp;

		// Token: 0x040000CA RID: 202
		public static readonly DerObjectIdentifier CertCrlTimestamp = PkcsObjectIdentifiers.IdAAEtsCertCrlTimestamp;

		// Token: 0x040000CB RID: 203
		public static readonly DerObjectIdentifier ArchiveTimestamp = PkcsObjectIdentifiers.IdAAEtsArchiveTimestamp;
	}
}

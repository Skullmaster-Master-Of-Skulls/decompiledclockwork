using System;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200062D RID: 1581
	public abstract class CommitmentTypeIdentifier
	{
		// Token: 0x040023D3 RID: 9171
		public static readonly DerObjectIdentifier ProofOfOrigin = PkcsObjectIdentifiers.IdCtiEtsProofOfOrigin;

		// Token: 0x040023D4 RID: 9172
		public static readonly DerObjectIdentifier ProofOfReceipt = PkcsObjectIdentifiers.IdCtiEtsProofOfReceipt;

		// Token: 0x040023D5 RID: 9173
		public static readonly DerObjectIdentifier ProofOfDelivery = PkcsObjectIdentifiers.IdCtiEtsProofOfDelivery;

		// Token: 0x040023D6 RID: 9174
		public static readonly DerObjectIdentifier ProofOfSender = PkcsObjectIdentifiers.IdCtiEtsProofOfSender;

		// Token: 0x040023D7 RID: 9175
		public static readonly DerObjectIdentifier ProofOfApproval = PkcsObjectIdentifiers.IdCtiEtsProofOfApproval;

		// Token: 0x040023D8 RID: 9176
		public static readonly DerObjectIdentifier ProofOfCreation = PkcsObjectIdentifiers.IdCtiEtsProofOfCreation;
	}
}

using System;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x02000487 RID: 1159
	public abstract class EtsiQCObjectIdentifiers
	{
		// Token: 0x04001B11 RID: 6929
		public static readonly DerObjectIdentifier IdEtsiQcs = new DerObjectIdentifier("0.4.0.1862.1");

		// Token: 0x04001B12 RID: 6930
		public static readonly DerObjectIdentifier IdEtsiQcsQcCompliance = new DerObjectIdentifier(EtsiQCObjectIdentifiers.IdEtsiQcs + ".1");

		// Token: 0x04001B13 RID: 6931
		public static readonly DerObjectIdentifier IdEtsiQcsLimitValue = new DerObjectIdentifier(EtsiQCObjectIdentifiers.IdEtsiQcs + ".2");

		// Token: 0x04001B14 RID: 6932
		public static readonly DerObjectIdentifier IdEtsiQcsRetentionPeriod = new DerObjectIdentifier(EtsiQCObjectIdentifiers.IdEtsiQcs + ".3");

		// Token: 0x04001B15 RID: 6933
		public static readonly DerObjectIdentifier IdEtsiQcsQcSscd = new DerObjectIdentifier(EtsiQCObjectIdentifiers.IdEtsiQcs + ".4");
	}
}

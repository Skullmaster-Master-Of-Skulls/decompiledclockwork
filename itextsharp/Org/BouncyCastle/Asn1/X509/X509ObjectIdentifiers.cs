using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200055C RID: 1372
	public abstract class X509ObjectIdentifiers
	{
		// Token: 0x04002088 RID: 8328
		internal const string ID = "2.5.4";

		// Token: 0x04002089 RID: 8329
		public static readonly DerObjectIdentifier CommonName = new DerObjectIdentifier("2.5.4.3");

		// Token: 0x0400208A RID: 8330
		public static readonly DerObjectIdentifier CountryName = new DerObjectIdentifier("2.5.4.6");

		// Token: 0x0400208B RID: 8331
		public static readonly DerObjectIdentifier LocalityName = new DerObjectIdentifier("2.5.4.7");

		// Token: 0x0400208C RID: 8332
		public static readonly DerObjectIdentifier StateOrProvinceName = new DerObjectIdentifier("2.5.4.8");

		// Token: 0x0400208D RID: 8333
		public static readonly DerObjectIdentifier Organization = new DerObjectIdentifier("2.5.4.10");

		// Token: 0x0400208E RID: 8334
		public static readonly DerObjectIdentifier OrganizationalUnitName = new DerObjectIdentifier("2.5.4.11");

		// Token: 0x0400208F RID: 8335
		public static readonly DerObjectIdentifier id_at_telephoneNumber = new DerObjectIdentifier("2.5.4.20");

		// Token: 0x04002090 RID: 8336
		public static readonly DerObjectIdentifier id_at_name = new DerObjectIdentifier("2.5.4.41");

		// Token: 0x04002091 RID: 8337
		public static readonly DerObjectIdentifier IdSha1 = new DerObjectIdentifier("1.3.14.3.2.26");

		// Token: 0x04002092 RID: 8338
		public static readonly DerObjectIdentifier RipeMD160 = new DerObjectIdentifier("1.3.36.3.2.1");

		// Token: 0x04002093 RID: 8339
		public static readonly DerObjectIdentifier RipeMD160WithRsaEncryption = new DerObjectIdentifier("1.3.36.3.3.1.2");

		// Token: 0x04002094 RID: 8340
		public static readonly DerObjectIdentifier IdEARsa = new DerObjectIdentifier("2.5.8.1.1");

		// Token: 0x04002095 RID: 8341
		public static readonly DerObjectIdentifier IdPkix = new DerObjectIdentifier("1.3.6.1.5.5.7");

		// Token: 0x04002096 RID: 8342
		public static readonly DerObjectIdentifier IdPE = new DerObjectIdentifier(X509ObjectIdentifiers.IdPkix + ".1");

		// Token: 0x04002097 RID: 8343
		public static readonly DerObjectIdentifier IdAD = new DerObjectIdentifier(X509ObjectIdentifiers.IdPkix + ".48");

		// Token: 0x04002098 RID: 8344
		public static readonly DerObjectIdentifier IdADCAIssuers = new DerObjectIdentifier(X509ObjectIdentifiers.IdAD + ".2");

		// Token: 0x04002099 RID: 8345
		public static readonly DerObjectIdentifier IdADOcsp = new DerObjectIdentifier(X509ObjectIdentifiers.IdAD + ".1");

		// Token: 0x0400209A RID: 8346
		public static readonly DerObjectIdentifier OcspAccessMethod = X509ObjectIdentifiers.IdADOcsp;

		// Token: 0x0400209B RID: 8347
		public static readonly DerObjectIdentifier CrlAccessMethod = X509ObjectIdentifiers.IdADCAIssuers;
	}
}

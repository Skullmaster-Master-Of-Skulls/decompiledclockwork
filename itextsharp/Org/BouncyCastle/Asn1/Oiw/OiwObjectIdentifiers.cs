using System;

namespace Org.BouncyCastle.Asn1.Oiw
{
	// Token: 0x02000629 RID: 1577
	public abstract class OiwObjectIdentifiers
	{
		// Token: 0x040023C2 RID: 9154
		public static readonly DerObjectIdentifier MD4WithRsa = new DerObjectIdentifier("1.3.14.3.2.2");

		// Token: 0x040023C3 RID: 9155
		public static readonly DerObjectIdentifier MD5WithRsa = new DerObjectIdentifier("1.3.14.3.2.3");

		// Token: 0x040023C4 RID: 9156
		public static readonly DerObjectIdentifier MD4WithRsaEncryption = new DerObjectIdentifier("1.3.14.3.2.4");

		// Token: 0x040023C5 RID: 9157
		public static readonly DerObjectIdentifier DesEcb = new DerObjectIdentifier("1.3.14.3.2.6");

		// Token: 0x040023C6 RID: 9158
		public static readonly DerObjectIdentifier DesCbc = new DerObjectIdentifier("1.3.14.3.2.7");

		// Token: 0x040023C7 RID: 9159
		public static readonly DerObjectIdentifier DesOfb = new DerObjectIdentifier("1.3.14.3.2.8");

		// Token: 0x040023C8 RID: 9160
		public static readonly DerObjectIdentifier DesCfb = new DerObjectIdentifier("1.3.14.3.2.9");

		// Token: 0x040023C9 RID: 9161
		public static readonly DerObjectIdentifier DesEde = new DerObjectIdentifier("1.3.14.3.2.17");

		// Token: 0x040023CA RID: 9162
		public static readonly DerObjectIdentifier IdSha1 = new DerObjectIdentifier("1.3.14.3.2.26");

		// Token: 0x040023CB RID: 9163
		public static readonly DerObjectIdentifier DsaWithSha1 = new DerObjectIdentifier("1.3.14.3.2.27");

		// Token: 0x040023CC RID: 9164
		public static readonly DerObjectIdentifier Sha1WithRsa = new DerObjectIdentifier("1.3.14.3.2.29");

		// Token: 0x040023CD RID: 9165
		public static readonly DerObjectIdentifier ElGamalAlgorithm = new DerObjectIdentifier("1.3.14.7.2.1.1");
	}
}

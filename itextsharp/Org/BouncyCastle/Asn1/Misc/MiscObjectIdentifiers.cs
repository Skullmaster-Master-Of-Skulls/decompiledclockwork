using System;

namespace Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x02000146 RID: 326
	public abstract class MiscObjectIdentifiers
	{
		// Token: 0x0400094B RID: 2379
		internal const string Verisign = "2.16.840.1.113733.1";

		// Token: 0x0400094C RID: 2380
		public static readonly DerObjectIdentifier Netscape = new DerObjectIdentifier("2.16.840.1.113730.1");

		// Token: 0x0400094D RID: 2381
		public static readonly DerObjectIdentifier NetscapeCertType = new DerObjectIdentifier(MiscObjectIdentifiers.Netscape + ".1");

		// Token: 0x0400094E RID: 2382
		public static readonly DerObjectIdentifier NetscapeBaseUrl = new DerObjectIdentifier(MiscObjectIdentifiers.Netscape + ".2");

		// Token: 0x0400094F RID: 2383
		public static readonly DerObjectIdentifier NetscapeRevocationUrl = new DerObjectIdentifier(MiscObjectIdentifiers.Netscape + ".3");

		// Token: 0x04000950 RID: 2384
		public static readonly DerObjectIdentifier NetscapeCARevocationUrl = new DerObjectIdentifier(MiscObjectIdentifiers.Netscape + ".4");

		// Token: 0x04000951 RID: 2385
		public static readonly DerObjectIdentifier NetscapeRenewalUrl = new DerObjectIdentifier(MiscObjectIdentifiers.Netscape + ".7");

		// Token: 0x04000952 RID: 2386
		public static readonly DerObjectIdentifier NetscapeCAPolicyUrl = new DerObjectIdentifier(MiscObjectIdentifiers.Netscape + ".8");

		// Token: 0x04000953 RID: 2387
		public static readonly DerObjectIdentifier NetscapeSslServerName = new DerObjectIdentifier(MiscObjectIdentifiers.Netscape + ".12");

		// Token: 0x04000954 RID: 2388
		public static readonly DerObjectIdentifier NetscapeCertComment = new DerObjectIdentifier(MiscObjectIdentifiers.Netscape + ".13");

		// Token: 0x04000955 RID: 2389
		public static readonly DerObjectIdentifier VerisignCzagExtension = new DerObjectIdentifier("2.16.840.1.113733.1.6.3");

		// Token: 0x04000956 RID: 2390
		public static readonly DerObjectIdentifier VerisignDnbDunsNumber = new DerObjectIdentifier("2.16.840.1.113733.1.6.15");

		// Token: 0x04000957 RID: 2391
		public static readonly string Novell = "2.16.840.1.113719";

		// Token: 0x04000958 RID: 2392
		public static readonly DerObjectIdentifier NovellSecurityAttribs = new DerObjectIdentifier(MiscObjectIdentifiers.Novell + ".1.9.4.1");

		// Token: 0x04000959 RID: 2393
		public static readonly string Entrust = "1.2.840.113533.7";

		// Token: 0x0400095A RID: 2394
		public static readonly DerObjectIdentifier EntrustVersionExtension = new DerObjectIdentifier(MiscObjectIdentifiers.Entrust + ".65.0");
	}
}

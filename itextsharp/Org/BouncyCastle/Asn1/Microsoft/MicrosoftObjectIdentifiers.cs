using System;

namespace Org.BouncyCastle.Asn1.Microsoft
{
	// Token: 0x02000387 RID: 903
	public abstract class MicrosoftObjectIdentifiers
	{
		// Token: 0x040015D0 RID: 5584
		public static readonly DerObjectIdentifier Microsoft = new DerObjectIdentifier("1.3.6.1.4.1.311");

		// Token: 0x040015D1 RID: 5585
		public static readonly DerObjectIdentifier MicrosoftCertTemplateV1 = new DerObjectIdentifier(MicrosoftObjectIdentifiers.Microsoft + ".20.2");

		// Token: 0x040015D2 RID: 5586
		public static readonly DerObjectIdentifier MicrosoftCAVersion = new DerObjectIdentifier(MicrosoftObjectIdentifiers.Microsoft + ".21.1");

		// Token: 0x040015D3 RID: 5587
		public static readonly DerObjectIdentifier MicrosoftPrevCACertHash = new DerObjectIdentifier(MicrosoftObjectIdentifiers.Microsoft + ".21.2");

		// Token: 0x040015D4 RID: 5588
		public static readonly DerObjectIdentifier MicrosoftCertTemplateV2 = new DerObjectIdentifier(MicrosoftObjectIdentifiers.Microsoft + ".21.7");

		// Token: 0x040015D5 RID: 5589
		public static readonly DerObjectIdentifier MicrosoftAppPolicies = new DerObjectIdentifier(MicrosoftObjectIdentifiers.Microsoft + ".21.10");
	}
}

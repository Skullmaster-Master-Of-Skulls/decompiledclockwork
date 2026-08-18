using System;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020001B4 RID: 436
	public abstract class OcspObjectIdentifiers
	{
		// Token: 0x04000C1C RID: 3100
		internal const string PkixOcspId = "1.3.6.1.5.5.7.48.1";

		// Token: 0x04000C1D RID: 3101
		public static readonly DerObjectIdentifier PkixOcsp = new DerObjectIdentifier("1.3.6.1.5.5.7.48.1");

		// Token: 0x04000C1E RID: 3102
		public static readonly DerObjectIdentifier PkixOcspBasic = new DerObjectIdentifier("1.3.6.1.5.5.7.48.1.1");

		// Token: 0x04000C1F RID: 3103
		public static readonly DerObjectIdentifier PkixOcspNonce = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".2");

		// Token: 0x04000C20 RID: 3104
		public static readonly DerObjectIdentifier PkixOcspCrl = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".3");

		// Token: 0x04000C21 RID: 3105
		public static readonly DerObjectIdentifier PkixOcspResponse = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".4");

		// Token: 0x04000C22 RID: 3106
		public static readonly DerObjectIdentifier PkixOcspNocheck = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".5");

		// Token: 0x04000C23 RID: 3107
		public static readonly DerObjectIdentifier PkixOcspArchiveCutoff = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".6");

		// Token: 0x04000C24 RID: 3108
		public static readonly DerObjectIdentifier PkixOcspServiceLocator = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".7");
	}
}

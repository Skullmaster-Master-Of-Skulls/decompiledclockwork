using System;
using Org.BouncyCastle.Asn1.X9;

namespace Org.BouncyCastle.Asn1.Sec
{
	// Token: 0x02000449 RID: 1097
	public abstract class SecObjectIdentifiers
	{
		// Token: 0x040019E9 RID: 6633
		public static readonly DerObjectIdentifier EllipticCurve = new DerObjectIdentifier("1.3.132.0");

		// Token: 0x040019EA RID: 6634
		public static readonly DerObjectIdentifier SecT163k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".1");

		// Token: 0x040019EB RID: 6635
		public static readonly DerObjectIdentifier SecT163r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".2");

		// Token: 0x040019EC RID: 6636
		public static readonly DerObjectIdentifier SecT239k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".3");

		// Token: 0x040019ED RID: 6637
		public static readonly DerObjectIdentifier SecT113r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".4");

		// Token: 0x040019EE RID: 6638
		public static readonly DerObjectIdentifier SecT113r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".5");

		// Token: 0x040019EF RID: 6639
		public static readonly DerObjectIdentifier SecP112r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".6");

		// Token: 0x040019F0 RID: 6640
		public static readonly DerObjectIdentifier SecP112r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".7");

		// Token: 0x040019F1 RID: 6641
		public static readonly DerObjectIdentifier SecP160r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".8");

		// Token: 0x040019F2 RID: 6642
		public static readonly DerObjectIdentifier SecP160k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".9");

		// Token: 0x040019F3 RID: 6643
		public static readonly DerObjectIdentifier SecP256k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".10");

		// Token: 0x040019F4 RID: 6644
		public static readonly DerObjectIdentifier SecT163r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".15");

		// Token: 0x040019F5 RID: 6645
		public static readonly DerObjectIdentifier SecT283k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".16");

		// Token: 0x040019F6 RID: 6646
		public static readonly DerObjectIdentifier SecT283r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".17");

		// Token: 0x040019F7 RID: 6647
		public static readonly DerObjectIdentifier SecT131r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".22");

		// Token: 0x040019F8 RID: 6648
		public static readonly DerObjectIdentifier SecT131r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".23");

		// Token: 0x040019F9 RID: 6649
		public static readonly DerObjectIdentifier SecT193r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".24");

		// Token: 0x040019FA RID: 6650
		public static readonly DerObjectIdentifier SecT193r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".25");

		// Token: 0x040019FB RID: 6651
		public static readonly DerObjectIdentifier SecT233k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".26");

		// Token: 0x040019FC RID: 6652
		public static readonly DerObjectIdentifier SecT233r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".27");

		// Token: 0x040019FD RID: 6653
		public static readonly DerObjectIdentifier SecP128r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".28");

		// Token: 0x040019FE RID: 6654
		public static readonly DerObjectIdentifier SecP128r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".29");

		// Token: 0x040019FF RID: 6655
		public static readonly DerObjectIdentifier SecP160r2 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".30");

		// Token: 0x04001A00 RID: 6656
		public static readonly DerObjectIdentifier SecP192k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".31");

		// Token: 0x04001A01 RID: 6657
		public static readonly DerObjectIdentifier SecP224k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".32");

		// Token: 0x04001A02 RID: 6658
		public static readonly DerObjectIdentifier SecP224r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".33");

		// Token: 0x04001A03 RID: 6659
		public static readonly DerObjectIdentifier SecP384r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".34");

		// Token: 0x04001A04 RID: 6660
		public static readonly DerObjectIdentifier SecP521r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".35");

		// Token: 0x04001A05 RID: 6661
		public static readonly DerObjectIdentifier SecT409k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".36");

		// Token: 0x04001A06 RID: 6662
		public static readonly DerObjectIdentifier SecT409r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".37");

		// Token: 0x04001A07 RID: 6663
		public static readonly DerObjectIdentifier SecT571k1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".38");

		// Token: 0x04001A08 RID: 6664
		public static readonly DerObjectIdentifier SecT571r1 = new DerObjectIdentifier(SecObjectIdentifiers.EllipticCurve + ".39");

		// Token: 0x04001A09 RID: 6665
		public static readonly DerObjectIdentifier SecP192r1 = X9ObjectIdentifiers.Prime192v1;

		// Token: 0x04001A0A RID: 6666
		public static readonly DerObjectIdentifier SecP256r1 = X9ObjectIdentifiers.Prime256v1;
	}
}

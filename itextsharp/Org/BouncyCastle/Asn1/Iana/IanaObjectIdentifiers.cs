using System;

namespace Org.BouncyCastle.Asn1.Iana
{
	// Token: 0x020004D4 RID: 1236
	public abstract class IanaObjectIdentifiers
	{
		// Token: 0x04001D53 RID: 7507
		public static readonly DerObjectIdentifier IsakmpOakley = new DerObjectIdentifier("1.3.6.1.5.5.8.1");

		// Token: 0x04001D54 RID: 7508
		public static readonly DerObjectIdentifier HmacMD5 = new DerObjectIdentifier(IanaObjectIdentifiers.IsakmpOakley + ".1");

		// Token: 0x04001D55 RID: 7509
		public static readonly DerObjectIdentifier HmacSha1 = new DerObjectIdentifier(IanaObjectIdentifiers.IsakmpOakley + ".2");

		// Token: 0x04001D56 RID: 7510
		public static readonly DerObjectIdentifier HmacTiger = new DerObjectIdentifier(IanaObjectIdentifiers.IsakmpOakley + ".3");

		// Token: 0x04001D57 RID: 7511
		public static readonly DerObjectIdentifier HmacRipeMD160 = new DerObjectIdentifier(IanaObjectIdentifiers.IsakmpOakley + ".4");
	}
}

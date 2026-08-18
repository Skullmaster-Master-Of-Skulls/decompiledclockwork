using System;

namespace Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x02000267 RID: 615
	public abstract class CryptoProObjectIdentifiers
	{
		// Token: 0x04000FD6 RID: 4054
		public const string GostID = "1.2.643.2.2";

		// Token: 0x04000FD7 RID: 4055
		public static readonly DerObjectIdentifier GostR3411 = new DerObjectIdentifier("1.2.643.2.2.9");

		// Token: 0x04000FD8 RID: 4056
		public static readonly DerObjectIdentifier GostR28147Cbc = new DerObjectIdentifier("1.2.643.2.2.21");

		// Token: 0x04000FD9 RID: 4057
		public static readonly DerObjectIdentifier GostR3410x94 = new DerObjectIdentifier("1.2.643.2.2.20");

		// Token: 0x04000FDA RID: 4058
		public static readonly DerObjectIdentifier GostR3410x2001 = new DerObjectIdentifier("1.2.643.2.2.19");

		// Token: 0x04000FDB RID: 4059
		public static readonly DerObjectIdentifier GostR3411x94WithGostR3410x94 = new DerObjectIdentifier("1.2.643.2.2.4");

		// Token: 0x04000FDC RID: 4060
		public static readonly DerObjectIdentifier GostR3411x94WithGostR3410x2001 = new DerObjectIdentifier("1.2.643.2.2.3");

		// Token: 0x04000FDD RID: 4061
		public static readonly DerObjectIdentifier GostR3411x94CryptoProParamSet = new DerObjectIdentifier("1.2.643.2.2.30.1");

		// Token: 0x04000FDE RID: 4062
		public static readonly DerObjectIdentifier GostR3410x94CryptoProA = new DerObjectIdentifier("1.2.643.2.2.32.2");

		// Token: 0x04000FDF RID: 4063
		public static readonly DerObjectIdentifier GostR3410x94CryptoProB = new DerObjectIdentifier("1.2.643.2.2.32.3");

		// Token: 0x04000FE0 RID: 4064
		public static readonly DerObjectIdentifier GostR3410x94CryptoProC = new DerObjectIdentifier("1.2.643.2.2.32.4");

		// Token: 0x04000FE1 RID: 4065
		public static readonly DerObjectIdentifier GostR3410x94CryptoProD = new DerObjectIdentifier("1.2.643.2.2.32.5");

		// Token: 0x04000FE2 RID: 4066
		public static readonly DerObjectIdentifier GostR3410x94CryptoProXchA = new DerObjectIdentifier("1.2.643.2.2.33.1");

		// Token: 0x04000FE3 RID: 4067
		public static readonly DerObjectIdentifier GostR3410x94CryptoProXchB = new DerObjectIdentifier("1.2.643.2.2.33.2");

		// Token: 0x04000FE4 RID: 4068
		public static readonly DerObjectIdentifier GostR3410x94CryptoProXchC = new DerObjectIdentifier("1.2.643.2.2.33.3");

		// Token: 0x04000FE5 RID: 4069
		public static readonly DerObjectIdentifier GostR3410x2001CryptoProA = new DerObjectIdentifier("1.2.643.2.2.35.1");

		// Token: 0x04000FE6 RID: 4070
		public static readonly DerObjectIdentifier GostR3410x2001CryptoProB = new DerObjectIdentifier("1.2.643.2.2.35.2");

		// Token: 0x04000FE7 RID: 4071
		public static readonly DerObjectIdentifier GostR3410x2001CryptoProC = new DerObjectIdentifier("1.2.643.2.2.35.3");

		// Token: 0x04000FE8 RID: 4072
		public static readonly DerObjectIdentifier GostR3410x2001CryptoProXchA = new DerObjectIdentifier("1.2.643.2.2.36.0");

		// Token: 0x04000FE9 RID: 4073
		public static readonly DerObjectIdentifier GostR3410x2001CryptoProXchB = new DerObjectIdentifier("1.2.643.2.2.36.1");

		// Token: 0x04000FEA RID: 4074
		public static readonly DerObjectIdentifier GostElSgDH3410Default = new DerObjectIdentifier("1.2.643.2.2.36.0");

		// Token: 0x04000FEB RID: 4075
		public static readonly DerObjectIdentifier GostElSgDH3410x1 = new DerObjectIdentifier("1.2.643.2.2.36.1");
	}
}

using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000121 RID: 289
	public class Gost3410PrivateKeyParameters : Gost3410KeyParameters
	{
		// Token: 0x06000AA8 RID: 2728 RVA: 0x00038020 File Offset: 0x00037020
		public Gost3410PrivateKeyParameters(BigInteger x, Gost3410Parameters parameters) : base(true, parameters)
		{
			if (x.SignValue < 1 || x.BitLength > 256 || x.CompareTo(base.Parameters.Q) >= 0)
			{
				throw new ArgumentException("Invalid x for GOST3410 private key", "x");
			}
			this.x = x;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00038078 File Offset: 0x00037078
		public Gost3410PrivateKeyParameters(BigInteger x, DerObjectIdentifier publicKeyParamSet) : base(true, publicKeyParamSet)
		{
			if (x.SignValue < 1 || x.BitLength > 256 || x.CompareTo(base.Parameters.Q) >= 0)
			{
				throw new ArgumentException("Invalid x for GOST3410 private key", "x");
			}
			this.x = x;
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x000380CE File Offset: 0x000370CE
		public BigInteger X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x04000880 RID: 2176
		private readonly BigInteger x;
	}
}

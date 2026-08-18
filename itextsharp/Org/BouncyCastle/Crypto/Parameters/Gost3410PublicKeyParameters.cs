using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020005A7 RID: 1447
	public class Gost3410PublicKeyParameters : Gost3410KeyParameters
	{
		// Token: 0x060031FB RID: 12795 RVA: 0x00137717 File Offset: 0x00136717
		public Gost3410PublicKeyParameters(BigInteger y, Gost3410Parameters parameters) : base(false, parameters)
		{
			if (y.SignValue < 1 || y.CompareTo(base.Parameters.P) >= 0)
			{
				throw new ArgumentException("Invalid y for GOST3410 public key", "y");
			}
			this.y = y;
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x00137755 File Offset: 0x00136755
		public Gost3410PublicKeyParameters(BigInteger y, DerObjectIdentifier publicKeyParamSet) : base(false, publicKeyParamSet)
		{
			if (y.SignValue < 1 || y.CompareTo(base.Parameters.P) >= 0)
			{
				throw new ArgumentException("Invalid y for GOST3410 public key", "y");
			}
			this.y = y;
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060031FD RID: 12797 RVA: 0x00137793 File Offset: 0x00136793
		public BigInteger Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x04002256 RID: 8790
		private readonly BigInteger y;
	}
}

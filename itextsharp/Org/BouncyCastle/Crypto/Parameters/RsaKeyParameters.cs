using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200011D RID: 285
	public class RsaKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06000A99 RID: 2713 RVA: 0x00037E54 File Offset: 0x00036E54
		public RsaKeyParameters(bool isPrivate, BigInteger modulus, BigInteger exponent) : base(isPrivate)
		{
			if (modulus == null)
			{
				throw new ArgumentNullException("modulus");
			}
			if (exponent == null)
			{
				throw new ArgumentNullException("exponent");
			}
			if (modulus.SignValue <= 0)
			{
				throw new ArgumentException("Not a valid RSA modulus", "modulus");
			}
			if (exponent.SignValue <= 0)
			{
				throw new ArgumentException("Not a valid RSA exponent", "exponent");
			}
			this.modulus = modulus;
			this.exponent = exponent;
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00037EC4 File Offset: 0x00036EC4
		public BigInteger Modulus
		{
			get
			{
				return this.modulus;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x00037ECC File Offset: 0x00036ECC
		public BigInteger Exponent
		{
			get
			{
				return this.exponent;
			}
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00037ED4 File Offset: 0x00036ED4
		public override bool Equals(object obj)
		{
			RsaKeyParameters rsaKeyParameters = obj as RsaKeyParameters;
			return rsaKeyParameters != null && (rsaKeyParameters.IsPrivate == base.IsPrivate && rsaKeyParameters.Modulus.Equals(this.modulus)) && rsaKeyParameters.Exponent.Equals(this.exponent);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00037F24 File Offset: 0x00036F24
		public override int GetHashCode()
		{
			return this.modulus.GetHashCode() ^ this.exponent.GetHashCode() ^ base.IsPrivate.GetHashCode();
		}

		// Token: 0x04000879 RID: 2169
		private readonly BigInteger modulus;

		// Token: 0x0400087A RID: 2170
		private readonly BigInteger exponent;
	}
}

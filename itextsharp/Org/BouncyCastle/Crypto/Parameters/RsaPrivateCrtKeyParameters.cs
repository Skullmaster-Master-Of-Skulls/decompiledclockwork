using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200023D RID: 573
	public class RsaPrivateCrtKeyParameters : RsaKeyParameters
	{
		// Token: 0x0600163D RID: 5693 RVA: 0x0008205C File Offset: 0x0008105C
		public RsaPrivateCrtKeyParameters(BigInteger modulus, BigInteger publicExponent, BigInteger privateExponent, BigInteger p, BigInteger q, BigInteger dP, BigInteger dQ, BigInteger qInv) : base(true, modulus, privateExponent)
		{
			RsaPrivateCrtKeyParameters.ValidateValue(publicExponent, "publicExponent", "exponent");
			RsaPrivateCrtKeyParameters.ValidateValue(p, "p", "P value");
			RsaPrivateCrtKeyParameters.ValidateValue(q, "q", "Q value");
			RsaPrivateCrtKeyParameters.ValidateValue(dP, "dP", "DP value");
			RsaPrivateCrtKeyParameters.ValidateValue(dQ, "dQ", "DQ value");
			RsaPrivateCrtKeyParameters.ValidateValue(qInv, "qInv", "InverseQ value");
			this.e = publicExponent;
			this.p = p;
			this.q = q;
			this.dP = dP;
			this.dQ = dQ;
			this.qInv = qInv;
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x00082106 File Offset: 0x00081106
		public BigInteger PublicExponent
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0008210E File Offset: 0x0008110E
		public BigInteger P
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x00082116 File Offset: 0x00081116
		public BigInteger Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0008211E File Offset: 0x0008111E
		public BigInteger DP
		{
			get
			{
				return this.dP;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x00082126 File Offset: 0x00081126
		public BigInteger DQ
		{
			get
			{
				return this.dQ;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x0008212E File Offset: 0x0008112E
		public BigInteger QInv
		{
			get
			{
				return this.qInv;
			}
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00082138 File Offset: 0x00081138
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = obj as RsaPrivateCrtKeyParameters;
			return rsaPrivateCrtKeyParameters != null && (rsaPrivateCrtKeyParameters.DP.Equals(this.dP) && rsaPrivateCrtKeyParameters.DQ.Equals(this.dQ) && rsaPrivateCrtKeyParameters.Exponent.Equals(base.Exponent) && rsaPrivateCrtKeyParameters.Modulus.Equals(base.Modulus) && rsaPrivateCrtKeyParameters.P.Equals(this.p) && rsaPrivateCrtKeyParameters.Q.Equals(this.q) && rsaPrivateCrtKeyParameters.PublicExponent.Equals(this.e)) && rsaPrivateCrtKeyParameters.QInv.Equals(this.qInv);
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x000821F4 File Offset: 0x000811F4
		public override int GetHashCode()
		{
			return this.DP.GetHashCode() ^ this.DQ.GetHashCode() ^ base.Exponent.GetHashCode() ^ base.Modulus.GetHashCode() ^ this.P.GetHashCode() ^ this.Q.GetHashCode() ^ this.PublicExponent.GetHashCode() ^ this.QInv.GetHashCode();
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00082260 File Offset: 0x00081260
		private static void ValidateValue(BigInteger x, string name, string desc)
		{
			if (x == null)
			{
				throw new ArgumentNullException(name);
			}
			if (x.SignValue <= 0)
			{
				throw new ArgumentException("Not a valid RSA " + desc, name);
			}
		}

		// Token: 0x04000F47 RID: 3911
		private readonly BigInteger e;

		// Token: 0x04000F48 RID: 3912
		private readonly BigInteger p;

		// Token: 0x04000F49 RID: 3913
		private readonly BigInteger q;

		// Token: 0x04000F4A RID: 3914
		private readonly BigInteger dP;

		// Token: 0x04000F4B RID: 3915
		private readonly BigInteger dQ;

		// Token: 0x04000F4C RID: 3916
		private readonly BigInteger qInv;
	}
}

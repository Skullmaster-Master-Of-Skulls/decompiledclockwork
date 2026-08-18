using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200054B RID: 1355
	public class ECPublicKeyParameters : ECKeyParameters
	{
		// Token: 0x06002E98 RID: 11928 RVA: 0x0011FBE0 File Offset: 0x0011EBE0
		public ECPublicKeyParameters(ECPoint q, ECDomainParameters parameters) : this("EC", q, parameters)
		{
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x0011FBEF File Offset: 0x0011EBEF
		[Obsolete("Use version with explicit 'algorithm' parameter")]
		public ECPublicKeyParameters(ECPoint q, DerObjectIdentifier publicKeyParamSet) : base("ECGOST3410", false, publicKeyParamSet)
		{
			if (q == null)
			{
				throw new ArgumentNullException("q");
			}
			this.q = q;
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x0011FC13 File Offset: 0x0011EC13
		public ECPublicKeyParameters(string algorithm, ECPoint q, ECDomainParameters parameters) : base(algorithm, false, parameters)
		{
			if (q == null)
			{
				throw new ArgumentNullException("q");
			}
			this.q = q;
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x0011FC33 File Offset: 0x0011EC33
		public ECPublicKeyParameters(string algorithm, ECPoint q, DerObjectIdentifier publicKeyParamSet) : base(algorithm, false, publicKeyParamSet)
		{
			if (q == null)
			{
				throw new ArgumentNullException("q");
			}
			this.q = q;
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002E9C RID: 11932 RVA: 0x0011FC53 File Offset: 0x0011EC53
		public ECPoint Q
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x0011FC5C File Offset: 0x0011EC5C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ECPublicKeyParameters ecpublicKeyParameters = obj as ECPublicKeyParameters;
			return ecpublicKeyParameters != null && this.Equals(ecpublicKeyParameters);
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x0011FC82 File Offset: 0x0011EC82
		protected bool Equals(ECPublicKeyParameters other)
		{
			return this.q.Equals(other.q) && base.Equals(other);
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x0011FCA0 File Offset: 0x0011ECA0
		public override int GetHashCode()
		{
			return this.q.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04002013 RID: 8211
		private readonly ECPoint q;
	}
}

using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020002F3 RID: 755
	public class ECPrivateKeyParameters : ECKeyParameters
	{
		// Token: 0x06001BC4 RID: 7108 RVA: 0x000A61E4 File Offset: 0x000A51E4
		public ECPrivateKeyParameters(BigInteger d, ECDomainParameters parameters) : this("EC", d, parameters)
		{
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x000A61F3 File Offset: 0x000A51F3
		[Obsolete("Use version with explicit 'algorithm' parameter")]
		public ECPrivateKeyParameters(BigInteger d, DerObjectIdentifier publicKeyParamSet) : base("ECGOST3410", true, publicKeyParamSet)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			this.d = d;
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x000A6217 File Offset: 0x000A5217
		public ECPrivateKeyParameters(string algorithm, BigInteger d, ECDomainParameters parameters) : base(algorithm, true, parameters)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			this.d = d;
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x000A6237 File Offset: 0x000A5237
		public ECPrivateKeyParameters(string algorithm, BigInteger d, DerObjectIdentifier publicKeyParamSet) : base(algorithm, true, publicKeyParamSet)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			this.d = d;
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x000A6257 File Offset: 0x000A5257
		public BigInteger D
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x000A6260 File Offset: 0x000A5260
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ECPrivateKeyParameters ecprivateKeyParameters = obj as ECPrivateKeyParameters;
			return ecprivateKeyParameters != null && this.Equals(ecprivateKeyParameters);
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x000A6286 File Offset: 0x000A5286
		protected bool Equals(ECPrivateKeyParameters other)
		{
			return this.d.Equals(other.d) && base.Equals(other);
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x000A62A4 File Offset: 0x000A52A4
		public override int GetHashCode()
		{
			return this.d.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001307 RID: 4871
		private readonly BigInteger d;
	}
}

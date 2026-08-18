using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000505 RID: 1285
	public class DsaPrivateKeyParameters : DsaKeyParameters
	{
		// Token: 0x06002BD7 RID: 11223 RVA: 0x0010919C File Offset: 0x0010819C
		public DsaPrivateKeyParameters(BigInteger x, DsaParameters parameters) : base(true, parameters)
		{
			if (x == null)
			{
				throw new ArgumentNullException("x");
			}
			this.x = x;
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x001091BB File Offset: 0x001081BB
		public BigInteger X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x001091C4 File Offset: 0x001081C4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DsaPrivateKeyParameters dsaPrivateKeyParameters = obj as DsaPrivateKeyParameters;
			return dsaPrivateKeyParameters != null && this.Equals(dsaPrivateKeyParameters);
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x001091EA File Offset: 0x001081EA
		protected bool Equals(DsaPrivateKeyParameters other)
		{
			return this.x.Equals(other.x) && base.Equals(other);
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x00109208 File Offset: 0x00108208
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001E44 RID: 7748
		private readonly BigInteger x;
	}
}

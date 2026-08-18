using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004BE RID: 1214
	public class DsaPublicKeyParameters : DsaKeyParameters
	{
		// Token: 0x0600295C RID: 10588 RVA: 0x000FC76B File Offset: 0x000FB76B
		public DsaPublicKeyParameters(BigInteger y, DsaParameters parameters) : base(false, parameters)
		{
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			this.y = y;
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x0600295D RID: 10589 RVA: 0x000FC78A File Offset: 0x000FB78A
		public BigInteger Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x000FC794 File Offset: 0x000FB794
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DsaPublicKeyParameters dsaPublicKeyParameters = obj as DsaPublicKeyParameters;
			return dsaPublicKeyParameters != null && this.Equals(dsaPublicKeyParameters);
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000FC7BA File Offset: 0x000FB7BA
		protected bool Equals(DsaPublicKeyParameters other)
		{
			return this.y.Equals(other.y) && base.Equals(other);
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000FC7D8 File Offset: 0x000FB7D8
		public override int GetHashCode()
		{
			return this.y.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001CF0 RID: 7408
		private readonly BigInteger y;
	}
}

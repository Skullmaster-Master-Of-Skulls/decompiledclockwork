using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000348 RID: 840
	public class ElGamalPrivateKeyParameters : ElGamalKeyParameters
	{
		// Token: 0x06001E4B RID: 7755 RVA: 0x000B57C8 File Offset: 0x000B47C8
		public ElGamalPrivateKeyParameters(BigInteger x, ElGamalParameters parameters) : base(true, parameters)
		{
			if (x == null)
			{
				throw new ArgumentNullException("x");
			}
			this.x = x;
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001E4C RID: 7756 RVA: 0x000B57E7 File Offset: 0x000B47E7
		public BigInteger X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x000B57F0 File Offset: 0x000B47F0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ElGamalPrivateKeyParameters elGamalPrivateKeyParameters = obj as ElGamalPrivateKeyParameters;
			return elGamalPrivateKeyParameters != null && this.Equals(elGamalPrivateKeyParameters);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x000B5816 File Offset: 0x000B4816
		protected bool Equals(ElGamalPrivateKeyParameters other)
		{
			return other.x.Equals(this.x) && base.Equals(other);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x000B5834 File Offset: 0x000B4834
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001505 RID: 5381
		private readonly BigInteger x;
	}
}

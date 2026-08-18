using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x0200023F RID: 575
	public class ElGamalPublicKeyParameters : ElGamalKeyParameters
	{
		// Token: 0x0600164C RID: 5708 RVA: 0x0008230F File Offset: 0x0008130F
		public ElGamalPublicKeyParameters(BigInteger y, ElGamalParameters parameters) : base(false, parameters)
		{
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			this.y = y;
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x0008232E File Offset: 0x0008132E
		public BigInteger Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x00082338 File Offset: 0x00081338
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ElGamalPublicKeyParameters elGamalPublicKeyParameters = obj as ElGamalPublicKeyParameters;
			return elGamalPublicKeyParameters != null && this.Equals(elGamalPublicKeyParameters);
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0008235E File Offset: 0x0008135E
		protected bool Equals(ElGamalPublicKeyParameters other)
		{
			return this.y.Equals(other.y) && base.Equals(other);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0008237C File Offset: 0x0008137C
		public override int GetHashCode()
		{
			return this.y.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04000F4E RID: 3918
		private readonly BigInteger y;
	}
}

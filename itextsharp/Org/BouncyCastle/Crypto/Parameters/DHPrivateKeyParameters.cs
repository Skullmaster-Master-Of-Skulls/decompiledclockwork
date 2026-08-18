using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004BF RID: 1215
	public class DHPrivateKeyParameters : DHKeyParameters
	{
		// Token: 0x06002961 RID: 10593 RVA: 0x000FC7EC File Offset: 0x000FB7EC
		public DHPrivateKeyParameters(BigInteger x, DHParameters parameters) : base(true, parameters)
		{
			this.x = x;
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06002962 RID: 10594 RVA: 0x000FC7FD File Offset: 0x000FB7FD
		public BigInteger X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000FC808 File Offset: 0x000FB808
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DHPrivateKeyParameters dhprivateKeyParameters = obj as DHPrivateKeyParameters;
			return dhprivateKeyParameters != null && this.Equals(dhprivateKeyParameters);
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x000FC82E File Offset: 0x000FB82E
		protected bool Equals(DHPrivateKeyParameters other)
		{
			return this.x.Equals(other.x) && base.Equals(other);
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x000FC84C File Offset: 0x000FB84C
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ base.GetHashCode();
		}

		// Token: 0x04001CF1 RID: 7409
		private readonly BigInteger x;
	}
}

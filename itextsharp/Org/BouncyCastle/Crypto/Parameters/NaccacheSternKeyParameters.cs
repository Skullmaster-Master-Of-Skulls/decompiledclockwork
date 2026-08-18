using System;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000345 RID: 837
	public class NaccacheSternKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06001E41 RID: 7745 RVA: 0x000B573F File Offset: 0x000B473F
		public NaccacheSternKeyParameters(bool privateKey, BigInteger g, BigInteger n, int lowerSigmaBound) : base(privateKey)
		{
			this.g = g;
			this.n = n;
			this.lowerSigmaBound = lowerSigmaBound;
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x000B575E File Offset: 0x000B475E
		public BigInteger G
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001E43 RID: 7747 RVA: 0x000B5766 File Offset: 0x000B4766
		public int LowerSigmaBound
		{
			get
			{
				return this.lowerSigmaBound;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x000B576E File Offset: 0x000B476E
		public BigInteger Modulus
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x040014FE RID: 5374
		private readonly BigInteger g;

		// Token: 0x040014FF RID: 5375
		private readonly BigInteger n;

		// Token: 0x04001500 RID: 5376
		private readonly int lowerSigmaBound;
	}
}

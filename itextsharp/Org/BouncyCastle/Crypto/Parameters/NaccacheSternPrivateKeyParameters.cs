using System;
using System.Collections;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x02000346 RID: 838
	public class NaccacheSternPrivateKeyParameters : NaccacheSternKeyParameters
	{
		// Token: 0x06001E45 RID: 7749 RVA: 0x000B5776 File Offset: 0x000B4776
		public NaccacheSternPrivateKeyParameters(BigInteger g, BigInteger n, int lowerSigmaBound, ArrayList smallPrimes, BigInteger phiN) : base(true, g, n, lowerSigmaBound)
		{
			this.smallPrimes = smallPrimes;
			this.phiN = phiN;
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x000B5792 File Offset: 0x000B4792
		public BigInteger PhiN
		{
			get
			{
				return this.phiN;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x000B579A File Offset: 0x000B479A
		public ArrayList SmallPrimes
		{
			get
			{
				return this.smallPrimes;
			}
		}

		// Token: 0x04001501 RID: 5377
		private readonly BigInteger phiN;

		// Token: 0x04001502 RID: 5378
		private readonly ArrayList smallPrimes;
	}
}

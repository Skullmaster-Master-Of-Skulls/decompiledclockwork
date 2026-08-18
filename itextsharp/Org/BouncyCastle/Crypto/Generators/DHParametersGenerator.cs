using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000431 RID: 1073
	public class DHParametersGenerator
	{
		// Token: 0x06002489 RID: 9353 RVA: 0x000DE8E6 File Offset: 0x000DD8E6
		public virtual void Init(int size, int certainty, SecureRandom random)
		{
			this.size = size;
			this.certainty = certainty;
			this.random = random;
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000DE900 File Offset: 0x000DD900
		public virtual DHParameters GenerateParameters()
		{
			BigInteger[] array = DHParametersHelper.GenerateSafePrimes(this.size, this.certainty, this.random);
			BigInteger p = array[0];
			BigInteger q = array[1];
			BigInteger g = DHParametersHelper.SelectGenerator(p, q, this.random);
			return new DHParameters(p, g, q, BigInteger.Two, null);
		}

		// Token: 0x04001986 RID: 6534
		private int size;

		// Token: 0x04001987 RID: 6535
		private int certainty;

		// Token: 0x04001988 RID: 6536
		private SecureRandom random;
	}
}

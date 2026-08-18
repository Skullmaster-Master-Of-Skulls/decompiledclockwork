using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200024A RID: 586
	public class ElGamalParametersGenerator
	{
		// Token: 0x06001681 RID: 5761 RVA: 0x00082B8C File Offset: 0x00081B8C
		public void Init(int size, int certainty, SecureRandom random)
		{
			this.size = size;
			this.certainty = certainty;
			this.random = random;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00082BA4 File Offset: 0x00081BA4
		public ElGamalParameters GenerateParameters()
		{
			BigInteger[] array = DHParametersHelper.GenerateSafePrimes(this.size, this.certainty, this.random);
			BigInteger p = array[0];
			BigInteger q = array[1];
			BigInteger g = DHParametersHelper.SelectGenerator(p, q, this.random);
			return new ElGamalParameters(p, g);
		}

		// Token: 0x04000F66 RID: 3942
		private int size;

		// Token: 0x04000F67 RID: 3943
		private int certainty;

		// Token: 0x04000F68 RID: 3944
		private SecureRandom random;
	}
}

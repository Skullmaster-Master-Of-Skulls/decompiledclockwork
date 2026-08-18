using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200001A RID: 26
	public class RsaBlindingFactorGenerator
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00005E5C File Offset: 0x00004E5C
		public void Init(ICipherParameters param)
		{
			if (param is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)param;
				this.key = (RsaKeyParameters)parametersWithRandom.Parameters;
				this.random = parametersWithRandom.Random;
			}
			else
			{
				this.key = (RsaKeyParameters)param;
				this.random = new SecureRandom();
			}
			if (this.key.IsPrivate)
			{
				throw new ArgumentException("generator requires RSA public key");
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00005EC8 File Offset: 0x00004EC8
		public BigInteger GenerateBlindingFactor()
		{
			if (this.key == null)
			{
				throw new InvalidOperationException("generator not initialised");
			}
			BigInteger modulus = this.key.Modulus;
			int sizeInBits = modulus.BitLength - 1;
			BigInteger bigInteger;
			BigInteger bigInteger2;
			do
			{
				bigInteger = new BigInteger(sizeInBits, this.random);
				bigInteger2 = bigInteger.Gcd(modulus);
			}
			while (bigInteger.SignValue == 0 || bigInteger.Equals(BigInteger.One) || !bigInteger2.Equals(BigInteger.One));
			return bigInteger;
		}

		// Token: 0x04000058 RID: 88
		private RsaKeyParameters key;

		// Token: 0x04000059 RID: 89
		private SecureRandom random;
	}
}

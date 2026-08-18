using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000430 RID: 1072
	public class DsaKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06002486 RID: 9350 RVA: 0x000DE84D File Offset: 0x000DD84D
		public void Init(KeyGenerationParameters parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			this.param = (DsaKeyGenerationParameters)parameters;
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x000DE86C File Offset: 0x000DD86C
		public AsymmetricCipherKeyPair GenerateKeyPair()
		{
			DsaParameters parameters = this.param.Parameters;
			SecureRandom random = this.param.Random;
			BigInteger q = parameters.Q;
			BigInteger bigInteger;
			do
			{
				bigInteger = new BigInteger(160, random);
			}
			while (bigInteger.SignValue == 0 || bigInteger.CompareTo(q) >= 0);
			BigInteger y = parameters.G.ModPow(bigInteger, parameters.P);
			return new AsymmetricCipherKeyPair(new DsaPublicKeyParameters(y, parameters), new DsaPrivateKeyParameters(bigInteger, parameters));
		}

		// Token: 0x04001985 RID: 6533
		private DsaKeyGenerationParameters param;
	}
}

using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020002F8 RID: 760
	public class DHBasicKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001BE3 RID: 7139 RVA: 0x000A6E13 File Offset: 0x000A5E13
		public virtual void Init(KeyGenerationParameters parameters)
		{
			this.param = (DHKeyGenerationParameters)parameters;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x000A6E24 File Offset: 0x000A5E24
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			DHKeyGeneratorHelper instance = DHKeyGeneratorHelper.Instance;
			DHParameters parameters = this.param.Parameters;
			BigInteger x = instance.CalculatePrivate(parameters, this.param.Random);
			BigInteger y = instance.CalculatePublic(parameters, x);
			return new AsymmetricCipherKeyPair(new DHPublicKeyParameters(y, parameters), new DHPrivateKeyParameters(x, parameters));
		}

		// Token: 0x04001310 RID: 4880
		private DHKeyGenerationParameters param;
	}
}

using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020002A4 RID: 676
	public class ElGamalKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x0600197A RID: 6522 RVA: 0x000945F9 File Offset: 0x000935F9
		public void Init(KeyGenerationParameters parameters)
		{
			this.param = (ElGamalKeyGenerationParameters)parameters;
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x00094608 File Offset: 0x00093608
		public AsymmetricCipherKeyPair GenerateKeyPair()
		{
			DHKeyGeneratorHelper instance = DHKeyGeneratorHelper.Instance;
			ElGamalParameters parameters = this.param.Parameters;
			DHParameters dhParams = new DHParameters(parameters.P, parameters.G, null, 0, parameters.L);
			BigInteger x = instance.CalculatePrivate(dhParams, this.param.Random);
			BigInteger y = instance.CalculatePublic(dhParams, x);
			return new AsymmetricCipherKeyPair(new ElGamalPublicKeyParameters(y, parameters), new ElGamalPrivateKeyParameters(x, parameters));
		}

		// Token: 0x04001107 RID: 4359
		private ElGamalKeyGenerationParameters param;
	}
}

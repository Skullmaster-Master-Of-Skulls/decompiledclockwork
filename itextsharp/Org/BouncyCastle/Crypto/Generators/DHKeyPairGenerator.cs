using System;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;

namespace Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x020001F5 RID: 501
	public class DHKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x0600137A RID: 4986 RVA: 0x0006F342 File Offset: 0x0006E342
		public virtual void Init(KeyGenerationParameters parameters)
		{
			this.param = (DHKeyGenerationParameters)parameters;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0006F350 File Offset: 0x0006E350
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			DHKeyGeneratorHelper instance = DHKeyGeneratorHelper.Instance;
			DHParameters parameters = this.param.Parameters;
			BigInteger x = instance.CalculatePrivate(parameters, this.param.Random);
			BigInteger y = instance.CalculatePublic(parameters, x);
			return new AsymmetricCipherKeyPair(new DHPublicKeyParameters(y, parameters), new DHPrivateKeyParameters(x, parameters));
		}

		// Token: 0x04000D96 RID: 3478
		private DHKeyGenerationParameters param;
	}
}

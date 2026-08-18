using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x020001F3 RID: 499
	public interface IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06001374 RID: 4980
		void Init(KeyGenerationParameters parameters);

		// Token: 0x06001375 RID: 4981
		AsymmetricCipherKeyPair GenerateKeyPair();
	}
}

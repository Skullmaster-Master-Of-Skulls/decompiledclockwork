using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200001B RID: 27
	public interface IDerivationFunction
	{
		// Token: 0x060000B0 RID: 176
		void Init(IDerivationParameters parameters);

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000B1 RID: 177
		IDigest Digest { get; }

		// Token: 0x060000B2 RID: 178
		int GenerateBytes(byte[] output, int outOff, int length);
	}
}

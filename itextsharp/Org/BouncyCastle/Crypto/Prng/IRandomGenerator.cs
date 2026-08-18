using System;

namespace Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000011 RID: 17
	public interface IRandomGenerator
	{
		// Token: 0x06000079 RID: 121
		void AddSeedMaterial(byte[] seed);

		// Token: 0x0600007A RID: 122
		void AddSeedMaterial(long seed);

		// Token: 0x0600007B RID: 123
		void NextBytes(byte[] bytes);

		// Token: 0x0600007C RID: 124
		void NextBytes(byte[] bytes, int start, int len);
	}
}

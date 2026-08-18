using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200001E RID: 30
	public interface IStreamCipher
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000B8 RID: 184
		string AlgorithmName { get; }

		// Token: 0x060000B9 RID: 185
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x060000BA RID: 186
		byte ReturnByte(byte input);

		// Token: 0x060000BB RID: 187
		void ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff);

		// Token: 0x060000BC RID: 188
		void Reset();
	}
}

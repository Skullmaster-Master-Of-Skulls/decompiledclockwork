using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000020 RID: 32
	public interface IWrapper
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000CC RID: 204
		string AlgorithmName { get; }

		// Token: 0x060000CD RID: 205
		void Init(bool forWrapping, ICipherParameters parameters);

		// Token: 0x060000CE RID: 206
		byte[] Wrap(byte[] input, int inOff, int length);

		// Token: 0x060000CF RID: 207
		byte[] Unwrap(byte[] input, int inOff, int length);
	}
}

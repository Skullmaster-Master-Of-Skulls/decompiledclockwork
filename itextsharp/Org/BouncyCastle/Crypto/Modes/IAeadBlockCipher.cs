using System;

namespace Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000088 RID: 136
	public interface IAeadBlockCipher
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000433 RID: 1075
		string AlgorithmName { get; }

		// Token: 0x06000434 RID: 1076
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x06000435 RID: 1077
		int GetBlockSize();

		// Token: 0x06000436 RID: 1078
		int ProcessByte(byte input, byte[] outBytes, int outOff);

		// Token: 0x06000437 RID: 1079
		int ProcessBytes(byte[] inBytes, int inOff, int len, byte[] outBytes, int outOff);

		// Token: 0x06000438 RID: 1080
		int DoFinal(byte[] outBytes, int outOff);

		// Token: 0x06000439 RID: 1081
		byte[] GetMac();

		// Token: 0x0600043A RID: 1082
		int GetUpdateOutputSize(int len);

		// Token: 0x0600043B RID: 1083
		int GetOutputSize(int len);

		// Token: 0x0600043C RID: 1084
		void Reset();
	}
}

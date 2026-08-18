using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000092 RID: 146
	public interface IBufferedCipher
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000498 RID: 1176
		string AlgorithmName { get; }

		// Token: 0x06000499 RID: 1177
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x0600049A RID: 1178
		int GetBlockSize();

		// Token: 0x0600049B RID: 1179
		int GetOutputSize(int inputLen);

		// Token: 0x0600049C RID: 1180
		int GetUpdateOutputSize(int inputLen);

		// Token: 0x0600049D RID: 1181
		byte[] ProcessByte(byte input);

		// Token: 0x0600049E RID: 1182
		int ProcessByte(byte input, byte[] output, int outOff);

		// Token: 0x0600049F RID: 1183
		byte[] ProcessBytes(byte[] input);

		// Token: 0x060004A0 RID: 1184
		byte[] ProcessBytes(byte[] input, int inOff, int length);

		// Token: 0x060004A1 RID: 1185
		int ProcessBytes(byte[] input, byte[] output, int outOff);

		// Token: 0x060004A2 RID: 1186
		int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff);

		// Token: 0x060004A3 RID: 1187
		byte[] DoFinal();

		// Token: 0x060004A4 RID: 1188
		byte[] DoFinal(byte[] input);

		// Token: 0x060004A5 RID: 1189
		byte[] DoFinal(byte[] input, int inOff, int length);

		// Token: 0x060004A6 RID: 1190
		int DoFinal(byte[] output, int outOff);

		// Token: 0x060004A7 RID: 1191
		int DoFinal(byte[] input, byte[] output, int outOff);

		// Token: 0x060004A8 RID: 1192
		int DoFinal(byte[] input, int inOff, int length, byte[] output, int outOff);

		// Token: 0x060004A9 RID: 1193
		void Reset();
	}
}

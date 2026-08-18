using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000017 RID: 23
	public interface IMac
	{
		// Token: 0x0600009A RID: 154
		void Init(ICipherParameters parameters);

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600009B RID: 155
		string AlgorithmName { get; }

		// Token: 0x0600009C RID: 156
		int GetMacSize();

		// Token: 0x0600009D RID: 157
		void Update(byte input);

		// Token: 0x0600009E RID: 158
		void BlockUpdate(byte[] input, int inOff, int len);

		// Token: 0x0600009F RID: 159
		int DoFinal(byte[] output, int outOff);

		// Token: 0x060000A0 RID: 160
		void Reset();
	}
}

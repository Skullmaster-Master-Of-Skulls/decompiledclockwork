using System;

namespace Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000432 RID: 1074
	public class NullEngine : IBlockCipher
	{
		// Token: 0x0600248D RID: 9357 RVA: 0x000DE959 File Offset: 0x000DD959
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.initialised = true;
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x000DE962 File Offset: 0x000DD962
		public string AlgorithmName
		{
			get
			{
				return "Null";
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x0600248F RID: 9359 RVA: 0x000DE969 File Offset: 0x000DD969
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x000DE96C File Offset: 0x000DD96C
		public int GetBlockSize()
		{
			return 1;
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x000DE970 File Offset: 0x000DD970
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.initialised)
			{
				throw new InvalidOperationException("Null engine not initialised");
			}
			if (inOff + 1 > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + 1 > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			for (int i = 0; i < 1; i++)
			{
				output[outOff + i] = input[inOff + i];
			}
			return 1;
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000DE9CF File Offset: 0x000DD9CF
		public void Reset()
		{
		}

		// Token: 0x04001989 RID: 6537
		private const int BlockSize = 1;

		// Token: 0x0400198A RID: 6538
		private bool initialised;
	}
}

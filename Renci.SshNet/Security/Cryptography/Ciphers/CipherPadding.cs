using System;

namespace Renci.SshNet.Security.Cryptography.Ciphers
{
	// Token: 0x02000089 RID: 137
	public abstract class CipherPadding
	{
		// Token: 0x06000725 RID: 1829
		public abstract byte[] Pad(int blockSize, byte[] input);

		// Token: 0x06000726 RID: 1830
		public abstract byte[] Pad(byte[] input, int length);
	}
}

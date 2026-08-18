using System;

namespace Renci.SshNet.Security.Cryptography.Ciphers.Paddings
{
	// Token: 0x0200008F RID: 143
	public class PKCS5Padding : CipherPadding
	{
		// Token: 0x06000765 RID: 1893 RVA: 0x0001CCA8 File Offset: 0x0001AEA8
		public override byte[] Pad(int blockSize, byte[] input)
		{
			int length = blockSize - input.Length % blockSize;
			return this.Pad(input, length);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001CCC8 File Offset: 0x0001AEC8
		public override byte[] Pad(byte[] input, int length)
		{
			byte[] array = new byte[input.Length + length];
			Buffer.BlockCopy(input, 0, array, 0, input.Length);
			for (int i = 0; i < length; i++)
			{
				array[input.Length + i] = (byte)length;
			}
			return array;
		}
	}
}

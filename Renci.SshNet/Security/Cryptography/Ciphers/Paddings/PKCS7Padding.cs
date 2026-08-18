using System;

namespace Renci.SshNet.Security.Cryptography.Ciphers.Paddings
{
	// Token: 0x02000090 RID: 144
	public class PKCS7Padding : CipherPadding
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x0001CD0C File Offset: 0x0001AF0C
		public override byte[] Pad(int blockSize, byte[] input)
		{
			int length = blockSize - input.Length % blockSize;
			return this.Pad(input, length);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001CD2C File Offset: 0x0001AF2C
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

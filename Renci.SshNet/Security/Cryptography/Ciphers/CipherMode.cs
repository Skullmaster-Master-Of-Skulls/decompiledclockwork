using System;

namespace Renci.SshNet.Security.Cryptography.Ciphers
{
	// Token: 0x02000088 RID: 136
	public abstract class CipherMode
	{
		// Token: 0x06000721 RID: 1825 RVA: 0x0001861F File Offset: 0x0001681F
		protected CipherMode(byte[] iv)
		{
			this.IV = iv;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001862E File Offset: 0x0001682E
		internal void Init(BlockCipher cipher)
		{
			this.Cipher = cipher;
			this._blockSize = (int)cipher.BlockSize;
			this.IV = this.IV.Take(this._blockSize);
		}

		// Token: 0x06000723 RID: 1827
		public abstract int EncryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

		// Token: 0x06000724 RID: 1828
		public abstract int DecryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

		// Token: 0x040002A1 RID: 673
		protected BlockCipher Cipher;

		// Token: 0x040002A2 RID: 674
		protected byte[] IV;

		// Token: 0x040002A3 RID: 675
		protected int _blockSize;
	}
}

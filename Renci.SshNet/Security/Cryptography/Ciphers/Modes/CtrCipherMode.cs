using System;
using System.Globalization;

namespace Renci.SshNet.Security.Cryptography.Ciphers.Modes
{
	// Token: 0x02000093 RID: 147
	public class CtrCipherMode : CipherMode
	{
		// Token: 0x06000771 RID: 1905 RVA: 0x0001D135 File Offset: 0x0001B335
		public CtrCipherMode(byte[] iv) : base(iv)
		{
			this._ivOutput = new byte[iv.Length];
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001D14C File Offset: 0x0001B34C
		public override int EncryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (inputBuffer.Length - inputOffset < this._blockSize)
			{
				throw new ArgumentException("Invalid input buffer");
			}
			if (outputBuffer.Length - outputOffset < this._blockSize)
			{
				throw new ArgumentException("Invalid output buffer");
			}
			if (inputCount != this._blockSize)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "inputCount must be {0}.", new object[]
				{
					this._blockSize
				}));
			}
			this.Cipher.EncryptBlock(this.IV, 0, this.IV.Length, this._ivOutput, 0);
			for (int i = 0; i < this._blockSize; i++)
			{
				outputBuffer[outputOffset + i] = (this._ivOutput[i] ^ inputBuffer[inputOffset + i]);
			}
			int num = this.IV.Length;
			while (--num >= 0)
			{
				byte[] iv = this.IV;
				int num2 = num;
				byte b = iv[num2] + 1;
				iv[num2] = b;
				if (b != 0)
				{
					break;
				}
			}
			return this._blockSize;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001D234 File Offset: 0x0001B434
		public override int DecryptBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (inputBuffer.Length - inputOffset < this._blockSize)
			{
				throw new ArgumentException("Invalid input buffer");
			}
			if (outputBuffer.Length - outputOffset < this._blockSize)
			{
				throw new ArgumentException("Invalid output buffer");
			}
			if (inputCount != this._blockSize)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "inputCount must be {0}.", new object[]
				{
					this._blockSize
				}));
			}
			this.Cipher.EncryptBlock(this.IV, 0, this.IV.Length, this._ivOutput, 0);
			for (int i = 0; i < this._blockSize; i++)
			{
				outputBuffer[outputOffset + i] = (this._ivOutput[i] ^ inputBuffer[inputOffset + i]);
			}
			int num = this.IV.Length;
			while (--num >= 0)
			{
				byte[] iv = this.IV;
				int num2 = num;
				byte b = iv[num2] + 1;
				iv[num2] = b;
				if (b != 0)
				{
					break;
				}
			}
			return this._blockSize;
		}

		// Token: 0x040002ED RID: 749
		private readonly byte[] _ivOutput;
	}
}

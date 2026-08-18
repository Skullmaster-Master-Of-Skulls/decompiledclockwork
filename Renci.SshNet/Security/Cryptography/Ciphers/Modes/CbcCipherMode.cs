using System;
using System.Globalization;

namespace Renci.SshNet.Security.Cryptography.Ciphers.Modes
{
	// Token: 0x02000091 RID: 145
	public class CbcCipherMode : CipherMode
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x0001CD66 File Offset: 0x0001AF66
		public CbcCipherMode(byte[] iv) : base(iv)
		{
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001CD70 File Offset: 0x0001AF70
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
			for (int i = 0; i < this._blockSize; i++)
			{
				byte[] iv = this.IV;
				int num = i;
				iv[num] ^= inputBuffer[inputOffset + i];
			}
			this.Cipher.EncryptBlock(this.IV, 0, inputCount, outputBuffer, outputOffset);
			Buffer.BlockCopy(outputBuffer, outputOffset, this.IV, 0, this.IV.Length);
			return this._blockSize;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001CE40 File Offset: 0x0001B040
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
			this.Cipher.DecryptBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
			for (int i = 0; i < this._blockSize; i++)
			{
				int num = outputOffset + i;
				outputBuffer[num] ^= this.IV[i];
			}
			Buffer.BlockCopy(inputBuffer, inputOffset, this.IV, 0, this.IV.Length);
			return this._blockSize;
		}
	}
}

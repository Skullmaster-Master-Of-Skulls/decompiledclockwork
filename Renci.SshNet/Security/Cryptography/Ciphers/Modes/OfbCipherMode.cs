using System;
using System.Globalization;

namespace Renci.SshNet.Security.Cryptography.Ciphers.Modes
{
	// Token: 0x02000094 RID: 148
	public class OfbCipherMode : CipherMode
	{
		// Token: 0x06000774 RID: 1908 RVA: 0x0001D31C File Offset: 0x0001B51C
		public OfbCipherMode(byte[] iv) : base(iv)
		{
			this._ivOutput = new byte[iv.Length];
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001D334 File Offset: 0x0001B534
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
			Buffer.BlockCopy(this.IV, this._blockSize, this.IV, 0, this.IV.Length - this._blockSize);
			Buffer.BlockCopy(outputBuffer, outputOffset, this.IV, this.IV.Length - this._blockSize, this._blockSize);
			return this._blockSize;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001D440 File Offset: 0x0001B640
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
			Buffer.BlockCopy(this.IV, this._blockSize, this.IV, 0, this.IV.Length - this._blockSize);
			Buffer.BlockCopy(outputBuffer, outputOffset, this.IV, this.IV.Length - this._blockSize, this._blockSize);
			return this._blockSize;
		}

		// Token: 0x040002EE RID: 750
		private readonly byte[] _ivOutput;
	}
}

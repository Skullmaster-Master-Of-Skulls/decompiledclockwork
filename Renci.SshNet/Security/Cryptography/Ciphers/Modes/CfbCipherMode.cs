using System;
using System.Globalization;

namespace Renci.SshNet.Security.Cryptography.Ciphers.Modes
{
	// Token: 0x02000092 RID: 146
	public class CfbCipherMode : CipherMode
	{
		// Token: 0x0600076E RID: 1902 RVA: 0x0001CF09 File Offset: 0x0001B109
		public CfbCipherMode(byte[] iv) : base(iv)
		{
			this._ivOutput = new byte[iv.Length];
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001CF20 File Offset: 0x0001B120
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

		// Token: 0x06000770 RID: 1904 RVA: 0x0001D02C File Offset: 0x0001B22C
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
			Buffer.BlockCopy(this.IV, this._blockSize, this.IV, 0, this.IV.Length - this._blockSize);
			Buffer.BlockCopy(inputBuffer, inputOffset, this.IV, this.IV.Length - this._blockSize, this._blockSize);
			for (int i = 0; i < this._blockSize; i++)
			{
				outputBuffer[outputOffset + i] = (this._ivOutput[i] ^ inputBuffer[inputOffset + i]);
			}
			return this._blockSize;
		}

		// Token: 0x040002EC RID: 748
		private readonly byte[] _ivOutput;
	}
}

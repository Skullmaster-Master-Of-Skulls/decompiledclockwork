using System;
using System.Security.Cryptography;

namespace Internal.Cryptography
{
	// Token: 0x0200000C RID: 12
	internal sealed class UniversalCryptoDecryptor : UniversalCryptoTransform
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002420 File Offset: 0x00000620
		public UniversalCryptoDecryptor(PaddingMode paddingMode, BasicSymmetricCipher basicSymmetricCipher) : base(paddingMode, basicSymmetricCipher)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000242C File Offset: 0x0000062C
		protected sealed override int UncheckedTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			int num = 0;
			if (this.DepaddingRequired)
			{
				if (this._heldoverCipher != null)
				{
					int num2 = base.BasicSymmetricCipher.Transform(this._heldoverCipher, 0, this._heldoverCipher.Length, outputBuffer, outputOffset);
					outputOffset += num2;
					num += num2;
				}
				else
				{
					this._heldoverCipher = new byte[base.InputBlockSize];
				}
				int srcOffset = inputOffset + inputCount - this._heldoverCipher.Length;
				Buffer.BlockCopy(inputBuffer, srcOffset, this._heldoverCipher, 0, this._heldoverCipher.Length);
				inputCount -= this._heldoverCipher.Length;
			}
			if (inputCount > 0)
			{
				num += base.BasicSymmetricCipher.Transform(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
			}
			return num;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000024D0 File Offset: 0x000006D0
		protected sealed override byte[] UncheckedTransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputCount % base.InputBlockSize != 0)
			{
				throw new CryptographicException(SR.GetString("Cryptography_PartialBlock"));
			}
			byte[] array;
			if (this._heldoverCipher == null)
			{
				array = new byte[inputCount];
				Buffer.BlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
			}
			else
			{
				array = new byte[this._heldoverCipher.Length + inputCount];
				Buffer.BlockCopy(this._heldoverCipher, 0, array, 0, this._heldoverCipher.Length);
				Buffer.BlockCopy(inputBuffer, inputOffset, array, this._heldoverCipher.Length, inputCount);
			}
			byte[] array2 = base.BasicSymmetricCipher.TransformFinal(array, 0, array.Length);
			byte[] result;
			if (array.Length != 0)
			{
				result = this.DepadBlock(array2, 0, array2.Length);
			}
			else
			{
				result = new byte[0];
			}
			this.Reset();
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000257C File Offset: 0x0000077C
		protected sealed override void Dispose(bool disposing)
		{
			if (disposing)
			{
				byte[] heldoverCipher = this._heldoverCipher;
				this._heldoverCipher = null;
				if (heldoverCipher != null)
				{
					Array.Clear(heldoverCipher, 0, heldoverCipher.Length);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025AE File Offset: 0x000007AE
		private void Reset()
		{
			if (this._heldoverCipher != null)
			{
				Array.Clear(this._heldoverCipher, 0, this._heldoverCipher.Length);
				this._heldoverCipher = null;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000025D3 File Offset: 0x000007D3
		private bool DepaddingRequired
		{
			get
			{
				return base.PaddingMode != PaddingMode.None && base.PaddingMode != PaddingMode.Zeros;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025EC File Offset: 0x000007EC
		private byte[] DepadBlock(byte[] block, int offset, int count)
		{
			int num;
			switch (base.PaddingMode)
			{
			case PaddingMode.None:
			case PaddingMode.Zeros:
				num = 0;
				break;
			case PaddingMode.PKCS7:
				num = (int)block[offset + count - 1];
				if (num <= 0 || num > base.InputBlockSize)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidPadding"));
				}
				for (int i = offset + count - num; i < offset + count; i++)
				{
					if ((int)block[i] != num)
					{
						throw new CryptographicException(SR.GetString("Cryptography_InvalidPadding"));
					}
				}
				break;
			case PaddingMode.ANSIX923:
			{
				num = (int)block[offset + count - 1];
				if (num <= 0 || num > base.InputBlockSize)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidPadding"));
				}
				int num2 = offset + count - 1;
				for (int j = offset + count - num; j < num2; j++)
				{
					if (block[j] != 0)
					{
						throw new CryptographicException(SR.GetString("Cryptography_InvalidPadding"));
					}
				}
				break;
			}
			case PaddingMode.ISO10126:
				num = (int)block[offset + count - 1];
				if (num <= 0 || num > base.InputBlockSize)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidPadding"));
				}
				break;
			default:
				throw new CryptographicException(SR.GetString("Cryptography_UnknownPaddingMode"));
			}
			byte[] array = new byte[count - num];
			Buffer.BlockCopy(block, offset, array, 0, array.Length);
			return array;
		}

		// Token: 0x04000064 RID: 100
		private byte[] _heldoverCipher;
	}
}

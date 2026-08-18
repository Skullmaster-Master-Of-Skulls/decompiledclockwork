using System;
using System.Security.Cryptography;

namespace Internal.Cryptography
{
	// Token: 0x0200000E RID: 14
	internal abstract class UniversalCryptoTransform : ICryptoTransform, IDisposable
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000028BE File Offset: 0x00000ABE
		public static ICryptoTransform Create(PaddingMode paddingMode, BasicSymmetricCipher cipher, bool encrypting)
		{
			if (encrypting)
			{
				return new UniversalCryptoEncryptor(paddingMode, cipher);
			}
			return new UniversalCryptoDecryptor(paddingMode, cipher);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028D2 File Offset: 0x00000AD2
		protected UniversalCryptoTransform(PaddingMode paddingMode, BasicSymmetricCipher basicSymmetricCipher)
		{
			this.PaddingMode = paddingMode;
			this.BasicSymmetricCipher = basicSymmetricCipher;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000028E8 File Offset: 0x00000AE8
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000028EB File Offset: 0x00000AEB
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000028EE File Offset: 0x00000AEE
		public int InputBlockSize
		{
			get
			{
				return this.BasicSymmetricCipher.BlockSizeInBytes;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000028FB File Offset: 0x00000AFB
		public int OutputBlockSize
		{
			get
			{
				return this.BasicSymmetricCipher.BlockSizeInBytes;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002908 File Offset: 0x00000B08
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002918 File Offset: 0x00000B18
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset");
			}
			if (inputOffset > inputBuffer.Length)
			{
				throw new ArgumentOutOfRangeException("inputOffset");
			}
			if (inputCount <= 0)
			{
				throw new ArgumentOutOfRangeException("inputCount");
			}
			if (inputCount % this.InputBlockSize != 0)
			{
				throw new ArgumentOutOfRangeException("inputCount", SR.GetString("Cryptography_MustTransformWholeBlock"));
			}
			if (inputCount > inputBuffer.Length - inputOffset)
			{
				throw new ArgumentOutOfRangeException("inputCount", SR.GetString("Cryptography_TransformBeyondEndOfBuffer"));
			}
			if (outputBuffer == null)
			{
				throw new ArgumentNullException("outputBuffer");
			}
			if (outputOffset > outputBuffer.Length)
			{
				throw new ArgumentOutOfRangeException("outputOffset");
			}
			if (inputCount > outputBuffer.Length - outputOffset)
			{
				throw new ArgumentOutOfRangeException("outputOffset", SR.GetString("Cryptography_TransformBeyondEndOfBuffer"));
			}
			return this.UncheckedTransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000029F0 File Offset: 0x00000BF0
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset");
			}
			if (inputCount < 0)
			{
				throw new ArgumentOutOfRangeException("inputCount");
			}
			if (inputOffset > inputBuffer.Length)
			{
				throw new ArgumentOutOfRangeException("inputOffset");
			}
			if (inputCount > inputBuffer.Length - inputOffset)
			{
				throw new ArgumentOutOfRangeException("inputCount", SR.GetString("Cryptography_TransformBeyondEndOfBuffer"));
			}
			return this.UncheckedTransformFinalBlock(inputBuffer, inputOffset, inputCount);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A62 File Offset: 0x00000C62
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.BasicSymmetricCipher.Dispose();
			}
		}

		// Token: 0x06000037 RID: 55
		protected abstract int UncheckedTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

		// Token: 0x06000038 RID: 56
		protected abstract byte[] UncheckedTransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002A72 File Offset: 0x00000C72
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002A7A File Offset: 0x00000C7A
		private protected PaddingMode PaddingMode { protected get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002A83 File Offset: 0x00000C83
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002A8B File Offset: 0x00000C8B
		private protected BasicSymmetricCipher BasicSymmetricCipher { protected get; private set; }
	}
}

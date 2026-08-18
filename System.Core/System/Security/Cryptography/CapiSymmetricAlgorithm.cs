using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020000F5 RID: 245
	internal sealed class CapiSymmetricAlgorithm : ICryptoTransform, IDisposable
	{
		// Token: 0x060007AD RID: 1965 RVA: 0x000190DC File Offset: 0x000172DC
		[SecurityCritical]
		public CapiSymmetricAlgorithm(int blockSize, int feedbackSize, SafeCspHandle provider, SafeCapiKeyHandle key, byte[] iv, CipherMode cipherMode, PaddingMode paddingMode, EncryptionMode encryptionMode)
		{
			this.m_blockSize = blockSize;
			this.m_encryptionMode = encryptionMode;
			this.m_paddingMode = paddingMode;
			this.m_provider = provider.Duplicate();
			this.m_key = CapiSymmetricAlgorithm.SetupKey(key, CapiSymmetricAlgorithm.ProcessIV(iv, blockSize, cipherMode), cipherMode, feedbackSize);
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0001912C File Offset: 0x0001732C
		public bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0001912F File Offset: 0x0001732F
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00019132 File Offset: 0x00017332
		public int InputBlockSize
		{
			get
			{
				return this.m_blockSize / 8;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0001913C File Offset: 0x0001733C
		public int OutputBlockSize
		{
			get
			{
				return this.m_blockSize / 8;
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00019148 File Offset: 0x00017348
		[SecuritySafeCritical]
		public void Dispose()
		{
			if (this.m_key != null)
			{
				this.m_key.Dispose();
			}
			if (this.m_provider != null)
			{
				this.m_provider.Dispose();
			}
			if (this.m_depadBuffer != null)
			{
				Array.Clear(this.m_depadBuffer, 0, this.m_depadBuffer.Length);
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00019198 File Offset: 0x00017398
		[SecuritySafeCritical]
		private int DecryptBlocks(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			int num = 0;
			if (this.m_paddingMode != PaddingMode.None && this.m_paddingMode != PaddingMode.Zeros)
			{
				if (this.m_depadBuffer != null)
				{
					int num2 = this.RawDecryptBlocks(this.m_depadBuffer, 0, this.m_depadBuffer.Length);
					Buffer.BlockCopy(this.m_depadBuffer, 0, outputBuffer, outputOffset, num2);
					Array.Clear(this.m_depadBuffer, 0, this.m_depadBuffer.Length);
					outputOffset += num2;
					num += num2;
				}
				else
				{
					this.m_depadBuffer = new byte[this.InputBlockSize];
				}
				Buffer.BlockCopy(inputBuffer, inputOffset + inputCount - this.m_depadBuffer.Length, this.m_depadBuffer, 0, this.m_depadBuffer.Length);
				inputCount -= this.m_depadBuffer.Length;
			}
			if (inputCount > 0)
			{
				Buffer.BlockCopy(inputBuffer, inputOffset, outputBuffer, outputOffset, inputCount);
				num += this.RawDecryptBlocks(outputBuffer, outputOffset, inputCount);
			}
			return num;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001926C File Offset: 0x0001746C
		private byte[] DepadBlock(byte[] block, int offset, int count)
		{
			int num;
			switch (this.m_paddingMode)
			{
			case PaddingMode.None:
			case PaddingMode.Zeros:
				num = 0;
				break;
			case PaddingMode.PKCS7:
				num = (int)block[offset + count - 1];
				if (num <= 0 || num > this.InputBlockSize)
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
				num = (int)block[offset + count - 1];
				if (num <= 0 || num > this.InputBlockSize)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidPadding"));
				}
				for (int j = offset + count - num; j < offset + count - 1; j++)
				{
					if (block[j] != 0)
					{
						throw new CryptographicException(SR.GetString("Cryptography_InvalidPadding"));
					}
				}
				break;
			case PaddingMode.ISO10126:
				num = (int)block[offset + count - 1];
				if (num <= 0 || num > this.InputBlockSize)
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

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001939C File Offset: 0x0001759C
		[SecurityCritical]
		private unsafe int EncryptBlocks(byte[] buffer, int offset, int count)
		{
			int result = count;
			fixed (byte* ptr = &buffer[offset])
			{
				byte* value = ptr;
				if (!CapiNative.UnsafeNativeMethods.CryptEncrypt(this.m_key, SafeCapiHashHandle.InvalidHandle, false, 0, new IntPtr((void*)value), ref result, buffer.Length - offset))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			return result;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000193E8 File Offset: 0x000175E8
		[SecuritySafeCritical]
		private byte[] PadBlock(byte[] block, int offset, int count)
		{
			int num = this.InputBlockSize - count % this.InputBlockSize;
			byte[] array;
			switch (this.m_paddingMode)
			{
			case PaddingMode.None:
				if (count % this.InputBlockSize != 0)
				{
					throw new CryptographicException(SR.GetString("Cryptography_PartialBlock"));
				}
				array = new byte[count];
				Buffer.BlockCopy(block, offset, array, 0, array.Length);
				break;
			case PaddingMode.PKCS7:
				array = new byte[count + num];
				Buffer.BlockCopy(block, offset, array, 0, count);
				for (int i = count; i < array.Length; i++)
				{
					array[i] = (byte)num;
				}
				break;
			case PaddingMode.Zeros:
				if (num == this.InputBlockSize)
				{
					num = 0;
				}
				array = new byte[count + num];
				Buffer.BlockCopy(block, offset, array, 0, count);
				break;
			case PaddingMode.ANSIX923:
				array = new byte[count + num];
				Buffer.BlockCopy(block, 0, array, 0, count);
				array[array.Length - 1] = (byte)num;
				break;
			case PaddingMode.ISO10126:
				array = new byte[count + num];
				CapiNative.UnsafeNativeMethods.CryptGenRandom(this.m_provider, array.Length - 1, array);
				Buffer.BlockCopy(block, 0, array, 0, count);
				array[array.Length - 1] = (byte)num;
				break;
			default:
				throw new CryptographicException(SR.GetString("Cryptography_UnknownPaddingMode"));
			}
			return array;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001950C File Offset: 0x0001770C
		private static byte[] ProcessIV(byte[] iv, int blockSize, CipherMode cipherMode)
		{
			byte[] array = null;
			if (iv != null)
			{
				if (blockSize / 8 > iv.Length)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidIVSize"));
				}
				array = new byte[blockSize / 8];
				Buffer.BlockCopy(iv, 0, array, 0, array.Length);
			}
			else if (cipherMode != CipherMode.ECB)
			{
				throw new CryptographicException(SR.GetString("Cryptography_MissingIV"));
			}
			return array;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00019564 File Offset: 0x00017764
		[SecurityCritical]
		private unsafe int RawDecryptBlocks(byte[] buffer, int offset, int count)
		{
			int result = count;
			fixed (byte* ptr = &buffer[offset])
			{
				byte* value = ptr;
				if (!CapiNative.UnsafeNativeMethods.CryptDecrypt(this.m_key, SafeCapiHashHandle.InvalidHandle, false, 0, new IntPtr((void*)value), ref result))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
			return result;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x000195AC File Offset: 0x000177AC
		[SecuritySafeCritical]
		private unsafe void Reset()
		{
			byte[] array = new byte[this.OutputBlockSize];
			int num = 0;
			byte[] array2;
			byte* value;
			if ((array2 = array) == null || array2.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array2[0];
			}
			if (this.m_encryptionMode == EncryptionMode.Encrypt)
			{
				CapiNative.UnsafeNativeMethods.CryptEncrypt(this.m_key, SafeCapiHashHandle.InvalidHandle, true, 0, new IntPtr((void*)value), ref num, array.Length);
			}
			else
			{
				if (!LocalAppContextSwitches.AesCryptoServiceProviderDontCorrectlyResetDecryptor)
				{
					num = array.Length;
				}
				CapiNative.UnsafeNativeMethods.CryptDecrypt(this.m_key, SafeCapiHashHandle.InvalidHandle, true, 0, new IntPtr((void*)value), ref num);
			}
			array2 = null;
			if (this.m_depadBuffer != null)
			{
				Array.Clear(this.m_depadBuffer, 0, this.m_depadBuffer.Length);
				this.m_depadBuffer = null;
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00019654 File Offset: 0x00017854
		[SecuritySafeCritical]
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
			if (inputCount > outputBuffer.Length - outputOffset)
			{
				throw new ArgumentOutOfRangeException("outputOffset", SR.GetString("Cryptography_TransformBeyondEndOfBuffer"));
			}
			if (this.m_encryptionMode == EncryptionMode.Encrypt)
			{
				Buffer.BlockCopy(inputBuffer, inputOffset, outputBuffer, outputOffset, inputCount);
				return this.EncryptBlocks(outputBuffer, outputOffset, inputCount);
			}
			return this.DecryptBlocks(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00019724 File Offset: 0x00017924
		[SecuritySafeCritical]
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
			if (inputCount > inputBuffer.Length - inputOffset)
			{
				throw new ArgumentOutOfRangeException("inputCount", SR.GetString("Cryptography_TransformBeyondEndOfBuffer"));
			}
			byte[] array;
			if (this.m_encryptionMode == EncryptionMode.Encrypt)
			{
				array = this.PadBlock(inputBuffer, inputOffset, inputCount);
				if (array.Length != 0)
				{
					this.EncryptBlocks(array, 0, array.Length);
				}
			}
			else
			{
				if (inputCount % this.InputBlockSize != 0)
				{
					throw new CryptographicException(SR.GetString("Cryptography_PartialBlock"));
				}
				byte[] array2;
				if (this.m_depadBuffer == null)
				{
					array2 = new byte[inputCount];
					Buffer.BlockCopy(inputBuffer, inputOffset, array2, 0, inputCount);
				}
				else
				{
					array2 = new byte[this.m_depadBuffer.Length + inputCount];
					Buffer.BlockCopy(this.m_depadBuffer, 0, array2, 0, this.m_depadBuffer.Length);
					Buffer.BlockCopy(inputBuffer, inputOffset, array2, this.m_depadBuffer.Length, inputCount);
				}
				if (array2.Length != 0)
				{
					int count = this.RawDecryptBlocks(array2, 0, array2.Length);
					array = this.DepadBlock(array2, 0, count);
				}
				else
				{
					array = new byte[0];
				}
			}
			this.Reset();
			return array;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00019840 File Offset: 0x00017A40
		[SecurityCritical]
		private static SafeCapiKeyHandle SetupKey(SafeCapiKeyHandle key, byte[] iv, CipherMode cipherMode, int feedbackSize)
		{
			SafeCapiKeyHandle safeCapiKeyHandle = key.Duplicate();
			CapiNative.SetKeyParameter(safeCapiKeyHandle, CapiNative.KeyParameter.Mode, (int)cipherMode);
			if (cipherMode != CipherMode.ECB)
			{
				CapiNative.SetKeyParameter(safeCapiKeyHandle, CapiNative.KeyParameter.IV, iv);
			}
			if (cipherMode == CipherMode.CFB || cipherMode == CipherMode.OFB)
			{
				CapiNative.SetKeyParameter(safeCapiKeyHandle, CapiNative.KeyParameter.ModeBits, feedbackSize);
			}
			return safeCapiKeyHandle;
		}

		// Token: 0x04000646 RID: 1606
		private int m_blockSize;

		// Token: 0x04000647 RID: 1607
		private byte[] m_depadBuffer;

		// Token: 0x04000648 RID: 1608
		private EncryptionMode m_encryptionMode;

		// Token: 0x04000649 RID: 1609
		[SecurityCritical]
		private SafeCapiKeyHandle m_key;

		// Token: 0x0400064A RID: 1610
		private PaddingMode m_paddingMode;

		// Token: 0x0400064B RID: 1611
		[SecurityCritical]
		private SafeCspHandle m_provider;
	}
}

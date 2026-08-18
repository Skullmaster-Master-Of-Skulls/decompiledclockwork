using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace System.IdentityModel
{
	// Token: 0x0200006C RID: 108
	internal class RijndaelCryptoServiceProvider : Rijndael
	{
		// Token: 0x06000344 RID: 836 RVA: 0x0000C820 File Offset: 0x0000AA20
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (rgbKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rgbKey");
			}
			if (rgbIV == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rgbIV");
			}
			if (this.ModeValue != CipherMode.CBC)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("AESCipherModeNotSupported", new object[]
				{
					this.ModeValue
				})));
			}
			return new RijndaelCryptoServiceProvider.RijndaelCryptoTransform(rgbKey, rgbIV, this.PaddingValue, this.BlockSizeValue, true);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000C8A0 File Offset: 0x0000AAA0
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
		{
			if (rgbKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rgbKey");
			}
			if (rgbIV == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rgbIV");
			}
			if (this.ModeValue != CipherMode.CBC)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("AESCipherModeNotSupported", new object[]
				{
					this.ModeValue
				})));
			}
			return new RijndaelCryptoServiceProvider.RijndaelCryptoTransform(rgbKey, rgbIV, this.PaddingValue, this.BlockSizeValue, false);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000C91E File Offset: 0x0000AB1E
		public override void GenerateKey()
		{
			this.KeyValue = new byte[this.KeySizeValue / 8];
			CryptoHelper.RandomNumberGenerator.GetBytes(this.KeyValue);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000C943 File Offset: 0x0000AB43
		public override void GenerateIV()
		{
			this.IVValue = new byte[this.BlockSizeValue / 8];
			CryptoHelper.RandomNumberGenerator.GetBytes(this.IVValue);
		}

		// Token: 0x02000238 RID: 568
		private class RijndaelCryptoTransform : ICryptoTransform, IDisposable
		{
			// Token: 0x06001207 RID: 4615 RVA: 0x0004EF54 File Offset: 0x0004D154
			public unsafe RijndaelCryptoTransform(byte[] rgbKey, byte[] rgbIV, PaddingMode paddingMode, int blockSizeBits, bool encrypt)
			{
				if (rgbKey.Length != 16 && rgbKey.Length != 24 && rgbKey.Length != 32)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("AESKeyLengthNotSupported", new object[]
					{
						rgbKey.Length * 8
					})));
				}
				if (rgbIV.Length != 16)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("AESIVLengthNotSupported", new object[]
					{
						rgbIV.Length * 8
					})));
				}
				if (paddingMode != PaddingMode.PKCS7 && paddingMode != PaddingMode.ISO10126)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("AESPaddingModeNotSupported", new object[]
					{
						paddingMode
					})));
				}
				this.paddingMode = paddingMode;
				this.blockSize = blockSizeBits / 8;
				this.encrypt = encrypt;
				SafeProvHandle safeProvHandle = null;
				SafeKeyHandle safeKeyHandle = null;
				try
				{
					RijndaelCryptoServiceProvider.RijndaelCryptoTransform.ThrowIfFalse("AESCryptAcquireContextFailed", NativeMethods.CryptAcquireContextW(out safeProvHandle, null, null, 24U, 4026531840U));
					int num = PLAINTEXTKEYBLOBHEADER.SizeOf + rgbKey.Length;
					byte[] array = new byte[num];
					Buffer.BlockCopy(rgbKey, 0, array, PLAINTEXTKEYBLOBHEADER.SizeOf, rgbKey.Length);
					try
					{
						fixed (byte* ptr = &array[0])
						{
							void* ptr2 = (void*)ptr;
							PLAINTEXTKEYBLOBHEADER* ptr3 = (PLAINTEXTKEYBLOBHEADER*)ptr2;
							ptr3->bType = 8;
							ptr3->bVersion = 2;
							ptr3->reserved = 0;
							if (rgbKey.Length == 16)
							{
								ptr3->aiKeyAlg = 26126;
							}
							else if (rgbKey.Length == 24)
							{
								ptr3->aiKeyAlg = 26127;
							}
							else
							{
								ptr3->aiKeyAlg = 26128;
							}
							ptr3->keyLength = rgbKey.Length;
							safeKeyHandle = SafeKeyHandle.SafeCryptImportKey(safeProvHandle, ptr2, num);
						}
					}
					finally
					{
						byte* ptr = null;
					}
					try
					{
						fixed (byte* ptr4 = &rgbIV[0])
						{
							void* pbData = (void*)ptr4;
							RijndaelCryptoServiceProvider.RijndaelCryptoTransform.ThrowIfFalse("AESCryptSetKeyParamFailed", NativeMethods.CryptSetKeyParam(safeKeyHandle, 1U, pbData, 0U));
						}
					}
					finally
					{
						byte* ptr4 = null;
					}
					this.keyHandle = safeKeyHandle;
					this.provHandle = safeProvHandle;
					safeKeyHandle = null;
					safeProvHandle = null;
				}
				finally
				{
					if (safeKeyHandle != null)
					{
						safeKeyHandle.Close();
					}
					if (safeProvHandle != null)
					{
						safeProvHandle.Close();
					}
				}
			}

			// Token: 0x170004FE RID: 1278
			// (get) Token: 0x06001208 RID: 4616 RVA: 0x00002434 File Offset: 0x00000634
			public bool CanReuseTransform
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170004FF RID: 1279
			// (get) Token: 0x06001209 RID: 4617 RVA: 0x00002434 File Offset: 0x00000634
			public bool CanTransformMultipleBlocks
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000500 RID: 1280
			// (get) Token: 0x0600120A RID: 4618 RVA: 0x0004F170 File Offset: 0x0004D370
			public int InputBlockSize
			{
				get
				{
					return this.blockSize;
				}
			}

			// Token: 0x17000501 RID: 1281
			// (get) Token: 0x0600120B RID: 4619 RVA: 0x0004F170 File Offset: 0x0004D370
			public int OutputBlockSize
			{
				get
				{
					return this.blockSize;
				}
			}

			// Token: 0x0600120C RID: 4620 RVA: 0x0004F178 File Offset: 0x0004D378
			public void Dispose()
			{
				try
				{
					this.keyHandle.Close();
				}
				finally
				{
					this.provHandle.Close();
				}
			}

			// Token: 0x0600120D RID: 4621 RVA: 0x0004F1B0 File Offset: 0x0004D3B0
			public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
			{
				if (inputBuffer == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("inputBuffer");
				}
				if (outputBuffer == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("outputBuffer");
				}
				if (inputOffset < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("inputOffset", SR.GetString("ValueMustBeNonNegative")));
				}
				if (inputCount <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("inputCount", SR.GetString("ValueMustBeGreaterThanZero")));
				}
				if (outputOffset < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("outputOffset", SR.GetString("ValueMustBeNonNegative")));
				}
				if (inputCount % this.blockSize != 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("AESInvalidInputBlockSize", new object[]
					{
						inputCount,
						this.blockSize
					})));
				}
				if (inputBuffer.Length - inputCount < inputOffset)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("inputOffset", SR.GetString("ValueMustBeInRange", new object[]
					{
						0,
						inputBuffer.Length - inputCount - 1
					})));
				}
				if (outputBuffer.Length < outputOffset)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("outputOffset", SR.GetString("ValueMustBeInRange", new object[]
					{
						0,
						outputBuffer.Length - 1
					})));
				}
				if (this.encrypt)
				{
					return this.EncryptData(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset, false);
				}
				if (this.paddingMode == PaddingMode.PKCS7)
				{
					return this.DecryptData(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset, false);
				}
				if (this.depadBuffer != null)
				{
					int num = this.DecryptData(this.depadBuffer, 0, this.depadBuffer.Length, outputBuffer, outputOffset, false);
					outputOffset += num;
					int num2 = inputCount - this.blockSize;
					Buffer.BlockCopy(inputBuffer, inputOffset + num2, this.depadBuffer, 0, this.blockSize);
					return num + ((num2 <= 0) ? 0 : this.DecryptData(inputBuffer, inputOffset, num2, outputBuffer, outputOffset, false));
				}
				this.depadBuffer = new byte[this.blockSize];
				int num3 = inputCount - this.blockSize;
				Buffer.BlockCopy(inputBuffer, inputOffset + num3, this.depadBuffer, 0, this.blockSize);
				if (num3 > 0)
				{
					return this.DecryptData(inputBuffer, inputOffset, num3, outputBuffer, outputOffset, false);
				}
				return 0;
			}

			// Token: 0x0600120E RID: 4622 RVA: 0x0004F3EC File Offset: 0x0004D5EC
			public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
			{
				if (inputBuffer == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("inputBuffer");
				}
				if (inputOffset < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("inputOffset", SR.GetString("ValueMustBeNonNegative")));
				}
				if (inputCount < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("inputCount", SR.GetString("ValueMustBeNonNegative")));
				}
				if (inputBuffer.Length - inputCount < inputOffset)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("inputOffset", SR.GetString("ValueMustBeInRange", new object[]
					{
						0,
						inputBuffer.Length - inputCount - 1
					})));
				}
				if (this.encrypt)
				{
					int num = this.blockSize - inputCount % this.blockSize;
					int num2 = inputCount + num;
					if (this.paddingMode == PaddingMode.ISO10126)
					{
						num2 += this.blockSize;
					}
					byte[] array = new byte[num2];
					int len = this.EncryptData(inputBuffer, inputOffset, inputCount, array, 0, true);
					return this.TruncateBuffer(array, len);
				}
				if (this.paddingMode == PaddingMode.PKCS7)
				{
					byte[] array2 = new byte[inputCount];
					int len2 = this.DecryptData(inputBuffer, inputOffset, inputCount, array2, 0, true);
					return this.TruncateBuffer(array2, len2);
				}
				if (this.depadBuffer == null)
				{
					byte[] array3 = new byte[inputCount];
					int len3 = this.DecryptData(inputBuffer, inputOffset, inputCount, array3, 0, true);
					return this.TruncateBuffer(array3, len3);
				}
				byte[] array4 = new byte[this.depadBuffer.Length + inputCount];
				int num3 = this.DecryptData(this.depadBuffer, 0, this.depadBuffer.Length, array4, 0, false);
				num3 += this.DecryptData(inputBuffer, inputOffset, inputCount, array4, num3, true);
				return this.TruncateBuffer(array4, num3);
			}

			// Token: 0x0600120F RID: 4623 RVA: 0x0004F580 File Offset: 0x0004D780
			private unsafe int EncryptData(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset, bool final)
			{
				if (outputBuffer.Length - outputOffset < inputCount)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("outputBuffer", SR.GetString("AESInsufficientOutputBuffer", new object[]
					{
						outputBuffer.Length - outputOffset,
						inputCount
					})));
				}
				bool flag = final && this.paddingMode == PaddingMode.ISO10126;
				byte[] array = outputBuffer;
				int num = outputOffset;
				int num2 = inputCount;
				bool flag2 = true;
				Buffer.BlockCopy(inputBuffer, inputOffset, array, num, inputCount);
				try
				{
					if (flag)
					{
						this.DoPadding(ref array, ref num, ref num2);
					}
					try
					{
						fixed (byte* ptr = &array[num])
						{
							void* pbData = (void*)ptr;
							RijndaelCryptoServiceProvider.RijndaelCryptoTransform.ThrowIfFalse("AESCryptEncryptFailed", NativeMethods.CryptEncrypt(this.keyHandle, IntPtr.Zero, final, 0U, pbData, ref num2, array.Length - num));
						}
					}
					finally
					{
						byte* ptr = null;
					}
					flag2 = false;
				}
				finally
				{
					if (flag2)
					{
						Array.Clear(array, num, inputCount);
					}
				}
				if (flag)
				{
					num2 -= this.blockSize;
				}
				if (array != outputBuffer)
				{
					Buffer.BlockCopy(array, num, outputBuffer, outputOffset, num2);
				}
				return num2;
			}

			// Token: 0x06001210 RID: 4624 RVA: 0x0004F694 File Offset: 0x0004D894
			private unsafe int DecryptData(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset, bool final)
			{
				bool flag = final && this.paddingMode == PaddingMode.PKCS7;
				int num = inputCount;
				if (num > 0)
				{
					Buffer.BlockCopy(inputBuffer, inputOffset, outputBuffer, outputOffset, inputCount);
					fixed (byte* ptr = &outputBuffer[outputOffset])
					{
						void* pbData = (void*)ptr;
						RijndaelCryptoServiceProvider.RijndaelCryptoTransform.ThrowIfFalse("AESCryptDecryptFailed", NativeMethods.CryptDecrypt(this.keyHandle, IntPtr.Zero, flag, 0U, pbData, ref num));
					}
				}
				if (!flag && final)
				{
					byte b = outputBuffer[outputOffset + num - 1];
					num -= (int)b;
				}
				return num;
			}

			// Token: 0x06001211 RID: 4625 RVA: 0x0004F710 File Offset: 0x0004D910
			private void DoPadding(ref byte[] tempBuffer, ref int tempOffset, ref int dwCount)
			{
				int num = dwCount % this.blockSize;
				int num2 = this.blockSize - num;
				byte[] array = new byte[num2];
				CryptoHelper.RandomNumberGenerator.GetBytes(array);
				array[num2 - 1] = (byte)num2;
				int num3 = dwCount + num2 + this.blockSize;
				if (tempBuffer.Length >= tempOffset + num3)
				{
					Buffer.BlockCopy(array, 0, tempBuffer, tempOffset + dwCount, num2);
				}
				else
				{
					byte[] array2 = new byte[num3];
					Buffer.BlockCopy(tempBuffer, tempOffset, array2, 0, dwCount);
					Buffer.BlockCopy(array, 0, array2, dwCount, num2);
					Array.Clear(tempBuffer, tempOffset, dwCount);
					tempBuffer = array2;
					tempOffset = 0;
				}
				dwCount += num2;
			}

			// Token: 0x06001212 RID: 4626 RVA: 0x0004F7AC File Offset: 0x0004D9AC
			private byte[] TruncateBuffer(byte[] buffer, int len)
			{
				if (len == buffer.Length)
				{
					return buffer;
				}
				byte[] array = new byte[len];
				Buffer.BlockCopy(buffer, 0, array, 0, len);
				if (!this.encrypt)
				{
					Array.Clear(buffer, 0, buffer.Length);
				}
				return array;
			}

			// Token: 0x06001213 RID: 4627 RVA: 0x0004F7E8 File Offset: 0x0004D9E8
			private static void ThrowIfFalse(string sr, bool ret)
			{
				if (!ret)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					string text = (lastWin32Error != 0) ? new Win32Exception(lastWin32Error).Message : string.Empty;
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString(sr, new object[]
					{
						text
					})));
				}
			}

			// Token: 0x04000F52 RID: 3922
			private SafeProvHandle provHandle = SafeProvHandle.InvalidHandle;

			// Token: 0x04000F53 RID: 3923
			private SafeKeyHandle keyHandle = SafeKeyHandle.InvalidHandle;

			// Token: 0x04000F54 RID: 3924
			private PaddingMode paddingMode;

			// Token: 0x04000F55 RID: 3925
			private byte[] depadBuffer;

			// Token: 0x04000F56 RID: 3926
			private int blockSize;

			// Token: 0x04000F57 RID: 3927
			private bool encrypt;
		}
	}
}

using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Internal.Cryptography
{
	// Token: 0x0200000F RID: 15
	internal sealed class BasicSymmetricCipherNCrypt : BasicSymmetricCipher
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002A94 File Offset: 0x00000C94
		public BasicSymmetricCipherNCrypt(Func<CngKey> cngKeyFactory, CipherMode cipherMode, int blockSizeInBytes, byte[] iv, bool encrypting) : base(iv, blockSizeInBytes)
		{
			this._encrypting = encrypting;
			this._cngKey = cngKeyFactory();
			CngProperty property;
			if (cipherMode != CipherMode.CBC)
			{
				if (cipherMode != CipherMode.ECB)
				{
					throw new CryptographicException(SR.GetString("Cryptography_InvalidCipherMode"));
				}
				property = BasicSymmetricCipherNCrypt.s_ECBMode;
			}
			else
			{
				property = BasicSymmetricCipherNCrypt.s_CBCMode;
			}
			this._cngKey.SetProperty(property);
			this.Reset();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002AF8 File Offset: 0x00000CF8
		[SecuritySafeCritical]
		public unsafe sealed override int Transform(byte[] input, int inputOffset, int count, byte[] output, int outputOffset)
		{
			byte* ptr;
			if (input == null || input.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &input[0];
			}
			byte* ptr2;
			if (output == null || output.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &output[0];
			}
			int num;
			Interop.NCrypt.ErrorCode errorCode;
			if (this._encrypting)
			{
				errorCode = Interop.NCrypt.NCryptEncrypt(this._cngKey.Handle, ptr + inputOffset, count, null, ptr2 + outputOffset, count, out num, Interop.NCrypt.AsymmetricPaddingMode.None);
			}
			else
			{
				errorCode = Interop.NCrypt.NCryptDecrypt(this._cngKey.Handle, ptr + inputOffset, count, null, ptr2 + outputOffset, count, out num, Interop.NCrypt.AsymmetricPaddingMode.None);
			}
			if (errorCode != Interop.NCrypt.ErrorCode.ERROR_SUCCESS)
			{
				throw errorCode.ToCryptographicException();
			}
			if (num != count)
			{
				throw new CryptographicException(SR.GetString("Cryptography_UnexpectedTransformTruncation"));
			}
			return num;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public sealed override byte[] TransformFinal(byte[] input, int inputOffset, int count)
		{
			byte[] array = new byte[count];
			if (count != 0)
			{
				int num = this.Transform(input, inputOffset, count, array, 0);
			}
			this.Reset();
			return array;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002BCE File Offset: 0x00000DCE
		protected sealed override void Dispose(bool disposing)
		{
			if (disposing && this._cngKey != null)
			{
				this._cngKey.Dispose();
				this._cngKey = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002BF4 File Offset: 0x00000DF4
		private void Reset()
		{
			if (base.IV != null)
			{
				CngProperty property = new CngProperty("IV", base.IV, CngPropertyOptions.None);
				this._cngKey.SetProperty(property);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C28 File Offset: 0x00000E28
		private static CngProperty CreateCngPropertyForCipherMode(string cipherMode)
		{
			byte[] bytes = Encoding.Unicode.GetBytes((cipherMode + "\0").ToCharArray());
			return new CngProperty("Chaining Mode", bytes, CngPropertyOptions.None);
		}

		// Token: 0x04000068 RID: 104
		private CngKey _cngKey;

		// Token: 0x04000069 RID: 105
		private readonly bool _encrypting;

		// Token: 0x0400006A RID: 106
		private static readonly CngProperty s_ECBMode = BasicSymmetricCipherNCrypt.CreateCngPropertyForCipherMode("ChainingModeECB");

		// Token: 0x0400006B RID: 107
		private static readonly CngProperty s_CBCMode = BasicSymmetricCipherNCrypt.CreateCngPropertyForCipherMode("ChainingModeCBC");
	}
}

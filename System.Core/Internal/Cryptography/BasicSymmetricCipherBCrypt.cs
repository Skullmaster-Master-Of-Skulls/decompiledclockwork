using System;
using System.Security;
using System.Security.Cryptography;
using Microsoft.Win32.SafeHandles;

namespace Internal.Cryptography
{
	// Token: 0x02000010 RID: 16
	internal sealed class BasicSymmetricCipherBCrypt : BasicSymmetricCipher
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002C7C File Offset: 0x00000E7C
		[SecuritySafeCritical]
		public BasicSymmetricCipherBCrypt(SafeBCryptAlgorithmHandle algorithm, CipherMode cipherMode, int blockSizeInBytes, byte[] key, byte[] iv, bool encrypting) : base(cipherMode.GetCipherIv(iv), blockSizeInBytes)
		{
			this._encrypting = encrypting;
			if (base.IV != null)
			{
				this._currentIv = new byte[base.IV.Length];
			}
			this._hKey = BCryptNative.BCryptImportKey(algorithm, key);
			this.Reset();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002CD0 File Offset: 0x00000ED0
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				SafeBCryptKeyHandle hKey = this._hKey;
				this._hKey = null;
				if (hKey != null)
				{
					hKey.Dispose();
				}
				byte[] currentIv = this._currentIv;
				this._currentIv = null;
				if (currentIv != null)
				{
					Array.Clear(currentIv, 0, currentIv.Length);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002D1C File Offset: 0x00000F1C
		[SecuritySafeCritical]
		public override int Transform(byte[] input, int inputOffset, int count, byte[] output, int outputOffset)
		{
			int num;
			if (this._encrypting)
			{
				num = BCryptNative.BCryptEncrypt(this._hKey, input, inputOffset, count, this._currentIv, output, outputOffset, output.Length - outputOffset);
			}
			else
			{
				num = BCryptNative.BCryptDecrypt(this._hKey, input, inputOffset, count, this._currentIv, output, outputOffset, output.Length - outputOffset);
			}
			if (num != count)
			{
				throw new CryptographicException("Cryptography_UnexpectedTransformTruncation");
			}
			return num;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D84 File Offset: 0x00000F84
		public override byte[] TransformFinal(byte[] input, int inputOffset, int count)
		{
			byte[] array = new byte[count];
			if (count != 0)
			{
				int num = this.Transform(input, inputOffset, count, array, 0);
			}
			this.Reset();
			return array;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002DAE File Offset: 0x00000FAE
		private void Reset()
		{
			if (base.IV != null)
			{
				Buffer.BlockCopy(base.IV, 0, this._currentIv, 0, base.IV.Length);
			}
		}

		// Token: 0x0400006C RID: 108
		private readonly bool _encrypting;

		// Token: 0x0400006D RID: 109
		private SafeBCryptKeyHandle _hKey;

		// Token: 0x0400006E RID: 110
		private byte[] _currentIv;
	}
}

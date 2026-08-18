using System;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Cryptography;

namespace System.Data.SqlClient
{
	// Token: 0x02000192 RID: 402
	internal class SqlAeadAes256CbcHmac256Algorithm : SqlClientEncryptionAlgorithm
	{
		// Token: 0x06001803 RID: 6147 RVA: 0x000AAB84 File Offset: 0x000A9F84
		internal SqlAeadAes256CbcHmac256Algorithm(SqlAeadAes256CbcHmac256EncryptionKey encryptionKey, SqlClientEncryptionType encryptionType, byte algorithmVersion)
		{
			this._columnEncryptionKey = encryptionKey;
			this._algorithmVersion = algorithmVersion;
			SqlAeadAes256CbcHmac256Algorithm._version[0] = algorithmVersion;
			if (encryptionType == SqlClientEncryptionType.Deterministic)
			{
				this._isDeterministic = true;
			}
			this._cryptoProviderPool = new ConcurrentQueue<AesCryptoServiceProvider>();
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x000AABC4 File Offset: 0x000A9FC4
		internal override byte[] EncryptData(byte[] plainText)
		{
			return this.EncryptData(plainText, true);
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x000AABDC File Offset: 0x000A9FDC
		protected byte[] EncryptData(byte[] plainText, bool hasAuthenticationTag)
		{
			byte[] array = new byte[16];
			if (this._isDeterministic)
			{
				SqlSecurityUtility.GetHMACWithSHA256(plainText, this._columnEncryptionKey.IVKey, array);
			}
			else
			{
				SqlSecurityUtility.GenerateRandomBytes(array);
			}
			int num = plainText.Length / 16 + 1;
			int num2 = hasAuthenticationTag ? 32 : 0;
			int num3 = 1 + num2;
			int num4 = num3 + 16;
			int num5 = 1 + num2 + array.Length + num * 16;
			byte[] array2 = new byte[num5];
			array2[0] = this._algorithmVersion;
			Buffer.BlockCopy(array, 0, array2, num3, array.Length);
			AesCryptoServiceProvider aesCryptoServiceProvider;
			if (!this._cryptoProviderPool.TryDequeue(out aesCryptoServiceProvider))
			{
				aesCryptoServiceProvider = new AesCryptoServiceProvider();
				try
				{
					aesCryptoServiceProvider.Key = this._columnEncryptionKey.EncryptionKey;
					aesCryptoServiceProvider.Mode = CipherMode.CBC;
					aesCryptoServiceProvider.Padding = PaddingMode.PKCS7;
				}
				catch (Exception)
				{
					if (aesCryptoServiceProvider != null)
					{
						aesCryptoServiceProvider.Dispose();
					}
					throw;
				}
			}
			try
			{
				aesCryptoServiceProvider.IV = array;
				using (ICryptoTransform cryptoTransform = aesCryptoServiceProvider.CreateEncryptor())
				{
					int num6 = 0;
					int num7 = num4;
					if (num > 1)
					{
						num6 = (num - 1) * 16;
						num7 += cryptoTransform.TransformBlock(plainText, 0, num6, array2, num7);
					}
					byte[] array3 = cryptoTransform.TransformFinalBlock(plainText, num6, plainText.Length - num6);
					Buffer.BlockCopy(array3, 0, array2, num7, array3.Length);
					num7 += array3.Length;
				}
				if (hasAuthenticationTag)
				{
					using (HMACSHA256 hmacsha = new HMACSHA256(this._columnEncryptionKey.MACKey))
					{
						hmacsha.TransformBlock(SqlAeadAes256CbcHmac256Algorithm._version, 0, SqlAeadAes256CbcHmac256Algorithm._version.Length, SqlAeadAes256CbcHmac256Algorithm._version, 0);
						hmacsha.TransformBlock(array, 0, array.Length, array, 0);
						hmacsha.TransformBlock(array2, num4, num * 16, array2, num4);
						hmacsha.TransformFinalBlock(SqlAeadAes256CbcHmac256Algorithm._versionSize, 0, SqlAeadAes256CbcHmac256Algorithm._versionSize.Length);
						byte[] hash = hmacsha.Hash;
						Buffer.BlockCopy(hash, 0, array2, 1, num2);
					}
				}
			}
			finally
			{
				this._cryptoProviderPool.Enqueue(aesCryptoServiceProvider);
			}
			return array2;
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x000AAE10 File Offset: 0x000AA210
		internal override byte[] DecryptData(byte[] cipherText)
		{
			return this.DecryptData(cipherText, true);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x000AAE28 File Offset: 0x000AA228
		protected byte[] DecryptData(byte[] cipherText, bool hasAuthenticationTag)
		{
			byte[] array = new byte[16];
			int num = hasAuthenticationTag ? 65 : 33;
			if (cipherText.Length < num)
			{
				throw SQL.InvalidCipherTextSize(cipherText.Length, num);
			}
			int num2 = 0;
			if (cipherText[num2] != this._algorithmVersion)
			{
				throw SQL.InvalidAlgorithmVersion(cipherText[num2], this._algorithmVersion);
			}
			num2++;
			int buffer2Index = 0;
			if (hasAuthenticationTag)
			{
				buffer2Index = num2;
				num2 += 32;
			}
			Buffer.BlockCopy(cipherText, num2, array, 0, array.Length);
			num2 += array.Length;
			int offset = num2;
			int num3 = cipherText.Length - num2;
			if (hasAuthenticationTag)
			{
				byte[] array2 = this.PrepareAuthenticationTag(array, cipherText, offset, num3);
				if (!SqlSecurityUtility.CompareBytes(array2, cipherText, buffer2Index, array2.Length))
				{
					throw SQL.InvalidAuthenticationTag();
				}
			}
			return this.DecryptData(array, cipherText, offset, num3);
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x000AAED4 File Offset: 0x000AA2D4
		private byte[] DecryptData(byte[] iv, byte[] cipherText, int offset, int count)
		{
			AesCryptoServiceProvider aesCryptoServiceProvider;
			if (!this._cryptoProviderPool.TryDequeue(out aesCryptoServiceProvider))
			{
				aesCryptoServiceProvider = new AesCryptoServiceProvider();
				try
				{
					aesCryptoServiceProvider.Key = this._columnEncryptionKey.EncryptionKey;
					aesCryptoServiceProvider.Mode = CipherMode.CBC;
					aesCryptoServiceProvider.Padding = PaddingMode.PKCS7;
				}
				catch (Exception)
				{
					if (aesCryptoServiceProvider != null)
					{
						aesCryptoServiceProvider.Dispose();
					}
					throw;
				}
			}
			byte[] result;
			try
			{
				aesCryptoServiceProvider.IV = iv;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (ICryptoTransform cryptoTransform = aesCryptoServiceProvider.CreateDecryptor())
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
						{
							cryptoStream.Write(cipherText, offset, count);
							cryptoStream.FlushFinalBlock();
							result = memoryStream.ToArray();
						}
					}
				}
			}
			finally
			{
				this._cryptoProviderPool.Enqueue(aesCryptoServiceProvider);
			}
			return result;
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x000AB00C File Offset: 0x000AA40C
		private byte[] PrepareAuthenticationTag(byte[] iv, byte[] cipherText, int offset, int length)
		{
			byte[] array = new byte[32];
			byte[] hash;
			using (HMACSHA256 hmacsha = new HMACSHA256(this._columnEncryptionKey.MACKey))
			{
				int num = hmacsha.TransformBlock(SqlAeadAes256CbcHmac256Algorithm._version, 0, SqlAeadAes256CbcHmac256Algorithm._version.Length, SqlAeadAes256CbcHmac256Algorithm._version, 0);
				num = hmacsha.TransformBlock(iv, 0, iv.Length, iv, 0);
				num = hmacsha.TransformBlock(cipherText, offset, length, cipherText, offset);
				hmacsha.TransformFinalBlock(SqlAeadAes256CbcHmac256Algorithm._versionSize, 0, SqlAeadAes256CbcHmac256Algorithm._versionSize.Length);
				hash = hmacsha.Hash;
			}
			Buffer.BlockCopy(hash, 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x04000E7A RID: 3706
		internal const string AlgorithmName = "AEAD_AES_256_CBC_HMAC_SHA256";

		// Token: 0x04000E7B RID: 3707
		private const int _KeySizeInBytes = 32;

		// Token: 0x04000E7C RID: 3708
		private const int _BlockSizeInBytes = 16;

		// Token: 0x04000E7D RID: 3709
		private const int _MinimumCipherTextLengthInBytesNoAuthenticationTag = 33;

		// Token: 0x04000E7E RID: 3710
		private const int _MinimumCipherTextLengthInBytesWithAuthenticationTag = 65;

		// Token: 0x04000E7F RID: 3711
		private const CipherMode _cipherMode = CipherMode.CBC;

		// Token: 0x04000E80 RID: 3712
		private const PaddingMode _paddingMode = PaddingMode.PKCS7;

		// Token: 0x04000E81 RID: 3713
		private readonly bool _isDeterministic;

		// Token: 0x04000E82 RID: 3714
		private readonly byte _algorithmVersion;

		// Token: 0x04000E83 RID: 3715
		private readonly SqlAeadAes256CbcHmac256EncryptionKey _columnEncryptionKey;

		// Token: 0x04000E84 RID: 3716
		private readonly ConcurrentQueue<AesCryptoServiceProvider> _cryptoProviderPool;

		// Token: 0x04000E85 RID: 3717
		private static readonly byte[] _version = new byte[]
		{
			1
		};

		// Token: 0x04000E86 RID: 3718
		private static readonly byte[] _versionSize = new byte[]
		{
			1
		};
	}
}

using System;
using System.IO;
using System.Security.Cryptography;

namespace System.Web.Security.Cryptography
{
	// Token: 0x02000609 RID: 1545
	internal sealed class NetFXCryptoService : ICryptoService
	{
		// Token: 0x06004DB2 RID: 19890 RVA: 0x0010D9DC File Offset: 0x0010BBDC
		public NetFXCryptoService(ICryptoAlgorithmFactory cryptoAlgorithmFactory, CryptographicKey encryptionKey, CryptographicKey validationKey, bool predictableIV = false)
		{
			this._cryptoAlgorithmFactory = cryptoAlgorithmFactory;
			this._encryptionKey = encryptionKey;
			this._validationKey = validationKey;
			this._predictableIV = predictableIV;
		}

		// Token: 0x06004DB3 RID: 19891 RVA: 0x0010DA04 File Offset: 0x0010BC04
		public byte[] Protect(byte[] clearData)
		{
			byte[] result;
			using (SymmetricAlgorithm encryptionAlgorithm = this._cryptoAlgorithmFactory.GetEncryptionAlgorithm())
			{
				encryptionAlgorithm.Key = this._encryptionKey.GetKeyMaterial();
				if (this._predictableIV)
				{
					encryptionAlgorithm.IV = CryptoUtil.CreatePredictableIV(clearData, encryptionAlgorithm.BlockSize);
				}
				else
				{
					encryptionAlgorithm.GenerateIV();
				}
				byte[] iv = encryptionAlgorithm.IV;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					memoryStream.Write(iv, 0, iv.Length);
					using (ICryptoTransform cryptoTransform = encryptionAlgorithm.CreateEncryptor())
					{
						using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
						{
							cryptoStream.Write(clearData, 0, clearData.Length);
							cryptoStream.FlushFinalBlock();
							using (KeyedHashAlgorithm validationAlgorithm = this._cryptoAlgorithmFactory.GetValidationAlgorithm())
							{
								validationAlgorithm.Key = this._validationKey.GetKeyMaterial();
								byte[] array = validationAlgorithm.ComputeHash(memoryStream.GetBuffer(), 0, checked((int)memoryStream.Length));
								memoryStream.Write(array, 0, array.Length);
								byte[] array2 = memoryStream.ToArray();
								result = array2;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06004DB4 RID: 19892 RVA: 0x0010DB58 File Offset: 0x0010BD58
		public byte[] Unprotect(byte[] protectedData)
		{
			checked
			{
				byte[] result;
				using (SymmetricAlgorithm encryptionAlgorithm = this._cryptoAlgorithmFactory.GetEncryptionAlgorithm())
				{
					encryptionAlgorithm.Key = this._encryptionKey.GetKeyMaterial();
					using (KeyedHashAlgorithm validationAlgorithm = this._cryptoAlgorithmFactory.GetValidationAlgorithm())
					{
						validationAlgorithm.Key = this._validationKey.GetKeyMaterial();
						int num = encryptionAlgorithm.BlockSize / 8;
						int num2 = validationAlgorithm.HashSize / 8;
						int num3 = protectedData.Length - num - num2;
						if (num3 <= 0)
						{
							result = null;
						}
						else
						{
							byte[] array = validationAlgorithm.ComputeHash(protectedData, 0, num + num3);
							if (!CryptoUtil.BuffersAreEqual(protectedData, num + num3, num2, array, 0, array.Length))
							{
								result = null;
							}
							else
							{
								byte[] array2 = new byte[num];
								Buffer.BlockCopy(protectedData, 0, array2, 0, array2.Length);
								encryptionAlgorithm.IV = array2;
								using (MemoryStream memoryStream = new MemoryStream())
								{
									using (ICryptoTransform cryptoTransform = encryptionAlgorithm.CreateDecryptor())
									{
										using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
										{
											cryptoStream.Write(protectedData, num, num3);
											cryptoStream.FlushFinalBlock();
											byte[] array3 = memoryStream.ToArray();
											result = array3;
										}
									}
								}
							}
						}
					}
				}
				return result;
			}
		}

		// Token: 0x0400296B RID: 10603
		private readonly ICryptoAlgorithmFactory _cryptoAlgorithmFactory;

		// Token: 0x0400296C RID: 10604
		private readonly CryptographicKey _encryptionKey;

		// Token: 0x0400296D RID: 10605
		private readonly bool _predictableIV;

		// Token: 0x0400296E RID: 10606
		private readonly CryptographicKey _validationKey;
	}
}

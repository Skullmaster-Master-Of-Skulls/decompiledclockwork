using System;
using System.Security.Cryptography;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x02000190 RID: 400
	public class SqlColumnEncryptionCngProvider : SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x060017E6 RID: 6118 RVA: 0x000AA138 File Offset: 0x000A9538
		public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
		{
			this.ValidateNonEmptyKeyPath(masterKeyPath, true);
			if (encryptedColumnEncryptionKey == null)
			{
				throw SQL.NullEncryptedColumnEncryptionKey();
			}
			if (encryptedColumnEncryptionKey.Length == 0)
			{
				throw SQL.EmptyEncryptedColumnEncryptionKey();
			}
			this.ValidateEncryptionAlgorithm(encryptionAlgorithm, true);
			RSACng rsaCngProvider = this.CreateRSACngProvider(masterKeyPath, true);
			int keySize = this.GetKeySize(rsaCngProvider);
			if (encryptedColumnEncryptionKey[0] != this._version[0])
			{
				throw SQL.InvalidAlgorithmVersionInEncryptedCEK(encryptedColumnEncryptionKey[0], this._version[0]);
			}
			int num = this._version.Length;
			ushort num2 = BitConverter.ToUInt16(encryptedColumnEncryptionKey, num);
			num += 2;
			ushort num3 = BitConverter.ToUInt16(encryptedColumnEncryptionKey, num);
			num += 2;
			num += (int)num2;
			if ((int)num3 != keySize)
			{
				throw SQL.InvalidCiphertextLengthInEncryptedCEKCng((int)num3, keySize, masterKeyPath);
			}
			int num4 = encryptedColumnEncryptionKey.Length - num - (int)num3;
			if (num4 != keySize)
			{
				throw SQL.InvalidSignatureInEncryptedCEKCng(num4, keySize, masterKeyPath);
			}
			byte[] array = new byte[(int)num3];
			Buffer.BlockCopy(encryptedColumnEncryptionKey, num, array, 0, array.Length);
			num += (int)num3;
			byte[] array2 = new byte[num4];
			Buffer.BlockCopy(encryptedColumnEncryptionKey, num, array2, 0, array2.Length);
			byte[] hash;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				sha256Cng.TransformFinalBlock(encryptedColumnEncryptionKey, 0, encryptedColumnEncryptionKey.Length - array2.Length);
				hash = sha256Cng.Hash;
			}
			if (!this.RSAVerifySignature(hash, array2, rsaCngProvider))
			{
				throw SQL.InvalidSignature(masterKeyPath);
			}
			return this.RSADecrypt(rsaCngProvider, array);
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x000AA27C File Offset: 0x000A967C
		public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
		{
			this.ValidateNonEmptyKeyPath(masterKeyPath, false);
			if (columnEncryptionKey == null)
			{
				throw SQL.NullColumnEncryptionKey();
			}
			if (columnEncryptionKey.Length == 0)
			{
				throw SQL.EmptyColumnEncryptionKey();
			}
			this.ValidateEncryptionAlgorithm(encryptionAlgorithm, false);
			RSACng rsaCngProvider = this.CreateRSACngProvider(masterKeyPath, false);
			int keySize = this.GetKeySize(rsaCngProvider);
			byte[] array = new byte[]
			{
				this._version[0]
			};
			byte[] bytes = Encoding.Unicode.GetBytes(masterKeyPath.ToLowerInvariant());
			byte[] bytes2 = BitConverter.GetBytes((short)bytes.Length);
			byte[] array2 = this.RSAEncrypt(rsaCngProvider, columnEncryptionKey);
			byte[] bytes3 = BitConverter.GetBytes((short)array2.Length);
			byte[] hash;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				sha256Cng.TransformBlock(array, 0, array.Length, array, 0);
				sha256Cng.TransformBlock(bytes2, 0, bytes2.Length, bytes2, 0);
				sha256Cng.TransformBlock(bytes3, 0, bytes3.Length, bytes3, 0);
				sha256Cng.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				sha256Cng.TransformFinalBlock(array2, 0, array2.Length);
				hash = sha256Cng.Hash;
			}
			byte[] array3 = this.RSASignHashedData(hash, rsaCngProvider);
			int num = array.Length + bytes3.Length + bytes2.Length + array2.Length + bytes.Length + array3.Length;
			byte[] array4 = new byte[num];
			int num2 = 0;
			Buffer.BlockCopy(array, 0, array4, num2, array.Length);
			num2 += array.Length;
			Buffer.BlockCopy(bytes2, 0, array4, num2, bytes2.Length);
			num2 += bytes2.Length;
			Buffer.BlockCopy(bytes3, 0, array4, num2, bytes3.Length);
			num2 += bytes3.Length;
			Buffer.BlockCopy(bytes, 0, array4, num2, bytes.Length);
			num2 += bytes.Length;
			Buffer.BlockCopy(array2, 0, array4, num2, array2.Length);
			num2 += array2.Length;
			Buffer.BlockCopy(array3, 0, array4, num2, array3.Length);
			return array4;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x000AA434 File Offset: 0x000A9834
		public override byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x000AA448 File Offset: 0x000A9848
		public override bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x000AA45C File Offset: 0x000A985C
		private void ValidateEncryptionAlgorithm(string encryptionAlgorithm, bool isSystemOp)
		{
			if (encryptionAlgorithm == null)
			{
				throw SQL.NullKeyEncryptionAlgorithm(isSystemOp);
			}
			if (!string.Equals(encryptionAlgorithm, "RSA_OAEP", StringComparison.OrdinalIgnoreCase))
			{
				throw SQL.InvalidKeyEncryptionAlgorithm(encryptionAlgorithm, "RSA_OAEP", isSystemOp);
			}
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x000AA490 File Offset: 0x000A9890
		private void ValidateNonEmptyKeyPath(string masterKeyPath, bool isSystemOp)
		{
			if (!string.IsNullOrWhiteSpace(masterKeyPath))
			{
				return;
			}
			if (masterKeyPath == null)
			{
				throw SQL.NullCngKeyPath(isSystemOp);
			}
			throw SQL.InvalidCngPath(masterKeyPath, isSystemOp);
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x000AA4B8 File Offset: 0x000A98B8
		private byte[] RSAEncrypt(RSACng rsaCngProvider, byte[] columnEncryptionKey)
		{
			return rsaCngProvider.Encrypt(columnEncryptionKey, RSAEncryptionPadding.OaepSHA1);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x000AA4D4 File Offset: 0x000A98D4
		private byte[] RSADecrypt(RSACng rsaCngProvider, byte[] encryptedColumnEncryptionKey)
		{
			return rsaCngProvider.Decrypt(encryptedColumnEncryptionKey, RSAEncryptionPadding.OaepSHA1);
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x000AA4F0 File Offset: 0x000A98F0
		private byte[] RSASignHashedData(byte[] dataToSign, RSACng rsaCngProvider)
		{
			return rsaCngProvider.SignData(dataToSign, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x000AA510 File Offset: 0x000A9910
		private bool RSAVerifySignature(byte[] dataToVerify, byte[] signature, RSACng rsaCngProvider)
		{
			return rsaCngProvider.VerifyData(dataToVerify, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x000AA530 File Offset: 0x000A9930
		private int GetKeySize(RSACng rsaCngProvider)
		{
			return rsaCngProvider.KeySize / 8;
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x000AA548 File Offset: 0x000A9948
		private RSACng CreateRSACngProvider(string keyPath, bool isSystemOp)
		{
			string text;
			string text2;
			this.GetCngProviderAndKeyId(keyPath, isSystemOp, out text, out text2);
			CngProvider provider = new CngProvider(text);
			CngKey key;
			try
			{
				key = CngKey.Open(text2, provider);
			}
			catch (CryptographicException)
			{
				throw SQL.InvalidCngKey(keyPath, text, text2, isSystemOp);
			}
			return new RSACng(key);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x000AA5A4 File Offset: 0x000A99A4
		private void GetCngProviderAndKeyId(string keyPath, bool isSystemOp, out string cngProvider, out string keyIdentifier)
		{
			int num = keyPath.IndexOf("/");
			if (num == -1)
			{
				throw SQL.InvalidCngPath(keyPath, isSystemOp);
			}
			cngProvider = keyPath.Substring(0, num);
			keyIdentifier = keyPath.Substring(num + 1, keyPath.Length - (num + 1));
			if (cngProvider.Length == 0)
			{
				throw SQL.EmptyCngName(keyPath, isSystemOp);
			}
			if (keyIdentifier.Length == 0)
			{
				throw SQL.EmptyCngKeyId(keyPath, isSystemOp);
			}
		}

		// Token: 0x04000E73 RID: 3699
		public const string ProviderName = "MSSQL_CNG_STORE";

		// Token: 0x04000E74 RID: 3700
		private const string RSAEncryptionAlgorithmWithOAEP = "RSA_OAEP";

		// Token: 0x04000E75 RID: 3701
		private readonly byte[] _version = new byte[]
		{
			1
		};
	}
}

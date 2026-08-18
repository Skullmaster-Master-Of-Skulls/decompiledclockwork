using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace System.Data.SqlClient
{
	// Token: 0x02000191 RID: 401
	public class SqlColumnEncryptionCspProvider : SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x060017F4 RID: 6132 RVA: 0x000AA630 File Offset: 0x000A9A30
		public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
		{
			this.ValidateNonEmptyCSPKeyPath(masterKeyPath, true);
			if (encryptedColumnEncryptionKey == null)
			{
				throw SQL.NullEncryptedColumnEncryptionKey();
			}
			if (encryptedColumnEncryptionKey.Length == 0)
			{
				throw SQL.EmptyEncryptedColumnEncryptionKey();
			}
			this.ValidateEncryptionAlgorithm(encryptionAlgorithm, true);
			RSACryptoServiceProvider rscp = this.CreateRSACryptoProvider(masterKeyPath, true);
			int keySize = this.GetKeySize(rscp);
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
				throw SQL.InvalidCiphertextLengthInEncryptedCEKCsp((int)num3, keySize, masterKeyPath);
			}
			int num4 = encryptedColumnEncryptionKey.Length - num - (int)num3;
			if (num4 != keySize)
			{
				throw SQL.InvalidSignatureInEncryptedCEKCsp(num4, keySize, masterKeyPath);
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
			if (!this.RSAVerifySignature(hash, array2, rscp))
			{
				throw SQL.InvalidSignature(masterKeyPath);
			}
			return this.RSADecrypt(rscp, array);
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x000AA774 File Offset: 0x000A9B74
		public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
		{
			this.ValidateNonEmptyCSPKeyPath(masterKeyPath, false);
			if (columnEncryptionKey == null)
			{
				throw SQL.NullColumnEncryptionKey();
			}
			if (columnEncryptionKey.Length == 0)
			{
				throw SQL.EmptyColumnEncryptionKey();
			}
			this.ValidateEncryptionAlgorithm(encryptionAlgorithm, false);
			RSACryptoServiceProvider rscp = this.CreateRSACryptoProvider(masterKeyPath, false);
			int keySize = this.GetKeySize(rscp);
			byte[] array = new byte[]
			{
				this._version[0]
			};
			byte[] bytes = Encoding.Unicode.GetBytes(masterKeyPath.ToLowerInvariant());
			byte[] bytes2 = BitConverter.GetBytes((short)bytes.Length);
			byte[] array2 = this.RSAEncrypt(rscp, columnEncryptionKey);
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
			byte[] array3 = this.RSASignHashedData(hash, rscp);
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

		// Token: 0x060017F6 RID: 6134 RVA: 0x000AA92C File Offset: 0x000A9D2C
		public override byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x000AA940 File Offset: 0x000A9D40
		public override bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x000AA954 File Offset: 0x000A9D54
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

		// Token: 0x060017F9 RID: 6137 RVA: 0x000AA988 File Offset: 0x000A9D88
		private void ValidateNonEmptyCSPKeyPath(string masterKeyPath, bool isSystemOp)
		{
			if (!string.IsNullOrWhiteSpace(masterKeyPath))
			{
				return;
			}
			if (masterKeyPath == null)
			{
				throw SQL.NullCspKeyPath(isSystemOp);
			}
			throw SQL.InvalidCspPath(masterKeyPath, isSystemOp);
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x000AA9B0 File Offset: 0x000A9DB0
		private byte[] RSAEncrypt(RSACryptoServiceProvider rscp, byte[] columnEncryptionKey)
		{
			return rscp.Encrypt(columnEncryptionKey, true);
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x000AA9C8 File Offset: 0x000A9DC8
		private byte[] RSADecrypt(RSACryptoServiceProvider rscp, byte[] encryptedColumnEncryptionKey)
		{
			return rscp.Decrypt(encryptedColumnEncryptionKey, true);
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x000AA9E0 File Offset: 0x000A9DE0
		private byte[] RSASignHashedData(byte[] dataToSign, RSACryptoServiceProvider rscp)
		{
			return rscp.SignData(dataToSign, "SHA256");
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x000AA9FC File Offset: 0x000A9DFC
		private bool RSAVerifySignature(byte[] dataToVerify, byte[] signature, RSACryptoServiceProvider rscp)
		{
			return rscp.VerifyData(dataToVerify, "SHA256", signature);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x000AAA18 File Offset: 0x000A9E18
		private int GetKeySize(RSACryptoServiceProvider rscp)
		{
			return rscp.KeySize / 8;
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x000AAA30 File Offset: 0x000A9E30
		private RSACryptoServiceProvider CreateRSACryptoProvider(string keyPath, bool isSystemOp)
		{
			string text;
			string text2;
			this.GetCspProviderAndKeyName(keyPath, isSystemOp, out text, out text2);
			int providerType = this.GetProviderType(text, keyPath, isSystemOp);
			CspParameters cspParameters = new CspParameters(providerType, text, text2);
			cspParameters.Flags = CspProviderFlags.UseExistingKey;
			RSACryptoServiceProvider result = null;
			try
			{
				result = new RSACryptoServiceProvider(cspParameters);
			}
			catch (CryptographicException ex)
			{
				if (ex.HResult == -2146893802)
				{
					throw SQL.InvalidCspKeyIdentifier(text2, keyPath, isSystemOp);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x000AAAAC File Offset: 0x000A9EAC
		private void GetCspProviderAndKeyName(string keyPath, bool isSystemOp, out string cspProviderName, out string keyIdentifier)
		{
			int num = keyPath.IndexOf("/");
			if (num == -1)
			{
				throw SQL.InvalidCspPath(keyPath, isSystemOp);
			}
			cspProviderName = keyPath.Substring(0, num);
			keyIdentifier = keyPath.Substring(num + 1, keyPath.Length - (num + 1));
			if (cspProviderName.Length == 0)
			{
				throw SQL.EmptyCspName(keyPath, isSystemOp);
			}
			if (keyIdentifier.Length == 0)
			{
				throw SQL.EmptyCspKeyId(keyPath, isSystemOp);
			}
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x000AAB14 File Offset: 0x000A9F14
		private int GetProviderType(string providerName, string keyPath, bool isSystemOp)
		{
			string name = string.Format("SOFTWARE\\Microsoft\\Cryptography\\Defaults\\Provider\\{0}", providerName);
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name);
			if (registryKey == null)
			{
				throw SQL.InvalidCspName(providerName, keyPath, isSystemOp);
			}
			int result = (int)registryKey.GetValue("Type");
			registryKey.Close();
			return result;
		}

		// Token: 0x04000E76 RID: 3702
		public const string ProviderName = "MSSQL_CSP_PROVIDER";

		// Token: 0x04000E77 RID: 3703
		private const string RSAEncryptionAlgorithmWithOAEP = "RSA_OAEP";

		// Token: 0x04000E78 RID: 3704
		private const string HashingAlgorithm = "SHA256";

		// Token: 0x04000E79 RID: 3705
		private readonly byte[] _version = new byte[]
		{
			1
		};
	}
}

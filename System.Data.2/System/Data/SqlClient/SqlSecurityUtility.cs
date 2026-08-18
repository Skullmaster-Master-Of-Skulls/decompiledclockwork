using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x0200019B RID: 411
	internal static class SqlSecurityUtility
	{
		// Token: 0x06001820 RID: 6176 RVA: 0x000AB50C File Offset: 0x000AA90C
		internal static void GetHMACWithSHA256(byte[] plainText, byte[] key, byte[] hash)
		{
			using (HMACSHA256 hmacsha = new HMACSHA256(key))
			{
				byte[] src = hmacsha.ComputeHash(plainText);
				Buffer.BlockCopy(src, 0, hash, 0, hash.Length);
			}
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x000AB55C File Offset: 0x000AA95C
		internal static string GetSHA256Hash(byte[] input)
		{
			string hexString;
			using (SHA256 sha = SHA256.Create())
			{
				byte[] input2 = sha.ComputeHash(input);
				hexString = SqlSecurityUtility.GetHexString(input2);
			}
			return hexString;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000AB5A8 File Offset: 0x000AA9A8
		internal static void GenerateRandomBytes(byte[] randomBytes)
		{
			RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
			rngcryptoServiceProvider.GetBytes(randomBytes);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x000AB5C4 File Offset: 0x000AA9C4
		internal static bool CompareBytes(byte[] buffer1, byte[] buffer2, int buffer2Index, int lengthToCompare)
		{
			if (buffer1 == null || buffer2 == null)
			{
				return false;
			}
			if (buffer2.Length - buffer2Index < lengthToCompare)
			{
				return false;
			}
			int num = 0;
			while (num < buffer1.Length && num < lengthToCompare)
			{
				if (buffer1[num] != buffer2[buffer2Index + num])
				{
					return false;
				}
				num++;
			}
			return true;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x000AB604 File Offset: 0x000AAA04
		internal static string GetHexString(byte[] input)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in input)
			{
				stringBuilder.AppendFormat(b.ToString("X2"), new object[0]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x000AB64C File Offset: 0x000AAA4C
		internal static string GetCurrentFunctionName()
		{
			StackTrace stackTrace = new StackTrace();
			StackFrame frame = stackTrace.GetFrame(1);
			MethodBase method = frame.GetMethod();
			return string.Format("{0}.{1}", method.DeclaringType.Name, method.Name);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x000AB68C File Offset: 0x000AAA8C
		private static string ValidateAndGetEncryptionAlgorithmName(byte cipherAlgorithmId, string cipherAlgorithmName)
		{
			if (cipherAlgorithmId == 0)
			{
				if (cipherAlgorithmName == null)
				{
					throw SQL.NullColumnEncryptionAlgorithm(SqlClientEncryptionAlgorithmFactoryList.GetInstance().GetRegisteredCipherAlgorithmNames());
				}
				return cipherAlgorithmName;
			}
			else
			{
				if (2 == cipherAlgorithmId)
				{
					return "AEAD_AES_256_CBC_HMAC_SHA256";
				}
				if (1 == cipherAlgorithmId)
				{
					return "AES_256_CBC";
				}
				throw SQL.UnknownColumnEncryptionAlgorithmId((int)cipherAlgorithmId, SqlSecurityUtility.GetRegisteredCipherAlgorithmIds());
			}
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x000AB6D0 File Offset: 0x000AAAD0
		private static string GetRegisteredCipherAlgorithmIds()
		{
			return "'1', '2'";
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x000AB6E4 File Offset: 0x000AAAE4
		internal static byte[] EncryptWithKey(byte[] plainText, SqlCipherMetadata md, string serverName)
		{
			if (!md.IsAlgorithmInitialized())
			{
				SqlSecurityUtility.DecryptSymmetricKey(md, serverName);
			}
			byte[] array = md.CipherAlgorithm.EncryptData(plainText);
			if (array == null || array.Length == 0)
			{
				SQL.NullCipherText();
			}
			return array;
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x000AB71C File Offset: 0x000AAB1C
		internal static string GetBytesAsString(byte[] buff, bool fLast, int countOfBytes)
		{
			int num = (buff.Length > countOfBytes) ? countOfBytes : buff.Length;
			int startIndex = 0;
			if (fLast)
			{
				startIndex = buff.Length - num;
			}
			return BitConverter.ToString(buff, startIndex, num);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x000AB74C File Offset: 0x000AAB4C
		internal static byte[] DecryptWithKey(byte[] cipherText, SqlCipherMetadata md, string serverName)
		{
			if (!md.IsAlgorithmInitialized())
			{
				SqlSecurityUtility.DecryptSymmetricKey(md, serverName);
			}
			byte[] result;
			try
			{
				byte[] array = md.CipherAlgorithm.DecryptData(cipherText);
				if (array == null)
				{
					throw SQL.NullPlainText();
				}
				result = array;
			}
			catch (Exception e)
			{
				string bytesAsString = SqlSecurityUtility.GetBytesAsString(md.EncryptionKeyInfo.Value.encryptedKey, true, 10);
				string bytesAsString2 = SqlSecurityUtility.GetBytesAsString(cipherText, false, 10);
				throw SQL.ThrowDecryptionFailed(bytesAsString, bytesAsString2, e);
			}
			return result;
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x000AB7D8 File Offset: 0x000AABD8
		internal static void DecryptSymmetricKey(SqlCipherMetadata md, string serverName)
		{
			SqlClientSymmetricKey key = null;
			SqlEncryptionKeyInfo? encryptionKeyInfo = null;
			SqlSecurityUtility.DecryptSymmetricKey(md.EncryptionInfo, serverName, out key, out encryptionKeyInfo);
			md.CipherAlgorithm = null;
			SqlClientEncryptionAlgorithm cipherAlgorithm = null;
			string algorithmName = SqlSecurityUtility.ValidateAndGetEncryptionAlgorithmName(md.CipherAlgorithmId, md.CipherAlgorithmName);
			SqlClientEncryptionAlgorithmFactoryList.GetInstance().GetAlgorithm(key, md.EncryptionType, algorithmName, out cipherAlgorithm);
			md.CipherAlgorithm = cipherAlgorithm;
			md.EncryptionKeyInfo = encryptionKeyInfo;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x000AB83C File Offset: 0x000AAC3C
		internal static void DecryptSymmetricKey(SqlTceCipherInfoEntry? sqlTceCipherInfoEntry, string serverName, out SqlClientSymmetricKey sqlClientSymmetricKey, out SqlEncryptionKeyInfo? encryptionkeyInfoChosen)
		{
			sqlClientSymmetricKey = null;
			encryptionkeyInfoChosen = null;
			Exception ex = null;
			SqlSymmetricKeyCache instance = SqlSymmetricKeyCache.GetInstance();
			foreach (SqlEncryptionKeyInfo sqlEncryptionKeyInfo in sqlTceCipherInfoEntry.Value.ColumnEncryptionKeyValues)
			{
				try
				{
					if (instance.GetKey(sqlEncryptionKeyInfo, serverName, out sqlClientSymmetricKey))
					{
						encryptionkeyInfoChosen = new SqlEncryptionKeyInfo?(sqlEncryptionKeyInfo);
						break;
					}
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
			}
			if (sqlClientSymmetricKey == null)
			{
				throw ex;
			}
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x000AB8F0 File Offset: 0x000AACF0
		internal static int GetBase64LengthFromByteLength(int byteLength)
		{
			return (int)((double)byteLength * 4.0 / 3.0) + 4;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x000AB918 File Offset: 0x000AAD18
		internal static void VerifyColumnMasterKeySignature(string keyStoreName, string keyPath, string serverName, bool isEnclaveEnabled, byte[] CMKSignature)
		{
			bool flag = false;
			try
			{
				if (CMKSignature == null || CMKSignature.Length == 0)
				{
					throw SQL.ColumnMasterKeySignatureNotFound(keyPath);
				}
				IList<string> list;
				if (SqlConnection.ColumnEncryptionTrustedMasterKeyPaths.TryGetValue(serverName, out list) && (list == null || list.Count<string>() == 0 || !list.Any((string s) => s.Equals(keyPath, StringComparison.InvariantCultureIgnoreCase))))
				{
					throw SQL.UntrustedKeyPath(keyPath, serverName);
				}
				SqlColumnEncryptionKeyStoreProvider sqlColumnEncryptionKeyStoreProvider;
				if (!SqlConnection.TryGetColumnEncryptionKeyStoreProvider(keyStoreName, out sqlColumnEncryptionKeyStoreProvider))
				{
					throw SQL.InvalidKeyStoreProviderName(keyStoreName, SqlConnection.GetColumnEncryptionSystemKeyStoreProviders(), SqlConnection.GetColumnEncryptionCustomKeyStoreProviders());
				}
				bool? signatureVerificationResult = SqlSecurityUtility.ColumnMasterKeyMetadataSignatureVerificationCache.GetSignatureVerificationResult(keyStoreName, keyPath, isEnclaveEnabled, CMKSignature);
				if (signatureVerificationResult == null)
				{
					flag = sqlColumnEncryptionKeyStoreProvider.VerifyColumnMasterKeyMetadata(keyPath, isEnclaveEnabled, CMKSignature);
					SqlSecurityUtility.ColumnMasterKeyMetadataSignatureVerificationCache.AddSignatureVerificationResult(keyStoreName, keyPath, isEnclaveEnabled, CMKSignature, flag);
				}
				else
				{
					flag = signatureVerificationResult.Value;
				}
			}
			catch (Exception innerExeption)
			{
				throw SQL.UnableToVerifyColumnMasterKeySignature(innerExeption);
			}
			if (!flag)
			{
				throw SQL.ColumnMasterKeySignatureVerificationFailed(keyPath);
			}
		}

		// Token: 0x04000E97 RID: 3735
		private static readonly ColumnMasterKeyMetadataSignatureVerificationCache ColumnMasterKeyMetadataSignatureVerificationCache = ColumnMasterKeyMetadataSignatureVerificationCache.Instance;
	}
}

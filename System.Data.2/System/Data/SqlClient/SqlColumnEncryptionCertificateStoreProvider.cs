using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x0200018F RID: 399
	public class SqlColumnEncryptionCertificateStoreProvider : SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x060017D5 RID: 6101 RVA: 0x000A99B4 File Offset: 0x000A8DB4
		public override byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey)
		{
			this.ValidateNonEmptyCertificatePath(masterKeyPath, true);
			if (encryptedColumnEncryptionKey == null)
			{
				throw SQL.NullEncryptedColumnEncryptionKey();
			}
			if (encryptedColumnEncryptionKey.Length == 0)
			{
				throw SQL.EmptyEncryptedColumnEncryptionKey();
			}
			this.ValidateEncryptionAlgorithm(encryptionAlgorithm, true);
			this.ValidateCertificatePathLength(masterKeyPath, true);
			X509Certificate2 certificateByPath = this.GetCertificateByPath(masterKeyPath, true);
			int num = certificateByPath.PublicKey.Key.KeySize / 8;
			if (encryptedColumnEncryptionKey[0] != this._version[0])
			{
				throw SQL.InvalidAlgorithmVersionInEncryptedCEK(encryptedColumnEncryptionKey[0], this._version[0]);
			}
			int num2 = this._version.Length;
			short num3 = BitConverter.ToInt16(encryptedColumnEncryptionKey, num2);
			num2 += 2;
			int num4 = (int)BitConverter.ToInt16(encryptedColumnEncryptionKey, num2);
			num2 += 2;
			num2 += (int)num3;
			if (num4 != num)
			{
				throw SQL.InvalidCiphertextLengthInEncryptedCEK(num4, num, masterKeyPath);
			}
			int num5 = encryptedColumnEncryptionKey.Length - num2 - num4;
			if (num5 != num)
			{
				throw SQL.InvalidSignatureInEncryptedCEK(num5, num, masterKeyPath);
			}
			byte[] array = new byte[num4];
			Buffer.BlockCopy(encryptedColumnEncryptionKey, num2, array, 0, array.Length);
			num2 += num4;
			byte[] array2 = new byte[num5];
			Buffer.BlockCopy(encryptedColumnEncryptionKey, num2, array2, 0, array2.Length);
			byte[] hash;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				sha256Cng.TransformFinalBlock(encryptedColumnEncryptionKey, 0, encryptedColumnEncryptionKey.Length - array2.Length);
				hash = sha256Cng.Hash;
			}
			if (!this.RSAVerifySignature(hash, array2, certificateByPath))
			{
				throw SQL.InvalidCertificateSignature(masterKeyPath);
			}
			return this.RSADecrypt(array, certificateByPath);
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x000A9B0C File Offset: 0x000A8F0C
		public override byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey)
		{
			this.ValidateNonEmptyCertificatePath(masterKeyPath, false);
			if (columnEncryptionKey == null)
			{
				throw SQL.NullColumnEncryptionKey();
			}
			if (columnEncryptionKey.Length == 0)
			{
				throw SQL.EmptyColumnEncryptionKey();
			}
			this.ValidateEncryptionAlgorithm(encryptionAlgorithm, false);
			this.ValidateCertificatePathLength(masterKeyPath, false);
			X509Certificate2 certificateByPath = this.GetCertificateByPath(masterKeyPath, false);
			int num = certificateByPath.PublicKey.Key.KeySize / 8;
			byte[] array = new byte[]
			{
				this._version[0]
			};
			byte[] bytes = Encoding.Unicode.GetBytes(masterKeyPath.ToLowerInvariant());
			byte[] bytes2 = BitConverter.GetBytes((short)bytes.Length);
			byte[] array2 = this.RSAEncrypt(columnEncryptionKey, certificateByPath);
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
			byte[] array3 = this.RSASignHashedData(hash, certificateByPath);
			int num2 = array.Length + bytes3.Length + bytes2.Length + array2.Length + bytes.Length + array3.Length;
			byte[] array4 = new byte[num2];
			int num3 = 0;
			Buffer.BlockCopy(array, 0, array4, num3, array.Length);
			num3 += array.Length;
			Buffer.BlockCopy(bytes2, 0, array4, num3, bytes2.Length);
			num3 += bytes2.Length;
			Buffer.BlockCopy(bytes3, 0, array4, num3, bytes3.Length);
			num3 += bytes3.Length;
			Buffer.BlockCopy(bytes, 0, array4, num3, bytes.Length);
			num3 += bytes.Length;
			Buffer.BlockCopy(array2, 0, array4, num3, array2.Length);
			num3 += array2.Length;
			Buffer.BlockCopy(array3, 0, array4, num3, array3.Length);
			return array4;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x000A9CD8 File Offset: 0x000A90D8
		public override byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			byte[] dataToSign = this.ComputeMasterKeyMetadataHash(masterKeyPath, allowEnclaveComputations, false);
			X509Certificate2 certificateByPath = this.GetCertificateByPath(masterKeyPath, false);
			return this.RSASignHashedData(dataToSign, certificateByPath);
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x000A9D04 File Offset: 0x000A9104
		public override bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			byte[] dataToVerify = this.ComputeMasterKeyMetadataHash(masterKeyPath, allowEnclaveComputations, true);
			X509Certificate2 certificateByPath = this.GetCertificateByPath(masterKeyPath, true);
			return this.RSAVerifySignature(dataToVerify, signature, certificateByPath);
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x000A9D30 File Offset: 0x000A9130
		private byte[] ComputeMasterKeyMetadataHash(string masterKeyPath, bool allowEnclaveComputations, bool isSystemOp)
		{
			this.ValidateNonEmptyCertificatePath(masterKeyPath, isSystemOp);
			this.ValidateCertificatePathLength(masterKeyPath, isSystemOp);
			string text = "MSSQL_CERTIFICATE_STORE" + masterKeyPath + allowEnclaveComputations.ToString();
			text = text.ToLowerInvariant();
			byte[] bytes = Encoding.Unicode.GetBytes(text.ToLowerInvariant());
			byte[] hash;
			using (SHA256Cng sha256Cng = new SHA256Cng())
			{
				sha256Cng.TransformFinalBlock(bytes, 0, bytes.Length);
				hash = sha256Cng.Hash;
			}
			return hash;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x000A9DBC File Offset: 0x000A91BC
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

		// Token: 0x060017DB RID: 6107 RVA: 0x000A9DF0 File Offset: 0x000A91F0
		private void ValidateCertificatePathLength(string masterKeyPath, bool isSystemOp)
		{
			if (masterKeyPath.Length >= 32767)
			{
				throw SQL.LargeCertificatePathLength(masterKeyPath.Length, 32767, isSystemOp);
			}
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x000A9E1C File Offset: 0x000A921C
		private string[] GetValidCertificateLocations()
		{
			return new string[]
			{
				"LocalMachine",
				"CurrentUser"
			};
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x000A9E40 File Offset: 0x000A9240
		private void ValidateNonEmptyCertificatePath(string masterKeyPath, bool isSystemOp)
		{
			if (!string.IsNullOrWhiteSpace(masterKeyPath))
			{
				return;
			}
			if (masterKeyPath == null)
			{
				throw SQL.NullCertificatePath(this.GetValidCertificateLocations(), isSystemOp);
			}
			throw SQL.InvalidCertificatePath(masterKeyPath, this.GetValidCertificateLocations(), isSystemOp);
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x000A9E74 File Offset: 0x000A9274
		private X509Certificate2 GetCertificateByPath(string keyPath, bool isSystemOp)
		{
			StoreLocation storeLocation = StoreLocation.LocalMachine;
			StoreName storeName = StoreName.My;
			string[] array = keyPath.Split(new char[]
			{
				'/'
			});
			if (array.Length > 3)
			{
				throw SQL.InvalidCertificatePath(keyPath, this.GetValidCertificateLocations(), isSystemOp);
			}
			if (array.Length > 2)
			{
				if (string.Equals(array[0], "LocalMachine", StringComparison.OrdinalIgnoreCase))
				{
					storeLocation = StoreLocation.LocalMachine;
				}
				else
				{
					if (!string.Equals(array[0], "CurrentUser", StringComparison.OrdinalIgnoreCase))
					{
						throw SQL.InvalidCertificateLocation(array[0], keyPath, this.GetValidCertificateLocations(), isSystemOp);
					}
					storeLocation = StoreLocation.CurrentUser;
				}
			}
			if (array.Length > 1)
			{
				if (!string.Equals(array[array.Length - 2], "My", StringComparison.OrdinalIgnoreCase))
				{
					throw SQL.InvalidCertificateStore(array[array.Length - 2], keyPath, "My", isSystemOp);
				}
				storeName = StoreName.My;
			}
			string text = array[array.Length - 1];
			if (string.IsNullOrEmpty(text))
			{
				throw SQL.EmptyCertificateThumbprint(keyPath, isSystemOp);
			}
			return this.GetCertificate(storeLocation, storeName, keyPath, text, isSystemOp);
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x000A9F40 File Offset: 0x000A9340
		private X509Certificate2 GetCertificate(StoreLocation storeLocation, StoreName storeName, string masterKeyPath, string thumbprint, bool isSystemOp)
		{
			X509Store x509Store = null;
			X509Certificate2 result;
			try
			{
				x509Store = new X509Store(storeName, storeLocation);
				x509Store.Open(OpenFlags.OpenExistingOnly);
				X509Certificate2Collection x509Certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
				if (x509Certificate2Collection == null || x509Certificate2Collection.Count == 0)
				{
					throw SQL.CertificateNotFound(thumbprint, storeName.ToString(), storeLocation.ToString(), isSystemOp);
				}
				X509Certificate2 x509Certificate = x509Certificate2Collection[0];
				if (!x509Certificate.HasPrivateKey)
				{
					throw SQL.CertificateWithNoPrivateKey(masterKeyPath, isSystemOp);
				}
				result = x509Certificate;
			}
			finally
			{
				if (x509Store != null)
				{
					x509Store.Close();
				}
			}
			return result;
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x000A9FE4 File Offset: 0x000A93E4
		private byte[] RSAEncrypt(byte[] plainText, X509Certificate2 certificate)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = (RSACryptoServiceProvider)certificate.PublicKey.Key;
			return rsacryptoServiceProvider.Encrypt(plainText, true);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x000AA00C File Offset: 0x000A940C
		private byte[] RSADecrypt(byte[] cipherText, X509Certificate2 certificate)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = (RSACryptoServiceProvider)certificate.PrivateKey;
			return rsacryptoServiceProvider.Decrypt(cipherText, true);
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x000AA030 File Offset: 0x000A9430
		private byte[] RSASignHashedData(byte[] dataToSign, X509Certificate2 certificate)
		{
			RSACryptoServiceProvider cspfromCertificatePrivateKey = this.GetCSPFromCertificatePrivateKey(certificate);
			RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(cspfromCertificatePrivateKey);
			rsapkcs1SignatureFormatter.SetHashAlgorithm("SHA256");
			return rsapkcs1SignatureFormatter.CreateSignature(dataToSign);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x000AA060 File Offset: 0x000A9460
		private bool RSAVerifySignature(byte[] dataToVerify, byte[] signature, X509Certificate2 certificate)
		{
			RSACryptoServiceProvider cspfromCertificatePrivateKey = this.GetCSPFromCertificatePrivateKey(certificate);
			RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(cspfromCertificatePrivateKey);
			rsapkcs1SignatureDeformatter.SetHashAlgorithm("SHA256");
			return rsapkcs1SignatureDeformatter.VerifySignature(dataToVerify, signature);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x000AA090 File Offset: 0x000A9490
		private RSACryptoServiceProvider GetCSPFromCertificatePrivateKey(X509Certificate2 certificate)
		{
			CspParameters cspParameters = new CspParameters();
			cspParameters = new CspParameters();
			cspParameters.KeyContainerName = ((RSACryptoServiceProvider)certificate.PrivateKey).CspKeyContainerInfo.KeyContainerName;
			cspParameters.ProviderType = 24;
			cspParameters.KeyNumber = (int)((RSACryptoServiceProvider)certificate.PrivateKey).CspKeyContainerInfo.KeyNumber;
			if (((RSACryptoServiceProvider)certificate.PrivateKey).CspKeyContainerInfo.MachineKeyStore)
			{
				cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
			}
			else
			{
				cspParameters.Flags = CspProviderFlags.UseExistingKey;
			}
			return new RSACryptoServiceProvider(cspParameters);
		}

		// Token: 0x04000E6B RID: 3691
		public const string ProviderName = "MSSQL_CERTIFICATE_STORE";

		// Token: 0x04000E6C RID: 3692
		internal const string RSAEncryptionAlgorithmWithOAEP = "RSA_OAEP";

		// Token: 0x04000E6D RID: 3693
		private const string _certLocationLocalMachine = "LocalMachine";

		// Token: 0x04000E6E RID: 3694
		private const string _certLocationCurrentUser = "CurrentUser";

		// Token: 0x04000E6F RID: 3695
		private const string _myCertificateStore = "My";

		// Token: 0x04000E70 RID: 3696
		private const string _certificatePathFormat = "[LocalMachine|CurrentUser]/My/[Thumbprint]";

		// Token: 0x04000E71 RID: 3697
		private const string _hashingAlgorithm = "SHA256";

		// Token: 0x04000E72 RID: 3698
		private readonly byte[] _version = new byte[]
		{
			1
		};
	}
}

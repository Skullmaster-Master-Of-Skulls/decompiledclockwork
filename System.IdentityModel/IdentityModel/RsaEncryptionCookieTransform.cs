using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace System.IdentityModel
{
	// Token: 0x0200006D RID: 109
	public class RsaEncryptionCookieTransform : CookieTransform
	{
		// Token: 0x06000348 RID: 840 RVA: 0x0000C968 File Offset: 0x0000AB68
		public RsaEncryptionCookieTransform(RSA key)
		{
			if (key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
			}
			this._encryptionKey = key;
			this._decryptionKeys.Add(this._encryptionKey);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000C9BC File Offset: 0x0000ABBC
		public RsaEncryptionCookieTransform(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			this._encryptionKey = X509Util.EnsureAndGetPrivateRSAKey(certificate);
			this._decryptionKeys.Add(this._encryptionKey);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000CA15 File Offset: 0x0000AC15
		internal RsaEncryptionCookieTransform()
		{
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000CA33 File Offset: 0x0000AC33
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000CA3B File Offset: 0x0000AC3B
		public virtual RSA EncryptionKey
		{
			get
			{
				return this._encryptionKey;
			}
			set
			{
				this._encryptionKey = value;
				this._decryptionKeys = new List<RSA>(new RSA[]
				{
					this._encryptionKey
				});
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000CA5E File Offset: 0x0000AC5E
		protected virtual ReadOnlyCollection<RSA> DecryptionKeys
		{
			get
			{
				return this._decryptionKeys.AsReadOnly();
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000CA6B File Offset: 0x0000AC6B
		// (set) Token: 0x0600034F RID: 847 RVA: 0x0000CA74 File Offset: 0x0000AC74
		public string HashName
		{
			get
			{
				return this._hashName;
			}
			set
			{
				using (HashAlgorithm hashAlgorithm = CryptoHelper.CreateHashAlgorithm(value))
				{
					if (hashAlgorithm == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID6034", new object[]
						{
							value
						}));
					}
					this._hashName = value;
				}
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000CAD4 File Offset: 0x0000ACD4
		public override byte[] Decode(byte[] encoded)
		{
			if (encoded == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encoded");
			}
			if (encoded.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("encoded", SR.GetString("ID6045"));
			}
			ReadOnlyCollection<RSA> decryptionKeys = this.DecryptionKeys;
			if (decryptionKeys.Count == 0)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID6039"));
			}
			RSA rsa = null;
			byte[] data;
			byte[] array;
			using (HashAlgorithm hashAlgorithm = CryptoHelper.CreateHashAlgorithm(this._hashName))
			{
				int count = hashAlgorithm.HashSize / 8;
				byte[] b;
				using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(encoded)))
				{
					b = binaryReader.ReadBytes(count);
					int num = binaryReader.ReadInt32();
					if (num < 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("ID1006", new object[]
						{
							num
						})));
					}
					if (num > encoded.Length)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("ID1007")));
					}
					data = binaryReader.ReadBytes(num);
					int num2 = binaryReader.ReadInt32();
					if (num2 < 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("ID1008", new object[]
						{
							num2
						})));
					}
					if (num2 > encoded.Length)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("ID1009")));
					}
					array = binaryReader.ReadBytes(num2);
				}
				foreach (RSA rsa2 in decryptionKeys)
				{
					byte[] a = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(rsa2.ToXmlString(false)));
					if (CryptoHelper.IsEqual(a, b))
					{
						rsa = rsa2;
						break;
					}
				}
			}
			if (rsa == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID6040"));
			}
			byte[] array2 = CngLightup.OaepSha1Decrypt(rsa, data);
			byte[] result;
			using (SymmetricAlgorithm symmetricAlgorithm = CryptoHelper.NewDefaultEncryption())
			{
				byte[] array3 = new byte[symmetricAlgorithm.KeySize / 8];
				if (array2.Length < array3.Length)
				{
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID6047", new object[]
					{
						array2.Length,
						array3.Length
					}));
				}
				byte[] array4 = new byte[array2.Length - array3.Length];
				Array.Copy(array2, array3, array3.Length);
				Array.Copy(array2, array3.Length, array4, 0, array4.Length);
				using (ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateDecryptor(array3, array4))
				{
					result = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
				}
			}
			return result;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		public override byte[] Encode(byte[] value)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			if (value.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID6044"));
			}
			RSA encryptionKey = this.EncryptionKey;
			if (encryptionKey == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID6043"));
			}
			byte[] buffer;
			using (HashAlgorithm hashAlgorithm = CryptoHelper.CreateHashAlgorithm(this._hashName))
			{
				buffer = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(encryptionKey.ToXmlString(false)));
			}
			byte[] array;
			byte[] array3;
			using (SymmetricAlgorithm symmetricAlgorithm = CryptoHelper.NewDefaultEncryption())
			{
				symmetricAlgorithm.GenerateIV();
				symmetricAlgorithm.GenerateKey();
				using (ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateEncryptor())
				{
					array = cryptoTransform.TransformFinalBlock(value, 0, value.Length);
				}
				if (!(encryptionKey is RSACryptoServiceProvider))
				{
					throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID6041"));
				}
				byte[] array2 = new byte[symmetricAlgorithm.Key.Length + symmetricAlgorithm.IV.Length];
				Array.Copy(symmetricAlgorithm.Key, array2, symmetricAlgorithm.Key.Length);
				Array.Copy(symmetricAlgorithm.IV, 0, array2, symmetricAlgorithm.Key.Length, symmetricAlgorithm.IV.Length);
				array3 = CngLightup.OaepSha1Encrypt(encryptionKey, array2);
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(buffer);
					binaryWriter.Write(array3.Length);
					binaryWriter.Write(array3);
					binaryWriter.Write(array.Length);
					binaryWriter.Write(array);
					binaryWriter.Flush();
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0400035E RID: 862
		private RSA _encryptionKey;

		// Token: 0x0400035F RID: 863
		private List<RSA> _decryptionKeys = new List<RSA>();

		// Token: 0x04000360 RID: 864
		private string _hashName = "SHA256";
	}
}

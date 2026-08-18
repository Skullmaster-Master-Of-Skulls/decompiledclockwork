using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace System.IdentityModel
{
	// Token: 0x0200006E RID: 110
	public class RsaSignatureCookieTransform : CookieTransform
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0000CFDC File Offset: 0x0000B1DC
		public RsaSignatureCookieTransform(RSA key)
		{
			if (key == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("key");
			}
			this._signingKey = key;
			this._verificationKeys.Add(this._signingKey);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000D030 File Offset: 0x0000B230
		public RsaSignatureCookieTransform(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			this._signingKey = X509Util.EnsureAndGetPrivateRSAKey(certificate);
			this._verificationKeys.Add(this._signingKey);
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000D089 File Offset: 0x0000B289
		// (set) Token: 0x06000355 RID: 853 RVA: 0x0000D094 File Offset: 0x0000B294
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

		// Token: 0x06000356 RID: 854 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		internal RsaSignatureCookieTransform()
		{
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000D112 File Offset: 0x0000B312
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000D11A File Offset: 0x0000B31A
		public virtual RSA SigningKey
		{
			get
			{
				return this._signingKey;
			}
			set
			{
				this._signingKey = value;
				this._verificationKeys = new List<RSA>(new RSA[]
				{
					this._signingKey
				});
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000D13D File Offset: 0x0000B33D
		protected virtual ReadOnlyCollection<RSA> VerificationKeys
		{
			get
			{
				return this._verificationKeys.AsReadOnly();
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000D14C File Offset: 0x0000B34C
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
			ReadOnlyCollection<RSA> verificationKeys = this.VerificationKeys;
			if (verificationKeys.Count == 0)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID6036"));
			}
			int num = 0;
			if (encoded.Length < 4)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("ID1012")));
			}
			int num2 = BitConverter.ToInt32(encoded, num);
			if (num2 < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("ID1005", new object[]
				{
					num2
				})));
			}
			if (num2 >= encoded.Length - 4)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(SR.GetString("ID1013")));
			}
			num += 4;
			byte[] array = new byte[num2];
			Array.Copy(encoded, num, array, 0, array.Length);
			num += array.Length;
			byte[] array2 = new byte[encoded.Length - num];
			Array.Copy(encoded, num, array2, 0, array2.Length);
			bool flag = false;
			try
			{
				using (HashAlgorithm hashAlgorithm = CryptoHelper.CreateHashAlgorithm(this.HashName))
				{
					hashAlgorithm.ComputeHash(array2);
					foreach (RSA rsa in verificationKeys)
					{
						AsymmetricSignatureDeformatter signatureDeformatter = this.GetSignatureDeformatter(rsa);
						if ((this.isSha256() && CryptoHelper.VerifySignatureForSha256(signatureDeformatter, hashAlgorithm, array)) || signatureDeformatter.VerifySignature(hashAlgorithm, array))
						{
							flag = true;
							break;
						}
					}
				}
			}
			catch (CryptographicException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID6035", new object[]
				{
					this.HashName,
					verificationKeys[0].GetType().FullName
				}), innerException));
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("ID1014")));
			}
			return array2;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000D368 File Offset: 0x0000B568
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
			RSA signingKey = this.SigningKey;
			if (signingKey == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID6042"));
			}
			RSACryptoServiceProvider rsacryptoServiceProvider = signingKey as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider == null && LocalAppContextSwitches.DisableCngCertificates)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID6042"));
			}
			if (rsacryptoServiceProvider != null && rsacryptoServiceProvider.PublicOnly)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID6046"));
			}
			byte[] array;
			using (HashAlgorithm hashAlgorithm = CryptoHelper.CreateHashAlgorithm(this.HashName))
			{
				try
				{
					hashAlgorithm.ComputeHash(value);
					AsymmetricSignatureFormatter signatureFormatter = this.GetSignatureFormatter(signingKey);
					if (this.isSha256())
					{
						array = CryptoHelper.CreateSignatureForSha256(signatureFormatter, hashAlgorithm);
					}
					else
					{
						array = signatureFormatter.CreateSignature(hashAlgorithm);
					}
				}
				catch (CryptographicException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("ID6035", new object[]
					{
						this.HashName,
						signingKey.GetType().FullName
					}), innerException));
				}
			}
			byte[] bytes = BitConverter.GetBytes(array.Length);
			int num = 0;
			byte[] array2 = new byte[bytes.Length + array.Length + value.Length];
			Array.Copy(bytes, 0, array2, num, bytes.Length);
			num += bytes.Length;
			Array.Copy(array, 0, array2, num, array.Length);
			num += array.Length;
			Array.Copy(value, 0, array2, num, value.Length);
			return array2;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
		private AsymmetricSignatureFormatter GetSignatureFormatter(RSA rsa)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = rsa as RSACryptoServiceProvider;
			if (this.isSha256() && rsacryptoServiceProvider != null)
			{
				return CryptoHelper.GetSignatureFormatterForSha256(rsacryptoServiceProvider);
			}
			return new RSAPKCS1SignatureFormatter(rsa);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000D524 File Offset: 0x0000B724
		private AsymmetricSignatureDeformatter GetSignatureDeformatter(RSA rsa)
		{
			RSACryptoServiceProvider rsacryptoServiceProvider = rsa as RSACryptoServiceProvider;
			if (this.isSha256() && rsacryptoServiceProvider != null)
			{
				return CryptoHelper.GetSignatureDeFormatterForSha256(rsacryptoServiceProvider);
			}
			return new RSAPKCS1SignatureDeformatter(rsa);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000D550 File Offset: 0x0000B750
		private bool isSha256()
		{
			return StringComparer.OrdinalIgnoreCase.Equals(this.HashName, "SHA256") || StringComparer.OrdinalIgnoreCase.Equals(this.HashName, "SHA-256") || StringComparer.OrdinalIgnoreCase.Equals(this.HashName, "System.Security.Cryptography.SHA256");
		}

		// Token: 0x04000361 RID: 865
		private RSA _signingKey;

		// Token: 0x04000362 RID: 866
		private List<RSA> _verificationKeys = new List<RSA>();

		// Token: 0x04000363 RID: 867
		private string _hashName = "SHA256";
	}
}

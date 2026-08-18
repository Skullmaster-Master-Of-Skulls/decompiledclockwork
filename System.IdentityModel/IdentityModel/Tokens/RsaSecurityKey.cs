using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200012E RID: 302
	public sealed class RsaSecurityKey : AsymmetricSecurityKey
	{
		// Token: 0x06000875 RID: 2165 RVA: 0x00023453 File Offset: 0x00021653
		public RsaSecurityKey(RSA rsa)
		{
			if (rsa == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rsa");
			}
			this.rsa = rsa;
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00023475 File Offset: 0x00021675
		public override int KeySize
		{
			get
			{
				return this.rsa.KeySize;
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00023484 File Offset: 0x00021684
		public override byte[] DecryptKey(string algorithm, byte[] keyData)
		{
			if (algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-1_5")
			{
				return EncryptedXml.DecryptKey(keyData, this.rsa, false);
			}
			if (algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p")
			{
				return EncryptedXml.DecryptKey(keyData, this.rsa, true);
			}
			if (this.IsSupportedAlgorithm(algorithm))
			{
				return EncryptedXml.DecryptKey(keyData, this.rsa, false);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnsupportedAlgorithmForCryptoOperation", new object[]
			{
				algorithm,
				"DecryptKey"
			})));
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0002350C File Offset: 0x0002170C
		public override byte[] EncryptKey(string algorithm, byte[] keyData)
		{
			if (algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-1_5")
			{
				return EncryptedXml.EncryptKey(keyData, this.rsa, false);
			}
			if (algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p")
			{
				return EncryptedXml.EncryptKey(keyData, this.rsa, true);
			}
			if (this.IsSupportedAlgorithm(algorithm))
			{
				return EncryptedXml.EncryptKey(keyData, this.rsa, false);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnsupportedAlgorithmForCryptoOperation", new object[]
			{
				algorithm,
				"EncryptKey"
			})));
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00023593 File Offset: 0x00021793
		public override AsymmetricAlgorithm GetAsymmetricAlgorithm(string algorithm, bool requiresPrivateKey)
		{
			if (requiresPrivateKey && !this.HasPrivateKey())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("NoPrivateKeyAvailable")));
			}
			return this.rsa;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000235C0 File Offset: 0x000217C0
		public override HashAlgorithm GetHashAlgorithmForSignature(string algorithm)
		{
			if (string.IsNullOrEmpty(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(algorithm, SR.GetString("EmptyOrNullArgumentString", new object[]
				{
					"algorithm"
				}));
			}
			object algorithmFromConfig = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			if (algorithmFromConfig != null)
			{
				SignatureDescription signatureDescription = algorithmFromConfig as SignatureDescription;
				if (signatureDescription != null)
				{
					return signatureDescription.CreateDigest();
				}
				HashAlgorithm hashAlgorithm = algorithmFromConfig as HashAlgorithm;
				if (hashAlgorithm != null)
				{
					return hashAlgorithm;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnsupportedCryptoAlgorithm", new object[]
				{
					algorithm
				})));
			}
			else
			{
				if (algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1")
				{
					return CryptoHelper.NewSha1HashAlgorithm();
				}
				if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnsupportedAlgorithmForCryptoOperation", new object[]
					{
						algorithm,
						"GetHashAlgorithmForSignature"
					})));
				}
				return CryptoHelper.NewSha256HashAlgorithm();
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00023698 File Offset: 0x00021898
		public override AsymmetricSignatureDeformatter GetSignatureDeformatter(string algorithm)
		{
			if (string.IsNullOrEmpty(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(algorithm, SR.GetString("EmptyOrNullArgumentString", new object[]
				{
					"algorithm"
				}));
			}
			object algorithmFromConfig = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			if (algorithmFromConfig != null)
			{
				SignatureDescription signatureDescription = algorithmFromConfig as SignatureDescription;
				if (signatureDescription != null)
				{
					return signatureDescription.CreateDeformatter(this.rsa);
				}
				try
				{
					AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = algorithmFromConfig as AsymmetricSignatureDeformatter;
					if (asymmetricSignatureDeformatter != null)
					{
						asymmetricSignatureDeformatter.SetKey(this.rsa);
						return asymmetricSignatureDeformatter;
					}
				}
				catch (InvalidCastException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("AlgorithmAndKeyMisMatch", new object[]
					{
						algorithm
					}), innerException));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnsupportedAlgorithmForCryptoOperation", new object[]
				{
					algorithm,
					"GetSignatureDeformatter"
				})));
			}
			else
			{
				if (algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1" || algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256")
				{
					return new RSAPKCS1SignatureDeformatter(this.rsa);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnsupportedAlgorithmForCryptoOperation", new object[]
				{
					algorithm,
					"GetSignatureDeformatter"
				})));
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x000237CC File Offset: 0x000219CC
		public override AsymmetricSignatureFormatter GetSignatureFormatter(string algorithm)
		{
			if (string.IsNullOrEmpty(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(algorithm, SR.GetString("EmptyOrNullArgumentString", new object[]
				{
					"algorithm"
				}));
			}
			object algorithmFromConfig = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			if (algorithmFromConfig != null)
			{
				SignatureDescription signatureDescription = algorithmFromConfig as SignatureDescription;
				if (signatureDescription != null)
				{
					return signatureDescription.CreateFormatter(this.rsa);
				}
				try
				{
					AsymmetricSignatureFormatter asymmetricSignatureFormatter = algorithmFromConfig as AsymmetricSignatureFormatter;
					if (asymmetricSignatureFormatter != null)
					{
						asymmetricSignatureFormatter.SetKey(this.rsa);
						return asymmetricSignatureFormatter;
					}
				}
				catch (InvalidCastException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("AlgorithmAndKeyMisMatch", new object[]
					{
						algorithm
					}), innerException));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnsupportedAlgorithmForCryptoOperation", new object[]
				{
					algorithm,
					"GetSignatureFormatter"
				})));
			}
			else
			{
				if (algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1" || algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256")
				{
					return new RSAPKCS1SignatureFormatter(this.rsa);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnsupportedAlgorithmForCryptoOperation", new object[]
				{
					algorithm,
					"GetSignatureFormatter"
				})));
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00023900 File Offset: 0x00021B00
		public override bool HasPrivateKey()
		{
			if (this.privateKeyStatus == RsaSecurityKey.PrivateKeyStatus.AvailabilityNotDetermined)
			{
				RSACryptoServiceProvider rsacryptoServiceProvider = this.rsa as RSACryptoServiceProvider;
				if (rsacryptoServiceProvider != null)
				{
					this.privateKeyStatus = (rsacryptoServiceProvider.PublicOnly ? RsaSecurityKey.PrivateKeyStatus.DoesNotHavePrivateKey : RsaSecurityKey.PrivateKeyStatus.HasPrivateKey);
				}
				else
				{
					try
					{
						byte[] rgb = new byte[20];
						this.rsa.DecryptValue(rgb);
						this.privateKeyStatus = RsaSecurityKey.PrivateKeyStatus.HasPrivateKey;
					}
					catch (CryptographicException)
					{
						this.privateKeyStatus = RsaSecurityKey.PrivateKeyStatus.DoesNotHavePrivateKey;
					}
				}
			}
			return this.privateKeyStatus == RsaSecurityKey.PrivateKeyStatus.HasPrivateKey;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0002397C File Offset: 0x00021B7C
		public override bool IsAsymmetricAlgorithm(string algorithm)
		{
			if (string.IsNullOrEmpty(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(algorithm, SR.GetString("EmptyOrNullArgumentString", new object[]
				{
					"algorithm"
				}));
			}
			return CryptoHelper.IsAsymmetricAlgorithm(algorithm);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000239B0 File Offset: 0x00021BB0
		public override bool IsSupportedAlgorithm(string algorithm)
		{
			if (string.IsNullOrEmpty(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(algorithm, SR.GetString("EmptyOrNullArgumentString", new object[]
				{
					"algorithm"
				}));
			}
			object obj = null;
			try
			{
				obj = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			}
			catch (InvalidOperationException)
			{
				obj = null;
			}
			if (obj != null)
			{
				SignatureDescription signatureDescription = obj as SignatureDescription;
				return signatureDescription != null || obj is AsymmetricAlgorithm;
			}
			return algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-1_5" || algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p" || algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1" || algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00023A60 File Offset: 0x00021C60
		public override bool IsSymmetricAlgorithm(string algorithm)
		{
			return CryptoHelper.IsSymmetricAlgorithm(algorithm);
		}

		// Token: 0x04000B1C RID: 2844
		private RsaSecurityKey.PrivateKeyStatus privateKeyStatus;

		// Token: 0x04000B1D RID: 2845
		private readonly RSA rsa;

		// Token: 0x0200025F RID: 607
		private enum PrivateKeyStatus
		{
			// Token: 0x04001021 RID: 4129
			AvailabilityNotDetermined,
			// Token: 0x04001022 RID: 4130
			HasPrivateKey,
			// Token: 0x04001023 RID: 4131
			DoesNotHavePrivateKey
		}
	}
}

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200018E RID: 398
	public class X509AsymmetricSecurityKey : AsymmetricSecurityKey
	{
		// Token: 0x06000CFF RID: 3327 RVA: 0x0003C26C File Offset: 0x0003A46C
		public X509AsymmetricSecurityKey(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("certificate");
			}
			this.certificate = certificate;
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0003C299 File Offset: 0x0003A499
		public override int KeySize
		{
			get
			{
				return this.PublicKey.KeySize;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0003C2A8 File Offset: 0x0003A4A8
		private AsymmetricAlgorithm PrivateKey
		{
			get
			{
				if (!this.privateKeyAvailabilityDetermined)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (LocalAppContextSwitches.DisableCngCertificates)
						{
							this.privateKey = this.certificate.PrivateKey;
						}
						else
						{
							this.privateKey = CngLightup.GetRSAPrivateKey(this.certificate);
							if (this.privateKey != null)
							{
								RSACryptoServiceProvider rsacryptoServiceProvider = this.privateKey as RSACryptoServiceProvider;
								if (rsacryptoServiceProvider != null && rsacryptoServiceProvider.CspKeyContainerInfo.ProviderType == 1)
								{
									CspParameters cspParameters = new CspParameters();
									cspParameters.ProviderType = 24;
									cspParameters.KeyContainerName = rsacryptoServiceProvider.CspKeyContainerInfo.KeyContainerName;
									cspParameters.KeyNumber = (int)rsacryptoServiceProvider.CspKeyContainerInfo.KeyNumber;
									if (rsacryptoServiceProvider.CspKeyContainerInfo.MachineKeyStore)
									{
										cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
									}
									cspParameters.Flags |= CspProviderFlags.UseExistingKey;
									this.privateKey = new RSACryptoServiceProvider(cspParameters);
								}
							}
							else
							{
								this.privateKey = CngLightup.GetDSAPrivateKey(this.certificate);
							}
							if (this.certificate.HasPrivateKey && this.privateKey == null)
							{
								DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("PrivateKeyNotSupported")));
							}
						}
						this.privateKeyAvailabilityDetermined = true;
					}
				}
				return this.privateKey;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0003C3FC File Offset: 0x0003A5FC
		private AsymmetricAlgorithm PublicKey
		{
			get
			{
				if (!this.publicKeyAvailabilityDetermined)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (!this.publicKeyAvailabilityDetermined)
						{
							if (LocalAppContextSwitches.DisableCngCertificates)
							{
								this.publicKey = this.certificate.PublicKey.Key;
							}
							else
							{
								this.publicKey = CngLightup.GetRSAPublicKey(this.certificate);
								if (this.publicKey == null)
								{
									this.publicKey = CngLightup.GetDSAPublicKey(this.certificate);
								}
								if (this.publicKey == null)
								{
									DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("PublicKeyNotSupported")));
								}
							}
							this.publicKeyAvailabilityDetermined = true;
						}
					}
				}
				return this.publicKey;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0003C4C4 File Offset: 0x0003A6C4
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0003C4CC File Offset: 0x0003A6CC
		public override byte[] DecryptKey(string algorithm, byte[] keyData)
		{
			if (this.PrivateKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("MissingPrivateKey")));
			}
			RSA rsa = this.PrivateKey as RSA;
			if (rsa == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("PrivateKeyNotRSA")));
			}
			if (rsa.KeyExchangeAlgorithm == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("PrivateKeyExchangeNotSupported")));
			}
			if (algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-1_5")
			{
				return EncryptedXml.DecryptKey(keyData, rsa, false);
			}
			if (algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p")
			{
				return EncryptedXml.DecryptKey(keyData, rsa, true);
			}
			if (this.IsSupportedAlgorithm(algorithm))
			{
				return EncryptedXml.DecryptKey(keyData, rsa, true);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedCryptoAlgorithm", new object[]
			{
				algorithm
			})));
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0003C5AC File Offset: 0x0003A7AC
		public override byte[] EncryptKey(string algorithm, byte[] keyData)
		{
			RSA rsa = this.PublicKey as RSA;
			if (rsa == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("PublicKeyNotRSA")));
			}
			if (algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-1_5")
			{
				return EncryptedXml.EncryptKey(keyData, rsa, false);
			}
			if (algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p")
			{
				return EncryptedXml.EncryptKey(keyData, rsa, true);
			}
			if (this.IsSupportedAlgorithm(algorithm))
			{
				return EncryptedXml.EncryptKey(keyData, rsa, true);
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedCryptoAlgorithm", new object[]
			{
				algorithm
			})));
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0003C648 File Offset: 0x0003A848
		public override AsymmetricAlgorithm GetAsymmetricAlgorithm(string algorithm, bool privateKey)
		{
			if (privateKey)
			{
				if (this.PrivateKey == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("MissingPrivateKey")));
				}
				if (string.IsNullOrEmpty(algorithm))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(algorithm, SR.GetString("EmptyOrNullArgumentString", new object[]
					{
						"algorithm"
					}));
				}
				if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#dsa-sha1"))
				{
					if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1") && !(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-1_5") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p"))
					{
						if (this.IsSupportedAlgorithm(algorithm))
						{
							return this.PrivateKey;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedCryptoAlgorithm", new object[]
						{
							algorithm
						})));
					}
					else
					{
						if (this.PrivateKey is RSA)
						{
							return this.PrivateKey as RSA;
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("AlgorithmAndPrivateKeyMisMatch")));
					}
				}
				else
				{
					if (this.PrivateKey is DSA)
					{
						return this.PrivateKey as DSA;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("AlgorithmAndPrivateKeyMisMatch")));
				}
			}
			else if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#dsa-sha1"))
			{
				if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1") && !(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-1_5") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedCryptoAlgorithm", new object[]
					{
						algorithm
					})));
				}
				if (this.PublicKey is RSA)
				{
					return this.PublicKey as RSA;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("AlgorithmAndPublicKeyMisMatch")));
			}
			else
			{
				if (this.PublicKey is DSA)
				{
					return this.PublicKey as DSA;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("AlgorithmAndPublicKeyMisMatch")));
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0003C854 File Offset: 0x0003AA54
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
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnsupportedAlgorithmForCryptoOperation", new object[]
				{
					algorithm,
					"CreateDigest"
				})));
			}
			else
			{
				if (algorithm == "http://www.w3.org/2000/09/xmldsig#dsa-sha1" || algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1")
				{
					return CryptoHelper.NewSha1HashAlgorithm();
				}
				if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedCryptoAlgorithm", new object[]
					{
						algorithm
					})));
				}
				return CryptoHelper.NewSha256HashAlgorithm();
			}
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0003C938 File Offset: 0x0003AB38
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
					return signatureDescription.CreateDeformatter(this.PublicKey);
				}
				try
				{
					AsymmetricSignatureDeformatter asymmetricSignatureDeformatter = algorithmFromConfig as AsymmetricSignatureDeformatter;
					if (asymmetricSignatureDeformatter != null)
					{
						asymmetricSignatureDeformatter.SetKey(this.PublicKey);
						return asymmetricSignatureDeformatter;
					}
				}
				catch (InvalidCastException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("AlgorithmAndPublicKeyMisMatch"), innerException));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnsupportedAlgorithmForCryptoOperation", new object[]
				{
					algorithm,
					"GetSignatureDeformatter"
				})));
			}
			else if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#dsa-sha1"))
			{
				if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1") && !(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedCryptoAlgorithm", new object[]
					{
						algorithm
					})));
				}
				RSA rsa = this.PublicKey as RSA;
				if (rsa == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("PublicKeyNotRSA")));
				}
				return new RSAPKCS1SignatureDeformatter(rsa);
			}
			else
			{
				DSA dsa = this.PublicKey as DSA;
				if (dsa == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("PublicKeyNotDSA")));
				}
				return new DSASignatureDeformatter(dsa);
			}
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0003CAC4 File Offset: 0x0003ACC4
		public override AsymmetricSignatureFormatter GetSignatureFormatter(string algorithm)
		{
			if (this.PrivateKey == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("MissingPrivateKey")));
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(algorithm, SR.GetString("EmptyOrNullArgumentString", new object[]
				{
					"algorithm"
				}));
			}
			AsymmetricAlgorithm asymmetricAlgorithm = X509AsymmetricSecurityKey.LevelUpRsa(this.PrivateKey, algorithm);
			object algorithmFromConfig = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			if (algorithmFromConfig != null)
			{
				SignatureDescription signatureDescription = algorithmFromConfig as SignatureDescription;
				if (signatureDescription != null)
				{
					return signatureDescription.CreateFormatter(asymmetricAlgorithm);
				}
				try
				{
					AsymmetricSignatureFormatter asymmetricSignatureFormatter = algorithmFromConfig as AsymmetricSignatureFormatter;
					if (asymmetricSignatureFormatter != null)
					{
						asymmetricSignatureFormatter.SetKey(asymmetricAlgorithm);
						return asymmetricSignatureFormatter;
					}
				}
				catch (InvalidCastException innerException)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("AlgorithmAndPrivateKeyMisMatch"), innerException));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("UnsupportedAlgorithmForCryptoOperation", new object[]
				{
					algorithm,
					"GetSignatureFormatter"
				})));
			}
			else if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#dsa-sha1"))
			{
				if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1"))
				{
					if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("UnsupportedCryptoAlgorithm", new object[]
						{
							algorithm
						})));
					}
					RSA rsa = asymmetricAlgorithm as RSA;
					if (rsa == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("PrivateKeyNotRSA")));
					}
					return new RSAPKCS1SignatureFormatter(rsa);
				}
				else
				{
					RSA rsa2 = this.PrivateKey as RSA;
					if (rsa2 == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("PrivateKeyNotRSA")));
					}
					return new RSAPKCS1SignatureFormatter(rsa2);
				}
			}
			else
			{
				DSA dsa = this.PrivateKey as DSA;
				if (dsa == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("PrivateKeyNotDSA")));
				}
				return new DSASignatureFormatter(dsa);
			}
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0003CCA4 File Offset: 0x0003AEA4
		private static AsymmetricAlgorithm LevelUpRsa(AsymmetricAlgorithm asymmetricAlgorithm, string algorithm)
		{
			if (LocalAppContextSwitches.DisableUpdatingRsaProviderType)
			{
				return asymmetricAlgorithm;
			}
			if (asymmetricAlgorithm == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("asymmetricAlgorithm"));
			}
			if (string.IsNullOrEmpty(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(algorithm, SR.GetString("EmptyOrNullArgumentString", new object[]
				{
					"algorithm"
				}));
			}
			if (!string.Equals(algorithm, "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"))
			{
				return asymmetricAlgorithm;
			}
			RSACryptoServiceProvider rsacryptoServiceProvider = asymmetricAlgorithm as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider == null)
			{
				return asymmetricAlgorithm;
			}
			if ((rsacryptoServiceProvider.CspKeyContainerInfo.ProviderType == 1 || rsacryptoServiceProvider.CspKeyContainerInfo.ProviderType == 12) && !rsacryptoServiceProvider.CspKeyContainerInfo.HardwareDevice)
			{
				CspParameters cspParameters = new CspParameters();
				cspParameters.ProviderType = 24;
				cspParameters.KeyContainerName = rsacryptoServiceProvider.CspKeyContainerInfo.KeyContainerName;
				cspParameters.KeyNumber = (int)rsacryptoServiceProvider.CspKeyContainerInfo.KeyNumber;
				if (rsacryptoServiceProvider.CspKeyContainerInfo.MachineKeyStore)
				{
					cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
				}
				cspParameters.Flags |= CspProviderFlags.UseExistingKey;
				return new RSACryptoServiceProvider(cspParameters);
			}
			return rsacryptoServiceProvider;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0003CD9D File Offset: 0x0003AF9D
		public override bool HasPrivateKey()
		{
			return this.PrivateKey != null;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0002397C File Offset: 0x00021B7C
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

		// Token: 0x06000D0D RID: 3341 RVA: 0x0003CDA8 File Offset: 0x0003AFA8
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
				algorithm = null;
			}
			if (obj != null)
			{
				SignatureDescription signatureDescription = obj as SignatureDescription;
				return signatureDescription != null || obj is AsymmetricAlgorithm;
			}
			if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#dsa-sha1"))
			{
				return (algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1" || algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256" || algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-1_5" || algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p") && this.PublicKey is RSA;
			}
			return this.PublicKey is DSA;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00023A60 File Offset: 0x00021C60
		public override bool IsSymmetricAlgorithm(string algorithm)
		{
			return CryptoHelper.IsSymmetricAlgorithm(algorithm);
		}

		// Token: 0x04000CA5 RID: 3237
		private X509Certificate2 certificate;

		// Token: 0x04000CA6 RID: 3238
		private AsymmetricAlgorithm privateKey;

		// Token: 0x04000CA7 RID: 3239
		private bool privateKeyAvailabilityDetermined;

		// Token: 0x04000CA8 RID: 3240
		private AsymmetricAlgorithm publicKey;

		// Token: 0x04000CA9 RID: 3241
		private bool publicKeyAvailabilityDetermined;

		// Token: 0x04000CAA RID: 3242
		private object thisLock = new object();
	}
}

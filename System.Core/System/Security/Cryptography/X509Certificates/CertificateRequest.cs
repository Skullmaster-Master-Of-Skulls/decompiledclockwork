using System;
using System.Collections.ObjectModel;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000120 RID: 288
	public sealed class CertificateRequest
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x00020564 File Offset: 0x0001E764
		public X500DistinguishedName SubjectName { get; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0002056C File Offset: 0x0001E76C
		public Collection<X509Extension> CertificateExtensions { get; } = new Collection<X509Extension>();

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00020574 File Offset: 0x0001E774
		public PublicKey PublicKey { get; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0002057C File Offset: 0x0001E77C
		public HashAlgorithmName HashAlgorithm { get; }

		// Token: 0x06000948 RID: 2376 RVA: 0x00020584 File Offset: 0x0001E784
		public CertificateRequest(string subjectName, ECDsa key, HashAlgorithmName hashAlgorithm)
		{
			if (subjectName == null)
			{
				throw new ArgumentNullException("subjectName");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(SR.GetString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
			}
			this.SubjectName = new X500DistinguishedName(subjectName);
			this._key = key;
			this._generator = X509SignatureGenerator.CreateForECDsa(key);
			this.PublicKey = this._generator.PublicKey;
			this.HashAlgorithm = hashAlgorithm;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00020618 File Offset: 0x0001E818
		public CertificateRequest(X500DistinguishedName subjectName, ECDsa key, HashAlgorithmName hashAlgorithm)
		{
			if (subjectName == null)
			{
				throw new ArgumentNullException("subjectName");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(SR.GetString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
			}
			this.SubjectName = subjectName;
			this._key = key;
			this._generator = X509SignatureGenerator.CreateForECDsa(key);
			this.PublicKey = this._generator.PublicKey;
			this.HashAlgorithm = hashAlgorithm;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x000206A8 File Offset: 0x0001E8A8
		public CertificateRequest(string subjectName, RSA key, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (subjectName == null)
			{
				throw new ArgumentNullException("subjectName");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(SR.GetString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			this.SubjectName = new X500DistinguishedName(subjectName);
			this._key = key;
			this._generator = X509SignatureGenerator.CreateForRSA(key, padding);
			this._rsaPadding = padding;
			this.PublicKey = this._generator.PublicKey;
			this.HashAlgorithm = hashAlgorithm;
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0002075C File Offset: 0x0001E95C
		public CertificateRequest(X500DistinguishedName subjectName, RSA key, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
		{
			if (subjectName == null)
			{
				throw new ArgumentNullException("subjectName");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(SR.GetString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
			}
			if (padding == null)
			{
				throw new ArgumentNullException("padding");
			}
			this.SubjectName = subjectName;
			this._key = key;
			this._generator = X509SignatureGenerator.CreateForRSA(key, padding);
			this._rsaPadding = padding;
			this.PublicKey = this._generator.PublicKey;
			this.HashAlgorithm = hashAlgorithm;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0002080C File Offset: 0x0001EA0C
		public CertificateRequest(X500DistinguishedName subjectName, PublicKey publicKey, HashAlgorithmName hashAlgorithm)
		{
			if (subjectName == null)
			{
				throw new ArgumentNullException("subjectName");
			}
			if (publicKey == null)
			{
				throw new ArgumentNullException("publicKey");
			}
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(SR.GetString("Cryptography_HashAlgorithmNameNullOrEmpty"), "hashAlgorithm");
			}
			this.SubjectName = subjectName;
			this.PublicKey = publicKey;
			this.HashAlgorithm = hashAlgorithm;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0002087E File Offset: 0x0001EA7E
		public byte[] CreateSigningRequest()
		{
			if (this._generator == null)
			{
				throw new InvalidOperationException(SR.GetString("Cryptography_CertReq_NoKeyProvided"));
			}
			return this.CreateSigningRequest(this._generator);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x000208A4 File Offset: 0x0001EAA4
		public byte[] CreateSigningRequest(X509SignatureGenerator signatureGenerator)
		{
			if (signatureGenerator == null)
			{
				throw new ArgumentNullException("signatureGenerator");
			}
			X501Attribute[] attributes = null;
			if (this.CertificateExtensions.Count > 0)
			{
				attributes = new X501Attribute[]
				{
					new Pkcs9ExtensionRequest(this.CertificateExtensions)
				};
			}
			Pkcs10CertificationRequestInfo pkcs10CertificationRequestInfo = new Pkcs10CertificationRequestInfo(this.SubjectName, this.PublicKey, attributes);
			return pkcs10CertificationRequestInfo.ToPkcs10Request(signatureGenerator, this.HashAlgorithm);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00020904 File Offset: 0x0001EB04
		public X509Certificate2 CreateSelfSigned(DateTimeOffset notBefore, DateTimeOffset notAfter)
		{
			if (notAfter < notBefore)
			{
				throw new ArgumentException(SR.GetString("Cryptography_CertReq_DatesReversed"));
			}
			if (this._key == null)
			{
				throw new InvalidOperationException(SR.GetString("Cryptography_CertReq_NoKeyProvided"));
			}
			byte[] array = new byte[8];
			using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
			{
				randomNumberGenerator.GetBytes(array);
			}
			using (X509Certificate2 x509Certificate = this.Create(this.SubjectName, this._generator, notBefore, notAfter, array))
			{
				RSA rsa = this._key as RSA;
				if (rsa != null)
				{
					return x509Certificate.CopyWithPrivateKey(rsa);
				}
				ECDsa ecdsa = this._key as ECDsa;
				if (ecdsa != null)
				{
					return x509Certificate.CopyWithPrivateKey(ecdsa);
				}
			}
			throw new CryptographicException();
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000209E0 File Offset: 0x0001EBE0
		public X509Certificate2 Create(X509Certificate2 issuerCertificate, DateTimeOffset notBefore, DateTimeOffset notAfter, byte[] serialNumber)
		{
			if (issuerCertificate == null)
			{
				throw new ArgumentNullException("issuerCertificate");
			}
			if (!issuerCertificate.HasPrivateKey)
			{
				throw new ArgumentException(SR.GetString("Cryptography_CertReq_IssuerRequiresPrivateKey"), "issuerCertificate");
			}
			if (notAfter < notBefore)
			{
				throw new ArgumentException(SR.GetString("Cryptography_CertReq_DatesReversed"));
			}
			if (serialNumber == null || serialNumber.Length < 1)
			{
				throw new ArgumentException(SR.GetString("Arg_EmptyOrNullArray"), "serialNumber");
			}
			if (issuerCertificate.PublicKey.Oid.Value != this.PublicKey.Oid.Value)
			{
				throw new ArgumentException(SR.GetString("Cryptography_CertReq_AlgorithmMustMatch", new object[]
				{
					issuerCertificate.PublicKey.Oid.Value,
					this.PublicKey.Oid.Value
				}), "issuerCertificate");
			}
			DateTime localDateTime = notBefore.LocalDateTime;
			if (localDateTime < issuerCertificate.NotBefore)
			{
				throw new ArgumentException(SR.GetString("Cryptography_CertReq_NotBeforeNotNested", new object[]
				{
					localDateTime,
					issuerCertificate.NotBefore
				}), "notBefore");
			}
			DateTime localDateTime2 = notAfter.LocalDateTime;
			long num = localDateTime2.Ticks;
			long num2 = num % 10000000L;
			num -= num2;
			localDateTime2 = new DateTime(num, localDateTime2.Kind);
			if (localDateTime2 > issuerCertificate.NotAfter)
			{
				throw new ArgumentException(SR.GetString("Cryptography_CertReq_NotAfterNotNested", new object[]
				{
					localDateTime2,
					issuerCertificate.NotAfter
				}), "notAfter");
			}
			X509BasicConstraintsExtension x509BasicConstraintsExtension = (X509BasicConstraintsExtension)issuerCertificate.Extensions["2.5.29.19"];
			X509KeyUsageExtension x509KeyUsageExtension = (X509KeyUsageExtension)issuerCertificate.Extensions["2.5.29.15"];
			if (x509BasicConstraintsExtension == null)
			{
				throw new ArgumentException(SR.GetString("Cryptography_CertReq_BasicConstraintsRequired"), "issuerCertificate");
			}
			if (!x509BasicConstraintsExtension.CertificateAuthority)
			{
				throw new ArgumentException(SR.GetString("Cryptography_CertReq_IssuerBasicConstraintsInvalid"), "issuerCertificate");
			}
			if (x509KeyUsageExtension != null && (x509KeyUsageExtension.KeyUsages & X509KeyUsageFlags.KeyCertSign) == X509KeyUsageFlags.None)
			{
				throw new ArgumentException(SR.GetString("Cryptography_CertReq_IssuerKeyUsageInvalid"), "issuerCertificate");
			}
			AsymmetricAlgorithm asymmetricAlgorithm = null;
			string keyAlgorithm = issuerCertificate.GetKeyAlgorithm();
			X509Certificate2 result;
			try
			{
				X509SignatureGenerator generator;
				if (!(keyAlgorithm == "1.2.840.113549.1.1.1"))
				{
					if (!(keyAlgorithm == "1.2.840.10045.2.1"))
					{
						throw new ArgumentException(SR.GetString("Cryptography_UnknownKeyAlgorithm", new object[]
						{
							keyAlgorithm
						}), "issuerCertificate");
					}
					ECDsa ecdsaPrivateKey = issuerCertificate.GetECDsaPrivateKey();
					asymmetricAlgorithm = ecdsaPrivateKey;
					generator = X509SignatureGenerator.CreateForECDsa(ecdsaPrivateKey);
				}
				else
				{
					if (this._rsaPadding == null)
					{
						throw new InvalidOperationException(SR.GetString("Cryptography_CertReq_RSAPaddingRequired"));
					}
					RSA rsaprivateKey = issuerCertificate.GetRSAPrivateKey();
					asymmetricAlgorithm = rsaprivateKey;
					generator = X509SignatureGenerator.CreateForRSA(rsaprivateKey, this._rsaPadding);
				}
				result = this.Create(issuerCertificate.SubjectName, generator, notBefore, notAfter, serialNumber);
			}
			finally
			{
				if (asymmetricAlgorithm != null)
				{
					asymmetricAlgorithm.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00020CC8 File Offset: 0x0001EEC8
		public X509Certificate2 Create(X500DistinguishedName issuerName, X509SignatureGenerator generator, DateTimeOffset notBefore, DateTimeOffset notAfter, byte[] serialNumber)
		{
			if (issuerName == null)
			{
				throw new ArgumentNullException("issuerName");
			}
			if (generator == null)
			{
				throw new ArgumentNullException("generator");
			}
			if (notAfter < notBefore)
			{
				throw new ArgumentException(SR.GetString("Cryptography_CertReq_DatesReversed"));
			}
			if (serialNumber == null || serialNumber.Length < 1)
			{
				throw new ArgumentException(SR.GetString("Arg_EmptyOrNullArray"), "serialNumber");
			}
			TbsCertificate tbsCertificate = new TbsCertificate
			{
				Version = 2,
				SerialNumber = serialNumber,
				Issuer = issuerName,
				PublicKey = this.PublicKey,
				NotBefore = notBefore,
				NotAfter = notAfter,
				Subject = this.SubjectName
			};
			Collection<X509Extension> extensions = tbsCertificate.Extensions;
			foreach (X509Extension item in this.CertificateExtensions)
			{
				extensions.Add(item);
			}
			return new X509Certificate2(tbsCertificate.Sign(generator, this.HashAlgorithm));
		}

		// Token: 0x040006F6 RID: 1782
		private readonly AsymmetricAlgorithm _key;

		// Token: 0x040006F7 RID: 1783
		private readonly X509SignatureGenerator _generator;

		// Token: 0x040006F8 RID: 1784
		private readonly RSASignaturePadding _rsaPadding;
	}
}

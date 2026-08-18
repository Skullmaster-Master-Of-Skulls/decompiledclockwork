using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Security
{
	// Token: 0x0200059F RID: 1439
	public sealed class DotNetUtilities
	{
		// Token: 0x0600316B RID: 12651 RVA: 0x00135005 File Offset: 0x00134005
		private DotNetUtilities()
		{
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x0013500D File Offset: 0x0013400D
		public static System.Security.Cryptography.X509Certificates.X509Certificate ToX509Certificate(X509CertificateStructure x509Struct)
		{
			return new System.Security.Cryptography.X509Certificates.X509Certificate(x509Struct.GetDerEncoded());
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x0013501A File Offset: 0x0013401A
		public static System.Security.Cryptography.X509Certificates.X509Certificate ToX509Certificate(Org.BouncyCastle.X509.X509Certificate x509Cert)
		{
			return new System.Security.Cryptography.X509Certificates.X509Certificate(x509Cert.GetEncoded());
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x00135027 File Offset: 0x00134027
		public static Org.BouncyCastle.X509.X509Certificate FromX509Certificate(System.Security.Cryptography.X509Certificates.X509Certificate x509Cert)
		{
			return new X509CertificateParser().ReadCertificate(x509Cert.GetRawCertData());
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x00135039 File Offset: 0x00134039
		public static AsymmetricCipherKeyPair GetDsaKeyPair(DSA dsa)
		{
			return DotNetUtilities.GetDsaKeyPair(dsa.ExportParameters(true));
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x00135048 File Offset: 0x00134048
		public static AsymmetricCipherKeyPair GetDsaKeyPair(DSAParameters dp)
		{
			DsaValidationParameters parameters = (dp.Seed != null) ? new DsaValidationParameters(dp.Seed, dp.Counter) : null;
			DsaParameters parameters2 = new DsaParameters(new BigInteger(1, dp.P), new BigInteger(1, dp.Q), new BigInteger(1, dp.G), parameters);
			DsaPublicKeyParameters publicParameter = new DsaPublicKeyParameters(new BigInteger(1, dp.Y), parameters2);
			DsaPrivateKeyParameters privateParameter = new DsaPrivateKeyParameters(new BigInteger(1, dp.X), parameters2);
			return new AsymmetricCipherKeyPair(publicParameter, privateParameter);
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x001350D2 File Offset: 0x001340D2
		public static DsaPublicKeyParameters GetDsaPublicKey(DSA dsa)
		{
			return DotNetUtilities.GetDsaPublicKey(dsa.ExportParameters(false));
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x001350E0 File Offset: 0x001340E0
		public static DsaPublicKeyParameters GetDsaPublicKey(DSAParameters dp)
		{
			DsaValidationParameters parameters = (dp.Seed != null) ? new DsaValidationParameters(dp.Seed, dp.Counter) : null;
			DsaParameters parameters2 = new DsaParameters(new BigInteger(1, dp.P), new BigInteger(1, dp.Q), new BigInteger(1, dp.G), parameters);
			return new DsaPublicKeyParameters(new BigInteger(1, dp.Y), parameters2);
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x0013514E File Offset: 0x0013414E
		public static AsymmetricCipherKeyPair GetRsaKeyPair(RSA rsa)
		{
			return DotNetUtilities.GetRsaKeyPair(rsa.ExportParameters(true));
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x0013515C File Offset: 0x0013415C
		public static AsymmetricCipherKeyPair GetRsaKeyPair(RSAParameters rp)
		{
			BigInteger modulus = new BigInteger(1, rp.Modulus);
			BigInteger bigInteger = new BigInteger(1, rp.Exponent);
			RsaKeyParameters publicParameter = new RsaKeyParameters(false, modulus, bigInteger);
			RsaPrivateCrtKeyParameters privateParameter = new RsaPrivateCrtKeyParameters(modulus, bigInteger, new BigInteger(1, rp.D), new BigInteger(1, rp.P), new BigInteger(1, rp.Q), new BigInteger(1, rp.DP), new BigInteger(1, rp.DQ), new BigInteger(1, rp.InverseQ));
			return new AsymmetricCipherKeyPair(publicParameter, privateParameter);
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x001351EB File Offset: 0x001341EB
		public static RsaKeyParameters GetRsaPublicKey(RSA rsa)
		{
			return DotNetUtilities.GetRsaPublicKey(rsa.ExportParameters(false));
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x001351F9 File Offset: 0x001341F9
		public static RsaKeyParameters GetRsaPublicKey(RSAParameters rp)
		{
			return new RsaKeyParameters(false, new BigInteger(1, rp.Modulus), new BigInteger(1, rp.Exponent));
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x0013521B File Offset: 0x0013421B
		public static AsymmetricCipherKeyPair GetKeyPair(AsymmetricAlgorithm privateKey)
		{
			if (privateKey is DSA)
			{
				return DotNetUtilities.GetDsaKeyPair((DSA)privateKey);
			}
			if (privateKey is RSA)
			{
				return DotNetUtilities.GetRsaKeyPair((RSA)privateKey);
			}
			throw new ArgumentException("Unsupported algorithm specified", "privateKey");
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x00135254 File Offset: 0x00134254
		public static RSA ToRSA(RsaKeyParameters rsaKey)
		{
			RSAParameters parameters = DotNetUtilities.ToRSAParameters(rsaKey);
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
			rsacryptoServiceProvider.ImportParameters(parameters);
			return rsacryptoServiceProvider;
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x00135278 File Offset: 0x00134278
		public static RSA ToRSA(RsaPrivateCrtKeyParameters privKey)
		{
			RSAParameters parameters = DotNetUtilities.ToRSAParameters(privKey);
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider();
			rsacryptoServiceProvider.ImportParameters(parameters);
			return rsacryptoServiceProvider;
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x0013529C File Offset: 0x0013429C
		public static RSAParameters ToRSAParameters(RsaKeyParameters rsaKey)
		{
			RSAParameters result = default(RSAParameters);
			result.Modulus = rsaKey.Modulus.ToByteArrayUnsigned();
			if (rsaKey.IsPrivate)
			{
				result.D = rsaKey.Exponent.ToByteArrayUnsigned();
			}
			else
			{
				result.Exponent = rsaKey.Exponent.ToByteArrayUnsigned();
			}
			return result;
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x001352F4 File Offset: 0x001342F4
		public static RSAParameters ToRSAParameters(RsaPrivateCrtKeyParameters privKey)
		{
			return new RSAParameters
			{
				Modulus = privKey.Modulus.ToByteArrayUnsigned(),
				Exponent = privKey.PublicExponent.ToByteArrayUnsigned(),
				D = privKey.Exponent.ToByteArrayUnsigned(),
				P = privKey.P.ToByteArrayUnsigned(),
				Q = privKey.Q.ToByteArrayUnsigned(),
				DP = privKey.DP.ToByteArrayUnsigned(),
				DQ = privKey.DQ.ToByteArrayUnsigned(),
				InverseQ = privKey.QInv.ToByteArrayUnsigned()
			};
		}
	}
}

using System;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Pkcs
{
	// Token: 0x020001DF RID: 479
	public sealed class PrivateKeyInfoFactory
	{
		// Token: 0x060012DE RID: 4830 RVA: 0x0006BEBC File Offset: 0x0006AEBC
		private PrivateKeyInfoFactory()
		{
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0006BEC4 File Offset: 0x0006AEC4
		public static PrivateKeyInfo CreatePrivateKeyInfo(AsymmetricKeyParameter key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (!key.IsPrivate)
			{
				throw new ArgumentException("Public key passed - private key expected", "key");
			}
			if (key is ElGamalPrivateKeyParameters)
			{
				ElGamalPrivateKeyParameters elGamalPrivateKeyParameters = (ElGamalPrivateKeyParameters)key;
				return new PrivateKeyInfo(new AlgorithmIdentifier(OiwObjectIdentifiers.ElGamalAlgorithm, new ElGamalParameter(elGamalPrivateKeyParameters.Parameters.P, elGamalPrivateKeyParameters.Parameters.G).ToAsn1Object()), new DerInteger(elGamalPrivateKeyParameters.X));
			}
			if (key is DsaPrivateKeyParameters)
			{
				DsaPrivateKeyParameters dsaPrivateKeyParameters = (DsaPrivateKeyParameters)key;
				return new PrivateKeyInfo(new AlgorithmIdentifier(X9ObjectIdentifiers.IdDsa, new DsaParameter(dsaPrivateKeyParameters.Parameters.P, dsaPrivateKeyParameters.Parameters.Q, dsaPrivateKeyParameters.Parameters.G).ToAsn1Object()), new DerInteger(dsaPrivateKeyParameters.X));
			}
			if (key is DHPrivateKeyParameters)
			{
				DHPrivateKeyParameters dhprivateKeyParameters = (DHPrivateKeyParameters)key;
				DHParameter dhparameter = new DHParameter(dhprivateKeyParameters.Parameters.P, dhprivateKeyParameters.Parameters.G, 0);
				return new PrivateKeyInfo(new AlgorithmIdentifier(PkcsObjectIdentifiers.DhKeyAgreement, dhparameter.ToAsn1Object()), new DerInteger(dhprivateKeyParameters.X));
			}
			if (key is RsaKeyParameters)
			{
				AlgorithmIdentifier algID = new AlgorithmIdentifier(PkcsObjectIdentifiers.RsaEncryption, DerNull.Instance);
				RsaPrivateKeyStructure rsaPrivateKeyStructure;
				if (key is RsaPrivateCrtKeyParameters)
				{
					RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = (RsaPrivateCrtKeyParameters)key;
					rsaPrivateKeyStructure = new RsaPrivateKeyStructure(rsaPrivateCrtKeyParameters.Modulus, rsaPrivateCrtKeyParameters.PublicExponent, rsaPrivateCrtKeyParameters.Exponent, rsaPrivateCrtKeyParameters.P, rsaPrivateCrtKeyParameters.Q, rsaPrivateCrtKeyParameters.DP, rsaPrivateCrtKeyParameters.DQ, rsaPrivateCrtKeyParameters.QInv);
				}
				else
				{
					RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)key;
					rsaPrivateKeyStructure = new RsaPrivateKeyStructure(rsaKeyParameters.Modulus, BigInteger.Zero, rsaKeyParameters.Exponent, BigInteger.Zero, BigInteger.Zero, BigInteger.Zero, BigInteger.Zero, BigInteger.Zero);
				}
				return new PrivateKeyInfo(algID, rsaPrivateKeyStructure.ToAsn1Object());
			}
			if (key is ECPrivateKeyParameters)
			{
				ECPrivateKeyParameters ecprivateKeyParameters = (ECPrivateKeyParameters)key;
				AlgorithmIdentifier algID2;
				ECPrivateKeyStructure ecprivateKeyStructure;
				if (ecprivateKeyParameters.AlgorithmName == "ECGOST3410")
				{
					if (ecprivateKeyParameters.PublicKeyParamSet == null)
					{
						throw Platform.CreateNotImplementedException("Not a CryptoPro parameter set");
					}
					Gost3410PublicKeyAlgParameters gost3410PublicKeyAlgParameters = new Gost3410PublicKeyAlgParameters(ecprivateKeyParameters.PublicKeyParamSet, CryptoProObjectIdentifiers.GostR3411x94CryptoProParamSet);
					algID2 = new AlgorithmIdentifier(CryptoProObjectIdentifiers.GostR3410x2001, gost3410PublicKeyAlgParameters.ToAsn1Object());
					ecprivateKeyStructure = new ECPrivateKeyStructure(ecprivateKeyParameters.D);
				}
				else
				{
					X962Parameters x962Parameters;
					if (ecprivateKeyParameters.PublicKeyParamSet == null)
					{
						ECDomainParameters parameters = ecprivateKeyParameters.Parameters;
						X9ECParameters ecParameters = new X9ECParameters(parameters.Curve, parameters.G, parameters.N, parameters.H, parameters.GetSeed());
						x962Parameters = new X962Parameters(ecParameters);
					}
					else
					{
						x962Parameters = new X962Parameters(ecprivateKeyParameters.PublicKeyParamSet);
					}
					Asn1Object parameters2 = x962Parameters.ToAsn1Object();
					ecprivateKeyStructure = new ECPrivateKeyStructure(ecprivateKeyParameters.D, parameters2);
					algID2 = new AlgorithmIdentifier(X9ObjectIdentifiers.IdECPublicKey, parameters2);
				}
				return new PrivateKeyInfo(algID2, ecprivateKeyStructure.ToAsn1Object());
			}
			if (!(key is Gost3410PrivateKeyParameters))
			{
				throw new ArgumentException("Class provided is not convertible: " + key.GetType().FullName);
			}
			Gost3410PrivateKeyParameters gost3410PrivateKeyParameters = (Gost3410PrivateKeyParameters)key;
			if (gost3410PrivateKeyParameters.PublicKeyParamSet == null)
			{
				throw Platform.CreateNotImplementedException("Not a CryptoPro parameter set");
			}
			byte[] array = gost3410PrivateKeyParameters.X.ToByteArrayUnsigned();
			byte[] array2 = new byte[array.Length];
			for (int num = 0; num != array2.Length; num++)
			{
				array2[num] = array[array.Length - 1 - num];
			}
			Gost3410PublicKeyAlgParameters gost3410PublicKeyAlgParameters2 = new Gost3410PublicKeyAlgParameters(gost3410PrivateKeyParameters.PublicKeyParamSet, CryptoProObjectIdentifiers.GostR3411x94CryptoProParamSet, null);
			AlgorithmIdentifier algID3 = new AlgorithmIdentifier(CryptoProObjectIdentifiers.GostR3410x94, gost3410PublicKeyAlgParameters2.ToAsn1Object());
			return new PrivateKeyInfo(algID3, new DerOctetString(array2));
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0006C249 File Offset: 0x0006B249
		public static PrivateKeyInfo CreatePrivateKeyInfo(char[] passPhrase, EncryptedPrivateKeyInfo encInfo)
		{
			return PrivateKeyInfoFactory.CreatePrivateKeyInfo(passPhrase, false, encInfo);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0006C254 File Offset: 0x0006B254
		public static PrivateKeyInfo CreatePrivateKeyInfo(char[] passPhrase, bool wrongPkcs12Zero, EncryptedPrivateKeyInfo encInfo)
		{
			AlgorithmIdentifier encryptionAlgorithm = encInfo.EncryptionAlgorithm;
			IBufferedCipher bufferedCipher = PbeUtilities.CreateEngine(encryptionAlgorithm) as IBufferedCipher;
			ICipherParameters parameters = PbeUtilities.GenerateCipherParameters(encryptionAlgorithm, passPhrase, wrongPkcs12Zero);
			bufferedCipher.Init(false, parameters);
			byte[] encryptedData = encInfo.GetEncryptedData();
			byte[] data = bufferedCipher.DoFinal(encryptedData);
			Asn1Object obj = Asn1Object.FromByteArray(data);
			return PrivateKeyInfo.GetInstance(obj);
		}
	}
}

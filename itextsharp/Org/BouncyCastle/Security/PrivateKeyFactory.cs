using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020005FE RID: 1534
	public sealed class PrivateKeyFactory
	{
		// Token: 0x0600344F RID: 13391 RVA: 0x001450EB File Offset: 0x001440EB
		private PrivateKeyFactory()
		{
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x001450F3 File Offset: 0x001440F3
		public static AsymmetricKeyParameter CreateKey(byte[] privateKeyInfoData)
		{
			return PrivateKeyFactory.CreateKey(PrivateKeyInfo.GetInstance(Asn1Object.FromByteArray(privateKeyInfoData)));
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x00145105 File Offset: 0x00144105
		public static AsymmetricKeyParameter CreateKey(Stream inStr)
		{
			return PrivateKeyFactory.CreateKey(PrivateKeyInfo.GetInstance(Asn1Object.FromStream(inStr)));
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x00145118 File Offset: 0x00144118
		public static AsymmetricKeyParameter CreateKey(PrivateKeyInfo keyInfo)
		{
			AlgorithmIdentifier algorithmID = keyInfo.AlgorithmID;
			DerObjectIdentifier objectID = algorithmID.ObjectID;
			if (objectID.Equals(PkcsObjectIdentifiers.RsaEncryption) || objectID.Equals(X509ObjectIdentifiers.IdEARsa) || objectID.Equals(PkcsObjectIdentifiers.IdRsassaPss) || objectID.Equals(PkcsObjectIdentifiers.IdRsaesOaep))
			{
				RsaPrivateKeyStructure rsaPrivateKeyStructure = new RsaPrivateKeyStructure(Asn1Sequence.GetInstance(keyInfo.PrivateKey));
				return new RsaPrivateCrtKeyParameters(rsaPrivateKeyStructure.Modulus, rsaPrivateKeyStructure.PublicExponent, rsaPrivateKeyStructure.PrivateExponent, rsaPrivateKeyStructure.Prime1, rsaPrivateKeyStructure.Prime2, rsaPrivateKeyStructure.Exponent1, rsaPrivateKeyStructure.Exponent2, rsaPrivateKeyStructure.Coefficient);
			}
			if (objectID.Equals(PkcsObjectIdentifiers.DhKeyAgreement))
			{
				DHParameter dhparameter = new DHParameter(Asn1Sequence.GetInstance(algorithmID.Parameters.ToAsn1Object()));
				DerInteger derInteger = (DerInteger)keyInfo.PrivateKey;
				BigInteger l = dhparameter.L;
				int l2 = (l == null) ? 0 : l.IntValue;
				DHParameters parameters = new DHParameters(dhparameter.P, dhparameter.G, null, l2);
				return new DHPrivateKeyParameters(derInteger.Value, parameters);
			}
			if (objectID.Equals(OiwObjectIdentifiers.ElGamalAlgorithm))
			{
				ElGamalParameter elGamalParameter = new ElGamalParameter(Asn1Sequence.GetInstance(algorithmID.Parameters.ToAsn1Object()));
				DerInteger derInteger2 = (DerInteger)keyInfo.PrivateKey;
				return new ElGamalPrivateKeyParameters(derInteger2.Value, new ElGamalParameters(elGamalParameter.P, elGamalParameter.G));
			}
			if (objectID.Equals(X9ObjectIdentifiers.IdDsa))
			{
				DerInteger derInteger3 = (DerInteger)keyInfo.PrivateKey;
				Asn1Encodable parameters2 = algorithmID.Parameters;
				DsaParameters parameters3 = null;
				if (parameters2 != null)
				{
					DsaParameter instance = DsaParameter.GetInstance(parameters2.ToAsn1Object());
					parameters3 = new DsaParameters(instance.P, instance.Q, instance.G);
				}
				return new DsaPrivateKeyParameters(derInteger3.Value, parameters3);
			}
			if (objectID.Equals(X9ObjectIdentifiers.IdECPublicKey))
			{
				X962Parameters x962Parameters = new X962Parameters(algorithmID.Parameters.ToAsn1Object());
				X9ECParameters x9ECParameters;
				if (x962Parameters.IsNamedCurve)
				{
					x9ECParameters = ECKeyPairGenerator.FindECCurveByOid((DerObjectIdentifier)x962Parameters.Parameters);
				}
				else
				{
					x9ECParameters = new X9ECParameters((Asn1Sequence)x962Parameters.Parameters);
				}
				ECDomainParameters parameters4 = new ECDomainParameters(x9ECParameters.Curve, x9ECParameters.G, x9ECParameters.N, x9ECParameters.H, x9ECParameters.GetSeed());
				ECPrivateKeyStructure ecprivateKeyStructure = new ECPrivateKeyStructure(Asn1Sequence.GetInstance(keyInfo.PrivateKey));
				return new ECPrivateKeyParameters(ecprivateKeyStructure.GetKey(), parameters4);
			}
			if (objectID.Equals(CryptoProObjectIdentifiers.GostR3410x2001))
			{
				Gost3410PublicKeyAlgParameters gost3410PublicKeyAlgParameters = new Gost3410PublicKeyAlgParameters(Asn1Sequence.GetInstance(algorithmID.Parameters.ToAsn1Object()));
				ECPrivateKeyStructure ecprivateKeyStructure2 = new ECPrivateKeyStructure(Asn1Sequence.GetInstance(keyInfo.PrivateKey));
				if (ECGost3410NamedCurves.GetByOid(gost3410PublicKeyAlgParameters.PublicKeyParamSet) == null)
				{
					return null;
				}
				return new ECPrivateKeyParameters("ECGOST3410", ecprivateKeyStructure2.GetKey(), gost3410PublicKeyAlgParameters.PublicKeyParamSet);
			}
			else
			{
				if (objectID.Equals(CryptoProObjectIdentifiers.GostR3410x94))
				{
					Gost3410PublicKeyAlgParameters gost3410PublicKeyAlgParameters2 = new Gost3410PublicKeyAlgParameters(Asn1Sequence.GetInstance(algorithmID.Parameters.ToAsn1Object()));
					DerOctetString derOctetString = (DerOctetString)keyInfo.PrivateKey;
					byte[] octets = derOctetString.GetOctets();
					byte[] array = new byte[octets.Length];
					for (int num = 0; num != octets.Length; num++)
					{
						array[num] = octets[octets.Length - 1 - num];
					}
					BigInteger x = new BigInteger(1, array);
					return new Gost3410PrivateKeyParameters(x, gost3410PublicKeyAlgParameters2.PublicKeyParamSet);
				}
				throw new SecurityUtilityException("algorithm identifier in key not recognised");
			}
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x0014545B File Offset: 0x0014445B
		public static AsymmetricKeyParameter DecryptKey(char[] passPhrase, EncryptedPrivateKeyInfo encInfo)
		{
			return PrivateKeyFactory.CreateKey(PrivateKeyInfoFactory.CreatePrivateKeyInfo(passPhrase, encInfo));
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x00145469 File Offset: 0x00144469
		public static AsymmetricKeyParameter DecryptKey(char[] passPhrase, byte[] encryptedPrivateKeyInfoData)
		{
			return PrivateKeyFactory.DecryptKey(passPhrase, Asn1Object.FromByteArray(encryptedPrivateKeyInfoData));
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x00145477 File Offset: 0x00144477
		public static AsymmetricKeyParameter DecryptKey(char[] passPhrase, Stream encryptedPrivateKeyInfoStream)
		{
			return PrivateKeyFactory.DecryptKey(passPhrase, Asn1Object.FromStream(encryptedPrivateKeyInfoStream));
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x00145485 File Offset: 0x00144485
		private static AsymmetricKeyParameter DecryptKey(char[] passPhrase, Asn1Object asn1Object)
		{
			return PrivateKeyFactory.DecryptKey(passPhrase, EncryptedPrivateKeyInfo.GetInstance(asn1Object));
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x00145493 File Offset: 0x00144493
		public static byte[] EncryptKey(DerObjectIdentifier algorithm, char[] passPhrase, byte[] salt, int iterationCount, AsymmetricKeyParameter key)
		{
			return EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(algorithm, passPhrase, salt, iterationCount, key).GetEncoded();
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x001454A5 File Offset: 0x001444A5
		public static byte[] EncryptKey(string algorithm, char[] passPhrase, byte[] salt, int iterationCount, AsymmetricKeyParameter key)
		{
			return EncryptedPrivateKeyInfoFactory.CreateEncryptedPrivateKeyInfo(algorithm, passPhrase, salt, iterationCount, key).GetEncoded();
		}
	}
}

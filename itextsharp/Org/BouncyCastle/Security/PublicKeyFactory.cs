using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Math.EC;

namespace Org.BouncyCastle.Security
{
	// Token: 0x02000075 RID: 117
	public sealed class PublicKeyFactory
	{
		// Token: 0x060003CE RID: 974 RVA: 0x00014160 File Offset: 0x00013160
		private PublicKeyFactory()
		{
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00014168 File Offset: 0x00013168
		public static AsymmetricKeyParameter CreateKey(byte[] keyInfoData)
		{
			return PublicKeyFactory.CreateKey(SubjectPublicKeyInfo.GetInstance(Asn1Object.FromByteArray(keyInfoData)));
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001417A File Offset: 0x0001317A
		public static AsymmetricKeyParameter CreateKey(Stream inStr)
		{
			return PublicKeyFactory.CreateKey(SubjectPublicKeyInfo.GetInstance(Asn1Object.FromStream(inStr)));
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001418C File Offset: 0x0001318C
		public static AsymmetricKeyParameter CreateKey(SubjectPublicKeyInfo keyInfo)
		{
			AlgorithmIdentifier algorithmID = keyInfo.AlgorithmID;
			DerObjectIdentifier objectID = algorithmID.ObjectID;
			if (objectID.Equals(PkcsObjectIdentifiers.RsaEncryption) || objectID.Equals(X509ObjectIdentifiers.IdEARsa) || objectID.Equals(PkcsObjectIdentifiers.IdRsassaPss) || objectID.Equals(PkcsObjectIdentifiers.IdRsaesOaep))
			{
				RsaPublicKeyStructure instance = RsaPublicKeyStructure.GetInstance(keyInfo.GetPublicKey());
				return new RsaKeyParameters(false, instance.Modulus, instance.PublicExponent);
			}
			if (objectID.Equals(PkcsObjectIdentifiers.DhKeyAgreement) || objectID.Equals(X9ObjectIdentifiers.DHPublicNumber))
			{
				DHParameter dhparameter = new DHParameter(Asn1Sequence.GetInstance(algorithmID.Parameters.ToAsn1Object()));
				DerInteger derInteger = (DerInteger)keyInfo.GetPublicKey();
				BigInteger l = dhparameter.L;
				int l2 = (l == null) ? 0 : l.IntValue;
				DHParameters parameters = new DHParameters(dhparameter.P, dhparameter.G, null, l2);
				return new DHPublicKeyParameters(derInteger.Value, parameters);
			}
			if (objectID.Equals(OiwObjectIdentifiers.ElGamalAlgorithm))
			{
				ElGamalParameter elGamalParameter = new ElGamalParameter(Asn1Sequence.GetInstance(algorithmID.Parameters.ToAsn1Object()));
				DerInteger derInteger2 = (DerInteger)keyInfo.GetPublicKey();
				return new ElGamalPublicKeyParameters(derInteger2.Value, new ElGamalParameters(elGamalParameter.P, elGamalParameter.G));
			}
			if (objectID.Equals(X9ObjectIdentifiers.IdDsa) || objectID.Equals(OiwObjectIdentifiers.DsaWithSha1))
			{
				DerInteger derInteger3 = (DerInteger)keyInfo.GetPublicKey();
				Asn1Encodable parameters2 = algorithmID.Parameters;
				DsaParameters parameters3 = null;
				if (parameters2 != null)
				{
					DsaParameter instance2 = DsaParameter.GetInstance(parameters2.ToAsn1Object());
					parameters3 = new DsaParameters(instance2.P, instance2.Q, instance2.G);
				}
				return new DsaPublicKeyParameters(derInteger3.Value, parameters3);
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
				ECDomainParameters ecdomainParameters = new ECDomainParameters(x9ECParameters.Curve, x9ECParameters.G, x9ECParameters.N, x9ECParameters.H, x9ECParameters.GetSeed());
				DerBitString publicKeyData = keyInfo.PublicKeyData;
				byte[] bytes = publicKeyData.GetBytes();
				Asn1OctetString s = new DerOctetString(bytes);
				X9ECPoint x9ECPoint = new X9ECPoint(ecdomainParameters.Curve, s);
				return new ECPublicKeyParameters(x9ECPoint.Point, ecdomainParameters);
			}
			if (objectID.Equals(CryptoProObjectIdentifiers.GostR3410x2001))
			{
				Gost3410PublicKeyAlgParameters gost3410PublicKeyAlgParameters = new Gost3410PublicKeyAlgParameters((Asn1Sequence)algorithmID.Parameters);
				Asn1OctetString asn1OctetString;
				try
				{
					asn1OctetString = (Asn1OctetString)keyInfo.GetPublicKey();
				}
				catch (IOException)
				{
					throw new ArgumentException("invalid info structure in GOST3410 public key");
				}
				byte[] octets = asn1OctetString.GetOctets();
				byte[] array = new byte[32];
				byte[] array2 = new byte[32];
				for (int num = 0; num != array2.Length; num++)
				{
					array[num] = octets[31 - num];
				}
				for (int num2 = 0; num2 != array.Length; num2++)
				{
					array2[num2] = octets[63 - num2];
				}
				ECDomainParameters byOid = ECGost3410NamedCurves.GetByOid(gost3410PublicKeyAlgParameters.PublicKeyParamSet);
				if (byOid == null)
				{
					return null;
				}
				ECPoint q = byOid.Curve.CreatePoint(new BigInteger(1, array), new BigInteger(1, array2), false);
				return new ECPublicKeyParameters("ECGOST3410", q, gost3410PublicKeyAlgParameters.PublicKeyParamSet);
			}
			else
			{
				if (objectID.Equals(CryptoProObjectIdentifiers.GostR3410x94))
				{
					Gost3410PublicKeyAlgParameters gost3410PublicKeyAlgParameters2 = new Gost3410PublicKeyAlgParameters((Asn1Sequence)algorithmID.Parameters);
					DerOctetString derOctetString;
					try
					{
						derOctetString = (DerOctetString)keyInfo.GetPublicKey();
					}
					catch (IOException)
					{
						throw new ArgumentException("invalid info structure in GOST3410 public key");
					}
					byte[] octets2 = derOctetString.GetOctets();
					byte[] array3 = new byte[octets2.Length];
					for (int num3 = 0; num3 != octets2.Length; num3++)
					{
						array3[num3] = octets2[octets2.Length - 1 - num3];
					}
					BigInteger y = new BigInteger(1, array3);
					return new Gost3410PublicKeyParameters(y, gost3410PublicKeyAlgParameters2.PublicKeyParamSet);
				}
				throw new SecurityUtilityException("algorithm identifier in key not recognised: " + objectID);
			}
		}
	}
}

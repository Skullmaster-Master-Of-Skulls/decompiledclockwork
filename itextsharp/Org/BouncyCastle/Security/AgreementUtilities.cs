using System;
using System.Collections;
using System.Globalization;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Agreement.Kdf;
using Org.BouncyCastle.Crypto.Digests;

namespace Org.BouncyCastle.Security
{
	// Token: 0x020004FF RID: 1279
	public sealed class AgreementUtilities
	{
		// Token: 0x06002BB7 RID: 11191 RVA: 0x00108A61 File Offset: 0x00107A61
		private AgreementUtilities()
		{
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x00108A69 File Offset: 0x00107A69
		static AgreementUtilities()
		{
			AgreementUtilities.algorithms[X9ObjectIdentifiers.DHSinglePassStdDHSha1KdfScheme.Id] = "ECDHWITHSHA1KDF";
			AgreementUtilities.algorithms[X9ObjectIdentifiers.MqvSinglePassSha1KdfScheme.Id] = "ECMQVWITHSHA1KDF";
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x00108AA7 File Offset: 0x00107AA7
		public static IBasicAgreement GetBasicAgreement(DerObjectIdentifier oid)
		{
			return AgreementUtilities.GetBasicAgreement(oid.Id);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x00108AB4 File Offset: 0x00107AB4
		public static IBasicAgreement GetBasicAgreement(string algorithm)
		{
			string text = algorithm.ToUpper(CultureInfo.InvariantCulture);
			string text2 = (string)AgreementUtilities.algorithms[text];
			if (text2 == null)
			{
				text2 = text;
			}
			string a;
			if ((a = text2) != null)
			{
				if (a == "DH" || a == "DIFFIEHELLMAN")
				{
					return new DHBasicAgreement();
				}
				if (a == "ECDH")
				{
					return new ECDHBasicAgreement();
				}
				if (a == "ECDHC")
				{
					return new ECDHCBasicAgreement();
				}
				if (a == "ECMQV")
				{
					return new ECMqvBasicAgreement();
				}
			}
			throw new SecurityUtilityException("Basic Agreement " + algorithm + " not recognised.");
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x00108B58 File Offset: 0x00107B58
		public static IBasicAgreement GetBasicAgreementWithKdf(DerObjectIdentifier oid, string wrapAlgorithm)
		{
			return AgreementUtilities.GetBasicAgreementWithKdf(oid.Id, wrapAlgorithm);
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x00108B68 File Offset: 0x00107B68
		public static IBasicAgreement GetBasicAgreementWithKdf(string agreeAlgorithm, string wrapAlgorithm)
		{
			string text = agreeAlgorithm.ToUpper(CultureInfo.InvariantCulture);
			string text2 = (string)AgreementUtilities.algorithms[text];
			if (text2 == null)
			{
				text2 = text;
			}
			string a;
			if ((a = text2) != null)
			{
				if (a == "DHWITHSHA1KDF" || a == "ECDHWITHSHA1KDF")
				{
					return new ECDHWithKdfBasicAgreement(wrapAlgorithm, new ECDHKekGenerator(new Sha1Digest()));
				}
				if (a == "ECMQVWITHSHA1KDF")
				{
					return new ECMqvWithKdfBasicAgreement(wrapAlgorithm, new ECDHKekGenerator(new Sha1Digest()));
				}
			}
			throw new SecurityUtilityException("Basic Agreement (with KDF) " + agreeAlgorithm + " not recognised.");
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x00108BFC File Offset: 0x00107BFC
		public static string GetAlgorithmName(DerObjectIdentifier oid)
		{
			return (string)AgreementUtilities.algorithms[oid.Id];
		}

		// Token: 0x04001E3E RID: 7742
		private static readonly Hashtable algorithms = new Hashtable();
	}
}

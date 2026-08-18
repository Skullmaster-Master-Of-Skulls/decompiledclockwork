using System;
using System.Collections;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Kisa;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Ntt;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000136 RID: 310
	public class CmsEnvelopedGenerator
	{
		// Token: 0x06000B56 RID: 2902 RVA: 0x0003FAD2 File Offset: 0x0003EAD2
		public CmsEnvelopedGenerator() : this(new SecureRandom())
		{
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0003FADF File Offset: 0x0003EADF
		public CmsEnvelopedGenerator(SecureRandom rand)
		{
			this.rand = rand;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0003FAFC File Offset: 0x0003EAFC
		public void AddKeyTransRecipient(X509Certificate cert)
		{
			KeyTransRecipientInfoGenerator keyTransRecipientInfoGenerator = new KeyTransRecipientInfoGenerator();
			keyTransRecipientInfoGenerator.RecipientCert = cert;
			this.recipientInfoGenerators.Add(keyTransRecipientInfoGenerator);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0003FB24 File Offset: 0x0003EB24
		public void AddKeyTransRecipient(AsymmetricKeyParameter pubKey, byte[] subKeyId)
		{
			KeyTransRecipientInfoGenerator keyTransRecipientInfoGenerator = new KeyTransRecipientInfoGenerator();
			keyTransRecipientInfoGenerator.RecipientPublicKey = pubKey;
			keyTransRecipientInfoGenerator.SubjectKeyIdentifier = new DerOctetString(subKeyId);
			this.recipientInfoGenerators.Add(keyTransRecipientInfoGenerator);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0003FB58 File Offset: 0x0003EB58
		public void AddKekRecipient(string keyAlgorithm, KeyParameter key, byte[] keyIdentifier)
		{
			KekRecipientInfoGenerator kekRecipientInfoGenerator = new KekRecipientInfoGenerator();
			kekRecipientInfoGenerator.KekIdentifier = new KekIdentifier(keyIdentifier, null, null);
			kekRecipientInfoGenerator.WrapAlgorithm = keyAlgorithm;
			kekRecipientInfoGenerator.WrapKey = key;
			this.recipientInfoGenerators.Add(kekRecipientInfoGenerator);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0003FB94 File Offset: 0x0003EB94
		public void AddPasswordRecipient(CmsPbeKey pbeKey, string kekAlgorithmOid)
		{
			Pbkdf2Params parameters = new Pbkdf2Params(pbeKey.Salt, pbeKey.IterationCount);
			PasswordRecipientInfoGenerator passwordRecipientInfoGenerator = new PasswordRecipientInfoGenerator();
			passwordRecipientInfoGenerator.DerivationAlg = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdPbkdf2, parameters);
			passwordRecipientInfoGenerator.WrapAlgorithm = kekAlgorithmOid;
			passwordRecipientInfoGenerator.WrapKey = pbeKey.GetEncoded(kekAlgorithmOid);
			this.recipientInfoGenerators.Add(passwordRecipientInfoGenerator);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0003FBEC File Offset: 0x0003EBEC
		public void AddKeyAgreementRecipient(string agreementAlgorithm, AsymmetricKeyParameter senderPrivateKey, AsymmetricKeyParameter senderPublicKey, X509Certificate recipientCert, string cekWrapAlgorithm)
		{
			this.AddKeyAgreementRecipients(agreementAlgorithm, senderPrivateKey, senderPublicKey, new ArrayList(1)
			{
				recipientCert
			}, cekWrapAlgorithm);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0003FC18 File Offset: 0x0003EC18
		public void AddKeyAgreementRecipients(string agreementAlgorithm, AsymmetricKeyParameter senderPrivateKey, AsymmetricKeyParameter senderPublicKey, ICollection recipientCerts, string cekWrapAlgorithm)
		{
			if (!senderPrivateKey.IsPrivate)
			{
				throw new ArgumentException("Expected private key", "senderPrivateKey");
			}
			if (senderPublicKey.IsPrivate)
			{
				throw new ArgumentException("Expected public key", "senderPublicKey");
			}
			KeyAgreeRecipientInfoGenerator keyAgreeRecipientInfoGenerator = new KeyAgreeRecipientInfoGenerator();
			keyAgreeRecipientInfoGenerator.AlgorithmOid = new DerObjectIdentifier(agreementAlgorithm);
			keyAgreeRecipientInfoGenerator.RecipientCerts = recipientCerts;
			keyAgreeRecipientInfoGenerator.SenderKeyPair = new AsymmetricCipherKeyPair(senderPublicKey, senderPrivateKey);
			keyAgreeRecipientInfoGenerator.WrapAlgorithmOid = new DerObjectIdentifier(cekWrapAlgorithm);
			this.recipientInfoGenerators.Add(keyAgreeRecipientInfoGenerator);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0003FC98 File Offset: 0x0003EC98
		protected internal virtual AlgorithmIdentifier GetAlgorithmIdentifier(string encryptionOid, KeyParameter encKey, Asn1Encodable asn1Params, out ICipherParameters cipherParameters)
		{
			Asn1Object asn1Object;
			if (asn1Params != null)
			{
				asn1Object = asn1Params.ToAsn1Object();
				cipherParameters = ParameterUtilities.GetCipherParameters(encryptionOid, encKey, asn1Object);
			}
			else
			{
				asn1Object = DerNull.Instance;
				cipherParameters = encKey;
			}
			return new AlgorithmIdentifier(new DerObjectIdentifier(encryptionOid), asn1Object);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0003FCD4 File Offset: 0x0003ECD4
		protected internal virtual Asn1Encodable GenerateAsn1Parameters(string encryptionOid, byte[] encKeyBytes)
		{
			Asn1Encodable result = null;
			try
			{
				if (encryptionOid.Equals(CmsEnvelopedGenerator.RC2Cbc))
				{
					byte[] array = new byte[8];
					this.rand.NextBytes(array);
					int num = encKeyBytes.Length * 8;
					int parameterVersion;
					if (num < 256)
					{
						parameterVersion = (int)CmsEnvelopedGenerator.rc2Table[num];
					}
					else
					{
						parameterVersion = num;
					}
					result = new RC2CbcParameter(parameterVersion, array);
				}
				else
				{
					result = ParameterUtilities.GenerateParameters(encryptionOid, this.rand);
				}
			}
			catch (SecurityUtilityException)
			{
			}
			return result;
		}

		// Token: 0x040008E2 RID: 2274
		public const string IdeaCbc = "1.3.6.1.4.1.188.7.1.1.2";

		// Token: 0x040008E3 RID: 2275
		public const string Cast5Cbc = "1.2.840.113533.7.66.10";

		// Token: 0x040008E4 RID: 2276
		internal static readonly short[] rc2Table = new short[]
		{
			189,
			86,
			234,
			242,
			162,
			241,
			172,
			42,
			176,
			147,
			209,
			156,
			27,
			51,
			253,
			208,
			48,
			4,
			182,
			220,
			125,
			223,
			50,
			75,
			247,
			203,
			69,
			155,
			49,
			187,
			33,
			90,
			65,
			159,
			225,
			217,
			74,
			77,
			158,
			218,
			160,
			104,
			44,
			195,
			39,
			95,
			128,
			54,
			62,
			238,
			251,
			149,
			26,
			254,
			206,
			168,
			52,
			169,
			19,
			240,
			166,
			63,
			216,
			12,
			120,
			36,
			175,
			35,
			82,
			193,
			103,
			23,
			245,
			102,
			144,
			231,
			232,
			7,
			184,
			96,
			72,
			230,
			30,
			83,
			243,
			146,
			164,
			114,
			140,
			8,
			21,
			110,
			134,
			0,
			132,
			250,
			244,
			127,
			138,
			66,
			25,
			246,
			219,
			205,
			20,
			141,
			80,
			18,
			186,
			60,
			6,
			78,
			236,
			179,
			53,
			17,
			161,
			136,
			142,
			43,
			148,
			153,
			183,
			113,
			116,
			211,
			228,
			191,
			58,
			222,
			150,
			14,
			188,
			10,
			237,
			119,
			252,
			55,
			107,
			3,
			121,
			137,
			98,
			198,
			215,
			192,
			210,
			124,
			106,
			139,
			34,
			163,
			91,
			5,
			93,
			2,
			117,
			213,
			97,
			227,
			24,
			143,
			85,
			81,
			173,
			31,
			11,
			94,
			133,
			229,
			194,
			87,
			99,
			202,
			61,
			108,
			180,
			197,
			204,
			112,
			178,
			145,
			89,
			13,
			71,
			32,
			200,
			79,
			88,
			224,
			1,
			226,
			22,
			56,
			196,
			111,
			59,
			15,
			101,
			70,
			190,
			126,
			45,
			123,
			130,
			249,
			64,
			181,
			29,
			115,
			248,
			235,
			38,
			199,
			135,
			151,
			37,
			84,
			177,
			40,
			170,
			152,
			157,
			165,
			100,
			109,
			122,
			212,
			16,
			129,
			68,
			239,
			73,
			214,
			174,
			46,
			221,
			118,
			92,
			47,
			167,
			28,
			201,
			9,
			105,
			154,
			131,
			207,
			41,
			57,
			185,
			233,
			76,
			255,
			67,
			171
		};

		// Token: 0x040008E5 RID: 2277
		public static readonly string DesEde3Cbc = PkcsObjectIdentifiers.DesEde3Cbc.Id;

		// Token: 0x040008E6 RID: 2278
		public static readonly string RC2Cbc = PkcsObjectIdentifiers.RC2Cbc.Id;

		// Token: 0x040008E7 RID: 2279
		public static readonly string Aes128Cbc = NistObjectIdentifiers.IdAes128Cbc.Id;

		// Token: 0x040008E8 RID: 2280
		public static readonly string Aes192Cbc = NistObjectIdentifiers.IdAes192Cbc.Id;

		// Token: 0x040008E9 RID: 2281
		public static readonly string Aes256Cbc = NistObjectIdentifiers.IdAes256Cbc.Id;

		// Token: 0x040008EA RID: 2282
		public static readonly string Camellia128Cbc = NttObjectIdentifiers.IdCamellia128Cbc.Id;

		// Token: 0x040008EB RID: 2283
		public static readonly string Camellia192Cbc = NttObjectIdentifiers.IdCamellia192Cbc.Id;

		// Token: 0x040008EC RID: 2284
		public static readonly string Camellia256Cbc = NttObjectIdentifiers.IdCamellia256Cbc.Id;

		// Token: 0x040008ED RID: 2285
		public static readonly string SeedCbc = KisaObjectIdentifiers.IdSeedCbc.Id;

		// Token: 0x040008EE RID: 2286
		public static readonly string DesEde3Wrap = PkcsObjectIdentifiers.IdAlgCms3DesWrap.Id;

		// Token: 0x040008EF RID: 2287
		public static readonly string Aes128Wrap = NistObjectIdentifiers.IdAes128Wrap.Id;

		// Token: 0x040008F0 RID: 2288
		public static readonly string Aes192Wrap = NistObjectIdentifiers.IdAes192Wrap.Id;

		// Token: 0x040008F1 RID: 2289
		public static readonly string Aes256Wrap = NistObjectIdentifiers.IdAes256Wrap.Id;

		// Token: 0x040008F2 RID: 2290
		public static readonly string Camellia128Wrap = NttObjectIdentifiers.IdCamellia128Wrap.Id;

		// Token: 0x040008F3 RID: 2291
		public static readonly string Camellia192Wrap = NttObjectIdentifiers.IdCamellia192Wrap.Id;

		// Token: 0x040008F4 RID: 2292
		public static readonly string Camellia256Wrap = NttObjectIdentifiers.IdCamellia256Wrap.Id;

		// Token: 0x040008F5 RID: 2293
		public static readonly string SeedWrap = KisaObjectIdentifiers.IdNpkiAppCmsSeedWrap.Id;

		// Token: 0x040008F6 RID: 2294
		public static readonly string ECDHSha1Kdf = X9ObjectIdentifiers.DHSinglePassStdDHSha1KdfScheme.Id;

		// Token: 0x040008F7 RID: 2295
		public static readonly string ECMqvSha1Kdf = X9ObjectIdentifiers.MqvSinglePassSha1KdfScheme.Id;

		// Token: 0x040008F8 RID: 2296
		internal readonly IList recipientInfoGenerators = new ArrayList();

		// Token: 0x040008F9 RID: 2297
		internal readonly SecureRandom rand;
	}
}

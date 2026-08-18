using System;
using System.Collections;
using System.Globalization;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Iana;
using Org.BouncyCastle.Asn1.Kisa;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Ntt;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;

namespace Org.BouncyCastle.Security
{
	// Token: 0x02000541 RID: 1345
	public sealed class GeneratorUtilities
	{
		// Token: 0x06002E36 RID: 11830 RVA: 0x0011D7D2 File Offset: 0x0011C7D2
		private GeneratorUtilities()
		{
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x0011D7DC File Offset: 0x0011C7DC
		static GeneratorUtilities()
		{
			GeneratorUtilities.AddKgAlgorithm("AES", new object[]
			{
				"AESWRAP"
			});
			GeneratorUtilities.AddKgAlgorithm("AES128", new object[]
			{
				"2.16.840.1.101.3.4.2",
				NistObjectIdentifiers.IdAes128Cbc,
				NistObjectIdentifiers.IdAes128Cfb,
				NistObjectIdentifiers.IdAes128Ecb,
				NistObjectIdentifiers.IdAes128Ofb,
				NistObjectIdentifiers.IdAes128Wrap
			});
			GeneratorUtilities.AddKgAlgorithm("AES192", new object[]
			{
				"2.16.840.1.101.3.4.22",
				NistObjectIdentifiers.IdAes192Cbc,
				NistObjectIdentifiers.IdAes192Cfb,
				NistObjectIdentifiers.IdAes192Ecb,
				NistObjectIdentifiers.IdAes192Ofb,
				NistObjectIdentifiers.IdAes192Wrap
			});
			GeneratorUtilities.AddKgAlgorithm("AES256", new object[]
			{
				"2.16.840.1.101.3.4.42",
				NistObjectIdentifiers.IdAes256Cbc,
				NistObjectIdentifiers.IdAes256Cfb,
				NistObjectIdentifiers.IdAes256Ecb,
				NistObjectIdentifiers.IdAes256Ofb,
				NistObjectIdentifiers.IdAes256Wrap
			});
			GeneratorUtilities.AddKgAlgorithm("BLOWFISH", new object[]
			{
				"1.3.6.1.4.1.3029.1.2"
			});
			GeneratorUtilities.AddKgAlgorithm("CAMELLIA", new object[]
			{
				"CAMELLIAWRAP"
			});
			GeneratorUtilities.AddKgAlgorithm("CAMELLIA128", new object[]
			{
				NttObjectIdentifiers.IdCamellia128Cbc,
				NttObjectIdentifiers.IdCamellia128Wrap
			});
			GeneratorUtilities.AddKgAlgorithm("CAMELLIA192", new object[]
			{
				NttObjectIdentifiers.IdCamellia192Cbc,
				NttObjectIdentifiers.IdCamellia192Wrap
			});
			GeneratorUtilities.AddKgAlgorithm("CAMELLIA256", new object[]
			{
				NttObjectIdentifiers.IdCamellia256Cbc,
				NttObjectIdentifiers.IdCamellia256Wrap
			});
			GeneratorUtilities.AddKgAlgorithm("CAST5", new object[]
			{
				"1.2.840.113533.7.66.10"
			});
			GeneratorUtilities.AddKgAlgorithm("CAST6", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("DES", new object[]
			{
				OiwObjectIdentifiers.DesCbc,
				OiwObjectIdentifiers.DesCfb,
				OiwObjectIdentifiers.DesEcb,
				OiwObjectIdentifiers.DesOfb
			});
			GeneratorUtilities.AddKgAlgorithm("DESEDE", new object[]
			{
				"DESEDEWRAP",
				OiwObjectIdentifiers.DesEde
			});
			GeneratorUtilities.AddKgAlgorithm("DESEDE3", new object[]
			{
				PkcsObjectIdentifiers.DesEde3Cbc,
				PkcsObjectIdentifiers.IdAlgCms3DesWrap
			});
			GeneratorUtilities.AddKgAlgorithm("GOST28147", new object[]
			{
				"GOST",
				"GOST-28147",
				CryptoProObjectIdentifiers.GostR28147Cbc
			});
			GeneratorUtilities.AddKgAlgorithm("HC128", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("HC256", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("IDEA", new object[]
			{
				"1.3.6.1.4.1.188.7.1.1.2"
			});
			GeneratorUtilities.AddKgAlgorithm("NOEKEON", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("RC2", new object[]
			{
				PkcsObjectIdentifiers.RC2Cbc,
				PkcsObjectIdentifiers.IdAlgCmsRC2Wrap
			});
			GeneratorUtilities.AddKgAlgorithm("RC4", new object[]
			{
				"ARC4",
				"1.2.840.113549.3.4"
			});
			GeneratorUtilities.AddKgAlgorithm("RC5", new object[]
			{
				"RC5-32"
			});
			GeneratorUtilities.AddKgAlgorithm("RC5-64", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("RC6", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("RIJNDAEL", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("SALSA20", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("SEED", new object[]
			{
				KisaObjectIdentifiers.IdNpkiAppCmsSeedWrap,
				KisaObjectIdentifiers.IdSeedCbc
			});
			GeneratorUtilities.AddKgAlgorithm("SERPENT", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("SKIPJACK", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("TEA", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("TWOFISH", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("VMPC", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("VMPC-KSA3", new object[0]);
			GeneratorUtilities.AddKgAlgorithm("XTEA", new object[0]);
			GeneratorUtilities.AddHMacKeyGenerator("MD2", new object[0]);
			GeneratorUtilities.AddHMacKeyGenerator("MD4", new object[0]);
			GeneratorUtilities.AddHMacKeyGenerator("MD5", new object[]
			{
				IanaObjectIdentifiers.HmacMD5
			});
			GeneratorUtilities.AddHMacKeyGenerator("SHA1", new object[]
			{
				PkcsObjectIdentifiers.IdHmacWithSha1,
				IanaObjectIdentifiers.HmacSha1
			});
			GeneratorUtilities.AddHMacKeyGenerator("SHA224", new object[]
			{
				PkcsObjectIdentifiers.IdHmacWithSha224
			});
			GeneratorUtilities.AddHMacKeyGenerator("SHA256", new object[]
			{
				PkcsObjectIdentifiers.IdHmacWithSha256
			});
			GeneratorUtilities.AddHMacKeyGenerator("SHA384", new object[]
			{
				PkcsObjectIdentifiers.IdHmacWithSha384
			});
			GeneratorUtilities.AddHMacKeyGenerator("SHA512", new object[]
			{
				PkcsObjectIdentifiers.IdHmacWithSha512
			});
			GeneratorUtilities.AddHMacKeyGenerator("RIPEMD128", new object[0]);
			GeneratorUtilities.AddHMacKeyGenerator("RIPEMD160", new object[]
			{
				IanaObjectIdentifiers.HmacRipeMD160
			});
			GeneratorUtilities.AddHMacKeyGenerator("TIGER", new object[]
			{
				IanaObjectIdentifiers.HmacTiger
			});
			GeneratorUtilities.AddKpgAlgorithm("DH", new object[]
			{
				"DIFFIEHELLMAN"
			});
			GeneratorUtilities.AddKpgAlgorithm("DSA", new object[0]);
			GeneratorUtilities.AddKpgAlgorithm("EC", new object[]
			{
				X9ObjectIdentifiers.DHSinglePassStdDHSha1KdfScheme
			});
			GeneratorUtilities.AddKpgAlgorithm("ECDH", new object[]
			{
				"ECIES"
			});
			GeneratorUtilities.AddKpgAlgorithm("ECDHC", new object[0]);
			GeneratorUtilities.AddKpgAlgorithm("ECMQV", new object[]
			{
				X9ObjectIdentifiers.MqvSinglePassSha1KdfScheme
			});
			GeneratorUtilities.AddKpgAlgorithm("ECDSA", new object[0]);
			GeneratorUtilities.AddKpgAlgorithm("ECGOST3410", new object[]
			{
				"ECGOST-3410",
				"GOST-3410-2001"
			});
			GeneratorUtilities.AddKpgAlgorithm("ELGAMAL", new object[0]);
			GeneratorUtilities.AddKpgAlgorithm("GOST3410", new object[]
			{
				"GOST-3410",
				"GOST-3410-94"
			});
			GeneratorUtilities.AddKpgAlgorithm("RSA", new object[]
			{
				"1.2.840.113549.1.1.1"
			});
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x0011DE34 File Offset: 0x0011CE34
		private static void AddKgAlgorithm(string canonicalName, params object[] aliases)
		{
			GeneratorUtilities.kgAlgorithms[canonicalName] = canonicalName;
			foreach (object obj in aliases)
			{
				GeneratorUtilities.kgAlgorithms[obj.ToString()] = canonicalName;
			}
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x0011DE74 File Offset: 0x0011CE74
		private static void AddKpgAlgorithm(string canonicalName, params object[] aliases)
		{
			GeneratorUtilities.kpgAlgorithms[canonicalName] = canonicalName;
			foreach (object obj in aliases)
			{
				GeneratorUtilities.kpgAlgorithms[obj.ToString()] = canonicalName;
			}
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x0011DEB4 File Offset: 0x0011CEB4
		private static void AddHMacKeyGenerator(string algorithm, params object[] aliases)
		{
			string text = "HMAC" + algorithm;
			GeneratorUtilities.kgAlgorithms[text] = text;
			GeneratorUtilities.kgAlgorithms["HMAC-" + algorithm] = text;
			GeneratorUtilities.kgAlgorithms["HMAC/" + algorithm] = text;
			foreach (object obj in aliases)
			{
				GeneratorUtilities.kgAlgorithms[obj.ToString()] = text;
			}
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x0011DF2A File Offset: 0x0011CF2A
		internal static string GetCanonicalKeyGeneratorAlgorithm(string algorithm)
		{
			return (string)GeneratorUtilities.kgAlgorithms[algorithm.ToUpper(CultureInfo.InvariantCulture)];
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x0011DF46 File Offset: 0x0011CF46
		internal static string GetCanonicalKeyPairGeneratorAlgorithm(string algorithm)
		{
			return (string)GeneratorUtilities.kpgAlgorithms[algorithm.ToUpper(CultureInfo.InvariantCulture)];
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x0011DF62 File Offset: 0x0011CF62
		public static CipherKeyGenerator GetKeyGenerator(DerObjectIdentifier oid)
		{
			return GeneratorUtilities.GetKeyGenerator(oid.Id);
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x0011DF70 File Offset: 0x0011CF70
		public static CipherKeyGenerator GetKeyGenerator(string algorithm)
		{
			string canonicalKeyGeneratorAlgorithm = GeneratorUtilities.GetCanonicalKeyGeneratorAlgorithm(algorithm);
			if (canonicalKeyGeneratorAlgorithm == null)
			{
				throw new SecurityUtilityException("KeyGenerator " + algorithm + " not recognised.");
			}
			int num = GeneratorUtilities.FindDefaultKeySize(canonicalKeyGeneratorAlgorithm);
			if (num == -1)
			{
				throw new SecurityUtilityException(string.Concat(new string[]
				{
					"KeyGenerator ",
					algorithm,
					" (",
					canonicalKeyGeneratorAlgorithm,
					") not supported."
				}));
			}
			string a;
			if ((a = canonicalKeyGeneratorAlgorithm) != null)
			{
				if (a == "DES")
				{
					return new DesKeyGenerator(num);
				}
				if (a == "DESEDE" || a == "DESEDE3")
				{
					return new DesEdeKeyGenerator(num);
				}
			}
			return new CipherKeyGenerator(num);
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x0011E01D File Offset: 0x0011D01D
		public static IAsymmetricCipherKeyPairGenerator GetKeyPairGenerator(DerObjectIdentifier oid)
		{
			return GeneratorUtilities.GetKeyPairGenerator(oid.Id);
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x0011E02C File Offset: 0x0011D02C
		public static IAsymmetricCipherKeyPairGenerator GetKeyPairGenerator(string algorithm)
		{
			string canonicalKeyPairGeneratorAlgorithm = GeneratorUtilities.GetCanonicalKeyPairGeneratorAlgorithm(algorithm);
			if (canonicalKeyPairGeneratorAlgorithm == null)
			{
				throw new SecurityUtilityException("KeyPairGenerator " + algorithm + " not recognised.");
			}
			string key;
			switch (key = canonicalKeyPairGeneratorAlgorithm)
			{
			case "DH":
				return new DHKeyPairGenerator();
			case "DSA":
				return new DsaKeyPairGenerator();
			case "EC":
			case "ECDH":
			case "ECDHC":
			case "ECDSA":
			case "ECGOST3410":
			case "ECMQV":
				return new ECKeyPairGenerator(canonicalKeyPairGeneratorAlgorithm);
			case "ELGAMAL":
				return new ElGamalKeyPairGenerator();
			case "GOST3410":
				return new Gost3410KeyPairGenerator();
			case "RSA":
				return new RsaKeyPairGenerator();
			}
			throw new SecurityUtilityException(string.Concat(new string[]
			{
				"KeyPairGenerator ",
				algorithm,
				" (",
				canonicalKeyPairGeneratorAlgorithm,
				") not supported."
			}));
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x0011E19D File Offset: 0x0011D19D
		internal static int GetDefaultKeySize(DerObjectIdentifier oid)
		{
			return GeneratorUtilities.GetDefaultKeySize(oid.Id);
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x0011E1AC File Offset: 0x0011D1AC
		internal static int GetDefaultKeySize(string algorithm)
		{
			string canonicalKeyGeneratorAlgorithm = GeneratorUtilities.GetCanonicalKeyGeneratorAlgorithm(algorithm);
			if (canonicalKeyGeneratorAlgorithm == null)
			{
				throw new SecurityUtilityException("KeyGenerator " + algorithm + " not recognised.");
			}
			int num = GeneratorUtilities.FindDefaultKeySize(canonicalKeyGeneratorAlgorithm);
			if (num == -1)
			{
				throw new SecurityUtilityException(string.Concat(new string[]
				{
					"KeyGenerator ",
					algorithm,
					" (",
					canonicalKeyGeneratorAlgorithm,
					") not supported."
				}));
			}
			return num;
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x0011E218 File Offset: 0x0011D218
		private static int FindDefaultKeySize(string canonicalName)
		{
			switch (canonicalName)
			{
			case "DES":
				return 64;
			case "SKIPJACK":
				return 80;
			case "AES128":
			case "BLOWFISH":
			case "CAMELLIA128":
			case "CAST5":
			case "DESEDE":
			case "HC128":
			case "HMACMD2":
			case "HMACMD4":
			case "HMACMD5":
			case "HMACRIPEMD128":
			case "IDEA":
			case "NOEKEON":
			case "RC2":
			case "RC4":
			case "RC5":
			case "SALSA20":
			case "SEED":
			case "TEA":
			case "XTEA":
			case "VMPC":
			case "VMPC-KSA3":
				return 128;
			case "HMACRIPEMD160":
			case "HMACSHA1":
				return 160;
			case "AES":
			case "AES192":
			case "CAMELLIA192":
			case "DESEDE3":
			case "HMACTIGER":
			case "RIJNDAEL":
			case "SERPENT":
				return 192;
			case "HMACSHA224":
				return 224;
			case "AES256":
			case "CAMELLIA":
			case "CAMELLIA256":
			case "CAST6":
			case "GOST28147":
			case "HC256":
			case "HMACSHA256":
			case "RC5-64":
			case "RC6":
			case "TWOFISH":
				return 256;
			case "HMACSHA384":
				return 384;
			case "HMACSHA512":
				return 512;
			}
			return -1;
		}

		// Token: 0x04001FFB RID: 8187
		private static readonly Hashtable kgAlgorithms = new Hashtable();

		// Token: 0x04001FFC RID: 8188
		private static readonly Hashtable kpgAlgorithms = new Hashtable();
	}
}

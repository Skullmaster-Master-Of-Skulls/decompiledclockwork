using System;
using System.Collections;
using System.Globalization;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Kisa;
using Org.BouncyCastle.Asn1.Misc;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Ntt;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;

namespace Org.BouncyCastle.Security
{
	// Token: 0x02000104 RID: 260
	public sealed class ParameterUtilities
	{
		// Token: 0x06000A37 RID: 2615 RVA: 0x00034F14 File Offset: 0x00033F14
		private ParameterUtilities()
		{
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00034F1C File Offset: 0x00033F1C
		static ParameterUtilities()
		{
			ParameterUtilities.AddAlgorithm("AES", new object[]
			{
				"AESWRAP"
			});
			ParameterUtilities.AddAlgorithm("AES128", new object[]
			{
				"2.16.840.1.101.3.4.2",
				NistObjectIdentifiers.IdAes128Cbc,
				NistObjectIdentifiers.IdAes128Cfb,
				NistObjectIdentifiers.IdAes128Ecb,
				NistObjectIdentifiers.IdAes128Ofb,
				NistObjectIdentifiers.IdAes128Wrap
			});
			ParameterUtilities.AddAlgorithm("AES192", new object[]
			{
				"2.16.840.1.101.3.4.22",
				NistObjectIdentifiers.IdAes192Cbc,
				NistObjectIdentifiers.IdAes192Cfb,
				NistObjectIdentifiers.IdAes192Ecb,
				NistObjectIdentifiers.IdAes192Ofb,
				NistObjectIdentifiers.IdAes192Wrap
			});
			ParameterUtilities.AddAlgorithm("AES256", new object[]
			{
				"2.16.840.1.101.3.4.42",
				NistObjectIdentifiers.IdAes256Cbc,
				NistObjectIdentifiers.IdAes256Cfb,
				NistObjectIdentifiers.IdAes256Ecb,
				NistObjectIdentifiers.IdAes256Ofb,
				NistObjectIdentifiers.IdAes256Wrap
			});
			ParameterUtilities.AddAlgorithm("BLOWFISH", new object[]
			{
				"1.3.6.1.4.1.3029.1.2"
			});
			ParameterUtilities.AddAlgorithm("CAMELLIA", new object[]
			{
				"CAMELLIAWRAP"
			});
			ParameterUtilities.AddAlgorithm("CAMELLIA128", new object[]
			{
				NttObjectIdentifiers.IdCamellia128Cbc,
				NttObjectIdentifiers.IdCamellia128Wrap
			});
			ParameterUtilities.AddAlgorithm("CAMELLIA192", new object[]
			{
				NttObjectIdentifiers.IdCamellia192Cbc,
				NttObjectIdentifiers.IdCamellia192Wrap
			});
			ParameterUtilities.AddAlgorithm("CAMELLIA256", new object[]
			{
				NttObjectIdentifiers.IdCamellia256Cbc,
				NttObjectIdentifiers.IdCamellia256Wrap
			});
			ParameterUtilities.AddAlgorithm("CAST5", new object[]
			{
				"1.2.840.113533.7.66.10"
			});
			ParameterUtilities.AddAlgorithm("CAST6", new object[0]);
			ParameterUtilities.AddAlgorithm("DES", new object[]
			{
				OiwObjectIdentifiers.DesCbc,
				OiwObjectIdentifiers.DesCfb,
				OiwObjectIdentifiers.DesEcb,
				OiwObjectIdentifiers.DesOfb
			});
			ParameterUtilities.AddAlgorithm("DESEDE", new object[]
			{
				"DESEDEWRAP",
				OiwObjectIdentifiers.DesEde,
				PkcsObjectIdentifiers.IdAlgCms3DesWrap
			});
			ParameterUtilities.AddAlgorithm("DESEDE3", new object[]
			{
				PkcsObjectIdentifiers.DesEde3Cbc
			});
			ParameterUtilities.AddAlgorithm("GOST28147", new object[]
			{
				"GOST",
				"GOST-28147",
				CryptoProObjectIdentifiers.GostR28147Cbc
			});
			ParameterUtilities.AddAlgorithm("HC128", new object[0]);
			ParameterUtilities.AddAlgorithm("HC256", new object[0]);
			ParameterUtilities.AddAlgorithm("NOEKEON", new object[0]);
			ParameterUtilities.AddAlgorithm("RC2", new object[]
			{
				PkcsObjectIdentifiers.RC2Cbc,
				PkcsObjectIdentifiers.IdAlgCmsRC2Wrap
			});
			ParameterUtilities.AddAlgorithm("RC4", new object[]
			{
				"ARC4",
				"1.2.840.113549.3.4"
			});
			ParameterUtilities.AddAlgorithm("RC5", new object[]
			{
				"RC5-32"
			});
			ParameterUtilities.AddAlgorithm("RC5-64", new object[0]);
			ParameterUtilities.AddAlgorithm("RC6", new object[0]);
			ParameterUtilities.AddAlgorithm("RIJNDAEL", new object[0]);
			ParameterUtilities.AddAlgorithm("SALSA20", new object[0]);
			ParameterUtilities.AddAlgorithm("SEED", new object[]
			{
				KisaObjectIdentifiers.IdNpkiAppCmsSeedWrap,
				KisaObjectIdentifiers.IdSeedCbc
			});
			ParameterUtilities.AddAlgorithm("SERPENT", new object[0]);
			ParameterUtilities.AddAlgorithm("SKIPJACK", new object[0]);
			ParameterUtilities.AddAlgorithm("TEA", new object[0]);
			ParameterUtilities.AddAlgorithm("TWOFISH", new object[0]);
			ParameterUtilities.AddAlgorithm("VMPC", new object[0]);
			ParameterUtilities.AddAlgorithm("VMPC-KSA3", new object[0]);
			ParameterUtilities.AddAlgorithm("XTEA", new object[0]);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00035310 File Offset: 0x00034310
		private static void AddAlgorithm(string canonicalName, params object[] aliases)
		{
			ParameterUtilities.algorithms[canonicalName] = canonicalName;
			foreach (object obj in aliases)
			{
				ParameterUtilities.algorithms[obj.ToString()] = canonicalName;
			}
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0003534E File Offset: 0x0003434E
		public static string GetCanonicalAlgorithmName(string algorithm)
		{
			return (string)ParameterUtilities.algorithms[algorithm.ToUpper(CultureInfo.InvariantCulture)];
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0003536A File Offset: 0x0003436A
		public static KeyParameter CreateKeyParameter(DerObjectIdentifier algOid, byte[] keyBytes)
		{
			return ParameterUtilities.CreateKeyParameter(algOid.Id, keyBytes, 0, keyBytes.Length);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0003537C File Offset: 0x0003437C
		public static KeyParameter CreateKeyParameter(string algorithm, byte[] keyBytes)
		{
			return ParameterUtilities.CreateKeyParameter(algorithm, keyBytes, 0, keyBytes.Length);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00035389 File Offset: 0x00034389
		public static KeyParameter CreateKeyParameter(DerObjectIdentifier algOid, byte[] keyBytes, int offset, int length)
		{
			return ParameterUtilities.CreateKeyParameter(algOid.Id, keyBytes, offset, length);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0003539C File Offset: 0x0003439C
		public static KeyParameter CreateKeyParameter(string algorithm, byte[] keyBytes, int offset, int length)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			string canonicalAlgorithmName = ParameterUtilities.GetCanonicalAlgorithmName(algorithm);
			if (canonicalAlgorithmName == null)
			{
				throw new SecurityUtilityException("Algorithm " + algorithm + " not recognised.");
			}
			string a;
			if ((a = canonicalAlgorithmName) != null)
			{
				if (a == "DES")
				{
					return new DesParameters(keyBytes, offset, length);
				}
				if (a == "DESEDE" || a == "DESEDE3")
				{
					return new DesEdeParameters(keyBytes, offset, length);
				}
				if (a == "RC2")
				{
					return new RC2Parameters(keyBytes, offset, length);
				}
			}
			return new KeyParameter(keyBytes, offset, length);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00035435 File Offset: 0x00034435
		public static ICipherParameters GetCipherParameters(DerObjectIdentifier algOid, ICipherParameters key, Asn1Object asn1Params)
		{
			return ParameterUtilities.GetCipherParameters(algOid.Id, key, asn1Params);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00035444 File Offset: 0x00034444
		public static ICipherParameters GetCipherParameters(string algorithm, ICipherParameters key, Asn1Object asn1Params)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			string canonicalAlgorithmName = ParameterUtilities.GetCanonicalAlgorithmName(algorithm);
			if (canonicalAlgorithmName == null)
			{
				throw new SecurityUtilityException("Algorithm " + algorithm + " not recognised.");
			}
			byte[] array = null;
			try
			{
				string key2;
				switch (key2 = canonicalAlgorithmName)
				{
				case "AES":
				case "AES128":
				case "AES192":
				case "AES256":
				case "BLOWFISH":
				case "CAMELLIA":
				case "CAMELLIA128":
				case "CAMELLIA192":
				case "CAMELLIA256":
				case "DES":
				case "DESEDE":
				case "DESEDE3":
				case "NOEKEON":
				case "RIJNDAEL":
				case "SEED":
				case "SKIPJACK":
				case "TWOFISH":
					array = ((Asn1OctetString)asn1Params).GetOctets();
					break;
				case "RC2":
					array = RC2CbcParameter.GetInstance(asn1Params).GetIV();
					break;
				case "CAST5":
					array = Cast5CbcParameters.GetInstance(asn1Params).GetIV();
					break;
				}
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("Could not process ASN.1 parameters", innerException);
			}
			if (array != null)
			{
				return new ParametersWithIV(key, array);
			}
			throw new SecurityUtilityException("Algorithm " + algorithm + " not recognised.");
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0003566C File Offset: 0x0003466C
		public static Asn1Encodable GenerateParameters(DerObjectIdentifier algID, SecureRandom random)
		{
			return ParameterUtilities.GenerateParameters(algID.Id, random);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0003567C File Offset: 0x0003467C
		public static Asn1Encodable GenerateParameters(string algorithm, SecureRandom random)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			string canonicalAlgorithmName = ParameterUtilities.GetCanonicalAlgorithmName(algorithm);
			if (canonicalAlgorithmName == null)
			{
				throw new SecurityUtilityException("Algorithm " + algorithm + " not recognised.");
			}
			string key;
			switch (key = canonicalAlgorithmName)
			{
			case "AES":
			case "AES128":
			case "AES192":
			case "AES256":
				return ParameterUtilities.CreateIVOctetString(random, 16);
			case "BLOWFISH":
				return ParameterUtilities.CreateIVOctetString(random, 8);
			case "CAMELLIA":
			case "CAMELLIA128":
			case "CAMELLIA192":
			case "CAMELLIA256":
				return ParameterUtilities.CreateIVOctetString(random, 16);
			case "CAST5":
				return new Cast5CbcParameters(ParameterUtilities.CreateIV(random, 8), 128);
			case "DES":
			case "DESEDE":
			case "DESEDE3":
				return ParameterUtilities.CreateIVOctetString(random, 8);
			case "NOEKEON":
				return ParameterUtilities.CreateIVOctetString(random, 16);
			case "RC2":
				return new RC2CbcParameter(ParameterUtilities.CreateIV(random, 8));
			case "SEED":
				return ParameterUtilities.CreateIVOctetString(random, 16);
			}
			throw new SecurityUtilityException("Algorithm " + algorithm + " not recognised.");
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00035864 File Offset: 0x00034864
		private static Asn1OctetString CreateIVOctetString(SecureRandom random, int ivLength)
		{
			return new DerOctetString(ParameterUtilities.CreateIV(random, ivLength));
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00035874 File Offset: 0x00034874
		private static byte[] CreateIV(SecureRandom random, int ivLength)
		{
			byte[] array = new byte[ivLength];
			random.NextBytes(array);
			return array;
		}

		// Token: 0x04000852 RID: 2130
		private static readonly Hashtable algorithms = new Hashtable();
	}
}

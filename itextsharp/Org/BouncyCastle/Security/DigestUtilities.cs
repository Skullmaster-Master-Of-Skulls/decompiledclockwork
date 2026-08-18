using System;
using System.Collections;
using System.Globalization;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.TeleTrust;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;

namespace Org.BouncyCastle.Security
{
	// Token: 0x02000008 RID: 8
	public sealed class DigestUtilities
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000027D8 File Offset: 0x000017D8
		private DigestUtilities()
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000027E0 File Offset: 0x000017E0
		static DigestUtilities()
		{
			DigestUtilities.algorithms[PkcsObjectIdentifiers.MD2.Id] = "MD2";
			DigestUtilities.algorithms[PkcsObjectIdentifiers.MD4.Id] = "MD4";
			DigestUtilities.algorithms[PkcsObjectIdentifiers.MD5.Id] = "MD5";
			DigestUtilities.algorithms["SHA1"] = "SHA-1";
			DigestUtilities.algorithms[OiwObjectIdentifiers.IdSha1.Id] = "SHA-1";
			DigestUtilities.algorithms["SHA224"] = "SHA-224";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha224.Id] = "SHA-224";
			DigestUtilities.algorithms["SHA256"] = "SHA-256";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha256.Id] = "SHA-256";
			DigestUtilities.algorithms["SHA384"] = "SHA-384";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha384.Id] = "SHA-384";
			DigestUtilities.algorithms["SHA512"] = "SHA-512";
			DigestUtilities.algorithms[NistObjectIdentifiers.IdSha512.Id] = "SHA-512";
			DigestUtilities.algorithms["RIPEMD-128"] = "RIPEMD128";
			DigestUtilities.algorithms[TeleTrusTObjectIdentifiers.RipeMD128.Id] = "RIPEMD128";
			DigestUtilities.algorithms["RIPEMD-160"] = "RIPEMD160";
			DigestUtilities.algorithms[TeleTrusTObjectIdentifiers.RipeMD160.Id] = "RIPEMD160";
			DigestUtilities.algorithms["RIPEMD-256"] = "RIPEMD256";
			DigestUtilities.algorithms[TeleTrusTObjectIdentifiers.RipeMD256.Id] = "RIPEMD256";
			DigestUtilities.algorithms["RIPEMD-320"] = "RIPEMD320";
			DigestUtilities.algorithms[CryptoProObjectIdentifiers.GostR3411.Id] = "GOST3411";
			DigestUtilities.oids["MD2"] = PkcsObjectIdentifiers.MD2;
			DigestUtilities.oids["MD4"] = PkcsObjectIdentifiers.MD4;
			DigestUtilities.oids["MD5"] = PkcsObjectIdentifiers.MD5;
			DigestUtilities.oids["SHA-1"] = OiwObjectIdentifiers.IdSha1;
			DigestUtilities.oids["SHA-224"] = NistObjectIdentifiers.IdSha224;
			DigestUtilities.oids["SHA-256"] = NistObjectIdentifiers.IdSha256;
			DigestUtilities.oids["SHA-384"] = NistObjectIdentifiers.IdSha384;
			DigestUtilities.oids["SHA-512"] = NistObjectIdentifiers.IdSha512;
			DigestUtilities.oids["RIPEMD128"] = TeleTrusTObjectIdentifiers.RipeMD128;
			DigestUtilities.oids["RIPEMD160"] = TeleTrusTObjectIdentifiers.RipeMD160;
			DigestUtilities.oids["RIPEMD256"] = TeleTrusTObjectIdentifiers.RipeMD256;
			DigestUtilities.oids["GOST3411"] = CryptoProObjectIdentifiers.GostR3411;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002AD4 File Offset: 0x00001AD4
		public static DerObjectIdentifier GetObjectIdentifier(string mechanism)
		{
			if (mechanism == null)
			{
				throw new ArgumentNullException("mechanism");
			}
			mechanism = mechanism.ToUpper(CultureInfo.InvariantCulture);
			string text = (string)DigestUtilities.algorithms[mechanism];
			if (text != null)
			{
				mechanism = text;
			}
			return (DerObjectIdentifier)DigestUtilities.oids[mechanism];
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002B23 File Offset: 0x00001B23
		public static ICollection Algorithms
		{
			get
			{
				return DigestUtilities.oids.Keys;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002B2F File Offset: 0x00001B2F
		public static IDigest GetDigest(DerObjectIdentifier id)
		{
			return DigestUtilities.GetDigest(id.Id);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002B3C File Offset: 0x00001B3C
		public static IDigest GetDigest(string algorithm)
		{
			string text = algorithm.ToUpper(CultureInfo.InvariantCulture);
			string text2 = (string)DigestUtilities.algorithms[text];
			if (text2 == null)
			{
				text2 = text;
			}
			string key;
			switch (key = text2)
			{
			case "GOST3411":
				return new Gost3411Digest();
			case "MD2":
				return new MD2Digest();
			case "MD4":
				return new MD4Digest();
			case "MD5":
				return new MD5Digest();
			case "RIPEMD128":
				return new RipeMD128Digest();
			case "RIPEMD160":
				return new RipeMD160Digest();
			case "RIPEMD256":
				return new RipeMD256Digest();
			case "RIPEMD320":
				return new RipeMD320Digest();
			case "SHA-1":
				return new Sha1Digest();
			case "SHA-224":
				return new Sha224Digest();
			case "SHA-256":
				return new Sha256Digest();
			case "SHA-384":
				return new Sha384Digest();
			case "SHA-512":
				return new Sha512Digest();
			case "TIGER":
				return new TigerDigest();
			case "WHIRLPOOL":
				return new WhirlpoolDigest();
			}
			throw new SecurityUtilityException("Digest " + text2 + " not recognised.");
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002D0E File Offset: 0x00001D0E
		public static string GetAlgorithmName(DerObjectIdentifier oid)
		{
			return (string)DigestUtilities.algorithms[oid.Id];
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002D28 File Offset: 0x00001D28
		public static byte[] CalculateDigest(string algorithm, byte[] input)
		{
			IDigest digest = DigestUtilities.GetDigest(algorithm);
			digest.BlockUpdate(input, 0, input.Length);
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002D50 File Offset: 0x00001D50
		public static byte[] DoFinal(IDigest digest)
		{
			byte[] array = new byte[digest.GetDigestSize()];
			digest.DoFinal(array, 0);
			return array;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002D73 File Offset: 0x00001D73
		public static byte[] DoFinal(IDigest digest, byte[] input)
		{
			digest.BlockUpdate(input, 0, input.Length);
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x04000008 RID: 8
		private static readonly Hashtable algorithms = new Hashtable();

		// Token: 0x04000009 RID: 9
		private static readonly Hashtable oids = new Hashtable();
	}
}

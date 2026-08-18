using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.CryptoPro;
using Org.BouncyCastle.Asn1.Kisa;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.Ntt;
using Org.BouncyCastle.Asn1.Oiw;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;

namespace Org.BouncyCastle.Security
{
	// Token: 0x02000105 RID: 261
	public sealed class CipherUtilities
	{
		// Token: 0x06000A45 RID: 2629 RVA: 0x00035890 File Offset: 0x00034890
		static CipherUtilities()
		{
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes128Ecb.Id] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes192Ecb.Id] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes256Ecb.Id] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms["AES//PKCS7"] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms["AES//PKCS7PADDING"] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms["AES//PKCS5"] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms["AES//PKCS5PADDING"] = "AES/ECB/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes128Cbc.Id] = "AES/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes192Cbc.Id] = "AES/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes256Cbc.Id] = "AES/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes128Ofb.Id] = "AES/OFB/NOPADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes192Ofb.Id] = "AES/OFB/NOPADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes256Ofb.Id] = "AES/OFB/NOPADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes128Cfb.Id] = "AES/CFB/NOPADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes192Cfb.Id] = "AES/CFB/NOPADDING";
			CipherUtilities.algorithms[NistObjectIdentifiers.IdAes256Cfb.Id] = "AES/CFB/NOPADDING";
			CipherUtilities.algorithms["RSA/ECB/PKCS1"] = "RSA//PKCS1PADDING";
			CipherUtilities.algorithms["RSA/ECB/PKCS1PADDING"] = "RSA//PKCS1PADDING";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.RsaEncryption.Id] = "RSA//PKCS1PADDING";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.IdRsaesOaep.Id] = "RSA//OAEPPADDING";
			CipherUtilities.algorithms[OiwObjectIdentifiers.DesCbc.Id] = "DES/CBC";
			CipherUtilities.algorithms[OiwObjectIdentifiers.DesCfb.Id] = "DES/CFB";
			CipherUtilities.algorithms[OiwObjectIdentifiers.DesEcb.Id] = "DES/ECB";
			CipherUtilities.algorithms[OiwObjectIdentifiers.DesOfb.Id] = "DES/OFB";
			CipherUtilities.algorithms[OiwObjectIdentifiers.DesEde.Id] = "DESEDE";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.DesEde3Cbc.Id] = "DESEDE/CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.RC2Cbc.Id] = "RC2/CBC";
			CipherUtilities.algorithms["1.3.6.1.4.1.188.7.1.1.2"] = "IDEA/CBC";
			CipherUtilities.algorithms["1.2.840.113533.7.66.10"] = "CAST5/CBC";
			CipherUtilities.algorithms["RC4"] = "ARC4";
			CipherUtilities.algorithms["ARCFOUR"] = "ARC4";
			CipherUtilities.algorithms["1.2.840.113549.3.4"] = "ARC4";
			CipherUtilities.algorithms["PBEWITHSHA1AND128BITRC4"] = "PBEWITHSHAAND128BITRC4";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithShaAnd128BitRC4.Id] = "PBEWITHSHAAND128BITRC4";
			CipherUtilities.algorithms["PBEWITHSHA1AND40BITRC4"] = "PBEWITHSHAAND40BITRC4";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithShaAnd40BitRC4.Id] = "PBEWITHSHAAND40BITRC4";
			CipherUtilities.algorithms["PBEWITHSHA1ANDDES"] = "PBEWITHSHA1ANDDES-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithSha1AndDesCbc.Id] = "PBEWITHSHA1ANDDES-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1ANDRC2"] = "PBEWITHSHA1ANDRC2-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithSha1AndRC2Cbc.Id] = "PBEWITHSHA1ANDRC2-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1AND3-KEYTRIPLEDES-CBC"] = "PBEWITHSHAAND3-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms["PBEWITHSHAAND3KEYTRIPLEDES"] = "PBEWITHSHAAND3-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithShaAnd3KeyTripleDesCbc.Id] = "PBEWITHSHAAND3-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1ANDDESEDE"] = "PBEWITHSHAAND3-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1AND2-KEYTRIPLEDES-CBC"] = "PBEWITHSHAAND2-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithShaAnd2KeyTripleDesCbc.Id] = "PBEWITHSHAAND2-KEYTRIPLEDES-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1AND128BITRC2-CBC"] = "PBEWITHSHAAND128BITRC2-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbeWithShaAnd128BitRC2Cbc.Id] = "PBEWITHSHAAND128BITRC2-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1AND40BITRC2-CBC"] = "PBEWITHSHAAND40BITRC2-CBC";
			CipherUtilities.algorithms[PkcsObjectIdentifiers.PbewithShaAnd40BitRC2Cbc.Id] = "PBEWITHSHAAND40BITRC2-CBC";
			CipherUtilities.algorithms["PBEWITHSHA1AND128BITAES-CBC-BC"] = "PBEWITHSHAAND128BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-1AND128BITAES-CBC-BC"] = "PBEWITHSHAAND128BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA1AND192BITAES-CBC-BC"] = "PBEWITHSHAAND192BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-1AND192BITAES-CBC-BC"] = "PBEWITHSHAAND192BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA1AND256BITAES-CBC-BC"] = "PBEWITHSHAAND256BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-1AND256BITAES-CBC-BC"] = "PBEWITHSHAAND256BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-256AND128BITAES-CBC-BC"] = "PBEWITHSHA256AND128BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-256AND192BITAES-CBC-BC"] = "PBEWITHSHA256AND192BITAES-CBC-BC";
			CipherUtilities.algorithms["PBEWITHSHA-256AND256BITAES-CBC-BC"] = "PBEWITHSHA256AND256BITAES-CBC-BC";
			CipherUtilities.algorithms["GOST"] = "GOST28147";
			CipherUtilities.algorithms["GOST-28147"] = "GOST28147";
			CipherUtilities.algorithms[CryptoProObjectIdentifiers.GostR28147Cbc.Id] = "GOST28147/CBC/PKCS7PADDING";
			CipherUtilities.algorithms["RC5-32"] = "RC5";
			CipherUtilities.algorithms[NttObjectIdentifiers.IdCamellia128Cbc.Id] = "CAMELLIA/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[NttObjectIdentifiers.IdCamellia192Cbc.Id] = "CAMELLIA/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[NttObjectIdentifiers.IdCamellia256Cbc.Id] = "CAMELLIA/CBC/PKCS7PADDING";
			CipherUtilities.algorithms[KisaObjectIdentifiers.IdSeedCbc.Id] = "SEED/CBC/PKCS7PADDING";
			CipherUtilities.algorithms["1.3.6.1.4.1.3029.1.2"] = "BLOWFISH/CBC";
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00035EAB File Offset: 0x00034EAB
		private CipherUtilities()
		{
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00035EB4 File Offset: 0x00034EB4
		public static DerObjectIdentifier GetObjectIdentifier(string mechanism)
		{
			if (mechanism == null)
			{
				throw new ArgumentNullException("mechanism");
			}
			mechanism = mechanism.ToUpper(CultureInfo.InvariantCulture);
			string text = (string)CipherUtilities.algorithms[mechanism];
			if (text != null)
			{
				mechanism = text;
			}
			return (DerObjectIdentifier)CipherUtilities.oids[mechanism];
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00035F03 File Offset: 0x00034F03
		public static ICollection Algorithms
		{
			get
			{
				return CipherUtilities.oids.Keys;
			}
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00035F0F File Offset: 0x00034F0F
		public static IBufferedCipher GetCipher(DerObjectIdentifier oid)
		{
			return CipherUtilities.GetCipher(oid.Id);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00035F1C File Offset: 0x00034F1C
		public static IBufferedCipher GetCipher(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			algorithm = algorithm.ToUpper(CultureInfo.InvariantCulture);
			string text = (string)CipherUtilities.algorithms[algorithm];
			if (text != null)
			{
				algorithm = text;
			}
			IBasicAgreement basicAgreement = null;
			if (algorithm == "IES")
			{
				basicAgreement = new DHBasicAgreement();
			}
			else if (algorithm == "ECIES")
			{
				basicAgreement = new ECDHBasicAgreement();
			}
			if (basicAgreement != null)
			{
				return new BufferedIesCipher(new IesEngine(basicAgreement, new Kdf2BytesGenerator(new Sha1Digest()), new HMac(new Sha1Digest())));
			}
			string key;
			if (algorithm.StartsWith("PBE") && (key = algorithm) != null)
			{
				if (<PrivateImplementationDetails>{BF4AB405-4DF6-41D5-BBC1-86C0068818F1}.$$method0x6000a29-1 == null)
				{
					<PrivateImplementationDetails>{BF4AB405-4DF6-41D5-BBC1-86C0068818F1}.$$method0x6000a29-1 = new Dictionary<string, int>(15)
					{
						{
							"PBEWITHSHAAND2-KEYTRIPLEDES-CBC",
							0
						},
						{
							"PBEWITHSHAAND3-KEYTRIPLEDES-CBC",
							1
						},
						{
							"PBEWITHSHAAND128BITRC2-CBC",
							2
						},
						{
							"PBEWITHSHAAND40BITRC2-CBC",
							3
						},
						{
							"PBEWITHSHAAND128BITAES-CBC-BC",
							4
						},
						{
							"PBEWITHSHAAND192BITAES-CBC-BC",
							5
						},
						{
							"PBEWITHSHAAND256BITAES-CBC-BC",
							6
						},
						{
							"PBEWITHSHA256AND128BITAES-CBC-BC",
							7
						},
						{
							"PBEWITHSHA256AND192BITAES-CBC-BC",
							8
						},
						{
							"PBEWITHSHA256AND256BITAES-CBC-BC",
							9
						},
						{
							"PBEWITHMD5AND128BITAES-CBC-OPENSSL",
							10
						},
						{
							"PBEWITHMD5AND192BITAES-CBC-OPENSSL",
							11
						},
						{
							"PBEWITHMD5AND256BITAES-CBC-OPENSSL",
							12
						},
						{
							"PBEWITHSHA1ANDDES-CBC",
							13
						},
						{
							"PBEWITHSHA1ANDRC2-CBC",
							14
						}
					};
				}
				int num;
				if (<PrivateImplementationDetails>{BF4AB405-4DF6-41D5-BBC1-86C0068818F1}.$$method0x6000a29-1.TryGetValue(key, out num))
				{
					switch (num)
					{
					case 0:
					case 1:
						return new PaddedBufferedBlockCipher(new CbcBlockCipher(new DesEdeEngine()));
					case 2:
					case 3:
						return new PaddedBufferedBlockCipher(new CbcBlockCipher(new RC2Engine()));
					case 4:
					case 5:
					case 6:
					case 7:
					case 8:
					case 9:
					case 10:
					case 11:
					case 12:
						return new PaddedBufferedBlockCipher(new CbcBlockCipher(new AesFastEngine()));
					case 13:
						return new PaddedBufferedBlockCipher(new CbcBlockCipher(new DesEngine()));
					case 14:
						return new PaddedBufferedBlockCipher(new CbcBlockCipher(new RC2Engine()));
					}
				}
			}
			string[] array = algorithm.Split(new char[]
			{
				'/'
			});
			IBlockCipher blockCipher = null;
			IAsymmetricBlockCipher asymmetricBlockCipher = null;
			IStreamCipher streamCipher = null;
			string key2;
			if ((key2 = array[0]) != null)
			{
				if (<PrivateImplementationDetails>{BF4AB405-4DF6-41D5-BBC1-86C0068818F1}.$$method0x6000a29-2 == null)
				{
					<PrivateImplementationDetails>{BF4AB405-4DF6-41D5-BBC1-86C0068818F1}.$$method0x6000a29-2 = new Dictionary<string, int>(30)
					{
						{
							"AES",
							0
						},
						{
							"ARC4",
							1
						},
						{
							"BLOWFISH",
							2
						},
						{
							"CAMELLIA",
							3
						},
						{
							"CAST5",
							4
						},
						{
							"CAST6",
							5
						},
						{
							"DES",
							6
						},
						{
							"DESEDE",
							7
						},
						{
							"ELGAMAL",
							8
						},
						{
							"GOST28147",
							9
						},
						{
							"HC128",
							10
						},
						{
							"HC256",
							11
						},
						{
							"NOEKEON",
							12
						},
						{
							"PBEWITHSHAAND128BITRC4",
							13
						},
						{
							"PBEWITHSHAAND40BITRC4",
							14
						},
						{
							"RC2",
							15
						},
						{
							"RC5",
							16
						},
						{
							"RC5-64",
							17
						},
						{
							"RC6",
							18
						},
						{
							"RIJNDAEL",
							19
						},
						{
							"RSA",
							20
						},
						{
							"SALSA20",
							21
						},
						{
							"SEED",
							22
						},
						{
							"SERPENT",
							23
						},
						{
							"SKIPJACK",
							24
						},
						{
							"TEA",
							25
						},
						{
							"TWOFISH",
							26
						},
						{
							"VMPC",
							27
						},
						{
							"VMPC-KSA3",
							28
						},
						{
							"XTEA",
							29
						}
					};
				}
				int num2;
				if (<PrivateImplementationDetails>{BF4AB405-4DF6-41D5-BBC1-86C0068818F1}.$$method0x6000a29-2.TryGetValue(key2, out num2))
				{
					switch (num2)
					{
					case 0:
						blockCipher = new AesFastEngine();
						break;
					case 1:
						streamCipher = new RC4Engine();
						break;
					case 2:
						blockCipher = new BlowfishEngine();
						break;
					case 3:
						blockCipher = new CamelliaEngine();
						break;
					case 4:
						blockCipher = new Cast5Engine();
						break;
					case 5:
						blockCipher = new Cast6Engine();
						break;
					case 6:
						blockCipher = new DesEngine();
						break;
					case 7:
						blockCipher = new DesEdeEngine();
						break;
					case 8:
						asymmetricBlockCipher = new ElGamalEngine();
						break;
					case 9:
						blockCipher = new Gost28147Engine();
						break;
					case 10:
						streamCipher = new HC128Engine();
						break;
					case 11:
						streamCipher = new HC256Engine();
						break;
					case 12:
						blockCipher = new NoekeonEngine();
						break;
					case 13:
					case 14:
						streamCipher = new RC4Engine();
						break;
					case 15:
						blockCipher = new RC2Engine();
						break;
					case 16:
						blockCipher = new RC532Engine();
						break;
					case 17:
						blockCipher = new RC564Engine();
						break;
					case 18:
						blockCipher = new RC6Engine();
						break;
					case 19:
						blockCipher = new RijndaelEngine();
						break;
					case 20:
						asymmetricBlockCipher = new RsaBlindedEngine();
						break;
					case 21:
						streamCipher = new Salsa20Engine();
						break;
					case 22:
						blockCipher = new SeedEngine();
						break;
					case 23:
						blockCipher = new SerpentEngine();
						break;
					case 24:
						blockCipher = new SkipjackEngine();
						break;
					case 25:
						blockCipher = new TeaEngine();
						break;
					case 26:
						blockCipher = new TwofishEngine();
						break;
					case 27:
						streamCipher = new VmpcEngine();
						break;
					case 28:
						streamCipher = new VmpcKsa3Engine();
						break;
					case 29:
						blockCipher = new XteaEngine();
						break;
					default:
						goto IL_591;
					}
					if (streamCipher != null)
					{
						if (array.Length > 1)
						{
							throw new ArgumentException("Modes and paddings not used for stream ciphers");
						}
						return new BufferedStreamCipher(streamCipher);
					}
					else
					{
						bool flag = false;
						bool flag2 = true;
						IBlockCipherPadding blockCipherPadding = null;
						IAeadBlockCipher aeadBlockCipher = null;
						if (array.Length > 2)
						{
							if (streamCipher != null)
							{
								throw new ArgumentException("Paddings not used for stream ciphers");
							}
							string key3;
							switch (key3 = array[2])
							{
							case "NOPADDING":
								flag2 = false;
								goto IL_953;
							case "":
							case "RAW":
								goto IL_953;
							case "ISO10126PADDING":
							case "ISO10126D2PADDING":
							case "ISO10126-2PADDING":
								blockCipherPadding = new ISO10126d2Padding();
								goto IL_953;
							case "ISO7816-4PADDING":
							case "ISO9797-1PADDING":
								blockCipherPadding = new ISO7816d4Padding();
								goto IL_953;
							case "ISO9796-1":
							case "ISO9796-1PADDING":
								asymmetricBlockCipher = new ISO9796d1Encoding(asymmetricBlockCipher);
								goto IL_953;
							case "OAEP":
							case "OAEPPADDING":
								asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher);
								goto IL_953;
							case "OAEPWITHMD5ANDMGF1PADDING":
								asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new MD5Digest());
								goto IL_953;
							case "OAEPWITHSHA1ANDMGF1PADDING":
							case "OAEPWITHSHA-1ANDMGF1PADDING":
								asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new Sha1Digest());
								goto IL_953;
							case "OAEPWITHSHA224ANDMGF1PADDING":
							case "OAEPWITHSHA-224ANDMGF1PADDING":
								asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new Sha224Digest());
								goto IL_953;
							case "OAEPWITHSHA256ANDMGF1PADDING":
							case "OAEPWITHSHA-256ANDMGF1PADDING":
								asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new Sha256Digest());
								goto IL_953;
							case "OAEPWITHSHA384ANDMGF1PADDING":
							case "OAEPWITHSHA-384ANDMGF1PADDING":
								asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new Sha384Digest());
								goto IL_953;
							case "OAEPWITHSHA512ANDMGF1PADDING":
							case "OAEPWITHSHA-512ANDMGF1PADDING":
								asymmetricBlockCipher = new OaepEncoding(asymmetricBlockCipher, new Sha512Digest());
								goto IL_953;
							case "PKCS1":
							case "PKCS1PADDING":
								asymmetricBlockCipher = new Pkcs1Encoding(asymmetricBlockCipher);
								goto IL_953;
							case "PKCS5":
							case "PKCS5PADDING":
							case "PKCS7":
							case "PKCS7PADDING":
								blockCipherPadding = new Pkcs7Padding();
								goto IL_953;
							case "TBCPADDING":
								blockCipherPadding = new TbcPadding();
								goto IL_953;
							case "WITHCTS":
								flag = true;
								goto IL_953;
							case "X9.23PADDING":
							case "X923PADDING":
								blockCipherPadding = new X923Padding();
								goto IL_953;
							case "ZEROBYTEPADDING":
								blockCipherPadding = new ZeroBytePadding();
								goto IL_953;
							}
							throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
						}
						IL_953:
						if (array.Length > 1)
						{
							string text2 = array[1];
							int digitIndex = CipherUtilities.GetDigitIndex(text2);
							string text3 = (digitIndex >= 0) ? text2.Substring(0, digitIndex) : text2;
							string key4;
							switch (key4 = text3)
							{
							case "":
							case "ECB":
							case "NONE":
								goto IL_B95;
							case "CBC":
								blockCipher = new CbcBlockCipher(blockCipher);
								goto IL_B95;
							case "CCM":
								aeadBlockCipher = new CcmBlockCipher(blockCipher);
								goto IL_B95;
							case "CFB":
							{
								int bitBlockSize = (digitIndex < 0) ? (8 * blockCipher.GetBlockSize()) : int.Parse(text2.Substring(digitIndex));
								blockCipher = new CfbBlockCipher(blockCipher, bitBlockSize);
								goto IL_B95;
							}
							case "CTR":
								blockCipher = new SicBlockCipher(blockCipher);
								goto IL_B95;
							case "CTS":
								flag = true;
								blockCipher = new CbcBlockCipher(blockCipher);
								goto IL_B95;
							case "EAX":
								aeadBlockCipher = new EaxBlockCipher(blockCipher);
								goto IL_B95;
							case "GCM":
								aeadBlockCipher = new GcmBlockCipher(blockCipher);
								goto IL_B95;
							case "GOFB":
								blockCipher = new GOfbBlockCipher(blockCipher);
								goto IL_B95;
							case "OFB":
							{
								int blockSize = (digitIndex < 0) ? (8 * blockCipher.GetBlockSize()) : int.Parse(text2.Substring(digitIndex));
								blockCipher = new OfbBlockCipher(blockCipher, blockSize);
								goto IL_B95;
							}
							case "OPENPGPCFB":
								blockCipher = new OpenPgpCfbBlockCipher(blockCipher);
								goto IL_B95;
							case "SIC":
								if (blockCipher.GetBlockSize() < 16)
								{
									throw new ArgumentException("Warning: SIC-Mode can become a twotime-pad if the blocksize of the cipher is too small. Use a cipher with a block size of at least 128 bits (e.g. AES)");
								}
								blockCipher = new SicBlockCipher(blockCipher);
								goto IL_B95;
							}
							throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
						}
						IL_B95:
						if (aeadBlockCipher != null)
						{
							if (flag)
							{
								throw new SecurityUtilityException("CTS mode not valid for AEAD ciphers.");
							}
							if (flag2 && array.Length > 2 && array[2] != "")
							{
								throw new SecurityUtilityException("Bad padding specified for AEAD cipher.");
							}
							return new BufferedAeadBlockCipher(aeadBlockCipher);
						}
						else if (blockCipher != null)
						{
							if (flag)
							{
								return new CtsBlockCipher(blockCipher);
							}
							if (blockCipherPadding != null)
							{
								return new PaddedBufferedBlockCipher(blockCipher, blockCipherPadding);
							}
							if (!flag2 || blockCipher.IsPartialBlockOkay)
							{
								return new BufferedBlockCipher(blockCipher);
							}
							return new PaddedBufferedBlockCipher(blockCipher);
						}
						else
						{
							if (asymmetricBlockCipher != null)
							{
								return new BufferedAsymmetricBlockCipher(asymmetricBlockCipher);
							}
							throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
						}
					}
				}
			}
			IL_591:
			throw new SecurityUtilityException("Cipher " + algorithm + " not recognised.");
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00036B53 File Offset: 0x00035B53
		public static string GetAlgorithmName(DerObjectIdentifier oid)
		{
			return (string)CipherUtilities.algorithms[oid.Id];
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00036B6C File Offset: 0x00035B6C
		private static int GetDigitIndex(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (char.IsDigit(s[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04000853 RID: 2131
		private static readonly Hashtable algorithms = new Hashtable();

		// Token: 0x04000854 RID: 2132
		private static readonly Hashtable oids = new Hashtable();
	}
}

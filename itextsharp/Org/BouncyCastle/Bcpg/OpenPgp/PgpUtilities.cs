using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000545 RID: 1349
	public sealed class PgpUtilities
	{
		// Token: 0x06002E63 RID: 11875 RVA: 0x0011EA30 File Offset: 0x0011DA30
		private PgpUtilities()
		{
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x0011EA38 File Offset: 0x0011DA38
		public static MPInteger[] DsaSigToMpi(byte[] encoding)
		{
			DerInteger derInteger;
			DerInteger derInteger2;
			try
			{
				Asn1Sequence asn1Sequence = (Asn1Sequence)Asn1Object.FromByteArray(encoding);
				derInteger = (DerInteger)asn1Sequence[0];
				derInteger2 = (DerInteger)asn1Sequence[1];
			}
			catch (IOException exception)
			{
				throw new PgpException("exception encoding signature", exception);
			}
			return new MPInteger[]
			{
				new MPInteger(derInteger.Value),
				new MPInteger(derInteger2.Value)
			};
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x0011EAB4 File Offset: 0x0011DAB4
		public static MPInteger[] RsaSigToMpi(byte[] encoding)
		{
			return new MPInteger[]
			{
				new MPInteger(new BigInteger(1, encoding))
			};
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x0011EAD8 File Offset: 0x0011DAD8
		public static string GetDigestName(HashAlgorithmTag hashAlgorithm)
		{
			switch (hashAlgorithm)
			{
			case HashAlgorithmTag.MD5:
				return "MD5";
			case HashAlgorithmTag.Sha1:
				return "SHA1";
			case HashAlgorithmTag.RipeMD160:
				return "RIPEMD160";
			case HashAlgorithmTag.MD2:
				return "MD2";
			case HashAlgorithmTag.Sha256:
				return "SHA256";
			case HashAlgorithmTag.Sha384:
				return "SHA384";
			case HashAlgorithmTag.Sha512:
				return "SHA512";
			case HashAlgorithmTag.Sha224:
				return "SHA224";
			}
			throw new PgpException("unknown hash algorithm tag in GetDigestName: " + hashAlgorithm);
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x0011EB64 File Offset: 0x0011DB64
		public static string GetSignatureName(PublicKeyAlgorithmTag keyAlgorithm, HashAlgorithmTag hashAlgorithm)
		{
			string str;
			switch (keyAlgorithm)
			{
			case PublicKeyAlgorithmTag.RsaGeneral:
			case PublicKeyAlgorithmTag.RsaSign:
				str = "RSA";
				goto IL_63;
			case PublicKeyAlgorithmTag.RsaEncrypt:
				break;
			default:
				switch (keyAlgorithm)
				{
				case PublicKeyAlgorithmTag.ElGamalEncrypt:
				case PublicKeyAlgorithmTag.ElGamalGeneral:
					str = "ElGamal";
					goto IL_63;
				case PublicKeyAlgorithmTag.Dsa:
					str = "DSA";
					goto IL_63;
				}
				break;
			}
			throw new PgpException("unknown algorithm tag in signature:" + keyAlgorithm);
			IL_63:
			return PgpUtilities.GetDigestName(hashAlgorithm) + "with" + str;
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x0011EBE8 File Offset: 0x0011DBE8
		public static string GetSymmetricCipherName(SymmetricKeyAlgorithmTag algorithm)
		{
			switch (algorithm)
			{
			case SymmetricKeyAlgorithmTag.Null:
				return null;
			case SymmetricKeyAlgorithmTag.Idea:
				return "IDEA";
			case SymmetricKeyAlgorithmTag.TripleDes:
				return "DESEDE";
			case SymmetricKeyAlgorithmTag.Cast5:
				return "CAST5";
			case SymmetricKeyAlgorithmTag.Blowfish:
				return "Blowfish";
			case SymmetricKeyAlgorithmTag.Safer:
				return "SAFER";
			case SymmetricKeyAlgorithmTag.Des:
				return "DES";
			case SymmetricKeyAlgorithmTag.Aes128:
				return "AES";
			case SymmetricKeyAlgorithmTag.Aes192:
				return "AES";
			case SymmetricKeyAlgorithmTag.Aes256:
				return "AES";
			case SymmetricKeyAlgorithmTag.Twofish:
				return "Twofish";
			default:
				throw new PgpException("unknown symmetric algorithm: " + algorithm);
			}
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x0011EC80 File Offset: 0x0011DC80
		public static int GetKeySize(SymmetricKeyAlgorithmTag algorithm)
		{
			int result;
			switch (algorithm)
			{
			case SymmetricKeyAlgorithmTag.Idea:
			case SymmetricKeyAlgorithmTag.Cast5:
			case SymmetricKeyAlgorithmTag.Blowfish:
			case SymmetricKeyAlgorithmTag.Safer:
			case SymmetricKeyAlgorithmTag.Aes128:
				result = 128;
				break;
			case SymmetricKeyAlgorithmTag.TripleDes:
			case SymmetricKeyAlgorithmTag.Aes192:
				result = 192;
				break;
			case SymmetricKeyAlgorithmTag.Des:
				result = 64;
				break;
			case SymmetricKeyAlgorithmTag.Aes256:
			case SymmetricKeyAlgorithmTag.Twofish:
				result = 256;
				break;
			default:
				throw new PgpException("unknown symmetric algorithm: " + algorithm);
			}
			return result;
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x0011ECF8 File Offset: 0x0011DCF8
		public static KeyParameter MakeKey(SymmetricKeyAlgorithmTag algorithm, byte[] keyBytes)
		{
			string symmetricCipherName = PgpUtilities.GetSymmetricCipherName(algorithm);
			return ParameterUtilities.CreateKeyParameter(symmetricCipherName, keyBytes);
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x0011ED14 File Offset: 0x0011DD14
		public static KeyParameter MakeRandomKey(SymmetricKeyAlgorithmTag algorithm, SecureRandom random)
		{
			int keySize = PgpUtilities.GetKeySize(algorithm);
			byte[] array = new byte[(keySize + 7) / 8];
			random.NextBytes(array);
			return PgpUtilities.MakeKey(algorithm, array);
		}

		// Token: 0x06002E6C RID: 11884 RVA: 0x0011ED44 File Offset: 0x0011DD44
		public static KeyParameter MakeKeyFromPassPhrase(SymmetricKeyAlgorithmTag algorithm, S2k s2k, char[] passPhrase)
		{
			int keySize = PgpUtilities.GetKeySize(algorithm);
			byte[] array = Strings.ToByteArray(new string(passPhrase));
			byte[] array2 = new byte[(keySize + 7) / 8];
			int i = 0;
			int num = 0;
			while (i < array2.Length)
			{
				IDigest digest;
				if (s2k != null)
				{
					try
					{
						HashAlgorithmTag hashAlgorithm = s2k.HashAlgorithm;
						if (hashAlgorithm != HashAlgorithmTag.Sha1)
						{
							throw new PgpException("unknown hash algorithm: " + s2k.HashAlgorithm);
						}
						digest = DigestUtilities.GetDigest("SHA1");
					}
					catch (Exception exception)
					{
						throw new PgpException("can't find S2k digest", exception);
					}
					for (int num2 = 0; num2 != num; num2++)
					{
						digest.Update(0);
					}
					byte[] iv = s2k.GetIV();
					switch (s2k.Type)
					{
					case 0:
						digest.BlockUpdate(array, 0, array.Length);
						goto IL_1E3;
					case 1:
						digest.BlockUpdate(iv, 0, iv.Length);
						digest.BlockUpdate(array, 0, array.Length);
						goto IL_1E3;
					case 3:
					{
						long num3 = s2k.IterationCount;
						digest.BlockUpdate(iv, 0, iv.Length);
						digest.BlockUpdate(array, 0, array.Length);
						num3 -= (long)(iv.Length + array.Length);
						while (num3 > 0L)
						{
							if (num3 < (long)iv.Length)
							{
								digest.BlockUpdate(iv, 0, (int)num3);
								break;
							}
							digest.BlockUpdate(iv, 0, iv.Length);
							num3 -= (long)iv.Length;
							if (num3 < (long)array.Length)
							{
								digest.BlockUpdate(array, 0, (int)num3);
								num3 = 0L;
							}
							else
							{
								digest.BlockUpdate(array, 0, array.Length);
								num3 -= (long)array.Length;
							}
						}
						goto IL_1E3;
					}
					}
					throw new PgpException("unknown S2k type: " + s2k.Type);
				}
				try
				{
					digest = DigestUtilities.GetDigest("MD5");
					for (int num4 = 0; num4 != num; num4++)
					{
						digest.Update(0);
					}
					digest.BlockUpdate(array, 0, array.Length);
				}
				catch (Exception exception2)
				{
					throw new PgpException("can't find MD5 digest", exception2);
				}
				IL_1E3:
				byte[] array3 = DigestUtilities.DoFinal(digest);
				if (array3.Length > array2.Length - i)
				{
					Array.Copy(array3, 0, array2, i, array2.Length - i);
				}
				else
				{
					Array.Copy(array3, 0, array2, i, array3.Length);
				}
				i += array3.Length;
				num++;
			}
			Array.Clear(array, 0, array.Length);
			return PgpUtilities.MakeKey(algorithm, array2);
		}

		// Token: 0x06002E6D RID: 11885 RVA: 0x0011EFAC File Offset: 0x0011DFAC
		public static void WriteFileToLiteralData(Stream outputStream, char fileType, FileInfo file)
		{
			Stream stream = file.OpenRead();
			Stream stream2 = new PgpLiteralDataGenerator().Open(outputStream, fileType, file.Name, file.Length, file.LastWriteTime);
			Streams.PipeAll(stream, stream2);
			stream.Close();
			stream2.Close();
		}

		// Token: 0x06002E6E RID: 11886 RVA: 0x0011EFF4 File Offset: 0x0011DFF4
		public static void WriteFileToLiteralData(Stream outputStream, char fileType, FileInfo file, byte[] buffer)
		{
			PgpLiteralDataGenerator pgpLiteralDataGenerator = new PgpLiteralDataGenerator();
			Stream stream = pgpLiteralDataGenerator.Open(outputStream, fileType, file.Name, file.LastWriteTime, buffer);
			FileStream fileStream = file.OpenRead();
			byte[] array = new byte[buffer.Length];
			int count;
			while ((count = fileStream.Read(array, 0, array.Length)) > 0)
			{
				stream.Write(array, 0, count);
			}
			pgpLiteralDataGenerator.Close();
			fileStream.Close();
		}

		// Token: 0x06002E6F RID: 11887 RVA: 0x0011F056 File Offset: 0x0011E056
		private static bool IsPossiblyBase64(int ch)
		{
			return (ch >= 65 && ch <= 90) || (ch >= 97 && ch <= 122) || (ch >= 48 && ch <= 57) || ch == 43 || ch == 47 || ch == 13 || ch == 10;
		}

		// Token: 0x06002E70 RID: 11888 RVA: 0x0011F08C File Offset: 0x0011E08C
		public static Stream GetDecoderStream(Stream inputStream)
		{
			if (!inputStream.CanSeek)
			{
				throw new ArgumentException("inputStream must be seek-able", "inputStream");
			}
			long position = inputStream.Position;
			int num = inputStream.ReadByte();
			if ((num & 128) != 0)
			{
				inputStream.Position = position;
				return inputStream;
			}
			if (!PgpUtilities.IsPossiblyBase64(num))
			{
				inputStream.Position = position;
				return new ArmoredInputStream(inputStream);
			}
			byte[] array = new byte[60];
			int num2 = 1;
			int num3 = 1;
			array[0] = (byte)num;
			while (num2 != 60 && (num = inputStream.ReadByte()) >= 0)
			{
				if (!PgpUtilities.IsPossiblyBase64(num))
				{
					inputStream.Position = position;
					return new ArmoredInputStream(inputStream);
				}
				if (num != 10 && num != 13)
				{
					array[num3++] = (byte)num;
				}
				num2++;
			}
			inputStream.Position = position;
			if (num2 < 4)
			{
				return new ArmoredInputStream(inputStream);
			}
			byte[] array2 = new byte[8];
			Array.Copy(array, 0, array2, 0, array2.Length);
			byte[] array3 = Base64.Decode(array2);
			bool hasHeaders = (array3[0] & 128) == 0;
			return new ArmoredInputStream(inputStream, hasHeaders);
		}

		// Token: 0x0400200A RID: 8202
		private const int ReadAhead = 60;
	}
}

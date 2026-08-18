using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Security;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.DAO.FileSign.Impl.PGP
{
	// Token: 0x02000006 RID: 6
	public class PgpEncrypt
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002425 File Offset: 0x00000625
		public PgpEncrypt(PgpEncryptionKeys encryptionKeys)
		{
			if (encryptionKeys == null)
			{
				throw new ArgumentNullException("encryptionKeys", "encryptionKeys is null.");
			}
			this.mEncryptionKeys = encryptionKeys;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002448 File Offset: 0x00000648
		public static void EncryptAndSign(string pubKey, string privKey, string password, string outputFileName, string sourceFileName)
		{
			PgpEncrypt pgpEncrypt = new PgpEncrypt(new PgpEncryptionKeys(pubKey, privKey, password));
			using (Stream stream = File.OpenWrite(outputFileName))
			{
				FileInfo unencryptedFileInfo = new FileInfo(sourceFileName);
				pgpEncrypt.EncryptAndSign(stream, unencryptedFileInfo);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002498 File Offset: 0x00000698
		public static byte[] EncryptAndSign(string pubKey, string privKey, string password, byte[] sourceFileContents)
		{
			string tempFileName = FileSystem.GetTempFileName(".data");
			string tempFileName2 = FileSystem.GetTempFileName(".edata");
			File.WriteAllBytes(tempFileName, sourceFileContents);
			PgpEncrypt.EncryptAndSign(pubKey, privKey, password, tempFileName2, tempFileName);
			if (!File.Exists(tempFileName2))
			{
				return null;
			}
			return File.ReadAllBytes(tempFileName2);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024DC File Offset: 0x000006DC
		public void EncryptAndSign(Stream outputStream, FileInfo unencryptedFileInfo)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream", "outputStream is null.");
			}
			if (unencryptedFileInfo == null)
			{
				throw new ArgumentNullException("unencryptedFileInfo", "unencryptedFileInfo is null.");
			}
			if (!File.Exists(unencryptedFileInfo.FullName))
			{
				throw new ArgumentException("File to encrypt not found.");
			}
			using (Stream stream = this.chainEncryptedOut(outputStream))
			{
				using (Stream stream2 = PgpEncrypt.chainCompressedOut(stream))
				{
					PgpSignatureGenerator signatureGenerator = this.initSignatureGenerator(stream2);
					using (Stream stream3 = PgpEncrypt.chainLiteralOut(stream2, unencryptedFileInfo))
					{
						using (FileStream fileStream = unencryptedFileInfo.OpenRead())
						{
							PgpEncrypt.writeOutputAndSign(stream2, stream3, fileStream, signatureGenerator);
						}
					}
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025B8 File Offset: 0x000007B8
		private void EncryptAndSign(Stream outputStream, byte[] encryptedBytes)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream", "outputStream is null.");
			}
			if (encryptedBytes == null)
			{
				throw new ArgumentNullException("unencryptedFileInfo", "unencryptedFileInfo is null.");
			}
			using (Stream stream = this.chainEncryptedOut(outputStream))
			{
				using (Stream stream2 = PgpEncrypt.chainCompressedOut(stream))
				{
					PgpSignatureGenerator signatureGenerator = this.initSignatureGenerator(stream2);
					using (Stream stream3 = PgpEncrypt.chainLiteralOut(stream2, encryptedBytes))
					{
						using (MemoryStream memoryStream = new MemoryStream(encryptedBytes))
						{
							PgpEncrypt.writeOutputAndSign(stream2, stream3, memoryStream, signatureGenerator);
						}
					}
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000267C File Offset: 0x0000087C
		private static void writeOutputAndSign(Stream compressedOut, Stream literalOut, Stream inputFile, PgpSignatureGenerator signatureGenerator)
		{
			byte[] array = new byte[65536];
			int num;
			while ((num = inputFile.Read(array, 0, array.Length)) > 0)
			{
				literalOut.Write(array, 0, num);
				signatureGenerator.Update(array, 0, num);
			}
			signatureGenerator.Generate().Encode(compressedOut);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026C6 File Offset: 0x000008C6
		private Stream chainEncryptedOut(Stream outputStream)
		{
			PgpEncryptedDataGenerator pgpEncryptedDataGenerator = new PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag.TripleDes, new SecureRandom());
			pgpEncryptedDataGenerator.AddMethod(this.mEncryptionKeys.PublicKey);
			return pgpEncryptedDataGenerator.Open(outputStream, new byte[65536]);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026F4 File Offset: 0x000008F4
		private static Stream chainCompressedOut(Stream encryptedOut)
		{
			return new PgpCompressedDataGenerator(CompressionAlgorithmTag.Zip).Open(encryptedOut);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002702 File Offset: 0x00000902
		private static Stream chainLiteralOut(Stream compressedOut, FileInfo file)
		{
			return new PgpLiteralDataGenerator().Open(compressedOut, 'b', file);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002714 File Offset: 0x00000914
		private static Stream chainLiteralOut(Stream compressedOut, byte[] fileContents)
		{
			return new PgpLiteralDataGenerator().Open(compressedOut, 'b', "PGPLiteralData.CONSOLE", DateTime.Now.Date, fileContents);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002744 File Offset: 0x00000944
		private PgpSignatureGenerator initSignatureGenerator(Stream compressedOut)
		{
			PgpSignatureGenerator pgpSignatureGenerator = new PgpSignatureGenerator(this.mEncryptionKeys.SecretKey.PublicKey.Algorithm, HashAlgorithmTag.Sha1);
			pgpSignatureGenerator.InitSign(0, this.mEncryptionKeys.PrivateKey);
			using (IEnumerator enumerator = this.mEncryptionKeys.SecretKey.PublicKey.GetUserIds().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					string userId = (string)enumerator.Current;
					PgpSignatureSubpacketGenerator pgpSignatureSubpacketGenerator = new PgpSignatureSubpacketGenerator();
					pgpSignatureSubpacketGenerator.SetSignerUserId(false, userId);
					pgpSignatureGenerator.SetHashedSubpackets(pgpSignatureSubpacketGenerator.Generate());
				}
			}
			pgpSignatureGenerator.GenerateOnePassVersion(false).Encode(compressedOut);
			return pgpSignatureGenerator;
		}

		// Token: 0x04000006 RID: 6
		private PgpEncryptionKeys mEncryptionKeys;

		// Token: 0x04000007 RID: 7
		private const int bufferSize = 65536;
	}
}

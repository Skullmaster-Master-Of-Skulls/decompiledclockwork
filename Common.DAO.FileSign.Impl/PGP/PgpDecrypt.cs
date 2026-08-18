using System;
using System.IO;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Utilities.IO;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.DAO.FileSign.Impl.PGP
{
	// Token: 0x02000005 RID: 5
	public class PgpDecrypt
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000020E3 File Offset: 0x000002E3
		public PgpDecrypt(PgpEncryptionKeys encryptionKeys)
		{
			if (encryptionKeys == null)
			{
				throw new ArgumentNullException("encryptionKeys", "encryptionKeys is null.");
			}
			this.mEncryptionKeys = encryptionKeys;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002108 File Offset: 0x00000308
		public static void DecryptAndVerify(string pubKey, string privKey, string password, string encryptedFileName, string outputFileName)
		{
			try
			{
				PgpDecrypt pgpDecrypt = new PgpDecrypt(new PgpEncryptionKeys(pubKey, privKey, password));
				using (Stream stream = File.OpenRead(encryptedFileName))
				{
					pgpDecrypt.decryptAndVerify(stream, outputFileName);
				}
			}
			catch (Exception innerException)
			{
				throw new DecryptAndVerifyFailedException("Failed to decrypt and verify file", innerException);
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000216C File Offset: 0x0000036C
		public static byte[] DecryptAndVerify(string pubKey, string privKey, string password, byte[] encryptedFileContents)
		{
			string tempFileName = FileSystem.GetTempFileName(".data");
			string tempFileName2 = FileSystem.GetTempFileName(".edata");
			File.WriteAllBytes(tempFileName2, encryptedFileContents);
			PgpDecrypt.DecryptAndVerify(pubKey, privKey, password, tempFileName2, tempFileName);
			if (!File.Exists(tempFileName))
			{
				return null;
			}
			return File.ReadAllBytes(tempFileName);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021B0 File Offset: 0x000003B0
		private byte[] decryptAndVerify(byte[] encryptedFileContents)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (Stream stream = new MemoryStream(encryptedFileContents))
			{
				this.decryptAndVerify(stream, memoryStream);
			}
			return memoryStream.GetBuffer();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021F4 File Offset: 0x000003F4
		public void DecryptAndVerify(Stream inputStream, string outputFile)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream", "inputStream is null.");
			}
			if (outputFile == null)
			{
				throw new ArgumentNullException("outputFile", "outputFile is null.");
			}
			this.decryptAndVerify(inputStream, outputFile);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002224 File Offset: 0x00000424
		private void decryptAndVerify(Stream inputStream, Stream outputStream)
		{
			PgpPublicKeyEncryptedData publicKeyED = PgpDecrypt.extractPublicKeyEncryptedData(inputStream);
			PgpObject pgpObject = this.getClearCompressedMessage(publicKeyED);
			if (!(pgpObject is PgpCompressedData))
			{
				return;
			}
			pgpObject = PgpDecrypt.processCompressedMessage(pgpObject);
			using (Stream inputStream2 = ((PgpLiteralData)pgpObject).GetInputStream())
			{
				Streams.PipeAll(inputStream2, outputStream);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002280 File Offset: 0x00000480
		private void decryptAndVerify(Stream inputStream, string outputFilePath)
		{
			PgpPublicKeyEncryptedData publicKeyED = PgpDecrypt.extractPublicKeyEncryptedData(inputStream);
			PgpObject pgpObject = this.getClearCompressedMessage(publicKeyED);
			if (!(pgpObject is PgpCompressedData))
			{
				return;
			}
			pgpObject = PgpDecrypt.processCompressedMessage(pgpObject);
			PgpLiteralData pgpLiteralData = (PgpLiteralData)pgpObject;
			using (Stream stream = File.Create(outputFilePath))
			{
				using (Stream inputStream2 = pgpLiteralData.GetInputStream())
				{
					Streams.PipeAll(inputStream2, stream);
				}
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002300 File Offset: 0x00000500
		private static PgpObject processCompressedMessage(PgpObject message)
		{
			PgpObjectFactory compressedFactory = new PgpObjectFactory(((PgpCompressedData)message).GetDataStream());
			message = PgpDecrypt.checkforOnePassSignatureList(message, compressedFactory);
			return message;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002328 File Offset: 0x00000528
		private PgpObject getClearCompressedMessage(PgpPublicKeyEncryptedData publicKeyED)
		{
			return PgpDecrypt.getClearDataStream(this.mEncryptionKeys.PrivateKey, publicKeyED).NextPgpObject();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002340 File Offset: 0x00000540
		private static PgpPublicKeyEncryptedData extractPublicKeyEncryptedData(Stream inputStream)
		{
			return PgpDecrypt.extractPublicKey(PgpDecrypt.getEncryptedDataList(PgpUtilities.GetDecoderStream(inputStream)));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002352 File Offset: 0x00000552
		private static PgpPublicKeyEncryptedData extractPublicKeyEncryptedData(byte[] fileContents)
		{
			return PgpDecrypt.extractPublicKey(PgpDecrypt.getEncryptedDataList(PgpUtilities.GetDecoderStream(new MemoryStream(fileContents))));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002369 File Offset: 0x00000569
		private static PgpObject checkforOnePassSignatureList(PgpObject message, PgpObjectFactory compressedFactory)
		{
			message = compressedFactory.NextPgpObject();
			if (message is PgpOnePassSignatureList)
			{
				message = compressedFactory.NextPgpObject();
			}
			return message;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002384 File Offset: 0x00000584
		private static PgpObjectFactory getClearDataStream(PgpPrivateKey privateKey, PgpPublicKeyEncryptedData publicKeyED)
		{
			return new PgpObjectFactory(publicKeyED.GetDataStream(privateKey));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002394 File Offset: 0x00000594
		private static PgpPublicKeyEncryptedData extractPublicKey(PgpEncryptedDataList encryptedDataList)
		{
			PgpPublicKeyEncryptedData result = null;
			foreach (object obj in encryptedDataList.GetEncryptedDataObjects())
			{
				PgpPublicKeyEncryptedData pgpPublicKeyEncryptedData = (PgpPublicKeyEncryptedData)obj;
				if (pgpPublicKeyEncryptedData != null)
				{
					result = pgpPublicKeyEncryptedData;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023F0 File Offset: 0x000005F0
		private static PgpEncryptedDataList getEncryptedDataList(Stream encodedFile)
		{
			PgpObjectFactory pgpObjectFactory = new PgpObjectFactory(encodedFile);
			PgpObject pgpObject = pgpObjectFactory.NextPgpObject();
			if (!(pgpObject is PgpEncryptedDataList))
			{
				return (PgpEncryptedDataList)pgpObjectFactory.NextPgpObject();
			}
			return (PgpEncryptedDataList)pgpObject;
		}

		// Token: 0x04000004 RID: 4
		private PgpEncryptionKeys mEncryptionKeys;

		// Token: 0x04000005 RID: 5
		private const int bufferSize = 65536;
	}
}

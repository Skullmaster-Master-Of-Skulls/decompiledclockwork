using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Internal.Cryptography
{
	// Token: 0x02000008 RID: 8
	internal static class SymmetricImportExportExtensions
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002134 File Offset: 0x00000334
		public static CngKey ToCngKey(this byte[] key, string algorithm)
		{
			int capacity = 16 + (algorithm.Length + 1) * 2 + 12 + key.Length;
			CngKey result;
			using (MemoryStream memoryStream = new MemoryStream(capacity))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream, Encoding.Unicode))
				{
					binaryWriter.Write(16);
					binaryWriter.Write(1380470851);
					binaryWriter.Write((algorithm.Length + 1) * 2);
					binaryWriter.Write(12 + key.Length);
					binaryWriter.Write(algorithm.ToCharArray());
					binaryWriter.Write('\0');
					binaryWriter.Write(1296188491);
					binaryWriter.Write(1);
					binaryWriter.Write(key.Length);
					binaryWriter.Write(key);
				}
				byte[] keyBlob = memoryStream.ToArray();
				CngKey cngKey = CngKey.Import(keyBlob, SymmetricImportExportExtensions.s_cipherKeyBlobFormat);
				result = cngKey;
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002228 File Offset: 0x00000428
		public static byte[] GetSymmetricKeyDataIfExportable(this CngKey cngKey, string algorithm)
		{
			byte[] buffer = cngKey.Export(SymmetricImportExportExtensions.s_cipherKeyBlobFormat);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(buffer))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.Unicode))
				{
					int num = binaryReader.ReadInt32();
					if (num != 16)
					{
						throw new CryptographicException(SR.GetString("Cryptography_KeyBlobParsingError"));
					}
					int num2 = binaryReader.ReadInt32();
					if (num2 != 1380470851)
					{
						throw new CryptographicException(SR.GetString("Cryptography_KeyBlobParsingError"));
					}
					int num3 = binaryReader.ReadInt32();
					binaryReader.ReadInt32();
					string text = new string(binaryReader.ReadChars(num3 / 2 - 1));
					if (text != algorithm)
					{
						throw new CryptographicException(SR.GetString("Cryptography_CngKeyWrongAlgorithm", new object[]
						{
							text,
							algorithm
						}));
					}
					char c = binaryReader.ReadChar();
					if (c != '\0')
					{
						throw new CryptographicException(SR.GetString("Cryptography_KeyBlobParsingError"));
					}
					int num4 = binaryReader.ReadInt32();
					if (num4 != 1296188491)
					{
						throw new CryptographicException(SR.GetString("Cryptography_KeyBlobParsingError"));
					}
					int num5 = binaryReader.ReadInt32();
					if (num5 != 1)
					{
						throw new CryptographicException(SR.GetString("Cryptography_KeyBlobParsingError"));
					}
					int count = binaryReader.ReadInt32();
					byte[] array = binaryReader.ReadBytes(count);
					result = array;
				}
			}
			return result;
		}

		// Token: 0x04000050 RID: 80
		private const int SizeOf_NCRYPT_KEY_BLOB_HEADER_SIZE = 16;

		// Token: 0x04000051 RID: 81
		private const int SizeOf_BCRYPT_KEY_DATA_BLOB_HEADER = 12;

		// Token: 0x04000052 RID: 82
		private static readonly CngKeyBlobFormat s_cipherKeyBlobFormat = new CngKeyBlobFormat("CipherKeyBlob");
	}
}

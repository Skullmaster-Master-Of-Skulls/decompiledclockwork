using System;
using System.Data;

namespace EncryptionClassLibrary
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	public class BaseEncryptionClass
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002FE3 File Offset: 0x000011E3
		public BaseEncryptionClass()
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002FE3 File Offset: 0x000011E3
		public BaseEncryptionClass(byte[] key, byte[] iv)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002FF0 File Offset: 0x000011F0
		public virtual IBatchDecryptor GetBatchDecryptor()
		{
			return null;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003004 File Offset: 0x00001204
		public virtual IBatchEncryptor GetBatchEncryptor()
		{
			return null;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003018 File Offset: 0x00001218
		public virtual byte[] Encrypt(string plainText)
		{
			return null;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000302C File Offset: 0x0000122C
		public virtual string Decrypt(string inputString)
		{
			return "";
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003044 File Offset: 0x00001244
		public virtual string Decrypt(byte[] inputInBytes)
		{
			return "";
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000305C File Offset: 0x0000125C
		public static EncryptionType ParseEncryptionType(string encryptionType)
		{
			string text = encryptionType.ToLower().Trim();
			string a = text;
			EncryptionType result;
			if (!(a == "tripledes_128bit"))
			{
				if (!(a == "tripledes_192bit"))
				{
					if (!(a == "tripledes_192bit_randomiv"))
					{
						result = EncryptionType.TripleDES_192bit;
					}
					else
					{
						result = EncryptionType.TripleDES_192bit_RandomIv;
					}
				}
				else
				{
					result = EncryptionType.TripleDES_192bit;
				}
			}
			else
			{
				result = EncryptionType.TripleDES_128bit;
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000030B8 File Offset: 0x000012B8
		public virtual DataTable[] DecryptNameDataTableBatch(DataTable tSource, bool includeStudentNumberInNameDescription)
		{
			return null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000030CC File Offset: 0x000012CC
		public virtual DataTable[] DecryptNameDataTableBatch(DataTable tSource, bool includeStudentNumberInNameDescription, bool firstNameThenLastName)
		{
			return null;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000030E0 File Offset: 0x000012E0
		public virtual DataTable EncryptOrDecryptNameDataTableBatch(bool encrypt, DataTable tSource, params string[] colNamesToEncryptOrDecryptInLowerCase)
		{
			return null;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000030F4 File Offset: 0x000012F4
		public virtual DataTable EncryptColumns(DataTable t, params string[] colNames)
		{
			return null;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003108 File Offset: 0x00001308
		public virtual DataTable DecryptColumns(DataTable t, params string[] colNames)
		{
			return null;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000311B File Offset: 0x0000131B
		public virtual void BeginBatchTransaction()
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000311B File Offset: 0x0000131B
		public virtual void EndBatchTransaction()
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003120 File Offset: 0x00001320
		public virtual string BatchDecrypt(byte[] bytes)
		{
			return "";
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000311B File Offset: 0x0000131B
		public virtual void DecryptDataTableBatchDynamicData(DataTable tSource, string colSaysWhetherToEncryptOrNot, string colEncrypted, string colTextToPlaceDecryptedText)
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003138 File Offset: 0x00001338
		public virtual object[] EncryptBatch(out byte[] encryptedBytes, string stringToEncrypt, object[] oo)
		{
			encryptedBytes = null;
			return null;
		}
	}
}

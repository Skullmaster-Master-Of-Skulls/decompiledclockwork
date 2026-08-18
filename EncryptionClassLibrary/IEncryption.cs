using System;
using System.Data;
using System.Text;

namespace EncryptionClassLibrary
{
	// Token: 0x0200000C RID: 12
	public interface IEncryption
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004D RID: 77
		EncryptionType Name { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004E RID: 78
		Encoding Encoder { get; }

		// Token: 0x0600004F RID: 79
		byte[] Encrypt(string plainText);

		// Token: 0x06000050 RID: 80
		string EncryptToString(string plainText);

		// Token: 0x06000051 RID: 81
		string Decrypt(string encryptedText);

		// Token: 0x06000052 RID: 82
		string Decrypt(byte[] encryptedText);

		// Token: 0x06000053 RID: 83
		IBatchDecryptor GetBatchDecryptor();

		// Token: 0x06000054 RID: 84
		IBatchEncryptor GetBatchEncryptor();

		// Token: 0x06000055 RID: 85
		DataTable[] DecryptNameDataTableBatch(DataTable tSource, bool includeStudentNumberInNameDescription);

		// Token: 0x06000056 RID: 86
		void DecryptDataTableBatchDynamicData(DataTable tSource, string colSaysWhetherToEncryptOrNot, string colEncrypted, string colTextToPlaceDecryptedText);

		// Token: 0x06000057 RID: 87
		DataTable EncryptOrDecryptNameDataTableBatch(bool encrypt, DataTable tSource, params string[] colNamesToEncryptOrDecryptInLowerCase);

		// Token: 0x06000058 RID: 88
		DataTable EncryptColumns(DataTable tSource, params string[] colNames);

		// Token: 0x06000059 RID: 89
		DataTable DecryptColumns(DataTable tSource, params string[] colNames);

		// Token: 0x0600005A RID: 90
		DataTable[] DecryptNameDataTableBatch(DataTable tSource, bool includeStudentNumberInNameDescription, bool firstNameThenLastName);

		// Token: 0x0600005B RID: 91
		object[] EncryptBatch(out byte[] encryptedBytes, string stringToEncrypt, object[] oo);
	}
}

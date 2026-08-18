using System;
using Spire.Xls.Core.Parser.Biff_Records;

namespace Spire.Xls.Core.Spreadsheet.Security
{
	// Token: 0x02000012 RID: 18
	public interface IEncryptor
	{
		// Token: 0x060000F3 RID: 243
		void SetEncryptionInfo(byte[] docId, string password);

		// Token: 0x060000F4 RID: 244
		void Encrypt(DataProvider provider, int offset, int length, long streamPosition);

		// Token: 0x060000F5 RID: 245
		void Encrypt(byte[] data, int offset, int length, long streamPosition);

		// Token: 0x060000F6 RID: 246
		FilePassRecord GetFilePassRecord();
	}
}

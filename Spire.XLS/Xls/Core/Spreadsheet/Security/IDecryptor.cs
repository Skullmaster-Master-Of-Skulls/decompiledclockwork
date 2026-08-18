using System;
using Spire.Xls.Core.Parser.Biff_Records;

namespace Spire.Xls.Core.Spreadsheet.Security
{
	// Token: 0x02000011 RID: 17
	public interface IDecryptor
	{
		// Token: 0x060000F0 RID: 240
		void Decrypt(DataProvider provider, int offset, int length, long streamPosition);

		// Token: 0x060000F1 RID: 241
		void Decrypt(byte[] buffer, int offset, int length);

		// Token: 0x060000F2 RID: 242
		bool SetDecryptionInfo(byte[] docId, byte[] encryptedDocId, byte[] digest, string password);
	}
}

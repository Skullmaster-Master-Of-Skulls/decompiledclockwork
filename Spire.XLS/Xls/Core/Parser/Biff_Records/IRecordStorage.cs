using System;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls.Core.Parser.Biff_Records
{
	// Token: 0x020001F4 RID: 500
	public interface IRecordStorage
	{
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06001C71 RID: 7281
		TBIFFRecord TypeCode { get; }

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06001C72 RID: 7282
		int RecordCode { get; }

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06001C73 RID: 7283
		bool NeedDataArray { get; }

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06001C74 RID: 7284
		// (set) Token: 0x06001C75 RID: 7285
		long StreamPos { get; set; }

		// Token: 0x06001C76 RID: 7286
		int GetStoreSize(ExcelVersion version);

		// Token: 0x06001C77 RID: 7287
		int FillStream(BinaryWriter writer, DataProvider provider, IEncryptor encryptor, int streamPosition);
	}
}

using System;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x0200001E RID: 30
	public interface IExtendedDataRecord : IDataRecord
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000214 RID: 532
		DataRecordInfo DataRecordInfo { get; }

		// Token: 0x06000215 RID: 533
		DbDataRecord GetDataRecord(int i);

		// Token: 0x06000216 RID: 534
		DbDataReader GetDataReader(int i);
	}
}

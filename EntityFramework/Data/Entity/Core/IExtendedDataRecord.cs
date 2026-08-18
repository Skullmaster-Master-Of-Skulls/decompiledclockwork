using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core
{
	// Token: 0x02000203 RID: 515
	public interface IExtendedDataRecord : IDataRecord
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600128D RID: 4749
		DataRecordInfo DataRecordInfo { get; }

		// Token: 0x0600128E RID: 4750
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "i")]
		DbDataRecord GetDataRecord(int i);

		// Token: 0x0600128F RID: 4751
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "i")]
		DbDataReader GetDataReader(int i);
	}
}

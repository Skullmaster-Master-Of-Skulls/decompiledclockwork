using System;

namespace System.Data
{
	// Token: 0x020000A1 RID: 161
	public interface IDataReader : IDisposable, IDataRecord
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000A9C RID: 2716
		int Depth { get; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000A9D RID: 2717
		bool IsClosed { get; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000A9E RID: 2718
		int RecordsAffected { get; }

		// Token: 0x06000A9F RID: 2719
		void Close();

		// Token: 0x06000AA0 RID: 2720
		DataTable GetSchemaTable();

		// Token: 0x06000AA1 RID: 2721
		bool NextResult();

		// Token: 0x06000AA2 RID: 2722
		bool Read();
	}
}

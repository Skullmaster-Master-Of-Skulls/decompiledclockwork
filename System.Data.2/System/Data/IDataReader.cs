using System;

namespace System.Data
{
	// Token: 0x02000103 RID: 259
	public interface IDataReader : IDisposable, IDataRecord
	{
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600107D RID: 4221
		int Depth { get; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600107E RID: 4222
		bool IsClosed { get; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600107F RID: 4223
		int RecordsAffected { get; }

		// Token: 0x06001080 RID: 4224
		void Close();

		// Token: 0x06001081 RID: 4225
		DataTable GetSchemaTable();

		// Token: 0x06001082 RID: 4226
		bool NextResult();

		// Token: 0x06001083 RID: 4227
		bool Read();
	}
}

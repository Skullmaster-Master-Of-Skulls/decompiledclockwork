using System;

namespace System.Data
{
	// Token: 0x020000BC RID: 188
	public interface IDbCommand : IDisposable
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000C72 RID: 3186
		// (set) Token: 0x06000C73 RID: 3187
		IDbConnection Connection { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000C74 RID: 3188
		// (set) Token: 0x06000C75 RID: 3189
		IDbTransaction Transaction { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000C76 RID: 3190
		// (set) Token: 0x06000C77 RID: 3191
		string CommandText { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000C78 RID: 3192
		// (set) Token: 0x06000C79 RID: 3193
		int CommandTimeout { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000C7A RID: 3194
		// (set) Token: 0x06000C7B RID: 3195
		CommandType CommandType { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000C7C RID: 3196
		IDataParameterCollection Parameters { get; }

		// Token: 0x06000C7D RID: 3197
		void Prepare();

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000C7E RID: 3198
		// (set) Token: 0x06000C7F RID: 3199
		UpdateRowSource UpdatedRowSource { get; set; }

		// Token: 0x06000C80 RID: 3200
		void Cancel();

		// Token: 0x06000C81 RID: 3201
		IDbDataParameter CreateParameter();

		// Token: 0x06000C82 RID: 3202
		int ExecuteNonQuery();

		// Token: 0x06000C83 RID: 3203
		IDataReader ExecuteReader();

		// Token: 0x06000C84 RID: 3204
		IDataReader ExecuteReader(CommandBehavior behavior);

		// Token: 0x06000C85 RID: 3205
		object ExecuteScalar();
	}
}

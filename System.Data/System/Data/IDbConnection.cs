using System;

namespace System.Data
{
	// Token: 0x020000BD RID: 189
	public interface IDbConnection : IDisposable
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000C86 RID: 3206
		// (set) Token: 0x06000C87 RID: 3207
		string ConnectionString { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000C88 RID: 3208
		int ConnectionTimeout { get; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000C89 RID: 3209
		string Database { get; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000C8A RID: 3210
		ConnectionState State { get; }

		// Token: 0x06000C8B RID: 3211
		IDbTransaction BeginTransaction();

		// Token: 0x06000C8C RID: 3212
		IDbTransaction BeginTransaction(IsolationLevel il);

		// Token: 0x06000C8D RID: 3213
		void Close();

		// Token: 0x06000C8E RID: 3214
		void ChangeDatabase(string databaseName);

		// Token: 0x06000C8F RID: 3215
		IDbCommand CreateCommand();

		// Token: 0x06000C90 RID: 3216
		void Open();
	}
}

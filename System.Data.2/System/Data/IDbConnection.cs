using System;

namespace System.Data
{
	// Token: 0x02000106 RID: 262
	public interface IDbConnection : IDisposable
	{
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060010B1 RID: 4273
		// (set) Token: 0x060010B2 RID: 4274
		string ConnectionString { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060010B3 RID: 4275
		int ConnectionTimeout { get; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060010B4 RID: 4276
		string Database { get; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060010B5 RID: 4277
		ConnectionState State { get; }

		// Token: 0x060010B6 RID: 4278
		IDbTransaction BeginTransaction();

		// Token: 0x060010B7 RID: 4279
		IDbTransaction BeginTransaction(IsolationLevel il);

		// Token: 0x060010B8 RID: 4280
		void Close();

		// Token: 0x060010B9 RID: 4281
		void ChangeDatabase(string databaseName);

		// Token: 0x060010BA RID: 4282
		IDbCommand CreateCommand();

		// Token: 0x060010BB RID: 4283
		void Open();
	}
}

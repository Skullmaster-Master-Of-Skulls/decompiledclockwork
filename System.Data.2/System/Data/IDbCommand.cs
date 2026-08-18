using System;

namespace System.Data
{
	// Token: 0x02000105 RID: 261
	public interface IDbCommand : IDisposable
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600109D RID: 4253
		// (set) Token: 0x0600109E RID: 4254
		IDbConnection Connection { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600109F RID: 4255
		// (set) Token: 0x060010A0 RID: 4256
		IDbTransaction Transaction { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060010A1 RID: 4257
		// (set) Token: 0x060010A2 RID: 4258
		string CommandText { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060010A3 RID: 4259
		// (set) Token: 0x060010A4 RID: 4260
		int CommandTimeout { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060010A5 RID: 4261
		// (set) Token: 0x060010A6 RID: 4262
		CommandType CommandType { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060010A7 RID: 4263
		IDataParameterCollection Parameters { get; }

		// Token: 0x060010A8 RID: 4264
		void Prepare();

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060010A9 RID: 4265
		// (set) Token: 0x060010AA RID: 4266
		UpdateRowSource UpdatedRowSource { get; set; }

		// Token: 0x060010AB RID: 4267
		void Cancel();

		// Token: 0x060010AC RID: 4268
		IDbDataParameter CreateParameter();

		// Token: 0x060010AD RID: 4269
		int ExecuteNonQuery();

		// Token: 0x060010AE RID: 4270
		IDataReader ExecuteReader();

		// Token: 0x060010AF RID: 4271
		IDataReader ExecuteReader(CommandBehavior behavior);

		// Token: 0x060010B0 RID: 4272
		object ExecuteScalar();
	}
}

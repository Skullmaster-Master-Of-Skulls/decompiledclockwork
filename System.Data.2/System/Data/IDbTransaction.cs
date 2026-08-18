using System;

namespace System.Data
{
	// Token: 0x02000109 RID: 265
	public interface IDbTransaction : IDisposable
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060010CA RID: 4298
		IDbConnection Connection { get; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060010CB RID: 4299
		IsolationLevel IsolationLevel { get; }

		// Token: 0x060010CC RID: 4300
		void Commit();

		// Token: 0x060010CD RID: 4301
		void Rollback();
	}
}

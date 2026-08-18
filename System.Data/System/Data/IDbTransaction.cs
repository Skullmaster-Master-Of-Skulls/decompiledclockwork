using System;

namespace System.Data
{
	// Token: 0x020000C0 RID: 192
	public interface IDbTransaction : IDisposable
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000C9F RID: 3231
		IDbConnection Connection { get; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000CA0 RID: 3232
		IsolationLevel IsolationLevel { get; }

		// Token: 0x06000CA1 RID: 3233
		void Commit();

		// Token: 0x06000CA2 RID: 3234
		void Rollback();
	}
}

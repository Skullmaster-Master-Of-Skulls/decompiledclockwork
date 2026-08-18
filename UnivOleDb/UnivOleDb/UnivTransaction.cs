using System;

namespace UnivOleDb
{
	// Token: 0x02000015 RID: 21
	public interface UnivTransaction
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000100 RID: 256
		UnivConnection Connection { get; }

		// Token: 0x06000101 RID: 257
		void Commit();

		// Token: 0x06000102 RID: 258
		void Rollback();
	}
}

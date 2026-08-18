using System;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000248 RID: 584
	internal class Disposer : IDisposable
	{
		// Token: 0x060014A2 RID: 5282 RVA: 0x00062590 File Offset: 0x00060790
		internal Disposer(Action action)
		{
			this._action = action;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0006259F File Offset: 0x0006079F
		public void Dispose()
		{
			this._action();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000704 RID: 1796
		private readonly Action _action;
	}
}

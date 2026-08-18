using System;

namespace System.Data.Common.EntitySql
{
	// Token: 0x0200033A RID: 826
	internal class Disposer : IDisposable
	{
		// Token: 0x06003135 RID: 12597 RVA: 0x000C238D File Offset: 0x000C058D
		internal Disposer(Action action)
		{
			this._action = action;
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000C239C File Offset: 0x000C059C
		public void Dispose()
		{
			this._action();
			GC.SuppressFinalize(this);
		}

		// Token: 0x04001555 RID: 5461
		private readonly Action _action;
	}
}

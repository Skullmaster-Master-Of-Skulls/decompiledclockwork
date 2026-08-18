using System;

namespace System.Web.WebPages
{
	// Token: 0x02000078 RID: 120
	internal class DisposableAction : IDisposable
	{
		// Token: 0x0600039C RID: 924 RVA: 0x0000C435 File Offset: 0x0000A635
		public DisposableAction(Action action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			this._action = action;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000C452 File Offset: 0x0000A652
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000C464 File Offset: 0x0000A664
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					if (!this._hasDisposed)
					{
						this._hasDisposed = true;
						this._action();
					}
				}
			}
		}

		// Token: 0x0400010F RID: 271
		private Action _action;

		// Token: 0x04000110 RID: 272
		private bool _hasDisposed;
	}
}

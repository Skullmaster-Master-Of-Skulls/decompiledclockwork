using System;

namespace System.Web.Razor.Utils
{
	// Token: 0x02000091 RID: 145
	internal class DisposableAction : IDisposable
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x0001755D File Offset: 0x0001575D
		public DisposableAction(Action action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			this._action = action;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001757A File Offset: 0x0001577A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00017589 File Offset: 0x00015789
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._action();
			}
		}

		// Token: 0x0400032C RID: 812
		private Action _action;
	}
}

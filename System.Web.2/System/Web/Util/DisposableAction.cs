using System;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x020001E1 RID: 481
	internal sealed class DisposableAction : IDisposable
	{
		// Token: 0x060017B0 RID: 6064 RVA: 0x0004A462 File Offset: 0x00048662
		public DisposableAction(Action disposeAction)
		{
			this._disposeAction = disposeAction;
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0004A474 File Offset: 0x00048674
		public void Dispose()
		{
			Action action = Interlocked.Exchange<Action>(ref this._disposeAction, null);
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x04001725 RID: 5925
		public static readonly DisposableAction Empty = new DisposableAction(null);

		// Token: 0x04001726 RID: 5926
		private Action _disposeAction;
	}
}

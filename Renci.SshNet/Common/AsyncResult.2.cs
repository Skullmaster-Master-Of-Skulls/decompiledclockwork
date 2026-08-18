using System;

namespace Renci.SshNet.Common
{
	// Token: 0x020000E5 RID: 229
	public abstract class AsyncResult<TResult> : AsyncResult
	{
		// Token: 0x060009B4 RID: 2484 RVA: 0x0000EA81 File Offset: 0x0000CC81
		protected AsyncResult(AsyncCallback asyncCallback, object state) : base(asyncCallback, state)
		{
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002055A File Offset: 0x0001E75A
		public void SetAsCompleted(TResult result, bool completedSynchronously)
		{
			this._result = result;
			base.SetAsCompleted(null, completedSynchronously);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002056B File Offset: 0x0001E76B
		public new TResult EndInvoke()
		{
			base.EndInvoke();
			return this._result;
		}

		// Token: 0x040003CE RID: 974
		private TResult _result;
	}
}

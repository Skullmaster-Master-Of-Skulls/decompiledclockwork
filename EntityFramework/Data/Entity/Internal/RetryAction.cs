using System;
using System.Diagnostics;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000799 RID: 1945
	internal class RetryAction<TInput>
	{
		// Token: 0x06005821 RID: 22561 RVA: 0x0017B214 File Offset: 0x00179414
		public RetryAction(Action<TInput> action)
		{
			this._action = action;
		}

		// Token: 0x06005822 RID: 22562 RVA: 0x0017B230 File Offset: 0x00179430
		[DebuggerStepThrough]
		public void PerformAction(TInput input)
		{
			lock (this._lock)
			{
				if (this._action != null)
				{
					Action<TInput> action = this._action;
					this._action = null;
					try
					{
						action(input);
					}
					catch (Exception)
					{
						this._action = action;
						throw;
					}
				}
			}
		}

		// Token: 0x0400235E RID: 9054
		private readonly object _lock = new object();

		// Token: 0x0400235F RID: 9055
		private Action<TInput> _action;
	}
}

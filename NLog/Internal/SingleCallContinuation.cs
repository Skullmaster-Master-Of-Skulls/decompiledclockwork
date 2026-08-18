using System;
using System.Threading;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x020000AD RID: 173
	internal class SingleCallContinuation
	{
		// Token: 0x0600055D RID: 1373 RVA: 0x0000C207 File Offset: 0x0000A407
		public SingleCallContinuation(AsyncContinuation asyncContinuation)
		{
			this.asyncContinuation = asyncContinuation;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000C218 File Offset: 0x0000A418
		public void Function(Exception exception)
		{
			try
			{
				AsyncContinuation asyncContinuation = Interlocked.Exchange<AsyncContinuation>(ref this.asyncContinuation, null);
				if (asyncContinuation != null)
				{
					asyncContinuation(exception);
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "Exception in asynchronous handler.");
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
		}

		// Token: 0x04000120 RID: 288
		private AsyncContinuation asyncContinuation;
	}
}

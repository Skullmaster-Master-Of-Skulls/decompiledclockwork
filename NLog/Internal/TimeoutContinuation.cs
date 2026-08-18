using System;
using System.Threading;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x020000B6 RID: 182
	internal class TimeoutContinuation : IDisposable
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x0000C4B2 File Offset: 0x0000A6B2
		public TimeoutContinuation(AsyncContinuation asyncContinuation, TimeSpan timeout)
		{
			this.asyncContinuation = asyncContinuation;
			this.timeoutTimer = new Timer(new TimerCallback(this.TimerElapsed), null, timeout, TimeSpan.FromMilliseconds(-1.0));
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
		public void Function(Exception exception)
		{
			try
			{
				this.StopTimer();
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

		// Token: 0x06000578 RID: 1400 RVA: 0x0000C53C File Offset: 0x0000A73C
		public void Dispose()
		{
			this.StopTimer();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000C54C File Offset: 0x0000A74C
		private void StopTimer()
		{
			lock (this)
			{
				if (this.timeoutTimer != null)
				{
					this.timeoutTimer.Dispose();
					this.timeoutTimer = null;
				}
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000C59C File Offset: 0x0000A79C
		private void TimerElapsed(object state)
		{
			this.Function(new TimeoutException("Timeout."));
		}

		// Token: 0x04000126 RID: 294
		private AsyncContinuation asyncContinuation;

		// Token: 0x04000127 RID: 295
		private Timer timeoutTimer;
	}
}

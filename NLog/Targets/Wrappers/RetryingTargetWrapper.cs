using System;
using System.ComponentModel;
using System.Threading;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000183 RID: 387
	[Target("RetryingWrapper", IsWrapper = true)]
	public class RetryingTargetWrapper : WrapperTargetBase
	{
		// Token: 0x06000E76 RID: 3702 RVA: 0x000233EF File Offset: 0x000215EF
		public RetryingTargetWrapper() : this(null, 3, 100)
		{
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x000233FB File Offset: 0x000215FB
		public RetryingTargetWrapper(string name, Target wrappedTarget, int retryCount, int retryDelayMilliseconds) : this(wrappedTarget, retryCount, retryDelayMilliseconds)
		{
			base.Name = name;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0002340E File Offset: 0x0002160E
		public RetryingTargetWrapper(Target wrappedTarget, int retryCount, int retryDelayMilliseconds)
		{
			base.WrappedTarget = wrappedTarget;
			this.RetryCount = retryCount;
			this.RetryDelayMilliseconds = retryDelayMilliseconds;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0002342B File Offset: 0x0002162B
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x00023433 File Offset: 0x00021633
		[DefaultValue(3)]
		public int RetryCount { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0002343C File Offset: 0x0002163C
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x00023444 File Offset: 0x00021644
		[DefaultValue(100)]
		public int RetryDelayMilliseconds { get; set; }

		// Token: 0x06000E7D RID: 3709 RVA: 0x00023528 File Offset: 0x00021728
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			AsyncContinuation continuation = null;
			int counter = 0;
			continuation = delegate(Exception ex)
			{
				if (ex == null)
				{
					logEvent.Continuation(null);
					return;
				}
				int num = Interlocked.Increment(ref counter);
				InternalLogger.Warn("Error while writing to '{0}': {1}. Try {2}/{3}", new object[]
				{
					this.WrappedTarget,
					ex,
					num,
					this.RetryCount
				});
				if (num >= this.RetryCount)
				{
					InternalLogger.Warn("Too many retries. Aborting.");
					logEvent.Continuation(ex);
					return;
				}
				Thread.Sleep(this.RetryDelayMilliseconds);
				this.WrappedTarget.WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(continuation));
			};
			base.WrappedTarget.WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(continuation));
		}
	}
}

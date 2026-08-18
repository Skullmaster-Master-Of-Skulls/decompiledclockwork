using System;
using System.Collections.Generic;
using System.Threading;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000187 RID: 391
	[Target("SplitGroup", IsCompound = true)]
	public class SplitGroupTarget : CompoundTargetBase
	{
		// Token: 0x06000E82 RID: 3714 RVA: 0x00023644 File Offset: 0x00021844
		public SplitGroupTarget() : this(new Target[0])
		{
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00023652 File Offset: 0x00021852
		public SplitGroupTarget(string name, params Target[] targets) : this(targets)
		{
			base.Name = name;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00023662 File Offset: 0x00021862
		public SplitGroupTarget(params Target[] targets) : base(targets)
		{
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0002368C File Offset: 0x0002188C
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			AsyncHelpers.ForEachItemSequentially<Target>(base.Targets, logEvent.Continuation, delegate(Target t, AsyncContinuation cont)
			{
				t.WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(cont));
			});
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x000236C8 File Offset: 0x000218C8
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			InternalLogger.Trace("Writing {0} events", new object[]
			{
				logEvents.Length
			});
			for (int i = 0; i < logEvents.Length; i++)
			{
				logEvents[i].Continuation = SplitGroupTarget.CountedWrap(logEvents[i].Continuation, base.Targets.Count);
			}
			foreach (Target target in base.Targets)
			{
				InternalLogger.Trace("Sending {0} events to {1}", new object[]
				{
					logEvents.Length,
					target
				});
				target.WriteAsyncLogEvents(logEvents);
			}
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00023840 File Offset: 0x00021A40
		private static AsyncContinuation CountedWrap(AsyncContinuation originalContinuation, int counter)
		{
			if (counter == 1)
			{
				return originalContinuation;
			}
			List<Exception> exceptions = new List<Exception>();
			return delegate(Exception ex)
			{
				List<Exception> exceptions;
				if (ex != null)
				{
					lock (exceptions)
					{
						exceptions.Add(ex);
					}
				}
				int num = Interlocked.Decrement(ref counter);
				if (num == 0)
				{
					Exception combinedException = AsyncHelpers.GetCombinedException(exceptions);
					InternalLogger.Trace("Combined exception: {0}", new object[]
					{
						combinedException
					});
					originalContinuation(combinedException);
					return;
				}
				InternalLogger.Trace("{0} remaining.", new object[]
				{
					num
				});
			};
		}
	}
}

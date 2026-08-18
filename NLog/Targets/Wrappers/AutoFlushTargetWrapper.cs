using System;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000177 RID: 375
	[Target("AutoFlushWrapper", IsWrapper = true)]
	public class AutoFlushTargetWrapper : WrapperTargetBase
	{
		// Token: 0x06000E1B RID: 3611 RVA: 0x000225AC File Offset: 0x000207AC
		public AutoFlushTargetWrapper() : this(null)
		{
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x000225B5 File Offset: 0x000207B5
		public AutoFlushTargetWrapper(string name, Target wrappedTarget) : this(wrappedTarget)
		{
			base.Name = name;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000225C5 File Offset: 0x000207C5
		public AutoFlushTargetWrapper(Target wrappedTarget)
		{
			base.WrappedTarget = wrappedTarget;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x000225D4 File Offset: 0x000207D4
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			base.WrappedTarget.WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(AsyncHelpers.PrecededBy(logEvent.Continuation, new AsynchronousAction(base.WrappedTarget.Flush))));
		}
	}
}

using System;
using System.ComponentModel;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000182 RID: 386
	[Target("RepeatingWrapper", IsWrapper = true)]
	public class RepeatingTargetWrapper : WrapperTargetBase
	{
		// Token: 0x06000E70 RID: 3696 RVA: 0x0002333C File Offset: 0x0002153C
		public RepeatingTargetWrapper() : this(null, 3)
		{
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00023346 File Offset: 0x00021546
		public RepeatingTargetWrapper(string name, Target wrappedTarget, int repeatCount) : this(wrappedTarget, repeatCount)
		{
			base.Name = name;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00023357 File Offset: 0x00021557
		public RepeatingTargetWrapper(Target wrappedTarget, int repeatCount)
		{
			base.WrappedTarget = wrappedTarget;
			this.RepeatCount = repeatCount;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0002336D File Offset: 0x0002156D
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x00023375 File Offset: 0x00021575
		[DefaultValue(3)]
		public int RepeatCount { get; set; }

		// Token: 0x06000E75 RID: 3701 RVA: 0x000233AC File Offset: 0x000215AC
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			AsyncHelpers.Repeat(this.RepeatCount, logEvent.Continuation, delegate(AsyncContinuation cont)
			{
				this.WrappedTarget.WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(cont));
			});
		}
	}
}

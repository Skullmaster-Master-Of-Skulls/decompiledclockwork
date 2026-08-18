using System;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x0200017A RID: 378
	[Target("FallbackGroup", IsCompound = true)]
	public class FallbackGroupTarget : CompoundTargetBase
	{
		// Token: 0x06000E36 RID: 3638 RVA: 0x000229B8 File Offset: 0x00020BB8
		public FallbackGroupTarget() : this(new Target[0])
		{
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000229C6 File Offset: 0x00020BC6
		public FallbackGroupTarget(string name, params Target[] targets) : this(targets)
		{
			base.Name = name;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000229D6 File Offset: 0x00020BD6
		public FallbackGroupTarget(params Target[] targets) : base(targets)
		{
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x000229EA File Offset: 0x00020BEA
		// (set) Token: 0x06000E3A RID: 3642 RVA: 0x000229F2 File Offset: 0x00020BF2
		public bool ReturnToFirstOnSuccess { get; set; }

		// Token: 0x06000E3B RID: 3643 RVA: 0x00022BC8 File Offset: 0x00020DC8
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			AsyncContinuation continuation = null;
			int tryCounter = 0;
			int targetToInvoke;
			continuation = delegate(Exception ex)
			{
				if (ex == null)
				{
					lock (this.lockObject)
					{
						if (this.currentTarget != 0 && this.ReturnToFirstOnSuccess)
						{
							InternalLogger.Debug("Fallback: target '{0}' succeeded. Returning to the first one.", new object[]
							{
								this.Targets[this.currentTarget]
							});
							this.currentTarget = 0;
						}
					}
					logEvent.Continuation(null);
					return;
				}
				lock (this.lockObject)
				{
					InternalLogger.Warn(ex, "Fallback: target '{0}' failed. Proceeding to the next one.", new object[]
					{
						this.Targets[this.currentTarget]
					});
					this.currentTarget = (this.currentTarget + 1) % this.Targets.Count;
					tryCounter++;
					targetToInvoke = this.currentTarget;
					if (tryCounter >= this.Targets.Count)
					{
						targetToInvoke = -1;
					}
				}
				if (targetToInvoke >= 0)
				{
					this.Targets[targetToInvoke].WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(continuation));
					return;
				}
				logEvent.Continuation(ex);
			};
			lock (this.lockObject)
			{
				targetToInvoke = this.currentTarget;
			}
			base.Targets[targetToInvoke].WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(continuation));
		}

		// Token: 0x04000401 RID: 1025
		private int currentTarget;

		// Token: 0x04000402 RID: 1026
		private object lockObject = new object();
	}
}

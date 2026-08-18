using System;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000184 RID: 388
	[Target("RoundRobinGroup", IsCompound = true)]
	public class RoundRobinGroupTarget : CompoundTargetBase
	{
		// Token: 0x06000E7E RID: 3710 RVA: 0x0002358A File Offset: 0x0002178A
		public RoundRobinGroupTarget() : this(new Target[0])
		{
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00023598 File Offset: 0x00021798
		public RoundRobinGroupTarget(string name, params Target[] targets) : this(targets)
		{
			base.Name = name;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x000235A8 File Offset: 0x000217A8
		public RoundRobinGroupTarget(params Target[] targets) : base(targets)
		{
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x000235BC File Offset: 0x000217BC
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			if (base.Targets.Count == 0)
			{
				logEvent.Continuation(null);
				return;
			}
			int index;
			lock (this.lockObject)
			{
				index = this.currentTarget;
				this.currentTarget = (this.currentTarget + 1) % base.Targets.Count;
			}
			base.Targets[index].WriteAsyncLogEvent(logEvent);
		}

		// Token: 0x0400041B RID: 1051
		private int currentTarget;

		// Token: 0x0400041C RID: 1052
		private object lockObject = new object();
	}
}

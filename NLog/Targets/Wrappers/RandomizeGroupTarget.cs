using System;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000181 RID: 385
	[Target("RandomizeGroup", IsCompound = true)]
	public class RandomizeGroupTarget : CompoundTargetBase
	{
		// Token: 0x06000E6C RID: 3692 RVA: 0x00023289 File Offset: 0x00021489
		public RandomizeGroupTarget() : this(new Target[0])
		{
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00023297 File Offset: 0x00021497
		public RandomizeGroupTarget(string name, params Target[] targets) : this(targets)
		{
			base.Name = name;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x000232A7 File Offset: 0x000214A7
		public RandomizeGroupTarget(params Target[] targets) : base(targets)
		{
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x000232BC File Offset: 0x000214BC
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			if (base.Targets.Count == 0)
			{
				logEvent.Continuation(null);
				return;
			}
			int index;
			lock (this.random)
			{
				index = this.random.Next(base.Targets.Count);
			}
			base.Targets[index].WriteAsyncLogEvent(logEvent);
		}

		// Token: 0x04000417 RID: 1047
		private readonly Random random = new Random();
	}
}

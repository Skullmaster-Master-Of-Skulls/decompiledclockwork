using System;

namespace NLog.Time
{
	// Token: 0x0200018C RID: 396
	public abstract class CachedTimeSource : TimeSource
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000E97 RID: 3735
		protected abstract DateTime FreshTime { get; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x000239DC File Offset: 0x00021BDC
		public override DateTime Time
		{
			get
			{
				int tickCount = Environment.TickCount;
				if (tickCount == this.lastTicks)
				{
					return this.lastTime;
				}
				DateTime freshTime = this.FreshTime;
				this.lastTicks = tickCount;
				this.lastTime = freshTime;
				return freshTime;
			}
		}

		// Token: 0x0400042A RID: 1066
		private int lastTicks = -1;

		// Token: 0x0400042B RID: 1067
		private DateTime lastTime = DateTime.MinValue;
	}
}

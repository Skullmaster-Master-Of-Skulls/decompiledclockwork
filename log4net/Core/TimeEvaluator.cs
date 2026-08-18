using System;

namespace log4net.Core
{
	// Token: 0x02000077 RID: 119
	public class TimeEvaluator : ITriggeringEventEvaluator
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x0000E0AD File Offset: 0x0000C2AD
		public TimeEvaluator() : this(0)
		{
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000E0B6 File Offset: 0x0000C2B6
		public TimeEvaluator(int interval)
		{
			this.m_interval = interval;
			this.m_lastTimeUtc = DateTime.UtcNow;
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000E0D0 File Offset: 0x0000C2D0
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
		public int Interval
		{
			get
			{
				return this.m_interval;
			}
			set
			{
				this.m_interval = value;
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		public bool IsTriggeringEvent(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			if (this.m_interval == 0)
			{
				return false;
			}
			bool result;
			lock (this)
			{
				if (DateTime.UtcNow.Subtract(this.m_lastTimeUtc).TotalSeconds > (double)this.m_interval)
				{
					this.m_lastTimeUtc = DateTime.UtcNow;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x040001D4 RID: 468
		private const int DEFAULT_INTERVAL = 0;

		// Token: 0x040001D5 RID: 469
		private int m_interval;

		// Token: 0x040001D6 RID: 470
		private DateTime m_lastTimeUtc;
	}
}

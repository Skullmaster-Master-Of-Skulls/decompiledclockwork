using System;

namespace log4net.Core
{
	// Token: 0x02000069 RID: 105
	public class LevelEvaluator : ITriggeringEventEvaluator
	{
		// Token: 0x06000379 RID: 889 RVA: 0x0000BEF7 File Offset: 0x0000A0F7
		public LevelEvaluator() : this(Level.Off)
		{
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000BF04 File Offset: 0x0000A104
		public LevelEvaluator(Level threshold)
		{
			if (threshold == null)
			{
				throw new ArgumentNullException("threshold");
			}
			this.m_threshold = threshold;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000BF27 File Offset: 0x0000A127
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000BF2F File Offset: 0x0000A12F
		public Level Threshold
		{
			get
			{
				return this.m_threshold;
			}
			set
			{
				this.m_threshold = value;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000BF38 File Offset: 0x0000A138
		public bool IsTriggeringEvent(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			return loggingEvent.Level >= this.m_threshold;
		}

		// Token: 0x0400018E RID: 398
		private Level m_threshold;
	}
}

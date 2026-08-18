using System;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x02000084 RID: 132
	public class LevelRangeFilter : FilterSkeleton
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000E67B File Offset: 0x0000C87B
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x0000E683 File Offset: 0x0000C883
		public bool AcceptOnMatch
		{
			get
			{
				return this.m_acceptOnMatch;
			}
			set
			{
				this.m_acceptOnMatch = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0000E68C File Offset: 0x0000C88C
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x0000E694 File Offset: 0x0000C894
		public Level LevelMin
		{
			get
			{
				return this.m_levelMin;
			}
			set
			{
				this.m_levelMin = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000E69D File Offset: 0x0000C89D
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x0000E6A5 File Offset: 0x0000C8A5
		public Level LevelMax
		{
			get
			{
				return this.m_levelMax;
			}
			set
			{
				this.m_levelMax = value;
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		public override FilterDecision Decide(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			if (this.m_levelMin != null && loggingEvent.Level < this.m_levelMin)
			{
				return FilterDecision.Deny;
			}
			if (this.m_levelMax != null && loggingEvent.Level > this.m_levelMax)
			{
				return FilterDecision.Deny;
			}
			if (this.m_acceptOnMatch)
			{
				return FilterDecision.Accept;
			}
			return FilterDecision.Neutral;
		}

		// Token: 0x040001E9 RID: 489
		private bool m_acceptOnMatch = true;

		// Token: 0x040001EA RID: 490
		private Level m_levelMin;

		// Token: 0x040001EB RID: 491
		private Level m_levelMax;
	}
}

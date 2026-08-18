using System;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x02000083 RID: 131
	public class LevelMatchFilter : FilterSkeleton
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000E60C File Offset: 0x0000C80C
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0000E614 File Offset: 0x0000C814
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

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000E61D File Offset: 0x0000C81D
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0000E625 File Offset: 0x0000C825
		public Level LevelToMatch
		{
			get
			{
				return this.m_levelToMatch;
			}
			set
			{
				this.m_levelToMatch = value;
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000E62E File Offset: 0x0000C82E
		public override FilterDecision Decide(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			if (!(this.m_levelToMatch != null) || !(this.m_levelToMatch == loggingEvent.Level))
			{
				return FilterDecision.Neutral;
			}
			if (!this.m_acceptOnMatch)
			{
				return FilterDecision.Deny;
			}
			return FilterDecision.Accept;
		}

		// Token: 0x040001E7 RID: 487
		private bool m_acceptOnMatch = true;

		// Token: 0x040001E8 RID: 488
		private Level m_levelToMatch;
	}
}

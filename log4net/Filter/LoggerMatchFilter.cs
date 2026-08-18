using System;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x02000085 RID: 133
	public class LoggerMatchFilter : FilterSkeleton
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000E72B File Offset: 0x0000C92B
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x0000E733 File Offset: 0x0000C933
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

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000E73C File Offset: 0x0000C93C
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x0000E744 File Offset: 0x0000C944
		public string LoggerToMatch
		{
			get
			{
				return this.m_loggerToMatch;
			}
			set
			{
				this.m_loggerToMatch = value;
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000E750 File Offset: 0x0000C950
		public override FilterDecision Decide(LoggingEvent loggingEvent)
		{
			if (loggingEvent == null)
			{
				throw new ArgumentNullException("loggingEvent");
			}
			if (this.m_loggerToMatch == null || this.m_loggerToMatch.Length == 0 || !loggingEvent.LoggerName.StartsWith(this.m_loggerToMatch))
			{
				return FilterDecision.Neutral;
			}
			if (this.m_acceptOnMatch)
			{
				return FilterDecision.Accept;
			}
			return FilterDecision.Deny;
		}

		// Token: 0x040001EC RID: 492
		private bool m_acceptOnMatch = true;

		// Token: 0x040001ED RID: 493
		private string m_loggerToMatch;
	}
}

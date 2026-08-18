using System;
using log4net.Core;

namespace log4net.Filter
{
	// Token: 0x02000081 RID: 129
	public sealed class DenyAllFilter : FilterSkeleton
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x0000E5FA File Offset: 0x0000C7FA
		public override FilterDecision Decide(LoggingEvent loggingEvent)
		{
			return FilterDecision.Deny;
		}
	}
}

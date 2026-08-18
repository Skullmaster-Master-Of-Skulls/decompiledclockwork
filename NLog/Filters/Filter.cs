using System;
using NLog.Config;

namespace NLog.Filters
{
	// Token: 0x0200005C RID: 92
	[NLogConfigurationItem]
	public abstract class Filter
	{
		// Token: 0x0600021A RID: 538 RVA: 0x00008660 File Offset: 0x00006860
		protected Filter()
		{
			this.Action = FilterResult.Neutral;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000866F File Offset: 0x0000686F
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00008677 File Offset: 0x00006877
		[RequiredParameter]
		public FilterResult Action { get; set; }

		// Token: 0x0600021D RID: 541 RVA: 0x00008680 File Offset: 0x00006880
		internal FilterResult GetFilterResult(LogEventInfo logEvent)
		{
			return this.Check(logEvent);
		}

		// Token: 0x0600021E RID: 542
		protected abstract FilterResult Check(LogEventInfo logEvent);
	}
}

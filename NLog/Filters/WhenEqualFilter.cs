using System;
using System.ComponentModel;
using NLog.Config;

namespace NLog.Filters
{
	// Token: 0x02000062 RID: 98
	[Filter("whenEqual")]
	public class WhenEqualFilter : LayoutBasedFilter
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00008769 File Offset: 0x00006969
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00008771 File Offset: 0x00006971
		[DefaultValue(false)]
		public bool IgnoreCase { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000877A File Offset: 0x0000697A
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00008782 File Offset: 0x00006982
		[RequiredParameter]
		public string CompareTo { get; set; }

		// Token: 0x06000232 RID: 562 RVA: 0x0000878C File Offset: 0x0000698C
		protected override FilterResult Check(LogEventInfo logEvent)
		{
			StringComparison comparisonType = this.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			if (base.Layout.Render(logEvent).Equals(this.CompareTo, comparisonType))
			{
				return base.Action;
			}
			return FilterResult.Neutral;
		}
	}
}

using System;
using System.ComponentModel;
using NLog.Config;

namespace NLog.Filters
{
	// Token: 0x02000061 RID: 97
	[Filter("whenContains")]
	public class WhenContainsFilter : LayoutBasedFilter
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00008702 File Offset: 0x00006902
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000870A File Offset: 0x0000690A
		[DefaultValue(false)]
		public bool IgnoreCase { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00008713 File Offset: 0x00006913
		// (set) Token: 0x0600022B RID: 555 RVA: 0x0000871B File Offset: 0x0000691B
		[RequiredParameter]
		public string Substring { get; set; }

		// Token: 0x0600022C RID: 556 RVA: 0x00008724 File Offset: 0x00006924
		protected override FilterResult Check(LogEventInfo logEvent)
		{
			StringComparison comparisonType = this.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			if (base.Layout.Render(logEvent).IndexOf(this.Substring, comparisonType) >= 0)
			{
				return base.Action;
			}
			return FilterResult.Neutral;
		}
	}
}

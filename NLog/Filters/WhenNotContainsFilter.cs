using System;
using System.ComponentModel;
using NLog.Config;

namespace NLog.Filters
{
	// Token: 0x02000063 RID: 99
	[Filter("whenNotContains")]
	public class WhenNotContainsFilter : LayoutBasedFilter
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000234 RID: 564 RVA: 0x000087D0 File Offset: 0x000069D0
		// (set) Token: 0x06000235 RID: 565 RVA: 0x000087D8 File Offset: 0x000069D8
		[RequiredParameter]
		public string Substring { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000236 RID: 566 RVA: 0x000087E1 File Offset: 0x000069E1
		// (set) Token: 0x06000237 RID: 567 RVA: 0x000087E9 File Offset: 0x000069E9
		[DefaultValue(false)]
		public bool IgnoreCase { get; set; }

		// Token: 0x06000238 RID: 568 RVA: 0x000087F4 File Offset: 0x000069F4
		protected override FilterResult Check(LogEventInfo logEvent)
		{
			StringComparison comparisonType = this.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			string text = base.Layout.Render(logEvent);
			if (text.IndexOf(this.Substring, comparisonType) < 0)
			{
				return base.Action;
			}
			return FilterResult.Neutral;
		}
	}
}

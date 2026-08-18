using System;
using System.ComponentModel;
using NLog.Config;

namespace NLog.Filters
{
	// Token: 0x02000064 RID: 100
	[Filter("whenNotEqual")]
	public class WhenNotEqualFilter : LayoutBasedFilter
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00008843 File Offset: 0x00006A43
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000884B File Offset: 0x00006A4B
		[RequiredParameter]
		public string CompareTo { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00008854 File Offset: 0x00006A54
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000885C File Offset: 0x00006A5C
		[DefaultValue(false)]
		public bool IgnoreCase { get; set; }

		// Token: 0x0600023F RID: 575 RVA: 0x00008868 File Offset: 0x00006A68
		protected override FilterResult Check(LogEventInfo logEvent)
		{
			StringComparison comparisonType = this.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			if (!base.Layout.Render(logEvent).Equals(this.CompareTo, comparisonType))
			{
				return base.Action;
			}
			return FilterResult.Neutral;
		}
	}
}

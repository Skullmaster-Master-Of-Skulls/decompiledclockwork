using System;
using NLog.Conditions;
using NLog.Config;

namespace NLog.Filters
{
	// Token: 0x0200005D RID: 93
	[Filter("when")]
	public class ConditionBasedFilter : Filter
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00008689 File Offset: 0x00006889
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00008691 File Offset: 0x00006891
		[RequiredParameter]
		public ConditionExpression Condition { get; set; }

		// Token: 0x06000221 RID: 545 RVA: 0x0000869C File Offset: 0x0000689C
		protected override FilterResult Check(LogEventInfo logEvent)
		{
			object obj = this.Condition.Evaluate(logEvent);
			if (ConditionBasedFilter.boxedTrue.Equals(obj))
			{
				return base.Action;
			}
			return FilterResult.Neutral;
		}

		// Token: 0x040000BC RID: 188
		private static readonly object boxedTrue = true;
	}
}

using System;
using NLog.Conditions;
using NLog.Config;

namespace NLog.Targets.Wrappers
{
	// Token: 0x0200017B RID: 379
	[NLogConfigurationItem]
	public class FilteringRule
	{
		// Token: 0x06000E3C RID: 3644 RVA: 0x00022C70 File Offset: 0x00020E70
		public FilteringRule() : this(null, null)
		{
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00022C7A File Offset: 0x00020E7A
		public FilteringRule(ConditionExpression whenExistsExpression, ConditionExpression filterToApply)
		{
			this.Exists = whenExistsExpression;
			this.Filter = filterToApply;
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x00022C90 File Offset: 0x00020E90
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x00022C98 File Offset: 0x00020E98
		[RequiredParameter]
		public ConditionExpression Exists { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x00022CA1 File Offset: 0x00020EA1
		// (set) Token: 0x06000E41 RID: 3649 RVA: 0x00022CA9 File Offset: 0x00020EA9
		[RequiredParameter]
		public ConditionExpression Filter { get; set; }
	}
}

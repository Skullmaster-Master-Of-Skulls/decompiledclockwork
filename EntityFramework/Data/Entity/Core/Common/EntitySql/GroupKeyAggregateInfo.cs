using System;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000253 RID: 595
	internal sealed class GroupKeyAggregateInfo : GroupAggregateInfo
	{
		// Token: 0x060014D0 RID: 5328 RVA: 0x00062ED7 File Offset: 0x000610D7
		internal GroupKeyAggregateInfo(GroupAggregateKind aggregateKind, ErrorContext errCtx, GroupAggregateInfo containingAggregate, ScopeRegion definingScopeRegion) : base(aggregateKind, null, errCtx, containingAggregate, definingScopeRegion)
		{
		}
	}
}

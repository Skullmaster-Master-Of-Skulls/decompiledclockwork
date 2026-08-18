using System;

namespace System.Data.Common.EntitySql
{
	// Token: 0x0200033F RID: 831
	internal sealed class GroupKeyAggregateInfo : GroupAggregateInfo
	{
		// Token: 0x06003148 RID: 12616 RVA: 0x000C26D5 File Offset: 0x000C08D5
		internal GroupKeyAggregateInfo(GroupAggregateKind aggregateKind, ErrorContext errCtx, GroupAggregateInfo containingAggregate, ScopeRegion definingScopeRegion) : base(aggregateKind, null, errCtx, containingAggregate, definingScopeRegion)
		{
		}
	}
}

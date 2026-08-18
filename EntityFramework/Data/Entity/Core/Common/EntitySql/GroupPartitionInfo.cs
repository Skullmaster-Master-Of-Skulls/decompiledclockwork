using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.EntitySql.AST;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000258 RID: 600
	internal sealed class GroupPartitionInfo : GroupAggregateInfo
	{
		// Token: 0x060014DC RID: 5340 RVA: 0x00062F42 File Offset: 0x00061142
		internal GroupPartitionInfo(GroupPartitionExpr groupPartitionExpr, ErrorContext errCtx, GroupAggregateInfo containingAggregate, ScopeRegion definingScopeRegion) : base(GroupAggregateKind.Partition, groupPartitionExpr, errCtx, containingAggregate, definingScopeRegion)
		{
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00062F50 File Offset: 0x00061150
		internal void AttachToAstNode(string aggregateName, DbExpression aggregateDefinition)
		{
			base.AttachToAstNode(aggregateName, aggregateDefinition.ResultType);
			this.AggregateDefinition = aggregateDefinition;
		}

		// Token: 0x04000733 RID: 1843
		internal DbExpression AggregateDefinition;
	}
}

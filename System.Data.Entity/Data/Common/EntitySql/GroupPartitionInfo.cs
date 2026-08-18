using System;
using System.Data.Common.CommandTrees;
using System.Data.Common.EntitySql.AST;

namespace System.Data.Common.EntitySql
{
	// Token: 0x0200033E RID: 830
	internal sealed class GroupPartitionInfo : GroupAggregateInfo
	{
		// Token: 0x06003146 RID: 12614 RVA: 0x000C26B1 File Offset: 0x000C08B1
		internal GroupPartitionInfo(GroupPartitionExpr groupPartitionExpr, ErrorContext errCtx, GroupAggregateInfo containingAggregate, ScopeRegion definingScopeRegion) : base(GroupAggregateKind.Partition, groupPartitionExpr, errCtx, containingAggregate, definingScopeRegion)
		{
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x000C26BF File Offset: 0x000C08BF
		internal void AttachToAstNode(string aggregateName, DbExpression aggregateDefinition)
		{
			base.AttachToAstNode(aggregateName, aggregateDefinition.ResultType);
			this.AggregateDefinition = aggregateDefinition;
		}

		// Token: 0x04001566 RID: 5478
		internal DbExpression AggregateDefinition;
	}
}

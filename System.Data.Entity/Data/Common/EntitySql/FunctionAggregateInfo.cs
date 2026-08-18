using System;
using System.Data.Common.CommandTrees;
using System.Data.Common.EntitySql.AST;

namespace System.Data.Common.EntitySql
{
	// Token: 0x0200033D RID: 829
	internal sealed class FunctionAggregateInfo : GroupAggregateInfo
	{
		// Token: 0x06003144 RID: 12612 RVA: 0x000C268D File Offset: 0x000C088D
		internal FunctionAggregateInfo(MethodExpr methodExpr, ErrorContext errCtx, GroupAggregateInfo containingAggregate, ScopeRegion definingScopeRegion) : base(GroupAggregateKind.Function, methodExpr, errCtx, containingAggregate, definingScopeRegion)
		{
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x000C269B File Offset: 0x000C089B
		internal void AttachToAstNode(string aggregateName, DbAggregate aggregateDefinition)
		{
			base.AttachToAstNode(aggregateName, aggregateDefinition.ResultType);
			this.AggregateDefinition = aggregateDefinition;
		}

		// Token: 0x04001565 RID: 5477
		internal DbAggregate AggregateDefinition;
	}
}

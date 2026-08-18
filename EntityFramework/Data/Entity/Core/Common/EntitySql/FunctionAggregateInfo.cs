using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.EntitySql.AST;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200024F RID: 591
	internal sealed class FunctionAggregateInfo : GroupAggregateInfo
	{
		// Token: 0x060014BA RID: 5306 RVA: 0x0006295D File Offset: 0x00060B5D
		internal FunctionAggregateInfo(MethodExpr methodExpr, ErrorContext errCtx, GroupAggregateInfo containingAggregate, ScopeRegion definingScopeRegion) : base(GroupAggregateKind.Function, methodExpr, errCtx, containingAggregate, definingScopeRegion)
		{
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0006296B File Offset: 0x00060B6B
		internal void AttachToAstNode(string aggregateName, DbAggregate aggregateDefinition)
		{
			base.AttachToAstNode(aggregateName, aggregateDefinition.ResultType);
			this.AggregateDefinition = aggregateDefinition;
		}

		// Token: 0x0400071A RID: 1818
		internal DbAggregate AggregateDefinition;
	}
}

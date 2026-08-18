using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000272 RID: 626
	internal sealed class ProjectionItemDefinitionScopeEntry : ScopeEntry
	{
		// Token: 0x06001606 RID: 5638 RVA: 0x0006B053 File Offset: 0x00069253
		internal ProjectionItemDefinitionScopeEntry(DbExpression expression) : base(ScopeEntryKind.ProjectionItemDefinition)
		{
			this._expression = expression;
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x0006B063 File Offset: 0x00069263
		internal override DbExpression GetExpression(string refName, ErrorContext errCtx)
		{
			return this._expression;
		}

		// Token: 0x040007B9 RID: 1977
		private readonly DbExpression _expression;
	}
}

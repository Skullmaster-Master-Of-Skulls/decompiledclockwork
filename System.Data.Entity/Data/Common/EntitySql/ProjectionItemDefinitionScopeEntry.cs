using System;
using System.Data.Common.CommandTrees;

namespace System.Data.Common.EntitySql
{
	// Token: 0x0200034B RID: 843
	internal sealed class ProjectionItemDefinitionScopeEntry : ScopeEntry
	{
		// Token: 0x0600317F RID: 12671 RVA: 0x000C2BCC File Offset: 0x000C0DCC
		internal ProjectionItemDefinitionScopeEntry(DbExpression expression) : base(ScopeEntryKind.ProjectionItemDefinition)
		{
			this._expression = expression;
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x000C2BDC File Offset: 0x000C0DDC
		internal override DbExpression GetExpression(string refName, ErrorContext errCtx)
		{
			return this._expression;
		}

		// Token: 0x04001584 RID: 5508
		private readonly DbExpression _expression;
	}
}

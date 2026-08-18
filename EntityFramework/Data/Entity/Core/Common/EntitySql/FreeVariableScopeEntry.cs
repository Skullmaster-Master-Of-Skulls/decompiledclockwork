using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000273 RID: 627
	internal sealed class FreeVariableScopeEntry : ScopeEntry
	{
		// Token: 0x06001608 RID: 5640 RVA: 0x0006B06B File Offset: 0x0006926B
		internal FreeVariableScopeEntry(DbVariableReferenceExpression varRef) : base(ScopeEntryKind.FreeVar)
		{
			this._varRef = varRef;
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x0006B07B File Offset: 0x0006927B
		internal override DbExpression GetExpression(string refName, ErrorContext errCtx)
		{
			return this._varRef;
		}

		// Token: 0x040007BA RID: 1978
		private readonly DbVariableReferenceExpression _varRef;
	}
}

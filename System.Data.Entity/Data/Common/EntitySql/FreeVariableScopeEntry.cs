using System;
using System.Data.Common.CommandTrees;

namespace System.Data.Common.EntitySql
{
	// Token: 0x0200034C RID: 844
	internal sealed class FreeVariableScopeEntry : ScopeEntry
	{
		// Token: 0x06003181 RID: 12673 RVA: 0x000C2BE4 File Offset: 0x000C0DE4
		internal FreeVariableScopeEntry(DbVariableReferenceExpression varRef) : base(ScopeEntryKind.FreeVar)
		{
			this._varRef = varRef;
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x000C2BF4 File Offset: 0x000C0DF4
		internal override DbExpression GetExpression(string refName, ErrorContext errCtx)
		{
			return this._varRef;
		}

		// Token: 0x04001585 RID: 5509
		private readonly DbVariableReferenceExpression _varRef;
	}
}

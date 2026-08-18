using System;
using System.Data.Common.CommandTrees;
using System.Data.Entity;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000349 RID: 841
	internal sealed class InvalidGroupInputRefScopeEntry : ScopeEntry
	{
		// Token: 0x06003178 RID: 12664 RVA: 0x000C2B6F File Offset: 0x000C0D6F
		internal InvalidGroupInputRefScopeEntry() : base(ScopeEntryKind.InvalidGroupInputRef)
		{
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000C2B78 File Offset: 0x000C0D78
		internal override DbExpression GetExpression(string refName, ErrorContext errCtx)
		{
			throw EntityUtil.EntitySqlError(errCtx, Strings.InvalidGroupIdentifierReference(refName));
		}
	}
}

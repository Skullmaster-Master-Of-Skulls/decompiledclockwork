using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200025D RID: 605
	internal sealed class InvalidGroupInputRefScopeEntry : ScopeEntry
	{
		// Token: 0x060014EB RID: 5355 RVA: 0x00063003 File Offset: 0x00061203
		internal InvalidGroupInputRefScopeEntry() : base(ScopeEntryKind.InvalidGroupInputRef)
		{
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0006300C File Offset: 0x0006120C
		internal override DbExpression GetExpression(string refName, ErrorContext errCtx)
		{
			string errorMessage = Strings.InvalidGroupIdentifierReference(refName);
			throw EntitySqlException.Create(errCtx, errorMessage, null);
		}
	}
}

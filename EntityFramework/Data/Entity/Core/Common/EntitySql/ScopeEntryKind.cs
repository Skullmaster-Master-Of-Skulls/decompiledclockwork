using System;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000268 RID: 616
	internal enum ScopeEntryKind
	{
		// Token: 0x0400074F RID: 1871
		SourceVar,
		// Token: 0x04000750 RID: 1872
		GroupKeyDefinition,
		// Token: 0x04000751 RID: 1873
		ProjectionItemDefinition,
		// Token: 0x04000752 RID: 1874
		FreeVar,
		// Token: 0x04000753 RID: 1875
		InvalidGroupInputRef
	}
}

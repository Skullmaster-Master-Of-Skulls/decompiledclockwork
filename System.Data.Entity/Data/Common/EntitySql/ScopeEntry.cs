using System;
using System.Data.Common.CommandTrees;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000345 RID: 837
	internal abstract class ScopeEntry
	{
		// Token: 0x06003166 RID: 12646 RVA: 0x000C29B4 File Offset: 0x000C0BB4
		internal ScopeEntry(ScopeEntryKind scopeEntryKind)
		{
			this._scopeEntryKind = scopeEntryKind;
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x000C29C3 File Offset: 0x000C0BC3
		internal ScopeEntryKind EntryKind
		{
			get
			{
				return this._scopeEntryKind;
			}
		}

		// Token: 0x06003168 RID: 12648
		internal abstract DbExpression GetExpression(string refName, ErrorContext errCtx);

		// Token: 0x04001579 RID: 5497
		private readonly ScopeEntryKind _scopeEntryKind;
	}
}

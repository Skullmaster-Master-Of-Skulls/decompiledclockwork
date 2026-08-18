using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000254 RID: 596
	internal abstract class ScopeEntry
	{
		// Token: 0x060014D1 RID: 5329 RVA: 0x00062EE5 File Offset: 0x000610E5
		internal ScopeEntry(ScopeEntryKind scopeEntryKind)
		{
			this._scopeEntryKind = scopeEntryKind;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x00062EF4 File Offset: 0x000610F4
		internal ScopeEntryKind EntryKind
		{
			get
			{
				return this._scopeEntryKind;
			}
		}

		// Token: 0x060014D3 RID: 5331
		internal abstract DbExpression GetExpression(string refName, ErrorContext errCtx);

		// Token: 0x0400072E RID: 1838
		private readonly ScopeEntryKind _scopeEntryKind;
	}
}

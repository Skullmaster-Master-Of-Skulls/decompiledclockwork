using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000274 RID: 628
	internal sealed class ScopeManager
	{
		// Token: 0x0600160A RID: 5642 RVA: 0x0006B083 File Offset: 0x00069283
		internal ScopeManager(IEqualityComparer<string> keyComparer)
		{
			this._keyComparer = keyComparer;
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x0006B09D File Offset: 0x0006929D
		internal void EnterScope()
		{
			this._scopes.Add(new Scope(this._keyComparer));
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0006B0B5 File Offset: 0x000692B5
		internal void LeaveScope()
		{
			this._scopes.RemoveAt(this.CurrentScopeIndex);
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x0006B0C8 File Offset: 0x000692C8
		internal int CurrentScopeIndex
		{
			get
			{
				return this._scopes.Count - 1;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x0006B0D7 File Offset: 0x000692D7
		internal Scope CurrentScope
		{
			get
			{
				return this._scopes[this.CurrentScopeIndex];
			}
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0006B0EC File Offset: 0x000692EC
		internal Scope GetScopeByIndex(int scopeIndex)
		{
			if (0 > scopeIndex || scopeIndex > this.CurrentScopeIndex)
			{
				string invalidScopeIndex = Strings.InvalidScopeIndex;
				throw new EntitySqlException(invalidScopeIndex);
			}
			return this._scopes[scopeIndex];
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0006B120 File Offset: 0x00069320
		internal void RollbackToScope(int scopeIndex)
		{
			if (scopeIndex > this.CurrentScopeIndex || scopeIndex < 0 || this.CurrentScopeIndex < 0)
			{
				string invalidSavePoint = Strings.InvalidSavePoint;
				throw new EntitySqlException(invalidSavePoint);
			}
			int num = this.CurrentScopeIndex - scopeIndex;
			if (num > 0)
			{
				this._scopes.RemoveRange(scopeIndex + 1, this.CurrentScopeIndex - scopeIndex);
			}
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0006B173 File Offset: 0x00069373
		internal bool IsInCurrentScope(string key)
		{
			return this.CurrentScope.Contains(key);
		}

		// Token: 0x040007BB RID: 1979
		private readonly IEqualityComparer<string> _keyComparer;

		// Token: 0x040007BC RID: 1980
		private readonly List<Scope> _scopes = new List<Scope>();
	}
}

using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace System.Data.Common.EntitySql
{
	// Token: 0x0200034D RID: 845
	internal sealed class ScopeManager
	{
		// Token: 0x06003183 RID: 12675 RVA: 0x000C2BFC File Offset: 0x000C0DFC
		internal ScopeManager(IEqualityComparer<string> keyComparer)
		{
			this._keyComparer = keyComparer;
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x000C2C16 File Offset: 0x000C0E16
		internal void EnterScope()
		{
			this._scopes.Add(new Scope(this._keyComparer));
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000C2C2E File Offset: 0x000C0E2E
		internal void LeaveScope()
		{
			this._scopes.RemoveAt(this.CurrentScopeIndex);
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06003186 RID: 12678 RVA: 0x000C2C41 File Offset: 0x000C0E41
		internal int CurrentScopeIndex
		{
			get
			{
				return this._scopes.Count - 1;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06003187 RID: 12679 RVA: 0x000C2C50 File Offset: 0x000C0E50
		internal Scope CurrentScope
		{
			get
			{
				return this._scopes[this.CurrentScopeIndex];
			}
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000C2C63 File Offset: 0x000C0E63
		internal Scope GetScopeByIndex(int scopeIndex)
		{
			if (0 > scopeIndex || scopeIndex > this.CurrentScopeIndex)
			{
				throw EntityUtil.EntitySqlError(Strings.InvalidScopeIndex);
			}
			return this._scopes[scopeIndex];
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000C2C8C File Offset: 0x000C0E8C
		internal void RollbackToScope(int scopeIndex)
		{
			if (scopeIndex > this.CurrentScopeIndex || scopeIndex < 0 || this.CurrentScopeIndex < 0)
			{
				throw EntityUtil.EntitySqlError(Strings.InvalidSavePoint);
			}
			int num = this.CurrentScopeIndex - scopeIndex;
			if (num > 0)
			{
				this._scopes.RemoveRange(scopeIndex + 1, this.CurrentScopeIndex - scopeIndex);
			}
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000C2CDD File Offset: 0x000C0EDD
		internal bool IsInCurrentScope(string key)
		{
			return this.CurrentScope.Contains(key);
		}

		// Token: 0x04001586 RID: 5510
		private readonly IEqualityComparer<string> _keyComparer;

		// Token: 0x04001587 RID: 5511
		private readonly List<Scope> _scopes = new List<Scope>();
	}
}

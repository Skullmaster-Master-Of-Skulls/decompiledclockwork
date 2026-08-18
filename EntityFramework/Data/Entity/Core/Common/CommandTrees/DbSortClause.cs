using System;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000107 RID: 263
	public sealed class DbSortClause
	{
		// Token: 0x0600068A RID: 1674 RVA: 0x000260C6 File Offset: 0x000242C6
		internal DbSortClause(DbExpression key, bool asc, string collation)
		{
			this._expr = key;
			this._asc = asc;
			this._coll = collation;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x000260E3 File Offset: 0x000242E3
		public bool Ascending
		{
			get
			{
				return this._asc;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x000260EB File Offset: 0x000242EB
		public string Collation
		{
			get
			{
				return this._coll;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x000260F3 File Offset: 0x000242F3
		public DbExpression Expression
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x040001FB RID: 507
		private readonly DbExpression _expr;

		// Token: 0x040001FC RID: 508
		private readonly bool _asc;

		// Token: 0x040001FD RID: 509
		private readonly string _coll;
	}
}

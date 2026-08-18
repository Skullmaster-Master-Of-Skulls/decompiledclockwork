using System;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000413 RID: 1043
	public sealed class DbSortClause
	{
		// Token: 0x060036CF RID: 14031 RVA: 0x000D1365 File Offset: 0x000CF565
		internal DbSortClause(DbExpression key, bool asc, string collation)
		{
			this._expr = key;
			this._asc = asc;
			this._coll = collation;
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x060036D0 RID: 14032 RVA: 0x000D1382 File Offset: 0x000CF582
		public bool Ascending
		{
			get
			{
				return this._asc;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x060036D1 RID: 14033 RVA: 0x000D138A File Offset: 0x000CF58A
		public string Collation
		{
			get
			{
				return this._coll;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x060036D2 RID: 14034 RVA: 0x000D1392 File Offset: 0x000CF592
		public DbExpression Expression
		{
			get
			{
				return this._expr;
			}
		}

		// Token: 0x0400181C RID: 6172
		private readonly DbExpression _expr;

		// Token: 0x0400181D RID: 6173
		private readonly bool _asc;

		// Token: 0x0400181E RID: 6174
		private readonly string _coll;
	}
}

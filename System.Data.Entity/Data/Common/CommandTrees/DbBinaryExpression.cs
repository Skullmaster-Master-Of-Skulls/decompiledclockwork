using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003F0 RID: 1008
	public abstract class DbBinaryExpression : DbExpression
	{
		// Token: 0x0600360C RID: 13836 RVA: 0x000D03E9 File Offset: 0x000CE5E9
		internal DbBinaryExpression(DbExpressionKind kind, TypeUsage type, DbExpression left, DbExpression right) : base(kind, type)
		{
			this._left = left;
			this._right = right;
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x000D0402 File Offset: 0x000CE602
		public DbExpression Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600360E RID: 13838 RVA: 0x000D040A File Offset: 0x000CE60A
		public DbExpression Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x040017F3 RID: 6131
		private readonly DbExpression _left;

		// Token: 0x040017F4 RID: 6132
		private readonly DbExpression _right;
	}
}

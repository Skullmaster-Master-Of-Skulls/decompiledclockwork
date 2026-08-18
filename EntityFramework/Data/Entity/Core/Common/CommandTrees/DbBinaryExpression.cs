using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000DD RID: 221
	public abstract class DbBinaryExpression : DbExpression
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x000253DC File Offset: 0x000235DC
		internal DbBinaryExpression()
		{
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000253E4 File Offset: 0x000235E4
		internal DbBinaryExpression(DbExpressionKind kind, TypeUsage type, DbExpression left, DbExpression right) : base(kind, type, true)
		{
			this._left = left;
			this._right = right;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x000253FE File Offset: 0x000235FE
		public virtual DbExpression Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00025406 File Offset: 0x00023606
		public virtual DbExpression Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x040001BE RID: 446
		private readonly DbExpression _left;

		// Token: 0x040001BF RID: 447
		private readonly DbExpression _right;
	}
}

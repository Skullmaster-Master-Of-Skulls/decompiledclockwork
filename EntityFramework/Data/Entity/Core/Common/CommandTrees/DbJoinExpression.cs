using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000F6 RID: 246
	public sealed class DbJoinExpression : DbExpression
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x00025A48 File Offset: 0x00023C48
		internal DbJoinExpression(DbExpressionKind joinKind, TypeUsage collectionOfRowResultType, DbExpressionBinding left, DbExpressionBinding right, DbExpression condition) : base(joinKind, collectionOfRowResultType, true)
		{
			this._left = left;
			this._right = right;
			this._condition = condition;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x00025A6A File Offset: 0x00023C6A
		public DbExpressionBinding Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00025A72 File Offset: 0x00023C72
		public DbExpressionBinding Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00025A7A File Offset: 0x00023C7A
		public DbExpression JoinCondition
		{
			get
			{
				return this._condition;
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00025A82 File Offset: 0x00023C82
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00025A97 File Offset: 0x00023C97
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001DB RID: 475
		private readonly DbExpressionBinding _left;

		// Token: 0x040001DC RID: 476
		private readonly DbExpressionBinding _right;

		// Token: 0x040001DD RID: 477
		private readonly DbExpression _condition;
	}
}

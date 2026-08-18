using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x0200040B RID: 1035
	public sealed class DbFilterExpression : DbExpression
	{
		// Token: 0x060036A7 RID: 13991 RVA: 0x000D10A9 File Offset: 0x000CF2A9
		internal DbFilterExpression(TypeUsage resultType, DbExpressionBinding input, DbExpression predicate) : base(DbExpressionKind.Filter, resultType)
		{
			this._input = input;
			this._predicate = predicate;
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x060036A8 RID: 13992 RVA: 0x000D10C2 File Offset: 0x000CF2C2
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x060036A9 RID: 13993 RVA: 0x000D10CA File Offset: 0x000CF2CA
		public DbExpression Predicate
		{
			get
			{
				return this._predicate;
			}
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000D10D2 File Offset: 0x000CF2D2
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000D10E9 File Offset: 0x000CF2E9
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0400180C RID: 6156
		private readonly DbExpressionBinding _input;

		// Token: 0x0400180D RID: 6157
		private readonly DbExpression _predicate;
	}
}

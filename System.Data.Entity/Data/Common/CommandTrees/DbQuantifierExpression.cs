using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000412 RID: 1042
	public sealed class DbQuantifierExpression : DbExpression
	{
		// Token: 0x060036CA RID: 14026 RVA: 0x000D130E File Offset: 0x000CF50E
		internal DbQuantifierExpression(DbExpressionKind kind, TypeUsage booleanResultType, DbExpressionBinding input, DbExpression predicate) : base(kind, booleanResultType)
		{
			this._input = input;
			this._predicate = predicate;
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x060036CB RID: 14027 RVA: 0x000D1327 File Offset: 0x000CF527
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x060036CC RID: 14028 RVA: 0x000D132F File Offset: 0x000CF52F
		public DbExpression Predicate
		{
			get
			{
				return this._predicate;
			}
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x000D1337 File Offset: 0x000CF537
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x000D134E File Offset: 0x000CF54E
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0400181A RID: 6170
		private readonly DbExpressionBinding _input;

		// Token: 0x0400181B RID: 6171
		private readonly DbExpression _predicate;
	}
}

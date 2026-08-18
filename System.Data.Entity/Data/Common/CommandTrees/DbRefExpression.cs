using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000421 RID: 1057
	public sealed class DbRefExpression : DbUnaryExpression
	{
		// Token: 0x06003714 RID: 14100 RVA: 0x000D193C File Offset: 0x000CFB3C
		internal DbRefExpression(TypeUsage refResultType, EntitySet entitySet, DbExpression refKeys) : base(DbExpressionKind.Ref, refResultType, refKeys)
		{
			this._entitySet = entitySet;
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06003715 RID: 14101 RVA: 0x000D194F File Offset: 0x000CFB4F
		public EntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000D1957 File Offset: 0x000CFB57
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x000D196E File Offset: 0x000CFB6E
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001837 RID: 6199
		private readonly EntitySet _entitySet;
	}
}

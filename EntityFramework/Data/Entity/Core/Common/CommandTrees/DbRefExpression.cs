using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000103 RID: 259
	public sealed class DbRefExpression : DbUnaryExpression
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x00025EA8 File Offset: 0x000240A8
		internal DbRefExpression(TypeUsage refResultType, EntitySet entitySet, DbExpression refKeys) : base(DbExpressionKind.Ref, refResultType, refKeys)
		{
			this._entitySet = entitySet;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00025EBB File Offset: 0x000240BB
		public EntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00025EC3 File Offset: 0x000240C3
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00025ED8 File Offset: 0x000240D8
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001F0 RID: 496
		private readonly EntitySet _entitySet;
	}
}

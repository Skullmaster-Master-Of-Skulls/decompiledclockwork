using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000402 RID: 1026
	public sealed class DbOfTypeExpression : DbUnaryExpression
	{
		// Token: 0x06003684 RID: 13956 RVA: 0x000D0E23 File Offset: 0x000CF023
		internal DbOfTypeExpression(DbExpressionKind ofTypeKind, TypeUsage collectionResultType, DbExpression argument, TypeUsage type) : base(ofTypeKind, collectionResultType, argument)
		{
			this._ofType = type;
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06003685 RID: 13957 RVA: 0x000D0E36 File Offset: 0x000CF036
		public TypeUsage OfType
		{
			get
			{
				return this._ofType;
			}
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x000D0E3E File Offset: 0x000CF03E
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x000D0E55 File Offset: 0x000CF055
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001805 RID: 6149
		private readonly TypeUsage _ofType;
	}
}

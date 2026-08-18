using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x0200041E RID: 1054
	public sealed class DbRelationshipNavigationExpression : DbExpression
	{
		// Token: 0x06003702 RID: 14082 RVA: 0x000D1732 File Offset: 0x000CF932
		internal DbRelationshipNavigationExpression(TypeUsage resultType, RelationshipType relType, RelationshipEndMember fromEnd, RelationshipEndMember toEnd, DbExpression navigateFrom) : base(DbExpressionKind.RelationshipNavigation, resultType)
		{
			this._relation = relType;
			this._fromRole = fromEnd;
			this._toRole = toEnd;
			this._from = navigateFrom;
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06003703 RID: 14083 RVA: 0x000D175B File Offset: 0x000CF95B
		public RelationshipType Relationship
		{
			get
			{
				return this._relation;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06003704 RID: 14084 RVA: 0x000D1763 File Offset: 0x000CF963
		public RelationshipEndMember NavigateFrom
		{
			get
			{
				return this._fromRole;
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x000D176B File Offset: 0x000CF96B
		public RelationshipEndMember NavigateTo
		{
			get
			{
				return this._toRole;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06003706 RID: 14086 RVA: 0x000D1773 File Offset: 0x000CF973
		public DbExpression NavigationSource
		{
			get
			{
				return this._from;
			}
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x000D177B File Offset: 0x000CF97B
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x06003708 RID: 14088 RVA: 0x000D1792 File Offset: 0x000CF992
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0400182E RID: 6190
		private readonly RelationshipType _relation;

		// Token: 0x0400182F RID: 6191
		private readonly RelationshipEndMember _fromRole;

		// Token: 0x04001830 RID: 6192
		private readonly RelationshipEndMember _toRole;

		// Token: 0x04001831 RID: 6193
		private readonly DbExpression _from;
	}
}

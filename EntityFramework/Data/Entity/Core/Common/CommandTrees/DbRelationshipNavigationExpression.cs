using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000105 RID: 261
	public sealed class DbRelationshipNavigationExpression : DbExpression
	{
		// Token: 0x0600067D RID: 1661 RVA: 0x00025FEE File Offset: 0x000241EE
		internal DbRelationshipNavigationExpression(TypeUsage resultType, RelationshipType relType, RelationshipEndMember fromEnd, RelationshipEndMember toEnd, DbExpression navigateFrom) : base(DbExpressionKind.RelationshipNavigation, resultType, true)
		{
			this._relation = relType;
			this._fromRole = fromEnd;
			this._toRole = toEnd;
			this._from = navigateFrom;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00026018 File Offset: 0x00024218
		public RelationshipType Relationship
		{
			get
			{
				return this._relation;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00026020 File Offset: 0x00024220
		public RelationshipEndMember NavigateFrom
		{
			get
			{
				return this._fromRole;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00026028 File Offset: 0x00024228
		public RelationshipEndMember NavigateTo
		{
			get
			{
				return this._toRole;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00026030 File Offset: 0x00024230
		public DbExpression NavigationSource
		{
			get
			{
				return this._from;
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00026038 File Offset: 0x00024238
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0002604D File Offset: 0x0002424D
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001F4 RID: 500
		private readonly RelationshipType _relation;

		// Token: 0x040001F5 RID: 501
		private readonly RelationshipEndMember _fromRole;

		// Token: 0x040001F6 RID: 502
		private readonly RelationshipEndMember _toRole;

		// Token: 0x040001F7 RID: 503
		private readonly DbExpression _from;
	}
}

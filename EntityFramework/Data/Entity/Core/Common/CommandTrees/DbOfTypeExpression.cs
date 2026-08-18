using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000FD RID: 253
	public sealed class DbOfTypeExpression : DbUnaryExpression
	{
		// Token: 0x06000656 RID: 1622 RVA: 0x00025CAC File Offset: 0x00023EAC
		internal DbOfTypeExpression(DbExpressionKind ofTypeKind, TypeUsage collectionResultType, DbExpression argument, TypeUsage type) : base(ofTypeKind, collectionResultType, argument)
		{
			this._ofType = type;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00025CBF File Offset: 0x00023EBF
		public TypeUsage OfType
		{
			get
			{
				return this._ofType;
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00025CC7 File Offset: 0x00023EC7
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00025CDC File Offset: 0x00023EDC
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001E8 RID: 488
		private readonly TypeUsage _ofType;
	}
}

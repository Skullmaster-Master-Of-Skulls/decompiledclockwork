using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000138 RID: 312
	public class DbScanExpression : DbExpression
	{
		// Token: 0x06000A84 RID: 2692 RVA: 0x00035EB2 File Offset: 0x000340B2
		internal DbScanExpression()
		{
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00035EBA File Offset: 0x000340BA
		internal DbScanExpression(TypeUsage collectionOfEntityType, EntitySetBase entitySet) : base(DbExpressionKind.Scan, collectionOfEntityType, true)
		{
			this._targetSet = entitySet;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00035ECD File Offset: 0x000340CD
		public virtual EntitySetBase Target
		{
			get
			{
				return this._targetSet;
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00035ED5 File Offset: 0x000340D5
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00035EEA File Offset: 0x000340EA
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040002D2 RID: 722
		private readonly EntitySetBase _targetSet;
	}
}

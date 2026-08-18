using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000E7 RID: 231
	public sealed class DbCrossJoinExpression : DbExpression
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x00025686 File Offset: 0x00023886
		internal DbCrossJoinExpression(TypeUsage collectionOfRowResultType, ReadOnlyCollection<DbExpressionBinding> inputs) : base(DbExpressionKind.CrossJoin, collectionOfRowResultType, true)
		{
			this._inputs = inputs;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00025698 File Offset: 0x00023898
		public IList<DbExpressionBinding> Inputs
		{
			get
			{
				return this._inputs;
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000256A0 File Offset: 0x000238A0
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000256B5 File Offset: 0x000238B5
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001CF RID: 463
		private readonly ReadOnlyCollection<DbExpressionBinding> _inputs;
	}
}

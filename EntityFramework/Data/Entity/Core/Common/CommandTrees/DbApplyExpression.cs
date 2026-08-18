using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000DF RID: 223
	public sealed class DbApplyExpression : DbExpression
	{
		// Token: 0x060005D9 RID: 1497 RVA: 0x00025444 File Offset: 0x00023644
		internal DbApplyExpression(DbExpressionKind applyKind, TypeUsage resultRowCollectionTypeUsage, DbExpressionBinding input, DbExpressionBinding apply) : base(applyKind, resultRowCollectionTypeUsage, true)
		{
			this._input = input;
			this._apply = apply;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0002545E File Offset: 0x0002365E
		public DbExpressionBinding Apply
		{
			get
			{
				return this._apply;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00025466 File Offset: 0x00023666
		public DbExpressionBinding Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0002546E File Offset: 0x0002366E
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00025483 File Offset: 0x00023683
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001C0 RID: 448
		private readonly DbExpressionBinding _input;

		// Token: 0x040001C1 RID: 449
		private readonly DbExpressionBinding _apply;
	}
}

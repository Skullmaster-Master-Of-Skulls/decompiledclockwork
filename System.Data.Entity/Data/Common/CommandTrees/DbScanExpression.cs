using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000423 RID: 1059
	public sealed class DbScanExpression : DbExpression
	{
		// Token: 0x0600371B RID: 14107 RVA: 0x000D19BE File Offset: 0x000CFBBE
		internal DbScanExpression(TypeUsage collectionOfEntityType, EntitySetBase entitySet) : base(DbExpressionKind.Scan, collectionOfEntityType)
		{
			this._targetSet = entitySet;
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x0600371C RID: 14108 RVA: 0x000D19D0 File Offset: 0x000CFBD0
		public EntitySetBase Target
		{
			get
			{
				return this._targetSet;
			}
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x000D19D8 File Offset: 0x000CFBD8
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x000D19EF File Offset: 0x000CFBEF
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001838 RID: 6200
		private readonly EntitySetBase _targetSet;
	}
}

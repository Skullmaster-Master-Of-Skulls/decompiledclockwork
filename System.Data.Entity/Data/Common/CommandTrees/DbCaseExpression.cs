using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003FC RID: 1020
	public sealed class DbCaseExpression : DbExpression
	{
		// Token: 0x0600366E RID: 13934 RVA: 0x000D0C8C File Offset: 0x000CEE8C
		internal DbCaseExpression(TypeUsage commonResultType, DbExpressionList whens, DbExpressionList thens, DbExpression elseExpr) : base(DbExpressionKind.Case, commonResultType)
		{
			this._when = whens;
			this._then = thens;
			this._else = elseExpr;
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x0600366F RID: 13935 RVA: 0x000D0CAC File Offset: 0x000CEEAC
		public IList<DbExpression> When
		{
			get
			{
				return this._when;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06003670 RID: 13936 RVA: 0x000D0CB4 File Offset: 0x000CEEB4
		public IList<DbExpression> Then
		{
			get
			{
				return this._then;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06003671 RID: 13937 RVA: 0x000D0CBC File Offset: 0x000CEEBC
		public DbExpression Else
		{
			get
			{
				return this._else;
			}
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x000D0CC4 File Offset: 0x000CEEC4
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000D0CDB File Offset: 0x000CEEDB
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001801 RID: 6145
		private readonly DbExpressionList _when;

		// Token: 0x04001802 RID: 6146
		private readonly DbExpressionList _then;

		// Token: 0x04001803 RID: 6147
		private readonly DbExpression _else;
	}
}

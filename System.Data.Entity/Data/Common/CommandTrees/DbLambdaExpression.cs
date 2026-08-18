using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x0200041D RID: 1053
	public sealed class DbLambdaExpression : DbExpression
	{
		// Token: 0x060036FD RID: 14077 RVA: 0x000D16DB File Offset: 0x000CF8DB
		internal DbLambdaExpression(TypeUsage resultType, DbLambda lambda, DbExpressionList args) : base(DbExpressionKind.Lambda, resultType)
		{
			this._lambda = lambda;
			this._arguments = args;
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060036FE RID: 14078 RVA: 0x000D16F4 File Offset: 0x000CF8F4
		public DbLambda Lambda
		{
			get
			{
				return this._lambda;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060036FF RID: 14079 RVA: 0x000D16FC File Offset: 0x000CF8FC
		public IList<DbExpression> Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000D1704 File Offset: 0x000CF904
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000D171B File Offset: 0x000CF91B
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x0400182C RID: 6188
		private readonly DbLambda _lambda;

		// Token: 0x0400182D RID: 6189
		private readonly DbExpressionList _arguments;
	}
}

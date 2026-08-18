using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000F7 RID: 247
	public sealed class DbLambdaExpression : DbExpression
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x00025AAC File Offset: 0x00023CAC
		internal DbLambdaExpression(TypeUsage resultType, DbLambda lambda, DbExpressionList args) : base(DbExpressionKind.Lambda, resultType, true)
		{
			this._lambda = lambda;
			this._arguments = args;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00025AC6 File Offset: 0x00023CC6
		public DbLambda Lambda
		{
			get
			{
				return this._lambda;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00025ACE File Offset: 0x00023CCE
		public IList<DbExpression> Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00025AD6 File Offset: 0x00023CD6
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00025AEB File Offset: 0x00023CEB
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001DE RID: 478
		private readonly DbLambda _lambda;

		// Token: 0x040001DF RID: 479
		private readonly DbExpressionList _arguments;
	}
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000E1 RID: 225
	public sealed class DbCaseExpression : DbExpression
	{
		// Token: 0x060005E2 RID: 1506 RVA: 0x000254DC File Offset: 0x000236DC
		internal DbCaseExpression(TypeUsage commonResultType, DbExpressionList whens, DbExpressionList thens, DbExpression elseExpr) : base(DbExpressionKind.Case, commonResultType, true)
		{
			this._when = whens;
			this._then = thens;
			this._else = elseExpr;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x000254FD File Offset: 0x000236FD
		public IList<DbExpression> When
		{
			get
			{
				return this._when;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00025505 File Offset: 0x00023705
		public IList<DbExpression> Then
		{
			get
			{
				return this._then;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0002550D File Offset: 0x0002370D
		public DbExpression Else
		{
			get
			{
				return this._else;
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00025515 File Offset: 0x00023715
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0002552A File Offset: 0x0002372A
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001C3 RID: 451
		private readonly DbExpressionList _when;

		// Token: 0x040001C4 RID: 452
		private readonly DbExpressionList _then;

		// Token: 0x040001C5 RID: 453
		private readonly DbExpression _else;
	}
}

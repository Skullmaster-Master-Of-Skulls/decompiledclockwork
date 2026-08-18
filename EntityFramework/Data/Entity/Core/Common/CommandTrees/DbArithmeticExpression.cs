using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000E0 RID: 224
	public sealed class DbArithmeticExpression : DbExpression
	{
		// Token: 0x060005DE RID: 1502 RVA: 0x00025498 File Offset: 0x00023698
		internal DbArithmeticExpression(DbExpressionKind kind, TypeUsage numericResultType, DbExpressionList args) : base(kind, numericResultType, true)
		{
			this._args = args;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x000254AA File Offset: 0x000236AA
		public IList<DbExpression> Arguments
		{
			get
			{
				return this._args;
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000254B2 File Offset: 0x000236B2
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000254C7 File Offset: 0x000236C7
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001C2 RID: 450
		private readonly DbExpressionList _args;
	}
}

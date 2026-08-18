using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000401 RID: 1025
	public sealed class DbIsOfExpression : DbUnaryExpression
	{
		// Token: 0x06003680 RID: 13952 RVA: 0x000D0DDA File Offset: 0x000CEFDA
		internal DbIsOfExpression(DbExpressionKind isOfKind, TypeUsage booleanResultType, DbExpression argument, TypeUsage isOfType) : base(isOfKind, booleanResultType, argument)
		{
			this._ofType = isOfType;
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06003681 RID: 13953 RVA: 0x000D0DED File Offset: 0x000CEFED
		public TypeUsage OfType
		{
			get
			{
				return this._ofType;
			}
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x000D0DF5 File Offset: 0x000CEFF5
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000D0E0C File Offset: 0x000CF00C
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001804 RID: 6148
		private TypeUsage _ofType;
	}
}

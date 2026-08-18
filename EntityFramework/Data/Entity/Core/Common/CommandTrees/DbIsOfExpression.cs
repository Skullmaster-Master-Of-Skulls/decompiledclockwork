using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000F5 RID: 245
	public sealed class DbIsOfExpression : DbUnaryExpression
	{
		// Token: 0x0600062E RID: 1582 RVA: 0x00025A03 File Offset: 0x00023C03
		internal DbIsOfExpression(DbExpressionKind isOfKind, TypeUsage booleanResultType, DbExpression argument, TypeUsage isOfType) : base(isOfKind, booleanResultType, argument)
		{
			this._ofType = isOfType;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00025A16 File Offset: 0x00023C16
		public TypeUsage OfType
		{
			get
			{
				return this._ofType;
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00025A1E File Offset: 0x00023C1E
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00025A33 File Offset: 0x00023C33
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001DA RID: 474
		private readonly TypeUsage _ofType;
	}
}

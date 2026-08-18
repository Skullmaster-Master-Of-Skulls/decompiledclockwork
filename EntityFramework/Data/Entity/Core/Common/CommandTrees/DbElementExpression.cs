using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000EA RID: 234
	public sealed class DbElementExpression : DbUnaryExpression
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x00025735 File Offset: 0x00023935
		internal DbElementExpression(TypeUsage resultType, DbExpression argument) : base(DbExpressionKind.Element, resultType, argument)
		{
			this._singlePropertyUnwrapped = false;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00025748 File Offset: 0x00023948
		internal DbElementExpression(TypeUsage resultType, DbExpression argument, bool unwrapSingleProperty) : base(DbExpressionKind.Element, resultType, argument)
		{
			this._singlePropertyUnwrapped = unwrapSingleProperty;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0002575B File Offset: 0x0002395B
		internal bool IsSinglePropertyUnwrapped
		{
			get
			{
				return this._singlePropertyUnwrapped;
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00025763 File Offset: 0x00023963
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00025778 File Offset: 0x00023978
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001D0 RID: 464
		private readonly bool _singlePropertyUnwrapped;
	}
}

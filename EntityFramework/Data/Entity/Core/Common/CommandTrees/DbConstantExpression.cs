using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020000E6 RID: 230
	public class DbConstantExpression : DbExpression
	{
		// Token: 0x060005F2 RID: 1522 RVA: 0x000255D5 File Offset: 0x000237D5
		internal DbConstantExpression()
		{
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000255E0 File Offset: 0x000237E0
		internal DbConstantExpression(TypeUsage resultType, object value) : base(DbExpressionKind.Constant, resultType, true)
		{
			PrimitiveType primitiveType;
			this._shouldCloneValue = (TypeHelpers.TryGetEdmType<PrimitiveType>(resultType, out primitiveType) && primitiveType.PrimitiveTypeKind == PrimitiveTypeKind.Binary);
			if (this._shouldCloneValue)
			{
				this._value = ((byte[])value).Clone();
				return;
			}
			this._value = value;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00025633 File Offset: 0x00023833
		internal object GetValue()
		{
			return this._value;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0002563B File Offset: 0x0002383B
		public virtual object Value
		{
			get
			{
				if (this._shouldCloneValue)
				{
					return ((byte[])this._value).Clone();
				}
				return this._value;
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0002565C File Offset: 0x0002385C
		public override void Accept(DbExpressionVisitor visitor)
		{
			Check.NotNull<DbExpressionVisitor>(visitor, "visitor");
			visitor.Visit(this);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00025671 File Offset: 0x00023871
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			Check.NotNull<DbExpressionVisitor<TResultType>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040001CD RID: 461
		private readonly bool _shouldCloneValue;

		// Token: 0x040001CE RID: 462
		private readonly object _value;
	}
}

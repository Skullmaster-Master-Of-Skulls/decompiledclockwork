using System;
using System.Data.Metadata.Edm;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x02000417 RID: 1047
	public sealed class DbConstantExpression : DbExpression
	{
		// Token: 0x060036E1 RID: 14049 RVA: 0x000D1494 File Offset: 0x000CF694
		internal DbConstantExpression(TypeUsage resultType, object value) : base(DbExpressionKind.Constant, resultType)
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

		// Token: 0x060036E2 RID: 14050 RVA: 0x000D14E6 File Offset: 0x000CF6E6
		internal object GetValue()
		{
			return this._value;
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060036E3 RID: 14051 RVA: 0x000D14EE File Offset: 0x000CF6EE
		public object Value
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

		// Token: 0x060036E4 RID: 14052 RVA: 0x000D150F File Offset: 0x000CF70F
		public override void Accept(DbExpressionVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.Visit(this);
				return;
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000D1526 File Offset: 0x000CF726
		public override TResultType Accept<TResultType>(DbExpressionVisitor<TResultType> visitor)
		{
			if (visitor != null)
			{
				return visitor.Visit(this);
			}
			throw EntityUtil.ArgumentNull("visitor");
		}

		// Token: 0x04001824 RID: 6180
		private readonly bool _shouldCloneValue;

		// Token: 0x04001825 RID: 6181
		private readonly object _value;
	}
}

using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200030D RID: 781
	internal sealed class FalseExpr<T_Identifier> : BoolExpr<T_Identifier>
	{
		// Token: 0x06001B27 RID: 6951 RVA: 0x000873AF File Offset: 0x000855AF
		private FalseExpr()
		{
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x000873B7 File Offset: 0x000855B7
		internal static FalseExpr<T_Identifier> Value
		{
			get
			{
				return FalseExpr<T_Identifier>._value;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x000873BE File Offset: 0x000855BE
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.False;
			}
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x000873C1 File Offset: 0x000855C1
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitFalse(this);
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x000873CA File Offset: 0x000855CA
		internal override BoolExpr<T_Identifier> MakeNegated()
		{
			return TrueExpr<T_Identifier>.Value;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x000873D1 File Offset: 0x000855D1
		protected override bool EquivalentTypeEquals(BoolExpr<T_Identifier> other)
		{
			return object.ReferenceEquals(this, other);
		}

		// Token: 0x04000993 RID: 2451
		private static readonly FalseExpr<T_Identifier> _value = new FalseExpr<T_Identifier>();
	}
}

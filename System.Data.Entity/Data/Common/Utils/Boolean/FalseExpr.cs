using System;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003AC RID: 940
	internal sealed class FalseExpr<T_Identifier> : BoolExpr<T_Identifier>
	{
		// Token: 0x060033A7 RID: 13223 RVA: 0x000C8920 File Offset: 0x000C6B20
		private FalseExpr()
		{
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x060033A8 RID: 13224 RVA: 0x000C8928 File Offset: 0x000C6B28
		internal static FalseExpr<T_Identifier> Value
		{
			get
			{
				return FalseExpr<T_Identifier>.s_value;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x060033A9 RID: 13225 RVA: 0x000C892F File Offset: 0x000C6B2F
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.False;
			}
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000C8932 File Offset: 0x000C6B32
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitFalse(this);
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000C893B File Offset: 0x000C6B3B
		internal override BoolExpr<T_Identifier> MakeNegated()
		{
			return TrueExpr<T_Identifier>.Value;
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x0005AF88 File Offset: 0x00059188
		protected override bool EquivalentTypeEquals(BoolExpr<T_Identifier> other)
		{
			return this == other;
		}

		// Token: 0x04001695 RID: 5781
		private static readonly FalseExpr<T_Identifier> s_value = new FalseExpr<T_Identifier>();
	}
}

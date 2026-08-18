using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000321 RID: 801
	internal sealed class TrueExpr<T_Identifier> : BoolExpr<T_Identifier>
	{
		// Token: 0x06001BA6 RID: 7078 RVA: 0x000882E5 File Offset: 0x000864E5
		private TrueExpr()
		{
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x000882ED File Offset: 0x000864ED
		internal static TrueExpr<T_Identifier> Value
		{
			get
			{
				return TrueExpr<T_Identifier>._value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x000882F4 File Offset: 0x000864F4
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.True;
			}
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x000882F7 File Offset: 0x000864F7
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitTrue(this);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x00088300 File Offset: 0x00086500
		internal override BoolExpr<T_Identifier> MakeNegated()
		{
			return FalseExpr<T_Identifier>.Value;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x00088307 File Offset: 0x00086507
		protected override bool EquivalentTypeEquals(BoolExpr<T_Identifier> other)
		{
			return object.ReferenceEquals(this, other);
		}

		// Token: 0x040009B1 RID: 2481
		private static readonly TrueExpr<T_Identifier> _value = new TrueExpr<T_Identifier>();
	}
}

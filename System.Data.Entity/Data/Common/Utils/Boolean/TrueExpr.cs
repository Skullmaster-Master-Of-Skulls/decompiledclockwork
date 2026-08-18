using System;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003AB RID: 939
	internal sealed class TrueExpr<T_Identifier> : BoolExpr<T_Identifier>
	{
		// Token: 0x060033A0 RID: 13216 RVA: 0x000C88F5 File Offset: 0x000C6AF5
		private TrueExpr()
		{
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x060033A1 RID: 13217 RVA: 0x000C88FD File Offset: 0x000C6AFD
		internal static TrueExpr<T_Identifier> Value
		{
			get
			{
				return TrueExpr<T_Identifier>.s_value;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x060033A2 RID: 13218 RVA: 0x0003C2A0 File Offset: 0x0003A4A0
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.True;
			}
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000C8904 File Offset: 0x000C6B04
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitTrue(this);
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000C890D File Offset: 0x000C6B0D
		internal override BoolExpr<T_Identifier> MakeNegated()
		{
			return FalseExpr<T_Identifier>.Value;
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x0005AF88 File Offset: 0x00059188
		protected override bool EquivalentTypeEquals(BoolExpr<T_Identifier> other)
		{
			return this == other;
		}

		// Token: 0x04001694 RID: 5780
		private static readonly TrueExpr<T_Identifier> s_value = new TrueExpr<T_Identifier>();
	}
}

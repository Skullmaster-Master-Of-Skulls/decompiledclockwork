using System;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003BE RID: 958
	internal abstract class Visitor<T_Identifier, T_Return>
	{
		// Token: 0x060033F5 RID: 13301
		internal abstract T_Return VisitTrue(TrueExpr<T_Identifier> expression);

		// Token: 0x060033F6 RID: 13302
		internal abstract T_Return VisitFalse(FalseExpr<T_Identifier> expression);

		// Token: 0x060033F7 RID: 13303
		internal abstract T_Return VisitTerm(TermExpr<T_Identifier> expression);

		// Token: 0x060033F8 RID: 13304
		internal abstract T_Return VisitNot(NotExpr<T_Identifier> expression);

		// Token: 0x060033F9 RID: 13305
		internal abstract T_Return VisitAnd(AndExpr<T_Identifier> expression);

		// Token: 0x060033FA RID: 13306
		internal abstract T_Return VisitOr(OrExpr<T_Identifier> expression);
	}
}

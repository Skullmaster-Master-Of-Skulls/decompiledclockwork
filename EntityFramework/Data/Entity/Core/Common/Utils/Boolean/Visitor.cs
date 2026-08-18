using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x020001E9 RID: 489
	internal abstract class Visitor<T_Identifier, T_Return>
	{
		// Token: 0x0600112B RID: 4395
		internal abstract T_Return VisitTrue(TrueExpr<T_Identifier> expression);

		// Token: 0x0600112C RID: 4396
		internal abstract T_Return VisitFalse(FalseExpr<T_Identifier> expression);

		// Token: 0x0600112D RID: 4397
		internal abstract T_Return VisitTerm(TermExpr<T_Identifier> expression);

		// Token: 0x0600112E RID: 4398
		internal abstract T_Return VisitNot(NotExpr<T_Identifier> expression);

		// Token: 0x0600112F RID: 4399
		internal abstract T_Return VisitAnd(AndExpr<T_Identifier> expression);

		// Token: 0x06001130 RID: 4400
		internal abstract T_Return VisitOr(OrExpr<T_Identifier> expression);
	}
}

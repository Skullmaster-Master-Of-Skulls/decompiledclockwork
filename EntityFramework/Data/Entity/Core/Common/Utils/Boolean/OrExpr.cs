using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x020001E2 RID: 482
	internal class OrExpr<T_Identifier> : TreeExpr<T_Identifier>
	{
		// Token: 0x06001102 RID: 4354 RVA: 0x000485DE File Offset: 0x000467DE
		internal OrExpr(params BoolExpr<T_Identifier>[] children) : this((IEnumerable<BoolExpr<T_Identifier>>)children)
		{
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x000485EC File Offset: 0x000467EC
		internal OrExpr(IEnumerable<BoolExpr<T_Identifier>> children) : base(children)
		{
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x000485F5 File Offset: 0x000467F5
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.Or;
			}
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x000485F8 File Offset: 0x000467F8
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitOr(this);
		}
	}
}

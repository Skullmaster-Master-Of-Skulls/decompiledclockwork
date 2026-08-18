using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x020001E4 RID: 484
	internal class AndExpr<T_Identifier> : TreeExpr<T_Identifier>
	{
		// Token: 0x0600110A RID: 4362 RVA: 0x0004867D File Offset: 0x0004687D
		internal AndExpr(params BoolExpr<T_Identifier>[] children) : this((IEnumerable<BoolExpr<T_Identifier>>)children)
		{
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0004868B File Offset: 0x0004688B
		internal AndExpr(IEnumerable<BoolExpr<T_Identifier>> children) : base(children)
		{
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x00048694 File Offset: 0x00046894
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.And;
			}
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00048697 File Offset: 0x00046897
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitAnd(this);
		}
	}
}

using System;
using System.Collections.Generic;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003AF RID: 943
	internal class AndExpr<T_Identifier> : TreeExpr<T_Identifier>
	{
		// Token: 0x060033BF RID: 13247 RVA: 0x000C8ACE File Offset: 0x000C6CCE
		internal AndExpr(params BoolExpr<T_Identifier>[] children) : this(children)
		{
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000C8AD7 File Offset: 0x000C6CD7
		internal AndExpr(IEnumerable<BoolExpr<T_Identifier>> children) : base(children)
		{
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x060033C1 RID: 13249 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.And;
			}
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000C8AE0 File Offset: 0x000C6CE0
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitAnd(this);
		}
	}
}

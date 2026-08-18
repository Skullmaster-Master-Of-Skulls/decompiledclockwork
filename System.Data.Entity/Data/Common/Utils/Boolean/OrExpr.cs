using System;
using System.Collections.Generic;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003B0 RID: 944
	internal class OrExpr<T_Identifier> : TreeExpr<T_Identifier>
	{
		// Token: 0x060033C3 RID: 13251 RVA: 0x000C8AE9 File Offset: 0x000C6CE9
		internal OrExpr(params BoolExpr<T_Identifier>[] children) : this(children)
		{
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000C8AF2 File Offset: 0x000C6CF2
		internal OrExpr(IEnumerable<BoolExpr<T_Identifier>> children) : base(children)
		{
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x00033532 File Offset: 0x00031732
		internal override ExprType ExprType
		{
			get
			{
				return ExprType.Or;
			}
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000C8AFB File Offset: 0x000C6CFB
		internal override T_Return Accept<T_Return>(Visitor<T_Identifier, T_Return> visitor)
		{
			return visitor.VisitOr(this);
		}
	}
}

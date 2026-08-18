using System;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003B5 RID: 949
	internal sealed class CnfClause<T_Identifier> : Clause<T_Identifier>, IEquatable<CnfClause<T_Identifier>>
	{
		// Token: 0x060033D6 RID: 13270 RVA: 0x000C8C2E File Offset: 0x000C6E2E
		internal CnfClause(Set<Literal<T_Identifier>> literals) : base(literals, ExprType.Or)
		{
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x000C8C38 File Offset: 0x000C6E38
		public bool Equals(CnfClause<T_Identifier> other)
		{
			return other != null && other.Literals.SetEquals(base.Literals);
		}
	}
}

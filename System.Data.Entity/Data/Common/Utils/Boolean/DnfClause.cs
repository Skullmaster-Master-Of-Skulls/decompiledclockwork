using System;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003B4 RID: 948
	internal sealed class DnfClause<T_Identifier> : Clause<T_Identifier>, IEquatable<DnfClause<T_Identifier>>
	{
		// Token: 0x060033D4 RID: 13268 RVA: 0x000C8C0C File Offset: 0x000C6E0C
		internal DnfClause(Set<Literal<T_Identifier>> literals) : base(literals, ExprType.And)
		{
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000C8C16 File Offset: 0x000C6E16
		public bool Equals(DnfClause<T_Identifier> other)
		{
			return other != null && other.Literals.SetEquals(base.Literals);
		}
	}
}

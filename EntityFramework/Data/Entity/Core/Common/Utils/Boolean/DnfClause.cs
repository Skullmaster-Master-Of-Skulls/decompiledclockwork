using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000307 RID: 775
	internal sealed class DnfClause<T_Identifier> : Clause<T_Identifier>, IEquatable<DnfClause<T_Identifier>>
	{
		// Token: 0x06001B0F RID: 6927 RVA: 0x00086CAC File Offset: 0x00084EAC
		internal DnfClause(Set<Literal<T_Identifier>> literals) : base(literals, ExprType.And)
		{
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x00086CB6 File Offset: 0x00084EB6
		public bool Equals(DnfClause<T_Identifier> other)
		{
			return other != null && other.Literals.SetEquals(base.Literals);
		}
	}
}

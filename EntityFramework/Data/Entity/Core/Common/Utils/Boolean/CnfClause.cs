using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000302 RID: 770
	internal sealed class CnfClause<T_Identifier> : Clause<T_Identifier>, IEquatable<CnfClause<T_Identifier>>
	{
		// Token: 0x06001AFF RID: 6911 RVA: 0x000869BD File Offset: 0x00084BBD
		internal CnfClause(Set<Literal<T_Identifier>> literals) : base(literals, ExprType.Or)
		{
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x000869C7 File Offset: 0x00084BC7
		public bool Equals(CnfClause<T_Identifier> other)
		{
			return other != null && other.Literals.SetEquals(base.Literals);
		}
	}
}

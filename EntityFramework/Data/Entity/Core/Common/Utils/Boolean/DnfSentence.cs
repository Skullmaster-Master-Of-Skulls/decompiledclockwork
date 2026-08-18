using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000308 RID: 776
	internal sealed class DnfSentence<T_Identifier> : Sentence<T_Identifier, DnfClause<T_Identifier>>
	{
		// Token: 0x06001B11 RID: 6929 RVA: 0x00086CCE File Offset: 0x00084ECE
		internal DnfSentence(Set<DnfClause<T_Identifier>> clauses) : base(clauses, ExprType.Or)
		{
		}
	}
}

using System;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003BB RID: 955
	internal sealed class DnfSentence<T_Identifier> : Sentence<T_Identifier, DnfClause<T_Identifier>>
	{
		// Token: 0x060033ED RID: 13293 RVA: 0x000C8F30 File Offset: 0x000C7130
		internal DnfSentence(Set<DnfClause<T_Identifier>> clauses) : base(clauses, ExprType.Or)
		{
		}
	}
}

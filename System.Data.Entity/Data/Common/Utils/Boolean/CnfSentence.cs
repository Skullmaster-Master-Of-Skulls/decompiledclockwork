using System;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003BC RID: 956
	internal sealed class CnfSentence<T_Identifier> : Sentence<T_Identifier, CnfClause<T_Identifier>>
	{
		// Token: 0x060033EE RID: 13294 RVA: 0x000C8F3A File Offset: 0x000C713A
		internal CnfSentence(Set<CnfClause<T_Identifier>> clauses) : base(clauses, ExprType.And)
		{
		}
	}
}

using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000304 RID: 772
	internal sealed class CnfSentence<T_Identifier> : Sentence<T_Identifier, CnfClause<T_Identifier>>
	{
		// Token: 0x06001B04 RID: 6916 RVA: 0x00086A70 File Offset: 0x00084C70
		internal CnfSentence(Set<CnfClause<T_Identifier>> clauses) : base(clauses, ExprType.And)
		{
		}
	}
}

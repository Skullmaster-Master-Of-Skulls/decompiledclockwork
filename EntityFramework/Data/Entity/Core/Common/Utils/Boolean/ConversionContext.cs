using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000305 RID: 773
	internal abstract class ConversionContext<T_Identifier>
	{
		// Token: 0x06001B05 RID: 6917
		internal abstract Vertex TranslateTermToVertex(TermExpr<T_Identifier> term);

		// Token: 0x06001B06 RID: 6918
		internal abstract IEnumerable<LiteralVertexPair<T_Identifier>> GetSuccessors(Vertex vertex);

		// Token: 0x0400097B RID: 2427
		internal readonly Solver Solver = new Solver();
	}
}

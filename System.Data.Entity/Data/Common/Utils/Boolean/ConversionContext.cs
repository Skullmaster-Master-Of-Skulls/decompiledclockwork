using System;
using System.Collections.Generic;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x0200039F RID: 927
	internal abstract class ConversionContext<T_Identifier>
	{
		// Token: 0x0600334D RID: 13133
		internal abstract Vertex TranslateTermToVertex(TermExpr<T_Identifier> term);

		// Token: 0x0600334E RID: 13134
		internal abstract IEnumerable<LiteralVertexPair<T_Identifier>> GetSuccessors(Vertex vertex);

		// Token: 0x04001676 RID: 5750
		internal readonly Solver Solver = new Solver();
	}
}

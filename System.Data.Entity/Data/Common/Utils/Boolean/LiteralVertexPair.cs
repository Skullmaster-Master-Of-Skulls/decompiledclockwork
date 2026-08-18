using System;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003A0 RID: 928
	internal sealed class LiteralVertexPair<T_Identifier>
	{
		// Token: 0x06003350 RID: 13136 RVA: 0x000C7B6D File Offset: 0x000C5D6D
		internal LiteralVertexPair(Vertex vertex, Literal<T_Identifier> literal)
		{
			this.Vertex = vertex;
			this.Literal = literal;
		}

		// Token: 0x04001677 RID: 5751
		internal readonly Vertex Vertex;

		// Token: 0x04001678 RID: 5752
		internal readonly Literal<T_Identifier> Literal;
	}
}

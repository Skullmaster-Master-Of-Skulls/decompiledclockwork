using System;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000314 RID: 788
	internal sealed class LiteralVertexPair<T_Identifier>
	{
		// Token: 0x06001B55 RID: 6997 RVA: 0x000877EC File Offset: 0x000859EC
		internal LiteralVertexPair(Vertex vertex, Literal<T_Identifier> literal)
		{
			this.Vertex = vertex;
			this.Literal = literal;
		}

		// Token: 0x0400099D RID: 2461
		internal readonly Vertex Vertex;

		// Token: 0x0400099E RID: 2462
		internal readonly Literal<T_Identifier> Literal;
	}
}

using System;
using System.Collections.Generic;

namespace System.Web.Razor.Parser.SyntaxTree
{
	// Token: 0x0200004A RID: 74
	internal class EquivalenceComparer : IEqualityComparer<SyntaxTreeNode>
	{
		// Token: 0x06000356 RID: 854 RVA: 0x0000DEA1 File Offset: 0x0000C0A1
		public bool Equals(SyntaxTreeNode x, SyntaxTreeNode y)
		{
			return x.EquivalentTo(y);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000DEAA File Offset: 0x0000C0AA
		public int GetHashCode(SyntaxTreeNode obj)
		{
			return obj.GetHashCode();
		}
	}
}

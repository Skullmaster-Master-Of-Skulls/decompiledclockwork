using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x02000276 RID: 630
	internal sealed class AnalyzedTree
	{
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0004A2D1 File Offset: 0x000484D1
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x0004A2D9 File Offset: 0x000484D9
		internal DebugInfoGenerator DebugInfoGenerator { get; set; }

		// Token: 0x0600167B RID: 5755 RVA: 0x0004A2E2 File Offset: 0x000484E2
		internal AnalyzedTree()
		{
		}

		// Token: 0x04000B28 RID: 2856
		internal readonly Dictionary<object, CompilerScope> Scopes = new Dictionary<object, CompilerScope>();

		// Token: 0x04000B29 RID: 2857
		internal readonly Dictionary<LambdaExpression, BoundConstants> Constants = new Dictionary<LambdaExpression, BoundConstants>();
	}
}

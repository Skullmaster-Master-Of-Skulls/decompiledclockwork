using System;
using System.Data.Metadata.Edm;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000110 RID: 272
	internal sealed class ComputedVar : Var
	{
		// Token: 0x06000DAA RID: 3498 RVA: 0x0003D19B File Offset: 0x0003B39B
		internal ComputedVar(int id, TypeUsage type) : base(id, VarType.Computed, type)
		{
		}
	}
}

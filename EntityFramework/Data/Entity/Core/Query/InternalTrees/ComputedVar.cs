using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005DB RID: 1499
	internal sealed class ComputedVar : Var
	{
		// Token: 0x06003BD0 RID: 15312 RVA: 0x0011876B File Offset: 0x0011696B
		internal ComputedVar(int id, TypeUsage type) : base(id, VarType.Computed, type)
		{
		}
	}
}

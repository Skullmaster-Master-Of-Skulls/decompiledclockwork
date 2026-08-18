using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000659 RID: 1625
	internal abstract class VarInfo
	{
		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06003F84 RID: 16260
		internal abstract VarInfoKind Kind { get; }

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06003F85 RID: 16261 RVA: 0x00122A89 File Offset: 0x00120C89
		internal virtual List<Var> NewVars
		{
			get
			{
				return null;
			}
		}
	}
}

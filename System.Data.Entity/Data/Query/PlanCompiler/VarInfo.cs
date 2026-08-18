using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000082 RID: 130
	internal abstract class VarInfo
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600095F RID: 2399
		internal abstract VarInfoKind Kind { get; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00006174 File Offset: 0x00004374
		internal virtual List<Var> NewVars
		{
			get
			{
				return null;
			}
		}
	}
}

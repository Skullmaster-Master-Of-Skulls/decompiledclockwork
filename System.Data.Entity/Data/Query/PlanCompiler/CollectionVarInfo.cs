using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000083 RID: 131
	internal class CollectionVarInfo : VarInfo
	{
		// Token: 0x06000962 RID: 2402 RVA: 0x00033505 File Offset: 0x00031705
		internal CollectionVarInfo(Var newVar)
		{
			this.m_newVars = new List<Var>();
			this.m_newVars.Add(newVar);
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x00033524 File Offset: 0x00031724
		internal Var NewVar
		{
			get
			{
				return this.m_newVars[0];
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00033532 File Offset: 0x00031732
		internal override VarInfoKind Kind
		{
			get
			{
				return VarInfoKind.CollectionVarInfo;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x00033535 File Offset: 0x00031735
		internal override List<Var> NewVars
		{
			get
			{
				return this.m_newVars;
			}
		}

		// Token: 0x04000884 RID: 2180
		private List<Var> m_newVars;
	}
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200065A RID: 1626
	internal class CollectionVarInfo : VarInfo
	{
		// Token: 0x06003F87 RID: 16263 RVA: 0x00122A94 File Offset: 0x00120C94
		internal CollectionVarInfo(Var newVar)
		{
			this.m_newVars = new List<Var>();
			this.m_newVars.Add(newVar);
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x00122AB3 File Offset: 0x00120CB3
		internal Var NewVar
		{
			get
			{
				return this.m_newVars[0];
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06003F89 RID: 16265 RVA: 0x00122AC1 File Offset: 0x00120CC1
		internal override VarInfoKind Kind
		{
			get
			{
				return VarInfoKind.CollectionVarInfo;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06003F8A RID: 16266 RVA: 0x00122AC4 File Offset: 0x00120CC4
		internal override List<Var> NewVars
		{
			get
			{
				return this.m_newVars;
			}
		}

		// Token: 0x040017B4 RID: 6068
		private readonly List<Var> m_newVars;
	}
}

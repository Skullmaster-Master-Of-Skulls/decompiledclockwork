using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000085 RID: 133
	internal class PrimitiveTypeVarInfo : VarInfo
	{
		// Token: 0x0600096F RID: 2415 RVA: 0x0003366C File Offset: 0x0003186C
		internal PrimitiveTypeVarInfo(Var newVar)
		{
			this.m_newVars = new List<Var>
			{
				newVar
			};
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x00033686 File Offset: 0x00031886
		internal Var NewVar
		{
			get
			{
				return this.m_newVars[0];
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override VarInfoKind Kind
		{
			get
			{
				return VarInfoKind.PrimitiveTypeVarInfo;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00033694 File Offset: 0x00031894
		internal override List<Var> NewVars
		{
			get
			{
				return this.m_newVars;
			}
		}

		// Token: 0x0400088B RID: 2187
		private List<Var> m_newVars;
	}
}

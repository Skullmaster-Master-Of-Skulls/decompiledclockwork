using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000691 RID: 1681
	internal class PrimitiveTypeVarInfo : VarInfo
	{
		// Token: 0x06004270 RID: 17008 RVA: 0x0013ADC8 File Offset: 0x00138FC8
		internal PrimitiveTypeVarInfo(Var newVar)
		{
			this.m_newVars = new List<Var>
			{
				newVar
			};
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06004271 RID: 17009 RVA: 0x0013ADEF File Offset: 0x00138FEF
		internal Var NewVar
		{
			get
			{
				return this.m_newVars[0];
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06004272 RID: 17010 RVA: 0x0013ADFD File Offset: 0x00138FFD
		internal override VarInfoKind Kind
		{
			get
			{
				return VarInfoKind.PrimitiveTypeVarInfo;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06004273 RID: 17011 RVA: 0x0013AE00 File Offset: 0x00139000
		internal override List<Var> NewVars
		{
			get
			{
				return this.m_newVars;
			}
		}

		// Token: 0x0400189E RID: 6302
		private readonly List<Var> m_newVars;
	}
}

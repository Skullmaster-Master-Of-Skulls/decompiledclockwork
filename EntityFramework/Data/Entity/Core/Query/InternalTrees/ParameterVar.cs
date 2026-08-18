using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200060F RID: 1551
	internal sealed class ParameterVar : Var
	{
		// Token: 0x06003CFB RID: 15611 RVA: 0x0011AB6C File Offset: 0x00118D6C
		internal ParameterVar(int id, TypeUsage type, string paramName) : base(id, VarType.Parameter, type)
		{
			this.m_paramName = paramName;
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06003CFC RID: 15612 RVA: 0x0011AB7E File Offset: 0x00118D7E
		internal string ParameterName
		{
			get
			{
				return this.m_paramName;
			}
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x0011AB86 File Offset: 0x00118D86
		internal override bool TryGetName(out string name)
		{
			name = this.ParameterName;
			return true;
		}

		// Token: 0x04001710 RID: 5904
		private readonly string m_paramName;
	}
}

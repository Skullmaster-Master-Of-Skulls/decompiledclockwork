using System;
using System.Data.Metadata.Edm;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x0200010E RID: 270
	internal sealed class ParameterVar : Var
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003D138 File Offset: 0x0003B338
		internal ParameterVar(int id, TypeUsage type, string paramName) : base(id, VarType.Parameter, type)
		{
			this.m_paramName = paramName;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0003D14A File Offset: 0x0003B34A
		internal string ParameterName
		{
			get
			{
				return this.m_paramName;
			}
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0003D152 File Offset: 0x0003B352
		internal override bool TryGetName(out string name)
		{
			name = this.ParameterName;
			return true;
		}

		// Token: 0x040009D5 RID: 2517
		private string m_paramName;
	}
}

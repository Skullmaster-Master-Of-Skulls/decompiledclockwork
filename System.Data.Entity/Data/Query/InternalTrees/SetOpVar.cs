using System;
using System.Data.Metadata.Edm;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000111 RID: 273
	internal sealed class SetOpVar : Var
	{
		// Token: 0x06000DAB RID: 3499 RVA: 0x0003D1A6 File Offset: 0x0003B3A6
		internal SetOpVar(int id, TypeUsage type) : base(id, VarType.SetOp, type)
		{
		}
	}
}

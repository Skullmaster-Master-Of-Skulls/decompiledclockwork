using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000622 RID: 1570
	internal sealed class SetOpVar : Var
	{
		// Token: 0x06003D59 RID: 15705 RVA: 0x0011B091 File Offset: 0x00119291
		internal SetOpVar(int id, TypeUsage type) : base(id, VarType.SetOp, type)
		{
		}
	}
}

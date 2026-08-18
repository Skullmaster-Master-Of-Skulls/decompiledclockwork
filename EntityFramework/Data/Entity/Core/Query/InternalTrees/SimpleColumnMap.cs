using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200061D RID: 1565
	internal abstract class SimpleColumnMap : ColumnMap
	{
		// Token: 0x06003D43 RID: 15683 RVA: 0x0011AF77 File Offset: 0x00119177
		internal SimpleColumnMap(TypeUsage type, string name) : base(type, name)
		{
		}
	}
}

using System;
using System.Data.Metadata.Edm;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x0200009A RID: 154
	internal abstract class SimpleColumnMap : ColumnMap
	{
		// Token: 0x06000A05 RID: 2565 RVA: 0x00035DF1 File Offset: 0x00033FF1
		internal SimpleColumnMap(TypeUsage type, string name) : base(type, name)
		{
		}
	}
}

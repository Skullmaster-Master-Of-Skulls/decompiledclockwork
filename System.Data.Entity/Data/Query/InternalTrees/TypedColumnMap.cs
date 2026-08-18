using System;
using System.Data.Metadata.Edm;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x0200009E RID: 158
	internal abstract class TypedColumnMap : StructuredColumnMap
	{
		// Token: 0x06000A14 RID: 2580 RVA: 0x00035F2A File Offset: 0x0003412A
		internal TypedColumnMap(TypeUsage type, string name, ColumnMap[] properties) : base(type, name, properties)
		{
		}
	}
}

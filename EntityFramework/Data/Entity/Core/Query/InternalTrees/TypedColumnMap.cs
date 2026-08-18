using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005D9 RID: 1497
	internal abstract class TypedColumnMap : StructuredColumnMap
	{
		// Token: 0x06003BCA RID: 15306 RVA: 0x00118700 File Offset: 0x00116900
		internal TypedColumnMap(TypeUsage type, string name, ColumnMap[] properties) : base(type, name, properties)
		{
		}
	}
}

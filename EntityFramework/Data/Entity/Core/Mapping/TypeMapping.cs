using System;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003D3 RID: 979
	public abstract class TypeMapping : MappingItem
	{
		// Token: 0x060023AD RID: 9133 RVA: 0x000A5894 File Offset: 0x000A3A94
		internal TypeMapping()
		{
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060023AE RID: 9134
		internal abstract EntitySetBaseMapping SetMapping { get; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060023AF RID: 9135
		internal abstract ReadOnlyCollection<EntityTypeBase> Types { get; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060023B0 RID: 9136
		internal abstract ReadOnlyCollection<EntityTypeBase> IsOfTypes { get; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060023B1 RID: 9137
		internal abstract ReadOnlyCollection<MappingFragment> MappingFragments { get; }
	}
}

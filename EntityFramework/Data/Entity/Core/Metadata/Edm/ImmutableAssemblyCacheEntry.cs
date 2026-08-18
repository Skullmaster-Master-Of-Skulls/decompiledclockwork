using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000513 RID: 1299
	internal class ImmutableAssemblyCacheEntry : AssemblyCacheEntry
	{
		// Token: 0x060030FA RID: 12538 RVA: 0x000EA90C File Offset: 0x000E8B0C
		internal ImmutableAssemblyCacheEntry(MutableAssemblyCacheEntry mutableEntry)
		{
			this._typesInAssembly = new ReadOnlyCollection<EdmType>(new List<EdmType>(mutableEntry.TypesInAssembly));
			this._closureAssemblies = new ReadOnlyCollection<Assembly>(new List<Assembly>(mutableEntry.ClosureAssemblies));
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060030FB RID: 12539 RVA: 0x000EA940 File Offset: 0x000E8B40
		internal override IList<EdmType> TypesInAssembly
		{
			get
			{
				return this._typesInAssembly;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x000EA948 File Offset: 0x000E8B48
		internal override IList<Assembly> ClosureAssemblies
		{
			get
			{
				return this._closureAssemblies;
			}
		}

		// Token: 0x04001284 RID: 4740
		private readonly ReadOnlyCollection<EdmType> _typesInAssembly;

		// Token: 0x04001285 RID: 4741
		private readonly ReadOnlyCollection<Assembly> _closureAssemblies;
	}
}

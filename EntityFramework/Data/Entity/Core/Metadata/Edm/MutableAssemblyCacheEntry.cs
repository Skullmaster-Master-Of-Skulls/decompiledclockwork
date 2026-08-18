using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000519 RID: 1305
	internal class MutableAssemblyCacheEntry : AssemblyCacheEntry
	{
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x000EAEE4 File Offset: 0x000E90E4
		internal override IList<EdmType> TypesInAssembly
		{
			get
			{
				return this._typesInAssembly;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x0600311E RID: 12574 RVA: 0x000EAEEC File Offset: 0x000E90EC
		internal override IList<Assembly> ClosureAssemblies
		{
			get
			{
				return this._closureAssemblies;
			}
		}

		// Token: 0x04001293 RID: 4755
		private readonly List<EdmType> _typesInAssembly = new List<EdmType>();

		// Token: 0x04001294 RID: 4756
		private readonly List<Assembly> _closureAssemblies = new List<Assembly>();
	}
}

using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000512 RID: 1298
	internal abstract class AssemblyCacheEntry
	{
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060030F5 RID: 12533
		internal abstract IList<EdmType> TypesInAssembly { get; }

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060030F6 RID: 12534
		internal abstract IList<Assembly> ClosureAssemblies { get; }

		// Token: 0x060030F7 RID: 12535 RVA: 0x000EA888 File Offset: 0x000E8A88
		internal bool TryGetEdmType(string typeName, out EdmType edmType)
		{
			edmType = null;
			foreach (EdmType edmType2 in this.TypesInAssembly)
			{
				if (edmType2.Identity == typeName)
				{
					edmType = edmType2;
					break;
				}
			}
			return edmType != null;
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x000EA8EC File Offset: 0x000E8AEC
		internal bool ContainsType(string typeName)
		{
			EdmType edmType = null;
			return this.TryGetEdmType(typeName, out edmType);
		}
	}
}

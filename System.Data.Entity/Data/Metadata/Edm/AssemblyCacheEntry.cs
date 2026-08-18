using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000215 RID: 533
	internal abstract class AssemblyCacheEntry
	{
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06002313 RID: 8979
		internal abstract IList<EdmType> TypesInAssembly { get; }

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06002314 RID: 8980
		internal abstract IList<Assembly> ClosureAssemblies { get; }

		// Token: 0x06002315 RID: 8981 RVA: 0x0007CB80 File Offset: 0x0007AD80
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

		// Token: 0x06002316 RID: 8982 RVA: 0x0007CBE4 File Offset: 0x0007ADE4
		internal bool ContainsType(string typeName)
		{
			EdmType edmType = null;
			return this.TryGetEdmType(typeName, out edmType);
		}
	}
}

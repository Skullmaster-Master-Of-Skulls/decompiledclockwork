using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x0200021A RID: 538
	internal class MutableAssemblyCacheEntry : AssemblyCacheEntry
	{
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x0007CE33 File Offset: 0x0007B033
		internal override IList<EdmType> TypesInAssembly
		{
			get
			{
				return this._typesInAssembly;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002330 RID: 9008 RVA: 0x0007CE3B File Offset: 0x0007B03B
		internal override IList<Assembly> ClosureAssemblies
		{
			get
			{
				return this._closureAssemblies;
			}
		}

		// Token: 0x04000FA5 RID: 4005
		private readonly List<EdmType> _typesInAssembly = new List<EdmType>();

		// Token: 0x04000FA6 RID: 4006
		private readonly List<Assembly> _closureAssemblies = new List<Assembly>();
	}
}

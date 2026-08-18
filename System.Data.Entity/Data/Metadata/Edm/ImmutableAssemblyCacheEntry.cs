using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000216 RID: 534
	internal class ImmutableAssemblyCacheEntry : AssemblyCacheEntry
	{
		// Token: 0x06002318 RID: 8984 RVA: 0x0007CBFC File Offset: 0x0007ADFC
		internal ImmutableAssemblyCacheEntry(MutableAssemblyCacheEntry mutableEntry)
		{
			this._typesInAssembly = new List<EdmType>(mutableEntry.TypesInAssembly).AsReadOnly();
			this._closureAssemblies = new List<Assembly>(mutableEntry.ClosureAssemblies).AsReadOnly();
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x0007CC30 File Offset: 0x0007AE30
		internal override IList<EdmType> TypesInAssembly
		{
			get
			{
				return this._typesInAssembly;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x0007CC38 File Offset: 0x0007AE38
		internal override IList<Assembly> ClosureAssemblies
		{
			get
			{
				return this._closureAssemblies;
			}
		}

		// Token: 0x04000F9B RID: 3995
		private readonly ReadOnlyCollection<EdmType> _typesInAssembly;

		// Token: 0x04000F9C RID: 3996
		private readonly ReadOnlyCollection<Assembly> _closureAssemblies;
	}
}

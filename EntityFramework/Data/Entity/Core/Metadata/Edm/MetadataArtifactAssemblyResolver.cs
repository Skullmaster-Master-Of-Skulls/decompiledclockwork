using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B4 RID: 1204
	internal abstract class MetadataArtifactAssemblyResolver
	{
		// Token: 0x06002C6E RID: 11374
		internal abstract bool TryResolveAssemblyReference(AssemblyName refernceName, out Assembly assembly);

		// Token: 0x06002C6F RID: 11375
		internal abstract IEnumerable<Assembly> GetWildcardAssemblies();
	}
}

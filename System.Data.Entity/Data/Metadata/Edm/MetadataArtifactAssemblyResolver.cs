using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x02000213 RID: 531
	internal abstract class MetadataArtifactAssemblyResolver
	{
		// Token: 0x06002308 RID: 8968
		internal abstract bool TryResolveAssemblyReference(AssemblyName refernceName, out Assembly assembly);

		// Token: 0x06002309 RID: 8969
		internal abstract IEnumerable<Assembly> GetWildcardAssemblies();
	}
}

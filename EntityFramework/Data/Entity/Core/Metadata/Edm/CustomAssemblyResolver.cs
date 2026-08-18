using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004B5 RID: 1205
	internal class CustomAssemblyResolver : MetadataArtifactAssemblyResolver
	{
		// Token: 0x06002C71 RID: 11377 RVA: 0x000D8FA6 File Offset: 0x000D71A6
		internal CustomAssemblyResolver(Func<IEnumerable<Assembly>> wildcardAssemblyEnumerator, Func<AssemblyName, Assembly> referenceResolver)
		{
			this._wildcardAssemblyEnumerator = wildcardAssemblyEnumerator;
			this._referenceResolver = referenceResolver;
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x000D8FBC File Offset: 0x000D71BC
		internal override bool TryResolveAssemblyReference(AssemblyName refernceName, out Assembly assembly)
		{
			assembly = this._referenceResolver(refernceName);
			return assembly != null;
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x000D8FD4 File Offset: 0x000D71D4
		internal override IEnumerable<Assembly> GetWildcardAssemblies()
		{
			IEnumerable<Assembly> enumerable = this._wildcardAssemblyEnumerator();
			if (enumerable == null)
			{
				throw new InvalidOperationException(Strings.WildcardEnumeratorReturnedNull);
			}
			return enumerable;
		}

		// Token: 0x0400105E RID: 4190
		private readonly Func<AssemblyName, Assembly> _referenceResolver;

		// Token: 0x0400105F RID: 4191
		private readonly Func<IEnumerable<Assembly>> _wildcardAssemblyEnumerator;
	}
}

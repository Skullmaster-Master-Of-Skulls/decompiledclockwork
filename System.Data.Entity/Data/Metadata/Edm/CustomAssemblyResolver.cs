using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;

namespace System.Data.Metadata.Edm
{
	// Token: 0x0200020F RID: 527
	internal class CustomAssemblyResolver : MetadataArtifactAssemblyResolver
	{
		// Token: 0x060022EF RID: 8943 RVA: 0x0007C2C4 File Offset: 0x0007A4C4
		internal CustomAssemblyResolver(Func<IEnumerable<Assembly>> wildcardAssemblyEnumerator, Func<AssemblyName, Assembly> referenceResolver)
		{
			this._wildcardAssemblyEnumerator = wildcardAssemblyEnumerator;
			this._referenceResolver = referenceResolver;
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x0007C2DA File Offset: 0x0007A4DA
		internal override bool TryResolveAssemblyReference(AssemblyName refernceName, out Assembly assembly)
		{
			assembly = this._referenceResolver(refernceName);
			return assembly != null;
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x0007C2F4 File Offset: 0x0007A4F4
		internal override IEnumerable<Assembly> GetWildcardAssemblies()
		{
			IEnumerable<Assembly> enumerable = this._wildcardAssemblyEnumerator();
			if (enumerable == null)
			{
				throw EntityUtil.InvalidOperation(Strings.WildcardEnumeratorReturnedNull);
			}
			return enumerable;
		}

		// Token: 0x04000F8D RID: 3981
		private Func<AssemblyName, Assembly> _referenceResolver;

		// Token: 0x04000F8E RID: 3982
		private Func<IEnumerable<Assembly>> _wildcardAssemblyEnumerator;
	}
}

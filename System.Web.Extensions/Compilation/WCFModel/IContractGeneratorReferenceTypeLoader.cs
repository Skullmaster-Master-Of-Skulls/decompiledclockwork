using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000016 RID: 22
	internal interface IContractGeneratorReferenceTypeLoader
	{
		// Token: 0x060000D5 RID: 213
		Type LoadType(string typeName);

		// Token: 0x060000D6 RID: 214
		Assembly LoadAssembly(string assemblyName);

		// Token: 0x060000D7 RID: 215
		void LoadAllAssemblies(out IEnumerable<Assembly> loadedAssemblies, out IEnumerable<Exception> loadingErrors);
	}
}

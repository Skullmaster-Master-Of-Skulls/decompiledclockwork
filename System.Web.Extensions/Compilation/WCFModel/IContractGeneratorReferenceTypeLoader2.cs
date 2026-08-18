using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Web.Compilation.WCFModel
{
	// Token: 0x02000017 RID: 23
	internal interface IContractGeneratorReferenceTypeLoader2
	{
		// Token: 0x060000D8 RID: 216
		IEnumerable<Type> LoadExportedTypes(Assembly assembly);
	}
}

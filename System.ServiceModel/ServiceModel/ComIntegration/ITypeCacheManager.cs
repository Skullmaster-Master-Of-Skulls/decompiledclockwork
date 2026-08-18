using System;
using System.Reflection;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000233 RID: 563
	internal interface ITypeCacheManager
	{
		// Token: 0x060010D1 RID: 4305
		void FindOrCreateType(Guid riid, out Type interfaceType, bool noAssemblyGeneration, bool isServer);

		// Token: 0x060010D2 RID: 4306
		void FindOrCreateType(Type serverType, Guid riid, out Type interfaceType, bool noAssemblyGeneration, bool isServer);

		// Token: 0x060010D3 RID: 4307
		void FindOrCreateType(Guid typeLibId, string typeLibVersion, Guid typeDefId, out Type userDefinedType, bool noAssemblyGeneration);

		// Token: 0x060010D4 RID: 4308
		Assembly ResolveAssembly(Guid assembly);
	}
}

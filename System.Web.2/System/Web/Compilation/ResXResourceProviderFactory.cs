using System;

namespace System.Web.Compilation
{
	// Token: 0x0200085B RID: 2139
	internal class ResXResourceProviderFactory : ResourceProviderFactory
	{
		// Token: 0x06006549 RID: 25929 RVA: 0x00164972 File Offset: 0x00162B72
		public override IResourceProvider CreateGlobalResourceProvider(string classKey)
		{
			return new GlobalResXResourceProvider(classKey);
		}

		// Token: 0x0600654A RID: 25930 RVA: 0x0016497A File Offset: 0x00162B7A
		public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
		{
			return new LocalResXResourceProvider(VirtualPath.Create(virtualPath));
		}
	}
}

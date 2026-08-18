using System;

namespace System.Web.Compilation
{
	// Token: 0x0200085A RID: 2138
	public abstract class ResourceProviderFactory
	{
		// Token: 0x06006546 RID: 25926
		public abstract IResourceProvider CreateGlobalResourceProvider(string classKey);

		// Token: 0x06006547 RID: 25927
		public abstract IResourceProvider CreateLocalResourceProvider(string virtualPath);
	}
}

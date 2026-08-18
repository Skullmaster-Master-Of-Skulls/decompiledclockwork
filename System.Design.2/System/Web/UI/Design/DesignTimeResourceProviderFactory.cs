using System;
using System.Web.Compilation;

namespace System.Web.UI.Design
{
	// Token: 0x0200003A RID: 58
	public abstract class DesignTimeResourceProviderFactory
	{
		// Token: 0x06000208 RID: 520
		public abstract IResourceProvider CreateDesignTimeGlobalResourceProvider(IServiceProvider serviceProvider, string classKey);

		// Token: 0x06000209 RID: 521
		public abstract IResourceProvider CreateDesignTimeLocalResourceProvider(IServiceProvider serviceProvider);

		// Token: 0x0600020A RID: 522
		public abstract IDesignTimeResourceWriter CreateDesignTimeLocalResourceWriter(IServiceProvider serviceProvider);
	}
}

using System;

namespace System.Web.WebPages
{
	// Token: 0x02000061 RID: 97
	internal static class VirtualPathFactoryExtensions
	{
		// Token: 0x06000265 RID: 613 RVA: 0x000099B8 File Offset: 0x00007BB8
		public static T CreateInstance<T>(this IVirtualPathFactory factory, string virtualPath) where T : class
		{
			VirtualPathFactoryManager virtualPathFactoryManager = factory as VirtualPathFactoryManager;
			if (virtualPathFactoryManager != null)
			{
				return virtualPathFactoryManager.CreateInstanceOfType<T>(virtualPath);
			}
			BuildManagerWrapper buildManagerWrapper = factory as BuildManagerWrapper;
			if (buildManagerWrapper != null)
			{
				return buildManagerWrapper.CreateInstanceOfType<T>(virtualPath);
			}
			return factory.CreateInstance(virtualPath) as T;
		}
	}
}

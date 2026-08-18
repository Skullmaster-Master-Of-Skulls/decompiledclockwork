using System;
using System.IO;
using System.Reflection;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000820 RID: 2080
	internal abstract class BuildResultCache
	{
		// Token: 0x06006378 RID: 25464 RVA: 0x0015C6F3 File Offset: 0x0015A8F3
		internal BuildResult GetBuildResult(string cacheKey)
		{
			return this.GetBuildResult(cacheKey, null, 0L, true);
		}

		// Token: 0x06006379 RID: 25465
		internal abstract BuildResult GetBuildResult(string cacheKey, VirtualPath virtualPath, long hashCode, bool ensureIsUpToDate = true);

		// Token: 0x0600637A RID: 25466 RVA: 0x0015C700 File Offset: 0x0015A900
		internal void CacheBuildResult(string cacheKey, BuildResult result, DateTime utcStart)
		{
			this.CacheBuildResult(cacheKey, result, 0L, utcStart);
		}

		// Token: 0x0600637B RID: 25467
		internal abstract void CacheBuildResult(string cacheKey, BuildResult result, long hashCode, DateTime utcStart);

		// Token: 0x0600637C RID: 25468 RVA: 0x0015C710 File Offset: 0x0015A910
		internal static string GetAssemblyCacheKey(string assemblyPath)
		{
			string assemblyNameFromFileName = Util.GetAssemblyNameFromFileName(Path.GetFileName(assemblyPath));
			return BuildResultCache.GetAssemblyCacheKeyFromName(assemblyNameFromFileName);
		}

		// Token: 0x0600637D RID: 25469 RVA: 0x0015C72F File Offset: 0x0015A92F
		internal static string GetAssemblyCacheKey(Assembly assembly)
		{
			return BuildResultCache.GetAssemblyCacheKeyFromName(assembly.GetName().Name);
		}

		// Token: 0x0600637E RID: 25470 RVA: 0x0015C741 File Offset: 0x0015A941
		internal static string GetAssemblyCacheKeyFromName(string assemblyName)
		{
			return "y" + assemblyName.ToLowerInvariant();
		}
	}
}

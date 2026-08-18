using System;

namespace System.Web.Compilation
{
	// Token: 0x02000826 RID: 2086
	internal class UpdatablePrecompilerDiskBuildResultCache : PrecompilerDiskBuildResultCache
	{
		// Token: 0x060063AD RID: 25517 RVA: 0x0015D788 File Offset: 0x0015B988
		internal UpdatablePrecompilerDiskBuildResultCache(string cacheDir) : base(cacheDir)
		{
		}

		// Token: 0x060063AE RID: 25518 RVA: 0x0015D791 File Offset: 0x0015B991
		internal override void CacheBuildResult(string cacheKey, BuildResult result, long hashCode, DateTime utcStart)
		{
			if (result is BuildResultCompiledTemplateType)
			{
				return;
			}
			base.CacheBuildResult(cacheKey, result, hashCode, utcStart);
		}
	}
}

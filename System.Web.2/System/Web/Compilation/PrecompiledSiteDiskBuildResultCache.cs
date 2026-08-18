using System;

namespace System.Web.Compilation
{
	// Token: 0x02000827 RID: 2087
	internal class PrecompiledSiteDiskBuildResultCache : PrecompBaseDiskBuildResultCache
	{
		// Token: 0x060063AF RID: 25519 RVA: 0x0015D7A7 File Offset: 0x0015B9A7
		internal PrecompiledSiteDiskBuildResultCache(string cacheDir) : base(cacheDir)
		{
		}

		// Token: 0x17001C2F RID: 7215
		// (get) Token: 0x060063B0 RID: 25520 RVA: 0x000097B7 File Offset: 0x000079B7
		protected override bool PrecompilationMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060063B1 RID: 25521 RVA: 0x00006164 File Offset: 0x00004364
		internal override void CacheBuildResult(string cacheKey, BuildResult result, long hashCode, DateTime utcStart)
		{
		}

		// Token: 0x060063B2 RID: 25522 RVA: 0x00006164 File Offset: 0x00004364
		internal override void RemoveAssemblyAndRelatedFiles(string baseName)
		{
		}
	}
}

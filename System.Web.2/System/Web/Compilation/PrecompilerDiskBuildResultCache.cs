using System;

namespace System.Web.Compilation
{
	// Token: 0x02000825 RID: 2085
	internal class PrecompilerDiskBuildResultCache : PrecompBaseDiskBuildResultCache
	{
		// Token: 0x060063AC RID: 25516 RVA: 0x0015D779 File Offset: 0x0015B979
		internal PrecompilerDiskBuildResultCache(string cacheDir) : base(cacheDir)
		{
			base.EnsureDiskCacheDirectoryCreated();
		}
	}
}

using System;
using System.IO;
using System.Web.Compilation;

namespace System.Web.WebPages.Deployment
{
	// Token: 0x02000007 RID: 7
	internal sealed class BuildManagerWrapper : IBuildManager
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002D87 File Offset: 0x00000F87
		public Stream ReadCachedFile(string path)
		{
			return BuildManager.ReadCachedFile(path);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002D8F File Offset: 0x00000F8F
		public Stream CreateCachedFile(string path)
		{
			return BuildManager.CreateCachedFile(path);
		}
	}
}

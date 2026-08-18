using System;
using System.IO;

namespace System.Web.WebPages.Deployment
{
	// Token: 0x02000006 RID: 6
	internal interface IBuildManager
	{
		// Token: 0x0600002C RID: 44
		Stream CreateCachedFile(string fileName);

		// Token: 0x0600002D RID: 45
		Stream ReadCachedFile(string fileName);
	}
}

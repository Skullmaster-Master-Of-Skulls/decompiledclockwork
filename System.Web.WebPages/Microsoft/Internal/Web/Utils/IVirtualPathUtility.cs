using System;

namespace Microsoft.Internal.Web.Utils
{
	// Token: 0x02000043 RID: 67
	internal interface IVirtualPathUtility
	{
		// Token: 0x060001D6 RID: 470
		string Combine(string basePath, string relativePath);

		// Token: 0x060001D7 RID: 471
		string ToAbsolute(string virtualPath);
	}
}

using System;
using System.Collections.Generic;

namespace System.Web.Optimization
{
	// Token: 0x02000010 RID: 16
	public interface IBundleResolver
	{
		// Token: 0x06000099 RID: 153
		bool IsBundleVirtualPath(string virtualPath);

		// Token: 0x0600009A RID: 154
		IEnumerable<string> GetBundleContents(string virtualPath);

		// Token: 0x0600009B RID: 155
		string GetBundleUrl(string virtualPath);
	}
}

using System;
using System.Collections.Generic;

namespace System.Web.Optimization
{
	// Token: 0x0200002E RID: 46
	public interface IBundleBuilder
	{
		// Token: 0x0600015B RID: 347
		string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files);
	}
}

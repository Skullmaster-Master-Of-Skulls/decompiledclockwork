using System;
using System.Collections.Generic;

namespace System.Web.Optimization
{
	// Token: 0x02000032 RID: 50
	public interface IBundleOrderer
	{
		// Token: 0x0600016A RID: 362
		IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files);
	}
}

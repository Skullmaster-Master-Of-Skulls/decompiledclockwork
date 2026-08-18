using System;

namespace System.Web.Optimization
{
	// Token: 0x02000006 RID: 6
	internal interface IBundleCache
	{
		// Token: 0x06000014 RID: 20
		bool IsEnabled(BundleContext context);

		// Token: 0x06000015 RID: 21
		BundleResponse Get(BundleContext context, Bundle bundle);

		// Token: 0x06000016 RID: 22
		void Put(BundleContext context, Bundle bundle, BundleResponse response);
	}
}

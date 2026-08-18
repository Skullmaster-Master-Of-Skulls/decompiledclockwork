using System;

namespace System.Web.Optimization
{
	// Token: 0x02000030 RID: 48
	public interface IBundleTransform
	{
		// Token: 0x06000166 RID: 358
		void Process(BundleContext context, BundleResponse response);
	}
}

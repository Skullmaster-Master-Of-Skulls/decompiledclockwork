using System;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000229 RID: 553
	internal interface IProxyCreator : IDisposable
	{
		// Token: 0x060010AE RID: 4270
		ComProxy CreateProxy(IntPtr outer, ref Guid riid);

		// Token: 0x060010AF RID: 4271
		bool SupportsErrorInfo(ref Guid riid);

		// Token: 0x060010B0 RID: 4272
		bool SupportsDispatch();

		// Token: 0x060010B1 RID: 4273
		bool SupportsIntrinsics();
	}
}

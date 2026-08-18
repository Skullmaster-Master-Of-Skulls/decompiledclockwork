using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000266 RID: 614
	[Guid("186d89bc-f277-4bcc-80d5-4df7b836ef4a")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IServiceThreadPoolConfig
	{
		// Token: 0x06001198 RID: 4504
		void SelectThreadPool(ThreadPoolOption threadPool);

		// Token: 0x06001199 RID: 4505
		void SetBindingInfo(BindingOption binding);
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200026D RID: 621
	[Guid("67532E0C-9E2F-4450-A354-035633944E17")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IServiceActivity
	{
		// Token: 0x060011AE RID: 4526
		void SynchronousCall(IServiceCall pIServiceCall);

		// Token: 0x060011AF RID: 4527
		void AsynchronousCall(IServiceCall pIServiceCall);

		// Token: 0x060011B0 RID: 4528
		void BindToCurrentThread();

		// Token: 0x060011B1 RID: 4529
		void UnbindFromThread();
	}
}

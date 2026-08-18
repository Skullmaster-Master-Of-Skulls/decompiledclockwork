using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200026C RID: 620
	[Guid("BD3E2E12-42DD-40f4-A09A-95A50C58304B")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IServiceCall
	{
		// Token: 0x060011AD RID: 4525
		void OnCall();
	}
}

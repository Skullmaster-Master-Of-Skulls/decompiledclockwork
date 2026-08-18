using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000238 RID: 568
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0000010c-0000-0000-C000-000000000046")]
	internal interface IPersist
	{
		// Token: 0x060010F2 RID: 4338
		void GetClassID(out Guid pClassID);
	}
}

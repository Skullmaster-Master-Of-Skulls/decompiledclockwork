using System;
using System.Runtime.InteropServices;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x0200026E RID: 622
	[Guid("000001ce-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IComThreadingInfo
	{
		// Token: 0x060011B2 RID: 4530
		void GetCurrentApartmentType(out uint aptType);

		// Token: 0x060011B3 RID: 4531
		void GetCurrentThreadType(out uint threadType);

		// Token: 0x060011B4 RID: 4532
		void GetCurrentLogicalThreadId(out Guid guidLogicalThreadID);

		// Token: 0x060011B5 RID: 4533
		void SetCurrentLogicalThreadId([MarshalAs(UnmanagedType.LPStruct)] [In] Guid guidLogicalThreadID);
	}
}

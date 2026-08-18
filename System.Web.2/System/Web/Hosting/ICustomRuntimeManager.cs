using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x0200078E RID: 1934
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("A0BBBDFF-5AF5-42E3-9753-34441F764A6B")]
	[ComImport]
	internal interface ICustomRuntimeManager
	{
		// Token: 0x06005C89 RID: 23689
		[return: MarshalAs(UnmanagedType.Interface)]
		ICustomRuntimeRegistrationToken Register([MarshalAs(UnmanagedType.Interface)] [In] ICustomRuntime customRuntime);
	}
}

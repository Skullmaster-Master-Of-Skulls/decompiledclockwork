using System;
using System.Runtime.InteropServices;

// Token: 0x02000002 RID: 2
[ComVisible(false)]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[Guid("00000001-0000-0000-C000-000000000046")]
[ComImport]
internal interface IClassFactory
{
	// Token: 0x06000001 RID: 1
	[return: MarshalAs(UnmanagedType.Interface)]
	object CreateInstance([MarshalAs(UnmanagedType.IUnknown)] [In] object pUnkOuter, [MarshalAs(UnmanagedType.LPStruct)] [In] Guid riid);

	// Token: 0x06000002 RID: 2
	void LockServer([MarshalAs(UnmanagedType.Bool)] [In] bool fLock);
}

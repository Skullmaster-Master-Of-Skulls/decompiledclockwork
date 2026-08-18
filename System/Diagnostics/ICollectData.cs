using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x0200075E RID: 1886
	[Guid("73386977-D6FD-11D2-BED5-00C04F79E3AE")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface ICollectData
	{
		// Token: 0x06003A05 RID: 14853
		[return: MarshalAs(UnmanagedType.I4)]
		void CollectData([MarshalAs(UnmanagedType.I4)] [In] int id, [MarshalAs(UnmanagedType.SysInt)] [In] IntPtr valueName, [MarshalAs(UnmanagedType.SysInt)] [In] IntPtr data, [MarshalAs(UnmanagedType.I4)] [In] int totalBytes, [MarshalAs(UnmanagedType.SysInt)] out IntPtr res);

		// Token: 0x06003A06 RID: 14854
		void CloseData();
	}
}

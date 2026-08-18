using System;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x020002CF RID: 719
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct MEMMAP
	{
		// Token: 0x0400178A RID: 6026
		[MarshalAs(UnmanagedType.U4)]
		internal uint dbgpid;

		// Token: 0x0400178B RID: 6027
		[MarshalAs(UnmanagedType.U4)]
		internal uint fOption;

		// Token: 0x0400178C RID: 6028
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		internal byte[] rgbMachineName;

		// Token: 0x0400178D RID: 6029
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] rgbDllName;

		// Token: 0x0400178E RID: 6030
		[MarshalAs(UnmanagedType.U4)]
		internal uint cbData;

		// Token: 0x0400178F RID: 6031
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
		internal byte[] rgbData;
	}
}

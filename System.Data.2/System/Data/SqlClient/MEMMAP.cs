using System;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x020001B9 RID: 441
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct MEMMAP
	{
		// Token: 0x04000F8B RID: 3979
		[MarshalAs(UnmanagedType.U4)]
		internal uint dbgpid;

		// Token: 0x04000F8C RID: 3980
		[MarshalAs(UnmanagedType.U4)]
		internal uint fOption;

		// Token: 0x04000F8D RID: 3981
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		internal byte[] rgbMachineName;

		// Token: 0x04000F8E RID: 3982
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] rgbDllName;

		// Token: 0x04000F8F RID: 3983
		[MarshalAs(UnmanagedType.U4)]
		internal uint cbData;

		// Token: 0x04000F90 RID: 3984
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 255)]
		internal byte[] rgbData;
	}
}

using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

// Token: 0x0200000A RID: 10
[CLSCompliant(false)]
internal class Win32NativeMethods
{
	// Token: 0x0600008D RID: 141 RVA: 0x00004040 File Offset: 0x00003440
	[ResourceExposure(ResourceScope.None)]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool IsTokenRestrictedWrapper(IntPtr token)
	{
		int num = 0;
		uint num2 = <Module>.UnmanagedIsTokenRestricted(token.ToPointer(), &num);
		if (0 != num2)
		{
			Marshal.ThrowExceptionForHR((num2 > 0) ? ((num2 & 65535) | -2147024896) : num2);
		}
		return ((num != 0) ? 1 : 0) != 0;
	}
}

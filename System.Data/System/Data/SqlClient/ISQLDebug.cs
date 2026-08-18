using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Data.SqlClient
{
	// Token: 0x020002CC RID: 716
	[Guid("6cb925bf-c3c0-45b3-9f44-5dd67c7b7fe8")]
	[BestFitMapping(false, ThrowOnUnmappableChar = true)]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ISQLDebug
	{
		// Token: 0x060024DC RID: 9436
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		bool SQLDebug(int dwpidDebugger, int dwpidDebuggee, [MarshalAs(UnmanagedType.LPStr)] string pszMachineName, [MarshalAs(UnmanagedType.LPStr)] string pszSDIDLLName, int dwOption, int cbData, byte[] rgbData);
	}
}

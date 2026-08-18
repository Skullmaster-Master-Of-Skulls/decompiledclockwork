using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Management
{
	// Token: 0x02000172 RID: 370
	[Guid("c84f668a-cc3f-11d7-b79e-505054503030")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IRegiisUtility
	{
		// Token: 0x06001486 RID: 5254
		void ProtectedConfigAction(long actionToPerform, [MarshalAs(UnmanagedType.LPWStr)] [In] string firstArgument, [MarshalAs(UnmanagedType.LPWStr)] [In] string secondArgument, [MarshalAs(UnmanagedType.LPWStr)] [In] string providerName, [MarshalAs(UnmanagedType.LPWStr)] [In] string appPath, [MarshalAs(UnmanagedType.LPWStr)] [In] string site, [MarshalAs(UnmanagedType.LPWStr)] [In] string cspOrLocation, int keySize, out IntPtr exception);

		// Token: 0x06001487 RID: 5255
		void RegisterSystemWebAssembly(int doReg, out IntPtr exception);

		// Token: 0x06001488 RID: 5256
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		void RegisterAsnetMmcAssembly(int doReg, [MarshalAs(UnmanagedType.LPWStr)] [In] string assemblyName, [MarshalAs(UnmanagedType.LPWStr)] [In] string binaryDirectory, out IntPtr exception);

		// Token: 0x06001489 RID: 5257
		void RemoveBrowserCaps(out IntPtr exception);
	}
}

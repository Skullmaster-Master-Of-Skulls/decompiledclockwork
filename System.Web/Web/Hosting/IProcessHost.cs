using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x020002B2 RID: 690
	[Guid("0ccd465e-3114-4ca3-ad50-cea561307e93")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[ComImport]
	public interface IProcessHost
	{
		// Token: 0x060023F5 RID: 9205
		void StartApplication([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string appPath, [MarshalAs(UnmanagedType.Interface)] out object runtimeInterface);

		// Token: 0x060023F6 RID: 9206
		void ShutdownApplication([MarshalAs(UnmanagedType.LPWStr)] [In] string appId);

		// Token: 0x060023F7 RID: 9207
		void Shutdown();

		// Token: 0x060023F8 RID: 9208
		void EnumerateAppDomains([MarshalAs(UnmanagedType.Interface)] out IAppDomainInfoEnum appDomainInfoEnum);
	}
}

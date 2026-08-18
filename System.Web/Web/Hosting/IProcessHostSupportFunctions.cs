using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x02000298 RID: 664
	[Guid("35f9c4c1-3800-4d17-99bc-018a62243687")]
	[SuppressUnmanagedCodeSecurity]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[ComImport]
	public interface IProcessHostSupportFunctions
	{
		// Token: 0x060022D0 RID: 8912
		void GetApplicationProperties([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, out string virtualPath, out string physicalPath, out string siteName, out string siteId);

		// Token: 0x060022D1 RID: 8913
		void MapPath([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string virtualPath, out string physicalPath);

		// Token: 0x060022D2 RID: 8914
		[return: MarshalAs(UnmanagedType.SysInt)]
		IntPtr GetConfigToken([MarshalAs(UnmanagedType.LPWStr)] [In] string appId);

		// Token: 0x060022D3 RID: 8915
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetAppHostConfigFilename();

		// Token: 0x060022D4 RID: 8916
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetRootWebConfigFilename();

		// Token: 0x060022D5 RID: 8917
		[return: MarshalAs(UnmanagedType.SysInt)]
		IntPtr GetNativeConfigurationSystem();
	}
}

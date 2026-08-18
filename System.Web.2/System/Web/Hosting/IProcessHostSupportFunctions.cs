using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x02000798 RID: 1944
	[Guid("35f9c4c1-3800-4d17-99bc-018a62243687")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[SuppressUnmanagedCodeSecurity]
	[ComImport]
	public interface IProcessHostSupportFunctions
	{
		// Token: 0x06005C9D RID: 23709
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		void GetApplicationProperties([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, out string virtualPath, out string physicalPath, out string siteName, out string siteId);

		// Token: 0x06005C9E RID: 23710
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		void MapPath([MarshalAs(UnmanagedType.LPWStr)] [In] string appId, [MarshalAs(UnmanagedType.LPWStr)] [In] string virtualPath, out string physicalPath);

		// Token: 0x06005C9F RID: 23711
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[return: MarshalAs(UnmanagedType.SysInt)]
		IntPtr GetConfigToken([MarshalAs(UnmanagedType.LPWStr)] [In] string appId);

		// Token: 0x06005CA0 RID: 23712
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetAppHostConfigFilename();

		// Token: 0x06005CA1 RID: 23713
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[return: MarshalAs(UnmanagedType.BStr)]
		string GetRootWebConfigFilename();

		// Token: 0x06005CA2 RID: 23714
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[return: MarshalAs(UnmanagedType.SysInt)]
		IntPtr GetNativeConfigurationSystem();
	}
}

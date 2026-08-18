using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x020002B5 RID: 693
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("9d98b251-453e-44f6-9cec-8b5aed970129")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[ComImport]
	public interface IProcessHostIdleAndHealthCheck
	{
		// Token: 0x060023FF RID: 9215
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsIdle();

		// Token: 0x06002400 RID: 9216
		void Ping(IProcessPingCallback callback);
	}
}

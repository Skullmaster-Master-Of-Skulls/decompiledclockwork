using System;
using System.Security.Permissions;

namespace System.Drawing
{
	// Token: 0x0200004D RID: 77
	public interface IDeviceContext : IDisposable
	{
		// Token: 0x060006F1 RID: 1777
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		IntPtr GetHdc();

		// Token: 0x060006F2 RID: 1778
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		void ReleaseHdc();
	}
}

using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x020007BF RID: 1983
	[Guid("08a2c56f-7c16-41c1-a8be-432917a1a2d1")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IISAPIRuntime
	{
		// Token: 0x06005F11 RID: 24337
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		void StartProcessing();

		// Token: 0x06005F12 RID: 24338
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		void StopProcessing();

		// Token: 0x06005F13 RID: 24339
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[return: MarshalAs(UnmanagedType.I4)]
		int ProcessRequest([In] IntPtr ecb, [MarshalAs(UnmanagedType.I4)] [In] int useProcessModel);

		// Token: 0x06005F14 RID: 24340
		[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
		void DoGCCollect();
	}
}

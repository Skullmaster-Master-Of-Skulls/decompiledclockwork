using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x0200029A RID: 666
	[Guid("dc3b0a85-9da7-47e4-ba1b-e27da9db8a1e")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[ComImport]
	public interface IListenerChannelCallback
	{
		// Token: 0x060022D7 RID: 8919
		void ReportStarted();

		// Token: 0x060022D8 RID: 8920
		void ReportStopped(int hr);

		// Token: 0x060022D9 RID: 8921
		void ReportMessageReceived();

		// Token: 0x060022DA RID: 8922
		int GetId();

		// Token: 0x060022DB RID: 8923
		int GetBlobLength();

		// Token: 0x060022DC RID: 8924
		void GetBlob([MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] buffer, ref int bufferSize);
	}
}

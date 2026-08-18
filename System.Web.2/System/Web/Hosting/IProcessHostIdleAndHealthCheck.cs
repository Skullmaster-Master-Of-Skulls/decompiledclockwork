using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x020007DB RID: 2011
	[Guid("9d98b251-453e-44f6-9cec-8b5aed970129")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface IProcessHostIdleAndHealthCheck
	{
		// Token: 0x0600603F RID: 24639
		[return: MarshalAs(UnmanagedType.Bool)]
		bool IsIdle();

		// Token: 0x06006040 RID: 24640
		void Ping(IProcessPingCallback callback);
	}
}

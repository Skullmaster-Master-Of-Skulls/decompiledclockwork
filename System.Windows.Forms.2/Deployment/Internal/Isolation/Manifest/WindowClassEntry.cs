using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000AE RID: 174
	[StructLayout(LayoutKind.Sequential)]
	internal class WindowClassEntry
	{
		// Token: 0x040002DA RID: 730
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ClassName;

		// Token: 0x040002DB RID: 731
		[MarshalAs(UnmanagedType.LPWStr)]
		public string HostDll;

		// Token: 0x040002DC RID: 732
		public bool fVersioned;
	}
}

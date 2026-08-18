using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000A5 RID: 165
	[StructLayout(LayoutKind.Sequential)]
	internal class CLRSurrogateEntry
	{
		// Token: 0x040002B7 RID: 695
		public Guid Clsid;

		// Token: 0x040002B8 RID: 696
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RuntimeVersion;

		// Token: 0x040002B9 RID: 697
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ClassName;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000BA RID: 186
	[StructLayout(LayoutKind.Sequential)]
	internal class AssemblyRequestEntry
	{
		// Token: 0x040002F2 RID: 754
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x040002F3 RID: 755
		[MarshalAs(UnmanagedType.LPWStr)]
		public string permissionSetID;
	}
}

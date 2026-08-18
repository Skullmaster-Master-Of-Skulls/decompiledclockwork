using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000B7 RID: 183
	[StructLayout(LayoutKind.Sequential)]
	internal class PermissionSetEntry
	{
		// Token: 0x040002EE RID: 750
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Id;

		// Token: 0x040002EF RID: 751
		[MarshalAs(UnmanagedType.LPWStr)]
		public string XmlSegment;
	}
}

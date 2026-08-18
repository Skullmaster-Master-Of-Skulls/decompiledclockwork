using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000B1 RID: 177
	[StructLayout(LayoutKind.Sequential)]
	internal class ResourceTableMappingEntry
	{
		// Token: 0x040002E0 RID: 736
		[MarshalAs(UnmanagedType.LPWStr)]
		public string id;

		// Token: 0x040002E1 RID: 737
		[MarshalAs(UnmanagedType.LPWStr)]
		public string FinalStringMapped;
	}
}

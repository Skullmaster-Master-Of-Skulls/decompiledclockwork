using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000B4 RID: 180
	[StructLayout(LayoutKind.Sequential)]
	internal class EntryPointEntry
	{
		// Token: 0x040002E4 RID: 740
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x040002E5 RID: 741
		[MarshalAs(UnmanagedType.LPWStr)]
		public string CommandLine_File;

		// Token: 0x040002E6 RID: 742
		[MarshalAs(UnmanagedType.LPWStr)]
		public string CommandLine_Parameters;

		// Token: 0x040002E7 RID: 743
		public IReferenceIdentity Identity;

		// Token: 0x040002E8 RID: 744
		public uint Flags;
	}
}

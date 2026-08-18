using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200009F RID: 159
	[StructLayout(LayoutKind.Sequential)]
	internal class COMServerEntry
	{
		// Token: 0x040002A3 RID: 675
		public Guid Clsid;

		// Token: 0x040002A4 RID: 676
		public uint Flags;

		// Token: 0x040002A5 RID: 677
		public Guid ConfiguredGuid;

		// Token: 0x040002A6 RID: 678
		public Guid ImplementedClsid;

		// Token: 0x040002A7 RID: 679
		public Guid TypeLibrary;

		// Token: 0x040002A8 RID: 680
		public uint ThreadingModel;

		// Token: 0x040002A9 RID: 681
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RuntimeVersion;

		// Token: 0x040002AA RID: 682
		[MarshalAs(UnmanagedType.LPWStr)]
		public string HostFile;
	}
}

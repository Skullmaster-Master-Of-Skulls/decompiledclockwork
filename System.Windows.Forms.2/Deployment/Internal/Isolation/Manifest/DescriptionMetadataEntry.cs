using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000BD RID: 189
	[StructLayout(LayoutKind.Sequential)]
	internal class DescriptionMetadataEntry
	{
		// Token: 0x040002F6 RID: 758
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Publisher;

		// Token: 0x040002F7 RID: 759
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Product;

		// Token: 0x040002F8 RID: 760
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;

		// Token: 0x040002F9 RID: 761
		[MarshalAs(UnmanagedType.LPWStr)]
		public string IconFile;

		// Token: 0x040002FA RID: 762
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ErrorReportUrl;

		// Token: 0x040002FB RID: 763
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SuiteName;
	}
}

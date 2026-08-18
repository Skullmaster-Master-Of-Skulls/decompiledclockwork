using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000093 RID: 147
	[StructLayout(LayoutKind.Sequential)]
	internal class FileAssociationEntry
	{
		// Token: 0x0400028B RID: 651
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Extension;

		// Token: 0x0400028C RID: 652
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;

		// Token: 0x0400028D RID: 653
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ProgID;

		// Token: 0x0400028E RID: 654
		[MarshalAs(UnmanagedType.LPWStr)]
		public string DefaultIcon;

		// Token: 0x0400028F RID: 655
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Parameter;
	}
}

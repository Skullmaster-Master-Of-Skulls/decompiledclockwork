using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000BF RID: 191
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("CB73147E-5FC2-4c31-B4E6-58D13DBE1A08")]
	[ComImport]
	internal interface IDescriptionMetadataEntry
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002B4 RID: 692
		DescriptionMetadataEntry AllData { [SecurityCritical] get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002B5 RID: 693
		string Publisher { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002B6 RID: 694
		string Product { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002B7 RID: 695
		string SupportUrl { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002B8 RID: 696
		string IconFile { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002B9 RID: 697
		string ErrorReportUrl { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002BA RID: 698
		string SuiteName { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}

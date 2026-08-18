using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000095 RID: 149
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0C66F299-E08E-48c5-9264-7CCBEB4D5CBB")]
	[ComImport]
	internal interface IFileAssociationEntry
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000261 RID: 609
		FileAssociationEntry AllData { [SecurityCritical] get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000262 RID: 610
		string Extension { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000263 RID: 611
		string Description { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000264 RID: 612
		string ProgID { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000265 RID: 613
		string DefaultIcon { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000266 RID: 614
		string Parameter { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}

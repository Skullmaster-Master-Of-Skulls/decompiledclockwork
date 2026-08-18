using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000092 RID: 146
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("A2A55FAD-349B-469b-BF12-ADC33D14A937")]
	[ComImport]
	internal interface IFileEntry
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000251 RID: 593
		FileEntry AllData { [SecurityCritical] get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000252 RID: 594
		string Name { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000253 RID: 595
		uint HashAlgorithm { [SecurityCritical] get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000254 RID: 596
		string LoadFrom { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000255 RID: 597
		string SourcePath { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000256 RID: 598
		string ImportPath { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000257 RID: 599
		string SourceName { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000258 RID: 600
		string Location { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000259 RID: 601
		object HashValue { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600025A RID: 602
		ulong Size { [SecurityCritical] get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600025B RID: 603
		string Group { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600025C RID: 604
		uint Flags { [SecurityCritical] get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600025D RID: 605
		IMuiResourceMapEntry MuiMapping { [SecurityCritical] get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600025E RID: 606
		uint WritableType { [SecurityCritical] get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600025F RID: 607
		ISection HashElements { [SecurityCritical] get; }
	}
}

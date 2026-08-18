using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000A1 RID: 161
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("3903B11B-FBE8-477c-825F-DB828B5FD174")]
	[ComImport]
	internal interface ICOMServerEntry
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000275 RID: 629
		COMServerEntry AllData { [SecurityCritical] get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000276 RID: 630
		Guid Clsid { [SecurityCritical] get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000277 RID: 631
		uint Flags { [SecurityCritical] get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000278 RID: 632
		Guid ConfiguredGuid { [SecurityCritical] get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000279 RID: 633
		Guid ImplementedClsid { [SecurityCritical] get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600027A RID: 634
		Guid TypeLibrary { [SecurityCritical] get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600027B RID: 635
		uint ThreadingModel { [SecurityCritical] get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600027C RID: 636
		string RuntimeVersion { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600027D RID: 637
		string HostFile { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}

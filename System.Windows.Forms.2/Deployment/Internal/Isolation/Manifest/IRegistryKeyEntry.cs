using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000DA RID: 218
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("186685d1-6673-48c3-bc83-95859bb591df")]
	[ComImport]
	internal interface IRegistryKeyEntry
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000306 RID: 774
		RegistryKeyEntry AllData { [SecurityCritical] get; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000307 RID: 775
		uint Flags { [SecurityCritical] get; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000308 RID: 776
		uint Protection { [SecurityCritical] get; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000309 RID: 777
		string BuildFilter { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600030A RID: 778
		object SecurityDescriptor { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600030B RID: 779
		object Values { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600030C RID: 780
		object Keys { [SecurityCritical] [return: MarshalAs(UnmanagedType.Interface)] get; }
	}
}

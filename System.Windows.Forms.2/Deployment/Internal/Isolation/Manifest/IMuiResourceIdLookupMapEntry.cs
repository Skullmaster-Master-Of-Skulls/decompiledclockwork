using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000083 RID: 131
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("24abe1f7-a396-4a03-9adf-1d5b86a5569f")]
	[ComImport]
	internal interface IMuiResourceIdLookupMapEntry
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600022B RID: 555
		MuiResourceIdLookupMapEntry AllData { [SecurityCritical] get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600022C RID: 556
		uint Count { [SecurityCritical] get; }
	}
}

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000A7 RID: 167
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("1E0422A1-F0D2-44ae-914B-8A2DECCFD22B")]
	[ComImport]
	internal interface ICLRSurrogateEntry
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000283 RID: 643
		CLRSurrogateEntry AllData { [SecurityCritical] get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000284 RID: 644
		Guid Clsid { [SecurityCritical] get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000285 RID: 645
		string RuntimeVersion { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000286 RID: 646
		string ClassName { [SecurityCritical] [return: MarshalAs(UnmanagedType.LPWStr)] get; }
	}
}

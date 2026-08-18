using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x020007C0 RID: 1984
	[Guid("15eb8d20-d4ed-4855-a276-91a75a696955")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IISAPIRuntime2
	{
		// Token: 0x06005F15 RID: 24341
		void StartProcessing();

		// Token: 0x06005F16 RID: 24342
		void StopProcessing();

		// Token: 0x06005F17 RID: 24343
		[return: MarshalAs(UnmanagedType.I4)]
		int ProcessRequest([In] IntPtr ecb, [MarshalAs(UnmanagedType.I4)] [In] int useProcessModel);

		// Token: 0x06005F18 RID: 24344
		void DoGCCollect();
	}
}

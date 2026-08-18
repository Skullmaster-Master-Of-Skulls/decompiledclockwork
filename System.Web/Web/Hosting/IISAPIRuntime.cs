using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x0200029D RID: 669
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("08a2c56f-7c16-41c1-a8be-432917a1a2d1")]
	[ComImport]
	public interface IISAPIRuntime
	{
		// Token: 0x060022EF RID: 8943
		void StartProcessing();

		// Token: 0x060022F0 RID: 8944
		void StopProcessing();

		// Token: 0x060022F1 RID: 8945
		[return: MarshalAs(UnmanagedType.I4)]
		int ProcessRequest([In] IntPtr ecb, [MarshalAs(UnmanagedType.I4)] [In] int useProcessModel);

		// Token: 0x060022F2 RID: 8946
		void DoGCCollect();
	}
}

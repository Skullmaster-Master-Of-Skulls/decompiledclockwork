using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x02000296 RID: 662
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("c96cb854-aec2-4208-9ada-a86a96860cb6")]
	[ComImport]
	internal interface IPipelineRuntime
	{
		// Token: 0x060022B2 RID: 8882
		void StartProcessing();

		// Token: 0x060022B3 RID: 8883
		void StopProcessing();

		// Token: 0x060022B4 RID: 8884
		void InitializeApplication([In] IntPtr appContext);

		// Token: 0x060022B5 RID: 8885
		IntPtr GetExecuteDelegate();

		// Token: 0x060022B6 RID: 8886
		IntPtr GetDisposeDelegate();

		// Token: 0x060022B7 RID: 8887
		IntPtr GetRoleDelegate();
	}
}

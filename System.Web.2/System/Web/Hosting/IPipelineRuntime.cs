using System;
using System.Runtime.InteropServices;

namespace System.Web.Hosting
{
	// Token: 0x020007B9 RID: 1977
	[Guid("c96cb854-aec2-4208-9ada-a86a96860cb6")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface IPipelineRuntime
	{
		// Token: 0x06005ED7 RID: 24279
		void StartProcessing();

		// Token: 0x06005ED8 RID: 24280
		void StopProcessing();

		// Token: 0x06005ED9 RID: 24281
		void InitializeApplication([In] IntPtr appContext);

		// Token: 0x06005EDA RID: 24282
		IntPtr GetAsyncCompletionDelegate();

		// Token: 0x06005EDB RID: 24283
		IntPtr GetAsyncDisconnectNotificationDelegate();

		// Token: 0x06005EDC RID: 24284
		IntPtr GetExecuteDelegate();

		// Token: 0x06005EDD RID: 24285
		IntPtr GetDisposeDelegate();

		// Token: 0x06005EDE RID: 24286
		IntPtr GetRoleDelegate();

		// Token: 0x06005EDF RID: 24287
		IntPtr GetPrincipalDelegate();
	}
}

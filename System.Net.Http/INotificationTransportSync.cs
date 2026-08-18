using System;
using System.Runtime.InteropServices;

namespace System.Net.Http
{
	// Token: 0x02000022 RID: 34
	[ComVisible(true)]
	[Guid("79eb1402-0ab8-49c0-9e14-a1ae4ba93058")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface INotificationTransportSync
	{
		// Token: 0x06000193 RID: 403
		void CompleteDelivery();

		// Token: 0x06000194 RID: 404
		void Flush();
	}
}

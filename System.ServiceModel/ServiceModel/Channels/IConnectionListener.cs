using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007CE RID: 1998
	internal interface IConnectionListener : IDisposable
	{
		// Token: 0x06004B45 RID: 19269
		void Listen();

		// Token: 0x06004B46 RID: 19270
		IAsyncResult BeginAccept(AsyncCallback callback, object state);

		// Token: 0x06004B47 RID: 19271
		IConnection EndAccept(IAsyncResult result);
	}
}

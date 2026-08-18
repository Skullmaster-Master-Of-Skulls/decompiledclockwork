using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x020000F4 RID: 244
	public interface IDuplexContextChannel : IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000526 RID: 1318
		// (set) Token: 0x06000527 RID: 1319
		bool AutomaticInputSessionShutdown { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000528 RID: 1320
		// (set) Token: 0x06000529 RID: 1321
		InstanceContext CallbackInstance { get; set; }

		// Token: 0x0600052A RID: 1322
		IAsyncResult BeginCloseOutputSession(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x0600052B RID: 1323
		void EndCloseOutputSession(IAsyncResult result);

		// Token: 0x0600052C RID: 1324
		void CloseOutputSession(TimeSpan timeout);
	}
}

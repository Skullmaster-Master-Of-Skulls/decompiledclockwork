using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x020000FB RID: 251
	[__DynamicallyInvokable]
	public interface IClientChannel : IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000537 RID: 1335
		// (set) Token: 0x06000538 RID: 1336
		[__DynamicallyInvokable]
		bool AllowInitializationUI { [__DynamicallyInvokable] get; [__DynamicallyInvokable] set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000539 RID: 1337
		[__DynamicallyInvokable]
		bool DidInteractiveInitialization { [__DynamicallyInvokable] get; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600053A RID: 1338
		[__DynamicallyInvokable]
		Uri Via { [__DynamicallyInvokable] get; }

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600053B RID: 1339
		// (remove) Token: 0x0600053C RID: 1340
		[__DynamicallyInvokable]
		event EventHandler<UnknownMessageReceivedEventArgs> UnknownMessageReceived;

		// Token: 0x0600053D RID: 1341
		[__DynamicallyInvokable]
		void DisplayInitializationUI();

		// Token: 0x0600053E RID: 1342
		[__DynamicallyInvokable]
		IAsyncResult BeginDisplayInitializationUI(AsyncCallback callback, object state);

		// Token: 0x0600053F RID: 1343
		[__DynamicallyInvokable]
		void EndDisplayInitializationUI(IAsyncResult result);
	}
}

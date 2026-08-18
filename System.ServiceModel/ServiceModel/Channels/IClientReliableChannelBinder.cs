using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000911 RID: 2321
	internal interface IClientReliableChannelBinder : IReliableChannelBinder
	{
		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x0600589C RID: 22684
		Uri Via { get; }

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x0600589D RID: 22685
		// (remove) Token: 0x0600589E RID: 22686
		event EventHandler ConnectionLost;

		// Token: 0x0600589F RID: 22687
		bool EnsureChannelForRequest();

		// Token: 0x060058A0 RID: 22688
		IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060058A1 RID: 22689
		IAsyncResult BeginRequest(Message message, TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state);

		// Token: 0x060058A2 RID: 22690
		Message EndRequest(IAsyncResult result);

		// Token: 0x060058A3 RID: 22691
		Message Request(Message message, TimeSpan timeout);

		// Token: 0x060058A4 RID: 22692
		Message Request(Message message, TimeSpan timeout, MaskingMode maskingMode);
	}
}

using System;
using System.ComponentModel;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace NLog.LogReceiverService
{
	// Token: 0x0200012F RID: 303
	public interface IWcfLogReceiverClient : ICommunicationObject
	{
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000A76 RID: 2678
		// (remove) Token: 0x06000A77 RID: 2679
		event EventHandler<AsyncCompletedEventArgs> ProcessLogMessagesCompleted;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000A78 RID: 2680
		// (remove) Token: 0x06000A79 RID: 2681
		event EventHandler<AsyncCompletedEventArgs> OpenCompleted;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000A7A RID: 2682
		// (remove) Token: 0x06000A7B RID: 2683
		event EventHandler<AsyncCompletedEventArgs> CloseCompleted;

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000A7C RID: 2684
		ClientCredentials ClientCredentials { get; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000A7D RID: 2685
		IClientChannel InnerChannel { get; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000A7E RID: 2686
		ServiceEndpoint Endpoint { get; }

		// Token: 0x06000A7F RID: 2687
		void OpenAsync();

		// Token: 0x06000A80 RID: 2688
		void OpenAsync(object userState);

		// Token: 0x06000A81 RID: 2689
		void CloseAsync();

		// Token: 0x06000A82 RID: 2690
		void CloseAsync(object userState);

		// Token: 0x06000A83 RID: 2691
		void ProcessLogMessagesAsync(NLogEvents events);

		// Token: 0x06000A84 RID: 2692
		void ProcessLogMessagesAsync(NLogEvents events, object userState);

		// Token: 0x06000A85 RID: 2693
		IAsyncResult BeginProcessLogMessages(NLogEvents events, AsyncCallback callback, object asyncState);

		// Token: 0x06000A86 RID: 2694
		void EndProcessLogMessages(IAsyncResult result);

		// Token: 0x06000A87 RID: 2695
		void DisplayInitializationUI();

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000A88 RID: 2696
		// (set) Token: 0x06000A89 RID: 2697
		CookieContainer CookieContainer { get; set; }
	}
}

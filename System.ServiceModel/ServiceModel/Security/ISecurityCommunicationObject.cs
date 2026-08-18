using System;

namespace System.ServiceModel.Security
{
	// Token: 0x02000319 RID: 793
	internal interface ISecurityCommunicationObject
	{
		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001B66 RID: 7014
		TimeSpan DefaultOpenTimeout { get; }

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001B67 RID: 7015
		TimeSpan DefaultCloseTimeout { get; }

		// Token: 0x06001B68 RID: 7016
		void OnAbort();

		// Token: 0x06001B69 RID: 7017
		IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06001B6A RID: 7018
		IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06001B6B RID: 7019
		void OnClose(TimeSpan timeout);

		// Token: 0x06001B6C RID: 7020
		void OnClosed();

		// Token: 0x06001B6D RID: 7021
		void OnClosing();

		// Token: 0x06001B6E RID: 7022
		void OnEndClose(IAsyncResult result);

		// Token: 0x06001B6F RID: 7023
		void OnEndOpen(IAsyncResult result);

		// Token: 0x06001B70 RID: 7024
		void OnFaulted();

		// Token: 0x06001B71 RID: 7025
		void OnOpen(TimeSpan timeout);

		// Token: 0x06001B72 RID: 7026
		void OnOpened();

		// Token: 0x06001B73 RID: 7027
		void OnOpening();
	}
}

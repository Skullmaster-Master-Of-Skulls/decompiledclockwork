using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000769 RID: 1897
	internal interface IAsyncRequest : IAsyncResult, IRequestBase
	{
		// Token: 0x0600486D RID: 18541
		void BeginSendRequest(Message message, TimeSpan timeout);

		// Token: 0x0600486E RID: 18542
		Message End();
	}
}

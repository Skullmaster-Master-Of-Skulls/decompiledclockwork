using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000737 RID: 1847
	internal interface IChannelDemuxFailureHandler
	{
		// Token: 0x0600463B RID: 17979
		void HandleDemuxFailure(Message message);

		// Token: 0x0600463C RID: 17980
		IAsyncResult BeginHandleDemuxFailure(Message message, RequestContext faultContext, AsyncCallback callback, object state);

		// Token: 0x0600463D RID: 17981
		IAsyncResult BeginHandleDemuxFailure(Message message, IOutputChannel faultContext, AsyncCallback callback, object state);

		// Token: 0x0600463E RID: 17982
		void EndHandleDemuxFailure(IAsyncResult result);
	}
}

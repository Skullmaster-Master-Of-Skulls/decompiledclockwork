using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000768 RID: 1896
	internal interface IRequest : IRequestBase
	{
		// Token: 0x0600486B RID: 18539
		void SendRequest(Message message, TimeSpan timeout);

		// Token: 0x0600486C RID: 18540
		Message WaitForReply(TimeSpan timeout);
	}
}

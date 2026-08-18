using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200093B RID: 2363
	internal class SendFaultHelper : TypedFaultHelper<Message>
	{
		// Token: 0x06005AD2 RID: 23250 RVA: 0x0014D915 File Offset: 0x0014BB15
		public SendFaultHelper(TimeSpan defaultSendTimeout, TimeSpan defaultCloseTimeout) : base(defaultSendTimeout, defaultCloseTimeout)
		{
		}

		// Token: 0x06005AD3 RID: 23251 RVA: 0x0014D91F File Offset: 0x0014BB1F
		protected override void AbortState(Message message, bool isOnAbortThread)
		{
			if (!isOnAbortThread)
			{
				message.Close();
			}
		}

		// Token: 0x06005AD4 RID: 23252 RVA: 0x0014D92A File Offset: 0x0014BB2A
		protected override IAsyncResult BeginSendFault(IReliableChannelBinder binder, Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return binder.BeginSend(message, timeout, callback, state);
		}

		// Token: 0x06005AD5 RID: 23253 RVA: 0x0014D938 File Offset: 0x0014BB38
		protected override void EndSendFault(IReliableChannelBinder binder, Message message, IAsyncResult result)
		{
			binder.EndSend(result);
			message.Close();
		}

		// Token: 0x06005AD6 RID: 23254 RVA: 0x0014D947 File Offset: 0x0014BB47
		protected override Message GetState(RequestContext requestContext, Message faultMessage)
		{
			return faultMessage;
		}
	}
}

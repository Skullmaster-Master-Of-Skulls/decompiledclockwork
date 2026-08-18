using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200093A RID: 2362
	internal class ReplyFaultHelper : TypedFaultHelper<FaultState>
	{
		// Token: 0x06005ACD RID: 23245 RVA: 0x0014D8B0 File Offset: 0x0014BAB0
		public ReplyFaultHelper(TimeSpan defaultSendTimeout, TimeSpan defaultCloseTimeout) : base(defaultSendTimeout, defaultCloseTimeout)
		{
		}

		// Token: 0x06005ACE RID: 23246 RVA: 0x0014D8BA File Offset: 0x0014BABA
		protected override void AbortState(FaultState faultState, bool isOnAbortThread)
		{
			if (!isOnAbortThread)
			{
				faultState.FaultMessage.Close();
			}
			faultState.RequestContext.Abort();
		}

		// Token: 0x06005ACF RID: 23247 RVA: 0x0014D8D7 File Offset: 0x0014BAD7
		protected override IAsyncResult BeginSendFault(IReliableChannelBinder binder, FaultState faultState, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return faultState.RequestContext.BeginReply(faultState.FaultMessage, timeout, callback, state);
		}

		// Token: 0x06005AD0 RID: 23248 RVA: 0x0014D8F1 File Offset: 0x0014BAF1
		protected override void EndSendFault(IReliableChannelBinder binder, FaultState faultState, IAsyncResult result)
		{
			faultState.RequestContext.EndReply(result);
			faultState.FaultMessage.Close();
		}

		// Token: 0x06005AD1 RID: 23249 RVA: 0x0014D90C File Offset: 0x0014BB0C
		protected override FaultState GetState(RequestContext requestContext, Message faultMessage)
		{
			return new FaultState(requestContext, faultMessage);
		}
	}
}

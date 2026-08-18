using System;
using System.Runtime;
using System.ServiceModel.Description;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A66 RID: 2662
	internal class TransactionRequestChannelGeneric<TChannel> : TransactionChannel<TChannel>, IRequestChannel, IChannel, ICommunicationObject where TChannel : class, IRequestChannel
	{
		// Token: 0x06006918 RID: 26904 RVA: 0x00188AA5 File Offset: 0x00186CA5
		public TransactionRequestChannelGeneric(ChannelManagerBase channelManager, TChannel innerChannel) : base(channelManager, innerChannel)
		{
		}

		// Token: 0x17001918 RID: 6424
		// (get) Token: 0x06006919 RID: 26905 RVA: 0x00188AAF File Offset: 0x00186CAF
		public EndpointAddress RemoteAddress
		{
			get
			{
				return base.InnerChannel.RemoteAddress;
			}
		}

		// Token: 0x17001919 RID: 6425
		// (get) Token: 0x0600691A RID: 26906 RVA: 0x00188AC1 File Offset: 0x00186CC1
		public Uri Via
		{
			get
			{
				return base.InnerChannel.Via;
			}
		}

		// Token: 0x0600691B RID: 26907 RVA: 0x00188AD3 File Offset: 0x00186CD3
		public IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state)
		{
			return this.BeginRequest(message, base.DefaultSendTimeout, callback, state);
		}

		// Token: 0x0600691C RID: 26908 RVA: 0x00188AE4 File Offset: 0x00186CE4
		public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback asyncCallback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.WriteTransactionDataToMessage(message, MessageDirection.Input);
			return base.InnerChannel.BeginRequest(message, timeoutHelper.RemainingTime(), asyncCallback, state);
		}

		// Token: 0x0600691D RID: 26909 RVA: 0x00188B1C File Offset: 0x00186D1C
		public Message EndRequest(IAsyncResult result)
		{
			Message message = base.InnerChannel.EndRequest(result);
			if (message != null)
			{
				base.ReadIssuedTokens(message, MessageDirection.Output);
			}
			return message;
		}

		// Token: 0x0600691E RID: 26910 RVA: 0x00188B47 File Offset: 0x00186D47
		public Message Request(Message message)
		{
			return this.Request(message, base.DefaultSendTimeout);
		}

		// Token: 0x0600691F RID: 26911 RVA: 0x00188B58 File Offset: 0x00186D58
		public Message Request(Message message, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.WriteTransactionDataToMessage(message, MessageDirection.Input);
			Message message2 = base.InnerChannel.Request(message, timeoutHelper.RemainingTime());
			if (message2 != null)
			{
				base.ReadIssuedTokens(message2, MessageDirection.Output);
			}
			return message2;
		}
	}
}

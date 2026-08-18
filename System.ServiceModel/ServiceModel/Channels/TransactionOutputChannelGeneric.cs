using System;
using System.Runtime;
using System.ServiceModel.Description;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A65 RID: 2661
	internal class TransactionOutputChannelGeneric<TChannel> : TransactionChannel<TChannel>, IOutputChannel, IChannel, ICommunicationObject where TChannel : class, IOutputChannel
	{
		// Token: 0x06006910 RID: 26896 RVA: 0x001889D4 File Offset: 0x00186BD4
		public TransactionOutputChannelGeneric(ChannelManagerBase channelManager, TChannel innerChannel) : base(channelManager, innerChannel)
		{
		}

		// Token: 0x17001916 RID: 6422
		// (get) Token: 0x06006911 RID: 26897 RVA: 0x001889DE File Offset: 0x00186BDE
		public EndpointAddress RemoteAddress
		{
			get
			{
				return base.InnerChannel.RemoteAddress;
			}
		}

		// Token: 0x17001917 RID: 6423
		// (get) Token: 0x06006912 RID: 26898 RVA: 0x001889F0 File Offset: 0x00186BF0
		public Uri Via
		{
			get
			{
				return base.InnerChannel.Via;
			}
		}

		// Token: 0x06006913 RID: 26899 RVA: 0x00188A02 File Offset: 0x00186C02
		public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
		{
			return this.BeginSend(message, base.DefaultSendTimeout, callback, state);
		}

		// Token: 0x06006914 RID: 26900 RVA: 0x00188A14 File Offset: 0x00186C14
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback asyncCallback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.WriteTransactionDataToMessage(message, MessageDirection.Input);
			return base.InnerChannel.BeginSend(message, timeoutHelper.RemainingTime(), asyncCallback, state);
		}

		// Token: 0x06006915 RID: 26901 RVA: 0x00188A4C File Offset: 0x00186C4C
		public void EndSend(IAsyncResult result)
		{
			base.InnerChannel.EndSend(result);
		}

		// Token: 0x06006916 RID: 26902 RVA: 0x00188A5F File Offset: 0x00186C5F
		public void Send(Message message)
		{
			this.Send(message, base.DefaultSendTimeout);
		}

		// Token: 0x06006917 RID: 26903 RVA: 0x00188A70 File Offset: 0x00186C70
		public void Send(Message message, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.WriteTransactionDataToMessage(message, MessageDirection.Input);
			base.InnerChannel.Send(message, timeoutHelper.RemainingTime());
		}
	}
}

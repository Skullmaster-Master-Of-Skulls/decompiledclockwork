using System;
using System.Runtime;
using System.ServiceModel.Description;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A6D RID: 2669
	internal class TransactionDuplexChannelGeneric<TChannel> : TransactionReceiveChannelGeneric<TChannel>, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel where TChannel : class, IDuplexChannel
	{
		// Token: 0x0600694C RID: 26956 RVA: 0x0018910C File Offset: 0x0018730C
		public TransactionDuplexChannelGeneric(ChannelManagerBase channelManager, TChannel innerChannel, MessageDirection direction) : base(channelManager, innerChannel, direction)
		{
			if (direction == MessageDirection.Input)
			{
				this.sendMessageDirection = MessageDirection.Output;
				return;
			}
			this.sendMessageDirection = MessageDirection.Input;
		}

		// Token: 0x17001920 RID: 6432
		// (get) Token: 0x0600694D RID: 26957 RVA: 0x00189129 File Offset: 0x00187329
		public EndpointAddress RemoteAddress
		{
			get
			{
				return base.InnerChannel.RemoteAddress;
			}
		}

		// Token: 0x17001921 RID: 6433
		// (get) Token: 0x0600694E RID: 26958 RVA: 0x0018913B File Offset: 0x0018733B
		public Uri Via
		{
			get
			{
				return base.InnerChannel.Via;
			}
		}

		// Token: 0x0600694F RID: 26959 RVA: 0x00189150 File Offset: 0x00187350
		public override void ReadTransactionDataFromMessage(Message message, MessageDirection direction)
		{
			try
			{
				base.ReadTransactionDataFromMessage(message, direction);
			}
			catch (FaultException ex)
			{
				Message message2 = Message.CreateMessage(message.Version, ex.CreateMessageFault(), ex.Action);
				RequestReplyCorrelator.AddressReply(message2, message);
				RequestReplyCorrelator.PrepareReply(message2, message.Headers.MessageId);
				try
				{
					this.Send(message2);
				}
				finally
				{
					message2.Close();
				}
				throw;
			}
		}

		// Token: 0x06006950 RID: 26960 RVA: 0x001891C8 File Offset: 0x001873C8
		public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
		{
			return this.BeginSend(message, base.DefaultSendTimeout, callback, state);
		}

		// Token: 0x06006951 RID: 26961 RVA: 0x001891DC File Offset: 0x001873DC
		public virtual IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback asyncCallback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.WriteTransactionDataToMessage(message, this.sendMessageDirection);
			return base.InnerChannel.BeginSend(message, timeoutHelper.RemainingTime(), asyncCallback, state);
		}

		// Token: 0x06006952 RID: 26962 RVA: 0x00189219 File Offset: 0x00187419
		public void EndSend(IAsyncResult result)
		{
			base.InnerChannel.EndSend(result);
		}

		// Token: 0x06006953 RID: 26963 RVA: 0x0018922C File Offset: 0x0018742C
		public void Send(Message message)
		{
			this.Send(message, base.DefaultSendTimeout);
		}

		// Token: 0x06006954 RID: 26964 RVA: 0x0018923C File Offset: 0x0018743C
		public virtual void Send(Message message, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.WriteTransactionDataToMessage(message, this.sendMessageDirection);
			base.InnerChannel.Send(message, timeoutHelper.RemainingTime());
		}

		// Token: 0x04003C30 RID: 15408
		private MessageDirection sendMessageDirection;
	}
}

using System;
using System.ServiceModel.Description;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A6A RID: 2666
	internal class TransactionReceiveChannelGeneric<TChannel> : TransactionChannel<TChannel>, IInputChannel, IChannel, ICommunicationObject where TChannel : class, IInputChannel
	{
		// Token: 0x06006930 RID: 26928 RVA: 0x00188DCC File Offset: 0x00186FCC
		public TransactionReceiveChannelGeneric(ChannelManagerBase channelManager, TChannel innerChannel, MessageDirection direction) : base(channelManager, innerChannel)
		{
			this.receiveMessageDirection = direction;
		}

		// Token: 0x1700191E RID: 6430
		// (get) Token: 0x06006931 RID: 26929 RVA: 0x00188DDD File Offset: 0x00186FDD
		public EndpointAddress LocalAddress
		{
			get
			{
				return base.InnerChannel.LocalAddress;
			}
		}

		// Token: 0x06006932 RID: 26930 RVA: 0x00188DEF File Offset: 0x00186FEF
		public Message Receive()
		{
			return this.Receive(base.DefaultReceiveTimeout);
		}

		// Token: 0x06006933 RID: 26931 RVA: 0x00188DFD File Offset: 0x00186FFD
		public Message Receive(TimeSpan timeout)
		{
			return InputChannel.HelpReceive(this, timeout);
		}

		// Token: 0x06006934 RID: 26932 RVA: 0x00188E06 File Offset: 0x00187006
		public IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			return this.BeginReceive(base.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x06006935 RID: 26933 RVA: 0x00188E16 File Offset: 0x00187016
		public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return InputChannel.HelpBeginReceive(this, timeout, callback, state);
		}

		// Token: 0x06006936 RID: 26934 RVA: 0x00188E21 File Offset: 0x00187021
		public Message EndReceive(IAsyncResult result)
		{
			return InputChannel.HelpEndReceive(result);
		}

		// Token: 0x06006937 RID: 26935 RVA: 0x00188E29 File Offset: 0x00187029
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginTryReceive(timeout, callback, state);
		}

		// Token: 0x06006938 RID: 26936 RVA: 0x00188E3E File Offset: 0x0018703E
		public virtual bool EndTryReceive(IAsyncResult asyncResult, out Message message)
		{
			if (!base.InnerChannel.EndTryReceive(asyncResult, out message))
			{
				return false;
			}
			if (message != null)
			{
				this.ReadTransactionDataFromMessage(message, this.receiveMessageDirection);
			}
			return true;
		}

		// Token: 0x06006939 RID: 26937 RVA: 0x00188E69 File Offset: 0x00187069
		public virtual bool TryReceive(TimeSpan timeout, out Message message)
		{
			if (!base.InnerChannel.TryReceive(timeout, out message))
			{
				return false;
			}
			if (message != null)
			{
				this.ReadTransactionDataFromMessage(message, this.receiveMessageDirection);
			}
			return true;
		}

		// Token: 0x0600693A RID: 26938 RVA: 0x00188E94 File Offset: 0x00187094
		public bool WaitForMessage(TimeSpan timeout)
		{
			return base.InnerChannel.WaitForMessage(timeout);
		}

		// Token: 0x0600693B RID: 26939 RVA: 0x00188EA7 File Offset: 0x001870A7
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginWaitForMessage(timeout, callback, state);
		}

		// Token: 0x0600693C RID: 26940 RVA: 0x00188EBC File Offset: 0x001870BC
		public bool EndWaitForMessage(IAsyncResult result)
		{
			return base.InnerChannel.EndWaitForMessage(result);
		}

		// Token: 0x04003C2F RID: 15407
		private MessageDirection receiveMessageDirection;
	}
}

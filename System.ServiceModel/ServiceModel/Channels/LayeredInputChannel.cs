using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200074F RID: 1871
	internal class LayeredInputChannel : LayeredChannel<IInputChannel>, IInputChannel, IChannel, ICommunicationObject
	{
		// Token: 0x06004777 RID: 18295 RVA: 0x001095BD File Offset: 0x001077BD
		public LayeredInputChannel(ChannelManagerBase channelManager, IInputChannel innerChannel) : base(channelManager, innerChannel)
		{
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x06004778 RID: 18296 RVA: 0x001095C7 File Offset: 0x001077C7
		public virtual EndpointAddress LocalAddress
		{
			get
			{
				return base.InnerChannel.LocalAddress;
			}
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x001095D4 File Offset: 0x001077D4
		private void InternalOnReceive(Message message)
		{
			if (message != null)
			{
				this.OnReceive(message);
			}
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x001095E0 File Offset: 0x001077E0
		protected virtual void OnReceive(Message message)
		{
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x001095E4 File Offset: 0x001077E4
		public Message Receive()
		{
			Message message = base.InnerChannel.Receive();
			this.InternalOnReceive(message);
			return message;
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x00109608 File Offset: 0x00107808
		public Message Receive(TimeSpan timeout)
		{
			Message message = base.InnerChannel.Receive(timeout);
			this.InternalOnReceive(message);
			return message;
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x0010962A File Offset: 0x0010782A
		public IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginReceive(callback, state);
		}

		// Token: 0x0600477E RID: 18302 RVA: 0x00109639 File Offset: 0x00107839
		public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginReceive(timeout, callback, state);
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x0010964C File Offset: 0x0010784C
		public Message EndReceive(IAsyncResult result)
		{
			Message message = base.InnerChannel.EndReceive(result);
			this.InternalOnReceive(message);
			return message;
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x0010966E File Offset: 0x0010786E
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginTryReceive(timeout, callback, state);
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x00109680 File Offset: 0x00107880
		public bool EndTryReceive(IAsyncResult result, out Message message)
		{
			bool result2 = base.InnerChannel.EndTryReceive(result, out message);
			this.InternalOnReceive(message);
			return result2;
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x001096A4 File Offset: 0x001078A4
		public bool TryReceive(TimeSpan timeout, out Message message)
		{
			bool result = base.InnerChannel.TryReceive(timeout, out message);
			this.InternalOnReceive(message);
			return result;
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x001096C8 File Offset: 0x001078C8
		public bool WaitForMessage(TimeSpan timeout)
		{
			return base.InnerChannel.WaitForMessage(timeout);
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x001096D6 File Offset: 0x001078D6
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginWaitForMessage(timeout, callback, state);
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x001096E6 File Offset: 0x001078E6
		public bool EndWaitForMessage(IAsyncResult result)
		{
			return base.InnerChannel.EndWaitForMessage(result);
		}
	}
}

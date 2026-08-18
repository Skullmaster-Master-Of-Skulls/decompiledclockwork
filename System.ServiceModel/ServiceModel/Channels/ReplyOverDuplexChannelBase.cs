using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000760 RID: 1888
	internal abstract class ReplyOverDuplexChannelBase<TInnerChannel> : LayeredChannel<TInnerChannel>, IReplyChannel, IChannel, ICommunicationObject where TInnerChannel : class, IDuplexChannel
	{
		// Token: 0x06004822 RID: 18466 RVA: 0x0010B19F File Offset: 0x0010939F
		public ReplyOverDuplexChannelBase(ChannelManagerBase channelManager, TInnerChannel innerChannel) : base(channelManager, innerChannel)
		{
		}

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06004823 RID: 18467 RVA: 0x0010B1A9 File Offset: 0x001093A9
		public EndpointAddress LocalAddress
		{
			get
			{
				return base.InnerChannel.LocalAddress;
			}
		}

		// Token: 0x06004824 RID: 18468 RVA: 0x0010B1BB File Offset: 0x001093BB
		public RequestContext ReceiveRequest()
		{
			return this.ReceiveRequest(base.DefaultReceiveTimeout);
		}

		// Token: 0x06004825 RID: 18469 RVA: 0x0010B1C9 File Offset: 0x001093C9
		public RequestContext ReceiveRequest(TimeSpan timeout)
		{
			return ReplyChannel.HelpReceiveRequest(this, timeout);
		}

		// Token: 0x06004826 RID: 18470 RVA: 0x0010B1D2 File Offset: 0x001093D2
		public IAsyncResult BeginReceiveRequest(AsyncCallback callback, object state)
		{
			return this.BeginReceiveRequest(base.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x06004827 RID: 18471 RVA: 0x0010B1E2 File Offset: 0x001093E2
		public IAsyncResult BeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return ReplyChannel.HelpBeginReceiveRequest(this, timeout, callback, state);
		}

		// Token: 0x06004828 RID: 18472 RVA: 0x0010B1ED File Offset: 0x001093ED
		public RequestContext EndReceiveRequest(IAsyncResult result)
		{
			return ReplyChannel.HelpEndReceiveRequest(result);
		}

		// Token: 0x06004829 RID: 18473 RVA: 0x0010B1F8 File Offset: 0x001093F8
		public bool TryReceiveRequest(TimeSpan timeout, out RequestContext context)
		{
			Message message;
			if (!base.InnerChannel.TryReceive(timeout, out message))
			{
				context = null;
				return false;
			}
			context = this.WrapInnerMessage(message);
			return true;
		}

		// Token: 0x0600482A RID: 18474 RVA: 0x0010B229 File Offset: 0x00109429
		public IAsyncResult BeginTryReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginTryReceive(timeout, callback, state);
		}

		// Token: 0x0600482B RID: 18475 RVA: 0x0010B240 File Offset: 0x00109440
		public bool EndTryReceiveRequest(IAsyncResult result, out RequestContext context)
		{
			Message message;
			if (!base.InnerChannel.EndTryReceive(result, out message))
			{
				context = null;
				return false;
			}
			context = this.WrapInnerMessage(message);
			return true;
		}

		// Token: 0x0600482C RID: 18476 RVA: 0x0010B271 File Offset: 0x00109471
		public bool WaitForRequest(TimeSpan timeout)
		{
			return base.InnerChannel.WaitForMessage(timeout);
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x0010B284 File Offset: 0x00109484
		public IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginWaitForMessage(timeout, callback, state);
		}

		// Token: 0x0600482E RID: 18478 RVA: 0x0010B299 File Offset: 0x00109499
		public bool EndWaitForRequest(IAsyncResult result)
		{
			return base.InnerChannel.EndWaitForMessage(result);
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x0010B2AC File Offset: 0x001094AC
		private RequestContext WrapInnerMessage(Message message)
		{
			if (message == null)
			{
				return null;
			}
			return new ReplyOverDuplexChannelBase<TInnerChannel>.DuplexRequestContext(message, base.Manager, base.InnerChannel);
		}

		// Token: 0x02000CE0 RID: 3296
		private class DuplexRequestContext : RequestContext
		{
			// Token: 0x06007A0E RID: 31246 RVA: 0x001C7253 File Offset: 0x001C5453
			public DuplexRequestContext(Message request, IDefaultCommunicationTimeouts defaultTimeouts, IDuplexChannel innerChannel)
			{
				this.request = request;
				this.defaultTimeouts = defaultTimeouts;
				this.innerChannel = innerChannel;
				if (request != null)
				{
					this.replyTo = request.Headers.ReplyTo;
				}
				this.thisLock = new object();
			}

			// Token: 0x17001B9F RID: 7071
			// (get) Token: 0x06007A0F RID: 31247 RVA: 0x001C728F File Offset: 0x001C548F
			public override Message RequestMessage
			{
				get
				{
					return this.request;
				}
			}

			// Token: 0x06007A10 RID: 31248 RVA: 0x001C7297 File Offset: 0x001C5497
			public override void Abort()
			{
				this.Dispose(true);
			}

			// Token: 0x06007A11 RID: 31249 RVA: 0x001C72A0 File Offset: 0x001C54A0
			public override void Close()
			{
				this.Close(this.defaultTimeouts.CloseTimeout);
			}

			// Token: 0x06007A12 RID: 31250 RVA: 0x001C72B3 File Offset: 0x001C54B3
			public override void Close(TimeSpan timeout)
			{
				this.Dispose(true);
			}

			// Token: 0x06007A13 RID: 31251 RVA: 0x001C72BC File Offset: 0x001C54BC
			public override void Reply(Message message)
			{
				this.Reply(message, this.defaultTimeouts.SendTimeout);
			}

			// Token: 0x06007A14 RID: 31252 RVA: 0x001C72D0 File Offset: 0x001C54D0
			public override void Reply(Message message, TimeSpan timeout)
			{
				this.PrepareReply(message);
				this.innerChannel.Send(message);
			}

			// Token: 0x06007A15 RID: 31253 RVA: 0x001C72E5 File Offset: 0x001C54E5
			public override IAsyncResult BeginReply(Message message, AsyncCallback callback, object state)
			{
				return this.BeginReply(message, this.defaultTimeouts.SendTimeout, callback, state);
			}

			// Token: 0x06007A16 RID: 31254 RVA: 0x001C72FB File Offset: 0x001C54FB
			public override IAsyncResult BeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				this.PrepareReply(message);
				return this.innerChannel.BeginSend(message, timeout, callback, state);
			}

			// Token: 0x06007A17 RID: 31255 RVA: 0x001C7314 File Offset: 0x001C5514
			public override void EndReply(IAsyncResult result)
			{
				this.innerChannel.EndSend(result);
			}

			// Token: 0x06007A18 RID: 31256 RVA: 0x001C7322 File Offset: 0x001C5522
			private void PrepareReply(Message message)
			{
				if (this.replyTo != null)
				{
					this.replyTo.ApplyTo(message);
				}
			}

			// Token: 0x06007A19 RID: 31257 RVA: 0x001C7340 File Offset: 0x001C5540
			protected override void Dispose(bool disposing)
			{
				bool flag = false;
				object obj = this.thisLock;
				lock (obj)
				{
					if (!this.disposed)
					{
						this.disposed = true;
						flag = true;
					}
				}
				if (flag && this.request != null)
				{
					this.request.Close();
				}
			}

			// Token: 0x040045D1 RID: 17873
			private IDuplexChannel innerChannel;

			// Token: 0x040045D2 RID: 17874
			private IDefaultCommunicationTimeouts defaultTimeouts;

			// Token: 0x040045D3 RID: 17875
			private Message request;

			// Token: 0x040045D4 RID: 17876
			private EndpointAddress replyTo;

			// Token: 0x040045D5 RID: 17877
			private bool disposed;

			// Token: 0x040045D6 RID: 17878
			private object thisLock;
		}
	}
}

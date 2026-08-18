using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007A2 RID: 1954
	internal class TransportReplyChannelAcceptor : ReplyChannelAcceptor
	{
		// Token: 0x060049E5 RID: 18917 RVA: 0x0010F4E4 File Offset: 0x0010D6E4
		public TransportReplyChannelAcceptor(TransportChannelListener listener) : base(listener)
		{
			this.listener = listener;
		}

		// Token: 0x060049E6 RID: 18918 RVA: 0x0010F4F4 File Offset: 0x0010D6F4
		protected override ReplyChannel OnCreateChannel()
		{
			return new TransportReplyChannelAcceptor.TransportReplyChannel(base.ChannelManager, null);
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x0010F502 File Offset: 0x0010D702
		protected override void OnOpening()
		{
			base.OnOpening();
			this.transportManagerContainer = this.listener.GetTransportManagers();
			this.listener = null;
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x0010F522 File Offset: 0x0010D722
		protected override void OnAbort()
		{
			base.OnAbort();
			if (this.transportManagerContainer != null && !this.TransferTransportManagers())
			{
				this.transportManagerContainer.Abort();
			}
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x0010F545 File Offset: 0x0010D745
		private IAsyncResult DummyBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060049EA RID: 18922 RVA: 0x0010F54E File Offset: 0x0010D74E
		private void DummyEndClose(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x060049EB RID: 18923 RVA: 0x0010F558 File Offset: 0x0010D758
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			ChainedBeginHandler begin = new ChainedBeginHandler(this.DummyBeginClose);
			ChainedEndHandler end = new ChainedEndHandler(this.DummyEndClose);
			if (this.transportManagerContainer != null && !this.TransferTransportManagers())
			{
				begin = new ChainedBeginHandler(this.transportManagerContainer.BeginClose);
				end = new ChainedEndHandler(this.transportManagerContainer.EndClose);
			}
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), begin, end);
		}

		// Token: 0x060049EC RID: 18924 RVA: 0x0010F5D5 File Offset: 0x0010D7D5
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x060049ED RID: 18925 RVA: 0x0010F5E0 File Offset: 0x0010D7E0
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnClose(timeoutHelper.RemainingTime());
			if (this.transportManagerContainer != null && !this.TransferTransportManagers())
			{
				this.transportManagerContainer.Close(timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x060049EE RID: 18926 RVA: 0x0010F624 File Offset: 0x0010D824
		private bool TransferTransportManagers()
		{
			TransportReplyChannelAcceptor.TransportReplyChannel transportReplyChannel = (TransportReplyChannelAcceptor.TransportReplyChannel)base.GetCurrentChannel();
			return transportReplyChannel != null && transportReplyChannel.TransferTransportManagers(this.transportManagerContainer);
		}

		// Token: 0x04002EDF RID: 11999
		private TransportManagerContainer transportManagerContainer;

		// Token: 0x04002EE0 RID: 12000
		private TransportChannelListener listener;

		// Token: 0x02000CF3 RID: 3315
		protected class TransportReplyChannel : ReplyChannel
		{
			// Token: 0x06007A7D RID: 31357 RVA: 0x001C8318 File Offset: 0x001C6518
			public TransportReplyChannel(ChannelManagerBase channelManager, EndpointAddress localAddress) : base(channelManager, localAddress)
			{
			}

			// Token: 0x06007A7E RID: 31358 RVA: 0x001C8324 File Offset: 0x001C6524
			public bool TransferTransportManagers(TransportManagerContainer transportManagerContainer)
			{
				object thisLock = base.ThisLock;
				bool result;
				lock (thisLock)
				{
					if (base.State != CommunicationState.Opened)
					{
						result = false;
					}
					else
					{
						this.transportManagerContainer = transportManagerContainer;
						result = true;
					}
				}
				return result;
			}

			// Token: 0x06007A7F RID: 31359 RVA: 0x001C8378 File Offset: 0x001C6578
			protected override void OnAbort()
			{
				if (this.transportManagerContainer != null)
				{
					this.transportManagerContainer.Abort();
				}
				base.OnAbort();
			}

			// Token: 0x06007A80 RID: 31360 RVA: 0x001C8394 File Offset: 0x001C6594
			protected override void OnClose(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (this.transportManagerContainer != null)
				{
					this.transportManagerContainer.Close(timeoutHelper.RemainingTime());
				}
				base.OnClose(timeoutHelper.RemainingTime());
			}

			// Token: 0x06007A81 RID: 31361 RVA: 0x001C83D0 File Offset: 0x001C65D0
			private IAsyncResult DummyBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new CompletedAsyncResult(callback, state);
			}

			// Token: 0x06007A82 RID: 31362 RVA: 0x001C83D9 File Offset: 0x001C65D9
			private void DummyEndClose(IAsyncResult result)
			{
				CompletedAsyncResult.End(result);
			}

			// Token: 0x06007A83 RID: 31363 RVA: 0x001C83E4 File Offset: 0x001C65E4
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				ChainedBeginHandler begin = new ChainedBeginHandler(this.DummyBeginClose);
				ChainedEndHandler end = new ChainedEndHandler(this.DummyEndClose);
				if (this.transportManagerContainer != null)
				{
					begin = new ChainedBeginHandler(this.transportManagerContainer.BeginClose);
					end = new ChainedEndHandler(this.transportManagerContainer.EndClose);
				}
				return new ChainedAsyncResult(timeout, callback, state, begin, end, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose));
			}

			// Token: 0x06007A84 RID: 31364 RVA: 0x001C8459 File Offset: 0x001C6659
			protected override void OnEndClose(IAsyncResult result)
			{
				ChainedAsyncResult.End(result);
			}

			// Token: 0x0400460A RID: 17930
			private TransportManagerContainer transportManagerContainer;
		}
	}
}

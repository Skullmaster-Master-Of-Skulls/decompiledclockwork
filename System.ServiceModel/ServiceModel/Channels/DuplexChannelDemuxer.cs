using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000723 RID: 1827
	internal class DuplexChannelDemuxer : DatagramChannelDemuxer<IDuplexChannel, Message>
	{
		// Token: 0x06004569 RID: 17769 RVA: 0x00104011 File Offset: 0x00102211
		public DuplexChannelDemuxer(BindingContext context) : base(context)
		{
		}

		// Token: 0x0600456A RID: 17770 RVA: 0x0010401A File Offset: 0x0010221A
		protected override void AbortItem(Message message)
		{
			TypedChannelDemuxer.AbortMessage(message);
		}

		// Token: 0x0600456B RID: 17771 RVA: 0x00104022 File Offset: 0x00102222
		protected override IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginReceive(timeout, callback, state);
		}

		// Token: 0x0600456C RID: 17772 RVA: 0x00104034 File Offset: 0x00102234
		protected override LayeredChannelListener<IDuplexChannel> CreateListener<IDuplexChannel>(ChannelDemuxerFilter filter)
		{
			SingletonChannelListener<IDuplexChannel, DuplexChannel, Message> singletonChannelListener = new SingletonChannelListener<IDuplexChannel, DuplexChannel, Message>(filter, this);
			singletonChannelListener.Acceptor = (IChannelAcceptor<IDuplexChannel>)new DuplexChannelDemuxer.DuplexChannelAcceptor(singletonChannelListener, this);
			return singletonChannelListener;
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x0010405C File Offset: 0x0010225C
		protected override void Dispatch(IChannelListener listener)
		{
			SingletonChannelListener<IDuplexChannel, DuplexChannel, Message> singletonChannelListener = (SingletonChannelListener<IDuplexChannel, DuplexChannel, Message>)listener;
			singletonChannelListener.Dispatch();
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x00104076 File Offset: 0x00102276
		protected override void EndpointNotFound(Message message)
		{
			if (base.DemuxFailureHandler != null)
			{
				base.DemuxFailureHandler.HandleDemuxFailure(message);
			}
			this.AbortItem(message);
		}

		// Token: 0x0600456F RID: 17775 RVA: 0x00104093 File Offset: 0x00102293
		protected override Message EndReceive(IAsyncResult result)
		{
			return base.InnerChannel.EndReceive(result);
		}

		// Token: 0x06004570 RID: 17776 RVA: 0x001040A4 File Offset: 0x001022A4
		protected override void EnqueueAndDispatch(IChannelListener listener, Message message, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			SingletonChannelListener<IDuplexChannel, DuplexChannel, Message> singletonChannelListener = (SingletonChannelListener<IDuplexChannel, DuplexChannel, Message>)listener;
			singletonChannelListener.EnqueueAndDispatch(message, dequeuedCallback, canDispatchOnThisThread);
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x001040C4 File Offset: 0x001022C4
		protected override void EnqueueAndDispatch(IChannelListener listener, Exception exception, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			SingletonChannelListener<IDuplexChannel, DuplexChannel, Message> singletonChannelListener = (SingletonChannelListener<IDuplexChannel, DuplexChannel, Message>)listener;
			singletonChannelListener.EnqueueAndDispatch(exception, dequeuedCallback, canDispatchOnThisThread);
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x001040E2 File Offset: 0x001022E2
		protected override Message GetMessage(Message message)
		{
			return message;
		}

		// Token: 0x02000CCA RID: 3274
		private class DuplexChannelAcceptor : SingletonChannelAcceptor<IDuplexChannel, DuplexChannel, Message>
		{
			// Token: 0x060079AF RID: 31151 RVA: 0x001C6059 File Offset: 0x001C4259
			public DuplexChannelAcceptor(ChannelManagerBase channelManager, DuplexChannelDemuxer demuxer) : base(channelManager)
			{
				this.demuxer = demuxer;
			}

			// Token: 0x060079B0 RID: 31152 RVA: 0x001C6069 File Offset: 0x001C4269
			protected override DuplexChannel OnCreateChannel()
			{
				return new DuplexChannelDemuxer.DuplexChannelWrapper(base.ChannelManager, this.demuxer.InnerChannel);
			}

			// Token: 0x060079B1 RID: 31153 RVA: 0x001C6081 File Offset: 0x001C4281
			protected override void OnTraceMessageReceived(Message message)
			{
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 262163, SR.GetString("TraceCodeMessageReceived"), MessageTransmitTraceRecord.CreateReceiveTraceRecord(message), this, null);
				}
			}

			// Token: 0x0400459F RID: 17823
			private DuplexChannelDemuxer demuxer;
		}

		// Token: 0x02000CCB RID: 3275
		private class DuplexChannelWrapper : DuplexChannel
		{
			// Token: 0x060079B2 RID: 31154 RVA: 0x001C60A7 File Offset: 0x001C42A7
			public DuplexChannelWrapper(ChannelManagerBase channelManager, IDuplexChannel innerChannel) : base(channelManager, innerChannel.LocalAddress)
			{
				this.innerChannel = innerChannel;
			}

			// Token: 0x17001B9A RID: 7066
			// (get) Token: 0x060079B3 RID: 31155 RVA: 0x001C60BD File Offset: 0x001C42BD
			public override EndpointAddress RemoteAddress
			{
				get
				{
					return this.innerChannel.RemoteAddress;
				}
			}

			// Token: 0x17001B9B RID: 7067
			// (get) Token: 0x060079B4 RID: 31156 RVA: 0x001C60CA File Offset: 0x001C42CA
			public override Uri Via
			{
				get
				{
					return this.innerChannel.Via;
				}
			}

			// Token: 0x060079B5 RID: 31157 RVA: 0x001C60D7 File Offset: 0x001C42D7
			protected override void OnSend(Message message, TimeSpan timeout)
			{
				this.innerChannel.Send(message, timeout);
			}

			// Token: 0x060079B6 RID: 31158 RVA: 0x001C60E6 File Offset: 0x001C42E6
			protected override IAsyncResult OnBeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerChannel.BeginSend(message, timeout, callback, state);
			}

			// Token: 0x060079B7 RID: 31159 RVA: 0x001C60F8 File Offset: 0x001C42F8
			protected override void OnEndSend(IAsyncResult result)
			{
				this.innerChannel.EndSend(result);
			}

			// Token: 0x060079B8 RID: 31160 RVA: 0x001C6106 File Offset: 0x001C4306
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new CompletedAsyncResult(callback, state);
			}

			// Token: 0x060079B9 RID: 31161 RVA: 0x001C610F File Offset: 0x001C430F
			protected override void OnEndOpen(IAsyncResult result)
			{
				CompletedAsyncResult.End(result);
			}

			// Token: 0x060079BA RID: 31162 RVA: 0x001C6117 File Offset: 0x001C4317
			protected override void OnOpen(TimeSpan timeout)
			{
			}

			// Token: 0x040045A0 RID: 17824
			private IDuplexChannel innerChannel;
		}
	}
}

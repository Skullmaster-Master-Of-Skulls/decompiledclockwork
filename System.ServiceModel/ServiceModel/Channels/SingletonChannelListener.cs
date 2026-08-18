using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000726 RID: 1830
	internal class SingletonChannelListener<TChannel, TQueuedChannel, TQueuedItem> : DelegatingChannelListener<TChannel>, IChannelDemuxerFilter where TChannel : class, IChannel where TQueuedChannel : InputQueueChannel<TQueuedItem> where TQueuedItem : class, IDisposable
	{
		// Token: 0x0600457F RID: 17791 RVA: 0x00104469 File Offset: 0x00102669
		public SingletonChannelListener(ChannelDemuxerFilter filter, IChannelDemuxer channelDemuxer) : base(true)
		{
			this.filter = filter;
			this.channelDemuxer = channelDemuxer;
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x06004580 RID: 17792 RVA: 0x00104480 File Offset: 0x00102680
		public ChannelDemuxerFilter Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06004581 RID: 17793 RVA: 0x00104488 File Offset: 0x00102688
		// (set) Token: 0x06004582 RID: 17794 RVA: 0x00104495 File Offset: 0x00102695
		private SingletonChannelAcceptor<TChannel, TQueuedChannel, TQueuedItem> SingletonAcceptor
		{
			get
			{
				return (SingletonChannelAcceptor<TChannel, TQueuedChannel, TQueuedItem>)base.Acceptor;
			}
			set
			{
				base.Acceptor = value;
			}
		}

		// Token: 0x06004583 RID: 17795 RVA: 0x001044A0 File Offset: 0x001026A0
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.channelDemuxer.OnOuterListenerOpen(this.filter, this, timeoutHelper.RemainingTime());
			base.OnOpen(timeoutHelper.RemainingTime());
		}

		// Token: 0x06004584 RID: 17796 RVA: 0x001044DB File Offset: 0x001026DB
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(this.OnBeginOuterListenerOpen), new ChainedEndHandler(this.OnEndOuterListenerOpen), new ChainedBeginHandler(base.OnBeginOpen), new ChainedEndHandler(base.OnEndOpen));
		}

		// Token: 0x06004585 RID: 17797 RVA: 0x00104515 File Offset: 0x00102715
		protected override void OnEndOpen(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06004586 RID: 17798 RVA: 0x0010451D File Offset: 0x0010271D
		private IAsyncResult OnBeginOuterListenerOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channelDemuxer.OnBeginOuterListenerOpen(this.filter, this, timeout, callback, state);
		}

		// Token: 0x06004587 RID: 17799 RVA: 0x00104534 File Offset: 0x00102734
		private void OnEndOuterListenerOpen(IAsyncResult result)
		{
			this.channelDemuxer.OnEndOuterListenerOpen(result);
		}

		// Token: 0x06004588 RID: 17800 RVA: 0x00104542 File Offset: 0x00102742
		protected override void OnAbort()
		{
			this.channelDemuxer.OnOuterListenerAbort(this.filter);
			base.OnAbort();
		}

		// Token: 0x06004589 RID: 17801 RVA: 0x0010455C File Offset: 0x0010275C
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.channelDemuxer.OnOuterListenerClose(this.filter, timeoutHelper.RemainingTime());
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x0600458A RID: 17802 RVA: 0x00104596 File Offset: 0x00102796
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(this.OnBeginOuterListenerClose), new ChainedEndHandler(this.OnEndOuterListenerClose), new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose));
		}

		// Token: 0x0600458B RID: 17803 RVA: 0x001045D0 File Offset: 0x001027D0
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x0600458C RID: 17804 RVA: 0x001045D8 File Offset: 0x001027D8
		private IAsyncResult OnBeginOuterListenerClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channelDemuxer.OnBeginOuterListenerClose(this.filter, timeout, callback, state);
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x001045EE File Offset: 0x001027EE
		private void OnEndOuterListenerClose(IAsyncResult result)
		{
			this.channelDemuxer.OnEndOuterListenerClose(result);
		}

		// Token: 0x0600458E RID: 17806 RVA: 0x001045FC File Offset: 0x001027FC
		public void Dispatch()
		{
			this.SingletonAcceptor.DispatchItems();
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x00104609 File Offset: 0x00102809
		public void EnqueueAndDispatch(TQueuedItem item, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			this.SingletonAcceptor.EnqueueAndDispatch(item, dequeuedCallback, canDispatchOnThisThread);
		}

		// Token: 0x06004590 RID: 17808 RVA: 0x00104619 File Offset: 0x00102819
		public void EnqueueAndDispatch(Exception exception, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			this.SingletonAcceptor.EnqueueAndDispatch(exception, dequeuedCallback, canDispatchOnThisThread);
		}

		// Token: 0x04002D5E RID: 11614
		private ChannelDemuxerFilter filter;

		// Token: 0x04002D5F RID: 11615
		private IChannelDemuxer channelDemuxer;
	}
}

using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000731 RID: 1841
	internal class InputQueueChannelListener<TChannel> : DelegatingChannelListener<TChannel> where TChannel : class, IChannel
	{
		// Token: 0x0600460B RID: 17931 RVA: 0x00105E44 File Offset: 0x00104044
		public InputQueueChannelListener(ChannelDemuxerFilter filter, IChannelDemuxer channelDemuxer) : base(true)
		{
			this.filter = filter;
			this.channelDemuxer = channelDemuxer;
			base.Acceptor = new InputQueueChannelAcceptor<TChannel>(this);
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x0600460C RID: 17932 RVA: 0x00105E67 File Offset: 0x00104067
		public ChannelDemuxerFilter Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x0600460D RID: 17933 RVA: 0x00105E6F File Offset: 0x0010406F
		public InputQueueChannelAcceptor<TChannel> InputQueueAcceptor
		{
			get
			{
				return (InputQueueChannelAcceptor<TChannel>)base.Acceptor;
			}
		}

		// Token: 0x0600460E RID: 17934 RVA: 0x00105E7C File Offset: 0x0010407C
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.channelDemuxer.OnOuterListenerOpen(this.filter, this, timeoutHelper.RemainingTime());
			base.OnOpen(timeoutHelper.RemainingTime());
		}

		// Token: 0x0600460F RID: 17935 RVA: 0x00105EB7 File Offset: 0x001040B7
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(this.OnBeginOuterListenerOpen), new ChainedEndHandler(this.OnEndOuterListenerOpen), new ChainedBeginHandler(base.OnBeginOpen), new ChainedEndHandler(base.OnEndOpen));
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x00105EF1 File Offset: 0x001040F1
		protected override void OnEndOpen(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06004611 RID: 17937 RVA: 0x00105EF9 File Offset: 0x001040F9
		private IAsyncResult OnBeginOuterListenerOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channelDemuxer.OnBeginOuterListenerOpen(this.filter, this, timeout, callback, state);
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x00105F10 File Offset: 0x00104110
		private void OnEndOuterListenerOpen(IAsyncResult result)
		{
			this.channelDemuxer.OnEndOuterListenerOpen(result);
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x00105F1E File Offset: 0x0010411E
		protected override void OnAbort()
		{
			this.channelDemuxer.OnOuterListenerAbort(this.filter);
			base.OnAbort();
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x00105F38 File Offset: 0x00104138
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.channelDemuxer.OnOuterListenerClose(this.filter, timeoutHelper.RemainingTime());
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x00105F72 File Offset: 0x00104172
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(this.OnBeginOuterListenerClose), new ChainedEndHandler(this.OnEndOuterListenerClose), new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose));
		}

		// Token: 0x06004616 RID: 17942 RVA: 0x00105FAC File Offset: 0x001041AC
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06004617 RID: 17943 RVA: 0x00105FB4 File Offset: 0x001041B4
		private IAsyncResult OnBeginOuterListenerClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channelDemuxer.OnBeginOuterListenerClose(this.filter, timeout, callback, state);
		}

		// Token: 0x06004618 RID: 17944 RVA: 0x00105FCA File Offset: 0x001041CA
		private void OnEndOuterListenerClose(IAsyncResult result)
		{
			this.channelDemuxer.OnEndOuterListenerClose(result);
		}

		// Token: 0x04002D70 RID: 11632
		private ChannelDemuxerFilter filter;

		// Token: 0x04002D71 RID: 11633
		private IChannelDemuxer channelDemuxer;
	}
}

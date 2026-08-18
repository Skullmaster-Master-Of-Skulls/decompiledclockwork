using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200074D RID: 1869
	internal abstract class LayeredChannel<TInnerChannel> : ChannelBase where TInnerChannel : class, IChannel
	{
		// Token: 0x06004761 RID: 18273 RVA: 0x00109381 File Offset: 0x00107581
		protected LayeredChannel(ChannelManagerBase channelManager, TInnerChannel innerChannel) : base(channelManager)
		{
			this.innerChannel = innerChannel;
			this.onInnerChannelFaulted = new EventHandler(this.OnInnerChannelFaulted);
			this.innerChannel.Faulted += this.onInnerChannelFaulted;
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x06004762 RID: 18274 RVA: 0x001093B9 File Offset: 0x001075B9
		protected TInnerChannel InnerChannel
		{
			get
			{
				return this.innerChannel;
			}
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x001093C4 File Offset: 0x001075C4
		public override T GetProperty<T>()
		{
			T property = base.GetProperty<T>();
			if (property != null)
			{
				return property;
			}
			return this.InnerChannel.GetProperty<T>();
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x001093F2 File Offset: 0x001075F2
		protected override void OnClosing()
		{
			this.innerChannel.Faulted -= this.onInnerChannelFaulted;
			base.OnClosing();
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x00109410 File Offset: 0x00107610
		protected override void OnAbort()
		{
			this.innerChannel.Abort();
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x00109422 File Offset: 0x00107622
		protected override void OnClose(TimeSpan timeout)
		{
			this.innerChannel.Close(timeout);
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x00109435 File Offset: 0x00107635
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannel.BeginClose(timeout, callback, state);
		}

		// Token: 0x06004768 RID: 18280 RVA: 0x0010944A File Offset: 0x0010764A
		protected override void OnEndClose(IAsyncResult result)
		{
			this.innerChannel.EndClose(result);
		}

		// Token: 0x06004769 RID: 18281 RVA: 0x0010945D File Offset: 0x0010765D
		protected override void OnOpen(TimeSpan timeout)
		{
			this.innerChannel.Open(timeout);
		}

		// Token: 0x0600476A RID: 18282 RVA: 0x00109470 File Offset: 0x00107670
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannel.BeginOpen(timeout, callback, state);
		}

		// Token: 0x0600476B RID: 18283 RVA: 0x00109485 File Offset: 0x00107685
		protected override void OnEndOpen(IAsyncResult result)
		{
			this.innerChannel.EndOpen(result);
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x00109498 File Offset: 0x00107698
		private void OnInnerChannelFaulted(object sender, EventArgs e)
		{
			base.Fault();
		}

		// Token: 0x04002DAB RID: 11691
		private TInnerChannel innerChannel;

		// Token: 0x04002DAC RID: 11692
		private EventHandler onInnerChannelFaulted;
	}
}

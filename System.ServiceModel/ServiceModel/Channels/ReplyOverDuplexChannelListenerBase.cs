using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200075D RID: 1885
	internal abstract class ReplyOverDuplexChannelListenerBase<TOuterChannel, TInnerChannel> : LayeredChannelListener<TOuterChannel> where TOuterChannel : class, IReplyChannel where TInnerChannel : class, IDuplexChannel
	{
		// Token: 0x06004814 RID: 18452 RVA: 0x0010B0A2 File Offset: 0x001092A2
		public ReplyOverDuplexChannelListenerBase(BindingContext context) : base(context.Binding, context.BuildInnerChannelListener<TInnerChannel>())
		{
		}

		// Token: 0x06004815 RID: 18453 RVA: 0x0010B0B6 File Offset: 0x001092B6
		protected override void OnOpening()
		{
			this.innerChannelListener = (IChannelListener<TInnerChannel>)this.InnerChannelListener;
			base.OnOpening();
		}

		// Token: 0x06004816 RID: 18454 RVA: 0x0010B0D0 File Offset: 0x001092D0
		protected override TOuterChannel OnAcceptChannel(TimeSpan timeout)
		{
			TInnerChannel innerChannel = this.innerChannelListener.AcceptChannel(timeout);
			return this.WrapInnerChannel(innerChannel);
		}

		// Token: 0x06004817 RID: 18455 RVA: 0x0010B0F1 File Offset: 0x001092F1
		protected override IAsyncResult OnBeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannelListener.BeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x06004818 RID: 18456 RVA: 0x0010B104 File Offset: 0x00109304
		protected override TOuterChannel OnEndAcceptChannel(IAsyncResult result)
		{
			TInnerChannel innerChannel = this.innerChannelListener.EndAcceptChannel(result);
			return this.WrapInnerChannel(innerChannel);
		}

		// Token: 0x06004819 RID: 18457 RVA: 0x0010B125 File Offset: 0x00109325
		protected override bool OnWaitForChannel(TimeSpan timeout)
		{
			return this.innerChannelListener.WaitForChannel(timeout);
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x0010B133 File Offset: 0x00109333
		protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannelListener.BeginWaitForChannel(timeout, callback, state);
		}

		// Token: 0x0600481B RID: 18459 RVA: 0x0010B143 File Offset: 0x00109343
		protected override bool OnEndWaitForChannel(IAsyncResult result)
		{
			return this.innerChannelListener.EndWaitForChannel(result);
		}

		// Token: 0x0600481C RID: 18460
		protected abstract TOuterChannel CreateWrappedChannel(ChannelManagerBase channelManager, TInnerChannel innerChannel);

		// Token: 0x0600481D RID: 18461 RVA: 0x0010B154 File Offset: 0x00109354
		private TOuterChannel WrapInnerChannel(TInnerChannel innerChannel)
		{
			if (innerChannel == null)
			{
				return default(TOuterChannel);
			}
			return this.CreateWrappedChannel(this, innerChannel);
		}

		// Token: 0x04002DE1 RID: 11745
		private IChannelListener<TInnerChannel> innerChannelListener;
	}
}

using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200073B RID: 1851
	public abstract class ChannelListenerBase<TChannel> : ChannelListenerBase, IChannelListener<TChannel>, IChannelListener, ICommunicationObject where TChannel : class, IChannel
	{
		// Token: 0x06004664 RID: 18020 RVA: 0x00106B21 File Offset: 0x00104D21
		protected ChannelListenerBase()
		{
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x00106B29 File Offset: 0x00104D29
		protected ChannelListenerBase(IDefaultCommunicationTimeouts timeouts) : base(timeouts)
		{
		}

		// Token: 0x06004666 RID: 18022
		protected abstract TChannel OnAcceptChannel(TimeSpan timeout);

		// Token: 0x06004667 RID: 18023
		protected abstract IAsyncResult OnBeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06004668 RID: 18024
		protected abstract TChannel OnEndAcceptChannel(IAsyncResult result);

		// Token: 0x06004669 RID: 18025 RVA: 0x00106B32 File Offset: 0x00104D32
		public TChannel AcceptChannel()
		{
			return this.AcceptChannel(base.InternalReceiveTimeout);
		}

		// Token: 0x0600466A RID: 18026 RVA: 0x00106B40 File Offset: 0x00104D40
		public TChannel AcceptChannel(TimeSpan timeout)
		{
			base.ThrowIfNotOpened();
			base.ThrowPending();
			return this.OnAcceptChannel(timeout);
		}

		// Token: 0x0600466B RID: 18027 RVA: 0x00106B55 File Offset: 0x00104D55
		public IAsyncResult BeginAcceptChannel(AsyncCallback callback, object state)
		{
			return this.BeginAcceptChannel(base.InternalReceiveTimeout, callback, state);
		}

		// Token: 0x0600466C RID: 18028 RVA: 0x00106B65 File Offset: 0x00104D65
		public IAsyncResult BeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.ThrowIfNotOpened();
			base.ThrowPending();
			return this.OnBeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x0600466D RID: 18029 RVA: 0x00106B7C File Offset: 0x00104D7C
		public TChannel EndAcceptChannel(IAsyncResult result)
		{
			return this.OnEndAcceptChannel(result);
		}
	}
}

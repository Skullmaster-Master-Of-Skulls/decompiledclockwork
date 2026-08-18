using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000752 RID: 1874
	internal abstract class LayeredChannelAcceptor<TChannel, TInnerChannel> : ChannelAcceptor<TChannel> where TChannel : class, IChannel where TInnerChannel : class, IChannel
	{
		// Token: 0x060047AD RID: 18349 RVA: 0x00109BA2 File Offset: 0x00107DA2
		protected LayeredChannelAcceptor(ChannelManagerBase channelManager, IChannelListener<TInnerChannel> innerListener) : base(channelManager)
		{
			this.innerListener = innerListener;
		}

		// Token: 0x060047AE RID: 18350
		protected abstract TChannel OnAcceptChannel(TInnerChannel innerChannel);

		// Token: 0x060047AF RID: 18351 RVA: 0x00109BB4 File Offset: 0x00107DB4
		public override TChannel AcceptChannel(TimeSpan timeout)
		{
			TInnerChannel tinnerChannel = this.innerListener.AcceptChannel(timeout);
			if (tinnerChannel == null)
			{
				return default(TChannel);
			}
			return this.OnAcceptChannel(tinnerChannel);
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x00109BE7 File Offset: 0x00107DE7
		public override IAsyncResult BeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerListener.BeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x00109BF8 File Offset: 0x00107DF8
		public override TChannel EndAcceptChannel(IAsyncResult result)
		{
			TInnerChannel tinnerChannel = this.innerListener.EndAcceptChannel(result);
			if (tinnerChannel == null)
			{
				return default(TChannel);
			}
			return this.OnAcceptChannel(tinnerChannel);
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x00109C2B File Offset: 0x00107E2B
		public override bool WaitForChannel(TimeSpan timeout)
		{
			return this.innerListener.WaitForChannel(timeout);
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x00109C39 File Offset: 0x00107E39
		public override IAsyncResult BeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerListener.BeginWaitForChannel(timeout, callback, state);
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x00109C49 File Offset: 0x00107E49
		public override bool EndWaitForChannel(IAsyncResult result)
		{
			return this.innerListener.EndWaitForChannel(result);
		}

		// Token: 0x04002DB4 RID: 11700
		private IChannelListener<TInnerChannel> innerListener;
	}
}

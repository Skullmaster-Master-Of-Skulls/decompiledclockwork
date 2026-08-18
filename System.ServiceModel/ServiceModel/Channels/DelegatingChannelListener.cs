using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000742 RID: 1858
	internal abstract class DelegatingChannelListener<TChannel> : LayeredChannelListener<TChannel> where TChannel : class, IChannel
	{
		// Token: 0x060046E0 RID: 18144 RVA: 0x00108764 File Offset: 0x00106964
		protected DelegatingChannelListener(IDefaultCommunicationTimeouts timeouts, IChannelListener innerChannelListener) : base(timeouts, innerChannelListener)
		{
		}

		// Token: 0x060046E1 RID: 18145 RVA: 0x0010876E File Offset: 0x0010696E
		protected DelegatingChannelListener(bool sharedInnerListener) : base(sharedInnerListener)
		{
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x00108777 File Offset: 0x00106977
		protected DelegatingChannelListener(bool sharedInnerListener, IDefaultCommunicationTimeouts timeouts) : base(sharedInnerListener, timeouts)
		{
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x00108781 File Offset: 0x00106981
		protected DelegatingChannelListener(bool sharedInnerListener, IDefaultCommunicationTimeouts timeouts, IChannelListener innerChannelListener) : base(sharedInnerListener, timeouts, innerChannelListener)
		{
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x060046E4 RID: 18148 RVA: 0x0010878C File Offset: 0x0010698C
		// (set) Token: 0x060046E5 RID: 18149 RVA: 0x00108794 File Offset: 0x00106994
		public IChannelAcceptor<TChannel> Acceptor
		{
			get
			{
				return this.channelAcceptor;
			}
			set
			{
				this.channelAcceptor = value;
			}
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x0010879D File Offset: 0x0010699D
		protected override TChannel OnAcceptChannel(TimeSpan timeout)
		{
			return this.channelAcceptor.AcceptChannel(timeout);
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x001087AB File Offset: 0x001069AB
		protected override IAsyncResult OnBeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channelAcceptor.BeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x001087BB File Offset: 0x001069BB
		protected override TChannel OnEndAcceptChannel(IAsyncResult result)
		{
			return this.channelAcceptor.EndAcceptChannel(result);
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x001087C9 File Offset: 0x001069C9
		protected override bool OnWaitForChannel(TimeSpan timeout)
		{
			return this.channelAcceptor.WaitForChannel(timeout);
		}

		// Token: 0x060046EA RID: 18154 RVA: 0x001087D7 File Offset: 0x001069D7
		protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channelAcceptor.BeginWaitForChannel(timeout, callback, state);
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x001087E7 File Offset: 0x001069E7
		protected override bool OnEndWaitForChannel(IAsyncResult result)
		{
			return this.channelAcceptor.EndWaitForChannel(result);
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x001087F5 File Offset: 0x001069F5
		protected override void OnAbort()
		{
			base.OnAbort();
			if (this.channelAcceptor != null)
			{
				this.channelAcceptor.Abort();
			}
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x00108810 File Offset: 0x00106A10
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedCloseAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), new ICommunicationObject[]
			{
				this.channelAcceptor
			});
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x0010884C File Offset: 0x00106A4C
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x00108854 File Offset: 0x00106A54
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnClose(timeoutHelper.RemainingTime());
			this.channelAcceptor.Close(timeoutHelper.RemainingTime());
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x00108888 File Offset: 0x00106A88
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedOpenAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginOpen), new ChainedEndHandler(base.OnEndOpen), new ICommunicationObject[]
			{
				this.channelAcceptor
			});
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x001088C4 File Offset: 0x00106AC4
		protected override void OnEndOpen(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x001088CC File Offset: 0x00106ACC
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			this.channelAcceptor.Open(timeoutHelper.RemainingTime());
		}

		// Token: 0x04002DA5 RID: 11685
		private IChannelAcceptor<TChannel> channelAcceptor;
	}
}

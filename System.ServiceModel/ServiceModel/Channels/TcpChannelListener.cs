using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000855 RID: 2133
	internal abstract class TcpChannelListener<TChannel, TChannelAcceptor> : TcpChannelListener, IChannelListener<TChannel>, IChannelListener, ICommunicationObject where TChannel : class, IChannel where TChannelAcceptor : ChannelAcceptor<TChannel>
	{
		// Token: 0x06004FFE RID: 20478 RVA: 0x00125A02 File Offset: 0x00123C02
		protected TcpChannelListener(TcpTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context)
		{
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x06004FFF RID: 20479
		protected abstract TChannelAcceptor ChannelAcceptor { get; }

		// Token: 0x06005000 RID: 20480 RVA: 0x00125A0C File Offset: 0x00123C0C
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			this.ChannelAcceptor.Open(timeoutHelper.RemainingTime());
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x00125A48 File Offset: 0x00123C48
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedOpenAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginOpen), new ChainedEndHandler(base.OnEndOpen), new ICommunicationObject[]
			{
				this.ChannelAcceptor
			});
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x00125A89 File Offset: 0x00123C89
		protected override void OnEndOpen(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x00125A94 File Offset: 0x00123C94
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.ChannelAcceptor.Close(timeoutHelper.RemainingTime());
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x00125AD0 File Offset: 0x00123CD0
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedCloseAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), new ICommunicationObject[]
			{
				this.ChannelAcceptor
			});
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x00125B11 File Offset: 0x00123D11
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x00125B19 File Offset: 0x00123D19
		protected override void OnAbort()
		{
			this.ChannelAcceptor.Abort();
			base.OnAbort();
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x00125B31 File Offset: 0x00123D31
		public TChannel AcceptChannel()
		{
			return this.AcceptChannel(this.DefaultReceiveTimeout);
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x00125B3F File Offset: 0x00123D3F
		public IAsyncResult BeginAcceptChannel(AsyncCallback callback, object state)
		{
			return this.BeginAcceptChannel(this.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x00125B4F File Offset: 0x00123D4F
		public TChannel AcceptChannel(TimeSpan timeout)
		{
			base.ThrowIfNotOpened();
			return this.ChannelAcceptor.AcceptChannel(timeout);
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x00125B68 File Offset: 0x00123D68
		public IAsyncResult BeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.ThrowIfNotOpened();
			return this.ChannelAcceptor.BeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x00125B83 File Offset: 0x00123D83
		public TChannel EndAcceptChannel(IAsyncResult result)
		{
			base.ThrowPending();
			return this.ChannelAcceptor.EndAcceptChannel(result);
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x00125B9C File Offset: 0x00123D9C
		protected override bool OnWaitForChannel(TimeSpan timeout)
		{
			return this.ChannelAcceptor.WaitForChannel(timeout);
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x00125BAF File Offset: 0x00123DAF
		protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.ChannelAcceptor.BeginWaitForChannel(timeout, callback, state);
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x00125BC4 File Offset: 0x00123DC4
		protected override bool OnEndWaitForChannel(IAsyncResult result)
		{
			return this.ChannelAcceptor.EndWaitForChannel(result);
		}
	}
}

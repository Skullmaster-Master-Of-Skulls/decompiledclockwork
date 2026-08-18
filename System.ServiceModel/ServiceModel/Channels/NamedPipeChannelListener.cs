using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200083F RID: 2111
	internal abstract class NamedPipeChannelListener<TChannel, TChannelAcceptor> : NamedPipeChannelListener, IChannelListener<TChannel>, IChannelListener, ICommunicationObject where TChannel : class, IChannel where TChannelAcceptor : ChannelAcceptor<TChannel>
	{
		// Token: 0x06004EE2 RID: 20194 RVA: 0x0011F755 File Offset: 0x0011D955
		protected NamedPipeChannelListener(NamedPipeTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context)
		{
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x06004EE3 RID: 20195
		protected abstract TChannelAcceptor ChannelAcceptor { get; }

		// Token: 0x06004EE4 RID: 20196 RVA: 0x0011F760 File Offset: 0x0011D960
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			this.ChannelAcceptor.Open(timeoutHelper.RemainingTime());
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x0011F79C File Offset: 0x0011D99C
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedOpenAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginOpen), new ChainedEndHandler(base.OnEndOpen), new ICommunicationObject[]
			{
				this.ChannelAcceptor
			});
		}

		// Token: 0x06004EE6 RID: 20198 RVA: 0x0011F7DD File Offset: 0x0011D9DD
		protected override void OnEndOpen(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06004EE7 RID: 20199 RVA: 0x0011F7E8 File Offset: 0x0011D9E8
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.ChannelAcceptor.Close(timeoutHelper.RemainingTime());
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06004EE8 RID: 20200 RVA: 0x0011F821 File Offset: 0x0011DA21
		protected override void OnAbort()
		{
			this.ChannelAcceptor.Abort();
			base.OnAbort();
		}

		// Token: 0x06004EE9 RID: 20201 RVA: 0x0011F83C File Offset: 0x0011DA3C
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedCloseAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), new ICommunicationObject[]
			{
				this.ChannelAcceptor
			});
		}

		// Token: 0x06004EEA RID: 20202 RVA: 0x0011F87D File Offset: 0x0011DA7D
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x0011F885 File Offset: 0x0011DA85
		public TChannel AcceptChannel()
		{
			return this.AcceptChannel(this.DefaultReceiveTimeout);
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x0011F893 File Offset: 0x0011DA93
		public IAsyncResult BeginAcceptChannel(AsyncCallback callback, object state)
		{
			return this.BeginAcceptChannel(this.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x06004EED RID: 20205 RVA: 0x0011F8A3 File Offset: 0x0011DAA3
		public TChannel AcceptChannel(TimeSpan timeout)
		{
			base.ThrowIfNotOpened();
			return this.ChannelAcceptor.AcceptChannel(timeout);
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x0011F8BC File Offset: 0x0011DABC
		public IAsyncResult BeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.ThrowIfNotOpened();
			return this.ChannelAcceptor.BeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x0011F8D7 File Offset: 0x0011DAD7
		public TChannel EndAcceptChannel(IAsyncResult result)
		{
			base.ThrowPending();
			return this.ChannelAcceptor.EndAcceptChannel(result);
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x0011F8F0 File Offset: 0x0011DAF0
		protected override bool OnWaitForChannel(TimeSpan timeout)
		{
			return this.ChannelAcceptor.WaitForChannel(timeout);
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x0011F903 File Offset: 0x0011DB03
		protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.ChannelAcceptor.BeginWaitForChannel(timeout, callback, state);
		}

		// Token: 0x06004EF2 RID: 20210 RVA: 0x0011F918 File Offset: 0x0011DB18
		protected override bool OnEndWaitForChannel(IAsyncResult result)
		{
			return this.ChannelAcceptor.EndWaitForChannel(result);
		}
	}
}

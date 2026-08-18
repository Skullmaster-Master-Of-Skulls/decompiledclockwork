using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009F3 RID: 2547
	[Obsolete("PeerChannel feature is obsolete and will be removed in the future.", false)]
	internal abstract class PeerChannelListener<TChannel, TChannelAcceptor> : PeerChannelListenerBase, IChannelListener<TChannel>, IChannelListener, ICommunicationObject where TChannel : class, IChannel where TChannelAcceptor : ChannelAcceptor<TChannel>
	{
		// Token: 0x060064E9 RID: 25833 RVA: 0x0017862E File Offset: 0x0017682E
		public PeerChannelListener(PeerTransportBindingElement bindingElement, BindingContext context, PeerResolver peerResolver) : base(bindingElement, context, peerResolver)
		{
		}

		// Token: 0x17001864 RID: 6244
		// (get) Token: 0x060064EA RID: 25834
		protected abstract TChannelAcceptor ChannelAcceptor { get; }

		// Token: 0x060064EB RID: 25835 RVA: 0x00178639 File Offset: 0x00176839
		internal override ITransportManagerRegistration CreateTransportManagerRegistration(Uri listenUri)
		{
			return null;
		}

		// Token: 0x060064EC RID: 25836 RVA: 0x0017863C File Offset: 0x0017683C
		public TChannel AcceptChannel()
		{
			return this.AcceptChannel(this.DefaultReceiveTimeout);
		}

		// Token: 0x060064ED RID: 25837 RVA: 0x0017864A File Offset: 0x0017684A
		public IAsyncResult BeginAcceptChannel(AsyncCallback callback, object state)
		{
			return this.BeginAcceptChannel(this.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x060064EE RID: 25838 RVA: 0x0017865A File Offset: 0x0017685A
		public TChannel AcceptChannel(TimeSpan timeout)
		{
			base.ThrowIfNotOpened();
			return this.ChannelAcceptor.AcceptChannel(timeout);
		}

		// Token: 0x060064EF RID: 25839 RVA: 0x00178673 File Offset: 0x00176873
		public IAsyncResult BeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.ThrowIfNotOpened();
			return this.ChannelAcceptor.BeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x060064F0 RID: 25840 RVA: 0x0017868E File Offset: 0x0017688E
		public TChannel EndAcceptChannel(IAsyncResult result)
		{
			return this.ChannelAcceptor.EndAcceptChannel(result);
		}

		// Token: 0x060064F1 RID: 25841 RVA: 0x001786A4 File Offset: 0x001768A4
		private void OnCloseCore(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.ChannelAcceptor.Close(timeoutHelper.RemainingTime());
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x060064F2 RID: 25842 RVA: 0x001786DD File Offset: 0x001768DD
		protected override void OnClose(TimeSpan timeout)
		{
			this.OnCloseCore(timeout);
		}

		// Token: 0x060064F3 RID: 25843 RVA: 0x001786E6 File Offset: 0x001768E6
		protected override void OnAbort()
		{
			if (this.ChannelAcceptor != null)
			{
				this.ChannelAcceptor.Abort();
			}
			base.OnAbort();
		}

		// Token: 0x060064F4 RID: 25844 RVA: 0x0017870C File Offset: 0x0017690C
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeoutHelper data = new TimeoutHelper(timeout);
			return new CompletedAsyncResult<TimeoutHelper>(data, callback, state);
		}

		// Token: 0x060064F5 RID: 25845 RVA: 0x0017872C File Offset: 0x0017692C
		protected override void OnEndClose(IAsyncResult result)
		{
			this.OnCloseCore(CompletedAsyncResult<TimeoutHelper>.End(result).RemainingTime());
		}

		// Token: 0x060064F6 RID: 25846 RVA: 0x00178750 File Offset: 0x00176950
		private void OnOpenCore(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			this.CreateAcceptor();
			this.ChannelAcceptor.Open(timeoutHelper.RemainingTime());
		}

		// Token: 0x060064F7 RID: 25847 RVA: 0x00178790 File Offset: 0x00176990
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.OnOpenCore(timeoutHelper.RemainingTime());
		}

		// Token: 0x060064F8 RID: 25848 RVA: 0x001787B4 File Offset: 0x001769B4
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeoutHelper data = new TimeoutHelper(timeout);
			return new CompletedAsyncResult<TimeoutHelper>(data, callback, state);
		}

		// Token: 0x060064F9 RID: 25849 RVA: 0x001787D4 File Offset: 0x001769D4
		protected override void OnEndOpen(IAsyncResult result)
		{
			this.OnOpenCore(CompletedAsyncResult<TimeoutHelper>.End(result).RemainingTime());
		}

		// Token: 0x060064FA RID: 25850 RVA: 0x001787F5 File Offset: 0x001769F5
		protected override bool OnWaitForChannel(TimeSpan timeout)
		{
			return this.ChannelAcceptor.WaitForChannel(timeout);
		}

		// Token: 0x060064FB RID: 25851 RVA: 0x00178808 File Offset: 0x00176A08
		protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.ChannelAcceptor.BeginWaitForChannel(timeout, callback, state);
		}

		// Token: 0x060064FC RID: 25852 RVA: 0x0017881D File Offset: 0x00176A1D
		protected override bool OnEndWaitForChannel(IAsyncResult result)
		{
			return this.ChannelAcceptor.EndWaitForChannel(result);
		}

		// Token: 0x060064FD RID: 25853
		protected abstract void CreateAcceptor();
	}
}

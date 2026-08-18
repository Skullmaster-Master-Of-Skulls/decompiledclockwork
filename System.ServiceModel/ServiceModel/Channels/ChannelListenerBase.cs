using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200073A RID: 1850
	public abstract class ChannelListenerBase : ChannelManagerBase, IChannelListener, ICommunicationObject
	{
		// Token: 0x06004656 RID: 18006 RVA: 0x001069EA File Offset: 0x00104BEA
		protected ChannelListenerBase()
		{
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x00106A20 File Offset: 0x00104C20
		protected ChannelListenerBase(IDefaultCommunicationTimeouts timeouts)
		{
			if (timeouts != null)
			{
				this.closeTimeout = timeouts.CloseTimeout;
				this.openTimeout = timeouts.OpenTimeout;
				this.receiveTimeout = timeouts.ReceiveTimeout;
				this.sendTimeout = timeouts.SendTimeout;
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x06004658 RID: 18008 RVA: 0x00106A92 File Offset: 0x00104C92
		protected override TimeSpan DefaultCloseTimeout
		{
			get
			{
				return this.closeTimeout;
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x06004659 RID: 18009 RVA: 0x00106A9A File Offset: 0x00104C9A
		protected override TimeSpan DefaultOpenTimeout
		{
			get
			{
				return this.openTimeout;
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x0600465A RID: 18010 RVA: 0x00106AA2 File Offset: 0x00104CA2
		protected override TimeSpan DefaultReceiveTimeout
		{
			get
			{
				return this.receiveTimeout;
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x0600465B RID: 18011 RVA: 0x00106AAA File Offset: 0x00104CAA
		protected override TimeSpan DefaultSendTimeout
		{
			get
			{
				return this.sendTimeout;
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x0600465C RID: 18012
		public abstract Uri Uri { get; }

		// Token: 0x0600465D RID: 18013 RVA: 0x00106AB4 File Offset: 0x00104CB4
		public virtual T GetProperty<T>() where T : class
		{
			if (typeof(T) == typeof(IChannelListener))
			{
				return (T)((object)this);
			}
			return default(T);
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x00106AEC File Offset: 0x00104CEC
		public bool WaitForChannel(TimeSpan timeout)
		{
			base.ThrowIfNotOpened();
			base.ThrowPending();
			return this.OnWaitForChannel(timeout);
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x00106B01 File Offset: 0x00104D01
		public IAsyncResult BeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.ThrowIfNotOpened();
			base.ThrowPending();
			return this.OnBeginWaitForChannel(timeout, callback, state);
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x00106B18 File Offset: 0x00104D18
		public bool EndWaitForChannel(IAsyncResult result)
		{
			return this.OnEndWaitForChannel(result);
		}

		// Token: 0x06004661 RID: 18017
		protected abstract bool OnWaitForChannel(TimeSpan timeout);

		// Token: 0x06004662 RID: 18018
		protected abstract IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06004663 RID: 18019
		protected abstract bool OnEndWaitForChannel(IAsyncResult result);

		// Token: 0x04002D86 RID: 11654
		private TimeSpan closeTimeout = ServiceDefaults.CloseTimeout;

		// Token: 0x04002D87 RID: 11655
		private TimeSpan openTimeout = ServiceDefaults.OpenTimeout;

		// Token: 0x04002D88 RID: 11656
		private TimeSpan receiveTimeout = ServiceDefaults.ReceiveTimeout;

		// Token: 0x04002D89 RID: 11657
		private TimeSpan sendTimeout = ServiceDefaults.SendTimeout;
	}
}

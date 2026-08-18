using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200071B RID: 1819
	internal abstract class ChannelAcceptor<TChannel> : CommunicationObject, IChannelAcceptor<TChannel>, ICommunicationObject where TChannel : class, IChannel
	{
		// Token: 0x060044FD RID: 17661 RVA: 0x00102E88 File Offset: 0x00101088
		protected ChannelAcceptor(ChannelManagerBase channelManager)
		{
			this.channelManager = channelManager;
		}

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x060044FE RID: 17662 RVA: 0x00102E97 File Offset: 0x00101097
		protected ChannelManagerBase ChannelManager
		{
			get
			{
				return this.channelManager;
			}
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x060044FF RID: 17663 RVA: 0x00102E9F File Offset: 0x0010109F
		protected override TimeSpan DefaultCloseTimeout
		{
			get
			{
				return this.channelManager.InternalCloseTimeout;
			}
		}

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x06004500 RID: 17664 RVA: 0x00102EAC File Offset: 0x001010AC
		protected override TimeSpan DefaultOpenTimeout
		{
			get
			{
				return this.channelManager.InternalOpenTimeout;
			}
		}

		// Token: 0x06004501 RID: 17665
		public abstract TChannel AcceptChannel(TimeSpan timeout);

		// Token: 0x06004502 RID: 17666
		public abstract IAsyncResult BeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06004503 RID: 17667
		public abstract TChannel EndAcceptChannel(IAsyncResult result);

		// Token: 0x06004504 RID: 17668
		public abstract bool WaitForChannel(TimeSpan timeout);

		// Token: 0x06004505 RID: 17669
		public abstract IAsyncResult BeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06004506 RID: 17670
		public abstract bool EndWaitForChannel(IAsyncResult result);

		// Token: 0x06004507 RID: 17671 RVA: 0x00102EB9 File Offset: 0x001010B9
		protected override void OnAbort()
		{
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x00102EBB File Offset: 0x001010BB
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06004509 RID: 17673 RVA: 0x00102EC4 File Offset: 0x001010C4
		protected override void OnEndClose(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x00102ECC File Offset: 0x001010CC
		protected override void OnClose(TimeSpan timeout)
		{
		}

		// Token: 0x0600450B RID: 17675 RVA: 0x00102ECE File Offset: 0x001010CE
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x00102ED7 File Offset: 0x001010D7
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x00102EDF File Offset: 0x001010DF
		protected override void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x04002D47 RID: 11591
		private ChannelManagerBase channelManager;
	}
}

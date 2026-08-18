using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000750 RID: 1872
	internal class LayeredDuplexChannel : LayeredInputChannel, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel
	{
		// Token: 0x06004786 RID: 18310 RVA: 0x001096F4 File Offset: 0x001078F4
		public LayeredDuplexChannel(ChannelManagerBase channelManager, IInputChannel innerInputChannel, EndpointAddress localAddress, IOutputChannel innerOutputChannel) : base(channelManager, innerInputChannel)
		{
			this.localAddress = localAddress;
			this.innerOutputChannel = innerOutputChannel;
			this.onInnerOutputChannelFaulted = new EventHandler(this.OnInnerOutputChannelFaulted);
			this.innerOutputChannel.Faulted += this.onInnerOutputChannelFaulted;
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06004787 RID: 18311 RVA: 0x00109730 File Offset: 0x00107930
		public override EndpointAddress LocalAddress
		{
			get
			{
				return this.localAddress;
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06004788 RID: 18312 RVA: 0x00109738 File Offset: 0x00107938
		public EndpointAddress RemoteAddress
		{
			get
			{
				return this.innerOutputChannel.RemoteAddress;
			}
		}

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x06004789 RID: 18313 RVA: 0x00109745 File Offset: 0x00107945
		public Uri Via
		{
			get
			{
				return this.innerOutputChannel.Via;
			}
		}

		// Token: 0x0600478A RID: 18314 RVA: 0x00109752 File Offset: 0x00107952
		protected override void OnClosing()
		{
			this.innerOutputChannel.Faulted -= this.onInnerOutputChannelFaulted;
			base.OnClosing();
		}

		// Token: 0x0600478B RID: 18315 RVA: 0x0010976B File Offset: 0x0010796B
		protected override void OnAbort()
		{
			this.innerOutputChannel.Abort();
			base.OnAbort();
		}

		// Token: 0x0600478C RID: 18316 RVA: 0x00109780 File Offset: 0x00107980
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedCloseAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), new ICommunicationObject[]
			{
				this.innerOutputChannel
			});
		}

		// Token: 0x0600478D RID: 18317 RVA: 0x001097BC File Offset: 0x001079BC
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x0600478E RID: 18318 RVA: 0x001097C4 File Offset: 0x001079C4
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.innerOutputChannel.Close(timeoutHelper.RemainingTime());
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x0600478F RID: 18319 RVA: 0x001097F8 File Offset: 0x001079F8
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedOpenAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginOpen), new ChainedEndHandler(base.OnEndOpen), new ICommunicationObject[]
			{
				this.innerOutputChannel
			});
		}

		// Token: 0x06004790 RID: 18320 RVA: 0x00109834 File Offset: 0x00107A34
		protected override void OnEndOpen(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06004791 RID: 18321 RVA: 0x0010983C File Offset: 0x00107A3C
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			this.innerOutputChannel.Open(timeoutHelper.RemainingTime());
		}

		// Token: 0x06004792 RID: 18322 RVA: 0x00109870 File Offset: 0x00107A70
		public void Send(Message message)
		{
			this.Send(message, base.DefaultSendTimeout);
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x0010987F File Offset: 0x00107A7F
		public void Send(Message message, TimeSpan timeout)
		{
			this.innerOutputChannel.Send(message, timeout);
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x0010988E File Offset: 0x00107A8E
		public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
		{
			return this.BeginSend(message, base.DefaultSendTimeout, callback, state);
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x0010989F File Offset: 0x00107A9F
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerOutputChannel.BeginSend(message, timeout, callback, state);
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x001098B1 File Offset: 0x00107AB1
		public void EndSend(IAsyncResult result)
		{
			this.innerOutputChannel.EndSend(result);
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x001098BF File Offset: 0x00107ABF
		private void OnInnerOutputChannelFaulted(object sender, EventArgs e)
		{
			base.Fault();
		}

		// Token: 0x04002DAE RID: 11694
		private IOutputChannel innerOutputChannel;

		// Token: 0x04002DAF RID: 11695
		private EndpointAddress localAddress;

		// Token: 0x04002DB0 RID: 11696
		private EventHandler onInnerOutputChannelFaulted;
	}
}

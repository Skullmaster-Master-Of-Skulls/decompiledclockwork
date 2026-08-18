using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008E8 RID: 2280
	internal abstract class MsmqInputChannelListenerBase : MsmqChannelListenerBase<IInputChannel>
	{
		// Token: 0x060056F5 RID: 22261 RVA: 0x0013F2F4 File Offset: 0x0013D4F4
		internal MsmqInputChannelListenerBase(MsmqBindingElementBase bindingElement, BindingContext context, MsmqReceiveParameters receiveParameters) : this(bindingElement, context, receiveParameters, TransportDefaults.GetDefaultMessageEncoderFactory())
		{
		}

		// Token: 0x060056F6 RID: 22262 RVA: 0x0013F304 File Offset: 0x0013D504
		internal MsmqInputChannelListenerBase(MsmqBindingElementBase bindingElement, BindingContext context, MsmqReceiveParameters receiveParameters, MessageEncoderFactory encoderFactory) : base(bindingElement, context, receiveParameters, encoderFactory)
		{
			this.acceptor = new InputQueueChannelAcceptor<IInputChannel>(this);
		}

		// Token: 0x060056F7 RID: 22263 RVA: 0x0013F320 File Offset: 0x0013D520
		private void OnNewChannelNeeded(object sender, EventArgs ea)
		{
			if (!base.IsDisposed && (CommunicationState.Opened == base.State || CommunicationState.Opening == base.State))
			{
				IInputChannel inputChannel = this.CreateInputChannel(this);
				inputChannel.Closed += this.OnNewChannelNeeded;
				this.acceptor.EnqueueAndDispatch(inputChannel);
			}
		}

		// Token: 0x060056F8 RID: 22264 RVA: 0x0013F36D File Offset: 0x0013D56D
		protected override void OnOpenCore(TimeSpan timeout)
		{
			base.OnOpenCore(timeout);
			this.acceptor.Open();
			this.OnNewChannelNeeded(this, EventArgs.Empty);
		}

		// Token: 0x060056F9 RID: 22265 RVA: 0x0013F38D File Offset: 0x0013D58D
		protected override void OnCloseCore(bool aborting)
		{
			this.acceptor.Close();
			base.OnCloseCore(aborting);
		}

		// Token: 0x060056FA RID: 22266
		protected abstract IInputChannel CreateInputChannel(MsmqInputChannelListenerBase listener);

		// Token: 0x060056FB RID: 22267 RVA: 0x0013F3A1 File Offset: 0x0013D5A1
		public override IInputChannel AcceptChannel()
		{
			return this.AcceptChannel(this.DefaultReceiveTimeout);
		}

		// Token: 0x060056FC RID: 22268 RVA: 0x0013F3AF File Offset: 0x0013D5AF
		public override IAsyncResult BeginAcceptChannel(AsyncCallback callback, object state)
		{
			return this.BeginAcceptChannel(this.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x060056FD RID: 22269 RVA: 0x0013F3BF File Offset: 0x0013D5BF
		public override IInputChannel AcceptChannel(TimeSpan timeout)
		{
			return this.acceptor.AcceptChannel(timeout);
		}

		// Token: 0x060056FE RID: 22270 RVA: 0x0013F3CD File Offset: 0x0013D5CD
		public override IAsyncResult BeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.acceptor.BeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x060056FF RID: 22271 RVA: 0x0013F3DD File Offset: 0x0013D5DD
		public override IInputChannel EndAcceptChannel(IAsyncResult result)
		{
			return this.acceptor.EndAcceptChannel(result);
		}

		// Token: 0x06005700 RID: 22272 RVA: 0x0013F3EB File Offset: 0x0013D5EB
		protected override bool OnWaitForChannel(TimeSpan timeout)
		{
			return this.acceptor.WaitForChannel(timeout);
		}

		// Token: 0x06005701 RID: 22273 RVA: 0x0013F3F9 File Offset: 0x0013D5F9
		protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.acceptor.BeginWaitForChannel(timeout, callback, state);
		}

		// Token: 0x06005702 RID: 22274 RVA: 0x0013F409 File Offset: 0x0013D609
		protected override bool OnEndWaitForChannel(IAsyncResult result)
		{
			return this.acceptor.EndWaitForChannel(result);
		}

		// Token: 0x04003591 RID: 13713
		private InputQueueChannelAcceptor<IInputChannel> acceptor;
	}
}

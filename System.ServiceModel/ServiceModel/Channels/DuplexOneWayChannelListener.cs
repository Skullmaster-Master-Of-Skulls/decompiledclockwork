using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000896 RID: 2198
	internal class DuplexOneWayChannelListener : LayeredChannelListener<IInputChannel>
	{
		// Token: 0x0600536F RID: 21359 RVA: 0x00133517 File Offset: 0x00131717
		public DuplexOneWayChannelListener(OneWayBindingElement bindingElement, BindingContext context) : base(context.Binding, context.BuildInnerChannelListener<IDuplexChannel>())
		{
			this.packetRoutable = bindingElement.PacketRoutable;
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x00133537 File Offset: 0x00131737
		protected override void OnOpening()
		{
			this.innerChannelListener = (IChannelListener<IDuplexChannel>)this.InnerChannelListener;
			base.OnOpening();
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x00133550 File Offset: 0x00131750
		protected override IInputChannel OnAcceptChannel(TimeSpan timeout)
		{
			IDuplexChannel innerChannel = this.innerChannelListener.AcceptChannel(timeout);
			return this.WrapInnerChannel(innerChannel);
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x00133571 File Offset: 0x00131771
		protected override IAsyncResult OnBeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannelListener.BeginAcceptChannel(timeout, callback, state);
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x00133581 File Offset: 0x00131781
		protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannelListener.BeginWaitForChannel(timeout, callback, state);
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x00133594 File Offset: 0x00131794
		protected override IInputChannel OnEndAcceptChannel(IAsyncResult result)
		{
			IDuplexChannel innerChannel = this.innerChannelListener.EndAcceptChannel(result);
			return this.WrapInnerChannel(innerChannel);
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x001335B5 File Offset: 0x001317B5
		protected override bool OnEndWaitForChannel(IAsyncResult result)
		{
			return this.innerChannelListener.EndWaitForChannel(result);
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x001335C3 File Offset: 0x001317C3
		protected override bool OnWaitForChannel(TimeSpan timeout)
		{
			return this.innerChannelListener.WaitForChannel(timeout);
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x001335D1 File Offset: 0x001317D1
		private IInputChannel WrapInnerChannel(IDuplexChannel innerChannel)
		{
			if (innerChannel == null)
			{
				return null;
			}
			return new DuplexOneWayChannelListener.DuplexOneWayInputChannel(this, innerChannel);
		}

		// Token: 0x040032C3 RID: 12995
		private IChannelListener<IDuplexChannel> innerChannelListener;

		// Token: 0x040032C4 RID: 12996
		private bool packetRoutable;

		// Token: 0x02000D70 RID: 3440
		private class DuplexOneWayInputChannel : LayeredChannel<IDuplexChannel>, IInputChannel, IChannel, ICommunicationObject
		{
			// Token: 0x06007E14 RID: 32276 RVA: 0x001D6DE8 File Offset: 0x001D4FE8
			public DuplexOneWayInputChannel(DuplexOneWayChannelListener listener, IDuplexChannel innerChannel) : base(listener, innerChannel)
			{
				this.validateHeader = listener.packetRoutable;
			}

			// Token: 0x17001C1E RID: 7198
			// (get) Token: 0x06007E15 RID: 32277 RVA: 0x001D6DFE File Offset: 0x001D4FFE
			public EndpointAddress LocalAddress
			{
				get
				{
					return base.InnerChannel.LocalAddress;
				}
			}

			// Token: 0x06007E16 RID: 32278 RVA: 0x001D6E0B File Offset: 0x001D500B
			public IAsyncResult BeginReceive(AsyncCallback callback, object state)
			{
				return this.BeginReceive(base.DefaultReceiveTimeout, callback, state);
			}

			// Token: 0x06007E17 RID: 32279 RVA: 0x001D6E1B File Offset: 0x001D501B
			public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return base.InnerChannel.BeginReceive(timeout, callback, state);
			}

			// Token: 0x06007E18 RID: 32280 RVA: 0x001D6E2B File Offset: 0x001D502B
			public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return base.InnerChannel.BeginTryReceive(timeout, callback, state);
			}

			// Token: 0x06007E19 RID: 32281 RVA: 0x001D6E3B File Offset: 0x001D503B
			public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return base.InnerChannel.BeginWaitForMessage(timeout, callback, state);
			}

			// Token: 0x06007E1A RID: 32282 RVA: 0x001D6E4C File Offset: 0x001D504C
			public Message EndReceive(IAsyncResult result)
			{
				Message message = base.InnerChannel.EndReceive(result);
				return this.ValidateMessage(message);
			}

			// Token: 0x06007E1B RID: 32283 RVA: 0x001D6E70 File Offset: 0x001D5070
			public bool EndTryReceive(IAsyncResult result, out Message message)
			{
				bool result2 = base.InnerChannel.EndTryReceive(result, out message);
				message = this.ValidateMessage(message);
				return result2;
			}

			// Token: 0x06007E1C RID: 32284 RVA: 0x001D6E96 File Offset: 0x001D5096
			public bool EndWaitForMessage(IAsyncResult result)
			{
				return base.InnerChannel.EndWaitForMessage(result);
			}

			// Token: 0x06007E1D RID: 32285 RVA: 0x001D6EA4 File Offset: 0x001D50A4
			public Message Receive()
			{
				return this.Receive(base.DefaultReceiveTimeout);
			}

			// Token: 0x06007E1E RID: 32286 RVA: 0x001D6EB4 File Offset: 0x001D50B4
			public Message Receive(TimeSpan timeout)
			{
				Message message = base.InnerChannel.Receive(timeout);
				return this.ValidateMessage(message);
			}

			// Token: 0x06007E1F RID: 32287 RVA: 0x001D6ED8 File Offset: 0x001D50D8
			public bool TryReceive(TimeSpan timeout, out Message message)
			{
				bool result = base.InnerChannel.TryReceive(timeout, out message);
				message = this.ValidateMessage(message);
				return result;
			}

			// Token: 0x06007E20 RID: 32288 RVA: 0x001D6EFE File Offset: 0x001D50FE
			public bool WaitForMessage(TimeSpan timeout)
			{
				return base.InnerChannel.WaitForMessage(timeout);
			}

			// Token: 0x06007E21 RID: 32289 RVA: 0x001D6F0C File Offset: 0x001D510C
			private Message ValidateMessage(Message message)
			{
				if (this.validateHeader && message != null)
				{
					PacketRoutableHeader.ValidateMessage(message);
				}
				return message;
			}

			// Token: 0x0400485D RID: 18525
			private bool validateHeader;
		}
	}
}

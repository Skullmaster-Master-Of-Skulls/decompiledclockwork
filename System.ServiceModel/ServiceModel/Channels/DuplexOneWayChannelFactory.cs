using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000893 RID: 2195
	internal class DuplexOneWayChannelFactory : LayeredChannelFactory<IOutputChannel>
	{
		// Token: 0x0600535E RID: 21342 RVA: 0x001332C5 File Offset: 0x001314C5
		public DuplexOneWayChannelFactory(OneWayBindingElement bindingElement, BindingContext context) : base(context.Binding, context.BuildInnerChannelFactory<IDuplexChannel>())
		{
			this.innnerFactory = (IChannelFactory<IDuplexChannel>)base.InnerChannelFactory;
			this.packetRoutable = bindingElement.PacketRoutable;
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x001332F8 File Offset: 0x001314F8
		protected override IOutputChannel OnCreateChannel(EndpointAddress address, Uri via)
		{
			IDuplexChannel innerChannel = this.innnerFactory.CreateChannel(address, via);
			return new DuplexOneWayChannelFactory.DuplexOutputChannel(this, innerChannel);
		}

		// Token: 0x040032BC RID: 12988
		private IChannelFactory<IDuplexChannel> innnerFactory;

		// Token: 0x040032BD RID: 12989
		private bool packetRoutable;

		// Token: 0x02000D6D RID: 3437
		private class DuplexOutputChannel : OutputChannel
		{
			// Token: 0x06007DE6 RID: 32230 RVA: 0x001D6818 File Offset: 0x001D4A18
			public DuplexOutputChannel(DuplexOneWayChannelFactory factory, IDuplexChannel innerChannel) : base(factory)
			{
				this.packetRoutable = factory.packetRoutable;
				this.innerChannel = innerChannel;
			}

			// Token: 0x17001C19 RID: 7193
			// (get) Token: 0x06007DE7 RID: 32231 RVA: 0x001D6834 File Offset: 0x001D4A34
			public override EndpointAddress RemoteAddress
			{
				get
				{
					return this.innerChannel.RemoteAddress;
				}
			}

			// Token: 0x17001C1A RID: 7194
			// (get) Token: 0x06007DE8 RID: 32232 RVA: 0x001D6841 File Offset: 0x001D4A41
			public override Uri Via
			{
				get
				{
					return this.innerChannel.Via;
				}
			}

			// Token: 0x06007DE9 RID: 32233 RVA: 0x001D684E File Offset: 0x001D4A4E
			protected override void OnAbort()
			{
				this.innerChannel.Abort();
			}

			// Token: 0x06007DEA RID: 32234 RVA: 0x001D685B File Offset: 0x001D4A5B
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerChannel.BeginClose(timeout, callback, state);
			}

			// Token: 0x06007DEB RID: 32235 RVA: 0x001D686B File Offset: 0x001D4A6B
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerChannel.BeginOpen(timeout, callback, state);
			}

			// Token: 0x06007DEC RID: 32236 RVA: 0x001D687B File Offset: 0x001D4A7B
			protected override IAsyncResult OnBeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				this.StampMessage(message);
				return this.innerChannel.BeginSend(message, timeout, callback, state);
			}

			// Token: 0x06007DED RID: 32237 RVA: 0x001D6894 File Offset: 0x001D4A94
			protected override void OnClose(TimeSpan timeout)
			{
				this.innerChannel.Close(timeout);
			}

			// Token: 0x06007DEE RID: 32238 RVA: 0x001D68A2 File Offset: 0x001D4AA2
			protected override void OnEndClose(IAsyncResult result)
			{
				this.innerChannel.EndClose(result);
			}

			// Token: 0x06007DEF RID: 32239 RVA: 0x001D68B0 File Offset: 0x001D4AB0
			protected override void OnEndOpen(IAsyncResult result)
			{
				this.innerChannel.EndOpen(result);
			}

			// Token: 0x06007DF0 RID: 32240 RVA: 0x001D68BE File Offset: 0x001D4ABE
			protected override void OnEndSend(IAsyncResult result)
			{
				this.innerChannel.EndSend(result);
			}

			// Token: 0x06007DF1 RID: 32241 RVA: 0x001D68CC File Offset: 0x001D4ACC
			protected override void OnOpen(TimeSpan timeout)
			{
				this.innerChannel.Open(timeout);
			}

			// Token: 0x06007DF2 RID: 32242 RVA: 0x001D68DA File Offset: 0x001D4ADA
			protected override void OnSend(Message message, TimeSpan timeout)
			{
				this.StampMessage(message);
				this.innerChannel.Send(message, timeout);
			}

			// Token: 0x06007DF3 RID: 32243 RVA: 0x001D68F0 File Offset: 0x001D4AF0
			private void StampMessage(Message message)
			{
				if (this.packetRoutable)
				{
					PacketRoutableHeader.AddHeadersTo(message, null);
				}
			}

			// Token: 0x04004853 RID: 18515
			private IDuplexChannel innerChannel;

			// Token: 0x04004854 RID: 18516
			private bool packetRoutable;
		}
	}
}

using System;
using System.IO;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000892 RID: 2194
	internal class RequestOneWayChannelFactory : LayeredChannelFactory<IOutputChannel>
	{
		// Token: 0x0600535C RID: 21340 RVA: 0x0013326F File Offset: 0x0013146F
		public RequestOneWayChannelFactory(OneWayBindingElement bindingElement, BindingContext context) : base(context.Binding, context.BuildInnerChannelFactory<IRequestChannel>())
		{
			if (bindingElement.PacketRoutable)
			{
				this.packetRoutableHeader = PacketRoutableHeader.Create();
			}
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x00133298 File Offset: 0x00131498
		protected override IOutputChannel OnCreateChannel(EndpointAddress to, Uri via)
		{
			IRequestChannel innerChannel = ((IChannelFactory<IRequestChannel>)base.InnerChannelFactory).CreateChannel(to, via);
			return new RequestOneWayChannelFactory.RequestOutputChannel(this, innerChannel, this.packetRoutableHeader);
		}

		// Token: 0x040032BB RID: 12987
		private PacketRoutableHeader packetRoutableHeader;

		// Token: 0x02000D6C RID: 3436
		private class RequestOutputChannel : OutputChannel
		{
			// Token: 0x06007DD6 RID: 32214 RVA: 0x001D65FE File Offset: 0x001D47FE
			public RequestOutputChannel(ChannelManagerBase factory, IRequestChannel innerChannel, MessageHeader packetRoutableHeader) : base(factory)
			{
				this.innerChannel = innerChannel;
				this.packetRoutableHeader = packetRoutableHeader;
			}

			// Token: 0x17001C17 RID: 7191
			// (get) Token: 0x06007DD7 RID: 32215 RVA: 0x001D6615 File Offset: 0x001D4815
			public override EndpointAddress RemoteAddress
			{
				get
				{
					return this.innerChannel.RemoteAddress;
				}
			}

			// Token: 0x17001C18 RID: 7192
			// (get) Token: 0x06007DD8 RID: 32216 RVA: 0x001D6622 File Offset: 0x001D4822
			public override Uri Via
			{
				get
				{
					return this.innerChannel.Via;
				}
			}

			// Token: 0x06007DD9 RID: 32217 RVA: 0x001D662F File Offset: 0x001D482F
			protected override void OnAbort()
			{
				this.innerChannel.Abort();
			}

			// Token: 0x06007DDA RID: 32218 RVA: 0x001D663C File Offset: 0x001D483C
			protected override void OnOpen(TimeSpan timeout)
			{
				this.innerChannel.Open(timeout);
			}

			// Token: 0x06007DDB RID: 32219 RVA: 0x001D664A File Offset: 0x001D484A
			protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerChannel.BeginOpen(timeout, callback, state);
			}

			// Token: 0x06007DDC RID: 32220 RVA: 0x001D665A File Offset: 0x001D485A
			protected override void OnEndOpen(IAsyncResult result)
			{
				this.innerChannel.EndOpen(result);
			}

			// Token: 0x06007DDD RID: 32221 RVA: 0x001D6668 File Offset: 0x001D4868
			protected override void OnClose(TimeSpan timeout)
			{
				this.innerChannel.Close(timeout);
			}

			// Token: 0x06007DDE RID: 32222 RVA: 0x001D6676 File Offset: 0x001D4876
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerChannel.BeginClose(timeout, callback, state);
			}

			// Token: 0x06007DDF RID: 32223 RVA: 0x001D6686 File Offset: 0x001D4886
			protected override void OnEndClose(IAsyncResult result)
			{
				this.innerChannel.EndClose(result);
			}

			// Token: 0x06007DE0 RID: 32224 RVA: 0x001D6694 File Offset: 0x001D4894
			public override T GetProperty<T>()
			{
				T property = base.GetProperty<T>();
				if (property == null)
				{
					property = this.innerChannel.GetProperty<T>();
				}
				return property;
			}

			// Token: 0x06007DE1 RID: 32225 RVA: 0x001D66BD File Offset: 0x001D48BD
			protected override void AddHeadersTo(Message message)
			{
				base.AddHeadersTo(message);
				if (this.packetRoutableHeader != null)
				{
					PacketRoutableHeader.AddHeadersTo(message, this.packetRoutableHeader);
				}
			}

			// Token: 0x06007DE2 RID: 32226 RVA: 0x001D66DC File Offset: 0x001D48DC
			protected override void OnSend(Message message, TimeSpan timeout)
			{
				Message message2 = this.innerChannel.Request(message, timeout);
				using (message2)
				{
					this.ValidateResponse(message2);
				}
			}

			// Token: 0x06007DE3 RID: 32227 RVA: 0x001D671C File Offset: 0x001D491C
			protected override IAsyncResult OnBeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.innerChannel.BeginRequest(message, timeout, callback, state);
			}

			// Token: 0x06007DE4 RID: 32228 RVA: 0x001D6730 File Offset: 0x001D4930
			protected override void OnEndSend(IAsyncResult result)
			{
				Message message = this.innerChannel.EndRequest(result);
				using (message)
				{
					this.ValidateResponse(message);
				}
			}

			// Token: 0x06007DE5 RID: 32229 RVA: 0x001D6770 File Offset: 0x001D4970
			private void ValidateResponse(Message response)
			{
				if (response == null)
				{
					return;
				}
				if (response.Version == MessageVersion.None && response is NullMessage)
				{
					response.Close();
					return;
				}
				Exception innerException = null;
				if (response.IsFault)
				{
					try
					{
						MessageFault fault = MessageFault.CreateFault(response, 65536);
						innerException = new FaultException(fault);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						if (!(ex is CommunicationException) && !(ex is TimeoutException) && !(ex is XmlException) && !(ex is IOException))
						{
							throw;
						}
						innerException = ex;
					}
				}
				throw TraceUtility.ThrowHelperError(new ProtocolException(SR.GetString("OneWayUnexpectedResponse"), innerException), response);
			}

			// Token: 0x04004851 RID: 18513
			private IRequestChannel innerChannel;

			// Token: 0x04004852 RID: 18514
			private MessageHeader packetRoutableHeader;
		}
	}
}

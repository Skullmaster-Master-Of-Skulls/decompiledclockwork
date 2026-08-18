using System;
using System.Runtime;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000898 RID: 2200
	internal sealed class InternalDuplexChannelFactory : LayeredChannelFactory<IDuplexChannel>
	{
		// Token: 0x06005388 RID: 21384 RVA: 0x00133CA4 File Offset: 0x00131EA4
		internal InternalDuplexChannelFactory(InternalDuplexBindingElement bindingElement, BindingContext context, InputChannelDemuxer channelDemuxer, IChannelFactory<IOutputChannel> innerChannelFactory, LocalAddressProvider localAddressProvider) : base(context.Binding, innerChannelFactory)
		{
			this.channelDemuxer = channelDemuxer;
			this.innerChannelFactory = innerChannelFactory;
			ChannelDemuxerFilter filter = new ChannelDemuxerFilter(new MatchNoneMessageFilter(), int.MinValue);
			this.innerChannelListener = this.channelDemuxer.BuildChannelListener<IInputChannel>(filter);
			this.localAddressProvider = localAddressProvider;
			this.providesCorrelation = bindingElement.ProvidesCorrelation;
		}

		// Token: 0x06005389 RID: 21385 RVA: 0x00133D04 File Offset: 0x00131F04
		private bool CreateUniqueLocalAddress(out EndpointAddress address, out int priority)
		{
			long num = Interlocked.Increment(ref InternalDuplexChannelFactory.channelCount);
			if (num > 1L)
			{
				AddressHeader addressHeader = AddressHeader.CreateAddressHeader(XD.UtilityDictionary.UniqueEndpointHeaderName, XD.UtilityDictionary.UniqueEndpointHeaderNamespace, num);
				address = new EndpointAddress(this.innerChannelListener.Uri, new AddressHeader[]
				{
					addressHeader
				});
				priority = 1;
				return true;
			}
			address = new EndpointAddress(this.innerChannelListener.Uri, new AddressHeader[0]);
			priority = 0;
			return false;
		}

		// Token: 0x0600538A RID: 21386 RVA: 0x00133D80 File Offset: 0x00131F80
		protected override IDuplexChannel OnCreateChannel(EndpointAddress address, Uri via)
		{
			bool usesUniqueHeader = false;
			EndpointAddress localAddress;
			MessageFilter filter;
			int priority;
			if (this.localAddressProvider != null)
			{
				localAddress = this.localAddressProvider.LocalAddress;
				filter = this.localAddressProvider.Filter;
				priority = this.localAddressProvider.Priority;
			}
			else
			{
				usesUniqueHeader = this.CreateUniqueLocalAddress(out localAddress, out priority);
				filter = new MatchAllMessageFilter();
			}
			return this.CreateChannel(address, via, localAddress, filter, priority, usesUniqueHeader);
		}

		// Token: 0x0600538B RID: 21387 RVA: 0x00133DDA File Offset: 0x00131FDA
		public IDuplexChannel CreateChannel(EndpointAddress address, Uri via, MessageFilter filter, int priority, bool usesUniqueHeader)
		{
			return this.CreateChannel(address, via, new EndpointAddress(this.innerChannelListener.Uri, new AddressHeader[0]), filter, priority, usesUniqueHeader);
		}

		// Token: 0x0600538C RID: 21388 RVA: 0x00133E00 File Offset: 0x00132000
		public IDuplexChannel CreateChannel(EndpointAddress remoteAddress, Uri via, EndpointAddress localAddress, MessageFilter filter, int priority, bool usesUniqueHeader)
		{
			ChannelDemuxerFilter filter2 = new ChannelDemuxerFilter(new AndMessageFilter(new EndpointAddressMessageFilter(localAddress, true), filter), priority);
			IDuplexChannel duplexChannel = null;
			IOutputChannel outputChannel = null;
			IChannelListener<IInputChannel> channelListener = null;
			IInputChannel inputChannel = null;
			try
			{
				outputChannel = this.innerChannelFactory.CreateChannel(remoteAddress, via);
				channelListener = this.channelDemuxer.BuildChannelListener<IInputChannel>(filter2);
				channelListener.Open();
				inputChannel = channelListener.AcceptChannel();
				duplexChannel = new InternalDuplexChannelFactory.ClientCompositeDuplexChannel(this, inputChannel, channelListener, localAddress, outputChannel, usesUniqueHeader);
			}
			finally
			{
				if (duplexChannel == null)
				{
					if (outputChannel != null)
					{
						outputChannel.Close();
					}
					if (channelListener != null)
					{
						channelListener.Close();
					}
					if (inputChannel != null)
					{
						inputChannel.Close();
					}
				}
			}
			return duplexChannel;
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x00133E98 File Offset: 0x00132098
		protected override void OnAbort()
		{
			base.OnAbort();
			this.innerChannelListener.Abort();
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x00133EAC File Offset: 0x001320AC
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnOpen(timeoutHelper.RemainingTime());
			this.innerChannelListener.Open(timeoutHelper.RemainingTime());
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x00133EE0 File Offset: 0x001320E0
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedOpenAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginOpen), new ChainedEndHandler(base.OnEndOpen), new ICommunicationObject[]
			{
				this.innerChannelListener
			});
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x00133F1C File Offset: 0x0013211C
		protected override void OnEndOpen(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x00133F24 File Offset: 0x00132124
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnClose(timeoutHelper.RemainingTime());
			this.innerChannelListener.Close(timeoutHelper.RemainingTime());
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x00133F58 File Offset: 0x00132158
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedCloseAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), new ICommunicationObject[]
			{
				this.innerChannelListener
			});
		}

		// Token: 0x06005393 RID: 21395 RVA: 0x00133F94 File Offset: 0x00132194
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06005394 RID: 21396 RVA: 0x00133F9C File Offset: 0x0013219C
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IChannelListener))
			{
				return (T)((object)this.innerChannelListener);
			}
			if (typeof(T) == typeof(ISecurityCapabilities) && !this.providesCorrelation)
			{
				return InternalDuplexBindingElement.GetSecurityCapabilities<T>(base.GetProperty<ISecurityCapabilities>());
			}
			T property = base.GetProperty<T>();
			if (property != null)
			{
				return property;
			}
			IChannelListener channelListener = this.innerChannelListener;
			if (channelListener != null)
			{
				return channelListener.GetProperty<T>();
			}
			return default(T);
		}

		// Token: 0x040032D3 RID: 13011
		private static long channelCount;

		// Token: 0x040032D4 RID: 13012
		private InputChannelDemuxer channelDemuxer;

		// Token: 0x040032D5 RID: 13013
		private IChannelFactory<IOutputChannel> innerChannelFactory;

		// Token: 0x040032D6 RID: 13014
		private IChannelListener<IInputChannel> innerChannelListener;

		// Token: 0x040032D7 RID: 13015
		private LocalAddressProvider localAddressProvider;

		// Token: 0x040032D8 RID: 13016
		private bool providesCorrelation;

		// Token: 0x02000D73 RID: 3443
		private class ClientCompositeDuplexChannel : LayeredDuplexChannel
		{
			// Token: 0x06007E3A RID: 32314 RVA: 0x001D753C File Offset: 0x001D573C
			public ClientCompositeDuplexChannel(ChannelManagerBase channelManager, IInputChannel innerInputChannel, IChannelListener<IInputChannel> innerInputListener, EndpointAddress localAddress, IOutputChannel innerOutputChannel, bool usesUniqueHeader) : base(channelManager, innerInputChannel, localAddress, innerOutputChannel)
			{
				this.innerInputListener = innerInputListener;
				this.usesUniqueHeader = usesUniqueHeader;
			}

			// Token: 0x06007E3B RID: 32315 RVA: 0x001D7559 File Offset: 0x001D5759
			protected override void OnAbort()
			{
				base.OnAbort();
				this.innerInputListener.Abort();
			}

			// Token: 0x06007E3C RID: 32316 RVA: 0x001D756C File Offset: 0x001D576C
			protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), new ChainedBeginHandler(this.innerInputListener.BeginClose), new ChainedEndHandler(this.innerInputListener.EndClose));
			}

			// Token: 0x06007E3D RID: 32317 RVA: 0x001D75C0 File Offset: 0x001D57C0
			protected override void OnClose(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.OnClose(timeoutHelper.RemainingTime());
				this.innerInputListener.Close(timeoutHelper.RemainingTime());
			}

			// Token: 0x06007E3E RID: 32318 RVA: 0x001D75F4 File Offset: 0x001D57F4
			protected override void OnEndClose(IAsyncResult result)
			{
				ChainedAsyncResult.End(result);
			}

			// Token: 0x06007E3F RID: 32319 RVA: 0x001D75FC File Offset: 0x001D57FC
			protected override void OnReceive(Message message)
			{
				if (this.usesUniqueHeader)
				{
					for (int i = 0; i < message.Headers.Count; i++)
					{
						if (message.Headers[i].Name == XD.UtilityDictionary.UniqueEndpointHeaderName.Value && message.Headers[i].Namespace == XD.UtilityDictionary.UniqueEndpointHeaderNamespace.Value)
						{
							message.Headers.AddUnderstood(i);
						}
					}
				}
			}

			// Token: 0x04004869 RID: 18537
			private IChannelListener<IInputChannel> innerInputListener;

			// Token: 0x0400486A RID: 18538
			private bool usesUniqueHeader;
		}
	}
}
